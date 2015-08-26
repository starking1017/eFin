Namespace DAL
    Public Class ProjectListDB

        Public Shared Function getProjectListDB(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable

            Return DAL.GenericDBOperation.GenericDataBaseOperationDB(DBOperation, SP_Name, arrParams)

        End Function


    End Class
End Namespace

