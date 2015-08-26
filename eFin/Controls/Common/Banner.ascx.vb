Option Compare Text
Imports System.Web.Security

Partial Class Controls_Banner
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub lnkbtnHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnHome.Click
        'Session.RemoveAll()
        'Session.Abandon()
        'FormsAuthentication.SignOut()

        'Response.Redirect(Utils.ApplicationPath + "/Default.aspx")
        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Researcher/frm_ProjectList.aspx")
    End Sub

    Public Overrides Sub Dispose()

        GC.Collect()

    End Sub

    Private Sub lnkbtnLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnLogout.Click

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
        'Response.Redirect("http://www.ucalgary.ca") ' change by David on 12 Dec 2013 to make it work on Firefox.

        Page.RegisterClientScriptBlock("OpenNewPage", strJScript)
        'Page.RegisterStartupScript("quit", "<script language=javascript>window.opener=null;window.close();</script>")
        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "quit", "<script language=javascript>window.opener=null;window.close();</script>", True)


    End Sub
End Class
