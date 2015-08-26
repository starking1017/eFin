Namespace BLL

    Public Class AccountDetails

        Public Shared Function LoadAccountDetails(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable

            Return DAL.AccountDetailsDB.LoadAccountDetailsDB(DBOperation, SP_Name, arrParams)

        End Function

    End Class

End Namespace
