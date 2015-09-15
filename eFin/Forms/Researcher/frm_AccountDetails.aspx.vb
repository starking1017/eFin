Option Compare Text

Partial Class Forms_Researcher_frm_AccountDetails
    Inherits System.Web.UI.Page
    Private HardKey As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, AccountsHeaderControl.UpdateAcoountDetail

        If Session("UCID") Is Nothing Then
            Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            CType(Page.Master, MasterPage).PageIdentity = "Enterprise Reporting<br/>eFIN - 04"
        End If

        Session("PageStart") = Date.Now

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        Dim startDate As String
        Dim endDate As String
        Dim strProjectID As String

        ' First time page load
        If Not IsPostBack Then
            EnableStartAtAccount()

            ' Retrieve period from session since first page load
            startDate = BLL.Period.GetPeriodFromTheSession.StartDate
            endDate = BLL.Period.GetPeriodFromTheSession.EndDate
            strProjectID = ""

            Dim str() As String

            If Not Request.QueryString("value") Is Nothing Then
                Dim strToDecrypted As String = Request.QueryString("value")

                If Not strToDecrypted Is Nothing Then

                    Dim objTamperProof As New BLL.Security.TamperProofQueryString64

                    Dim strDecryptedResult As String = objTamperProof.QueryStringDecode(strToDecrypted, Me.HardKey)

                    str = strDecryptedResult.Split("|")

                    If str(1) = "ACCTID" Then
                        Session("ACCTID") = str(0)
                        DisableStartAtAccount()
                    End If
                End If
            End If
        Else
            ' Retrieve period from the header control
            startDate = AccountsHeaderControl.GetStartDate
            endDate = AccountsHeaderControl.GetEndDate
            strProjectID = AccountsHeaderControl.GetProjectID

            ' If same as viewstate -> start date and end date never changed
            ' Return
            If CType(ViewState("StartDate"), String) = startDate AndAlso _
               CType(ViewState("EndDate"), String) = endDate AndAlso _
               CType(ViewState("StartAtAccount"), String) = strProjectID Then

                Return

            End If

        End If

        LoadAccountDetails(startDate, endDate)    ' Update financial summary between start and end date

        ' Store new start and end date to viewstate
        ViewState("StartDate") = startDate
        ViewState("EndDate") = endDate
        ViewState("StartAtAccount") = strProjectID

        If Request.QueryString("category") = "ACTIVITYSINGLEACCOUNT" Or Request.QueryString("category") = "PROJECTSINGLEACCOUNT" Then
            ddlAccount.Visible = True
        Else
            ddlAccount.Visible = False
        End If

    End Sub

    ' Load financial summary depending on what type of category
    Private Sub LoadAccountDetails(ByRef startDate As String, ByRef endDate As String)

        Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session
        Dim strAccount As String = CType(Session("ACCTID"), String)
        Dim arrParams As New ArrayList
        Dim dt As New DataTable

        If dgrdAccounts.Attributes("SortExpression") Is Nothing Then
            dgrdAccounts.Attributes("SortExpression") = "Journal_Date"
            dgrdAccounts.Attributes("SortDirection") = "DESC"
        End If

        Select Case strCategory
            Case "CP"   ' Child Project
                ' Show Activity Column
                Me.dgrdAccounts.Columns(5).Visible = True

                Dim strUCID As String = CType(Session("UCID"), String)
                Dim strCPID As String = CType(Session("CPID"), String)

                arrParams.Add(strUCID)
                arrParams.Add(strCPID)
                arrParams.Add(startDate)
                arrParams.Add(endDate)

                If Request.QueryString("category") = "PROJECTSINGLEACCOUNT" Then
                    arrParams.Add(Session("ACCTID"))
                    dt = BLL.AccountDetails.LoadAccountDetails("SELECT", "dbo.eFinsp_LoadProjectSingleAccountDetails_1", arrParams)

                Else
                    arrParams.Add(AccountsHeaderControl.GetProjectID)
                    dt = BLL.AccountDetails.LoadAccountDetails("SELECT", "dbo.eFinsp_LoadProjectAllAccountDetails_1", arrParams)
                End If

                LoadDdlAccountList(strAccount)

            Case "ACT"  ' Activity

                ' Hide Activity Column
                Me.dgrdAccounts.Columns(5).Visible = False

                Dim strUCID As String = CType(Session("UCID"), String)
                Dim strCPID As String = CType(Session("CPID"), String)
                Dim strACTID As String = CType(Session("ACTID"), String)

                arrParams.Add(strUCID)
                arrParams.Add(strCPID)
                arrParams.Add(strACTID)
                arrParams.Add(startDate)
                arrParams.Add(endDate)

                If Request.QueryString("category") = "ACTIVITYSINGLEACCOUNT" Then
                    arrParams.Add(Session("ACCTID"))
                    dt = BLL.AccountDetails.LoadAccountDetails("SELECT", "dbo.eFinsp_LoadActivitySingleAccountDetails_1", arrParams)
                Else
                    arrParams.Add(AccountsHeaderControl.GetProjectID)
                    dt = BLL.AccountDetails.LoadAccountDetails("SELECT", "dbo.eFinsp_LoadActivityAllAccountDetails_1", arrParams)
                End If

                LoadDdlAccountList(strAccount)

        End Select

        Session("dtAccounts") = dt

        Me.dgrdAccounts.CurrentPageIndex = 0
        BindData(dt)
    End Sub

    Private Sub BindData(dt As DataTable)

        Dim dv As DataView = New DataView()
        Dim tb2 As DataTable = New DataTable()
        ' Copy Total row
        tb2 = dt.Clone()
        tb2.ImportRow(dt.Rows(dt.Rows.Count() - 1))

        ' Remove Total and change to view for sort
        dt.Rows.RemoveAt(dt.Rows.Count() - 1)
        dv = dt.DefaultView

        Dim strSortExpression As String = dgrdAccounts.Attributes("SortExpression")
        Dim strSortDirection As String = dgrdAccounts.Attributes("SortDirection")
        dv.Sort = strSortExpression + " " + strSortDirection + ", " + "Acct_Code ASC"

        ' Create new table add sorted table and Total row
        Dim myNewTable As DataTable = dv.ToTable()
        myNewTable.ImportRow(tb2.Rows(0))


        ' Add back Total row
        dt.ImportRow(tb2.Rows(0))

        ' Bind data to gridview
        Me.dgrdAccounts.DataSource = myNewTable
        Me.dgrdAccounts.DataBind()

    End Sub

    Private Sub LoadDdlAccountList(strAccount As String)

        If Session("AllarrActCode") IsNot Nothing Then
            Dim ArrayList As ArrayList = CType(Session("AllarrActCode"), ArrayList)
            If ArrayList.Count > 0 Then
                ArrayList.Sort()
                ddlAccount.DataSource = ArrayList
                ddlAccount.DataBind()

                Dim nSelectindex As Integer = 0
                For i = 0 To ddlAccount.Items.Count - 1
                    If ddlAccount.Items(i).ToString().Substring(0, 5) = strAccount Then
                        nSelectindex = i
                    End If
                Next
                ddlAccount.SelectedIndex = nSelectindex
            End If
        End If

    End Sub

    Private Sub dgrdAccounts_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrdAccounts.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim actual As Decimal = CType(e.Item.DataItem("Amount"), Decimal)
            Dim encumbrance As Decimal = CType(e.Item.DataItem("Encumbrances"), Decimal)
            Dim strEMPLID As String = CType(e.Item.DataItem("EMPLID"), String)
            Dim strVendor As String = CType(e.Item.DataItem("Vendor_ID"), String)
            Dim strFiscalPeriod As String = CType(e.Item.DataItem("Fiscal_Period"), String)
            Dim strPurchaseOrder As String = CType(e.Item.DataItem("Purchase_Order"), String)
            Dim dateJournalDate As Date = CType(e.Item.DataItem("Journal_Date"), Date)


            If strFiscalPeriod <> "" Then
                strFiscalPeriod = strFiscalPeriod.Insert(strFiscalPeriod.Length - 2, "/")
            End If

            Dim strJournalDate As String = dateJournalDate.ToString("yyyyMMdd").Trim

            If strPurchaseOrder = "^" Then
                strPurchaseOrder = ""
            End If

            Dim strActual As String
            Dim strEncumbrance As String

            strActual = String.Format("{0:n}", actual)
            strEncumbrance = String.Format("{0:n}", encumbrance)
            
            If Decimal.Parse(strActual) < 0 Then
                strActual = "(" + strActual.Substring(1) + ")"
            Else
                e.Item.Cells(14).Style.Add("padding-right", "9px")
                If Decimal.Parse(strActual) <> 0 Then
                End If
            End If

            If Decimal.Parse(strEncumbrance) < 0 Then
                strEncumbrance = "(" + strEncumbrance.Substring(1) + ")"
            Else
                e.Item.Cells(15).Style.Add("padding-right", "9px")
                If Decimal.Parse(strEncumbrance) <> 0 Then
                End If
            End If

            ' Add excel export style to ensure the column format is perserved (Supplier / Customer / Employee)
            e.Item.Cells(0).Style.Add("mso-number-format", "\@")

            ' Add excel export style to ensure the column format is perserved (Account Code)
            e.Item.Cells(3).Style.Add("mso-number-format", "\@")

            ' Add excel export style to ensure the column format is perserved (Activity Code)
            e.Item.Cells(5).Style.Add("mso-number-format", "\@")

            ' Add excel export style to ensure the column format is perserved (Purchase Order #)
            e.Item.Cells(6).Style.Add("mso-number-format", "\@")

            ' Add excel export style to ensure the column format is perserved (Reported ID)
            e.Item.Cells(8).Style.Add("mso-number-format", "\@")

            ' Add excel export style to ensure the column format is perserved (Journal Line Ref)
            e.Item.Cells(10).Style.Add("mso-number-format", "\@")

            Dim lbl As Label = CType(e.Item.FindControl("lblSupplier"), Label)

            If strEMPLID <> "^" And strEMPLID <> "" Then
                lbl.Text = strEMPLID

                If strEMPLID = "TOTAL" Then
                    e.Item.Style("background-color") = "#999999"
                    e.Item.Style("font-weight") = "bold"

                    If (strActual.Contains("(")) Then
                        e.Item.Cells(14).BackColor = Drawing.Color.FromArgb(197, 227, 191)
                    ElseIf Decimal.Parse(strActual) <> 0 Then
                        e.Item.Cells(14).BackColor = Drawing.Color.FromArgb(205, 69, 69)
                    End If
                    strJournalDate = ""
                End If

            ElseIf strVendor <> "^" And strVendor <> "" Then
                lbl.Text = strVendor
            Else
                lbl.Text = ""
            End If

            lbl = CType(e.Item.FindControl("lblJournalDate"), Label)
            lbl.Text = strJournalDate

            lbl = CType(e.Item.FindControl("lblActual"), Label)
            lbl.Text = strActual

            lbl = CType(e.Item.FindControl("lblEncumbrance"), Label)
            lbl.Text = strEncumbrance

            lbl = CType(e.Item.FindControl("lblFiscalPeriod"), Label)
            lbl.Text = strFiscalPeriod

            lbl = CType(e.Item.FindControl("lblPurchaseOrder"), Label)
            lbl.Text = strPurchaseOrder

        End If

    End Sub

    Private Sub dgrdAccounts_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgrdAccounts.PageIndexChanged
        Me.dgrdAccounts.CurrentPageIndex = e.NewPageIndex
        Me.dgrdAccounts.DataSource = CType(Session("dtAccounts"), DataTable)
        Me.dgrdAccounts.DataBind()
    End Sub


    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        If Not Session("PageStart") Is Nothing Then

            'Me.lblResponseTime.Text = String.Format("{0}.{1}", Date.Now.Subtract(CType(Session("PageStart"), Date)).Seconds, Date.Now.Subtract(CType(Session("PageStart"), Date)).Milliseconds)

            Session.Remove("PageStart")

        End If

        MyBase.Render(writer)

    End Sub

    Private Sub DisableStartAtAccount()
        Dim pan As Panel = CType(AccountsHeaderControl.FindControl("panStartAt"), Panel)
        pan.Visible = False
    End Sub

    Private Sub EnableStartAtAccount()
        Dim pan As Panel = CType(AccountsHeaderControl.FindControl("panStartAt"), Panel)
        pan.Visible = True
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel"
        HttpContext.Current.Response.Charset = ""

        Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session

        Select Case strCategory
            Case "CP"   ' Child Project
                Dim strCPID As String = CType(Session("CPID"), String)

                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strCPID + " - AccountDetails.xls")

            Case "ACT"  ' Activity
                Dim strCPID As String = CType(Session("CPID"), String)
                Dim strACTID As String = CType(Session("ACTID"), String)

                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strCPID + " - " + strACTID + " - AccountDetails.xls")

            Case Else
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=AccountDetails.xls")

        End Select

        EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)

        Me.dgrdAccounts.AllowPaging = False
        Me.dgrdAccounts.DataSource = CType(Session("dtAccounts"), DataTable)
        Me.dgrdAccounts.DataBind()

        'Convert the controls to string literals
        Utils.ClearControls(dgrdAccounts)
        ProjectChartField1.RenderControl(oHtmlTextWriter)
        dgrdAccounts.RenderControl(oHtmlTextWriter)

        HttpContext.Current.Response.Write(oStringWriter.ToString())
        HttpContext.Current.Response.End()

        Me.dgrdAccounts.AllowPaging = True
        Me.dgrdAccounts.DataSource = CType(Session("dtAccounts"), DataTable)
        Me.dgrdAccounts.DataBind()
    End Sub

    Protected Sub ddlAccount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccount.SelectedIndexChanged

        Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session
        Select Case strCategory
            Case "CP"   ' Child Project
                'added the rt425 for overhead encumbrance
                Dim strAccount = ddlAccount.SelectedValue.Substring(0, 5)
                If IsNumeric(strAccount) = True Or strAccount = "RT425" Then
                    Dim strToEncrypted As String = strAccount + "|" + "ACCTID"
                    Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                    Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                    Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_AccountDetails.aspx?value=" + strEncryptedResult + "%26category=PROJECTSINGLEACCOUNT")
                End If
            Case "ACT"  ' Activity
                'added the rt425 for overhead encumbrance
                Dim strAccount = ddlAccount.SelectedValue.Substring(0, 5)
                If IsNumeric(strAccount) = True Or strAccount = "RT425" Then
                    Dim strToEncrypted As String = strAccount + "|" + "ACCTID"
                    Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                    Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                    Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_AccountDetails.aspx?value=" + strEncryptedResult + "%26category=ACTIVITYSINGLEACCOUNT")
                End If
        End Select

    End Sub

    Protected Sub dgrdAccounts_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgrdAccounts.SortCommand
        Dim SortExpression As String = e.SortExpression
        Dim SortDirection As String = "ASC"
        If SortExpression.Equals(dgrdAccounts.Attributes("SortExpression").ToString()) Then
            If dgrdAccounts.Attributes("SortDirection").ToString().StartsWith("ASC") Then
                SortDirection = "DESC"
            Else
                SortDirection = "ASC"
            End If
        End If

        dgrdAccounts.Attributes("SortExpression") = SortExpression
        dgrdAccounts.Attributes("SortDirection") = SortDirection
        BindData(CType(Session("dtAccounts"), DataTable))
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

        Dim dt As DataTable
        If (Not Session("dtAwardData") Is Nothing) Then
            dt = CType(Session("dtAwardData"), DataTable).Copy()
            dt.Rows.RemoveAt(dt.Rows.Count - 1)
            Dim row As DataRow = dt.NewRow()
            row.Item(0) = "-- Select Year --"
            dt.Rows.InsertAt(row, 0)
            Session("dtProjectYear") = dt

            AccountsHeaderControl.GetPHControlProjectYear().DataSource = dt
            AccountsHeaderControl.GetPHControlProjectYear().DataTextField = "fld_Year"
            AccountsHeaderControl.GetPHControlProjectYear().DataValueField = "fld_Year"
            AccountsHeaderControl.GetPHControlProjectYear().DataBind()
        End If

    End Sub
End Class
