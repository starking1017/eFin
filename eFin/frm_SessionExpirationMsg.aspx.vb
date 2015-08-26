
Partial Class frm_SessionExpirationMsg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' This page was missing. Added by David on 16 Jan 2014
        Session.RemoveAll()
        Session.Abandon()
        FormsAuthentication.SignOut()

        Dim endStr As String = "</"
        Dim strJScript As String

        strJScript = "<script language=""JavaScript"">"
        strJScript += "<!-- " + vbCrLf
        strJScript += "window.opener = null;"
        strJScript += "window.close();"

        strJScript += "// -->"
        strJScript += endStr + "script>"
        'Response.Redirect("http://www.ucalgary.ca") ' changed by David on 12 Dec 2013 to make it work on Firefox.

        Page.RegisterClientScriptBlock("OpenNewPage", strJScript)
    End Sub
End Class
