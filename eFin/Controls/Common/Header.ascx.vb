Option Compare Text

Partial Class Controls_Common_Header
    Inherits System.Web.UI.UserControl
    Private _strStartDate As String
    Private _strEndDate As String
    Private _strProjectID As String
    Private _strBeginDate As String
    Private _strAsAtDate As String

    Public Event Search As EventHandler
    Public Event UpdateEmployeeDetail As EventHandler


#Region "Property Functions"
    Public ReadOnly Property GetStartDate() As String
        Get
            Me._strStartDate = Me.ddlStartYear.SelectedValue + Me.ddlStartMonth.SelectedValue
            Return Me._strStartDate
        End Get
    End Property

    Public ReadOnly Property GetEndDate() As String
        Get
            Me._strEndDate = Me.ddlEndYear.SelectedValue + Me.ddlEndMonth.SelectedValue
            Return Me._strEndDate
        End Get
    End Property

    Public ReadOnly Property GetProjectID() As String
        Get
            Me._strProjectID = Me.txtStartAtProject.Text
            Return Me._strProjectID
        End Get
    End Property
    Public WriteOnly Property SetProjectID() As String
        Set(ByVal value As String)
            Me.txtStartAtProject.Text = value
        End Set
    End Property

    Public ReadOnly Property GetBeginDate() As String
        Get
            Me._strBeginDate = Me.ddlStartYear.Items(0).Value + Me.ddlStartMonth.Items(3).Value
            Return Me._strBeginDate
        End Get
    End Property

    Public ReadOnly Property GetAsAtDate() As String
        Get
            Me._strAsAtDate = BLL.Header.GetAsAtYear.ToString + BLL.Header.GetAsAtMonth.ToString.PadLeft(2, "0")
            Return Me._strAsAtDate
        End Get
    End Property
    'Added by Jack on May 11 for searching by PH
    Public Property ProjectSearch() As ArrayList
#If False Then
        Get
            Dim arr As New ArrayList
            arr.Add(ddlPI.SelectedValue.Replace("'", "''"))
            arr.Add(ddlStatus.SelectedValue)
            arr.Add(ddlFaculty.SelectedValue)
            arr.Add(ddlDepartment.SelectedValue)
            arr.Add(txtKeyWord.Text)
            Return arr
        End Get
        Set(ByVal value As ArrayList)
            ddlPI.ClearSelection()
            If ddlPI.Items.FindItemByValue(value(0).Replace("''", "'")) IsNot Nothing Then
                ddlPI.Items.FindItemByValue(value(0).Replace("''", "'")).Selected = True
            End If

            ddlStatus.ClearSelection()
            ddlStatus.Items.FindByValue(value(1)).Selected = True

            ddlFaculty.ClearSelection()
            ddlFaculty.Items.FindByValue(value(2)).Selected = True

            ddlDepartment.ClearSelection()
            ddlDepartment.Items.FindByValue(value(3)).Selected = True

            txtKeyWord.Text = value(4)

        End Set
#Else
        Get
            Dim arr As New ArrayList
            arr.Add(ddlPI2.SelectedValue.Replace("'", "''"))
            arr.Add(ddlStatus.SelectedValue)
            arr.Add(ddlFaculty.SelectedValue)
            arr.Add(ddlDepartment.SelectedValue)
            arr.Add(txtKeyWord.Text)
            Return arr
        End Get
        Set(ByVal value As ArrayList)
            ddlPI2.ClearSelection()
            If ddlPI2.Items.FindByValue(value(0).Replace("''", "'")) IsNot Nothing Then
                ddlPI2.Items.FindByValue(value(0).Replace("''", "'")).Selected = True
            End If

            ddlStatus.ClearSelection()
            ddlStatus.Items.FindByValue(value(1)).Selected = True

            ddlFaculty.ClearSelection()
            ddlFaculty.Items.FindByValue(value(2)).Selected = True

            ddlDepartment.ClearSelection()
            ddlDepartment.Items.FindByValue(value(3)).Selected = True

            txtKeyWord.Text = value(4)

        End Set
