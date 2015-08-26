Option Compare Text

Partial Class Controls_ProjectAwardData
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        Dim bLoadCache As Boolean = False
        Dim strProjectID As String = ""
        Dim category As String = CType(Session("Category"), String) ' Used to determine which panel to show
        Select Case category
            Case "SP"
                strProjectID = CType(Session("SPID"), String)
            Case "CP"
                strProjectID = CType(Session("CPID"), String)
            Case Else
                Return
        End Select


        If Not Me.IsPostBack Then

            Dim strOldAwardProjectID As String = CType(Session("OldAwardProjectID"), String)
            If (strOldAwardProjectID = strProjectID) Then
                bLoadCache = True
            Else
                strOldAwardProjectID = strProjectID
                Session("OldAwardProjectID") = strOldAwardProjectID
            End If

            LoadProjectAwardInfo(bLoadCache)

        End If

    End Sub

    ' Retrieve the team members for a particular child project
    Private Sub LoadProjectAwardInfo(ByRef bLoadCache As Boolean)

        If bLoadCache Then
            Me.dgProjects.DataSource = CType(Session("dtAwardData"), DataTable)
            Me.dgProjects.DataBind()
        Else
            Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session
            Dim strProjectID As String = ""
            Dim arrParams As New ArrayList
            Dim dt As DataTable

            Dim strStartDate As String = BLL.ProjectPeriod.GetProjectPeriodFromTheSession.StartDate.Substring(0, 6)
            Dim strEndDate As String = BLL.Header.GetAsAtDate.ToString("yyyyMM")

            Select Case strCategory
                Case "SP"   ' Summary Project
                    Dim strSPID As String = CType(Session("SPID"), String)
                    strProjectID = strSPID

                    arrParams.Add(CType(Session("UCID"), String))
                    arrParams.Add(strSPID)
                    arrParams.Add(strStartDate)
                    arrParams.Add(strEndDate)

                    ' Retrieve summary project financial summary, security is handle on database side
                    dt = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadSummaryProjectAwardInfo", arrParams)

                Case "CP"   ' Child Project
                    Dim strCPID As String = CType(Session("CPID"), String)
                    strProjectID = strCPID

                    arrParams.Add(CType(Session("UCID"), String))
                    arrParams.Add(strCPID)
                    arrParams.Add(strStartDate)
                    arrParams.Add(strEndDate)

                    ' Retrieve child project financial summary, security is handle on database side
                    dt = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadProjectAwardInfo", arrParams)

            End Select

            If Not dt Is Nothing Then

                Session("dtAwardData") = dt
                Me.dgProjects.DataSource = dt
                Me.dgProjects.DataBind()

            End If
        End If
    End Sub

    Private Sub dgrdTeamMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProjects.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim strYear As String = CType(e.Item.DataItem("fld_Year"), String)
            Dim strBudget As String = CType(e.Item.DataItem("fld_Budget"), String)
            Dim strReceived As String = CType(e.Item.DataItem("fld_Received"), String)
            Dim strVariance As String = CType(e.Item.DataItem("fld_Variance"), String)
            Dim lbl As Label

            If strYear = "TOTAL" Then
                e.Item.Style("background-color") = "#999999"
                e.Item.Style("font-weight") = "bold"
            End If

            If strBudget <> "N/A" And strBudget <> "" Then
                strBudget = (Decimal.Round(Decimal.Parse(strBudget), 2)).ToString()
                strBudget = String.Format("{0:F2}", Decimal.Parse(strBudget)).ToString()

                strBudget = String.Format("{0:n}", Decimal.Parse(strBudget))

                If Decimal.Parse(strBudget) < 0 Then
                    strBudget = "(" + strBudget.Substring(1) + ")"
                Else
                    e.Item.Cells(1).Style.Add("padding-right", "9px")
                End If

                If strBudget = "0" Then
                    strBudget = "0.00"
                End If

            ElseIf strBudget = "N/A" Then
                e.Item.Cells(1).Style.Add("padding-right", "9px")
            End If

            If strReceived <> "N/A" And strReceived <> "" Then
                strReceived = (Decimal.Round(Decimal.Parse(strReceived), 2)).ToString()
                strReceived = String.Format("{0:F2}", Decimal.Parse(strReceived)).ToString()

                strReceived = String.Format("{0:n}", Decimal.Parse(strReceived))

                If Decimal.Parse(strReceived) < 0 Then
                    strReceived = "(" + strReceived.Substring(1) + ")"
                Else
                    e.Item.Cells(2).Style.Add("padding-right", "9px")
                End If

                If strReceived = "0" Then
                    strReceived = "0.00"
                End If

            ElseIf strReceived = "N/A" Then
                e.Item.Cells(2).Style.Add("padding-right", "9px")
            End If

            If strVariance <> "N/A" And strVariance <> "" Then
                strVariance = (Decimal.Round(Decimal.Parse(strVariance), 2)).ToString()
                strVariance = String.Format("{0:F2}", Decimal.Parse(strVariance)).ToString()

                strVariance = String.Format("{0:n}", Decimal.Parse(strVariance))

                If Decimal.Parse(strVariance) < 0 Then
                    strVariance = "(" + strVariance.Substring(1) + ")"
                Else
                    e.Item.Cells(3).Style.Add("padding-right", "9px")
                End If

                If strVariance = "0" Then
                    strVariance = "0.00"
                End If

            ElseIf strVariance = "N/A" Then
                e.Item.Cells(3).Style.Add("padding-right", "9px")
            End If


            ' deal with View Authorizations
            lbl = CType(e.Item.FindControl("lblBudget"), Label)
            lbl.Text = strBudget

            lbl = CType(e.Item.FindControl("lblReceived"), Label)
            lbl.Text = strReceived

            lbl = CType(e.Item.FindControl("lblVariance"), Label)
            lbl.Text = strVariance
        End If

    End Sub
End Class
