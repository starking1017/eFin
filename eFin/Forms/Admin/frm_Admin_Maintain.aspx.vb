Option Compare Text
Imports System.Collections.Generic

Partial Class Forms_Admin_frm_Admin_Maintain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ddlDepartment.DataSource = FacultyDeptAuthorization.DepartmentTable()
            ddlDepartment.DataTextField = "Dept_Desc"
            ddlDepartment.DataValueField = "Dept_ID"
            ddlDepartment.DataBind()
            LoadAdmin()
        End If
    End Sub
    Private Sub LoadAdmin()
        Session("alladmins") = AppUser.GetAllAdmins()
        dgList.DataSource = CType(Session("alladmins"), List(Of AppUser))
        dgList.DataBind()
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        PanAddNew.Visible = True
        panSearch.Visible = False
        lbMessage.Text = "Add New User"
        txtUcid.Enabled = True
        btSearch.Visible = True
        txtUcid.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        ddlDepartment.SelectedIndex = 0
        rblRole.SelectedValue = 1
        cbActive.Checked = True

        btSave.Enabled = False
    End Sub

    Protected Sub dgList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgList.ItemDataBound
        Dim lbtn As LinkButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            lbtn = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            If Not lbtn Is Nothing Then
                lbtn.Attributes.Add("onclick", "return confirm('Confirm delete?');")
            End If
        End If
    End Sub

    Protected Sub dgList_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgList.DeleteCommand
        CType(Session("alladmins"), List(Of AppUser))(dgList.CurrentPageIndex * dgList.PageSize + e.Item.ItemIndex).RemoveMe()
        dgList.CurrentPageIndex = 0
        LoadAdmin()
    End Sub

    Protected Sub dgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        dgList.CurrentPageIndex = e.NewPageIndex
        LoadAdmin()
    End Sub

    Protected Sub dgList_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgList.EditCommand
        Dim admin As AppUser = CType(Session("alladmins"), List(Of AppUser))(dgList.CurrentPageIndex * dgList.PageSize + e.Item.ItemIndex)
        PanAddNew.Visible = True
        panSearch.Visible = False
        lbMessage.Text = "Edit User"
        txtUcid.Enabled = False
        btSearch.Visible = False

        txtUcid.Text = admin.Ucid
        txtFirstName.Text = admin.FirstName
        txtLastName.Text = admin.LastName
        ddlDepartment.ClearSelection()
        If ddlDepartment.Items.FindByValue(admin.DepartmentID) IsNot Nothing Then
            ddlDepartment.Items.FindByValue(admin.DepartmentID).Selected = True
        End If
        rblRole.SelectedValue = admin.RoleId
        cbActive.Checked = admin.IsActive
        btSave.Enabled = True
    End Sub

    Protected Sub btSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSearch.Click
        Me.lblAddNewMsg.Visible = False
        Me.lblAddNewMsg.Text = ""
        '--------------------------------------

        GetPersonalInfo(Me.txtUcid.Text.Trim)
    End Sub
    Private Function GetPersonalInfo(ByVal UCID As String)
        Try
            Dim data As HR = HR.GetPersonalInfo(UCID)
            If Not data Is Nothing Then
                If Not data.ErrorMsg Is Nothing OrElse data.ErrorMsg <> "" Then
                    lblAddNewMsg.Visible = True
                    lblAddNewMsg.Text = data.ErrorMsg
                End If

                With data
                    Me.txtLastName.Text = .FamilyName
                    Me.txtFirstName.Text = .GivenName
                    Me.ddlDepartment.SelectedValue = .DepartmentCode
                End With
                btSave.Enabled = True
            Else
                btSave.Enabled = False
            End If

        Catch ex As Exception
        End Try
    End Function
    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSave.Click
        Dim admin As New AppUser
        admin.Ucid = txtUcid.Text
        admin.FirstName = txtFirstName.Text
        admin.LastName = txtLastName.Text
        admin.DepartmentID = ddlDepartment.SelectedValue
        admin.Department = ddlDepartment.SelectedItem.Text
        admin.RoleId = rblRole.SelectedValue
        admin.RoleName = rblRole.SelectedItem.Text
        admin.IsActive = cbActive.Checked

        Dim mess As String = ValidateData(admin)

        If mess = "" Then
            admin.SaveMe()
            btCancel_Click(Nothing, Nothing)
        Else
            Me.lblAddNewMsg.Visible = True
            Me.lblAddNewMsg.Text = mess
        End If
    End Sub

    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btCancel.Click
        PanAddNew.Visible = False
        panSearch.Visible = True
        LoadAdmin()
    End Sub
    Public Function ValidateData(ByVal admin As AppUser) As String
        'function validate user input

        'check UCID
        If admin.Ucid = "" Or admin.Ucid = Nothing Then
            Return "UCID required "
        End If


        If admin.DepartmentID = "" Then
            Return "Select Department"
        End If

        'added by jack on JUly 5, 2009 for mutiple ucids issue
        Dim data As HR = HR.GetPersonalInfo(admin.Ucid)
        If data Is Nothing Then
            Return "Invalid UCID "
        Else
            admin.LastName = data.FamilyName
            admin.FirstName = data.GivenName
        End If
        '---------------------------

        Return ""

    End Function
End Class
