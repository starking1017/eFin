Namespace Tools
    Public Class DataCleaner
        Public Sub New()

        End Sub

        Public Shared Function RemoveInvisibleCharacters(ByVal str As String) As String
            Dim myBytes() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(str)
            Dim i As Integer
            For i = 0 To myBytes.Length - 1
                If (myBytes(i) > 0 AndAlso myBytes(i) < 32) OrElse (myBytes(i) = 127) Then
                    myBytes(i) = 32
                End If
            Next

            Return System.Text.ASCIIEncoding.ASCII.GetChars(myBytes)
        End Function

        Public Shared Function ChangeSingleQuoteToDoubleQuote(ByVal TheString As String) As String
            Dim retval As Integer = InStr(TheString, "'")
            If retval > 0 Then
                Dim TempString As String = Microsoft.VisualBasic.Replace(TheString, "'", "''")
                Return TempString
            Else
                Return TheString

            End If
        End Function

        Public Shared Function ChangeSingleQuoteToEmpty(ByVal TheString As String) As String
            Dim retval As Integer = InStr(TheString, "'")
            If retval > 0 Then
                Dim TempString As String = Microsoft.VisualBasic.Replace(TheString, "'", "")
                Return TempString
            Else
                Return TheString

            End If
        End Function

    End Class


End Namespace