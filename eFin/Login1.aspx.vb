Option Compare Text

Imports System.Data.SqlClient
Imports Ca.Ucalgary.IT.CAS
Partial Class Login1
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Me.IsPostBack Then
            Session("RandomKey") = BLL.Security.RandomKey.GenerateRandomKey
        End If
        Dim casIdentity As CasIdentity = CType(Session("casIdentity"), CasIdentity)
        If casIdentity Is Nothing Then
            Dim casLoginUrl As String = ConfigurationManager.AppSettings("CasLoginUrl")
            Dim casValidateUrl As String = ConfigurationManager.AppSettings("CasValidateUrl")
            casIdentity = CasAuthenticate(casLoginUrl, casValidateUrl, Me)
            Session("casIdentity") = casIdentity
        End If

        If casIdentity IsNot Nothing Then
            ValidateUser(casIdentity.UcidList)
        Else
            Throw New Exception("Sorry, you are not authorized to access eFIN or timed out, please relogin into eFIN.")
        End If
        'my.Computer.Network.DownloadFile()
    End Sub
    Private Function CasAuthenticate(ByVal loginURL As String, ByVal validateURL As String, ByVal Page As Page) As CasIdentity
        Dim serviceURL As String = Page.Request.Url.AbsoluteUri.Split("?")(0)
        Dim ticket As String = Page.Request.QueryString("ticket")
        If ticket IsNot Nothing Then
            Try
                Dim casIdentity As CasIdentity = Ca.Ucalgary.IT.CAS.CASP.AuthenticateUC(validateURL, ticket, serviceURL)
                Return casIdentity
            Catch ex As Exception

            End Try
        End If
        'Page.Response.Redirect(loginURL + "?service=" + serviceURL, True)
        Page.Response.Redirect(loginURL + "?service=" + serviceURL + "&ca.ucalgary.authent.mustpost=true ", True)

        Return Nothing
    End Function
    Private Sub ValidateUser(ByVal ucid As ArrayList)

        Dim id As String

        For Each id In ucid
            If id <> "" Then
                If (VerifyLogin(id) = True) Then
                    ' Use security system to set the UserID within a client-side Cookie
                    Session("UCID") = id
                    If IsAdmin(id) Then
                        Session("isadmin") = "true"
                    Else
                        Session("isadmin") = "false"
                    End If
                    Exit For
                End If
            End If
        Next

        If Session("UCID") Is Nothing Then
            Dim casIdentity As CasIdentity = CType(Session("casIdentity"), CasIdentity)

            Throw New Exception("Sorry, you are not authorized to access eFIN - " + casIdentity.Principal() + ".")
        Else

            'for testing
            'Session("isadmin") = "false"
            'Session("UCID") = "04098654"
            'common the lines above out after testing
            Session("user") = New AppUser(Session("UCID"))

            'Page.Response.Redirect("~\Default.aspx")
            Page.Response.Redirect(Utils.ApplicationPath + "/frm_ProgressPage.aspx?destPage=Forms/Researcher/frm_ProjectList.aspx")

        End If

    End Sub


    Private Function VerifyLogin(ByRef strUCID As String) As Boolean
        Try
            Dim int As Integer
            Dim sqlConn As SqlConnection = New SqlConnection(Utils.getSqlConnectionString)
            sqlConn.Open()

            Dim sqlString As String = "SELECT COUNT(1) AS Expr1 FROM dbo.eFin_Security WHERE UCID = '" + strUCID + "'"
            Dim sqlComm As SqlCommand = New SqlCommand(sqlString, sqlConn)
            sqlComm.CommandTimeout = 600

            int = CType(sqlComm.ExecuteScalar(), Integer)

            sqlConn.Close()

            If int > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception("User verification failed.")
        End Try

    End Function
    Private Function IsAdmin(ByRef strUCID As String) As Boolean
        Try
            Dim int As Integer
            Dim sqlConn As SqlConnection = New SqlConnection(Utils.getSqlConnectionString)
            sqlConn.Open()

            Dim sqlString As String = "SELECT COUNT(1) AS Expr1 FROM dbo.eFin_Security WHERE UCID = '" + strUCID + "'" & _
            " AND (Dept_L5_Code = 'any') AND (Dept_ID = 'any') AND (Parent_Project_Code = 'any') AND (Project_Code = 'any') AND " & _
                      "(Acct_L3L4_Code = 'ANY ') AND (Acct_Code = 'ANY ')"
            Dim sqlComm As SqlCommand = New SqlCommand(sqlString, sqlConn)
            sqlComm.CommandTimeout = 600

            int = CType(sqlComm.ExecuteScalar(), Integer)

            sqlConn.Close()

            If int > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw New Exception("User verification failed.")
        End Try

    End Function
End Class
