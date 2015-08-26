Namespace BLL.Security

    Public Class SecuritySessionParameterObject


#Region "Add Session Parameter Object  To Session"

        Public Shared Sub AddParameterToTheSession(ByRef arrParam As ArrayList)

            If Not HttpContext.Current.Session Is Nothing Then

                HttpContext.Current.Session("SessionQueryString") = arrParam


            End If

        End Sub



        Public Shared Function GetParameterFromTheSession() As ArrayList

            Dim arrParam As ArrayList = Nothing

            If Not HttpContext.Current.Session Is Nothing Then
                arrParam = HttpContext.Current.Session("SessionQueryString")

            End If

            Return arrParam

        End Function



#End Region




    End Class


End Namespace
