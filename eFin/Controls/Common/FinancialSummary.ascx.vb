Option Compare Text
Imports System.Web.UI.DataVisualization.Charting

Partial Public Class Controls_FinancialSummary
    Inherits System.Web.UI.UserControl
    Private HardKey As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        Dim startDate As String
        Dim endDate As String
        Dim category As String = CType(Session("Category"), String) ' Used to determine which panel to show
        Dim strCacheSPID As String = CType(Session("CacheSPID"), String) ' Used to determine to cache table or not
        Dim strSecurityRank As String = CType(Session("SecurityRank"), String) ' Used to determine to cache table or not
        Dim strProjectID As String = ""

        ' First time page load
        If Not IsPostBack Then
            '----------------------------
            panSummaryProject.Visible = False
            PanDetailStatus.Visible = False
            SetPanelVisiblity(category)

            '------------------------------
            Select Case category
                Case "SP"   ' Summary Project
                    panProject.Visible = False
                    panActivity.Visible = False
                    ddlChildproject.Visible = False
                    lblChildProject.Visible = False

                    strProjectID = CType(Session("SPID"), String)

                Case "CP"   ' Child Project
                    panProject.Visible = True
                    panActivity.Visible = False

                    ' Only for CP
                    hypParentProject.Visible = True
                    lblParentProject.Visible = True

                    If Session("AllarrChildProject") Is Nothing Then
                        ddlChildproject.Visible = False
                        lblChildProject.Visible = False
                    End If

                    strProjectID = CType(Session("CPID"), String)

                Case "ACT"  ' Activity Project
                    panProject.Visible = False
                    panActivity.Visible = True
                    ddlChildproject.Visible = False
                    lblChildProject.Visible = False

                    strProjectID = CType(Session("ACTID"), String)

                    SetTabView(category)
            End Select

            ' Retrieve period from session since first page load
            startDate = BLL.Period.GetPeriodFromTheSession.StartDate
            endDate = BLL.Period.GetPeriodFromTheSession.EndDate

        Else
            Dim ctrlHeader As Object = CType(Parent.FindControl("HeaderControl"), Object)

            ' Retrieve period from the header control
            startDate = CType(ctrlHeader.GetStartDate, String)
            endDate = CType(ctrlHeader.GetEndDate, String)

            ' If viewstate = current start date and end date -> start date and end date never changed
            ' Return
            If (Not ViewState("StartDate") Is Nothing) AndAlso CType(ViewState("StartDate"), String) = startDate AndAlso _
               (Not ViewState("EndDate") Is Nothing) AndAlso CType(ViewState("EndDate"), String) = endDate Then

                Return

            End If

        End If

        If (startDate <= endDate) Then
            If (Not strCacheSPID Is Nothing And strCacheSPID = strProjectID) Then
                LoadCacheFinancialSummary(startDate, endDate)    ' Update Cache financial summary between start and end date
            Else
                LoadFinancialSummary(startDate, endDate)    ' Update financial summary between start and end date
            End If

            If (category <> "ACT") Then
                'Load chart data
                LoadFinancialChart(startDate, endDate)

                ' Temporary for demo
                CalculateBurnRateByData(startDate, endDate)
            End If
           
            ' Store new start and end date to viewstate
            ViewState("StartDate") = startDate
            ViewState("EndDate") = endDate
        End If

        ' add ParentChildRelationSwitch component
        AddProjectRelateSwitch(category, strCacheSPID, strProjectID, strSecurityRank)

    End Sub

    Private Sub AddProjectRelateSwitch(category As String, strCacheSPID As String, strProjectID As String, strSecurityRank As String)

        ' Add and select default value for ddlChildproject
        If Session("AllarrChildProject") IsNot Nothing Then

            Dim arrayList As ArrayList = CType(Session("AllarrChildProject"), ArrayList)
            arrayList.Sort()
            ddlChildproject.DataSource = arrayList
            ddlChildproject.DataBind()
            ddlChildproject.SelectedIndex = ddlChildproject.Items.IndexOf(ddlChildproject.Items.FindByValue(strProjectID))
        End If


        Select Case category
            Case "CP"   ' Child Project

                ' Control ddlChildproject only visible under own parent project
                strProjectID = CType(Session("CPID"), String)
                Dim isHasitem As Integer = ddlChildproject.Items.IndexOf(ddlChildproject.Items.FindByValue(strProjectID))

                If Session("AllarrChildProject") Is Nothing Or isHasitem = -1 Then
                    ddlChildproject.Visible = False
                    lblChildProject.Visible = False
                End If

                ' For hyperLink to parent project
                AddParentHyp(category, strCacheSPID, strSecurityRank)
        End Select

    End Sub

    Private Sub AddParentHyp(ByRef category As String, ByRef strCacheSPID As String, ByRef strSecurityRank As String)

        Try
            If Session("ParentProject") Is Nothing Then
                ' CP which no parent
                hypParentProject.Visible = False
                lblParentProject.Visible = False
            Else
                Dim strParentProject As String = CType(Session("ParentProject"), String)
                Dim intSecurityRank As Integer = CType(Session("SecurityRank"), Integer)
                hypParentProject.Text = strParentProject

                Dim strToEncrypted As String = ""
                If strCacheSPID <> strParentProject Then
                    strToEncrypted = strParentProject + "|" + "SP" + "|" + "NeedToCache" + "|" + strSecurityRank
                Else
                    strToEncrypted = strParentProject + "|" + "SP" + "|" + strParentProject + "|" + strSecurityRank
                End If

                Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, HardKey)
                If intSecurityRank <= 3 Then
                    hypParentProject.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim

                End If
            End If
        Catch ex As Exception
        Finally
            Session("ParentProject") = Nothing
        End Try

    End Sub

    Private Sub SetTabView(ByVal category As String)

        Select Case category
            Case "SP"   ' Summary Project
                rts1.Tabs(2).Visible = True
                rts1.Tabs(3).Visible = True
            Case "CP"   ' Child Project
                rts1.Tabs(2).Visible = True
                rts1.Tabs(3).Visible = True
            Case "ACT"  ' Activity Project
                rts1.Tabs(2).Visible = False
                rts1.Tabs(3).Visible = False
        End Select

    End Sub

    Private Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.PreRender

        ' Get total expenditure and current balance to calculate burn rate
        Dim category As String = CType(Session("Category"), String) ' Used to determine which panel to show

        If Not IsPostBack Then
            If (category <> "ACT") Then
                CalcWholePeriodBurnRate()
            End If
        Else
            If (Not Session("BurnRate") Is Nothing And Not Session("PZBD") Is Nothing) Then
                lblBurnRate1.Text = Session("BurnRate").ToString()
                lblZeroBalDate1.Text = Session("PZBD").ToString()
            End If
        End If

        ' Add attachment link to PeopleSoft after project status loaded
        AddAttachmentLink()

    End Sub

    Private Sub AddAttachmentLink()

        Dim category As String = CType(Session("Category"), String)
        Dim strProjectID As String = ""

        Select Case category
            Case "SP"   ' Summary Project
                strProjectID = CType(Session("SPID"), String)
            Case "CP"   ' Child Project
                strProjectID = CType(Session("CPID"), String)
            Case "ACT"  ' Activity Project
                strProjectID = CType(Session("ACTID"), String)
        End Select
        ' add attachment files list
        Dim strConnectFSTSTX As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionFSTSTX")
        Dim strBusinessUnit As String = ProjectActivityStatusControl.ProjectlblBusinessUnit
        If strBusinessUnit Is Nothing Or strProjectID Is Nothing Then
            hpyAttachment.Visible = False
        Else
            Dim enStrConnect = "BUSINESS_UNIT=" + strBusinessUnit + _
                               "&PROJECT_ID=" + strProjectID + _
                               "&PAGE=PROJECT_DOC_01"
            'Dim ensts = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(enStrConnect))
            hpyAttachment.NavigateUrl = strConnectFSTSTX + "EMPLOYEE/ERP/c/CREATE_PROJECTS.PROJECT_GENERAL.GBL?" + enStrConnect
        End If

    End Sub



    ' Load financial summary depending on what type of category
    Private Sub LoadFinancialSummary(ByRef startDate As String, ByRef endDate As String)

        Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session
        Dim strCacheSP As String = CType(Session("CacheSPID"), String) ' Used to determine to cache table or not
        Dim strProjectID As String = ""

        Dim arrParams As New ArrayList

        Dim arrChildProject As New ArrayList
        Dim arrActCode As New ArrayList

        Dim dt As New DataTable

        Select Case strCategory
            Case "SP"   ' Summary Project
                Dim strSPID As String = CType(Session("SPID"), String)
                strProjectID = strSPID

                arrParams.Add(CType(Session("UCID"), String))
                arrParams.Add(strSPID)
                arrParams.Add(startDate)
                arrParams.Add(endDate)
                arrParams.Add("00000")
                arrParams.Add("00000")

                ' Retrieve summary project financial summary, security is handle on database side
                dt = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadSummaryProjectFinancialSummary_1", arrParams)

            Case "CP"   ' Child Project
                Dim strCPID As String = CType(Session("CPID"), String)
                strProjectID = strCPID

                arrParams.Add(CType(Session("UCID"), String))
                arrParams.Add(strCPID)
                arrParams.Add(startDate)
                arrParams.Add(endDate)
                arrParams.Add("00000")
                arrParams.Add("00000")

                ' Retrieve child project financial summary, security is handle on database side
                dt = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadProjectFinancialSummary_1", arrParams)

            Case "ACT"  ' Activity
                Dim strCPID As String = CType(Session("CPID"), String)
                Dim strACTID As String = CType(Session("ACTID"), String)
                strProjectID = strACTID

                arrParams.Add(CType(Session("UCID"), String))
                arrParams.Add(strCPID)
                arrParams.Add(startDate)
                arrParams.Add(endDate)
                arrParams.Add(strACTID) '@StartAtAccount varchar(20)
                arrParams.Add(strACTID) '@activitycode varchar(60)
                arrParams.Add(strACTID) '@actid varchar(20)

                ' Retrieve activity financial summary, security is handle on database side
                dt = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadActivityFinancialSummary_1", arrParams)

        End Select

        ' Save list to session for DDL
        CreateDdlData(dt, arrChildProject, arrActCode)

        ' For add Budget Variance column, remember cache table also need to do
        Dim dt2 As New DataTable
        dt2 = dt.Clone()
        RestructureDT(dt, dt2)

        dgrdFinancialSummary.DataSource = dt2
        dgrdFinancialSummary.DataBind()

        ' For collapse all data in advance 
        ToggleExpandAll(True, False)

        ' Insert "/" between the year and month for parsing
        Dim financialEndDate As String = BLL.FinancialSummary.convertDate(endDate.Insert(4, "/"), "end") 'Convert date to Month/year
        Dim financialStartDate As String = BLL.FinancialSummary.convertDate(startDate.Insert(4, "/"), "start") 'Convert date to Month/year

        'Messaging
        If endDate <= "201107" Then
            lblFinancialMessage.Text = "Salary encumbrance is calculated based on 12 month."
        Else
            lblFinancialMessage.Text = "Salary encumbrance is calculated based on project life."
        End If
        '-----------------

        lblDate.Text = financialEndDate

        lblDateFinancialFrom.Text = financialStartDate

        lblRefresh.Text = BLL.Header.GetAsAtDate.ToString("MMMM d, yyyy")

        If (strCacheSP = "NeedToCache") Then
            Session("OriginalSPCache") = dt
            Session("CacheSPID") = strProjectID
        End If

    End Sub

    Private Sub CreateDdlData(dt As DataTable, arrChildProject As ArrayList, arrActCode As ArrayList)
        Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session
        Select Case strCategory
            Case "SP"   ' Summary Project
                ' save child project list to session
                Session("AllarrChildProject") = ""
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("fld_SubProject") <> "" Then
                        If Not arrChildProject.Contains(dt.Rows(i).Item("fld_SubProject")) Then
                            arrChildProject.Add(dt.Rows(i).Item("fld_SubProject"))
                        End If
                    End If
                Next
                Session("AllarrChildProject") = arrChildProject

            Case Else
                ' save account code list to session
                Session("AllarrActCode") = ""
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("fld_Account") <> "" Then
                        arrActCode.Add(dt.Rows(i).Item("fld_Account") + " - " + dt.Rows(i).Item("fld_Desc"))
                    End If
                Next
                Session("AllarrActCode") = arrActCode
        End Select

    End Sub

    Private Sub LoadFinancialChart(ByRef startDate As String, ByRef endDate As String)

        Dim dtChart As New DataTable
        'Dim dtChart2 As New DataTable
        ' Load chart control data
        Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session
        Dim arrParams As New ArrayList
        ChartActualOverAll.Series.Clear()

        Select Case strCategory
            Case "SP"   ' Summary Project
                Dim strSPID As String = CType(Session("SPID"), String)
                arrParams.Add(CType(Session("UCID"), String))
                arrParams.Add(CType(Session("SPID"), String))
                arrParams.Add(startDate)
                arrParams.Add(endDate)

                ' Retrieve summary project financial summary, security is handle on database side
                dtChart = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadSummaryProjectExpendbyCategory", arrParams)
                If (dtChart Is Nothing) Then
                    ChartActualOverAll.Visible = False
                    ChartSpendbyCategory.Visible = False
                    lblError.Visible = True
                    lblError.Text = "There is no data for this time period."
                Else
                    If (dtChart.Rows.Count < 0) Then
                        ChartActualOverAll.Visible = False
                        ChartSpendbyCategory.Visible = False
                        lblError.Visible = True
                        lblError.Text = "There is no data for this time period."
                    Else
                        SetAndBindPPChart(dtChart)
                        ChartActualOverAll.Visible = True
                        ChartSpendbyCategory.Visible = True
                        lblError.Visible = False
                    End If
                End If

                ' Not for parent project
                ChartTotalActuals.Visible = False
            Case Else
                arrParams.Add(CType(Session("UCID"), String))
                arrParams.Add(CType(Session("CPID"), String))
                arrParams.Add(startDate)
                arrParams.Add(endDate)

                ' Retrieve summary project financial summary, security is handle on database side
                dtChart = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadProjectExpendbyCategory", arrParams)
                If (Not dtChart Is Nothing And dtChart.Rows.Count > 0) Then
                    SetAndBindCPChart(dtChart)
                    ChartActualOverAll.Visible = True
                    lblError.Visible = False
                Else
                    ChartActualOverAll.Visible = False
                    ChartSpendbyCategory.Visible = False
                    ChartTotalActuals.Visible = False
                    lblError.Visible = True
                    lblError.Text = "There is no data for this time period."
                End If
                '' Retrieve summary project financial summary, security is handle on database side
                ''dtChart2 = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadTopTenExpendofPIProject", arrParams)
                ''If (Not dtChart2 Is Nothing) Then
                ''    BindPIAllSpendChart(dtChart2)
                ''End If
        End Select

    End Sub

    Private Sub SetAndBindCPChart(dtChart As DataTable)

        ChartSpendbyCategory.Visible = False
        ChartTotalActuals.Visible = False

        '-----------------------------------------------------------------------------------
        'Bind and setting first chart
        '-----------------------------------------------------------------------------------
        ChartActualOverAll.DataBindTable(dtChart.DefaultView, "Category")
        ChartActualOverAll.Series("Budget").IsValueShownAsLabel = True
        ChartActualOverAll.Series("Actual").IsValueShownAsLabel = True

        ChartActualOverAll.ChartAreas("ChartArea1").AxisY.Title = "£¨$£©"
        ChartActualOverAll.ChartAreas("ChartArea1").AxisX.Title = "Category"
        ChartActualOverAll.Series("Budget").ToolTip = "#VALY"
        ChartActualOverAll.Series("Actual").ToolTip = "#VALY"

        'Add Legend for Chart
        Dim LegendAOA As Legend = New Legend("AOALegend")
        ChartActualOverAll.Legends.Clear()
        ChartActualOverAll.Legends.Add(LegendAOA)

        'Set the style for chart component
        ChartActualOverAll.Series("Budget").LabelFormat = "#,##0"
        ChartActualOverAll.Series("Actual").LabelFormat = "#,##0"
        ChartActualOverAll.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "#,###"

        ChartActualOverAll.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False

        ChartActualOverAll.ChartAreas("ChartArea1").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Solid
        ChartActualOverAll.ChartAreas("ChartArea1").BorderWidth = 1


        '-----------------------------------------------------------------------------------
        'Bind and setting second chart
        '-----------------------------------------------------------------------------------

        'ChartSpendbyCategory.Series("Series1").XValueMember = "Category"
        'ChartSpendbyCategory.Series("Series1").YValueMembers = "Actual"

        'ChartSpendbyCategory.DataSource = dtChart
        'ChartSpendbyCategory.DataBind()

        'ChartSpendbyCategory.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True


        'ChartActualOverAll.Series.Clear()
        'ChartActualOverAll.Legends.Clear()
        'Dim seriesBudget As Series = New Series("Budget")
        'Dim seriesActual As Series = New Series("Actual")

        'For i As Integer = 2 To List.Count - 1
        '    seriesBudget.Points.AddXY(List.Item(i).Title, 50000 * i)
        '    seriesActual.Points.AddXY(List.Item(i).Title, List.Item(i).Actual)
        'Next

        'ChartActualOverAll.Series.Add(seriesBudget)
        'ChartActualOverAll.Series.Add(seriesActual)

        'Dim seriesActual As Series = New Series("Actual")
        'Dim LegendCSC As Legend = New Legend("CSCLegend")
        'ChartSpendbyCategory.Series.Clear()
        'ChartSpendbyCategory.Series.Add("Series1")
        'ChartSpendbyCategory.Series("Series1").Points.AddXY("Online", 242)
        'ChartSpendbyCategory.Series("Series1").Points.AddXY("Offline", 256)
        'ChartSpendbyCategory.Series("Series1").Points.AddXY("Offline", 125)

        'ChartTotalActuals.Series.Clear()
        'ChartTotalActuals.Series.Add("Series2")
        'ChartTotalActuals.Series("Series2").Points.AddXY("Online", 60)
        'ChartTotalActuals.Series("Series2").Points.AddXY("Offline", 40)
        'ChartTotalActuals.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

    End Sub

    Private Sub BindPIAllSpendChart(dtChart As DataTable)
        '-----------------------------------------------------------------------------------
        'Bind and setting third chart
        '-----------------------------------------------------------------------------------
        ChartTotalActuals.Series("Series1").XValueMember = "Project"
        ChartTotalActuals.Series("Series1").YValueMembers = "Spend"

        ChartTotalActuals.DataSource = dtChart
        ChartTotalActuals.DataBind()

        ChartTotalActuals.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

    End Sub

    Private Sub SetAndBindPPChart(dtChart As DataTable)

        '--------------------------------------------------------------------
        'Bind and setting first chart
        '--------------------------------------------------------------------
        ChartActualOverAll.DataBindTable(dtChart.DefaultView, "Project")

        ChartActualOverAll.ChartAreas("ChartArea1").AxisX.Interval = 1
        ChartActualOverAll.ChartAreas("ChartArea1").AxisX.IntervalOffset = 1
        'ChartActualOverAll.ChartAreas("ChartArea1").AxisX.LabelStyle.IsStaggered = True
        'ChartActualOverAll.ChartAreas("ChartArea1").AxisX.LabelStyle.Angle = -90

        ChartActualOverAll.Series("Budget").ChartType = SeriesChartType.Bar
        ChartActualOverAll.Series("Actual").ChartType = SeriesChartType.Bar
        ChartActualOverAll.Series("Budget").IsValueShownAsLabel = True
        ChartActualOverAll.Series("Actual").IsValueShownAsLabel = True
        ChartActualOverAll.ChartAreas("ChartArea1").AxisY.Title = "£¨$£©"
        ChartActualOverAll.ChartAreas("ChartArea1").AxisX.Title = "Child Project"

        'Add Legend for Chart
        Dim LegendAOA As Legend = New Legend("AOALegend")
        ChartActualOverAll.Legends.Clear()
        ChartActualOverAll.Legends.Add(LegendAOA)

        'Set the style for chart component
        ChartActualOverAll.Series("Budget").LabelFormat = "#,##0"
        ChartActualOverAll.Series("Actual").LabelFormat = "#,##0"
        ChartActualOverAll.ChartAreas("ChartArea1").AxisY.LabelStyle.Format = "#,###"

        ChartActualOverAll.ChartAreas("ChartArea1").AxisX.MajorGrid.Enabled = False

        ChartActualOverAll.ChartAreas("ChartArea1").BorderDashStyle = DataVisualization.Charting.ChartDashStyle.Solid
        ChartActualOverAll.ChartAreas("ChartArea1").BorderWidth = 1


        '--------------------------------------------------------------------
        'Bind and setting second chart
        '--------------------------------------------------------------------
        ChartSpendbyCategory.Series("Series1").XValueMember = "Project"
        ChartSpendbyCategory.Series("Series1").YValueMembers = "Actual"
        ChartSpendbyCategory.Titles("Spend by Category").Text = "Spend by Child Project"

        ChartSpendbyCategory.DataSource = dtChart
        ChartSpendbyCategory.DataBind()

        ChartSpendbyCategory.ChartAreas("ChartArea1").Area3DStyle.Enable3D = True

    End Sub

    Private Sub RestructureDT(ByRef dt As DataTable, ByRef dt2 As DataTable)
        Dim dtIndex As Integer = 0
        Dim drOldDT As DataRow

        For dtIndex = 0 To dt.Rows.Count - 1
            drOldDT = dt.Rows(dtIndex)
            dt2.ImportRow(drOldDT)
        Next

        dt2.Columns.Add(New DataColumn("fld_BudgetVariance", GetType(String)))

        For i = 0 To dt2.Rows.Count - 1
            dt2.Rows(i).Item("fld_BudgetVariance") = ""
        Next i
    End Sub

    Private Sub LoadCacheFinancialSummary(ByRef startDate As String, ByRef endDate As String)

        Dim dt As DataTable
        dt = CType(Session("OriginalSPCache"), DataTable)

        If Not dt Is Nothing Then
            ' for add Budget Variance column
            Dim dt2 As New DataTable
            dt2 = dt.Clone()
            RestructureDT(dt, dt2)

            dgrdFinancialSummary.DataSource = dt2
            dgrdFinancialSummary.DataBind()

            'CheckOnlyOne()
            ' For collapse all data in advance 
            ToggleExpandAll(True, False)

            ' Insert "/" between the year and month for parsing
            Dim financialEndDate As String = BLL.FinancialSummary.convertDate(endDate.Insert(4, "/"), "end") 'Convert date to Month/year
            Dim financialStartDate As String = BLL.FinancialSummary.convertDate(startDate.Insert(4, "/"), "start") 'Convert date to Month/year

            'Messaging
            If endDate <= "201107" Then
                lblFinancialMessage.Text = "Salary encumbrance is calculated based on 12 month."
            Else
                lblFinancialMessage.Text = "Salary encumbrance is calculated based on project life."
            End If
            '-----------------

            lblDate.Text = financialEndDate

            lblDateFinancialFrom.Text = financialStartDate

            lblRefresh.Text = BLL.Header.GetAsAtDate.ToString("MMMM d, yyyy")

        End If
    End Sub

    ' Return the month in text corresponding to the month in number

    Protected Sub dgrdFinancialSummary_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgrdFinancialSummary.ItemCommand
        Dim nSelectIndex As Integer = e.Item.DataSetIndex

        If e.CommandName = "Collapse" Then
            ' Find the row that not include the toggle and make it invisible
            If e.Item.DataSetIndex < dgrdFinancialSummary.Items.Count Then
                For i As Integer = e.Item.DataSetIndex + 1 To dgrdFinancialSummary.Items.Count - 1
                    If (dgrdFinancialSummary.Items(i).FindControl("imgCollapse").Visible = True Or _
                        dgrdFinancialSummary.Items(i).FindControl("imgExpand").Visible = True) Then
                        Exit For
                    End If
                    dgrdFinancialSummary.Items(i).Visible = False
                Next
                ' Change the toggle line's image and check is the last one row
                HideUnHideToggleButtons(e, False, True)
                CheckIsLastOneToggle(e)
            End If
        End If

        If e.CommandName = "Expand" Then
            ' Find the row that not include the toggle and make it visible
            If e.Item.DataSetIndex < dgrdFinancialSummary.Items.Count Then
                For i As Integer = e.Item.DataSetIndex + 1 To dgrdFinancialSummary.Items.Count - 1
                    If (dgrdFinancialSummary.Items(i).FindControl("imgCollapse").Visible = True Or _
                        dgrdFinancialSummary.Items(i).FindControl("imgExpand").Visible = True) Then
                        Exit For
                    End If
                    dgrdFinancialSummary.Items(i).Visible = True
                Next
                ' Change the toggle line's image and check is the last one row
                HideUnHideToggleButtons(e, True, False)
                CheckIsLastOneToggle(e)
            End If
        End If

        If e.CommandName = "CollapseAll" Then
            ToggleExpandAll(False, False)
            HideUnHideToggleButtons(e, False, True)
        End If

        If e.CommandName = "ExpandAll" Then
            ToggleExpandAll(False, True)
            HideUnHideToggleButtons(e, True, False)
        End If
    End Sub

    Private Sub ToggleExpandAll(bIsFirstInit As Boolean, bIsExpandAll As Boolean)

        For i As Integer = 0 To dgrdFinancialSummary.Items.Count - 1
            Dim imgCollapse As Control = dgrdFinancialSummary.Items(i).FindControl("imgCollapse")
            Dim imgExpand As Control = dgrdFinancialSummary.Items(i).FindControl("imgExpand")

            ' Find the row which is own toggle button
            If imgCollapse.Visible <> False Or imgExpand.Visible <> False Then
                ' Switch the image
                If Not bIsFirstInit Then
                    imgCollapse.Visible = bIsExpandAll
                    imgExpand.Visible = Not bIsExpandAll
                End If
                Continue For
            End If

            ' Set item visible or invisible
            dgrdFinancialSummary.Items(i).Visible = bIsExpandAll
        Next

        For i As Integer = 0 To dgrdFinancialSummary.Items.Count - 1
            Dim imgCollapse As Control = dgrdFinancialSummary.Items(i).FindControl("imgCollapse")
            Dim imgExpand As Control = dgrdFinancialSummary.Items(i).FindControl("imgExpand")

            ' Find the row which is own toggle button
            If imgCollapse.Visible <> False Or imgExpand.Visible <> False Then
                ' Switch the image
                If Not bIsFirstInit Then
                    imgCollapse.Visible = bIsExpandAll
                    imgExpand.Visible = Not bIsExpandAll
                End If
                Continue For
            End If

            ' Set item visible or invisible
            dgrdFinancialSummary.Items(i).Visible = bIsExpandAll
        Next

    End Sub

    Private Sub HideUnHideToggleButtons(e As DataGridCommandEventArgs, hideCollapseButton As Boolean, hideExpandButton As Boolean)

        Dim imgCollapse As Control = e.Item.FindControl("imgCollapse")
        imgCollapse.Visible = hideCollapseButton
        Dim imgExpand As Control = e.Item.FindControl("imgExpand")
        imgExpand.Visible = hideExpandButton

    End Sub

    Private Sub CheckIsLastOneToggle(e As DataGridCommandEventArgs)

        Dim bIsFinalToogle As Boolean = True
        ' To see if the last different one toggler.
        For i As Integer = 0 To dgrdFinancialSummary.Items.Count - 1
            Dim imgCollapse As Control = dgrdFinancialSummary.Items(i).FindControl("imgCollapse")
            Dim imgExpand As Control = dgrdFinancialSummary.Items(i).FindControl("imgExpand")

            If imgCollapse.Visible <> False Or imgExpand.Visible <> False Then
                If e.Item.FindControl("imgCollapse").Visible <> imgCollapse.Visible Then
                    bIsFinalToogle = False
                    Exit For
                End If
            End If
        Next

        ' If the last one, it need to change the header one to the same image
        If bIsFinalToogle Then
            dgrdFinancialSummary.Controls(0).Controls(0).FindControl("imgCollapse").Visible = e.Item.FindControl("imgCollapse").Visible
            dgrdFinancialSummary.Controls(0).Controls(0).FindControl("imgExpand").Visible = e.Item.FindControl("imgExpand").Visible
        End If

    End Sub

    ' Set the style and color of the financial summary datagrid
    Private Sub dgrdFinancialSummary_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrdFinancialSummary.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Retrieve a record from the data table
            Dim colorFlag As String = CType(e.Item.DataItem("fld_Color"), String)
            Dim title As String = CType(e.Item.DataItem("fld_Title"), String)

            Dim strChildProject As String = CType(e.Item.DataItem("fld_SubProject"), String)
            Dim strAccount As String = CType(e.Item.DataItem("fld_Account"), String)
            Dim strDesc As String = CType(e.Item.DataItem("fld_Desc"), String)

            Dim strBudget As String = CType(e.Item.DataItem("fld_Budget"), String)
            Dim strActual As String = CType(e.Item.DataItem("fld_Actual"), String)
            Dim strEncumbrance As String = CType(e.Item.DataItem("fld_Encumbrance"), String)
            Dim strBalance As String = CType(e.Item.DataItem("fld_Balance"), String)
            Dim strBudgetVariance As String = CType(e.Item.DataItem("fld_BudgetVariance"), String)

            Dim nEncumbrance As Decimal

            If strEncumbrance <> "N/A" Then
                nEncumbrance = Decimal.Parse(strEncumbrance)
            End If

            lblOverspentOrOvercommitted.Text = ""

            ' Set the background colors
            Select Case colorFlag
                Case "0"
                    e.Item.Style("background-color") = "#ffffff"
                    Dim imgExpand As ImageButton = e.Item.FindControl("imgExpand")
                    imgExpand.Visible = False

                Case "1"
                    e.Item.Style("background-color") = "#999999"
                    e.Item.Style("font-weight") = "bold"
                    e.Item.Style("border-bottom") = "2px solid #000"

                Case "2"
                    e.Item.Style("background-color") = "#CCCCCC"
                    e.Item.Style("font-weight") = "bold"
            End Select

            ' Calculate BudgetVariance data and format the number into financial format
            If strBudget <> "N/A" And strBudget <> "" And strBalance <> "N/A" And strBalance <> "" Then
                strBudgetVariance = String.Format("{0:F2}", Decimal.Parse(strBudget) - Decimal.Parse(strBalance)).ToString()
                strBudgetVariance = String.Format("{0:n}", Decimal.Parse(strBudgetVariance))

                If Decimal.Parse(strBudgetVariance) < 0 Then
                    strBudgetVariance = "(" + strBudgetVariance.Substring(1) + ")"
                Else
                    e.Item.Cells(8).Style.Add("padding-right", "9px")
                End If

                If strBudget = "0" Then
                    strBudget = "0.00"
                End If
            End If

            ' If budget is an actual number, format the number into financial format
            If strBudget <> "N/A" And strBudget <> "" Then
                'strBudget = Decimal.Round(Decimal.Parse(strBudget), 2)
                strBudget = String.Format("{0:F2}", Decimal.Parse(strBudget)).ToString()

                strBudget = String.Format("{0:n}", Decimal.Parse(strBudget))

                If Decimal.Parse(strBudget) < 0 Then
                    strBudget = "(" + strBudget.Substring(1) + ")"
                Else
                    e.Item.Cells(5).Style.Add("padding-right", "9px")
                End If

                If strBudget = "0" Then
                    strBudget = "0.00"
                End If

            ElseIf strBudget = "N/A" Then
                e.Item.Cells(5).Style.Add("padding-right", "9px")
            End If

            ' If actual is an actual number, format the number into financial format
            If strActual <> "N/A" Then
                'strActual = Decimal.Round(Decimal.Parse(strActual), 2)
                strActual = String.Format("{0:F2}", Decimal.Parse(strActual)).ToString()

                strActual = String.Format("{0:n}", Decimal.Parse(strActual))

                If Decimal.Parse(strActual) < 0 Then
                    strActual = "(" + strActual.Substring(1) + ")"
                ElseIf Decimal.Parse(strActual) > 0 Then
                    lblOverspentOrOvercommitted.Text = "OVERSPENT"
                    e.Item.Cells(6).Style.Add("padding-right", "9px")
                    'lblOverspentOvercommittedMsg.Text = "This activity is OVERSPENT by " + strActual
                Else
                    e.Item.Cells(6).Style.Add("padding-right", "9px")
                End If

                If strActual = "0" Then
                    strActual = "0.00"
                End If

            ElseIf strActual = "N/A" Then
                e.Item.Cells(6).Style.Add("padding-right", "9px")
            End If

            ' If encumbrance is an actual number, format the number into financial format
            If strEncumbrance <> "N/A" Then
                'strEncumbrance = Decimal.Round(Decimal.Parse(strEncumbrance), 2)
                strEncumbrance = String.Format("{0:F2}", Decimal.Parse(strEncumbrance)).ToString()
                strEncumbrance = String.Format("{0:n}", Decimal.Parse(strEncumbrance))

                If Decimal.Parse(strEncumbrance) < 0 Then
                    strEncumbrance = "(" + strEncumbrance.Substring(1) + ")"
                Else
                    e.Item.Cells(6).Style.Add("padding-right", "9px")
                End If

                If strEncumbrance = "0" Then
                    strEncumbrance = "0.00"
                End If

            ElseIf strEncumbrance = "N/A" Then
                e.Item.Cells(6).Style.Add("padding-right", "9px")
            End If

            ' If balance is an actual number, format the number into financial format
            If strBalance <> "N/A" Then
                'strBalance = Decimal.Round(Decimal.Parse(strBalance), 2)
                strBalance = String.Format("{0:F2}", Decimal.Parse(strBalance)).ToString()

                strBalance = String.Format("{0:n}", Decimal.Parse(strBalance))

                If Decimal.Parse(strBalance) < 0 Then
                    strBalance = "(" + strBalance.Substring(1) + ")"
                    'added by jack on April 1, 2009 for backgroud color
                    If colorFlag = "1" Then
                        'e.Item.Cells(4).Style.Add(HtmlTextWriterStyle.BackgroundColor, "Green")
                        e.Item.Cells(8).BackColor = Drawing.Color.FromArgb(197, 227, 191)
                    End If
                    '-----------------------------------

                ElseIf Decimal.Parse(strBalance) > 0 Then
                    If Decimal.Parse(nEncumbrance) > 0 Then
                        If lblOverspentOrOvercommitted.Text <> "" Then
                            lblOverspentOrOvercommitted.Text = lblOverspentOrOvercommitted.Text + "/OVERCOMMITTED"
                        Else
                            lblOverspentOrOvercommitted.Text = "OVERCOMMITTED"
                        End If
                    End If
                    e.Item.Cells(8).Style.Add("padding-right", "9px")
                    'added by jack on April 1, 2009 for backgroud color
                    If colorFlag = "1" Then
                        'e.Item.Cells(4).Style.Add(HtmlTextWriterStyle.BackgroundColor, "Red")
                        e.Item.Cells(8).BackColor = Drawing.Color.FromArgb(205, 69, 69)
                    End If
                    '-----------------------------------
                Else
                    lblOverspentOrOvercommitted.Text = ""
                    e.Item.Cells(8).Style.Add("padding-right", "9px")

                End If

                If strBalance = "0" Then
                    strBalance = "0.00"
                End If

            ElseIf strBalance = "N/A" Then
                e.Item.Cells(8).Style.Add("padding-right", "9px")
            End If

            ' Add hyperlink for ChildProject
            Dim objTamperProof As New BLL.Security.TamperProofQueryString64
            Dim strSPID As String = CType(Session("SPID"), String)
            Dim iSecurityRank As Integer = CType(Session("SecurityRank"), Integer)

            If strChildProject <> "" Then
                Dim hyplnkSubProjuct As HyperLink
                hyplnkSubProjuct = CType(e.Item.FindControl("hyplnkChildProject"), HyperLink)
                hyplnkSubProjuct.Text = strChildProject
                Dim strToEncryptedCP As String = strChildProject + "|" + "CP"
                If iSecurityRank <= 3 Then
                    strToEncryptedCP = strToEncryptedCP + "|" + strSPID + "|" + iSecurityRank.ToString()
                End If
                Dim strEncryptedResultCP As String = objTamperProof.QueryStringEncode(strToEncryptedCP, HardKey)
                ' Set a progress bar to pop-up when user clicks on this child project
                hyplnkSubProjuct.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResultCP.Trim
            End If

            ' add hyperlink for Account
            Dim hyplnkAcc As HyperLink
            hyplnkAcc = CType(e.Item.FindControl("hyplnkAccount"), HyperLink)
            hyplnkAcc.Text = strAccount

            'added the rt425 for overhead encumbrance
            If IsNumeric(strAccount) = True Or strAccount = "RT425" Then
                Dim strToEncrypted As String = strAccount + "|" + "ACCTID"
                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, HardKey)
                hyplnkAcc.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_AccountDetails.aspx?value=" + strEncryptedResult + "%26category=PROJECTSINGLEACCOUNT"
            End If

            ' set all data to label
            Dim lbl As Label
            lbl = CType(e.Item.FindControl("lblBudget"), Label)
            lbl.Text = strBudget

            lbl = CType(e.Item.FindControl("lblActual"), Label)
            lbl.Text = strActual

            lbl = CType(e.Item.FindControl("lblEncumbrance"), Label)
            lbl.Text = strEncumbrance

            lbl = CType(e.Item.FindControl("lblBalance"), Label)
            lbl.Text = strBalance

            lbl = CType(e.Item.FindControl("lblBudgetVariance"), Label)
            lbl.Text = strBudgetVariance
        End If

    End Sub


    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentType = "application/vnd.ms-Excel"
        HttpContext.Current.Response.Charset = ""

        Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session

        Select Case strCategory
            Case "SP"   ' Summary Project
                Dim strSPID As String = CType(Session("SPID"), String)

                If Not strSPID Is Nothing Then
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strSPID + " - FinancialSummary.xls")
                End If

            Case "CP"   ' Child Project
                Dim strCPID As String = CType(Session("CPID"), String)

                If Not strCPID Is Nothing Then
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strCPID + " - FinancialSummary.xls")
                End If

            Case "ACT"  ' Activity
                Dim strCPID As String = CType(Session("CPID"), String)
                Dim strACTID As String = CType(Session("ACTID"), String)

                If Not strCPID Is Nothing And Not strACTID Is Nothing Then
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strCPID + " - " + strACTID + " - FinancialSummary.xls")
                End If

            Case Else
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=FinancialSummary.xls")

        End Select

        EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)

        'Convert the controls to string literals
        Utils.ClearControls(dgrdFinancialSummary)
        ProjectChartField2.RenderControl(oHtmlTextWriter)
        dgrdFinancialSummary.RenderControl(oHtmlTextWriter)

        HttpContext.Current.Response.Write(oStringWriter.ToString())
        HttpContext.Current.Response.End()
    End Sub


    Private Sub SetPanelVisiblity(ByRef strCategory As String)

        If strCategory.Length > 0 Then

            Select Case strCategory

                Case "SP"

                    panSummaryProject.Visible = True
                    PanDetailStatus.Visible = False
                    panTeamMember.Visible = False

                Case "CP"

                    panSummaryProject.Visible = False
                    PanDetailStatus.Visible = True
                    panTeamMember.Visible = True

                Case "ACT"

                    panSummaryProject.Visible = False
                    PanDetailStatus.Visible = True
                    panTeamMember.Visible = False
                    panBurnRate.Visible = False
                    panBurnRatePeriod.Visible = False

            End Select

        End If

    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        Dim category As String = CType(Session("Category"), String) ' Used to determine which panel to show

        Select Case category
            Case "SP"   ' Summary Project
                lblStartDate1.Text = SummaryProjectStatusControl.ProjectStartDate
                lblEndDate1.Text = SummaryProjectStatusControl.ProjectEndDate

            Case "CP"   ' Child Project
                lblStartDate1.Text = ProjectActivityStatusControl.ProjectStartDate
                lblEndDate1.Text = ProjectActivityStatusControl.ProjectEndDate
        End Select

        MyBase.Render(writer)
    End Sub

    Private Sub CalcWholePeriodBurnRate()

        Dim arrParamsBR As New ArrayList
        Dim dtBurnRate As New DataTable

        Dim strStartDate As String = BLL.ProjectPeriod.GetProjectPeriodFromTheSession.StartDate
        Dim strEndDate As String = BLL.Header.GetAsAtDate.ToString("yyyyMMdd")

        Dim category As String = CType(Session("Category"), String) ' Used to determine which panel to show

        ' Get data from database
        Select Case category
            Case "SP"   ' Summary Project
                arrParamsBR.Add(CType(Session("UCID"), String))
                arrParamsBR.Add(CType(Session("SPID"), String))
                arrParamsBR.Add(strStartDate)
                arrParamsBR.Add(strEndDate)
                dtBurnRate = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadSummaryProjectExpenditureAndBalance", arrParamsBR)

            Case "CP"
                arrParamsBR.Add(CType(Session("UCID"), String))
                arrParamsBR.Add(CType(Session("CPID"), String))
                arrParamsBR.Add(strStartDate)
                arrParamsBR.Add(strEndDate)
                dtBurnRate = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadProjectExpenditureAndBalance", arrParamsBR)
        End Select

        If Not dtBurnRate Is Nothing Then
            Dim dTotalExpend As Double = CType(dtBurnRate.Rows(0).Item(0), Double)
            Dim dBalance As Double = CType((-dtBurnRate.Rows(1).Item(0)), Double)
            Dim nMonthCount As Integer = CType(dtBurnRate.Rows(2).Item(1), Integer)

            ' Convert the format to financial
            If nMonthCount > 0 Then
                Dim strBurnRate As String = CType((dTotalExpend / nMonthCount), String)
                strBurnRate = String.Format("{0:F2}", Decimal.Parse(strBurnRate)).ToString()
                strBurnRate = String.Format("{0:n}", Decimal.Parse(strBurnRate))
                lblBurnRate1.Text = "$" + strBurnRate + " / month"

                If strBurnRate <= 0 Then
                    lblZeroBalDate1.Text = "Infinity!"
                Else
                    If dBalance > 0 Then
                        Dim nZeroBalMonth As Integer = CType((dBalance / strBurnRate), Integer)
                        If Not strEndDate Is Nothing Then
                            Dim dateEndDate As DateTime = New Date(CType(strEndDate.Substring(0, 4), Integer), CType(strEndDate.Substring(4, 2), Integer), CType(strEndDate.Substring(6, 2), Integer))
                            Dim dateZeroBalDate As DateTime = DateAdd(DateInterval.Month, nZeroBalMonth, dateEndDate)
                            Dim strZeroBalDate As String = dateZeroBalDate.ToString("MMMM, yyyy")
                            lblZeroBalDate1.Text = strZeroBalDate

                        End If
                    Else
                        lblZeroBalDate1.Text = "OVER SPENT!"
                    End If
                End If
            End If
        End If

        Session("BurnRate") = lblBurnRate1.Text
        Session("PZBD") = lblZeroBalDate1.Text
    End Sub

    Private Sub CalculateBurnRateByData(ByRef startDate As String, ByRef endDate As String)

        '---------------------------------------------------------------------------------
        Dim arrParamsBR As New ArrayList
        Dim dtBurnRate As New DataTable

        Dim strStartDate As String = startDate + "01"
        Dim strEndDate As String = endDate + "01"

        Dim category As String = CType(Session("Category"), String) ' Used to determine which panel to show
        ' Get data from database
        Select Case category
            Case "SP"   ' Summary Project
                arrParamsBR.Add(CType(Session("UCID"), String))
                arrParamsBR.Add(CType(Session("SPID"), String))
                arrParamsBR.Add(strStartDate)
                arrParamsBR.Add(strEndDate)

                dtBurnRate = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadSummaryProjectExpenditureAndBalance", arrParamsBR)

            Case "CP"
                arrParamsBR.Add(CType(Session("UCID"), String))
                arrParamsBR.Add(CType(Session("CPID"), String))
                arrParamsBR.Add(strStartDate)
                arrParamsBR.Add(strEndDate)

                dtBurnRate = BLL.FinancialSummary.LoadFinancialSummary("SELECT", "dbo.eFinsp_LoadProjectExpenditureAndBalance", arrParamsBR)
        End Select

        If Not dtBurnRate Is Nothing Then
            Dim dTotalExpend As Double = CType(dtBurnRate.Rows(0).Item(0), Double)
            Dim dBalance As Double = CType(-dtBurnRate.Rows(1).Item(0), Double)
            Dim nMonthCount As Integer = CType(dtBurnRate.Rows(2).Item(1), Integer)

            ' Convert the format to financial
            If nMonthCount > 0 Then
                Dim strBurnRate As String = (dTotalExpend / nMonthCount).ToString()
                strBurnRate = String.Format("{0:F2}", Decimal.Parse(strBurnRate)).ToString()
                strBurnRate = String.Format("{0:n}", Decimal.Parse(strBurnRate))
                lblBurnRate.Text = "$" + strBurnRate + " / month"

                If strBurnRate <= 0 Then
                    lblZeroBalDate.Text = "Infinity!"
                Else
                    If dBalance > 0 Then
                        Dim nZeroBalMonth As Integer = CType((dBalance / strBurnRate), Integer)
                        If Not strEndDate Is Nothing Then
                            Dim dateEndDate As DateTime = New Date(CType(strEndDate.Substring(0, 4), Integer), CType(strEndDate.Substring(4, 2), Integer), CType(strEndDate.Substring(6, 2), Integer))
                            Dim dateZeroBalDate As DateTime = DateAdd(DateInterval.Month, nZeroBalMonth, dateEndDate)
                            Dim strZeroBalDate As String = dateZeroBalDate.ToString("MMMM, yyyy")
                            lblZeroBalDate.Text = strZeroBalDate

                        End If
                    Else
                        lblZeroBalDate.Text = "OVER SPENT!"
                    End If
                End If
            End If

            Dim financialEndDate As String = BLL.FinancialSummary.convertDate(strEndDate.Substring(0, 6).Insert(4, "/"), "end") 'Convert date to Month/year
            Dim financialStartDate As String = BLL.FinancialSummary.convertDate(strStartDate.Substring(0, 6).Insert(4, "/"), "start") 'Convert date to Month/year


            lblEndDate.Text = financialEndDate
            lblStartDate.Text = financialStartDate
        End If

    End Sub

    Protected Sub ddlChildproject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlChildproject.SelectedIndexChanged

        Dim strSecurityRank As String = CType(Session("SecurityRank"), String) ' Used to determine to cache table or not
        Dim strToEncrypted As String = ddlChildproject.SelectedValue + "|" + "CP"
        strToEncrypted = strToEncrypted + "|" + hypParentProject.Text + "|" + strSecurityRank
        Dim objTamperProof As New BLL.Security.TamperProofQueryString64
        Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, HardKey)

        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim)
    End Sub

End Class
