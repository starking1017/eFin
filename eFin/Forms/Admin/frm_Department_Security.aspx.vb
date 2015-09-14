Option Compare Text

Partial Class Forms_Admin_frm_Department_Security
    Inherits System.Web.UI.Page

    Protected FacultyDeptAuthorization As New FacultyDeptAuthorization

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("DepartTable") Is Nothing Then
                Session("DepartTable") = FacultyDeptAuthorization.DepartmentTable()
            End If
            If Session("FacultyTable") Is Nothing Then
                Session("FacultyTable") = FacultyDeptAuthorization.FacultyTable()
            End If

            cboSearchFaculty.DataSource = Session("FacultyTable")
            cboSearchFaculty.DataTextField = "Dept_L5_Desc"
            cboSearchFaculty.DataValueField = "Dept_L5_Code"
            cboSearchFaculty.DataBind()
            Dim fstItem As ListItem = New ListItem("--- ALL ---", "")
            cboSearchFaculty.Items.Insert(0, fstItem)

            cboFaculty.DataSource = Session("FacultyTable")
            cboFaculty.DataTextField = "Dept_L5_Desc"
            cboFaculty.DataValueField = "Dept_L5_Code"
            cboFaculty.DataBind()
            fstItem = New ListItem("Not Applicable", "NA")
            Me.cboFaculty.Items.Insert(0, fstItem)
            fstItem = New ListItem("ALL", "ALL")
            cboFaculty.Items.Insert(0, fstItem)

            cboSearchDepartment.DataSource = Session("DepartTable")
            cboSearchDepartment.DataTextField = "Dept_Desc"
            cboSearchDepartment.DataValueField = "Dept_ID"
            cboSearchDepartment.DataBind()
            fstItem = New ListItem("--- ALL ---", "")
            cboSearchDepartment.Items.Insert(0, fstItem)

            cboDepartment.DataSource = Session("DepartTable")
            cboDepartment.DataTextField = "Dept_Desc"
            cboDepartment.DataValueField = "Dept_ID"
            cboDepartment.DataBind()
            fstItem = New ListItem("Not Applicable", "NA")
            Me.cboDepartment.Items.Insert(0, fstItem)
            fstItem = New ListItem("ALL", "ALL")
            cboDepartment.Items.Insert(0, fstItem)
            Dim admin As AppUser = CType(Session("user"), AppUser)

            If admin.RoleId <> 1 Then
                btnAddNew.Enabled = False
            End If
        End If
    End Sub

    Private Sub BindList()
        With FacultyDeptAuthorization
            .UCID = txtSearchUCID.Text.Trim
            .LastName = Tools.DataCleaner.ChangeSingleQuoteToEmpty(txtSearchFamilyName.Text.Trim)
            .FirstName = Tools.DataCleaner.ChangeSingleQuoteToEmpty(txtSearchGivenName.Text.Trim)
            .FacultyID = cboSearchFaculty.SelectedValue
            .DepartmentID = cboSearchDepartment.SelectedValue

            Dim dt As DataTable
            dt = .Search()
            Session("dtDepartFacAuth") = dt
        End With

        dgList.Visible = True
        dgList.CurrentPageIndex = 0
        RebindList()

    End Sub
    Private Sub RebindList()
        Dim dt As DataTable
        If Not Session("dtDepartFacAuth") Is Nothing Then
            dt = CType(Session("dtDepartFacAuth"), DataTable)
            Dim dv As DataView = dt.DefaultView

            If lblSort.Text.Trim <> "" Then
                dv.Sort = lblSort.Text.Trim
            End If

            dgList.DataSource = dv
            dgList.DataBind()
        End If
    End Sub
    Private Sub dgList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgList.ItemCommand
        Select Case e.CommandName

            Case "Delete"
                Dim lbl As Label
                Dim UCID As String = ""
                Dim FacultyID As String = ""
                Dim DepartmentID As String = ""

                lbl = e.Item.FindControl("lblUCID")
                If Not lbl Is Nothing Then
                    UCID = lbl.Text.Trim
                End If

                lbl = e.Item.FindControl("lblFacultyID")
                If Not lbl Is Nothing Then
                    FacultyID = lbl.Text.Trim
                End If

                lbl = e.Item.FindControl("lblDepartmentID")
                If Not lbl Is Nothing Then
                    DepartmentID = lbl.Text.Trim
                End If

                With FacultyDeptAuthorization
                    .Ucid = UCID
                    .FacultyID = FacultyID
                    .DepartmentID = DepartmentID
                    .DeleteeFinDepartFacAuthorization()
                End With

                BindList()
                btnCancel_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub dgList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgList.ItemDataBound
        Dim lbtn As LinkButton
        Dim admin As AppUser = CType(Session("user"), AppUser)


        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            lbtn = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            If Not lbtn Is Nothing Then
                lbtn.Attributes.Add("onclick", "return getconfirm();")
                If admin.RoleId <> 1 Then
                    lbtn.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub dgList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        dgList.CurrentPageIndex = e.NewPageIndex
        RebindList()
    End Sub

    Private Sub dgList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgList.SortCommand
        If String.Compare(lblSort.Text, e.SortExpression, True) = 0 Then
            lblSort.Text = e.SortExpression + " DESC"
        Else
            lblSort.Text = e.SortExpression
        End If

        RebindList()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'Show panSearch, Hide panAddNew
        PanVisible(True, False)
        ResetPanAddNew()
    End Sub

    Private Sub ResetPanAddNew()
        txtUCID.Text = ""
        txtUCID.ReadOnly = False
        txtFamilyName.Text = ""
        txtGivenName.Text = ""
        cboFaculty.SelectedIndex = 0
        cboFaculty.Enabled = True
        cboDepartment.SelectedIndex = 0
        cboDepartment.Enabled = True
    End Sub
    Private Sub ResetPanSearch()
        lblMsg.Text = String.Empty

        txtSearchUCID.Text = ""
        txtSearchFamilyName.Text = ""
        txtSearchGivenName.Text = ""
        cboSearchFaculty.SelectedIndex = 0
        cboSearchDepartment.SelectedIndex = 0
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        lblAddNewMsg.Text = ""

        With FacultyDeptAuthorization
            .Ucid = txtUCID.Text.Trim
            .LastName = txtFamilyName.Text.Trim
            .FirstName = txtGivenName.Text.Trim
            .FacultyID = cboFaculty.SelectedValue
            .Faculty = cboFaculty.SelectedItem.Text.Trim
            .DepartmentID = cboDepartment.SelectedValue
            .Department = cboDepartment.SelectedItem.Text.Trim

            .ValidateData()
            If .ValidationMessage = "" Then
                'Check duplicated Department & Faculty Authorization
                If .IsExist() Then
                    lblAddNewMsg.Text = "Department & Faculty Authorization exists"
                    lblAddNewMsg.Visible = True
                    Exit Sub
                End If

                If .AddNeweFinDepartFacAuthorization Then
                    'Show message
                    lblMsg.ForeColor = Drawing.Color.Green
                    Me.lblMsg.Text = "New Department & Faculty Authorization added on " + Date.Now.ToLongDateString + " @ " + Date.Now.ToShortTimeString
                Else
                    lblMsg.ForeColor = Drawing.Color.Red
                    Me.lblMsg.Text = "Adding new Department & Faculty Authorization failed on " + Date.Now.ToLongDateString + " @ " + Date.Now.ToShortTimeString
                End If

                BindList()
                btnCancel_Click(Nothing, Nothing)

            Else
                Me.lblAddNewMsg.Visible = True
                Me.lblAddNewMsg.Text = .ValidationMessage
            End If

            Me.lblMsg.Visible = True
        End With
    End Sub

    Protected Sub btnRetrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetrieve.Click
        'ReadDataFromOracledb(Me.txtUCID.Text.Trim)
        'added by jack on August 6, 2009 fixing the bug
        Me.lblAddNewMsg.Visible = False
        Me.lblAddNewMsg.Text = ""
        '--------------------------------------

        GetPersonalInfo(Me.txtUCID.Text.Trim)
    End Sub

    Private Sub GetPersonalInfo(ByVal UCID As String)
        Try
            Dim data As HR = HR.GetPersonalInfo(UCID)
            If Not data Is Nothing Then
                If Not data.ErrorMsg Is Nothing OrElse data.ErrorMsg <> "" Then
                    lblAddNewMsg.Visible = True
                    lblAddNewMsg.Text = data.ErrorMsg
                End If

                With data
                    Me.txtFamilyName.Text = .FamilyName
                    Me.txtGivenName.Text = .GivenName
                    Me.cboFaculty.SelectedValue = .FacultyID
                    Me.cboDepartment.SelectedValue = .DepartmentCode
                End With
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = String.Empty

        BindList()
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Me.panSearch.Visible = False
        Me.PanAddNew.Visible = True

        Me.lblAddNewMsg.Text = ""
        Me.lblTitle.Text = "Add New Department & Faculty Viewing Authority"
        Me.lblTitle.Visible = True
        Me.lblEditState.Text = "New"
        Me.lblEditState.Visible = False
        Me.lblMsg.Text = String.Empty
        Me.btnSave.Text = "Add New"

        ResetPanAddNew()

        'Show panAddNew, Hide panSearch
        PanVisible(False, True)
        btnSave.Enabled = False
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ResetPanSearch()
    End Sub
    Private Sub PanVisible(ByVal PanSearchVisible As Boolean, ByVal PanAddNewVisible As Boolean)
        Me.panSearch.Visible = PanSearchVisible
        Me.PanAddNew.Visible = PanAddNewVisible
    End Sub
End Class
