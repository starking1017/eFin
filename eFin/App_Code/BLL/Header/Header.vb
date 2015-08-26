Imports System.Data
Namespace BLL

    Public Class Header

        Public Shared Function GetAsAtDate(ByRef DBOperation As String, ByRef SP_Name As String, ByRef arrParams As ArrayList) As DataTable
            Return DAL.FinancialSummaryDB.LoadFinancialSummaryDB(DBOperation, SP_Name, arrParams)
        End Function

        Public Shared Function GetAsAtMonth() As Integer

            Dim dt As New DataTable

            dt = GetAsAtDate("SELECT", "eFinsp_GetAsAtDate", Nothing)

            If Not dt Is Nothing Then
                Dim dr As DataRow = dt.Rows(0)
                Dim AsAtDate As Date = dr("As_At_Date")

                Return AsAtDate.Month()
            End If

        End Function


        Public Shared Function GetAsAtYear() As Integer

            Dim dt As New DataTable

            dt = GetAsAtDate("SELECT", "dbo.eFinsp_GetAsAtDate", Nothing)

            If Not dt Is Nothing Then
                Dim dr As DataRow = dt.Rows(0)
                Dim AsAtDate As Date = dr("As_At_Date")

                Return AsAtDate.Year
            End If

        End Function

        Public Shared Function GetAsAtDate() As Date

            Dim dt As New DataTable

            dt = GetAsAtDate("SELECT", "eFinsp_GetAsAtDate", Nothing)

            If Not dt Is Nothing Then
                Dim dr As DataRow = dt.Rows(0)
                Dim AsAtDate As Date = dr("As_At_Date")

                Return AsAtDate
            End If

        End Function

    End Class

End Namespace
