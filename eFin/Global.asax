<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        'HttpContext.Current.Response.ContentType = "application/xhtml+xml " '"text/html"

    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim lastError As Exception = Server.GetLastError()

            If (Not lastError Is Nothing) Then
                If Not lastError.InnerException Is Nothing Then
                    Session("ErrorException") = lastError.InnerException
                Else
                    Session("ErrorException") = lastError
                End If
            End If
            Utils.sendEmail(Utils.getEmailList, "Error from eFin application", _
                                                        String.Format("{0}{0}Error Message : " + _
                                                        "{0}{1}" + _
                                                        "{0}{0}Error Source : " + _
                                                        "{0}{2}" + _
                                                        "{0}{0}Details : " + _
                                                        "{0}{3}", _
                                                        Environment.NewLine, lastError.Message, lastError.Source, lastError.ToString()))


            Server.ClearError()
            Response.Redirect("~/frm_Error.aspx")

        Catch ex As Exception
            Response.Write("Sorry. A critical error happened. Please use <Back> button to try again. ")
        End Try
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>