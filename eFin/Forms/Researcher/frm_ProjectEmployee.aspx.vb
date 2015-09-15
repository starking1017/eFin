Option Compare Text

Partial Class Forms_Researcher_frm_ProjectEmployee
    Inherits System.Web.UI.Page
    Private HardKey As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, HeaderControl.UpdateEmployeeDetail

        If Session("UCID") Is Nothing Then
            Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            CType(Page.Master, MasterPage).PageIdentity = "Enterprise Reporting<br/>eFIN - 07"
        End If

        Session("PageStart") = Date.Now

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        HardKey = CType(Session("RandomKey"), String)

        Dim startDate As String
        Dim endDate As String
        Dim bLoadCache As Boolean = False

        If Not IsPostBack Then
            ' Retrieve period from session since first page load
            startDate = BLL.Period.GetPeriodFromTheSession.StartDate
            endDate = BLL.Period.GetPeriodFromTheSession.EndDate

            EnableStartAtEmployee(False)

            ' Add flag for cache
            Dim strOldProjectID As String = CType(Session("OldEmployeeProjectID"), String)
            If strOldProjectID = CType(Session("CPID"), String) Then
                If CType(Session("EmployeeStartDate"), String) = startDate AndAlso _
                    CType(Session("EmployeeEndDate"), String) = endDate Then
                    bLoadCache = True
                End If
            Else
                strOldProjectID = CType(Session("CPID"), String)
                Session("OldEmployeeProjectID") = strOldProjectID
            End If
        Else
            ' Retrieve period from the header control
            startDate = HeaderControl.GetStartDate
            endDate = HeaderControl.GetEndDate

            ' First time page load
            ' If same as viewstate -> start date and end date never changed
            ' Return
            If CType(ViewState("StartDate"), String) = startDate AndAlso _
               CType(ViewState("EndDate"), String) = endDate Then
                Return

            End If
        End If

        LoadEmployees(startDate, endDate, bLoadCache)    ' Get activity summary for the account

        ' Store new start and end date to viewstate
        ViewState("StartDate") = startDate
        ViewState("EndDate") = endDate

        Session("EmployeeStartDate") = startDate
        Session("EmployeeEndDate") = endDate

    End Sub


    ' Enables the start at account feature
    Private Sub EnableStartAtEmployee(ByVal bEnableStart As Boolean)
        Dim pan As Panel = CType(HeaderControl.FindControl("panStartAt"), Panel)
        pan.Visible = bEnableStart

        Dim lbl As Label = CType(HeaderControl.FindControl("lblStartAtProject"), Label)

        lbl.Visible = bEnableStart
        lbl.Text = "Start At UCID:"
    End Sub


    Private Sub LoadEmployees(ByRef startDate As String, ByRef endDate As String, ByRef bLoadCache As Boolean)

        If dgrdEmployees.Attributes("SortExpression") Is Nothing Then
            dgrdEmployees.Attributes("SortExpression") = "Employee_Name"
            dgrdEmployees.Attributes("SortDirection") = "ASC"
        End If

        If bLoadCache And Not Session("dtEmployee") Is Nothing Then
            BindData(CType(Session("dtEmployee"), DataTable))
        Else
            Dim strUCID As String = CType(Session("UCID"), String)
            Dim strCPID As String = CType(Session("CPID"), String)
            Dim dt As DataTable
            Dim arrParams As New ArrayList

            arrParams.Add(strUCID)
            arrParams.Add(strCPID)
            arrParams.Add(startDate)
            arrParams.Add(endDate)
            arrParams.Add(HeaderControl.GetProjectID)

            dt = BLL.ProjectEmployee.LoadProjectEmployee("SELECT", "dbo.eFinsp_LoadProjectEmployee_1", arrParams)

            If Not dt Is Nothing Then
                Session("dtEmployee") = dt
                Me.dgrdEmployees.CurrentPageIndex = 0
                BindData(dt)
            End If
        End If

    End Sub


    Private Sub BindData(ByRef dt As DataTable)

        Dim dv As DataView = New DataView()
        Dim tb2 As DataTable = New DataTable()
        ' Copy Total row
        tb2 = dt.Clone()
        tb2.ImportRow(dt.Rows(dt.Rows.Count() - 1))

        ' Remove Total and change to view for sort
        dt.Rows.RemoveAt(dt.Rows.Count() - 1)
        dv = dt.DefaultView
        Dim SortExpression As String = dgrdEmployees.Attributes("SortExpression")
        Dim SortDirection As String = dgrdEmployees.Attributes("SortDirection")
        dv.Sort = SortExpression + " " + SortDirection

        ' Create new table add sorted table and Total row
        Dim myNewTable As DataTable = dv.ToTable()
        myNewTable.ImportRow(tb2.Rows(0))

        ' Add back Total row
        dt.ImportRow(tb2.Rows(0))

        ' Bind data to gridview
        Me.dgrdEmployees.DataSource = myNewTable
        Me.dgrdEmployees.DataBind()

    End Sub

    Private Sub dgrdEmployees_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrdEmployees.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim strUCID As String = CType(e.Item.DataItem("EMPLID"), String)
            Dim employeeName As String = CType(e.Item.DataItem("Employee_Name"), String)
            Dim salary As Decimal = CType(e.Item.DataItem("Salary"), Decimal)
            Dim benefit As Decimal = CType(e.Item.DataItem("Benefit"), Decimal)
            Dim salben As Decimal = CType(e.Item.DataItem("Salben"), Decimal)
            Dim encumbrance As Decimal = CType(e.Item.DataItem("Encumbrance"), Decimal)
            Dim actencum As Decimal = CType(e.Item.DataItem("ActEncum"), Decimal)

            Dim strSalary As String
            Dim strBenefit As String
            Dim strSalBen As String
            Dim strEncumbrance As String
            Dim strActEncum As String

            strSalary = String.Format("{0:n}", salary)
            strBenefit = String.Format("{0:n}", benefit)
            strSalBen = String.Format("{0:n}", salben)
            strEncumbrance = String.Format("{0:n}", encumbrance)
            strActEncum = String.Format("{0:n}", actencum)

            If strUCID = "TOTAL" Then
                e.Item.Style("background-color") = "#999999"
                e.Item.Style("font-weight") = "bold"
            End If

            If Decimal.Parse(strSalary) < 0 Then
                strSalary = "(" + strSalary.Substring(1) + ")"
            Else
                e.Item.Cells(4).Style.Add("padding-right", "9px")
            End If

            If Decimal.Parse(strBenefit) < 0 Then
                strBenefit = "(" + strBenefit.Substring(1) + ")"
            Else
                e.Item.Cells(5).Style.Add("padding-right", "9px")
            End If

            If Decimal.Parse(strSalBen) < 0 Then
                strSalBen = "(" + strSalBen.Substring(1) + ")"
            Else
                e.Item.Cells(6).Style.Add("padding-right", "9px")
            End If

            If Decimal.Parse(strEncumbrance) < 0 Then
                strEncumbrance = "(" + strEncumbrance.Substring(1) + ")"
            Else
                e.Item.Cells(7).Style.Add("padding-right", "9px")
            End If

            If Decimal.Parse(strActEncum) < 0 Then
                strActEncum = "(" + strActEncum.Substring(1) + ")"
            Else
                e.Item.Cells(8).Style.Add("padding-right", "9px")
            End If


            Dim lbl As Label

            lbl = CType(e.Item.FindControl("lblUCID"), Label)
            lbl.Text = strUCID

            Dim hyplnk As HyperLink = CType(e.Item.FindControl("hyplnkEmployeeName"), HyperLink)
            If employeeName <> "ZZZZZZZZ" Then
                ' Add hyperlink to name but parameter is UCID to link to employees detail
                hyplnk.Text = employeeName
                Dim strToEncrypted As String = employeeName + "|" + strUCID + "|" + "UCID"
                Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)
                ' Set a progress bar to pop-up when user clicks on this employeeName detail
                hyplnk.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectEmployeeDetails.aspx?value=" + strEncryptedResult.Trim + "%26category=PROJECTEMPLOYEEDETAIL"
            Else
                hyplnk.Text = "TOTAL"
            End If

            lbl = CType(e.Item.FindControl("lblSalary"), Label)
            lbl.Text = strSalary

            lbl = CType(e.Item.FindControl("lblBenefit"), Label)
            lbl.Text = strBenefit

            lbl = CType(e.Item.FindControl("lblSalBen"), Label)
            lbl.Text = strSalBen

            lbl = CType(e.Item.FindControl("lblEncumbrance"), Label)
            lbl.Text = strEncumbrance

            lbl = CType(e.Item.FindControl("lblActEncum"), Label)
            lbl.Text = strActEncum

        End If


    End Sub

    Private Sub dgrdEmployees_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgrdEmployees.PageIndexChanged

        Me.dgrdEmployees.CurrentPageIndex = e.NewPageIndex
        Me.dgrdEmployees.DataSource = CType(Session("dtEmployee"), DataTable)
        Me.dgrdEmployees.DataBind()

    End Sub


    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        If Not Session("PageStart") Is Nothing Then

            'Me.lblResponseTime.Text = String.Format("{0}.{1}", Date.Now.Subtract(CType(Session("PageStart"), Date)).Seconds, Date.Now.Subtract(CType(Session("PageStart"), Date)).Milliseconds)

            Session.Remove("PageStart")

        End If

        MyBase.Render(writer)

    End Sub

    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentType = "application/vnd.ms-Excel"
        HttpContext.Current.Response.Charset = ""

        Dim strCPID As String = CType(Session("CPID"), String)

        If Not strCPID Is Nothing Then
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strCPID + " - ProjectEmployees.xls")
        Else
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=ProjectEmployees.xls")
        End If

        EnableViewState = False
        Dim oStringWriter As System.IO.StringWriter = New System.IO.StringWriter()
        Dim oHtmlTextWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriter)

        Me.dgrdEmployees.AllowPaging = False
        Me.dgrdEmployees.DataSource = CType(Session("dtEmployee"), DataTable)
        Me.dgrdEmployees.DataBind()

        'Convert the controls to string literals
        Utils.ClearControls(Me.dgrdEmployees)
        ProjectChartField1.RenderControl(oHtmlTextWriter)
        dgrdEmployees.RenderControl(oHtmlTextWriter)

        HttpContext.Current.Response.Write(oStringWriter.ToString())
        HttpContext.Current.Response.End()

        Me.dgrdEmployees.AllowPaging = True
        Me.dgrdEmployees.DataSource = CType(Session("dtEmployee"), DataTable)
        Me.dgrdEmployees.DataBind()
    End Sub

    Protected Sub dgrdEmployees_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgrdEmployees.SortCommand
        Dim SortExpression As String = e.SortExpression
        Dim SortDirection As String = "ASC"
        If SortExpression.Equals(dgrdEmployees.Attributes("SortExpression").ToString()) Then
            If dgrdEmployees.Attributes("SortDirection").ToString().StartsWith("ASC") Then
                SortDirection = "DESC"
            Else
                SortDirection = "ASC"
            End If
        End If
        dgrdEmployees.Attributes("SortExpression") = SortExpression
        dgrdEmployees.Attributes("SortDirection") = SortDirection
        BindData(CType(Session("dtEmployee"), DataTable))
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

            HeaderControl.GetPHControlProjectYear().DataSource = dt
            HeaderControl.GetPHControlProjectYear().DataTextField = "fld_Year"
            HeaderControl.GetPHControlProjectYear().DataValueField = "fld_Year"
            HeaderControl.GetPHControlProjectYear().DataBind()
        End If

    End Sub

End Class
