Option Compare Text

Partial Class Forms_Researcher_frm_ProjectList
    Inherits System.Web.UI.Page

    Private HardKey As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session("UCID") Is Nothing Then
            Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            CType(Page.Master, MasterPage).PageIdentity = "Enterprise Reporting<br/>eFIN - 02"
        End If

        Dim admin As AppUser = CType(Session("user"), AppUser)
        If admin Is Nothing OrElse Not admin.IsActive Then
            lbSecurity.Visible = False
        End If

        If BLL.EfinSecurityOverride.EfinSecurityOverride Then
            Dim arrParams As New ArrayList

            arrParams.Add(CType(Session("UCID"), String))
            If Not BLL.EfinSecurityOverride.ShowReportGeneration("SELECT", "dbo.eFinsp_EfinOverrideUser", arrParams) Then
                lbGenerateReport.Enabled = False
            Else
                'open new window for report generation
                lbGenerateReport.Attributes.Add("onClick", "javascript:window.open('" + lbGenerateReport.CommandArgument + "')")
                '------------------------------------------------------
            End If
        Else
            If Not BLL.EfinSecurityOverride.UserHasProjectLevelAccess(CType(Session("UCID"), String)) Then
                lbGenerateReport.Enabled = False
            Else
                'open new window for report generation
                lbGenerateReport.Attributes.Add("onClick", "javascript:window.open('" + lbGenerateReport.CommandArgument + "')")
                '------------------------------------------------------
            End If
        End If
        '------------------------------------------------------


        Session("PageStart") = Date.Now

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        HardKey = CType(Session("RandomKey"), String)


        'First time on Page_Load
        If Not Me.IsPostBack And Not Me.IsCallback Then
            Dim projectID As String

            'Added by Jack on May 11 for searching by PH
            Dim ProjectSearch As New ArrayList
            ProjectSearch.Add("")
            ProjectSearch.Add("ALL")
            ProjectSearch.Add("")
            ProjectSearch.Add("")
            ProjectSearch.Add("")

            '-----------------------------------------------

            HeaderControl.InitializeDate()  ' Initialized the date of the header control so we have a date to put into session
            Session("Category") = ""        ' We're going to project list page, thus we're not in a category

            ' If a period is not currently in session, store current fiscal year period
            If Session("Period") Is Nothing Then
                BLL.Period.AddPeriodToTheSession(New BLL.Period(HeaderControl.GetStartDate, HeaderControl.GetEndDate))
            End If

            EnableStartAtProject()          ' Enable the Start At Project controls
            dgProjects.CurrentPageIndex = 0 ' Reset page index for the project list

            ' Since first load on the page, grab period from session
            'endDate = BLL.Period.GetPeriodFromTheSession.EndDate
            projectID = ""
            If Session("StartAtProject") IsNot Nothing Then
                projectID = Session("StartAtProject")
                HeaderControl.SetProjectID = projectID
            End If
            'Added by Jack on May 12 for searching by PH
            If Session("ProjectSearch") IsNot Nothing Then
                ProjectSearch = Session("ProjectSearch")
                If ProjectSearch(1) <> "ALL" Or ProjectSearch(2) <> "" Or ProjectSearch(3) <> "" Or ProjectSearch(4) <> "" Then
                    HeaderControl.ibHead_Click(Nothing, Nothing) 'expand the advanced search
                End If
            End If

            ' load projectHolder if many datas (over 100) then open GetPHControl panel
            LoadProjectHolders()

            If Session("OriginalProjectList") Is Nothing Then
                If Session("isadmin") <> "true" Then
                    LoadAndBuildProjectList()    ' Grab project list by end date

                End If
                Session("StartAtProject") = projectID
                Session("ProjectSearch") = ProjectSearch
                Return
            End If

            Try
                If Session("isadmin") = "true" Then
                    SearchAndBuildProjectList(projectID, ProjectSearch)
                Else
                    LoadCacheProjectList(projectID, ProjectSearch)    ' Grab project list by end date
                End If

                'Session("EndDate") = endDate
                Session("ProjectSearch") = ProjectSearch
                Session("StartAtProject") = projectID

            Catch ex As Exception

            Finally

            End Try

        End If

    End Sub

    Private Sub SearchProject(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HeaderControl.Search
        Dim projectID As String
        Dim ProjectSearch As New ArrayList

        projectID = HeaderControl.GetProjectID.ToUpper.Trim
        ProjectSearch = HeaderControl.ProjectSearch
        Try
            If Session("isadmin") = "true" Then
                SearchAndBuildProjectList(projectID, ProjectSearch)
            Else
                LoadCacheProjectList(projectID, ProjectSearch)    ' Grab project list by end date
            End If

            'Session("EndDate") = endDate
            Session("ProjectSearch") = ProjectSearch
            Session("StartAtProject") = projectID

        Catch ex As Exception

        Finally
        End Try
    End Sub

    ' Enable the Start At Project feature in the project list page
    Private Sub EnableStartAtProject()
        Dim pan As Panel = CType(HeaderControl.FindControl("panStartAt"), Panel)
        pan.Visible = True

        Dim lbl As Label = CType(HeaderControl.FindControl("lblStartAtProject"), Label)

        lbl.Visible = True
        'lbl.Text = "Start At Project:"
    End Sub


    ' Retrieve the project list from the data warehouse given the end date, UCID and project ID(if exist)
    Private Sub LoadAndBuildProjectList()
        Dim dt As DataTable
        Dim arrParams As New ArrayList

        ' Add parameters used for the database
        arrParams.Add(CType(Session("UCID"), String))
        'arrParams.Add(endDate)
        'arrParams.Add(projectID)

        ' User entered a different period or project id
        ' We must go to data warehouse to grab the project list corresponding to the new parameters
        dt = BLL.ProjectList.getProjectList("SELECT", "dbo.eFinsp_LoadPrejoinedProjectListWithSecurity", arrParams)

        Dim dt2 As New DataTable
        dt2 = dt.Clone()
        RestructureDT(dt, dt2)

        ' Remove old project list cache data, insert new project list into cache
        If Not dt2 Is Nothing Then
            'Cache.Remove("ProjectListDataTable")
            'Cache.Insert("ProjectListDataTable", dt2)
            Session("OriginalProjectList") = dt2
            Session("ProjectListDataTable") = dt2
            dgProjects.CurrentPageIndex = 0
            Dim dv As New DataView(dt2)
            BinddgProjects(dv) ' Bind project list to datagrid

        End If
        dt.Dispose()
        dt2.Dispose()
    End Sub

    'Added by Jack on May 11 for searching PH
    Private Sub LoadProjectHolders()
        Dim dt As DataTable
        Dim arrParams As New ArrayList
        If Session("ProjectHolder") Is Nothing Then
            arrParams.Add(CType(Session("UCID"), String))
            dt = BLL.ProjectList.getProjectList("SELECT", "dbo.eFinsp_LoadPrejoinedProjectHolderWithSecurity", arrParams)

            If Not dt Is Nothing Then
                If dt.Rows.Count > 100 Then
                    'HeaderControl.GetPHControl2.EnableLoadOnDemand = True
                Else
                    'HeaderControl.GetPHControl2.EnableLoadOnDemand = False
                    Dim dr As DataRow = dt.NewRow()
                    dt.Rows.InsertAt(dr, 0)
                    HeaderControl.GetPHControl2.DataSource = dt
                    HeaderControl.GetPHControl2.DataValueField = "ProjectHolder"
                    HeaderControl.GetPHControl2.DataTextField = "ProjectHolder"
                    HeaderControl.GetPHControl2.DataBind()
                    Session("ProjectHolder") = dt
                End If
            End If
            dt.Dispose()
        Else
            HeaderControl.GetPHControl2.DataSource = CType(Session("ProjectHolder"), DataTable)
            HeaderControl.GetPHControl2.DataValueField = "ProjectHolder"
            HeaderControl.GetPHControl2.DataTextField = "ProjectHolder"
            HeaderControl.GetPHControl2.DataBind()
            If Session("ProjectSearch") IsNot Nothing Then
                Dim ph As String = (CType(Session("ProjectSearch"), ArrayList))(0).Replace("''", "'")
                If HeaderControl.GetPHControl2.Items.FindByValue(ph) IsNot Nothing Then
                    HeaderControl.GetPHControl2.Items.FindByValue(ph).Selected = True
                End If
                HeaderControl.GetPHControl2.Text = ph

            End If
        End If

    End Sub
    '-----------------------------------------------------------------

    Private Sub SearchAndBuildProjectList(ByVal projectid As String, ByVal para As ArrayList)
        Dim prePara As ArrayList = CType(Session("ProjectSearch"), ArrayList)

        Dim dt As DataTable
        Dim arrParams As New ArrayList
        If projectid.Trim = "" And para(1) = "ALL" And para(2) = "" And para(3) = "" And para(4) = "" Then
            Return
        End If

        If (Not Session("StartAtProject") Is Nothing) AndAlso CType(Session("StartAtProject"), String) <> projectid Or _
        (prePara(1) <> para(1)) Or (prePara(2) <> para(2)) Or (prePara(3) <> para(3)) Or (prePara(4) <> para(4)) Then
            arrParams.Add(CType(Session("UCID"), String))
            arrParams.Add(projectid)
            arrParams.Add(para(1))
            arrParams.Add(para(2))
            arrParams.Add(para(3))
            arrParams.Add(para(4))

            dt = BLL.ProjectList.getProjectList("SELECT", "eFinsp_LoadPrejoinedProjectListByProjectCode", arrParams)

            Dim dt2 As New DataTable
            dt2 = dt.Clone()
            RestructureDT(dt, dt2)

            ' Remove old project list cache data, insert new project list into cache
            If Not dt2 Is Nothing Then

                Session("OriginalProjectList") = dt2

                dgProjects.CurrentPageIndex = 0

                Dim dv As New DataView(dt2)

                BinddgProjects(dv) ' Bind project list to datagrid

            End If
            dt.Dispose()
            dt2.Dispose()
        End If

        If Session("OriginalProjectList") IsNot Nothing Then
            dt = CType(Session("OriginalProjectList"), DataTable)    ' Grab cache data

            If Not dt Is Nothing Then
                Dim dv As New DataView(dt)
                BinddgProjects(dv) ' Rebind cache data

                Return
            End If
        End If


    End Sub

    Private Sub LoadCacheProjectList(ByRef projectID As String, ByVal ProjectSearch As ArrayList)

        Dim dt As DataTable

        If (Not Session("StartAtProject") Is Nothing) AndAlso CType(Session("StartAtProject"), String) <> projectID Then
            'Dim dt3 As New DataTable

            dt = CType(Session("OriginalProjectList"), DataTable).Copy    ' Grab cache data


            If Not dt Is Nothing Then
                Dim i As Integer

                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows(0).Item("fld_Project_Code") Like "*" + projectID + "*" Or dt.Rows(0).Item("fld_Parent_Project_Code") Like "*" + projectID + "*" Then
                        Exit For
                    Else
                        dt.Rows.RemoveAt(0)
                    End If
                Next

            End If

            If Not dt Is Nothing Then
                Session("ProjectListDataTable") = dt

                dgProjects.CurrentPageIndex = 0
                'BinddgProjects(dt3) ' Rebind cache data

                'Return

            End If
        End If

        If (Not Session("StartAtProject") Is Nothing) AndAlso CType(Session("StartAtProject"), String) = projectID AndAlso projectID <> "" Then
            'dt = CType(Cache.Get("ProjectListDataTable"), DataTable)    ' Grab cache data
            dt = CType(Session("ProjectListDataTable"), DataTable)    ' Grab cache data

            If Not dt Is Nothing Then
                'BinddgProjects(dt) ' Rebind cache data

                'Return

            End If
        End If

        If (Not Session("OriginalProjectList") Is Nothing) AndAlso projectID = "" Then
            dt = CType(Session("OriginalProjectList"), DataTable)

            If Not dt Is Nothing Then

                ''dgProjects.CurrentPageIndex = 0
                'BinddgProjects(dt) ' Rebind cache data

                'Return

            End If
        End If

        If dt IsNot Nothing Then
            Dim dv As DataView = New DataView(dt)
            Dim fil As String = "fld_project_desc like '%" + ProjectSearch(0) + "%'"

            If ProjectSearch(1) <> "ALL" Then
                fil = fil + " and Project_Status_Desc = '" + ProjectSearch(1) + "'"
            End If
            If ProjectSearch(2) <> "" Then
                fil = fil + " and Fac_ID = '" + ProjectSearch(2) + "'"
            End If
            If ProjectSearch(3) <> "" Then
                fil = fil + " and Dept_ID = '" + ProjectSearch(3) + "'"
            End If
            If ProjectSearch(4) <> "" Then
                fil = fil + " and (fld_Parent_Project_Desc like '%" + ProjectSearch(4) + _
                "%' or fld_Project_Desc like '%" + ProjectSearch(4) + "%' or fld_Reference_Number like '%" + ProjectSearch(4) + _
                "%' or fld_Second_Reference_Number like '%" + ProjectSearch(4) + "%' or keywords like '%" + ProjectSearch(4) + "%')"
            End If
            dv.RowFilter = fil
            BinddgProjects(dv)
        End If
    End Sub


    ' Bind datatable to datagrid
    Private Sub BinddgProjects(ByRef dt As DataView)
        Me.btnExcel.Visible = True
        dgProjects.DataSource = dt
        If dt.Count > 1000 Then
            likPaging.Enabled = False
        End If

        If Not Me.IsPostBack And Not Me.IsCallback Then
            If Request.Cookies("dvPaging") Is Nothing Then
                Response.Cookies("dvPaging").Value = "True"
            End If
            dgProjects.AllowPaging = CType(Request.Cookies("dvPaging").Value, Boolean)
            If dgProjects.AllowPaging = True Then
                likPaging.Text = "Off"
            Else
                likPaging.Text = "On"
            End If
        End If

        dgProjects.DataBind()
    End Sub


    ' The datatable queried from the database needs to be restructured such that it can be displayed properly
    ' on the project list screen.  This functions adds the top tag needed to display parent projects and
    ' child projects correctly on the project list screen.
    Private Sub RestructureDT(ByRef dt As DataTable, ByRef dt2 As DataTable)
        Dim dtIndex As Integer = 0
        Dim drOldDT As DataRow
        Dim dtPPIndex As Integer = 0
        Dim drOldPPDT As DataRow
        Dim drNewDT As DataRow

        Dim parentProjectCode As String = ""
        Dim projectCode As String = ""

        Dim currentParentProjectCode As String = ""
        Dim currentProjectCode As String = ""

        For dtIndex = 0 To dt.Rows.Count - 1
            drOldDT = dt.Rows(dtIndex)
            drOldPPDT = drOldDT
            currentParentProjectCode = drOldDT("fld_Parent_Project_Code").ToString.Trim

            ' Find PP SecurityRank
            For dtPPIndex = 0 To dt.Rows.Count - 1
                If (dt.Rows(dtPPIndex).Item("fld_Parent_Project_Code").ToString.Trim = currentParentProjectCode And _
                    dt.Rows(dtPPIndex).Item("fld_Project_Type").ToString.Trim = "PP") Then
                    drOldPPDT = dt.Rows(dtPPIndex)
                End If
            Next

            If parentProjectCode <> currentParentProjectCode And drOldDT("fld_Project_Type").ToString.Trim = "CP" And drOldDT("fld_Parent_Child_Flag").ToString.Trim = "Y" Then
                parentProjectCode = currentParentProjectCode

                If dtIndex <> 0 Then
                    drNewDT = dt2.NewRow
                    drNewDT(0) = ""
                    drNewDT(1) = ""
                    drNewDT(2) = ""
                    drNewDT(3) = ""
                    drNewDT(4) = ""
                    drNewDT(5) = "100"
                    drNewDT(6) = "0.00"
                    drNewDT(7) = "0.00"
                    drNewDT(8) = "0.00"
                    drNewDT(9) = "0.00"
                    drNewDT(10) = ""
                    drNewDT(11) = ""
                    drNewDT(12) = ""
                    drNewDT(13) = ""
                    drNewDT(14) = ""

                    drNewDT(15) = ""
                    drNewDT(16) = ""
                    drNewDT(17) = ""
                    drNewDT(18) = ""
                    drNewDT(19) = ""
                    drNewDT(20) = ""
                    drNewDT(21) = ""
                    dt2.Rows.Add(drNewDT)
                End If

                drNewDT = dt2.NewRow
                drNewDT(0) = "Y"
                drNewDT(1) = currentParentProjectCode
                drNewDT(2) = "PPT"
                drNewDT(3) = "XXXXXXXXXXXXXXX"
                drNewDT(4) = "XXXXXXXXXXXXXXX"
                drNewDT(5) = drOldPPDT("fld_Security_Rank").ToString.Trim
                drNewDT(6) = "0.00"
                drNewDT(7) = "0.00"
                drNewDT(8) = "0.00"
                drNewDT(9) = "0.00"
                drNewDT(10) = drOldDT("fld_Parent_Project_Desc").ToString.Trim
                drNewDT(11) = ""
                drNewDT(12) = ""
                drNewDT(13) = ""
                drNewDT(14) = ""

                drNewDT(15) = ""
                drNewDT(16) = ""
                drNewDT(17) = ""
                drNewDT(18) = ""
                drNewDT(19) = ""
                drNewDT(20) = ""
                drNewDT(21) = ""

                dt2.Rows.Add(drNewDT)

                projectCode = drOldDT("fld_Project_Code").ToString.Trim

                drNewDT = dt2.NewRow
                drNewDT(0) = "Y"
                drNewDT(1) = parentProjectCode
                drNewDT(2) = "CP"
                drNewDT(3) = projectCode
                drNewDT(4) = "YYYYYYYYYYYYYYY"
                drNewDT(5) = drOldDT("fld_Security_Rank").ToString.Trim
                drNewDT(6) = "0.00"
                drNewDT(7) = "0.00"
                drNewDT(8) = "0.00"
                drNewDT(9) = "0.00"

                ' Just put securityRank in this field to send
                drNewDT(10) = drOldPPDT("fld_Security_Rank").ToString.Trim

                drNewDT(11) = drOldDT("fld_Project_Desc").ToString.Trim
                drNewDT(12) = drOldDT("fld_Reference_Number").ToString.Trim
                drNewDT(13) = drOldDT("fld_Second_Reference_Number").ToString.Trim
                drNewDT(14) = ""

                drNewDT(15) = drOldDT("Project_Type_Desc").ToString.Trim
                drNewDT(16) = drOldDT("Other_Attributes").ToString.Trim
                drNewDT(17) = drOldDT("Project_Status_Desc").ToString.Trim
                drNewDT(18) = drOldDT("Department").ToString.Trim
                drNewDT(19) = drOldDT("Dept_ID").ToString.Trim
                drNewDT(20) = drOldDT("Fac_ID").ToString.Trim
                drNewDT(21) = drOldDT("KeyWords").ToString.Trim

                dt2.Rows.Add(drNewDT)
                dt2.ImportRow(drOldDT)

            ElseIf parentProjectCode <> currentParentProjectCode And drOldDT("fld_Project_Type").ToString.Trim = "PP" And drOldDT("fld_Parent_Child_Flag").ToString.Trim = "Y" Then
                parentProjectCode = currentParentProjectCode

                If dtIndex <> 0 Then
                    drNewDT = dt2.NewRow
                    drNewDT(0) = ""
                    drNewDT(1) = ""
                    drNewDT(2) = ""
                    drNewDT(3) = ""
                    drNewDT(4) = ""
                    drNewDT(5) = "100"
                    drNewDT(6) = "0.00"
                    drNewDT(7) = "0.00"
                    drNewDT(8) = "0.00"
                    drNewDT(9) = "0.00"
                    drNewDT(10) = ""
                    drNewDT(11) = ""
                    drNewDT(12) = ""
                    drNewDT(13) = ""
                    drNewDT(14) = ""

                    drNewDT(15) = ""
                    drNewDT(16) = ""
                    drNewDT(17) = ""
                    drNewDT(18) = ""
                    drNewDT(19) = ""
                    drNewDT(20) = ""
                    drNewDT(21) = ""


                    dt2.Rows.Add(drNewDT)
                End If

                drNewDT = dt2.NewRow
                drNewDT(0) = "Y"
                drNewDT(1) = currentParentProjectCode
                drNewDT(2) = "PPT"
                drNewDT(3) = "XXXXXXXXXXXXXXX"
                drNewDT(4) = "XXXXXXXXXXXXXXX"
                drNewDT(5) = drOldDT("fld_Security_Rank").ToString.Trim
                drNewDT(6) = "0.00"
                drNewDT(7) = "0.00"
                drNewDT(8) = "0.00"
                drNewDT(9) = "0.00"
                drNewDT(10) = drOldDT("fld_Parent_Project_Desc").ToString.Trim
                drNewDT(11) = ""
                drNewDT(12) = ""
                drNewDT(13) = ""
                drNewDT(14) = ""

                drNewDT(15) = ""
                drNewDT(16) = ""
                drNewDT(17) = ""
                drNewDT(18) = ""
                drNewDT(19) = ""
                drNewDT(20) = ""
                drNewDT(21) = ""

                dt2.Rows.Add(drNewDT)

                dt2.ImportRow(drOldDT)

            Else
                If (drOldDT("fld_Activity_Code").ToString().Trim() = "ZZZZZZZZZZZZZZZ") Then
                    drOldDT("fld_Parent_Project_Desc") = drOldPPDT("fld_Security_Rank").ToString.Trim
                End If

                currentProjectCode = drOldDT("fld_Project_Code").ToString.Trim

                If projectCode <> currentProjectCode And drOldDT("fld_Project_Type").ToString.Trim = "CP" Then
                    projectCode = currentProjectCode

                    If drOldDT("fld_Parent_Child_Flag").ToString.Trim = "N" Then
                        drNewDT = dt2.NewRow
                        drNewDT(0) = ""
                        drNewDT(1) = ""
                        drNewDT(2) = ""
                        drNewDT(3) = ""
                        drNewDT(4) = ""
                        drNewDT(5) = "100"
                        drNewDT(6) = "0.00"
                        drNewDT(7) = "0.00"
                        drNewDT(8) = "0.00"
                        drNewDT(9) = "0.00"
                        drNewDT(10) = ""
                        drNewDT(11) = ""
                        drNewDT(12) = ""
                        drNewDT(13) = ""
                        drNewDT(14) = ""

                        drNewDT(15) = ""
                        drNewDT(16) = ""
                        drNewDT(17) = ""
                        drNewDT(18) = ""
                        drNewDT(19) = ""
                        drNewDT(20) = ""
                        drNewDT(21) = ""

                        dt2.Rows.Add(drNewDT)
                    End If

                    drNewDT = dt2.NewRow
                    drNewDT(0) = "N"
                    drNewDT(1) = parentProjectCode
                    drNewDT(2) = "CP"
                    drNewDT(3) = projectCode
                    drNewDT(4) = "YYYYYYYYYYYYYYY"
                    drNewDT(5) = drOldDT("fld_Security_Rank").ToString.Trim
                    drNewDT(6) = "0.00"
                    drNewDT(7) = "0.00"
                    drNewDT(8) = "0.00"
                    drNewDT(9) = "0.00"
                    drNewDT(10) = ""
                    drNewDT(11) = drOldDT("fld_Project_Desc").ToString.Trim
                    drNewDT(12) = drOldDT("fld_Reference_Number").ToString.Trim
                    drNewDT(13) = drOldDT("fld_Second_Reference_Number").ToString.Trim
                    drNewDT(14) = ""

                    drNewDT(15) = drOldDT("Project_Type_Desc").ToString.Trim
                    drNewDT(16) = drOldDT("Other_Attributes").ToString.Trim
                    drNewDT(17) = drOldDT("Project_Status_Desc").ToString.Trim
                    drNewDT(18) = drOldDT("Department").ToString.Trim
                    drNewDT(19) = drOldDT("Dept_ID").ToString.Trim
                    drNewDT(20) = drOldDT("Fac_ID").ToString.Trim
                    drNewDT(21) = drOldDT("KeyWords").ToString.Trim

                    dt2.Rows.Add(drNewDT)

                    dt2.ImportRow(drOldDT)

                Else
                    dt2.ImportRow(drOldDT)

                End If

            End If

        Next
    End Sub


    Private Sub dgProjects_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgProjects.ItemDataBound
        ' tried to test restricted access to the project on 17 dec by david 
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            ' Retrieve a record from the data table
            Dim parentChildFlag As String = CType(e.Item.DataItem("fld_Parent_Child_Flag"), String)
            Dim parentProjectCode As String = CType(e.Item.DataItem("fld_Parent_Project_Code"), String)
            Dim projectType As String = CType(e.Item.DataItem("fld_Project_Type"), String)
            Dim projectCode As String = CType(e.Item.DataItem("fld_Project_Code"), String)
            Dim activityCode As String = CType(e.Item.DataItem("fld_Activity_Code"), String)
            Dim securityRank As Integer = CType(e.Item.DataItem("fld_Security_Rank"), String)
            Dim budget As Decimal = CType(e.Item.DataItem("fld_Budget"), Decimal)
            Dim actual As Decimal = CType(e.Item.DataItem("fld_Actual"), Decimal)
            Dim preEncumbrance As Decimal = CType(e.Item.DataItem("fld_Pre_Encumbrance"), Decimal)
            Dim encumbrance As Decimal = CType(e.Item.DataItem("fld_Encumbrance"), Decimal)
            Dim parentProjectDesc As String = CType(e.Item.DataItem("fld_Parent_Project_Desc"), String)
            Dim projectDesc As String = CType(e.Item.DataItem("fld_Project_Desc"), String)
            Dim referenceNumber As String = CType(e.Item.DataItem("fld_Reference_Number"), String)
            Dim secondReferenceNumber As String = CType(e.Item.DataItem("fld_Second_Reference_Number"), String)
            Dim activityDesc As String = CType(e.Item.DataItem("fld_Activity_Desc"), String)

            'added by jack March 2011 for additional attributes
            Dim projectTypeDesc As String = CType(e.Item.DataItem("Project_Type_Desc"), String)
            Dim otherAttributes As String = CType(e.Item.DataItem("Other_Attributes"), String)

            Dim strBalance As String


            strBalance = String.Format("{0:n}", actual + preEncumbrance + encumbrance)

            If Decimal.Parse(strBalance) < 0 Then
                strBalance = "(" + strBalance.Substring(1) + ")"
                'added by jack on April 1, 2009 for backgroud color
                'e.Item.Cells(4).Style.Add(HtmlTextWriterStyle.BackgroundColor, "Green")
                e.Item.Cells(6).BackColor = Drawing.Color.FromArgb(197, 227, 191)
                '-----------------------------------
            Else
                e.Item.Cells(6).Style.Add("padding-right", "9px")
                'added by jack on April 1, 2009 for background color
                If Decimal.Parse(strBalance) <> 0 Then
                    'e.Item.Cells(4).Style.Add(HtmlTextWriterStyle.BackgroundColor, "Red")
                    e.Item.Cells(6).BackColor = Drawing.Color.FromArgb(205, 69, 69)
                End If
                '----------------------------------------
            End If
            'securityRank = 4 ' added by David on 17 Dec. 2013 for testing

            Select Case parentChildFlag

                Case "Y"


                    Select Case projectType

                        Case "CP"


                            Select Case activityCode

                                Case "YYYYYYYYYYYYYYY"

                                    e.Item.Style("background-color") = "#999999"
                                    e.Item.Cells(0).Style.Add("text-align", "right")

                                    Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkProject"), HyperLink)
                                    Dim lbl As Label = CType(e.Item.FindControl("lblDescription"), Label)
                                    Dim lblProjectType As Label = CType(e.Item.FindControl("lblProjectType"), Label)
                                    Dim lblOtherAttributes As Label = CType(e.Item.FindControl("lblOtherAttributes"), Label)

                                    hyplnk.Text = projectCode
                                    lbl.Text = projectDesc

                                    lblProjectType.Text = projectTypeDesc
                                    lblOtherAttributes.Text = otherAttributes

                                    lbl = CType(e.Item.FindControl("lblReference"), Label)
                                    lbl.Text = referenceNumber '+ " " + secondReferenceNumber

                                    If securityRank <= 4 Or securityRank = 7 Or securityRank = 9 Then
                                        Dim strToEncrypted As String = projectCode + "|" + "CP"

                                        strToEncrypted = strToEncrypted + "|" + parentProjectCode + "|" + parentProjectDesc
                                    
                                    Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                                    Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                                    ' Set a progress bar to pop-up when user clicks on this child project
                                    hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                                    hyplnk.Text = projectCode
                                    End If

                                Case "ZZZZZZZZZZZZZZZ"

                                    e.Item.Style("background-color") = "#FFFFFF"
                                    e.Item.Style("color") = "black"
                                    e.Item.Cells(0).Style.Add("text-align", "right")

                                    Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkProject"), HyperLink)
                                    Dim lbl As Label = CType(e.Item.FindControl("lblBalance"), Label)

                                    hyplnk.Text = projectCode

                                    ' If security rank is <= 4 then we can show this user the balance of this child project and
                                    ' provide a link to the child project details
                                    'lbl.Text = strBalance 'CHANGED BY DAVID ON 2 jAN.2014
                                    'e.Item.Cells(6).Style.Add("padding-right", "9px")

                                    If securityRank <= 4 Or securityRank = 7 Or securityRank = 9 Then ' changed by David on 17 Dec.2013
                                        Dim strToEncrypted As String = projectCode + "|" + "CP"

                                        strToEncrypted = strToEncrypted + "|" + parentProjectCode + "|" + parentProjectDesc
                                        Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                                        Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                                        ' Set a progress bar to pop-up when user clicks on this child project
                                        hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                                        'lbl.Text = strBalance 'add by david on 16 dec 2013
                                        If securityRank <= 4 Then
                                            lbl.Text = strBalance
                                        Else
                                            lbl.Text = "N/A"
                                            e.Item.Cells(6).Style.Add("padding-right", "9px") 'should be Cells(6) instead of Cells(4) by david on 27 Jan. 2014

                                        End If

                                    Else
                                        lbl.Text = "N/A"    ' Can't see financial total
                                        'lbl.Text = strBalance 'added by david on 17 dec 2013
                                        e.Item.Cells(6).Style.Add("padding-right", "9px")

                                    End If

                                    lbl = CType(e.Item.FindControl("lblDescription"), Label)
                                    'lbl.Text = "TOTAL"

                                Case Else 'activities 

                                    e.Item.Style("background-color") = "#ffffff"    ' Set background color to white

                                    Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkActivity"), HyperLink)
                                    Dim lbl As Label = CType(e.Item.FindControl("lblBalance"), Label)

                                    ' This if statement handles the junk data from the database
                                    ' If activity is a " ", then it is a blank activity
                                    ' We assigned the hyperlink an "@" so we can click the hyperlink
                                    ' otherwise the hyperlink will not show up
                                    If activityCode = " " Then
                                        hyplnk.Text = "N/A"
                                    Else
                                        hyplnk.Text = activityCode
                                    End If

                                    ' If security rank is <= 9 changed to 9 from 5 David on 17 dec 2013 then we can show this user the balance of this activity
                                    If securityRank <= 5 Then
                                        lbl.Text = strBalance

                                    Else
                                        lbl.Text = "N/A"
                                        e.Item.Cells(6).Style.Add("padding-right", "9px")

                                    End If

                                    ' Provide link to activity details if security permits
                                    If securityRank <> 99 Then

                                        Dim strToEncrypted As String = activityCode + "|" + projectCode + "|" + "ACT"
                                        Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                                        Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                                        ' Set a progress bar to pop-up when user clicks on this activity
                                        hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                                    End If

                                    lbl = CType(e.Item.FindControl("lblDescription"), Label)
                                    lbl.Text = activityDesc

                            End Select ' end select activitycode

                        Case "PP"

                            e.Item.Style("background-color") = "#FFFFFF"
                            e.Item.Style("color") = "black"
                            'e.Item.Cells(0).Style.Add("font-weight", "bold")

                            Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkProject"), HyperLink)
                            Dim lbl As Label = CType(e.Item.FindControl("lblBalance"), Label)
                            Dim strCacheSPID As String = CType(Session("CacheSPID"), String) ' Used to determine to cache table or not

                            hyplnk.Text = parentProjectCode + " - Parent"

                            ' If security rank is <= 3 then we can show this user the balance of this summary project and
                            ' provide a link to the project details
                            If securityRank <= 3 Then
                                Dim strToEncrypted As String = ""
                                If strCacheSPID <> parentProjectCode Then
                                    strToEncrypted = parentProjectCode + "|" + "SP" + "|" + "NeedToCache" + "|" + securityRank.ToString()
                                Else
                                    strToEncrypted = parentProjectCode + "|" + "SP" + "|" + parentProjectCode + "|" + securityRank.ToString()
                                End If

                                Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                                ' Set a progress bar to pop-up when user clicks on this summary project
                                hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                                lbl.Text = strBalance

                            Else
                                lbl.Text = "N/A" 'strBalance
                                'lbl.Text = strBalance 'added by david on 17 dec 2013
                            End If

                            lbl = CType(e.Item.FindControl("lblDescription"), Label)
                            'lbl.Text = "TOTAL"

                        Case "PPT"
                            e.Item.Style("background-color") = "#CCCCCC"
                            'e.Item.Cells(0).Style.Add("font-weight", "bold")

                            Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkProject"), HyperLink)
                            Dim lbl As Label = CType(e.Item.FindControl("lblDescription"), Label)
                            Dim strCacheSPID As String = CType(Session("CacheSPID"), String) ' Used to determine to cache table or not

                            If securityRank <= 3 Then
                                Dim strToEncrypted As String = ""
                                If strCacheSPID <> parentProjectCode Then
                                    strToEncrypted = parentProjectCode + "|" + "SP" + "|" + "NeedToCache" + "|" + securityRank.ToString()
                                Else
                                    strToEncrypted = parentProjectCode + "|" + "SP" + "|" + parentProjectCode + "|" + securityRank.ToString()
                                End If

                                Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                                ' Set a progress bar to pop-up when user clicks on this summary project
                                hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                                hyplnk.Text = parentProjectCode + " - Parent"
                                lbl.Text = parentProjectDesc
                            Else
                                hyplnk.Text = parentProjectCode + " - Parent"
                                lbl.Text = parentProjectDesc
                            End If
                    End Select  ' end slect ProjectType

                Case "N"


                    Select Case activityCode


                        Case "YYYYYYYYYYYYYYY"

                            e.Item.Style("background-color") = "#999999"
                            e.Item.Cells(0).Style.Add("text-align", "right")

                            Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkProject"), HyperLink)
                            Dim lbl As Label = CType(e.Item.FindControl("lblDescription"), Label)

                            hyplnk.Text = projectCode
                            lbl.Text = projectDesc

                            Dim lblProjectType As Label = CType(e.Item.FindControl("lblProjectType"), Label)
                            Dim lblOtherAttributes As Label = CType(e.Item.FindControl("lblOtherAttributes"), Label)
                            lblProjectType.Text = projectTypeDesc
                            lblOtherAttributes.Text = otherAttributes

                            If securityRank <= 4 Or securityRank = 7 Or securityRank = 9 Then
                                Dim strToEncrypted As String = projectCode + "|" + "CP"
                                Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                                ' Set a progress bar to pop-up when user clicks on this child project
                                hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                                hyplnk.Text = projectCode
                            End If

                            lbl = CType(e.Item.FindControl("lblReference"), Label)
                            lbl.Text = referenceNumber '+ " " + secondReferenceNumber

                        Case "ZZZZZZZZZZZZZZZ"

                            e.Item.Style("background-color") = "#ffffff"    ' Set background color to white
                            e.Item.Cells(0).Style.Add("text-align", "right")

                            Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkProject"), HyperLink)
                            Dim lbl As Label = CType(e.Item.FindControl("lblBalance"), Label)

                            hyplnk.Text = projectCode

                            'lbl.Text = strBalance 'added by david on 17 dec 2013
                            'e.Item.Cells(6).Style.Add("padding-right", "9px")


                            ' If security rank is <= 4 then we can show this user the balance of this child project and
                            ' provide a link to the child project details
                            If securityRank <= 4 Or securityRank = 7 Or securityRank = 9 Then
                                Dim strToEncrypted As String = projectCode + "|" + "CP"
                                Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                                ' Set a progress bar to pop-up when user clicks on this child project
                                hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                                'lbl.Text = strBalance 'add by david on 16 dec 2013
                                If securityRank <= 4 Then
                                    lbl.Text = strBalance
                                Else
                                    lbl.Text = "N/A"
                                    e.Item.Cells(6).Style.Add("padding-right", "9px")

                                End If

                            Else
                                lbl.Text = "N/A"    ' Can't see financial total
                                'lbl.Text = strBalance 'added by david on 17 dec 2013
                                e.Item.Cells(6).Style.Add("padding-right", "9px")

                            End If

                                lbl = CType(e.Item.FindControl("lblDescription"), Label)
                                'lbl.Text = "TOTAL"

                        Case Else ' activities

                            e.Item.Style("background-color") = "#ffffff"    ' Set background color to white

                            Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkActivity"), HyperLink)
                            Dim lbl As Label = CType(e.Item.FindControl("lblBalance"), Label)

                            ' This if statement handles the junk data from the database
                            ' If activity is a " ", then it is a blank activity
                            ' We assigned the hyperlink an "@" so we can click the hyperlink
                            ' otherwise the hyperlink will not show up
                            If activityCode = " " Then
                                hyplnk.Text = "N/A"
                            Else
                                hyplnk.Text = activityCode
                            End If

                            ' If security rank is <= 9 (change by David from 5 to 9 on 17 Dec 2013 then we can show this user the balance of this activity
                            If securityRank <= 5 Then
                                lbl.Text = strBalance

                            Else
                                lbl.Text = "N/A"
                                e.Item.Cells(6).Style.Add("padding-right", "9px")

                            End If

                            ' Provide link to activity details if security permits
                            If securityRank <> 99 Then

                                Dim strToEncrypted As String = activityCode + "|" + projectCode + "|" + "ACT"
                                Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)

                                ' Set a progress bar to pop-up when user clicks on this activity
                                hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                            End If

                            lbl = CType(e.Item.FindControl("lblDescription"), Label)
                            lbl.Text = activityDesc

                    End Select
                Case Else
                    e.Item.Style("background-color") = "#ffffff"    ' Set background color to white

                    Dim lbl As Label
                    lbl = CType(e.Item.FindControl("lblDescription"), Label)
                    lbl.Text = "&nbsp;"

            End Select
        End If

    End Sub


    ' Change the page index on the datagrid
    Private Sub dgProjects_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProjects.PageIndexChanged


        Dim dt As DataTable
        dgProjects.CurrentPageIndex = e.NewPageIndex

        If HeaderControl.GetProjectID.ToUpper.Trim = "" Or Session("isadmin") = "true" Then
            dt = CType(Session("OriginalProjectList"), DataTable)
        Else
            dt = CType(Session("ProjectListDataTable"), DataTable)
        End If

        If Not dt Is Nothing Then
            'changed by Jack on Aug 24, 2009 for paging project list issue
            Dim ProjectSearch As New ArrayList
            If Session("ProjectSearch") IsNot Nothing Then
                ProjectSearch = Session("ProjectSearch")
            End If

            If dt IsNot Nothing Then
                Dim dv As DataView = New DataView(dt)
                dv.RowFilter = "fld_project_desc like '%" + ProjectSearch(0) + "%'"
                If ProjectSearch(1) <> "ALL" Then
                    dv.RowFilter = dv.RowFilter + " and Project_Status_Desc = '" + ProjectSearch(1) + "'"
                End If
                If ProjectSearch(2) <> "" Then
                    dv.RowFilter = dv.RowFilter + " and Fac_ID = '" + ProjectSearch(2) + "'"
                End If
                If ProjectSearch(3) <> "" Then
                    dv.RowFilter = dv.RowFilter + " and Dept_ID = '" + ProjectSearch(3) + "'"
                End If
                If ProjectSearch(4) <> "" Then
                    'dv.RowFilter = dv.RowFilter + " and Project_Status_Desc = '" + ProjectSearch(1) + "')"
                End If

                If Session("isadmin") <> "true" Then
                    BinddgProjects(dv)
                Else 'changed by David Zeng on 18 Nov 2013 should update here as CurrentPageIndex is required.
                    Me.btnExcel.Visible = True
                    dgProjects.DataSource = dt
                    dgProjects.DataBind()
                End If

            End If
                'Dim dv As New DataView(dt)
                'BinddgProjects(dv)
                '=====================================================================

        End If

    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        If Not Session("PageStart") Is Nothing Then

            'Me.lblResponseTime.Text = String.Format("{0}.{1}", Date.Now.Subtract(CType(Session("PageStart"), Date)).Seconds, Date.Now.Subtract(CType(Session("PageStart"), Date)).Milliseconds)

            Session.Remove("PageStart")

        End If
        ' added by David on 17 Jan 2014
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If 'end addition

        MyBase.Render(writer)

    End Sub

    Private Sub BindGrid()
        Dim dt As DataTable


        If HeaderControl.GetProjectID.ToUpper.Trim = "" Or Session("isadmin") = "true" Then
            dt = CType(Session("OriginalProjectList"), DataTable)
        Else
            dt = CType(Session("ProjectListDataTable"), DataTable)
        End If

        If Not dt Is Nothing Then
            'changed by Jack on Aug 24, 2009 for paging project list issue
            Dim ProjectSearch As New ArrayList
            If Session("ProjectSearch") IsNot Nothing Then
                ProjectSearch = Session("ProjectSearch")
            End If

            If dt IsNot Nothing Then
                Dim dv As DataView = New DataView(dt)
                dv.RowFilter = "fld_project_desc like '%" + ProjectSearch(0) + "%'"
                If ProjectSearch(1) <> "ALL" Then
                    dv.RowFilter = dv.RowFilter + " and Project_Status_Desc = '" + ProjectSearch(1) + "'"
                End If
                If ProjectSearch(2) <> "" Then
                    dv.RowFilter = dv.RowFilter + " and Fac_ID = '" + ProjectSearch(2) + "'"
                End If
                If ProjectSearch(3) <> "" Then
                    dv.RowFilter = dv.RowFilter + " and Dept_ID = '" + ProjectSearch(3) + "'"
                End If
                If ProjectSearch(4) <> "" Then
                    'dv.RowFilter = dv.RowFilter + " and Project_Status_Desc = '" + ProjectSearch(1) + "')"
                End If
                BinddgProjects(dv)
            End If
        End If
    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=ProjectList.xls")
        HttpContext.Current.Response.ContentType = "application/vnd.ms-Excel"
        HttpContext.Current.Response.Charset = ""

        EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)

        Me.dgProjects.AllowPaging = False
        BindGrid()

        'Convert the controls to string literals
        Utils.ClearControls(dgProjects)
        dgProjects.RenderControl(oHtmlTextWriter)

        HttpContext.Current.Response.Write(oStringWriter.ToString())
        HttpContext.Current.Response.End()

        Me.dgProjects.AllowPaging = True
        BindGrid()
    End Sub

    Protected Sub lbSecurity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSecurity.Click
        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Admin/frm_Team_Security.aspx")
    End Sub

    Protected Sub likPaging_Click(sender As Object, e As EventArgs) Handles likPaging.Click
        If (dgProjects.AllowPaging) Then
            dgProjects.AllowPaging = False
            likPaging.Text = "On"
            BindGrid()
            Response.Cookies("dvPaging").Value = "False"
        Else
            dgProjects.AllowPaging = True
            likPaging.Text = "Off"
            BindGrid()
            Response.Cookies("dvPaging").Value = "True"
        End If
    End Sub
End Class
