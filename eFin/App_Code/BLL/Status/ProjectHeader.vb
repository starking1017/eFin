Imports System.Data
Namespace BLL.Status

    Public Class ProjectHeader

#Region "Member variable "
        Private _projectStatus As String

        ''' Line 1, elements , Project Holders Status
        Private _name As String
        Private _status As String
        Private _dept As String
        Private _contactinformation As String
        Private _emailforEC As String
        Private _phone As String
        Private _nsercpin As String


        ''' Line 2, elements, other affiliation 
        Private _otherdept As String
        Private _institutecentre As String
        Private _researchgroup As String
        Private _location As String


        ''' Line 3, Project Classifications 
        Private _generalpurpose As String
        Private _purposeoffunds As String
        Private _subpurpose As String
        Private _awardstatusname As String


        ''' Line 4, Projedct Title 
        Private _title As String


        ''' Line 5, Project Effective Dates
        Private _period As BLL.Period
        Private _reviewdate As String


        ''' Line 6, Project funding
        Private _sponsorname As String
        Private _reference1 As String
        Private _reference2 As String
        Private _primarysource As String
        Private _sponsorsource As String


        ''' Line 7, Progress Reporting by Project Holder
        ''' It needs to be clarified , June 8, 2005 
        Private _progressreporting As String
        Private _projectholderbilling As String


        ''' Line 8, Billing
        Private _billingfreqdesc As String
        Private _billingcurrency As String


        ''' Line 9 , Agency Reporting
        Private _sponsorreportingfreqdesc As String
        Private _sponsorreportformat As String
        Private _subreportformat As String

        ''' Line 10, Equipment Ownership
        Private _ownership As String


        ''' line 11, Certification 
        Private _humanCertification As String
        Private _animalCertification As String
        Private _bioCertification As String

        Private _authorizedOverExpenditurePeriod As String
        Private _authorizedOverExpenditureAmount As String
        Private _authorizedOEStartDate As String
        Private _authorizedOEEndDate As String

        Private _AdminName As String
        Private _AdminEmail As String
        Private _FinalCloseDate As String = ""
        Private _BillingFormat1 As String
        Private _BillingFormat2 As String
        Private _BillingFreqDesc2 As String
        Private _SponsorReportingFreqDesc2 As String
        Private _ResearchType As String
        Private _HumanCertNumber As String
        Private _AnimalCertNumber As String
        Private _BiosafetyCertNumber As String
        Private _AdminPhone As String
        Private _ProjectFund As String
        Private _TriCouncilSource As String
        'added on April 5, 2011 by Jack for new attributes
        Private _GeneralClassification As String
        Private _ProjectType As String
        Private _PayrollSubType As String
        Private _SecondEndDate As String = ""
        Private _businessUnitPC As String = ""
        '------------------------------- added on June 17, 2015 by Chris for new attributes
        Private _Overhead_Amount As String
        Private _Parent_Project_Code As String
        Private _Project_Long_Desc As String
        Private _Retain_Fund As String  'Hold_Back_Applicable_YN
        Private _Interest_Bearing As String 'Interest_Bearing_YN
        Private _HoldBack_Amount As String
        Private _RSO_Number As String   'RSO_App_ID
        Private _CRO As String   'CRO


#End Region


