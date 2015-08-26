Option Compare Text

Partial Class EfinSecurity
    Inherits System.Web.UI.MasterPage

    Protected Sub lbtSecurity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtSecurity.Click
        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Admin/frm_Team_Security.aspx")
    End Sub

    Protected Sub lbtReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtReturn.Click
        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Researcher/frm_ProjectList.aspx")
    End Sub

    Protected Sub lbtAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtAdmin.Click
        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Admin/frm_Admin_Maintain.aspx")
    End Sub

    Protected Sub lbtDepartment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtDepartment.Click
        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Admin/frm_Department_Security.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim admin As AppUser = CType(Session("user"), AppUser)
            If Not admin.IsActive Then
                Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Researcher/frm_ProjectList.aspx")
            Else
                If admin.RoleId <> 1 Then
                    'lbtUpload.Visible = False
                    lbtAdmin.Visible = False
                    'lbtDepartment.Visible = False
                End If
            End If

        End If
    End Sub

    Protected Sub lbtPOI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtPOI.Click
        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Admin/frm_POI_Team.aspx")
    End Sub
End Class

