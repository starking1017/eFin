Option Compare Text
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            lbDateTime.Text = "Generated at<br/>" + Now.ToLongDateString & " " & Now.ToShortTimeString
            If Session("UCID") Is Nothing Then
                Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
            End If
        End If
    End Sub
    Public Property PageIdentity() As String
        Get
            Return LbIdentity.Text
        End Get
        Set(ByVal value As String)
            LbIdentity.Text = value
        End Set
    End Property
End Class

