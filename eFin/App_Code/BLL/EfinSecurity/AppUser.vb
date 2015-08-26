Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class AppUser
    Private fUcid As String = ""
    Private fLastName As String = ""
    Private fFirstName As String = ""
    Private fRoleID As Integer = 0
    Private fRoleName As String = ""
    Private fDepartmentID As String = ""
    Private fDepartment As String = ""
    Private fFacultyID As String = ""
    Private fFaculty As String = ""
    Private fActive As Boolean = False
#Region " Properties"
    ''' <summary>
    ''' Get and set User Ucid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Ucid() As String
        Get
            Return fUcid
        End Get
        Set(ByVal value As String)
            fUcid = value
        End Set
    End Property
    ''' <summary>
    ''' Get and set User Last name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastName() As String
        Get
            Return fLastName
        End Get
        Set(ByVal value As String)
            fLastName = value
        End Set
    End Property
    ''' <summary>
    ''' Get and set user first name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FirstName() As String
        Get
            Return fFirstName
        End Get
        Set(ByVal value As String)
            fFirstName = value
        End Set
    End Property
    Public Property RoleId() As Integer
        Get
            Return fRoleID
        End Get
        Set(ByVal value As Integer)
            fRoleID = value
        End Set
    End Property
    Public Property RoleName() As String
        Get
            Return fRoleName
        End Get
        Set(ByVal value As String)
            fRoleName = value
        End Set
    End Property
    Public Property DepartmentID() As String
        Get
            Return fDepartmentID
        End Get
        Set(ByVal value As String)
            fDepartmentID = value
        End Set
    End Property
    ''' <summary>
    ''' Get and set user's department
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Department() As String
        Get
            Return fDepartment
        End Get
        Set(ByVal value As String)
            fDepartment = value
        End Set
    End Property
    Public Property FacultyID() As String
        Get
            Return fFacultyID
        End Get
        Set(ByVal value As String)
            fFacultyID = value
        End Set
    End Property
    Public Property Faculty() As String
        Get
            Return fFaculty
        End Get
        Set(ByVal value As String)
            fFaculty = value
        End Set
    End Property
    ''' <summary>
    ''' get and set if user is active
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsActive() As Boolean
        Get
            Return fActive
        End Get
        Set(ByVal value As Boolean)
            fActive = value
        End Set
    End Property


    ''' <summary>
    ''' Get the full name of the user
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FullName() As String
        Get
            Return FirstName & " " & LastName
        End Get
    End Property
#End Region
#Region " Constructors"
    Public Sub New()

    End Sub

    Public Sub New(ByVal pUcid As String)
        Dim dt As DataTable
        Dim arrParams As New ArrayList
        arrParams.Add(pUcid)

        dt = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_GetAdmin", arrParams)
        If dt.Rows.Count > 0 Then
            fUcid = dt.Rows(0)("fld_UserID").ToString
            fLastName = dt.Rows(0)("fld_FamilyName").ToString
            fFirstName = dt.Rows(0)("fld_GivenName").ToString
            fRoleID = dt.Rows(0)("fld_RoleID")
            fRoleName = dt.Rows(0)("fld_RoleName").ToString
            fDepartmentID = dt.Rows(0)("fld_DepartmentID").ToString
            fDepartment = dt.Rows(0)("fld_DepartmentName").ToString
            fActive = dt.Rows(0)("fld_Active")
        End If


    End Sub

    Public Shared Function GetAllAdmins() As List(Of AppUser)
        Dim users As New List(Of AppUser)
        Dim fUser As AppUser
        Dim dt As DataTable
        Dim arrParams As New ArrayList
        arrParams.Add("all")

        dt = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_GetAdmin", arrParams)
        For Each dr As DataRow In dt.Rows
            fUser = New AppUser
            With fUser
                .Ucid = dr("fld_UserID").ToString
                .LastName = dr("fld_FamilyName").ToString
                .FirstName = dr("fld_GivenName").ToString
                .RoleId = dr("fld_RoleID")
                .RoleName = dr("fld_RoleName").ToString
                .DepartmentID = dr("fld_DepartmentID").ToString
                .Department = dr("fld_DepartmentName").ToString
                .IsActive = dr("fld_Active")
            End With
            users.Add(fUser)
        Next
        Return users
    End Function
    Public Sub RemoveMe()
        Dim arrParams As New ArrayList
        arrParams.Add(Me.Ucid)

        DAL.GenericDBOperation.GenericDataBaseOperationDB("DELETE", "dbo.eFinsp_DeleteAdmin", arrParams)

    End Sub

    Public Sub SaveMe()
        Dim arrParams As New ArrayList
        arrParams.Add(Me.Ucid)
        arrParams.Add(Me.LastName)
        arrParams.Add(Me.FirstName)
        arrParams.Add(Me.RoleId)
        arrParams.Add(Me.RoleName)
        arrParams.Add(Me.DepartmentID)
        arrParams.Add(Me.Department)
        arrParams.Add(Me.IsActive)
        DAL.GenericDBOperation.GenericDataBaseOperationDB("DELETE", "dbo.eFinsp_EditAdmin", arrParams)

    End Sub
#End Region
End Class
''' <summary>
''' This class is used to sort DDUser
''' </summary>
''' <remarks></remarks>
Public Class AppUserComparer
    Implements IComparer(Of AppUser)

    Private _sortColumn As String
    Private _sortDirection As Utils.SortDirection
    Public Sub New()

    End Sub
    Public Sub New(ByVal sortColumn As String, ByVal sortDirection As Utils.SortDirection)
        _sortColumn = sortColumn
        _sortDirection = sortDirection
    End Sub

    Public Function Compare(ByVal x As AppUser, ByVal y As AppUser) As Integer Implements System.Collections.Generic.IComparer(Of AppUser).Compare
        Dim result As Integer = 0
        Dim reverseDirection As Integer = 1
        If _sortDirection = Utils.SortDirection.DESC Then
            reverseDirection = -1
        End If
        Select Case _sortColumn
            Case "Ucid"
                result = String.Compare(x.Ucid, y.Ucid, True)
            Case "FirstName"
                result = String.Compare(x.FirstName, y.FirstName, True)
            Case "LastName"
                result = String.Compare(x.LastName, y.LastName, True)
            Case "Department"
                result = String.Compare(x.Department, y.Department, True)
        End Select
        Return result * reverseDirection
    End Function
End Class