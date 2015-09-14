Option Compare Text

Partial Class Controls_SummaryProjectStatus
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        If Not Me.IsPostBack Then

            Dim strSPID As String = CType(Session("SPID"), String)

            Dim strCategory As String = CType(Session("Category"), String)  ' Retrieve category from session
            If strCategory <> "SP" Then
                Return
            End If

            If Not strSPID Is Nothing Then
                Dim arrParams As New ArrayList
                arrParams.Add(strSPID)

                Dim objSumProjStatus As New BLL.Status.SummaryProjectHeader

                ' Retrieve summary project header and assign values to the controls
                If objSumProjStatus.LoadSummaryProjectHeader("SELECT", "dbo.eFinsp_LoadSumProjectStatus", arrParams) = True Then
                    Select Case objSumProjStatus.ProjectStatus.Trim
                        Case "A"
                            Me.lblProjectStatus.Text = "Active"
                        Case "I"
                            Me.lblProjectStatus.Text = "Inactive"
                        Case "C"
                            Me.lblProjectStatus.Text = "Closed"
                        Case "S"
                            Me.lblProjectStatus.Text = "Suspended"
                        Case "P"
                            Me.lblProjectStatus.Text = "Pending"

                    End Select

                    Me.lblName.Text = objSumProjStatus.Name
                    Me.lblStatus.Text = objSumProjStatus.ProjectHolderStatus
                    Me.lblDeptCode.Text = objSumProjStatus.Dept
                    Me.lblContactInfo.Text = objSumProjStatus.Contactinformation

                    If objSumProjStatus.EmailForEC <> "" Then
                        Me.lblContactInfo.Text = Me.lblContactInfo.Text + "<br>" + objSumProjStatus.EmailForEC
                    End If

                    If objSumProjStatus.Phone <> "" Then
                        Me.lblContactInfo.Text = Me.lblContactInfo.Text + "<br>" + objSumProjStatus.Phone
                    End If

                    Me.lblTitle.Text = objSumProjStatus.Title

                    If objSumProjStatus.Period.StartDate <> "Jan/01/1900" Then
                        Me.lblBeginDate.Text = objSumProjStatus.Period.StartDate
                    Else
                        Me.lblBeginDate.Text = ""
                    End If

                    If objSumProjStatus.Period.EndDate <> "Jan/01/1900" Then
                        Me.lblEndDate.Text = objSumProjStatus.Period.EndDate
                    Else
                        Me.lblEndDate.Text = ""
                    End If

                    If objSumProjStatus.ReviewDate <> "Jan/01/1900" Then
                        Me.lblReviewDate.Text = objSumProjStatus.ReviewDate
                    Else
                        Me.lblReviewDate.Text = ""
                    End If
                    Me.lblAgencyReporting.Text = objSumProjStatus.AgencyReporting
                    Me.lblAgencyReporting2.Text = objSumProjStatus.AgencyReporting2
                    Me.lblReportFormat.Text = objSumProjStatus.ReportFormat
                    Me.lblReportFormat2.Text = objSumProjStatus.ReportFormat2
                End If

            End If

            ' Save project period
            If Not lblBeginDate.Text Is Nothing Then

                Dim dateProjectStart As Date = CType(lblBeginDate.Text, DateTime)
                Dim strProjectStart As String = dateProjectStart.ToString("yyyyMMdd")
                Dim dateProjectEnd As Date = CType(lblEndDate.Text, DateTime)
                Dim strProjectEnd As String = dateProjectEnd.ToString("yyyyMMdd")

                Dim strProjectExpiry As String
                If Not lblReviewDate.Text = "" Then
                    Dim dateProjectExpiry As Date = CType(lblReviewDate.Text, DateTime)
                    strProjectExpiry = dateProjectExpiry.ToString("yyyyMMdd")
                Else
                    strProjectExpiry = strProjectEnd
                End If

                BLL.ProjectPeriod.AddProjectPeriodToTheSession(New BLL.ProjectPeriod(strProjectStart, strProjectEnd, strProjectExpiry))

            End If
        End If

    End Sub
    Public ReadOnly Property ProjectStartDate() As String
        Get
            Return Me.lblBeginDate.Text
        End Get
    End Property

    Public ReadOnly Property ProjectEndDate() As String
        Get
            Return Me.lblEndDate.Text
        End Get
    End Property
End Class