#Region "Property Functions"
        Public ReadOnly Property ProjectStatus() As String
            Get
                Return Me._projectStatus
            End Get
        End Property

        ''' Line 1, elements , Project Holders Status
        Public ReadOnly Property Name()
            Get
                Return Me._name
            End Get
        End Property

        Public ReadOnly Property Status()
            Get
                Return Me._status
            End Get
        End Property

        Public ReadOnly Property Dept()
            Get
                Return Me._dept
            End Get
        End Property

        Public ReadOnly Property ContactInfo()
            Get
                Return Me._contactinformation
            End Get
        End Property

        Public ReadOnly Property EmailforEC()
            Get
                Return Me._emailforEC
            End Get
        End Property

        Public ReadOnly Property Phone()
            Get
                Return Me._phone
            End Get
        End Property

        Public ReadOnly Property NSERC()
            Get
                Return Me._nsercpin
            End Get
        End Property


        ''' Line 2, elements, other affiliation 
        Public ReadOnly Property OtherDept()
            Get
                Return Me._otherdept
            End Get
        End Property

        Public ReadOnly Property Institute()
            Get
                Return Me._institutecentre
            End Get
        End Property

        Public ReadOnly Property ResearchGroup()
            Get
                Return Me._researchgroup
            End Get
        End Property

        Public ReadOnly Property Location()
            Get
                Return Me._location
            End Get
        End Property


        ''' Line 3, Project Classifications 
        Public ReadOnly Property GeneralPurpose()
            Get
                Return Me._generalpurpose
            End Get
        End Property

        Public ReadOnly Property PurposeOfFunds()
            Get
                Return Me._purposeoffunds
            End Get
        End Property

        Public ReadOnly Property SubPurpose()
            Get
                Return Me._subpurpose
            End Get
        End Property

        Public ReadOnly Property AwardStatusName()
            Get
                Return Me._awardstatusname
            End Get
        End Property


        ''' Line 4, Project Title 
        Public ReadOnly Property Title()
            Get
                Return Me._title
            End Get
        End Property


        ''' Line 5, Project Effective Dates
        Public ReadOnly Property Period() As BLL.Period
            Get
                Return Me._period
            End Get
        End Property

        Public ReadOnly Property ReviewDate()
            Get
                Return Me._reviewdate
            End Get
        End Property


        ''' Line 6, Project funding
        Public ReadOnly Property SponsorName()
            Get
                Return Me._sponsorname
            End Get
        End Property

        Public ReadOnly Property Reference1()
            Get
                Return Me._reference1
            End Get
        End Property

        Public ReadOnly Property Reference2()
            Get
                Return Me._reference2
            End Get
        End Property

        Public ReadOnly Property PrimarySource()
            Get
                Return Me._primarysource
            End Get
        End Property

        Public ReadOnly Property SponsorSource()
            Get
                Return Me._sponsorsource
            End Get
        End Property


        ''' Line 7, Progress Reporting by Project Holder
        ''' It needs to be clarified , June 8, 2005 
        Public ReadOnly Property ProgressReporting()
            Get
                Return Me._progressreporting
            End Get
        End Property

        Public ReadOnly Property ProjectHolderBilling()
            Get
                Return Me._projectholderbilling
            End Get
        End Property


        ''' Line 8, Billing
        Public ReadOnly Property BillingFreqDesc()
            Get
                Return Me._billingfreqdesc
            End Get
        End Property

        Public ReadOnly Property BillingCurrency()
            Get
                Return Me._billingcurrency
            End Get
        End Property


        ''' Line 9 , Agency Reporting
        Public ReadOnly Property SponsorReportingFreqDesc()
            Get
                Return Me._sponsorreportingfreqdesc
            End Get
        End Property

        Public ReadOnly Property SponsorReportFormat()
            Get
                Return Me._sponsorreportformat
            End Get
        End Property

        Public ReadOnly Property SubReportFormat()
            Get
                Return Me._subreportformat
            End Get
        End Property


        ''' Line 10, Equipment Ownership
        Public ReadOnly Property Ownership()
            Get
                Return Me._ownership
            End Get
        End Property


        ''' Line 11, Certification 
        Public ReadOnly Property HumanCertification()
            Get
                Return Me._humanCertification
            End Get
        End Property

        Public ReadOnly Property AnimalCertification()
            Get
                Return Me._animalCertification
            End Get
        End Property

        Public ReadOnly Property BioCertification()
            Get
                Return Me._bioCertification
            End Get
        End Property

        Public ReadOnly Property AuthorizedOverExpPeriod()
            Get
                Return Me._authorizedOverExpenditurePeriod
            End Get
        End Property

        Public ReadOnly Property AuthorizedOverExpAmount()
            Get
                Return Me._authorizedOverExpenditureAmount
            End Get
        End Property
        Public ReadOnly Property AuthorizedOEStartDate()
            Get
                Return Me._authorizedOEStartDate
            End Get
        End Property
        Public ReadOnly Property AuthorizedOEEndDate()
            Get
                Return Me._authorizedOEEndDate
            End Get
        End Property
        'Added by Jack on August 8, 2008
        Public ReadOnly Property AdminName() As String
            Get
                Return _AdminName
            End Get
        End Property
        Public ReadOnly Property AdminEmail() As String
            Get
                Return _AdminEmail
            End Get
        End Property
        Public ReadOnly Property FinalCloseDate() As String
            Get
                Return _FinalCloseDate
            End Get
        End Property
        Public ReadOnly Property BillingFormat1() As String
            Get
                Return _BillingFormat1
            End Get
        End Property
        Public ReadOnly Property BillingFormat2() As String
            Get
                Return _BillingFormat2
            End Get
        End Property
        Public ReadOnly Property BillingFreqDesc2() As String
            Get
                Return Me._BillingFreqDesc2
            End Get
        End Property
        Public ReadOnly Property SponsorReportingFreqDesc2() As String
            Get
                Return Me._SponsorReportingFreqDesc2
            End Get
        End Property
        Public ReadOnly Property ResearchType() As String
            Get
                Return Me._ResearchType
            End Get
        End Property
        Public ReadOnly Property HumanCertNumber() As String
            Get
                Return Me._HumanCertNumber
            End Get
        End Property
        Public ReadOnly Property AnimalCertNumber() As String
            Get
                Return Me._AnimalCertNumber
            End Get
        End Property
        Public ReadOnly Property BiosafetyCertNumber() As String
            Get
                Return Me._BiosafetyCertNumber
            End Get
        End Property
        Public ReadOnly Property AdminPhone() As String
            Get
                Return Me._AdminPhone
            End Get
        End Property
        Public ReadOnly Property ProjectFund() As String
            Get
                Return Me._ProjectFund
            End Get
        End Property
        Public ReadOnly Property TriCouncilSource() As String
            Get
                Return Me._TriCouncilSource
            End Get
        End Property
        'New attributes added on April 5, 2011
        Public ReadOnly Property GeneralClassification() As String
            Get
                Return Me._GeneralClassification
            End Get
        End Property
        Public ReadOnly Property ProjectType() As String
            Get
                Return Me._ProjectType
            End Get
        End Property
        Public ReadOnly Property PayrollSubType() As String
            Get
                Return Me._PayrollSubType
            End Get
        End Property
        Public ReadOnly Property SecondEnddate() As String
            Get
                Return Me._SecondEndDate
            End Get
        End Property
        Public ReadOnly Property BusinessUnitPC() As String
            Get
                Return Me._businessUnitPC
            End Get
        End Property
        Public ReadOnly Property Overhead_Amount() As String
            Get
                Return Me._Overhead_Amount
            End Get
        End Property
        Public ReadOnly Property Parent_Project_Code() As String
            Get
                Return Me._Parent_Project_Code
            End Get
        End Property
        Public ReadOnly Property Project_Long_Desc() As String
            Get
                Return Me._Project_Long_Desc
            End Get
        End Property
        Public ReadOnly Property Retain_Fund() As String
            Get
                Return Me._Retain_Fund
            End Get
        End Property
        Public ReadOnly Property Interest_Bearing() As String
            Get
                Return Me._Interest_Bearing
            End Get
        End Property
        Public ReadOnly Property HoldBack_Amount() As String
            Get
                Return Me._HoldBack_Amount
            End Get
        End Property
        Public ReadOnly Property RSO_Number() As String
            Get
                Return Me._RSO_Number
            End Get
        End Property
        Public ReadOnly Property CRO() As String
            Get
                Return Me._CRO
            End Get
        End Property
