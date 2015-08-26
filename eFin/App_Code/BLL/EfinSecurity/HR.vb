Imports System.Data.SqlClient
Imports System.Data

Public Class HR

    Private _UCID As String
    Private _Initials As String
    Private _GivenName As String
    Private _FamilyName As String
    Private _RankCode As String
    Private _RankDescriptiion As String
    Private _DepartCode As String
    Private _DepartDescription As String
    Private _ErrorMsg As String
    Private _Email As String
    Private _Phone As String
    Private _FacultyID As String
    Private _FacultyName As String
    Private _ActiveStaff As String
    'added by jack on July 5, 2009 for mutiple ucids issue
    Private _OtherUcids As String
    '------------------------------------------

    Public Sub New()
    End Sub

    Public Sub New(ByVal ucid As String, _
                 ByVal givenname As String, _
                 ByVal familyname As String, _
                 ByVal departmentCode As String, _
                 ByVal departmentDescription As String, _
                 ByVal email As String, _
                 ByVal phone As String, _
                 ByVal facultyID As String, _
                 ByVal facultyName As String, _
                 ByVal activeStaffStatus As String, _
                 ByVal OtherUcids As String)

        _UCID = ucid
        _GivenName = givenname
        _FamilyName = familyname
        _DepartCode = departmentCode
        _DepartDescription = departmentDescription
        _Email = email
        _Phone = phone
        _ActiveStaff = activeStaffStatus
        'added by jack on July 5, 2009 for mutiple ucids issue
        _OtherUcids = OtherUcids
        '--------------------------------
    End Sub

    Public Property ErrorMsg() As String
        Get
            Return _ErrorMsg
        End Get
        Set(ByVal Value As String)
            _ErrorMsg = Value
        End Set
    End Property

    Public Property Initials() As String
        Get
            Return _Initials
        End Get
        Set(ByVal Value As String)
            _Initials = Value
        End Set
    End Property

    Public Property UCID() As String
        Get
            Return _UCID
        End Get
        Set(ByVal Value As String)
            _UCID = Value
        End Set
    End Property

    Public Property GivenName() As String
        Get
            Return _GivenName
        End Get
        Set(ByVal Value As String)
            _GivenName = Value
        End Set
    End Property

    Public Property FamilyName() As String
        Get
            Return _FamilyName
        End Get
        Set(ByVal Value As String)
            _FamilyName = Value
        End Set
    End Property

    Public Property RankCode() As String
        Get
            Return _RankCode
        End Get
        Set(ByVal Value As String)
            _RankCode = Value
        End Set
    End Property

    Public Property RankDescriptiion() As String
        Get
            Return _RankDescriptiion
        End Get
        Set(ByVal Value As String)
            _RankDescriptiion = Value
        End Set
    End Property

    Public Property DepartmentCode() As String
        Get
            Return _DepartCode
        End Get
        Set(ByVal Value As String)
            _DepartCode = Value
        End Set
    End Property

    Public Property DepartDescription() As String
        Get
            Return _DepartDescription
        End Get
        Set(ByVal Value As String)
            _DepartDescription = Value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal Value As String)
            _Email = Value
        End Set
    End Property

    Public Property Phone() As String
        Get
            Return _Phone
        End Get
        Set(ByVal Value As String)
            _Phone = Value
        End Set
    End Property

    Public Property FacultyID() As String
        Get
            Return _FacultyID
        End Get
        Set(ByVal Value As String)
            _FacultyID = Value
        End Set
    End Property

    Public Property FacultyName() As String
        Get
            Return _FacultyName
        End Get
        Set(ByVal Value As String)
            _FacultyName = Value
        End Set
    End Property

    Public Property ActiveStaff() As String
        Get
            Return _ActiveStaff
        End Get
        Set(ByVal Value As String)
            _ActiveStaff = Value
        End Set
    End Property
    'added by jack on July 5, 2009 for mutiple ucids issue
    Public Property OtherUcids() As String
        Get
            Return _OtherUcids
        End Get
        Set(ByVal value As String)
            _OtherUcids = value
        End Set
    End Property
    '----------------------------------------------------------------


    Public Shared Function GetPersonalInfo(ByVal ucid As String) As HR
        ' Get Trust Holder Information

        Dim StrDepart_ID, StrDepartName As String

        Dim SPARusers As HR = Nothing

        Try
            Dim al As New ArrayList
            ''===================Search Parameters======================
            With al
                .Add(ucid)
            End With
            Dim dt As DataTable = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_LoadPersonInfo", al)


            If dt.Rows.Count > 0 Then
                Dim dtr As DataRow = dt.Rows(0)
                SPARusers = New HR( _
                                                CType(dtr("Emplid"), String).Trim(), _
                                                CType(dtr("Person_First_Name"), String).Trim(), _
                                                CType(dtr("Person_Last_Name"), String).Trim(), _
                                                IIf(CType(dtr("Person_Main_Dept_ID"), String).Trim() = "", Nothing, CType(dtr("Person_Main_Dept_ID"), String).Trim()), _
                                                IIf(CType(dtr("Person_Main_Dept_Desc"), String).Trim() = "", Nothing, CType(dtr("Person_Main_Dept_Desc"), String).Trim()), _
                                                CType(dtr("Person_Email_Addr"), String).Trim(), _
                                                CType(dtr("Person_Phone_Number"), String).Trim(), _
                                                String.Empty, _
                                                String.Empty, _
                                                CType(dtr("Person_Finance_Status"), String).Trim(), _
                                                "")

            End If

            'SPARusers.ErrorMsg = Nothing

        Catch ex As Exception

            'SPARusers.ErrorMsg = "Error in Database." + ex.ToString()

        End Try

        Return SPARusers

    End Function

End Class