#End If
    End Property

#If False Then

    Public ReadOnly Property GetPHControl() As Telerik.Web.UI.RadComboBox
        Get
            Return Me.ddlPI
        End Get
    End Property
#Else
    Public ReadOnly Property GetPHControl2() As System.Web.UI.WebControls.DropDownList
        Get
            Return Me.ddlPI2
        End Get
    End Property

#End If

    Public ReadOnly Property GetPHControlProjectYear() As System.Web.UI.WebControls.DropDownList
        Get
            Return Me.ddlProjectYear
        End Get
    End Property
    '-----------------------------------------
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Or Session("Period") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        If Not IsPostBack Then
            ibHead.Visible = True

            If Session("DepartTable") Is Nothing Then
                Session("DepartTable") = FacultyDeptAuthorization.DepartmentTable()
            End If
            If Session("FacultyTable") Is Nothing Then
                Session("FacultyTable") = FacultyDeptAuthorization.FacultyTable()
            End If

            ddlFaculty.DataSource = Session("FacultyTable")
            ddlFaculty.DataTextField = "Dept_L5_Desc"
            ddlFaculty.DataValueField = "Dept_L5_Code"
            ddlFaculty.DataBind()
            Dim fstItem As ListItem = New ListItem("--- ALL ---", "")
            ddlFaculty.Items.Insert(0, fstItem)
            ddlFaculty.SelectedIndex = 0

            ddlDepartment.DataSource = Session("DepartTable")
            ddlDepartment.DataTextField = "Dept_Desc"
            ddlDepartment.DataValueField = "Dept_ID"
            ddlDepartment.DataBind()
            fstItem = New ListItem("--- ALL ---", "")
            ddlDepartment.Items.Insert(0, fstItem)
            ddlDepartment.SelectedIndex = 0

            If Not Session("ProjectPeriod") Is Nothing Then
                InitializeDate()    ' Initialized the header date and set date to current fiscal year
            End If

            If Session("ProjectSearch") IsNot Nothing Then
                ProjectSearch = Session("ProjectSearch")
            End If

            ' Set the start and end date of the control if coming from another page.
            Dim strStartDate As String = BLL.Period.GetPeriodFromTheSession.StartDate
            Dim strEndDate As String = BLL.Period.GetPeriodFromTheSession.EndDate

            ' If coming from another page, set the header to the start date that was previously chosen on the previous page
            If Not strStartDate Is Nothing Then
                Me.ddlStartYear.SelectedValue = strStartDate.Substring(0, 4)
                Me.ddlStartMonth.SelectedValue = strStartDate.Substring(4)
            End If

            ' If coming from another page, set the header to the end date that was previously chosen on the previous page
            If Not strEndDate Is Nothing Then
                Me.ddlEndYear.SelectedValue = strEndDate.Substring(0, 4)
                Me.ddlEndMonth.SelectedValue = strEndDate.Substring(4)
            End If

            ' Display the period the user is currently retrieving information for
            Dim period As BLL.Period = BLL.Period.GetPeriodFromTheSession()
            If Not period Is Nothing Then
                Me.lblPeriod.Text = period.StartDate.Insert(4, "/") + " - " + period.EndDate.Insert(4, "/")
            End If

            Dim category As String = CType(Session("Category"), String)

            ' Set header properties based on category
            Select Case category
                Case "SP"   ' Summary Project
                    Me.panHeaderDetails.Visible = True
                    Me.panDisplayOptions.Visible = True
                    Me.lblHeaderTitle.Text = "SUMMARY PROJECT HEADER"
                    Me.lblProjectSummaryTitle.Visible = True
                    Me.lblProjectTitle.Visible = False
                    Me.lblActivityTitle.Visible = False
                    Me.lblProject.Text = CType(Session("SPID"), String)

                Case "CP"   ' Child Project
                    Dim str As String = Request.QueryString("category")
                    If str = "PROJDETAILALLACCT" Then   ' In project summary page
                        Me.lblHeaderTitle.Text = "PROJECT SUMMARY BY ACCOUNT"
                        ' set visible
                        Me.ibHead.Visible = False
                        Me.panAdvanceSearch.Visible = False
                        Me.panGoReset.Visible = False
                        Me.lnkBtnGo.Visible = True
                        Me.lnkbtnReset.Visible = True

                    ElseIf str = "PROJECTEMPLOYEE" Then
                        Me.lblHeaderTitle.Text = "PROJECT EMPLOYEES"
                    ElseIf str = "PROJECTEMPLOYEEDETAIL" Then
                        Me.lblHeaderTitle.Text = "PROJECT EMPLOYEES DETAIL"
                    Else
                        Me.lblHeaderTitle.Text = "PROJECT HEADER"
                    End If

                    Me.panHeaderDetails.Visible = True
                    Me.panDisplayOptions.Visible = True
                    Me.lblProjectSummaryTitle.Visible = False
                    Me.lblProjectTitle.Visible = True
                    Me.lblActivityTitle.Visible = False
                    Me.lblProject.Text = CType(Session("CPID"), String)

                Case "ACT"  ' Activity
                    Dim str As String = Request.QueryString("category")
                    If str = "ACTDETAILALLACCT" Then    ' In activity summary page
                        Me.lblHeaderTitle.Text = "ACTIVITY SUMMARY BY ACCOUNT"
                        ' set visible
                        Me.ibHead.Visible = False
                        Me.panAdvanceSearch.Visible = False
                        Me.panGoReset.Visible = False
                        Me.lnkBtnGo.Visible = True
                        Me.lnkbtnReset.Visible = True
                    Else
                        Me.lblHeaderTitle.Text = "ACTIVITY HEADER"
                    End If

                    Me.panHeaderDetails.Visible = True
                    Me.panDisplayOptions.Visible = True
                    Me.lblProjectSummaryTitle.Visible = False
                    Me.lblProjectTitle.Visible = True
                    Me.lblActivityTitle.Visible = True
                    Me.lblProject.Text = CType(Session("CPID"), String)

                    If CType(Session("ACTID"), String) = " " Then
                        Me.lblActivity.Text = "N/A"
                    Else
                        Me.lblActivity.Text = CType(Session("ACTID"), String)
                    End If

                Case ""     ' Project list page
                    Me.panDisplayOptions.Visible = False
                    Me.panHeaderDetails.Visible = False
                    Me.panAdvanceSearch.Visible = True
                    Me.lblProject.Text = "N/A"
                    Me.lblActivity.Text = "N/A"
                    Me.lblHeaderTitle.Text = "Home Page"
                    If Session("isadmin") = "true" Then
                        Me.lblStartAtProject.Text = "Project Number Contains:"
                        Me.lblmessage1.Visible = True
                    Else
                        'added by jack on Apr 1, 2009 for showing PI list
                        Me.panPI.Visible = True
                        '------------------------------------------------
                    End If
                    ibHead.Visible = True

            End Select
        End If

        If lnkBtnGo.Visible = True Then
            Panel1.DefaultButton = lnkBtnGo.ID
            panAdvanceSearch.DefaultButton = lnkBtnGo.ID
        Else
            Panel1.DefaultButton = lnkBtnGo2.ID
            panAdvanceSearch.DefaultButton = lnkBtnGo2.ID
        End If

        Dim controlCausedPostBack As String = IIf(Not Request.Form("__EVENTTARGET") Is Nothing, Request.Form("__EVENTTARGET"), "")
        If controlCausedPostBack.Contains("lnkBtnFullview") Then
            SetToProjectPeriod()
        End If
        If controlCausedPostBack.Contains("ddlProjectYear") Then
            SetPojectPeriod()
        End If

    End Sub

    Private Sub Page_PreRender(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        ReInitializeDate()
    End Sub

    ' Initializes the control with dates from 1993 to current date
    Public Sub InitializeDate()

        Dim nCurrYear As Integer = BLL.Header.GetAsAtYear()
        Dim nCurrMonth As Integer = BLL.Header.GetAsAtMonth()
        Dim i As Integer

        Me.ddlEndYear.Items.Clear()
        Me.ddlStartYear.Items.Clear()

        ' Dynamically add the years for the start and end year drop down list
        For i = 1993 To nCurrYear
            Me.ddlStartYear.Items.Add(New ListItem(i.ToString, i.ToString))
            Me.ddlEndYear.Items.Add(New ListItem(i.ToString, i.ToString))
        Next

        Me.ddlEndYear.SelectedValue = BLL.Header.GetAsAtYear()  ' Set end year to current year

        ' Each fiscal year starts in April
        ' If the current month is before April, the beginning fiscal year would be the year before
        ' If the current month April or after, the beginning fiscal year would be the current year
        If nCurrMonth <= 4 Then
            Me.ddlStartYear.SelectedValue = (nCurrYear - 1).ToString
        Else
            Me.ddlStartYear.SelectedValue = nCurrYear.ToString
        End If

        'If nCurrYear = 2005 Then
        '    Me.ddlEndMonth.SelectedIndex = 9

        'Else
        Me.ddlEndMonth.SelectedIndex = BLL.Header.GetAsAtMonth() - 1

        'End If


    End Sub

    ' ReInitializes the control with dates from 1993 to project expiry date
    Public Sub ReInitializeDate()

        If Not IsPostBack And Not Session("ProjectPeriod") Is Nothing Then
            Dim strExpiryDate As String = BLL.ProjectPeriod.GetBudgetLastDate(GetAsAtDate(), BLL.Header.GetMaxExtendYear())

            Dim nCurrYear As Integer = CType(strExpiryDate.Substring(0, 4), Integer)
            Dim nCurrMonth As Integer = CType(strExpiryDate.Substring(4, 2), Integer)

            Dim i As Integer

            Me.ddlEndYear.Items.Clear()
            Me.ddlStartYear.Items.Clear()

            ' Dynamically add the years for the start and end year drop down list
            For i = 1993 To nCurrYear
                Me.ddlStartYear.Items.Add(New ListItem(i.ToString, i.ToString))
                Me.ddlEndYear.Items.Add(New ListItem(i.ToString, i.ToString))
            Next

            ' Set the start and end date of the control if coming from another page.
            Dim strStartDate As String = BLL.Period.GetPeriodFromTheSession.StartDate
            Dim strEndDate As String = BLL.Period.GetPeriodFromTheSession.EndDate

            ' If coming from another page, set the header to the start date that was previously chosen on the previous page
            If Not strStartDate Is Nothing Then
                Me.ddlStartYear.SelectedValue = strStartDate.Substring(0, 4)
                Me.ddlStartMonth.SelectedValue = strStartDate.Substring(4)
            End If

            ' If coming from another page, set the header to the end date that was previously chosen on the previous page
            If Not strEndDate Is Nothing Then
                Me.ddlEndYear.SelectedValue = strEndDate.Substring(0, 4)
                Me.ddlEndMonth.SelectedValue = strEndDate.Substring(4)
            End If

        End If

    End Sub

    Private Sub SetToProjectPeriod()

        ' Set the start and end date of the control if coming from another page.
        Dim strStartDate As String = BLL.ProjectPeriod.GetProjectPeriodFromTheSession.StartDate

        ' If coming from another page, set the header to the start date that was previously chosen on the previous page
        If Not strStartDate Is Nothing Then
            ddlStartYear.SelectedIndex = ddlStartYear.Items.IndexOf(ddlStartYear.Items.FindByValue(strStartDate.Substring(0, 4)))
            ddlStartMonth.SelectedIndex = ddlStartMonth.Items.IndexOf(ddlStartMonth.Items.FindByValue(strStartDate.Substring(4, 2)))
        End If

        ' If coming from another page, set the header to the end date that was previously chosen on the previous page
        Dim strExpiryDate As String = BLL.ProjectPeriod.GetBudgetLastDate(GetAsAtDate(), BLL.Header.GetMaxExtendYear())

        Dim nCurrYear As Integer = CType(strExpiryDate.Substring(0, 4), Integer)
        Dim nCurrMonth As Integer = CType(strExpiryDate.Substring(4, 2), Integer)

        Me.ddlEndYear.SelectedIndex = ddlEndYear.Items.IndexOf(ddlEndYear.Items.FindByValue(nCurrYear.ToString()))
        Me.ddlEndMonth.SelectedIndex = ddlEndMonth.Items.IndexOf(ddlEndMonth.Items.FindByValue(nCurrMonth.ToString().PadLeft(2, "0")))

    End Sub



    ' Set selected period to session and does error checking on the dates
    Public Sub lnkBtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBtnRefresh.Click

        Dim nStartDate As Integer = Integer.Parse(Me.ddlStartYear.SelectedValue + Me.ddlStartMonth.SelectedValue)
        Dim nEndDate As Integer = Integer.Parse(Me.ddlEndYear.SelectedValue + Me.ddlEndMonth.SelectedValue)

        If nEndDate < nStartDate Then
            ' Error end date cannot be smaller than start date
            Me.lblError.Text = "End date cannot be before start date, please reselect date."

            Return
        End If

        'If nStartDate > Integer.Parse(Me.GetAsAtDate) Then
        '    Me.lblError.Text = "Start date cannont be after current date, please reselect date."

        '    Return
        'End If

        'If nEndDate > Integer.Parse(Me.GetAsAtDate) Then '+ nExtendYear * 100 Then
        '    Me.lblError.Text = "eFin currently has information up until " + BLL.Header.GetAsAtDate.ToLongDateString + ", please reselect date."

        'Return
        'End If

        Me.lblError.Text = ""

        Dim period As BLL.Period = New BLL.Period(nStartDate.ToString.Trim, nEndDate.ToString.Trim)

        ' Display current fiscal period on the header control
        If Not period Is Nothing Then
            Me.lblPeriod.Text = period.StartDate.Insert(4, "/") + " - " + period.EndDate.Insert(4, "/")
        End If

        ' Add period to session
        BLL.Period.AddPeriodToTheSession(period)

        'Reset Current Index of that dgProject to be 0
        Dim dgrd As DataGrid = CType(Me.Parent.FindControl("dgProjects"), DataGrid)

        If Not dgrd Is Nothing Then
            dgrd.CurrentPageIndex = 0
        End If

        'Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Request.Url.ToString)

    End Sub

    Private Sub lnkBtnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBtnGo.Click
        'Me._strProjectID = Me.txtStartAtProject.Text.Trim
        RaiseEvent Search(Me, e)
        'Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Request.Url.ToString)
    End Sub

    Private Sub lnkbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnReset.Click
        Me.txtStartAtProject.Text = ""
        Me._strProjectID = ""

        Session("StartAtProject") = ""

        ddlStatus.ClearSelection()
        ddlStatus.SelectedIndex = 0

        ddlDepartment.ClearSelection()
        ddlDepartment.SelectedIndex = 0

        ddlFaculty.ClearSelection()
        ddlFaculty.SelectedIndex = 0

        If ddlPI2.Visible = True Or ddlPI2.Items.Count > 0 Then
            ddlPI2.SelectedIndex = 0
        End If
        txtKeyWord.Text = ""

        Session("ProjectSearch") = ProjectSearch
        Response.Redirect(Request.Url.ToString)

    End Sub


    Private Sub lnkBtnGo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBtnGo2.Click
        'Me._strProjectID = Me.txtStartAtProject.Text.Trim
        'lnkBtnGo_Click(Me, e)
        RaiseEvent Search(Me, e)
        'Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Request.Url.ToString)

    End Sub


    Private Sub lnkbtnReset2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnReset2.Click
        lnkbtnReset_Click(Me, e)
    End Sub

#If False Then

    'Protected Sub ddlPI_ItemsRequested(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles ddlPI.ItemsRequested
    '    Dim dt As DataTable
    '    Dim arrParams As New ArrayList

    '    arrParams.Add(CType(Session("UCID"), String))
    '    arrParams.Add(e.Text)
    '    dt = BLL.ProjectList.getProjectList("SELECT", "dbo.eFinsp_LoadPrejoinedProjectHolderByName", arrParams)

    '    If Not dt Is Nothing Then
    '        ddlPI.DataSource = dt
    '        ddlPI.DataValueField = "ProjectHolder"
    '        ddlPI.DataTextField = "ProjectHolder"
    '        ddlPI.DataBind()
    '    End If
    'End Sub
#Else
    Protected Sub LoadDdlData()
        Dim dt As DataTable
        Dim arrParams As New ArrayList

        arrParams.Add(CType(Session("UCID"), String))
        arrParams.Add("%")
        dt = BLL.ProjectList.getProjectList("SELECT", "dbo.eFinsp_LoadPrejoinedProjectHolderByName", arrParams)

        If Not dt Is Nothing Then
            ddlPI2.DataSource = dt
            ddlPI2.DataValueField = "ProjectHolder"
            ddlPI2.DataTextField = "ProjectHolder"
            ddlPI2.DataBind()
        End If
    End Sub

#End If

    Public Sub ibHead_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibHead.Click
        If ibHead.ImageUrl.Contains("expand") Then
            ibHead.ImageUrl = "~/Images/collapse.gif"
            ibHead.ToolTip = "Hide Advanced Search Options"
            panAdvanceSearch.Visible = True
        Else
            ibHead.ImageUrl = "~/Images/expand.gif"
            ibHead.ToolTip = "Show Advanced Search Options"
            panAdvanceSearch.Visible = False

            ddlStatus.ClearSelection()
            ddlStatus.SelectedIndex = 0

            ddlDepartment.ClearSelection()
            ddlDepartment.SelectedIndex = 0

            ddlFaculty.ClearSelection()
            ddlFaculty.SelectedIndex = 0

            txtKeyWord.Text = ""

            'Session("ProjectSearch") = ProjectSearch
        End If

    End Sub

    Protected Sub lnkBtnFullview_Click(sender As Object, e As EventArgs) Handles lnkBtnFullview.Click
        RaiseEvent UpdateEmployeeDetail(Me, e)
        lnkBtnRefresh_Click(Me, e)
    End Sub

    Protected Sub ddlPI2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPI2.SelectedIndexChanged
        lnkBtnGo_Click(Me, e)
    End Sub

    Protected Sub ddlProjectYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProjectYear.SelectedIndexChanged
        RaiseEvent UpdateEmployeeDetail(Me, e)
        lnkBtnRefresh_Click(Me, e)
    End Sub

    Protected Sub SetPojectPeriod()

        If Not (ddlProjectYear.SelectedValue = "") Then

            Me.ddlStartYear.SelectedValue = ddlProjectYear.SelectedValue.Substring(0, 4)
            Me.ddlStartMonth.SelectedValue = ddlProjectYear.SelectedValue.Substring(5, 2)

            Me.ddlEndYear.SelectedValue = ddlProjectYear.SelectedValue.Substring(10, 4)
            Me.ddlEndMonth.SelectedValue = ddlProjectYear.SelectedValue.Substring(15, 2)
        End If
    End Sub
End Class