#End Region


#Region "Constructor"
        Public Sub New(ByRef newProjectStatus As String, _
                       ByRef newName As String, _
                       ByRef newStatus As String, _
                       ByRef newDept As String, _
                       ByRef newContactInfo As String, _
                       ByRef newEmailForEC As String, _
                       ByRef newPhone As String, _
                       ByRef newNSERCPin As String, _
                       ByRef newOtherDept As String, _
                       ByRef newInstituteCenter As String, _
                       ByRef newResearchGroup As String, _
                       ByRef newLocation As String, _
                       ByRef newGeneralPurpose As String, _
                       ByRef newPurposeOfFunds As String, _
                       ByRef newSubPurpose As String, _
                       ByRef newAwardStatusName As String, _
                       ByRef newTitle As String, _
                       ByRef newPeriod As BLL.Period, _
                       ByRef newReviewDate As String, _
                       ByRef newSponsorName As String, _
                       ByRef newReference1 As String, _
                       ByRef newReference2 As String, _
                       ByRef newPrimarySource As String, _
                       ByRef newSponsorSource As String, _
                       ByRef newProgressReport As String, _
                       ByRef newProjectHolderBilling As String, _
                       ByRef newBillingFreqDesc As String, _
                       ByRef newBillingCurrency As String, _
                       ByRef newSponsorReportingFreqDesc As String, _
                       ByRef newSponsorReportFormat As String, _
                       ByRef newOwnership As String, _
                       ByRef newHumanCertification As String, _
                       ByRef newAnimalCertification As String, _
                       ByRef newBioCertification As String)

            Me._projectStatus = newProjectStatus
            Me._name = newName
            Me._status = newStatus
            Me._dept = newDept
            Me._contactinformation = newContactInfo
            Me._emailforEC = newEmailForEC
            Me._phone = newPhone
            Me._nsercpin = newNSERCPin
            Me._otherdept = newOtherDept
            Me._institutecentre = newInstituteCenter
            Me._researchgroup = newResearchGroup
            Me._location = newLocation
            Me._generalpurpose = newGeneralPurpose
            Me._purposeoffunds = newPurposeOfFunds
            Me._subpurpose = newSubPurpose
            Me._awardstatusname = newAwardStatusName
            Me._title = newTitle
            Me._period = newPeriod
            Me._reviewdate = newReviewDate
            Me._sponsorname = newSponsorName
            Me._reference1 = newReference1
            Me._reference2 = newReference2
            Me._primarysource = newPrimarySource
            Me._sponsorsource = newSponsorSource
            Me._progressreporting = newProgressReport
            Me._projectholderbilling = newProjectHolderBilling
            Me._billingfreqdesc = newBillingFreqDesc
            Me._billingcurrency = newBillingCurrency
            Me._sponsorreportingfreqdesc = newSponsorReportingFreqDesc
            Me._sponsorreportformat = newSponsorReportFormat
            Me._ownership = newOwnership
            Me._humanCertification = newHumanCertification
            Me._animalCertification = newAnimalCertification
            Me._bioCertification = newBioCertification
        End Sub

        Public Sub New()

        End Sub
