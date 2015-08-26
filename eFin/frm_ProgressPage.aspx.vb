Option Compare Text

Partial Class frm_ProgressPage
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check to see if session expired, if expired redirected to expiration page
        If Session("UCID") Is Nothing Then
            Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
        End If
    End Sub
End Class
