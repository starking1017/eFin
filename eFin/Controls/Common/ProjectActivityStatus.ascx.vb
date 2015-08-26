Option Compare Text

Partial Class Controls_ProjectActivityStatus
    Inherits System.Web.UI.UserControl
    Private HardKey As String = ""

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        If Not Me.IsPostBack Then
            Dim strCPID As String = CType(Session("CPID"), String)
            Dim strACTID As String = CType(Session("ACTID"), String)
            Dim strCategory As String = CType(Session("Category"), String)

            If strCategory <> "CP" And strCategory <> "ACT" Then
                Return
            End If

            If Not strCPID Is Nothing Then
                Dim arrParams As New ArrayList
                arrParams.Add(strCPID)

                Dim objProjectStatus As New BLL.Status.ProjectHeader

                ' Retrieve project header and assign values to the controls
                If objProjectStatus.LoadProjectHeader("SELECT", "dbo.eFinsp_LoadProjectStatus_1", arrParams) = True Then
                    Select Case objProjectStatus.ProjectStatus.Trim
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

                    ' First part normal user table
                    Me.lblTitle.Text = objProjectStatus.Title
                    Me.lblName.Text = objProjectStatus.Name
                    Me.lblStatus.Text = objProjectStatus.Status
                    Me.lblDescription.Text = objProjectStatus.Project_Long_Desc
                    Me.lblDescription.ToolTip = objProjectStatus.Project_Long_Desc

                    Me.lblFund.Text = objProjectStatus.ProjectFund
                    Me.lbDeptCode.Text = objProjectStatus.Dept
                    Me.lblBusinessUnit.Text = objProjectStatus.BusinessUnitPC

                    ' Save to section for project detail header
                    Dim arrltProChartField As ArrayList = New ArrayList
                    arrltProChartField.Add(lblFund.Text)
                    arrltProChartField.Add(lbDeptCode.Text)
                    arrltProChartField.Add(lblBusinessUnit.Text)
                    Session("ProjectChartFields") = arrltProChartField

                    If objProjectStatus.Period.StartDate <> "Jan/01/1900" Then
                        Me.lblBeginDate.Text = objProjectStatus.Period.StartDate
                    Else
                        Me.lblBeginDate.Text = ""
                    End If

                    If objProjectStatus.Period.EndDate <> "Jan/01/1900" Then
                        Me.lblEndDate.Text = objProjectStatus.Period.EndDate
                    Else
                        Me.lblEndDate.Text = ""
                    End If

                    'If objProjectStatus.SecondEnddate <> "Jan/01/1900" Then
                    '    Me.lblSeconEndDate.Text = objProjectStatus.SecondEnddate
                    'Else
                    '    Me.lblSeconEndDate.Text = ""
                    'End If

                    If objProjectStatus.ReviewDate <> "Jan/01/1900" Then
                        Me.lblExpiryDate.Text = objProjectStatus.ReviewDate
                    Else
                        Me.lblExpiryDate.Text = ""
                    End If

                    Me.lblRetainFund.Text = objProjectStatus.Retain_Fund

                    Me.lblSponsor.Text = objProjectStatus.SponsorName
                    Me.lblSponsor.ToolTip = objProjectStatus.SponsorName
                    Me.lbTricouncil.Text = objProjectStatus.TriCouncilSource
                    Me.lblReference1.Text = objProjectStatus.Reference1
                    Me.lblReference2.Text = objProjectStatus.Reference2
                    Me.lblPaymentMethod.Text = objProjectStatus.PrimarySource
                    Me.lblHoldbackAmount.Text = objProjectStatus.HoldBack_Amount
                    Me.lblInterestBearing.Text = objProjectStatus.Interest_Bearing
                    Me.lblOverheadAmount.Text = objProjectStatus.Overhead_Amount

                    Me.lblAOAmount.Text = "$" + String.Format("{0:n}", Decimal.Parse(objProjectStatus.AuthorizedOverExpAmount))
                    Me.lblAOStartDate.Text = IIf(objProjectStatus.AuthorizedOEStartDate = "Jan/01/1900", "", objProjectStatus.AuthorizedOEStartDate)
                    Me.lblAOEndDate.Text = IIf(objProjectStatus.AuthorizedOEEndDate = "Jan/01/1900", "", objProjectStatus.AuthorizedOEEndDate)

                    ' Add for link to parent project
                    Me.hpyParentProject.Text = objProjectStatus.Parent_Project_Code
                    Session("ParentProject") = objProjectStatus.Parent_Project_Code
                    Dim strParentProject As String = CType(Session("ParentProject"), String) ' Used to determine which panel to show
                    Dim intSecurityRank As Integer = CType(Session("SecurityRank"), Integer) ' Used to determine which panel to show
                    Dim strSecurityRank As String = CType(intSecurityRank, String) ' Used to determine to cache table or not
                    Dim strCacheSPID As String = CType(Session("CacheSPID"), String) ' Used to determine to cache table or not

                    Dim strToEncrypted As String = ""
                    If strCacheSPID <> strParentProject Then
                        strToEncrypted = strParentProject + "|" + "SP" + "|" + "NeedToCache" + "|" + strSecurityRank
                    Else
                        strToEncrypted = strParentProject + "|" + "SP" + "|" + strParentProject + "|" + strSecurityRank
                    End If

                    Dim objTamperProof As New BLL.Security.TamperProofQueryString64
                    Dim strEncryptedResult As String = objTamperProof.QueryStringEncode(strToEncrypted, Me.HardKey)
                    If intSecurityRank <= 3 Then
                        hpyParentProject.NavigateUrl = Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=" + Utils.ApplicationPath + "/Forms/Researcher/frm_ProjectDetail.aspx?value=" + strEncryptedResult.Trim
                    End If

                    Me.lblHuman.Text = objProjectStatus.HumanCertNumber
                    Me.lblAnimal.Text = objProjectStatus.AnimalCertNumber
                    Me.lblBiosafety.Text = objProjectStatus.BiosafetyCertNumber

                    Me.lbRSO.Text = objProjectStatus.RSO_Number
                    Me.lbCRO.Text = objProjectStatus.CRO

                    ' For second part administrative view
                    Me.lblLocation.Text = objProjectStatus.Location

                    Me.lblPurposeofFunds.Text = objProjectStatus.PurposeOfFunds
                    Me.lblGeneralClassification.Text = objProjectStatus.GeneralClassification
                    Me.lblPrimarySource.Text = objProjectStatus.PrimarySource

                    Me.lblFrequencyofInvoice1.Text = objProjectStatus.BillingFreqDesc
                    'Me.lblFrequencyofInvoice2.Text = objProjectStatus.BillingFreqDesc2
                    Me.lblAgencyInvoice1.Text = objProjectStatus.BillingFormat1
                    'Me.lblAgencyInvoice2.Text = objProjectStatus.BillingFormat2

                    Me.lblFrequencyofFinancial1.Text = objProjectStatus.SponsorReportingFreqDesc
                    'Me.lblFrequencyofFinancial2.Text = objProjectStatus.SponsorReportingFreqDesc2
                    Me.lblAgencyReportingFormat1.Text = objProjectStatus.SponsorReportFormat
                    'Me.lblAgencyReportingFormat2.Text = objProjectStatus.SubReportFormat

                    ' For reserve in the future
                    '=================================================================================================
                    'Me.lblContactInfo.Text = objProjectStatus.ContactInfo
                    'If objProjectStatus.EmailforEC <> "" Then
                    '    Me.lblContactInfo.Text = Me.lblContactInfo.Text + "<br>" + objProjectStatus.EmailforEC
                    'End IflblInstituteCentre

                    'If objProjectStatus.Phone <> "" Then
                    '    Me.lblContactInfo.Text = Me.lblContactInfo.Text + "<br>" + objProjectStatus.Phone
                    'End If

                    'Me.lblResearchGroup.Text = objProjectStatus.ResearchGroup
                    'Me.lblSubPurpose.Text = objProjectStatus.SubPurpose
                    'Me.lblProgressReporting.Text = objProjectStatus.ProgressReporting
                    'Me.lblReportingBilling.Text = objProjectStatus.ProjectHolderBilling
                    'Me.lblOwnership.Text = objProjectStatus.Ownership
                    'Me.lblAdminName.Text = objProjectStatus.AdminName
                    'Me.lblEmail.Text = objProjectStatus.AdminEmail
                    'Me.lblCloseDate.Text = objProjectStatus.FinalCloseDate
                    'Me.lbAdminPhone.Text = objProjectStatus.AdminPhone
                    'Me.lblPin.Text = objProjectStatus.NSERC

                    'If objProjectStatus.ResearchType.ToLower = "s" Then
                    '    lblProjectType.Text = "Sponsored Research"
                    'ElseIf objProjectStatus.ResearchType.ToLower = "o" Then
                    '    lblProjectType.Text = "Other Restricted"
                    'Else
                    '    lblProjectType.Text = ""
                    'End If
                    'lblProjectType.Text = objProjectStatus.ProjectType

                    'Me.lblInterestBearing.Text = objProjectStatus.SponsorSource
                    'Me.lblCurrency.Text = objProjectStatus.BillingCurrency
                    'Me.lblHuman.Text = objProjectStatus.HumanCertification
                    'Me.lblAnimal.Text = objProjectStatus.AnimalCertification
                    'Me.lblBiosafety.Text = objProjectStatus.BioCertification
                    'Me.lblPayrollSubType.Text = objProjectStatus.PayrollSubType
                    'Me.lblInstituteCentre.Text = objProjectStatus.Institute
                    'Me.lblPrimarySource.Text = objProjectStatus.AwardStatusName
                    '------------------------------------------------------------------------------------------

                End If

                ' Determine if this is an activity header
                If strCategory = "ACT" Then
                    Me.panStatus.Visible = True
                    Me.panActivity.Visible = True

                    arrParams.Add(strACTID)

                    Dim objActivityStatus As New BLL.Status.ActivityHeader

                    'Retrieve activity header information
                    If objActivityStatus.LoadActivityHeader("SELECT", "dbo.eFinsp_LoadActivityStatus", arrParams) = True Then
                        Select Case objActivityStatus.ActivityStatus.Trim
                            Case "A"
                                Me.lblActStatus.Text = "Active"

                                Me.lblActivityStatus.Visible = True
                                Me.lblActStatus.Visible = True
                            Case "I"
                                Me.lblActStatus.Text = "Inactive"

                                Me.lblActivityStatus.Visible = True
                                Me.lblActStatus.Visible = True
                            Case "C"
                                Me.lblActStatus.Text = "Closed"

                                Me.lblActivityStatus.Visible = True
                                Me.lblActStatus.Visible = True
                            Case "S"
                                Me.lblActStatus.Text = "Suspended"

                                Me.lblActivityStatus.Visible = True
                                Me.lblActStatus.Visible = True
                            Case "P"
                                Me.lblActStatus.Text = "Pending"

                                Me.lblActivityStatus.Visible = True
                                Me.lblActStatus.Visible = True
                            Case ""
                                Me.lblActivityStatus.Visible = False
                                Me.lblActStatus.Visible = False

                        End Select

                        Me.lblActDescription.Text = objActivityStatus.ActivityDescription

                        If objActivityStatus.ActivityStartDate <> "Jan/01/1900" Then
                            Me.lblActBeginDate.Text = objActivityStatus.ActivityStartDate
                        Else
                            Me.lblActBeginDate.Text = ""
                        End If

                        If objActivityStatus.ActivityEndDate <> "Jan/01/1900" Then
                            Me.lblActEndDate.Text = objActivityStatus.ActivityEndDate
                        Else
                            Me.lblActEndDate.Text = ""
                        End If
                    End If


                Else
                    Me.panStatus.Visible = True
                    Me.panActivity.Visible = False

                    Dim fSum As Object = CType(Me.Parent, Object)

                    Dim lbl As Label
                    Dim amount As Decimal

                    lbl = CType(fSum.FindControl("lblPeriod"), Label)
                    lbl.Text = objProjectStatus.AuthorizedOverExpPeriod

                    lbl = CType(fSum.FindControl("lblAmount"), Label)

                    amount = objProjectStatus.AuthorizedOverExpAmount

                    lbl.Text = "$" + String.Format("{0:n}", Decimal.Parse(amount))

                End If

            End If

            ' Save project period
            Dim dateProjectStart As Date = CType(lblBeginDate.Text, DateTime)
            Dim strProjectStart As String = dateProjectStart.ToString("yyyyMMdd")
            BLL.ProjectPeriod.AddProjectPeriodToTheSession(New BLL.ProjectPeriod(strProjectStart, lblEndDate.Text))

        End If

        If Session("isadmin") = "true" Then
            Me.panStatusAdmin.Visible = True
        End If
    End Sub

    Public ReadOnly Property ProjectlblBusinessUnit() As String
        Get
            Return Me.lblBusinessUnit.Text
        End Get
    End Property
    
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