#End Region

        Public Function LoadProjectHeader(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As Boolean
            Dim dt As DataTable = DAL.Status.ProjectHeaderDB.LoadProjectHeaderDB(DBOperation, SP_Name, arrParams)

            If dt.Rows.Count <> 0 Then
                Dim dr As DataRow = dt.Rows(0)

                Me._projectStatus = dr("Project_Status").ToString.Trim
                Me._name = dr("Project_Holder_Name").ToString.Trim
                Me._status = dr("Project_Holder_Status").ToString.Trim
                Me._dept = dr("Project_Holder_Dept").ToString.Trim
                Me._contactinformation = dr("Project_Holder_Email").ToString.Trim
                Me._emailforEC = dr("Project_Holder_EmailforEC").ToString.Trim
                Me._phone = dr("Project_Holder_Phone").ToString.Trim
                Me._nsercpin = dr("NSERC_Pin").ToString.Trim
                Me._otherdept = dr("Other_Dept").ToString.Trim
                Me._institutecentre = dr("Institute_Centre").ToString.Trim
                Me._researchgroup = dr("Research_Group").ToString.Trim
                Me._location = dr("Location").ToString.Trim
                Me._generalpurpose = dr("General_Purpose").ToString.Trim
                Me._purposeoffunds = dr("Purpose_Of_Funds").ToString.Trim
                Me._subpurpose = dr("Sub_Purpose_Name").ToString.Trim
                Me._awardstatusname = dr("Award_Status_Name").ToString.Trim
                Me._title = dr("Project_Title").ToString.Trim

                Dim startDate As Date = dr("Project_Start_Date")
                Dim endDate As Date = dr("Project_End_Date")
                Dim reviewDate As Date = dr("Project_Review_Date")
                Me._period = New BLL.Period(startDate.ToString("MMM/dd/yyyy").Trim, endDate.ToString("MMM/dd/yyyy").Trim)
                Me._reviewdate = reviewDate.ToString("MMM/dd/yyyy").Trim

                Me._sponsorname = dr("Sponsor_Name").ToString.Trim
                Me._reference1 = dr("Reference_Number").ToString.Trim
                Me._reference2 = dr("Second_Reference_Number").ToString.Trim
                Me._primarysource = dr("Primary_Source").ToString.Trim
                Me._sponsorsource = dr("Sponsor_Source").ToString.Trim
                Me._progressreporting = dr("Project_Holder_Progress_Reporting").ToString.Trim
                Me._projectholderbilling = dr("Project_Holder_Billing").ToString.Trim
                Me._billingfreqdesc = dr("Billing_Freq_Desc").ToString.Trim
                Me._billingcurrency = dr("Billing_Currency").ToString.Trim
                Me._sponsorreportingfreqdesc = dr("Sponsor_Reporting_Freq_Desc").ToString.Trim
                Me._sponsorreportformat = dr("Sponsor_Reporting_Format").ToString.Trim
                Me._subreportformat = dr("Sub_Agency_Reporting_Format_Name").ToString.Trim
                Me._ownership = dr("Equipment_Ownership").ToString.Trim
                Me._humanCertification = dr("Human_Certification_YN").ToString.Trim
                Me._animalCertification = dr("Animal_Certification_YN").ToString.Trim
                Me._bioCertification = dr("Biosafety_Certification_YN").ToString.Trim
                Me._authorizedOverExpenditurePeriod = dr("Authorized_Overexpenditure_Period").ToString.Trim
                Me._authorizedOverExpenditureAmount = dr("Authorized_Overexpenditure_Amt").ToString.Trim
                Me._authorizedOEStartDate = IIf(dr("Authorized_OE_Start_Date").ToString.Trim = "", "", CType(dr("Authorized_OE_Start_Date"), Date).ToString("MMM/dd/yyyy"))
                Me._authorizedOEEndDate = IIf(dr("Authorized_OE_End_Date").ToString.Trim = "", "", CType(dr("Authorized_OE_End_Date"), Date).ToString("MMM/dd/yyyy"))

                Me._AdminName = dr("Administrator_Name").ToString.Trim
                Me._AdminEmail = dr("Administrator_eMail").ToString.Trim
                If DateTime.Parse(dr("Final_Closing_Date").ToString.Trim).Year <> 1900 Then
                    Me._FinalCloseDate = DateTime.Parse(dr("Final_Closing_Date").ToString.Trim).ToShortDateString
                End If
                Me._BillingFormat1 = dr("Sponsor_Invoice_Billing_Format_Name").ToString.Trim
                Me._BillingFormat2 = dr("Sponsor_Invoice_Billing_Format_Name_2nd").ToString.Trim
                Me._BillingFreqDesc2 = dr("Billing_Freq_Desc2").ToString.Trim
                Me._SponsorReportingFreqDesc2 = dr("Sponsor_Reporting_Freq_Desc2").ToString.Trim
                Me._ResearchType = dr("Research_Type").ToString.Trim
                Me._HumanCertNumber = dr("Human_Certification_Number").ToString.Trim
                Me._AnimalCertNumber = dr("Animal_Certification_Number").ToString.Trim
                Me._BiosafetyCertNumber = dr("Biosafety_Certification_Number").ToString.Trim
                Me._AdminPhone = dr("Administrator_Phone").ToString.Trim
                Me._ProjectFund = dr("Fund").ToString.Trim
                Me._TriCouncilSource = dr("Tri_Council_Source").ToString.Trim
                'added by Jack on April 5, 2011 for new attributes
                Me._GeneralClassification = dr("General_Classification_Desc").ToString.Trim
                Me._ProjectType = dr("Project_Type_Desc").ToString.Trim
                Me._PayrollSubType = dr("Payroll_Subtype_Desc").ToString.Trim
                Me._SecondEndDate = CType(dr("End_Date_2nd"), Date).ToString("MMM/dd/yyyy").Trim
                Me._businessUnitPC = dr("Business_Unit_PC").ToString.Trim
                '-----------------------------------
                'added by Chris Chang on June 9, 2015 for new attributes
                Me._Overhead_Amount = dr("Overhead_Amount").ToString.Trim
                Me._Parent_Project_Code = dr("Parent_Project_Code").ToString.Trim
                Me._Project_Long_Desc = dr("Project_Long_Desc").ToString.Trim

                Me._Retain_Fund = dr("Hold_Back_Applicable_YN").ToString.Trim
                Me._Interest_Bearing = dr("Interest_Bearing_YN").ToString.Trim
                Me._HoldBack_Amount = dr("HoldBack_Amount").ToString.Trim
                Me._RSO_Number = dr("RSO_App_ID").ToString.Trim
                Me._CRO = dr("CRO").ToString.Trim
                Return True
            Else
                Return False
            End If

        End Function

    End Class

End Namespace
