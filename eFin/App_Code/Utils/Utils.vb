Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.IO
Imports System.Web.Mail


Public Class Utils

    Public Const PRODUCTION_MODE = "PRODUCTION"
    Public Const TEST_MODE = "TEST"
    ''' <summary>
    ''' Enumerate the sort direction
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SortDirection
        ASC
        DESC
    End Enum
#Region "Path's Functions"

    Public Shared ReadOnly Property ApplicationPath() As String
        Get
            If HttpContext.Current.Request.ApplicationPath.Trim = "/" Then
                Return ""
            End If
            Return HttpContext.Current.Request.ApplicationPath.Trim
        End Get
    End Property
    Public Shared ReadOnly Property ImagesPath() As String
        Get
            Return ApplicationPath() + "/Images" 'HttpContext.Current.AAR.Utils.ApplicationPath + "/Images"
        End Get
    End Property

#End Region

#Region "Authentication Server Functions"
    Public Shared Function getAuthenticationServer() As String
        Return System.Configuration.ConfigurationManager.AppSettings("UofC_AuthenticationServer")
    End Function
    Public Shared Function getAuthenticationServerPort() As String
        Return System.Configuration.ConfigurationManager.AppSettings("UofC_AuthenticationServerPort")
    End Function

    Public Shared Function GetApplicationMode() As String

        Dim result As String = System.Configuration.ConfigurationManager.AppSettings("ApplicationMode").ToUpper()

        If result Is Nothing OrElse result = "" Then
            Return "PRODUCTION"
        End If

        Return result

    End Function
#End Region

#Region "Database Connection Functions"
    '==============================================================================
    '//Database Connection Functions
    Public Shared Function getSqlConnectionString() As String
        Return getDecryptedConnectionString(System.Configuration.ConfigurationManager.AppSettings("ConnectionString"))
        'Return "Data Source=ERDBDEV11;Initial Catalog=eFinConnection;Integrated Security=True"
    End Function
    Public Shared Function getRsoSqlConnectionString() As String
        Return getDecryptedConnectionString(System.Configuration.ConfigurationManager.AppSettings("RSOConnectionString"))
    End Function
    Public Shared Function getOracleConnectionString() As String
        Return getDecryptedConnectionString(System.Configuration.ConfigurationManager.AppSettings("OracleConnectionString"))
    End Function
    Public Shared Function getUcpOracleConnectionString() As String
        Return getDecryptedConnectionString(System.Configuration.ConfigurationManager.AppSettings("UcpOracleConnectionString"))
    End Function
    Public Shared Function getSisConnectionString() As String
        Return getDecryptedConnectionString(System.Configuration.ConfigurationManager.AppSettings("SisConnectionString"))
    End Function
    '-----------------------------------------------------------------------------
    Public Shared Function getDecryptedConnectionString(ByVal connectionStrig As String) As String

        Dim data() As Byte = Convert.FromBase64String(connectionStrig)
        Dim str As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Return str

    End Function

    ' for test to get Encrypt connectionStrig
    Public Shared Function getEncryptedConnectionString(ByVal connectionStrig As String) As String
        Dim data() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(connectionStrig)
        Try
            Dim base64String As String = System.Convert.ToBase64String(data)
            Return base64String
        Catch exp As System.ArgumentNullException
            Return "Binary data array is null."
        End Try
    End Function
    '==============================================================================
