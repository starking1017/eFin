Imports System
Imports System.IO
Imports System.Xml
Imports System.Text
Imports System.Security.Cryptography


Namespace BLL.Security



#Region "Class Encrypt Query String 64"


    Public Class EncryptQueryString64

        Private key() As Byte = {}
        Private IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

        Public Function Decrypt(ByVal stringToDecrypt As String, _
            ByVal sEncryptionKey As String) As String
            Dim inputByteArray(stringToDecrypt.Length) As Byte
            Try
                key = System.Text.Encoding.UTF8.GetBytes(Left(sEncryptionKey, 8))
                Dim des As New DESCryptoServiceProvider
                inputByteArray = Convert.FromBase64String(stringToDecrypt)
                Dim ms As New MemoryStream
                Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, IV), _
                    CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
                Return encoding.GetString(ms.ToArray())
            Catch e As Exception
                Return e.Message
            End Try
        End Function



        Public Function Encrypt(ByVal stringToEncrypt As String, _
            ByVal SEncryptionKey As String) As String
            Try
                key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8))
                Dim des As New DESCryptoServiceProvider
                Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes( _
                    stringToEncrypt)
                Dim ms As New MemoryStream
                Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, IV), _
                    CryptoStreamMode.Write)
                cs.Write(inputByteArray, 0, inputByteArray.Length)
                cs.FlushFinalBlock()
                Return Convert.ToBase64String(ms.ToArray())
            Catch e As Exception
                Return e.Message
            End Try
        End Function


    End Class


#End Region


#Region "Class TamperProof"

    Public Class TamperProofQueryString64


        Function TamperProofStringEncode(ByRef value As String, _
                           ByRef key As String) As String
            Dim mac3des As New System.Security.Cryptography.MACTripleDES
            Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key))
            Return Convert.ToBase64String( _
              System.Text.Encoding.UTF8.GetBytes(value)) & "-"c & _
              Convert.ToBase64String(mac3des.ComputeHash( _
              System.Text.Encoding.UTF8.GetBytes(value)))
        End Function


        'Function to decode the string
        'Throws an exception if the data is corrupt
        Function TamperProofStringDecode(ByRef value As String, _
                  ByRef key As String) As String
            Dim dataValue As String = ""
            Dim calcHash As String = ""
            Dim storedHash As String = ""

            Dim mac3des As New System.Security.Cryptography.MACTripleDES
            Dim md5 As New System.Security.Cryptography.MD5CryptoServiceProvider
            mac3des.Key = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key.Trim))

            Try
                dataValue = System.Text.Encoding.UTF8.GetString( _
                        Convert.FromBase64String(value.Split("-"c)(0)))

                'storedHash = System.Text.Encoding.UTF8.GetString( _
                '    Convert.FromBase64String(value.Trim.Split("-"c)(1)))

                'If Not (System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(value.Trim.Split("-"c)(1))) = Nothing) Then

                '    storedHash = System.Text.Encoding.UTF8.GetString( _
                '        Convert.FromBase64String(value.Trim.Split("-"c)(1)))



                'End If


                'storedHash = System.Text.Encoding.UTF8.GetString( _
                '    Convert.FromBase64String(value.Trim.Split("-"c)(1)))


                'calcHash = System.Text.Encoding.UTF8.GetString( _
                '  mac3des.ComputeHash(System.Text.Encoding.UTF8.GetBytes(dataValue)))

                'If storedHash <> calcHash Then
                '    'Data was corrupted

                '    'Throw New ArgumentException("Hash value does not match")
                '    Throw New ArgumentException("Error value!")

                '    'This error is immediately caught below
                'End If
            Catch ex As Exception

                'Throw New ArgumentException("Invalid TamperProofString")
                Throw New ArgumentException("Error Value!")

            End Try

            Return dataValue

        End Function




        Function QueryStringEncode(ByRef value As String, ByRef TamperProofKey As String) As String

            Return HttpUtility.UrlEncode(TamperProofStringEncode(value, TamperProofKey))

        End Function

        Function QueryStringDecode(ByRef value As String, ByRef TamperProofKey As String) As String

            Return HttpUtility.UrlDecode(TamperProofStringDecode(value, TamperProofKey))

        End Function




        'Function QueryStringEncode(ByRef value As String, ByRef TamperProofKey As String) As String

        '    Return TamperProofStringEncode(value.Trim, TamperProofKey.Trim)

        'End Function

        'Function QueryStringDecode(ByRef value As String, ByRef TamperProofKey As String) As String

        '    Return TamperProofStringDecode(value.Trim, TamperProofKey.Trim)

        'End Function



    End Class



#End Region




End Namespace

