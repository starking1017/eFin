Option Compare Text

Partial Class Forms_Researcher_frm_ActSummaryByAccount
    Inherits System.Web.UI.Page
    Private HardKey As String = ""

    'Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Session("PageStart") = Date.Now

    '    Response.Cache.SetCacheability(HttpCacheability.NoCache)

    '    ' Check to see if session expired, if expired redirected to expiration page
    '    If Session("RandomKey") Is Nothing Then
    '        HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
    '    End If

    '    HardKey = CType(Session("RandomKey"), String)

    '    Dim startDate As String
    '    Dim endDate As String
    '    Dim accountID As String

    '    If Not IsPostBack Then

    '        EnableStartAtAccount()  ' Enable start at account feature

    '        ' First time load, get period from session
    '        startDate = BLL.Period.GetPeriodFromTheSession.StartDate
    '        endDate = BLL.Period.GetPeriodFromTheSession.EndDate
    '        accountID = ""

    '    Else
    '        ' Subsequent load, get period from the header control
    '        startDate = HeaderControl.GetStartDate
    '        endDate = HeaderControl.GetEndDate
    '        accountID = HeaderControl.GetProjectID

    '        ' Check to see if the start and end dates have changed since last page load
    '        ' If same, return and do nothing
    '        If (Not ViewState("StartDate") Is Nothing) AndAlso CType(ViewState("StartDate"), String) = startDate AndAlso _
    '           (Not ViewState("EndDate") Is Nothing) AndAlso CType(ViewState("EndDate"), String) = endDate AndAlso _
    '           (Not ViewState("StartAtAccount") Is Nothing) AndAlso CType(ViewState("StartAtAccount"), String) = accountID Then

    '            Return

    '        End If
    '    End If

    '    Try
    '        LoadActivitySummaryByAccount(startDate, endDate)    ' Get activity summary for the account

    '        ' Store new start and end date to viewstate
    '        ViewState("StartDate") = startDate
    '        ViewState("EndDate") = endDate
    '        ViewState("StartAtAccount") = accountID

    '    Catch ex As Exception

    '    Finally

    '    End Try

    'End Sub




    ' Enables the start at account feature
    Private Sub EnableStartAtAccount()
        Dim pan As Panel = CType(HeaderControl.FindControl("panStartAt"), Panel)
        pan.Visible = True

        Dim lbl As Label = CType(HeaderControl.FindControl("lblStartAtProject"), Label)

        lbl.Visible = True
        lbl.Text = "Start At Revenue / Expenditures:"
    End Sub


    ' Retrieve Activity Summary by account and bind it to datagrid
    Private Sub LoadActivitySummaryByAccount(ByRef startDate As String, ByRef endDate As String)
        Dim strUCID As String = CType(Session("UCID"), String)
        Dim strCPID As String = CType(Session("CPID"), String)
        Dim strACTID As String = CType(Session("ACTID"), String)
        Dim dt As DataTable
        Dim arrParams As New ArrayList

        arrParams.Add(strUCID)
        arrParams.Add(strCPID)
        arrParams.Add(strACTID)
        arrParams.Add(startDate)
        arrParams.Add(endDate)
        arrParams.Add(HeaderControl.GetProjectID)

        dt = BLL.ProjectSummaryByAccount.LoadProjectSummaryByAccount("SELECT", "dbo.eFinsp_LoadActivitySummaryByAccountListsWithSecurity", arrParams) ' With start at account feature

        ' Bind data to datagrid
        If Not dt Is Nothing Then
            Me.dgProjects.DataSource = dt
            Me.dgProjects.DataBind()
        End If
    End Sub


    ' Set the style and color of the account summary datagrid
    Private Sub dgProjects_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProjects.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Retrieve a record from the data table
            Dim colorFlag As String = CType(e.Item.DataItem("fld_ColorFlag"), String)
            Dim strAccount As String = CType(e.Item.DataItem("fld_Account"), String)
            Dim strBudget As String = CType(IIf(e.Item.DataItem("fld_Budget") Is DBNull.Value, "0.00", e.Item.DataItem("fld_Budget")), String)
            Dim strActual As String = CType(IIf(e.Item.DataItem("fld_Actual") Is DBNull.Value, "0.00", e.Item.DataItem("fld_Actual")), String)
            Dim strEncumbrance As String = CType(IIf(e.Item.DataItem("fld_Encumbrances") Is DBNull.Value, "0.00", e.Item.DataItem("fld_Encumbrances")), String)
            Dim strBalance As String = CType(IIf(e.Item.DataItem("fld_Balance") Is DBNull.Value, "0.00", e.Item.DataItem("fld_Balance")), String)

            ' Set the background colors
            Select Case colorFlag
                Case "0"
                    e.Item.Style("background-color") = "#999999"
                    e.Item.Style("font-weight") = "bold"

                Case "1"
                    e.Item.Style("background-color") = "#CCCCCC"
                    e.Item.Style("color") = "#000000"
                    e.Item.Style("font-weight") = "bold"

                Case "2"
                    e.Item.Style("background-color") = "#ffffff"
            End Select

            ' If budget is an actual number, format the number into financial format
            If strBudget <> "" And strBudget <> "N/A" Then
                'strBudget = Decimal.Round(Decimal.Parse(strBudget), 2).ToString
                strBudget = String.Format("{0:F2}", Decimal.Parse(strBudget)).ToString
                strBudget = String.Format("{0:n}", Decimal.Parse(strBudget))

                If Decimal.Parse(strBudget) < 0 Then
                    strBudget = "(" + strBudget.Substring(1) + ")"
                Else
                    e.Item.Cells(2).Style.Add("padding-right", "9px")
                End If

                If strBudget = "0" Then
                    strBudget = "0.00"
                End If

            ElseIf strBudget = "N/A" Then
                e.Item.Cells(2).Style.Add("padding-right", "9px")
            End If

            ' If actual is an actual number, format the number into financial format
            If strActual <> "" And strActual <> "N/A" Then
                'strActual = Decimal.Round(Decimal.Parse(strActual), 2).ToString
                strActual = String.Format("{0:F2}", Decimal.Parse(strActual)).ToString()
                strActual = String.Format("{0:n}", Decimal.Parse(strActual))

                If Decimal.Parse(strActual) < 0 Then
                    strActual = "(" + strActual.Substring(1) + ")"
                Else
                    e.Item.Cells(3).Style.Add("padding-right", "9px")
                End If

                If strActual = "0" Then
                    strActual = "0.00"
                End If

            ElseIf strActual = "N/A" Then
                e.Item.Cells(3).Style.Add("padding-right", "9px")
            End If

            ' If encumbrance is an actual number, format the number into financial format
            If strEncumbrance <> "" And strEncumbrance <> "N/A" Then
                'strEncumbrance = Decimal.Round(Decimal.Parse(strEncumbrance), 2).ToString
                strEncumbrance = String.Format("{0:F2}", Decimal.Parse(strEncumbrance)).ToString()

                strEncumbrance = String.Format("{0:n}", Decimal.Parse(strEncumbrance))

                If Decimal.Parse(strEncumbrance) < 0 Then
                    strEncumbrance = "(" + strEncumbrance.Substring(1) + ")"
                Else
                    e.Item.Cells(4).Style.Add("padding-right", "9px")
                End If

                If strEncumbrance = "0" Then
                    strEncumbrance = "0.00"
                End If

            ElseIf strEncumbrance = "N/A" Then
                e.Item.Cells(4).Style.Add("padding-right", "9px")
            End If

            ' If balance is an actual number, format the number into financial format
            If strBalance <> "" And strBalance <> "N/A" Then
                'strBalance = Decimal.Round(Decimal.Parse(strBalance), 2).ToString
                strBalance = String.Format("{0:F2}", Decimal.Parse(strBalance)).ToString()

                strBalance = String.Format("{0:n}", Decimal.Parse(strBalance))

                If Decimal.Parse(strBalance) < 0 Then
                    strBalance = "(" + strBalance.Substring(1) + ")"
                    'added by jack on April 1, 2009 for backgroud color
                    If colorFlag = "0" Then
                        'e.Item.Cells(5).Style.Add(HtmlTextWriterStyle.BackgroundColor, "Green")
                        e.Item.Cells(5).BackColor = Drawing.Color.FromArgb(197, 227, 191)
                    End If
                    '-----------------------------------
                Else
                    e.Item.Cells(5).Style.Add("padding-right", "9px")
                    'added by jack on April 1, 2009 for backgroud color
                    If Decimal.Parse(strBalance) > 0 And colorFlag = "0" Then
                        'e.Item.Cells(5).Style.Add(HtmlTextWriterStyle.BackgroundColor, "Red")
                        e.Item.Cells(5).BackColor = Drawing.Color.FromArgb(205, 69, 69)
                    End If
                    '-----------------------------------
                End If

                If strBalance = "0" Then
                    strBalance = "0.00"
                End If

            ElseIf strBalance = "N/A" Then
                e.Item.Cells(5).Style.Add("padding-right", "9px")
            End If

            Dim lbl As Label
            lbl = CType(e.Item.FindControl("lblBudget"), Label)
            lbl.Text = strBudget

            lbl = CType(e.Item.FindControl("lblActual"), Label)
            lbl.Text = strActual

            lbl = CType(e.Item.FindControl("lblEncumbrances"), Label)
            lbl.Text = strEncumbrance

            lbl = CType(e.Item.FindControl("lblBalance"), Label)
            lbl.Text = strBalance

            Dim hyplnk As HyperLink
            hyplnk = CType(e.Item.FindControl("hyplnkAccount"), HyperLink)
            hyplnk.Text = strAccount
            'added the rt425 for overhead encumbrance
            If IsNumeric(strAccount) = True Or strAccount = "RT425" Then
                Dim strToEncrypted As String = strAccount + "|" + "ACCTID"
                Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_AccountDetails.aspx?value=" + strEncryptedResult + "%26category=ACTIVITYSINGLEACCOUNT"
            End If

        End If
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        If Not Session("PageStart") Is Nothing Then

            'Me.lblResponseTime.Text = String.Format("{0}.{1}", Date.Now.Subtract(CType(Session("PageStart"), Date)).Seconds, Date.Now.Subtract(CType(Session("PageStart"), Date)).Milliseconds)

            Session.Remove("PageStart")

        End If

        MyBase.Render(writer)

    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session("UCID") Is Nothing Then
            Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            CType(Page.Master, MasterPage).PageIdentity = "Enterprise Reporting<br/>eFIN - 06"
        End If

    End Sub


    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender

        Session("PageStart") = Date.Now

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'Check to see if session expired, if expired redirected to expiration page This page was missing . Added by David on 16 Jan 2014
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        HardKey = CType(Session("RandomKey"), String)

        Dim startDate As String
        Dim endDate As String
        Dim accountID As String

        If Not IsPostBack Then

            EnableStartAtAccount()  ' Enable start at account feature

            ' First time load, get period from session
            startDate = BLL.Period.GetPeriodFromTheSession.StartDate
            endDate = BLL.Period.GetPeriodFromTheSession.EndDate
            accountID = ""

        Else
            ' Subsequent load, get period from the header control
            startDate = HeaderControl.GetStartDate
            endDate = HeaderControl.GetEndDate
            accountID = HeaderControl.GetProjectID

            ' Check to see if the start and end dates have changed since last page load
            ' If same, return and do nothing
            If (Not ViewState("StartDate") Is Nothing) AndAlso CType(ViewState("StartDate"), String) = startDate AndAlso _
               (Not ViewState("EndDate") Is Nothing) AndAlso CType(ViewState("EndDate"), String) = endDate AndAlso _
               (Not ViewState("StartAtAccount") Is Nothing) AndAlso CType(ViewState("StartAtAccount"), String) = accountID Then

                Return

            End If
        End If

        Try
            LoadActivitySummaryByAccount(startDate, endDate)    ' Get activity summary for the account

            ' Store new start and end date to viewstate
            ViewState("StartDate") = startDate
            ViewState("EndDate") = endDate
            ViewState("StartAtAccount") = accountID

        Catch ex As Exception

        Finally

        End Try

    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentType = "application/vnd.ms-Excel"
        HttpContext.Current.Response.Charset = ""

        Dim strCPID As String = CType(Session("CPID"), String)
        Dim strACTID As String = CType(Session("ACTID"), String)

        If Not strCPID Is Nothing And Not strACTID Is Nothing Then
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strCPID + " - " + strACTID + " - ActivitySummaryByAccount.xls")
        Else
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=ActivitySummaryByAccount.xls")
        End If

        EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)

        'Convert the controls to string literals
        Utils.ClearControls(dgProjects)
        ProjectChartField1.RenderControl(oHtmlTextWriter)
        dgProjects.RenderControl(oHtmlTextWriter)

        HttpContext.Current.Response.Write(oStringWriter.ToString())
        HttpContext.Current.Response.End()
    End Sub
End Class