#End Region

    'Public Shared Sub ExportToExcel(ByVal tbl As DataTable, ByVal filename As String)
    '    Dim gv As GridView = New GridView()
    '    gv.AllowPaging = False
    '    gv.DataSource = tbl
    '    gv.DataBind()

    '    Dim context As HttpContext = HttpContext.Current
    '    context.Response.Clear()
    '    context.Response.Buffer = True
    '    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".xls")
    '    context.Response.Charset = ""
    '    context.Response.ContentType = "application/vnd.ms-excel"

    '    Dim sw As StringWriter = New StringWriter()
    '    Dim hw As HtmlTextWriter = New HtmlTextWriter(sw)

    '    For i As Integer = 0 To gv.Rows.Count() - 1

    '        For j As Integer = 0 To gv.Rows(i).Cells.Count() - 1
    '            gv.Rows(i).Cells(j).Style.Add("mso-number-format", "\@")

    '            'If gv.Rows(i).Cells(j).Text.Contains("@") Then
    '            '    gv.Rows(i).Cells(j).Style.Add("font-style", "italic")

    '            '    Dim temp As String = gv.Rows(i).Cells(j).Text

    '            '    temp = temp.Replace("@", String.Empty)

    '            '    gv.Rows(i).Cells(j).Text = temp
    '            'End If
    '        Next
    '    Next

    '    gv.RenderControl(hw)

    '    context.Response.Output.Write(sw.ToString())
    '    context.Response.Flush()
    '    context.Response.End()
    'End Sub

    Public Shared Sub ClearControls(ByVal Ctrl As Control)
        For i As Integer = Ctrl.Controls.Count - 1 To 0 Step -1
            ClearControls(Ctrl.Controls(i))
        Next

        If TypeOf (Ctrl) Is HyperLink Then
            Dim lbl As Label = New Label()

            Ctrl.Parent.Controls.Add(lbl)

            lbl.Text = CType(Ctrl, HyperLink).Text
            CType(CType(Ctrl, HyperLink).Parent, TableCell).Style.Add("mso-number-format", "\@")
            If (CType(Ctrl, HyperLink)).NavigateUrl <> Nothing Then
                CType(CType(Ctrl, HyperLink).Parent, TableCell).Style.Add("color", "blue")
            End If
            Ctrl.Parent.Controls.Remove(Ctrl)
            Return
        End If

        'If TypeOf (Ctrl) Is LiteralControl Then
        '    Dim lit As LiteralControl = CType(Ctrl, LiteralControl)
        '    CType(CType(Ctrl, LiteralControl).Parent, TableCell).Style.Add("mso-number-format", "\@")
        'End If

        ' no define will be general and then if number will be account format unless define text
        If TypeOf (Ctrl) Is Label Then
            Dim lbl As Label = CType(Ctrl, Label)
            If (CType(CType(Ctrl, Label).Parent, TableCell).Style("mso-number-format") = Nothing And _
                IsNumeric(lbl.Text)) Then
                CType(CType(Ctrl, Label).Parent, TableCell).Style.Add("mso-number-format", "\#\,\#\#0\.00_\)\;\[Black\]\\(\#\,\#\#0\.00\\)")
            End If
            Return
        End If

        ' for expand and collapse control display
        If TypeOf (Ctrl) Is ImageButton Then
            Dim lbl As Label = New Label()
            Ctrl.Parent.Controls.Add(lbl)
            If Ctrl.ID = "imgCollapse" And Ctrl.Visible = True Then
                lbl.Text = "-"
                Ctrl.Parent.Controls.Add(lbl)
            ElseIf Ctrl.ID = "imgExpand" And Ctrl.Visible = True Then
                lbl.Text = "+"
                Ctrl.Parent.Controls.Add(lbl)
            End If
            Ctrl.Parent.Controls.Remove(Ctrl)
            Return
        End If
    End Sub


    'get the error email list
    Public Shared Function getEmailList() As String
        Return System.Configuration.ConfigurationManager.AppSettings("MailList")
    End Function


    'Generate the email message body according to the different situations
    Public Shared Function CreateEmailBody(ByVal invoiceNumber As String, ByVal supplierStaffName As String, _
        ByVal supplierName As String, ByVal PONumber As String, ByVal message As String) As String
        Dim messageBuilder As New System.Text.StringBuilder

        With messageBuilder
            .Append(" ===== University of Calgary Web Invoice System Notification ===== ")
            .Append(vbCrLf)
            .Append(vbCrLf)
            .Append("   ")
            .Append(message)

            .Append(vbCrLf)
            .Append(vbCrLf)
            .Append("   Application Number: ")
            .Append(invoiceNumber)

            .Append(vbCrLf)
            .Append("   Prepared By: ")
            .Append(supplierStaffName)
            .Append("    From: ")
            .Append(supplierName)

            If Not PONumber.Equals("") Then
                .Append(vbCrLf)
                .Append("   For Purchase Order: ")
                .Append(PONumber)
            End If

            .Append(vbCrLf)
            .Append(vbCrLf)
            .Append("   The invoice can be viewed or processed at http://my.ucalgary.ca. ")

            .Append(vbCrLf)
            .Append(vbCrLf)
            .Append(" ================================================================= ")

            .Append(vbCrLf)
            .Append(vbCrLf)
            .Append(" Note: This email is automatically generated. Please do not reply this email.")
        End With
        Return messageBuilder.ToString()
    End Function

    'Send a email with the message which is passed in
    Public Shared Sub sendEmail(ByVal emailTo As String, _
        ByVal subject As String, ByVal message As String)
        Dim email As New MailMessage

        email.Fields("http://schemas.microsoft.com/cdo/configuration/smtsperver") = EMailManager.GetEmailServer()
        email.Fields("http://schemas.microsoft.com/cdo/configuration/smtpserverport") = EMailManager.GetSmtpPort()
        email.Fields("http://schemas.microsoft.com/cdo/configuration/sendusing") = 2 '//cdoSendUsingPort
        email.Fields("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = 1
        email.Fields("http://schemas.microsoft.com/cdo/configuration/sendusername") = EMailManager.GetSendUser()
        email.Fields("http://schemas.microsoft.com/cdo/configuration/sendpassword") = EMailManager.GetSendPassword()

        email.To = emailTo
        email.From = "eFN@ucalgary.ca"
        email.Subject = subject
        email.BodyFormat = MailFormat.Html
        email.Body = message

        'SmtpMail.SmtpServer = "mailhost.ucalgary.ca"
        Try
            SmtpMail.SmtpServer = EMailManager.GetEmailServer()
            SmtpMail.Send(email)
        Catch ex As Exception
        End Try
    End Sub
End Class


Public Class EMailManager
    Public Shared Function GetEmailServer() As String
        Return System.Configuration.ConfigurationManager.AppSettings("EmailServer")
    End Function
    Public Shared Function GetSmtpPort() As String
        Return System.Configuration.ConfigurationManager.AppSettings("SmtpPort")
    End Function
    Public Shared Function GetSendUser() As String
        Dim data() As Byte = Convert.FromBase64String(System.Configuration.ConfigurationManager.AppSettings("SendUserName"))
        Dim str As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Return str
    End Function
    Public Shared Function GetSendPassword() As String
        Dim data() As Byte = Convert.FromBase64String(System.Configuration.ConfigurationManager.AppSettings("SendPassword"))
        Dim str As String = System.Text.ASCIIEncoding.ASCII.GetString(data)
        Return str
    End Function
    Public Shared Function GetMailList() As String
        Return System.Configuration.ConfigurationManager.AppSettings("MailList")
    End Function
End Class
