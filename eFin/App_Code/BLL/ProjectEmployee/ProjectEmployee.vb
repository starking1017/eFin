Namespace BLL

    Public Class ProjectEmployee

        Public Shared Function LoadProjectEmployee(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable

            Return DAL.ProjectEmployeeDB.LoadProjectEmployeeDB(DBOperation, SP_Name, arrParams)

        End Function

        Public Shared Function LoadProjectEmployeeDetails(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable

            Return DAL.ProjectEmployeeDB.LoadProjectEmployeeDetailsDB(DBOperation, SP_Name, arrParams)

        End Function

    End Class

End Namespace
