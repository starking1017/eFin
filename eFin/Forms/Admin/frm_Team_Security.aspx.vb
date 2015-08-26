Option Compare Text
Imports System.Drawing


Partial Class Forms_Researcher_frm_Security
    Inherits System.Web.UI.Page

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblViewAuthError.Visible = False

        Dim al As New ArrayList
        ''===================Search Parameters======================
        With al
            .Add(txtNumber.Text.Trim)
        End With

        Dim dt As DataTable = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_SearchTeam", al)

        Me.Panel2.Visible = False
        If dt.Rows.Count > 0 Then
            'keep project code
            ViewState("ViewAuthProject") = dt.Rows(0)("Project_Code").ToString
            lbParent.Text = dt.Rows(0)("Parent_Flag").ToString
            '------------------
            lblProjectDesc.ForeColor = Drawing.Color.Black
            lblProjectDesc.Text = dt.Rows(0)("Project_Desc").ToString
            Session("dgGenericSearchList") = dt
            dgSigningAuthorities.CurrentPageIndex = 0
            RebindGenericSearchList()
            Panel1.Visible = True
        Else
            ViewState("ViewAuthProject") = Nothing
            lbParent.Text = "N"
            Session("dgGenericSearchList") = Nothing
            lblProjectDesc.ForeColor = Drawing.Color.Red
            lblProjectDesc.Text = "Project not exist or no eFIN viewers found for this project"
            Panel1.Visible = False
        End If

    End Sub
    Private Sub RebindGenericSearchList()
        If Not Session("dgGenericSearchList") Is Nothing Then
            Dim dv As DataView = CType(Session("dgGenericSearchList"), DataTable).DefaultView
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
            Dim lbtEdit As LinkButton = CType(e.Item.FindControl("lbtEdit"), LinkButton)
            If status = "A" Then
                lbtEdit.Enabled = True
                e.Item.BackColor = Color.FromArgb(83, 196, 84)
            Else
                lbtEdit.Enabled = False
                e.Item.BackColor = Color.FromArgb(196, 94, 83)
            End If

        End If
    End Sub

    Protected Sub dgSigningAuthorities_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSigningAuthorities.EditCommand
        '----------------------------------------------------------------------------------
        'Keep viewing authority UCID and project code
        lblViewAuthError.Visible = False

        ViewState("ViewAuthUCID") = CStr(dgSigningAuthorities.DataKeys(e.Item.ItemIndex))
        ViewState("MemberType") = CType(e.Item.FindControl("lbType"), Label).Text.Trim
        ViewState("GivenName") = CType(e.Item.FindControl("lblGivenName"), Label).Text.Trim
        ViewState("FamilyName") = CType(e.Item.FindControl("lblFamilyName"), Label).Text.Trim
        '----------------------------------------------------------------------------------
        Dim str, TeamUcid, Name As String

        Name = CType(e.Item.FindControl("lblGivenName"), Label).Text.Trim & " " & _
               CType(e.Item.FindControl("lblFamilyName"), Label).Text.Trim

        TeamUcid = CStr(dgSigningAuthorities.DataKeys(e.Item.ItemIndex))

        str = "UCID: " & TeamUcid & "<br />" & "Name: " & Name
        Me.Panel2.Visible = True
        RebindViewingAuthority()
        Me.lblViewAuthHeader.Visible = True
        Me.lblViewAuthHeader.Text = str

    End Sub
    Private Sub RebindViewingAuthority()
        'Rebind Viewing Authority Data Grid

        Dim al As New ArrayList
        ''===================Search Parameters======================
        With al
            .Add(ViewState("ViewAuthUCID").ToString.Trim)
            .Add(ViewState("ViewAuthProject").ToString)
        End With
        Session("dgEfinSecurity") = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_EfinSecuritySearch", al)
        Dim dt1 As DataTable = Session("dgEfinSecurity")
        Panel2.Visible = True
        dgViewingAuth.DataSource = dt1
        dgViewingAuth.DataBind()

    End Sub
    Private Sub dgViewingAuth_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgViewingAuth.ItemDataBound
        Dim cbo As DropDownList

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lbtn As LinkButton
            lbtn = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            If Not lbtn Is Nothing Then
                lbtn.Attributes.Add("onclick", "return getconfirm();")
            End If

            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#eeeded'")
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            cbo = CType(e.Item.FindControl("cboNewAccGroup"), DropDownList)
            If Not cbo Is Nothing Then
                cbo.DataSource = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_LoadAccountGroup", Nothing)
                cbo.DataTextField = "fld_desc"
                cbo.DataValueField = "fld_id"
                cbo.DataBind()
                Dim fstItem As ListItem = New ListItem(" ", " ")
                cbo.Items.Insert(0, fstItem)
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem Then
            cbo = CType(e.Item.FindControl("cboEditAccGroup"), DropDownList)
            If Not cbo Is Nothing Then
                cbo.DataSource = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_LoadAccountGroup", Nothing)
                cbo.DataTextField = "fld_desc"
                cbo.DataValueField = "fld_id"
                cbo.DataBind()
                Dim fstItem As ListItem = New ListItem(" ", " ")
                cbo.Items.Insert(0, fstItem)
                cbo.SelectedValue = e.Item.DataItem("AccountGroup")
            End If
        End If
    End Sub

    Protected Sub dgViewingAuth_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgViewingAuth.ItemCommand

        Dim dt As DataTable
        Dim dr As DataRow
        Dim al As New ArrayList
        lblViewAuthError.Visible = False

        Select Case e.CommandName
            Case "Edit"

                ''===================Search Parameters======================
                With al
                    .Add(ViewState("ViewAuthUCID").ToString.Trim)
                    .Add(ViewState("ViewAuthProject").ToString)
                    .Add(CType(e.Item.FindControl("lblAccountGroup"), Label).Text)
                    .Add(CType(e.Item.FindControl("lblAccount"), Label).Text)
                    .Add(CType(e.Item.FindControl("lblActivity"), Label).Text)
                End With
                ViewState("Values") = al

                Me.dgViewingAuth.EditItemIndex = e.Item.ItemIndex
                RebindViewingAuthority()

            Case "EditCancel"
                Me.dgViewingAuth.EditItemIndex = -1
                RebindViewingAuthority()

            Case "Update"
                Dim cboAcctGroup As DropDownList = CType(e.Item.FindControl("cboEditAccGroup"), DropDownList)
                Dim lblAccount As TextBox = CType(e.Item.FindControl("txtEditAccount"), TextBox)
                Dim lblActivity As TextBox = CType(e.Item.FindControl("txtEditActivity"), TextBox)

                '============================ Account Group/Account validation==========================
                If cboAcctGroup.SelectedValue.Trim <> "" And cboAcctGroup.SelectedValue.Trim <> "NA" Then
                    If lblAccount.Text.Trim <> "" And lblAccount.Text.Trim <> "NA" Then
                        lblViewAuthError.Visible = True
                        lblViewAuthError.Text = "Please enter Account group or Account"
                        Exit Sub
                    Else
                        lblViewAuthError.Visible = False
                    End If
                End If
                '======================================================================================
                If Not cboAcctGroup Is Nothing AndAlso Not lblAccount Is Nothing AndAlso _
                    Not lblActivity Is Nothing Then

                    Dim AccGroup, _Account, _Activity As String

                    If (cboAcctGroup.SelectedValue.Trim = "" Or cboAcctGroup.SelectedValue.Trim = "ALL") And lblAccount.Text.Trim = "" Then
                        AccGroup = "ALL"
                        _Account = "ALL"
                    ElseIf (cboAcctGroup.SelectedValue.Trim = "NA") And lblAccount.Text.Trim = "" Then
                        AccGroup = "NA"
                        _Account = "ALL"

                        'Case 2: AccountGroup <>"" Account="" 
                    ElseIf cboAcctGroup.SelectedValue.Trim <> "" And lblAccount.Text.Trim = "" Then
                        AccGroup = cboAcctGroup.SelectedValue.Trim
                        _Account = "NA"

                        'Case 3: AccountGroup ="" Account<>""
                    ElseIf cboAcctGroup.SelectedValue.Trim = "" And lblAccount.Text.Trim <> "" Then
                        AccGroup = "NA"
                        _Account = lblAccount.Text.Trim

                        'Case 4: AccountGroup <>"" Account<> ""
                    ElseIf cboAcctGroup.SelectedValue.Trim <> "" And lblAccount.Text.Trim <> "" Then
                        AccGroup = cboAcctGroup.SelectedValue.Trim
                        _Account = lblAccount.Text.Trim
                    End If

                    If lblActivity.Text.Trim = "" Then
                        _Activity = "ALL"
                    Else
                        _Activity = lblActivity.Text.Trim
                    End If

                    '========================-=== Check duplicate =========================================
                    Dim _ucid As String
                    If Not Session("dgEfinSecurity") Is Nothing Then
                        dt = CType(Session("dgEfinSecurity"), DataTable)
                        For Each dr In dt.Rows
                            If dr.RowState <> DataRowState.Deleted Then
                                If Not ViewState("ViewAuthUCID") Is Nothing Then
                                    _ucid = ViewState("ViewAuthUCID").ToString
                                End If

                                If dr("UCID") = _ucid And dr("AccountGroup") = AccGroup _
                                  And dr("Acct_Code") = _Account.ToUpper And dr("Activity_Code") = _Activity.ToUpper Then
                                    lblViewAuthError.Visible = True
                                    lblViewAuthError.Text = "Duplicate data"
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                    '================================ End Duplicate check =============================
                    al = CType(ViewState("Values"), ArrayList)
                    Dim a2 As New ArrayList
                    If (al(2) = "ALL" And al(3) = "ALL" And al(4) = "ALL") Then
                        'insert record
                        With a2
                            .Add(ViewState("ViewAuthUCID").ToString.Trim)
                            .Add(ViewState("GivenName").ToString)
                            .Add(ViewState("FamilyName").ToString)
                            .Add("NA")
                            .Add("NA")
                            If (lbParent.Text = "N") Then
                                .Add("NA")
                                .Add(ViewState("ViewAuthProject").ToString)
                            Else
                                .Add(ViewState("ViewAuthProject").ToString)
                                .Add("NA")
                            End If
                            .Add(AccGroup)
                            .Add(_Account)
                            .Add(_Activity)
                            .Add(ViewState("MemberType").ToString)

                        End With

                        DAL.GenericDBOperation.GenericDataBaseOperationDB("INSERT", "dbo.eFinsp_EfinSecurityInsert", a2)
                    ElseIf (AccGroup = "ALL" And _Account = "ALL" And _Activity = "ALL") Then
                        With a2
                            .Add(al(0)) 'ucid
                            .Add(al(1)) 'project
                            .Add(al(2)) 'account group
                            .Add(al(3)) 'account
                            .Add(al(4)) 'activity
                            .Add(lbParent.Text)

                        End With
                        DAL.GenericDBOperation.GenericDataBaseOperationDB("DELETE", "dbo.eFinsp_EfinSecurityDelete", a2)

                    Else
                        'update security

                        With a2
                            .Add(al(0)) 'ucid
                            .Add(al(1)) 'project
                            .Add(al(2)) 'account group
                            .Add(al(3)) 'account
                            .Add(al(4)) 'activity
                            'AccGroup, _Account, _Activity
                            .Add(AccGroup)
                            .Add(_Account)
                            .Add(_Activity)
                            .Add(lbParent.Text)
                        End With
                        DAL.GenericDBOperation.GenericDataBaseOperationDB("UPDATE", "dbo.eFinsp_EfinSecurityUpdate", a2)
                    End If
                    Me.dgViewingAuth.EditItemIndex = -1
                    RebindViewingAuthority()
                Else
                    Me.dgViewingAuth.EditItemIndex = -1
                    RebindViewingAuthority()
                End If


            Case "Delete"
                'delete security
                With al
                    .Add(ViewState("ViewAuthUCID").ToString.Trim)
                    .Add(ViewState("ViewAuthProject").ToString)
                    .Add(CType(e.Item.FindControl("lblAccountGroup"), Label).Text)
                    .Add(CType(e.Item.FindControl("lblAccount"), Label).Text)
                    .Add(CType(e.Item.FindControl("lblActivity"), Label).Text)
                    .Add(lbParent.Text)

                End With
                DAL.GenericDBOperation.GenericDataBaseOperationDB("DELETE", "dbo.eFinsp_EfinSecurityDelete", al)

                Me.dgViewingAuth.EditItemIndex = -1
                RebindViewingAuthority()
            Case "Add New"

                Dim AccGroup, _Account, _Activity As String

                Dim cboAcctGroup As DropDownList = CType(e.Item.FindControl("cboNewAccGroup"), DropDownList)
                Dim lblAccount As TextBox = CType(e.Item.FindControl("txtNewAccount"), TextBox)
                Dim lblActivity As TextBox = CType(e.Item.FindControl("txtNewActivity"), TextBox)

                If cboAcctGroup.SelectedValue.Trim <> "" And cboAcctGroup.SelectedValue.Trim <> "NA" Then
                    If lblAccount.Text.Trim <> "" And lblAccount.Text.Trim <> "NA" Then
                        lblViewAuthError.Visible = True
                        lblViewAuthError.Text = "Please enter Account group or Account"
                        Exit Sub
                    Else
                        lblViewAuthError.Visible = False
                    End If
                End If

                If Not cboAcctGroup Is Nothing AndAlso _
                     Not lblAccount Is Nothing AndAlso _
                     Not lblActivity Is Nothing Then

                    ' Check condtions
                    'Case 1: AccountGroup ="" Account="" 
                    If (cboAcctGroup.SelectedValue.Trim = "" Or cboAcctGroup.SelectedValue.Trim = "ALL") And lblAccount.Text.Trim = "" Then
                        AccGroup = "ALL"
                        _Account = "ALL"
                    ElseIf (cboAcctGroup.SelectedValue.Trim = "NA") And lblAccount.Text.Trim = "" Then
                        AccGroup = "NA"
                        _Account = "ALL"

                        'Case 2: AccountGroup <>"" Account="" 
                    ElseIf cboAcctGroup.SelectedValue.Trim <> "" And lblAccount.Text.Trim = "" Then
                        AccGroup = cboAcctGroup.SelectedValue.Trim
                        _Account = "NA"

                        'Case 3: AccountGroup ="" Account<>""
                    ElseIf (cboAcctGroup.SelectedValue.Trim = "" Or cboAcctGroup.SelectedValue.Trim = "NA") And lblAccount.Text.Trim <> "" Then
                        AccGroup = "NA"
                        _Account = lblAccount.Text.Trim

                        'Case 4: AccountGroup <>"" Account<> ""
                    ElseIf cboAcctGroup.SelectedValue.Trim <> "" And lblAccount.Text.Trim <> "" Then
                        AccGroup = cboAcctGroup.SelectedValue.Trim
                        _Account = lblAccount.Text.Trim
                    End If

                    If lblActivity.Text.Trim = "" Then
                        _Activity = "ALL"
                    Else
                        _Activity = lblActivity.Text.Trim
                    End If
                    If AccGroup = "ALL" And _Account = "ALL" And _Activity = "ALL" Then
                        lblViewAuthError.Visible = True
                        lblViewAuthError.Text = "Delete other restriction will return to no restriction"
                        Exit Sub

                    End If
                    '=========================== Check duplicate =========================================
                    Dim _ucid As String
                    If Not Session("dgEfinSecurity") Is Nothing Then
                        dt = CType(Session("dgEfinSecurity"), DataTable)
                        For Each dr In dt.Rows
                            If dr.RowState <> DataRowState.Deleted Then
                                If Not ViewState("ViewAuthUCID") Is Nothing Then
                                    _ucid = ViewState("ViewAuthUCID").ToString
                                End If

                                If dr("UCID") = _ucid And dr("AccountGroup") = AccGroup _
                                  And dr("Acct_Code") = _Account.ToUpper And dr("Activity_Code") = _Activity.ToUpper Then
                                    lblViewAuthError.Visible = True
                                    lblViewAuthError.Text = "Duplicate data"
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                    '================================ End Duplicate check =============================
                    With al
                        .Add(ViewState("ViewAuthUCID").ToString.Trim)
                        .Add(ViewState("GivenName").ToString)
                        .Add(ViewState("FamilyName").ToString)
                        .Add("NA")
                        .Add("NA")
                        If (lbParent.Text.ToUpper = "N") Then
                            .Add("NA")
                            .Add(ViewState("ViewAuthProject").ToString)
                        Else
                            .Add(ViewState("ViewAuthProject").ToString)
                            .Add("NA")
                        End If
                        .Add(AccGroup)
                        .Add(_Account)
                        .Add(_Activity)
                        .Add(ViewState("MemberType").ToString)

                    End With

                    DAL.GenericDBOperation.GenericDataBaseOperationDB("INSERT", "dbo.eFinsp_EfinSecurityInsert", al)
                    Me.dgViewingAuth.EditItemIndex = -1
                    RebindViewingAuthority()

                End If

        End Select
    End Sub
End Class
