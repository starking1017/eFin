Option Compare Text

Partial Class Forms_Researcher_frm_ProjectEmployeeDetails
    Inherits System.Web.UI.Page
    Private HardKey As String = ""


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, HeaderControl.UpdateEmployeeDetail

        If Session("UCID") Is Nothing Then
            Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            CType(Page.Master, MasterPage).PageIdentity = "Enterprise Reporting<br/>eFIN - 08"
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

        ' First time page load
        If Not IsPostBack Then
            EnableStartAtEmployee(False)

            ' Retrieve period from session since first page load
            startDate = BLL.Period.GetPeriodFromTheSession.StartDate
            endDate = BLL.Period.GetPeriodFromTheSession.EndDate

        Else
            ' Retrieve period from the header control
            startDate = HeaderControl.GetStartDate
            endDate = HeaderControl.GetEndDate

            ' If same as viewstate -> start date and end date never changed
            ' Return
            If CType(ViewState("StartDate"), String) = startDate AndAlso _
               CType(ViewState("EndDate"), String) = endDate Then
                Return
            End If
        End If

        Dim str() As String
        Dim strToDecrypted As String = Request.QueryString("value")
        Dim strEmployeeID As String = ""
        Dim strEmployeeName As String = ""

        If Not strToDecrypted Is Nothing Then
            Dim objTamperProof As New BLL.Security.TamperProofQueryString64
            Dim strDecryptedResult As String = objTamperProof.QueryStringDecode(strToDecrypted, Me.HardKey)
            str = strDecryptedResult.Split("|")
            If (str.Length = 3) Then
                strEmployeeName = str(0)
                strEmployeeID = str(1)
            End If
        End If

        LoadEmployeesDetail(startDate, endDate, strEmployeeID)    ' Get activity summary for the account
        lblEmployeeNameS.Text = strEmployeeName
        lblEmployeeNameB.Text = strEmployeeName

        ' Store new start and end date to viewstate
        ViewState("StartDate") = startDate
        ViewState("EndDate") = endDate

    End Sub

    Private Sub EnableStartAtEmployee(ByVal bEnableStart As Boolean)

        Dim pan As Panel = CType(HeaderControl.FindControl("panStartAt"), Panel)
        pan.Visible = bEnableStart

        Dim lbl As Label = CType(HeaderControl.FindControl("lblStartAtProject"), Label)

        lbl.Visible = bEnableStart
        lbl.Text = "Start At UCID:"

    End Sub

    Private Sub LoadEmployeesDetail(ByRef startDate As String, ByRef endDate As String, ByVal EmployeeID As String)

        Dim strUCID As String = CType(Session("UCID"), String)
        Dim strCPID As String = CType(Session("CPID"), String)
        Dim dt As DataTable
        Dim arrParams As New ArrayList

        arrParams.Add(strUCID)
        arrParams.Add(strCPID)
        arrParams.Add(startDate)
        arrParams.Add(endDate)
        arrParams.Add(EmployeeID)

        dt = BLL.ProjectEmployee.LoadProjectEmployee("SELECT", "dbo.eFinsp_LoadProjectEmployeeDetail", arrParams)

        If Not dt Is Nothing Then
            Me.dgrdEmployeesSalary.CurrentPageIndex = 0
            BindData(dt)
        End If

    End Sub

    Private Sub BindData(ByRef dt As DataTable)

        ' for add Budget Variance column, remember cache table also need to do
        Dim dtS As New DataTable
        dtS = dt.Clone()
        RestructureDT(dt, dtS, "fld_Salary")
        Session("dtEmployeesSalary") = dtS

        Me.dgrdEmployeesSalary.DataSource = dtS
        Me.dgrdEmployeesSalary.DataBind()

        ' for add Budget Variance column, remember cache table also need to do
        Dim dtB As New DataTable
        dtB = dt.Clone()
        RestructureDT(dt, dtB, "fld_Benefit")
        Session("dtEmployeesBenefit") = dtB

        Me.dgrdEmployeesBenefit.DataSource = dtB
        Me.dgrdEmployeesBenefit.DataBind()

    End Sub

    Private Sub RestructureDT(ByRef dtSource As DataTable, ByRef dtDest As DataTable, ByVal sType As String)

        Dim dtIndex As Integer = 0
        Dim drOldDT As DataRow
        Select Case sType
            Case "fld_Salary"
                For dtIndex = 0 To dtSource.Rows.Count - 1
                    If dtSource.Rows(dtIndex).Item(sType) <> 0 Or _
                    ((dtSource.Rows(dtIndex).Item("fld_Salary") = 0 And dtSource.Rows(dtIndex).Item("fld_Benefit") = 0) And _
                    dtSource.Rows(dtIndex).Item("fld_EmplID").ToString() <> "TOTALB") Then
                        drOldDT = dtSource.Rows(dtIndex)
                        dtDest.ImportRow(drOldDT)
                    End If
                Next
            Case "fld_Benefit"
                For dtIndex = 0 To dtSource.Rows.Count - 1
                    If dtSource.Rows(dtIndex).Item(sType) <> 0 Or dtSource.Rows(dtIndex).Item("fld_EmplID").ToString() = "TOTALB" Then
                        drOldDT = dtSource.Rows(dtIndex)
                        dtDest.ImportRow(drOldDT)
                    End If
                Next
        End Select

    End Sub


    Private Sub dgrdEmployeesSalary_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrdEmployeesSalary.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim strUCID As String = CType(e.Item.DataItem("fld_EmplID"), String)
            Dim strFiscalPeriod As String = CType(e.Item.DataItem("fld_Fiscal_Period"), String)

            Dim salary As Decimal = CType(e.Item.DataItem("fld_Salary"), Decimal)
            Dim benefit As Decimal = CType(e.Item.DataItem("fld_Benefit"), Decimal)
            Dim encumbrance As Decimal = CType(e.Item.DataItem("fld_Encumbrance"), Decimal)

            Dim strSalary As String
            Dim strBenefit As String
            Dim strEncumbrance As String

            strSalary = String.Format("{0:n}", salary)
            strBenefit = String.Format("{0:n}", benefit)
            strEncumbrance = String.Format("{0:n}", encumbrance)

            If strUCID = "TOTALS" Then
                strUCID = "TOTAL"
                e.Item.Style("background-color") = "#999999"
                e.Item.Style("font-weight") = "bold"
            End If

            If strFiscalPeriod <> "" Then
                strFiscalPeriod = strFiscalPeriod.Insert(strFiscalPeriod.Length - 2, "/")
            End If

            ' Add excel export style to ensure the column format is perserved (Employee)
            e.Item.Cells(0).Style.Add("mso-number-format", "\@")
            ' Add excel export style to ensure the column format is perserved (Account Code)
            e.Item.Cells(3).Style.Add("mso-number-format", "\@")
            ' Add excel export style to ensure the column format is perserved (Activity Code)
            e.Item.Cells(5).Style.Add("mso-number-format", "\@")
            ' Add excel export style to ensure the column format is perserved (Journal Ref)
            e.Item.Cells(7).Style.Add("mso-number-format", "\@")

            If strSalary = 0 Or strUCID <> "TOTAL" Then
                If (strBenefit <> 0) Then
                    e.Item.Visible = False
                End If
            End If

            If Decimal.Parse(strSalary) < 0 Then
                strSalary = "(" + strSalary.Substring(1) + ")"
            Else
                e.Item.Cells(8).Style.Add("padding-right", "9px")
            End If

            If Decimal.Parse(strBenefit) < 0 Then
                strBenefit = "(" + strBenefit.Substring(1) + ")"
            Else
                e.Item.Cells(9).Style.Add("padding-right", "9px")
            End If

            If Decimal.Parse(strEncumbrance) < 0 Then
                strEncumbrance = "(" + strEncumbrance.Substring(1) + ")"
            Else
                e.Item.Cells(10).Style.Add("padding-right", "9px")
            End If

            Dim lbl As Label

            lbl = CType(e.Item.FindControl("lblUCID"), Label)
            lbl.Text = strUCID

            lbl = CType(e.Item.FindControl("lblFiscalPeriod"), Label)
            lbl.Text = strFiscalPeriod

            lbl = CType(e.Item.FindControl("lblSalary"), Label)
            lbl.Text = strSalary

            lbl = CType(e.Item.FindControl("lblBenefit"), Label)
            lbl.Text = strBenefit

            lbl = CType(e.Item.FindControl("lblEncumbrance"), Label)
            lbl.Text = strEncumbrance

        End If

    End Sub

    Protected Sub dgrdEmployeesBenfit_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgrdEmployeesBenefit.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim strUCID As String = CType(e.Item.DataItem("fld_EmplID"), String)
            Dim strFiscalPeriod As String = CType(e.Item.DataItem("fld_Fiscal_Period"), String)

            Dim salary As Decimal = CType(e.Item.DataItem("fld_Salary"), Decimal)
            Dim benefit As Decimal = CType(e.Item.DataItem("fld_Benefit"), Decimal)
            Dim encumbrance As Decimal = CType(e.Item.DataItem("fld_Encumbrance"), Decimal)

            Dim strSalary As String
            Dim strBenefit As String
            Dim strEncumbrance As String

            strSalary = String.Format("{0:n}", salary)
            strBenefit = String.Format("{0:n}", benefit)
            strEncumbrance = String.Format("{0:n}", encumbrance)

            If strUCID = "TOTALB" Then
                strUCID = "TOTAL"
                e.Item.Style("background-color") = "#999999"
                e.Item.Style("font-weight") = "bold"
            End If

            If strFiscalPeriod <> "" Then
                strFiscalPeriod = strFiscalPeriod.Insert(strFiscalPeriod.Length - 2, "/")
            End If

            ' Add excel export style to ensure the column format is perserved (Employee)
            e.Item.Cells(0).Style.Add("mso-number-format", "\@")
            ' Add excel export style to ensure the column format is perserved (Account Code)
            e.Item.Cells(3).Style.Add("mso-number-format", "\@")
            ' Add excel export style to ensure the column format is perserved (Activity Code)
            e.Item.Cells(5).Style.Add("mso-number-format", "\@")
            ' Add excel export style to ensure the column format is perserved (Journal Ref)
            e.Item.Cells(7).Style.Add("mso-number-format", "\@")
           
            If strBenefit = 0 Or strUCID <> "TOTAL" Then
                If (strSalary <> 0) Then
                    e.Item.Visible = False
                End If
            End If

            If Decimal.Parse(strSalary) < 0 Then
                strSalary = "(" + strSalary.Substring(1) + ")"
            Else
                e.Item.Cells(8).Style.Add("padding-right", "9px")
            End If

            If Decimal.Parse(strBenefit) < 0 Then
                strBenefit = "(" + strBenefit.Substring(1) + ")"
            Else
                e.Item.Cells(9).Style.Add("padding-right", "9px")
            End If

            If Decimal.Parse(strEncumbrance) < 0 Then
                strEncumbrance = "(" + strEncumbrance.Substring(1) + ")"
            Else
                e.Item.Cells(10).Style.Add("padding-right", "9px")
            End If

            Dim lbl As Label

            lbl = CType(e.Item.FindControl("lblUCID"), Label)
            lbl.Text = strUCID

            lbl = CType(e.Item.FindControl("lblFiscalPeriod"), Label)
            lbl.Text = strFiscalPeriod

            lbl = CType(e.Item.FindControl("lblSalary"), Label)
            lbl.Text = strSalary

            lbl = CType(e.Item.FindControl("lblBenefit"), Label)
            lbl.Text = strBenefit

            lbl = CType(e.Item.FindControl("lblEncumbrance"), Label)
            lbl.Text = strEncumbrance

        End If

    End Sub


    Private Sub dgrdEmployeesSalary_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgrdEmployeesSalary.PageIndexChanged

        Me.dgrdEmployeesSalary.CurrentPageIndex = e.NewPageIndex
        Me.dgrdEmployeesSalary.DataSource = CType(Session("dtEmployeesSalary"), DataTable)
        Me.dgrdEmployeesSalary.DataBind()

    End Sub

    Private Sub dgrdEmployeesBenfit_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgrdEmployeesBenefit.PageIndexChanged

        Me.dgrdEmployeesBenefit.CurrentPageIndex = e.NewPageIndex
        Me.dgrdEmployeesBenefit.DataSource = CType(Session("dtEmployeesBenefit"), DataTable)
        Me.dgrdEmployeesBenefit.DataBind()

    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        If Not Session("PageStart") Is Nothing Then
            'Me.lblResponseTime.Text = String.Format("{0}.{1}", Date.Now.Subtract(CType(Session("PageStart"), Date)).Seconds, Date.Now.Subtract(CType(Session("PageStart"), Date)).Milliseconds)
            Session.Remove("PageStart")
        End If
        MyBase.Render(writer)

    End Sub

    Protected Sub btnExcelSalary_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        HttpContext.Current.Response.ClearHeaders()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Buffer = True
        HttpContext.Current.Response.ContentType = "application/vnd.ms-Excel"
        HttpContext.Current.Response.Charset = ""

        Dim strCPID As String = CType(Session("CPID"), String)

        If Not strCPID Is Nothing Then
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strCPID + " - ProjectEmployeesSalary.xls")
        Else
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=ProjectEmployeesSalary.xls")
        End If

        EnableViewState = False
        Dim oStringWriterC As System.IO.StringWriter = New System.IO.StringWriter()
        Dim oHtmlTextWriterC As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriterC)

        Dim oStringWriterS As System.IO.StringWriter = New System.IO.StringWriter()
        Dim oStringWriterB As System.IO.StringWriter = New System.IO.StringWriter()
        Dim oHtmlTextWriterS As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriterS)
        Dim oHtmlTextWriterB As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(oStringWriterB)

        ' Disable paging
        Me.dgrdEmployeesSalary.AllowPaging = False
        Me.dgrdEmployeesSalary.DataSource = CType(Session("dtEmployeesSalary"), DataTable)
        Me.dgrdEmployeesSalary.DataBind()
        Me.dgrdEmployeesBenefit.AllowPaging = False
        Me.dgrdEmployeesBenefit.DataSource = CType(Session("dtEmployeesBenefit"), DataTable)
        Me.dgrdEmployeesBenefit.DataBind()


        ' Convert the controls to string literals
        ProjectChartField4.RenderControl(oHtmlTextWriterC)
        HttpContext.Current.Response.Write(oStringWriterC.ToString())

        HttpContext.Current.Response.Write("<P><H3>Salary Details</H3><P>")
        HttpContext.Current.Response.Write("<BR>" + "Employee Name: ")
        lblEmployeeNameS.RenderControl(oHtmlTextWriterS)
        Utils.ClearControls(Me.dgrdEmployeesSalary)

        dgrdEmployeesSalary.RenderControl(oHtmlTextWriterS)
        HttpContext.Current.Response.Write(oStringWriterS.ToString())

        HttpContext.Current.Response.Write("<BR><H3>Benefit Details</H3><P>")
        HttpContext.Current.Response.Write("Employee Name: ")
        Utils.ClearControls(Me.dgrdEmployeesBenefit)
        lblEmployeeNameB.RenderControl(oHtmlTextWriterB)

        dgrdEmployeesBenefit.RenderControl(oHtmlTextWriterB)
        HttpContext.Current.Response.Write(oStringWriterB.ToString())

        HttpContext.Current.Response.End()

        ' Enable paging
        Me.dgrdEmployeesSalary.AllowPaging = True
        Me.dgrdEmployeesSalary.DataSource = CType(Session("dtEmployeesSalary"), DataTable)
        Me.dgrdEmployeesSalary.DataBind()
        Me.dgrdEmployeesBenefit.AllowPaging = True
        Me.dgrdEmployeesBenefit.DataSource = CType(Session("dtEmployeesBenefit"), DataTable)
        Me.dgrdEmployeesBenefit.DataBind()
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