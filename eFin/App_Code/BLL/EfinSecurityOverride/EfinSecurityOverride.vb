Namespace BLL
    Public Class EfinSecurityOverride


        Public Shared Function ShowReportGeneration(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As Boolean
            Dim dt As DataTable
            dt = DAL.GenericDBOperation.GenericDataBaseOperationDB(DBOperation, SP_Name, arrParams)
            If dt.Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If

        End Function


        Public Shared Function EfinSecurityOverride() As Boolean
            Dim dt As DataTable
            dt = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_EfinOverrideFlag", Nothing)
            If dt.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Shared Function UserHasProjectLevelAccess(ByVal ucid As String) As Boolean
            Dim arrParams As New ArrayList
            arrParams.Add(ucid)

            Dim dt As DataTable
            dt = DAL.GenericDBOperation.GenericDataBaseOperationDB("SELECT", "dbo.eFinsp_UserProjectLevelAccess", arrParams)
            If dt.Rows.Count > 0 Then
                Return True
                ' June 6,2015. Remove the domain check because all user go to UC, no need this anymore
                'If DomainUserExist(dt.Rows(0).Item("Person_IT_Username").ToString) Then
                '    Return True
                'Else
                '    Return False
                'End If
            Else
                Return False
            End If
        End Function

        Private Shared Function DomainUserExist(ByVal name As String) As Boolean
            Try
                Dim UserObj As Object = GetObject("WinNT://UC/" & name)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class


End Namespace
