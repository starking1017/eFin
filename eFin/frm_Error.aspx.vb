Option Compare Text

Partial Class frm_Error
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Me.IsPostBack Then
            'CType(Page.Master, MasterPage).PageIdentity = "Enterprise Reporting<br/>eFIN - 06"

            Dim exc As Exception
            If Not Session("ErrorException") Is Nothing Then
                exc = CType(Session("ErrorException"), Exception)
                Me.lblError.Text = exc.Message
            Else
                exc = Nothing
            End If
            Session.RemoveAll()
            Session.Abandon()
            FormsAuthentication.SignOut()
        End If
    End Sub

    Private Sub lnkbtnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnLogin.Click
        Session.RemoveAll()
        Session.Abandon()
        FormsAuthentication.SignOut()
        Response.Redirect("~/frm_ProgressPage.aspx?destPage=Forms/Researcher/frm_ProjectList.aspx.aspx")
        'Dim loginURL As String = ConfigurationManager.AppSettings("CasLoginUrl")

        'Page.Response.Redirect(loginURL + "?service=" + Page.Request.Url.GetLeftPart(UriPartial.Authority) + Page.ResolveUrl("~") + "&ca.ucalgary.authent.mustpost=true ", True)

    End Sub
End Class
