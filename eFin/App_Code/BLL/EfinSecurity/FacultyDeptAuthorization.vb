Imports Microsoft.VisualBasic

Public Class FacultyDeptAuthorization
    Inherits AppUser
    Public ValidationMessage As String
    Public Sub New()
        'Default Constructor
    End Sub

    Public Shared Function FacultyTable() As DataTable
        Dim al As New ArrayList
        ''===================Search Parameters======================
        With al
            .Add("faculty")
            .Add("")
        End With
        Return DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_LoadFacDept_1", al)

    End Function
    Public Shared Function DepartmentTable() As DataTable
        Dim al As New ArrayList
        ''===================Search Parameters======================
        With al
            .Add("department")
            .Add("")
        End With
        Return DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_LoadFacDept_1", al)
    End Function

    Public Function Search() As DataTable
        Dim al As New ArrayList
        With al
            .Add(Me.Ucid.Trim)
            .Add(Me.LastName.Trim)
            .Add(Me.FirstName.Trim)
            .Add(Me.FacultyID.Trim)
            .Add(Me.DepartmentID.Trim)
        End With

        Dim dt As DataTable = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", _
                                "sp_eFin_Security_Search_DeptFac", al)

        dt.Columns.Add(New DataColumn("FacultyName", GetType(String)))
        dt.Columns.Add(New DataColumn("DepartmentName", GetType(String)))

        Dim dr As DataRow
        For Each dr In dt.Rows
            dr.BeginEdit()
            'If CStr(dr("Dept_L5_Code")).Trim = "ANY" Then
            '    dr("FacultyName") = "ANY"
            If CStr(dr("Dept_L5_Code")).Trim = "NA" Then
                dr("FacultyName") = "NA"
            ElseIf CStr(dr("Dept_L5_Code")).Trim = "ALL" Then
                dr("FacultyName") = "ALL"
            Else
                dr("FacultyName") = getFacultyName(CStr(dr("Dept_L5_Code")))
            End If
            'If CStr(dr("Dept_ID")).Trim = "ANY" Then
            '    dr("DepartmentName") = "ANY"
            If CStr(dr("Dept_ID")).Trim = "NA" Then
                dr("DepartmentName") = "NA"
            ElseIf CStr(dr("Dept_ID")).Trim = "ALL" Then
                dr("DepartmentName") = "ALL"
            Else
                dr("DepartmentName") = getDepartmentName(CStr(dr("Dept_ID")))
            End If
            dr.EndEdit()
        Next

        Return dt
    End Function
    Public Function getFacultyName(ByVal FacultyCode As String) As String
        Try
            Dim al As New ArrayList
            ''===================Search Parameters======================
            With al
                .Add("faculty")
                .Add(FacultyCode)
            End With
            Dim dt As DataTable = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_LoadFacDept", al)
            Dim dr As DataRow
            dr = dt.Rows(0)
            Return dr("Dept_L5_Desc")
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function getDepartmentName(ByVal DepartmentCode As String) As String
        Try
            Dim al As New ArrayList
            ''===================Search Parameters======================
            With al
                .Add("department")
                .Add(DepartmentCode)
            End With
            Dim dt As DataTable = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_LoadFacDept", al)

            Dim dr As DataRow = dt.Rows(0)

            Return dr("Dept_Desc").ToString()

        Catch ex As Exception
            Return ""
        End Try

    End Function
    Public Function DeleteeFinDepartFacAuthorization() As Boolean
        'Function delete user from the database
        Dim al As New ArrayList
        With al
            .Add(Me.Ucid.Trim)
            .Add(Me.FacultyID)
            .Add(Me.DepartmentID)

        End With

        Return DAL.GenericDBOperation.GenericDataBaseOperationDB( _
                                    "DELETE", "eFinsp_DeptSecurityDelete", al)
    End Function
    Public Function ValidateData() As String
        'function validate user input

        'check UCID
        If Me.UCID = "" Or Me.UCID = Nothing Then
            ValidationMessage = "UCID required "
            Exit Function
        End If

        'Check UCID length
        If Me.UCID.Length > 8 Or Me.UCID.Length < 8 Then
            ValidationMessage = "Invalid UCID "
            Exit Function
        End If


        If Me.DepartmentID.Trim <> "NA" And Me.FacultyID.Trim <> "NA" Then
            ValidationMessage = "Select Faculty or Department"
            Exit Function
        End If

        'added by jack on JUly 5, 2009 for mutiple ucids issue
        Dim data As HR = HR.GetPersonalInfo(Ucid)
        If data Is Nothing Then
            ValidationMessage = "Invalid UCID "
            Exit Function
        Else
            Me.LastName = data.FamilyName
            Me.FirstName = data.GivenName
        End If
        '---------------------------

        ValidationMessage = ""

    End Function
    Public Function IsExist() As Boolean
        Dim al As New ArrayList
        With al
            .Add(Me.Ucid)
            .Add("")
            .Add("")
            .Add(Me.FacultyID)
            .Add(Me.DepartmentID)
        End With

        Dim dt As DataTable
        dt = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", _
                "sp_eFin_Security_Search_DeptFac", al)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function AddNeweFinDepartFacAuthorization() As Boolean
        Dim al As New ArrayList
        With al
            .Add(Me.Ucid)
            .Add(Me.FirstName)
            .Add(Me.LastName)
            .Add(Me.FacultyID)
            .Add(Me.DepartmentID)
            .Add("NA") 'Parent Project Number 
            .Add("NA") 'Project Number 
            .Add("NA") 'Account Group
            .Add("NA") 'Account
            .Add("NA") 'Activity 
            .Add("FAC/DEPT") 'Type
        End With

        Return DAL.GenericDBOperation.GenericDataBaseOperationDB("INSERT", _
                                                                 "eFinsp_EfinSecurityInsert", al)

    End Function
End Class
