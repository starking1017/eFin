Namespace BLL
    Public Class ProjectSummaryByAccount

        Public Shared Function LoadProjectSummaryByAccount(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable

            Return DAL.ProjectSummaryByAccountDB.LoadProjectSummaryByAccountDB(DBOperation, SP_Name, arrParams)

        End Function

    End Class

End Namespace
