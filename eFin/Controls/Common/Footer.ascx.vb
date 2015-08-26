Option Compare Text

Partial Class Controls_Footer
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If
    End Sub
End Class
