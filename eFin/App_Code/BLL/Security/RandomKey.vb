Namespace BLL.Security

    Public Class RandomKey


        Public Shared Function GenerateRandomKey() As String

            Return System.Guid.NewGuid.ToString.Trim


        End Function




    End Class

End Namespace
