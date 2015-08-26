Namespace BLL
    Public Class ProjectList


        Public Shared Function getProjectList(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable

            Return DAL.ProjectListDB.getProjectListDB(DBOperation, SP_Name, arrParams)

        End Function



    End Class

    
    End Namespace
