Option Compare Text
Imports Telerik.WebControls
Imports System.Drawing

Partial Class Forms_Admin_frm_POI_Team
    Inherits System.Web.UI.Page
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblViewAuthError.Visible = False

        Dim al As New ArrayList
        ''===================Search Parameters======================
        With al
            .Add(txtNumber.Text.Trim)
        End With

        Dim dt As DataTable = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_SearchPOITeam", al)

        Me.Panel2.Visible = False
        If dt.Rows.Count > 0 Then
            'keep project code
            ViewState("ViewAuthProject") = dt.Rows(0)("Project_Code").ToString
            '------------------
            lblProjectDesc.ForeColor = Drawing.Color.Black
            lblProjectDesc.Text = dt.Rows(0)("Project_Desc").ToString
            Session("dgPOI") = dt
            dgSigningAuthorities.CurrentPageIndex = 0
            If Not dt.Rows(0).IsNull("UCID") Then
                RebindGenericSearchList()
            Else
                dgSigningAuthorities.DataSource = Nothing
                dgSigningAuthorities.DataBind()
            End If
            Panel1.Visible = True
        Else
            ViewState("ViewAuthProject") = Nothing
            Session("dgPOI") = Nothing
            lblProjectDesc.ForeColor = Drawing.Color.Red
            lblProjectDesc.Text = "Project does not exist"
            Panel1.Visible = False
        End If

    End Sub
    Private Sub RebindGenericSearchList()
        If Not Session("dgPOI") Is Nothing Then
            Dim dv As DataView = CType(Session("dgPOI"), DataTable).DefaultView
            dgSigningAuthorities.DataSource = dv
            dgSigningAuthorities.DataBind()
        End If
    End Sub
    Private Sub dgSigningAuthorities_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSigningAuthorities.PageIndexChanged, dgSigningAuthorities.PageIndexChanged
        Me.dgSigningAuthorities.CurrentPageIndex = e.NewPageIndex
        RebindGenericSearchList()
    End Sub

    Protected Sub dgSigningAuthorities_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSigningAuthorities.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim status As String = CType(e.Item.FindControl("Status"), Label).Text
            Dim lbtActive As LinkButton = CType(e.Item.FindControl("lbtActive"), LinkButton)
            If status = "Active" Then
                lbtActive.Text = "[Inactivate]"
                e.Item.BackColor = Color.FromArgb(83, 196, 84)
            Else
                lbtActive.Text = "[Activate]"
                e.Item.BackColor = Color.FromArgb(196, 94, 83)
            End If

        End If
    End Sub

    Protected Sub dgSigningAuthorities_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSigningAuthorities.EditCommand
        lblViewAuthError.Visible = False
        Dim lbtActive As LinkButton = CType(e.Item.FindControl("lbtActive"), LinkButton)
        statusChange(ViewState("ViewAuthProject"), CStr(dgSigningAuthorities.DataKeys(e.Item.ItemIndex)), lbtActive.Text)
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Protected Sub statusChange(ByVal projectCode As String, ByVal ucid As String, ByVal status As String)
        Dim admin As AppUser = CType(Session("user"), AppUser)

        Dim a2 As New ArrayList
        With a2
            .Add(projectCode)
            .Add(ucid)
            If status = "[Activate]" Then
                .Add("Active")
            Else
                .Add("Inactive")
            End If
            .Add(admin.Ucid)
        End With
        DAL.GenericDBOperation.GenericDataBaseOperationDB("UPDATE", "dbo.eFinsp_POITeamUpdate", a2)
    End Sub


    Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        lblViewAuthError.Visible = False
        txtUCID.Text = ""
        txtFamilyName.Text = ""
        txtGivenName.Text = ""
        Me.Panel2.Visible = True
        btnAddNew.Enabled = False
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        lblViewAuthError.Visible = False
        Me.Panel2.Visible = False
        btnAddNew.Enabled = False
    End Sub

    Protected Sub btnRetrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetrieve.Click
        lblViewAuthError.Visible = False
        btnAddNew.Enabled = False
        If txtUCID.Text.Trim <> "" Then
            Dim data As HR = HR.GetPersonalInfo(txtUCID.Text)
            If Not data Is Nothing Then
                If Not data.ErrorMsg Is Nothing OrElse data.ErrorMsg <> "" Then
                    lblViewAuthError.Visible = True
                    lblViewAuthError.Text = data.ErrorMsg
                Else
                    With data
                        Me.txtFamilyName.Text = .FamilyName
                        Me.txtGivenName.Text = .GivenName
                    End With
                    btnAddNew.Enabled = True
                End If
            Else
                lblViewAuthError.Text = "UCID entered does not exist"
                lblViewAuthError.Visible = True
                btnAddNew.Enabled = False
            End If
        End If

    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click

        If txtUCID.Text.Trim <> "" Then
            Dim data As HR = HR.GetPersonalInfo(txtUCID.Text)
            If Not data Is Nothing Then
                If Not data.ErrorMsg Is Nothing OrElse data.ErrorMsg <> "" Then
                    lblViewAuthError.Visible = True
                    lblViewAuthError.Text = data.ErrorMsg
                Else
                    With data
                        Me.txtFamilyName.Text = .FamilyName
                        Me.txtGivenName.Text = .GivenName
                    End With
                    If Not memberExist(txtUCID.Text) Then
                        savePOIMember(ViewState("ViewAuthProject"), txtUCID.Text)
                        lblViewAuthError.Visible = False
                        btnAddNew.Enabled = False
                        Me.Panel2.Visible = False

                    Else
                        lblViewAuthError.Text = "The POI entered already exists"
                        btnAddNew.Enabled = False
                        lblViewAuthError.Visible = True
                    End If


                End If
            Else
                lblViewAuthError.Text = "UCID entered does not exist"
                lblViewAuthError.Visible = True
                btnAddNew.Enabled = False
            End If
        End If

    End Sub

    Protected Function memberExist(ByVal ucid As String) As Boolean
        Dim dt As DataTable = CType(Session("dgPOI"), DataTable)
        For Each dr As DataRow In dt.Rows
            If dr("UCID").ToString = ucid Then
                Return True
            End If
        Next
        Return False
    End Function

    Protected Sub savePOIMember(ByVal projectCode As String, ByVal ucid As String)
        Dim admin As AppUser = CType(Session("user"), AppUser)
        Dim a2 As New ArrayList
        With a2
            .Add(projectCode)
            .Add(ucid)
            .Add("Active")
            .Add(admin.Ucid)
        End With
        DAL.GenericDBOperation.GenericDataBaseOperationDB("INSERT", "dbo.eFinsp_POITeamInsert", a2)
        btnSearch_Click(Nothing, Nothing)
    End Sub
End Class
