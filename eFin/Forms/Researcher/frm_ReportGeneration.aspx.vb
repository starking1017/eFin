Option Compare Text

Partial Class Forms_Researcher_frm_ReportGeneration
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Response.Redirect(Request.UrlReferrer.ToString)
        Response.Redirect("https://ereports.ucalgary.ca/CognosXNcas")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("RandomKey") = ""
        Session("UCID") = ""
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("https://ereports.ucalgary.ca/cognosXN8/cgi-bin/cognosisapi.dll?b_action=xts.run&m=portal/report-viewer.xts&ui.action=run&ui.object=%2fcontent%2fpackage%5b%40name%3d%27PS%20Research%20and%20Trust%27%5d%2freport%5b%40name%3d%27Research%20and%20Trust%20Project%20Summary%20Report%27%5d&cv.ccurl=1&ui.backURL=%2fcognosXN8%2fcgi-bin%2fcognosisapi.dll%3fb_action%3dxts.run%26m%3dportal%2fcc.xts%26m_folder%3di0A3A9B9618484FEC836E178D1D1027D0")
    End Sub
End Class
