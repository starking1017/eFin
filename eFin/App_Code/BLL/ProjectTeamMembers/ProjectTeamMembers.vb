Namespace BLL

    Public Class ProjectTeamMembers

        Public Shared Function LoadProjectTeamMembers(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable

            Return DAL.ProjectTeamMembersDB.LoadProjectTeamMembersDB(DBOperation, SP_Name, arrParams)

        End Function


    End Class

End Namespace
