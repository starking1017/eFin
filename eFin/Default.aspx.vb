Option Compare Text
Imports System
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

Imports System.Data.SqlClient
Imports Ca.Ucalgary.IT.CAS
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UCID") Is Nothing Then
            Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
        Else
            Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Researcher/frm_ProjectList.aspx")
        End If
        If Not Me.IsPostBack Then
            'Me.lblDate.Text = "LAST UPDATED AS OF " + BLL.Header.GetAsAtDate.ToLongDateString.ToUpper
            CType(Page.Master, MasterPage).PageIdentity = "Enterprise Reporting<br/>eFIN - 01"

        End If
    End Sub

    Protected Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Researcher/frm_ProjectList.aspx")
    End Sub
    Protected Sub display()
        'Response.Write("Hi")
        ' code added by david Zeng on 15 Nov 2013 to pop up the page from the database table:dbo.eFin_Splash_Page
        Try
            Dim sqlConn As SqlConnection = New SqlConnection(Utils.getSqlConnectionString)

            sqlConn.Open()
            Dim sqlString As String = "SELECT * FROM dbo.eFin_Splash_Page"
            Dim sqlComm As SqlCommand = New SqlCommand(sqlString, sqlConn)
            Dim strDisplay As String
            strDisplay = Convert.ToString(sqlComm.ExecuteScalar())
            Response.Write(strDisplay)
            sqlConn.Close()
        Catch ex As Exception
            Throw New Exception("")
        End Try


    End Sub

End Class
