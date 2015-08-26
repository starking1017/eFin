Imports System.Data
Namespace BLL.Status

    Public Class SummaryProjectHeader

#Region "Member variables"
        Private _projectStatus As String

        ''' Summary Project Holder Row 1
        Private _name As String
        Private _projectHolderStatus As String
        Private _dept As String
        Private _contactinformation As String
        Private _emailforEC As String
        Private _phone As String

        ''' Summary Project Title Row 2
        Private _title As String

        ''' Summary Project Effective Dates Row 3
        Private _period As BLL.Period
        Private _reviewdate As String

        ''' Agency Reproting
        Private _agencyReporting As String
        Private _reportFormat As String
        Private _agencyReporting2 As String
        Private _reportFormat2 As String


#End Region


#Region "Public Property"
        Public ReadOnly Property ProjectStatus() As String
            Get
                Return Me._projectStatus
            End Get
        End Property

        Public ReadOnly Property Name() As String

            Get
                Return Me._name
            End Get

        End Property


        Public ReadOnly Property ProjectHolderStatus() As String
            Get
                Return Me._projectHolderStatus
            End Get
        End Property


        Public ReadOnly Property Dept() As String
            Get
                Return Me._dept
            End Get
        End Property


        Public ReadOnly Property Contactinformation() As String
            Get

                Return Me._contactinformation

            End Get
        End Property


        Public ReadOnly Property EmailForEC() As String
            Get
                Return Me._emailforEC
            End Get
        End Property


        Public ReadOnly Property Phone() As String
            Get
                Return Me._phone
            End Get
        End Property


        Public ReadOnly Property Title() As String
            Get
                Return Me._title
            End Get
        End Property


        Public ReadOnly Property Period() As BLL.Period
            Get
                Return Me._period
            End Get
        End Property


        Public ReadOnly Property ReviewDate() As String
            Get
                Return Me._reviewdate
            End Get
        End Property


        Public ReadOnly Property AgencyReporting() As String
            Get
                Return Me._agencyReporting
            End Get
        End Property


        Public ReadOnly Property ReportFormat() As String
            Get
                Return Me._reportFormat
            End Get
        End Property
        Public ReadOnly Property AgencyReporting2() As String
            Get
                Return Me._agencyReporting2
            End Get
        End Property


        Public ReadOnly Property ReportFormat2() As String
            Get
                Return Me._reportFormat2
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New(ByRef projectStatus As String, _
                       ByRef name As String, _
                       ByRef projectholderstatus As String, _
                       ByRef dept As String, _
                       ByRef contactinformation As String, _
                       ByRef emailforEC As String, _
                       ByRef phone As String, _
                       ByRef title As String, _
                       ByRef Period As BLL.Period, _
                       ByRef reviewdate As String, _
                       ByRef agencyreporting As String, _
                       ByRef reportformat As String)

            Me._projectStatus = projectStatus
            Me._name = name
            Me._projectHolderStatus = projectholderstatus
            Me._dept = dept
            Me._contactinformation = contactinformation
            Me._emailforEC = emailforEC
            Me._phone = phone
            Me._title = title
            Me._period = Period
            Me._reviewdate = reviewdate
            Me._agencyReporting = agencyreporting
            Me._reportFormat = reportformat

        End Sub

        Public Sub New()

        End Sub
#End Region

        Public Function LoadSummaryProjectHeader(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As Boolean
            Dim dt As DataTable = DAL.Status.SummaryProjectHeaderDB.LoadSummaryProjectHeaderDB(DBOperation, SP_Name, arrParams)

            If dt.Rows.Count <> 0 Then
                Dim dr As DataRow = dt.Rows(0)

                Me._projectStatus = dr("Project_Status").ToString.Trim

                ' Summary Project Holder Row 1
                Me._name = dr("Project_Holder_Name").ToString.Trim
                Me._projectHolderStatus = dr("Project_Holder_Status").ToString.Trim
                Me._dept = dr("Project_Holder_Dept").ToString.Trim
                Me._contactinformation = dr("Project_Holder_Email").ToString.Trim
                Me._emailforEC = dr("Project_Holder_EmailforEC").ToString.Trim
                Me._phone = dr("Project_Holder_Phone").ToString.Trim

                ' Summary Project Holder Row 2
                Me._title = dr("Project_Title").ToString.Trim

                ' Summary Project Holder Row 3
                Dim startDate As Date = dr("Project_Start_Date")
                Dim endDate As Date = dr("Project_End_Date")
                Dim reviewDate As Date = dr("Project_Review_Date")
                Me._period = New BLL.Period(startDate.ToString("MMM/dd/yyyy").Trim, endDate.ToString("MMM/dd/yyyy").Trim)
                Me._reviewdate = reviewDate.ToString("MMM/dd/yyyy").Trim

                'If dr("Other_Finance_Statements").ToString.Trim <> "" Then
                '    Me._agencyReporting = dr("Finance_Statements").ToString.Trim + "/" + dr("Other_Finance_Statements").ToString.Trim
                'Else
                '    Me._agencyReporting = dr("Finance_Statements").ToString.Trim
                'End If
                Me._agencyReporting = dr("Sponsor_Reporting_Freq_Desc")
                Me._agencyReporting2 = dr("Sponsor_Reporting_Freq_Desc2")
                Me._reportFormat = dr("Sponsor_Reporting_Format")
                Me._reportFormat2 = dr("Sub_Agency_Reporting_Format_Name")

                Return True
            Else
                Return False
            End If

        End Function

    End Class

End Namespace

