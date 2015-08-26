Option Compare Text

Partial Class Controls_Common_AccountsHeader
    Inherits System.Web.UI.UserControl
    Private _strStartDate As String
    Private _strEndDate As String
    Private _strProjectID As String
    Private _strBeginDate As String
    Private _strAsAtDate As String

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
            Me._strProjectID = Me.txtStartAtAccount.Text
            Return Me._strProjectID
        End Get
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
#End Region
    Public Event UpdateAcoountDetail As EventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Or Session("Period") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        If Not IsPostBack Then
            InitializeDate()    ' Initialized the header date and set date to current fiscal year

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
            Dim period As BLL.Period = CType(BLL.Period.GetPeriodFromTheSession(), BLL.Period)
            If Not period Is Nothing Then
                Me.lblPeriod.Text = period.StartDate.Insert(4, "/") + " - " + period.EndDate.Insert(4, "/")
            End If

            Dim category As String = CType(Session("Category"), String)

            ' Set header properties based on category
            Select Case category
                Case "CP"   ' Child Project
                    Dim str As String = Request.QueryString("category")
                    If str = "PROJECTSINGLEACCOUNT" Then   ' In project summary page
                        Me.lblHeaderTitle.Text = "PROJECT DETAIL FOR A SINGLE ACCOUNT"
                    Else
                        Me.lblHeaderTitle.Text = "PROJECT DETAIL FOR ALL ACCOUNTS"
                    End If

                    Me.panHeaderDetails.Visible = True
                    Me.lblProjectTitle.Visible = True
                    Me.lblActivityTitle.Visible = False
                    Me.lblProject.Text = CType(Session("CPID"), String)

                Case "ACT"  ' Activity
                    Dim str As String = Request.QueryString("category")
                    If str = "ACTIVITYSINGLEACCOUNT" Then    ' In activity summary page
                        Me.lblHeaderTitle.Text = "ACTIVITY DETAIL FOR A SINGLE ACCOUNT"
                    Else
                        Me.lblHeaderTitle.Text = "ACTIVITY DETAIL FOR ALL ACCOUNTS"
                    End If

                    Me.panHeaderDetails.Visible = True
                    Me.lblProjectTitle.Visible = True
                    Me.lblActivityTitle.Visible = True
                    Me.lblProject.Text = CType(Session("CPID"), String)

                    If CType(Session("ACTID"), String) = " " Then
                        Me.lblActivity.Text = "N/A"
                    Else
                        Me.lblActivity.Text = CType(Session("ACTID"), String)
                    End If

            End Select

        End If
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


    ' Set selected period to session and does error checking on the dates
    Public Sub lnkRefresh(sender As Object, e As EventArgs)

        Dim nStartDate As Integer = Integer.Parse(Me.ddlStartYear.SelectedValue + Me.ddlStartMonth.SelectedValue)
        Dim nEndDate As Integer = Integer.Parse(Me.ddlEndYear.SelectedValue + Me.ddlEndMonth.SelectedValue)

        If nEndDate < nStartDate Then
            ' Error end date cannot be smaller than start date
            Me.lblError.Text = "End date cannot be before start date, please reselect date."

            Return
        End If

        If nStartDate > Integer.Parse(Me.GetAsAtDate) Then
            Me.lblError.Text = "Start date cannont be after current date, please reselect date."

            Return
        End If

        If nEndDate > Integer.Parse(Me.GetAsAtDate) Then
            Me.lblError.Text = "eFin currently has information up until " + BLL.Header.GetAsAtDate.ToLongDateString + ", please reselect date."

            Return
        End If

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
        'Me._strProjectID = Me.txtStartAtAccount.Text.Trim

        'Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Request.Url.ToString)

    End Sub

    Private Sub SetToProjectPeriod()
        ' Set the start and end date of the control if coming from another page.
        Dim strStartDate As String = BLL.ProjectPeriod.GetProjectPeriodFromTheSession.StartDate
        'Dim strEndDate As String = BLL.ProjectPeriod.GetProjectPeriodFromTheSession.EndDate

        ' If coming from another page, set the header to the start date that was previously chosen on the previous page
        If Not strStartDate Is Nothing Then
            ddlStartYear.SelectedIndex = ddlStartYear.Items.IndexOf(ddlStartYear.Items.FindByValue(strStartDate.Substring(0, 4)))
            ddlStartMonth.SelectedIndex = ddlStartMonth.Items.IndexOf(ddlStartMonth.Items.FindByValue(strStartDate.Substring(4, 2)))
        End If

        ' If coming from another page, set the header to the end date that was previously chosen on the previous page
        Dim nCurrYear As Integer = BLL.Header.GetAsAtYear()
        Dim nCurrMonth As Integer = BLL.Header.GetAsAtMonth()

        Me.ddlEndYear.SelectedIndex = ddlEndYear.Items.IndexOf(ddlEndYear.Items.FindByValue(nCurrYear))
        Me.ddlEndMonth.SelectedIndex = ddlEndMonth.Items.IndexOf(ddlEndMonth.Items.FindByValue(nCurrMonth.ToString().PadLeft(2, "0")))
    End Sub

    'Private Sub ddlStartYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStartYear.SelectedIndexChanged

    '    If Me.ddlStartYear.SelectedValue = 2005 Then
    '        Dim hTable As New SortedList

    '        hTable.Add("01", "January")
    '        hTable.Add("02", "February")
    '        hTable.Add("03", "March")
    '        hTable.Add("04", "April")
    '        hTable.Add("05", "May")
    '        hTable.Add("06", "June")
    '        hTable.Add("07", "July")
    '        hTable.Add("08", "August")
    '        hTable.Add("09", "September")
    '        hTable.Add("10", "October")
    '        hTable.Add("11", "November")

    '        Me.ddlStartMonth.DataSource = hTable
    '        Me.ddlStartMonth.DataValueField = "Key"
    '        Me.ddlStartMonth.DataTextField = "Value"
    '        Me.ddlStartMonth.DataBind()

    '    Else

    '        Dim hTable As New SortedList

    '        hTable.Add("01", "January")
    '        hTable.Add("02", "February")
    '        hTable.Add("03", "March")
    '        hTable.Add("04", "April")
    '        hTable.Add("05", "May")
    '        hTable.Add("06", "June")
    '        hTable.Add("07", "July")
    '        hTable.Add("08", "August")
    '        hTable.Add("09", "September")
    '        hTable.Add("10", "October")
    '        hTable.Add("11", "November")
    '        hTable.Add("12", "December")

    '        Me.ddlStartMonth.DataSource = hTable
    '        Me.ddlStartMonth.DataValueField = "Key"
    '        Me.ddlStartMonth.DataTextField = "Value"
    '        Me.ddlStartMonth.DataBind()

    '    End If

    '    Dim strStartDate As String = BLL.Period.GetPeriodFromTheSession.StartDate

    '    If Not strStartDate Is Nothing Then
    '        Me.ddlStartMonth.SelectedValue = strStartDate.Substring(4)
    '    End If

    'End Sub

    'Private Sub ddlEndYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEndYear.SelectedIndexChanged

    '    If Me.ddlEndYear.SelectedValue = 2005 Then
    '        Dim hTable As New SortedList

    '        hTable.Add("01", "January")
    '        hTable.Add("02", "February")
    '        hTable.Add("03", "March")
    '        hTable.Add("04", "April")
    '        hTable.Add("05", "May")
    '        hTable.Add("06", "June")
    '        hTable.Add("07", "July")
    '        hTable.Add("08", "August")
    '        hTable.Add("09", "September")
    '        hTable.Add("10", "October")
    '        hTable.Add("11", "November")

    '        Me.ddlEndMonth.DataSource = hTable
    '        Me.ddlEndMonth.DataValueField = "Key"
    '        Me.ddlEndMonth.DataTextField = "Value"
    '        Me.ddlEndMonth.DataBind()

    '    Else

    '        Dim hTable As New SortedList

    '        hTable.Add("01", "January")
    '        hTable.Add("02", "February")
    '        hTable.Add("03", "March")
    '        hTable.Add("04", "April")
    '        hTable.Add("05", "May")
    '        hTable.Add("06", "June")
    '        hTable.Add("07", "July")
    '        hTable.Add("08", "August")
    '        hTable.Add("09", "September")
    '        hTable.Add("10", "October")
    '        hTable.Add("11", "November")
    '        hTable.Add("12", "December")

    '        Me.ddlEndMonth.DataSource = hTable
    '        Me.ddlEndMonth.DataValueField = "Key"
    '        Me.ddlEndMonth.DataTextField = "Value"
    '        Me.ddlEndMonth.DataBind()

    '    End If

    '    Dim strEndDate As String = BLL.Period.GetPeriodFromTheSession.EndDate

    '    If Not strEndDate Is Nothing Then
    '        Me.ddlEndMonth.SelectedValue = strEndDate.Substring(4)
    '    End If
    'End Sub

    Protected Sub lnkBtnFullview_Click(sender As Object, e As EventArgs) Handles lnkBtnFullview.Click
        SetToProjectPeriod()
        RaiseEvent UpdateAcoountDetail(Me, e)
        lnkRefresh(Me, EventArgs.Empty)
    End Sub
End Class
