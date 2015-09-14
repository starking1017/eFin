Option Compare Text

Partial Class Forms_Researcher_frm_ProjectDetail
    Inherits System.Web.UI.Page
    Private HardKey As String = ""


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session("UCID") Is Nothing Then
            Response.Redirect(Utils.ApplicationPath + "/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            CType(Page.Master, MasterPage).PageIdentity = "Enterprise Reporting<br/>eFIN - 03"
        End If

        Session("PageStart") = Date.Now

        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        HardKey = CType(Session("RandomKey"), String)

        If Not Me.IsPostBack Then

            Me.CommonXmlFooterMenuControl.Visible = True

            DisableStartAtAccount()

            Dim str() As String = {""}

            Dim strToDecrypted As String = Request.QueryString("value")

            If Not strToDecrypted Is Nothing Then

                Session("strToDecrypted") = strToDecrypted

                Dim objTamperProof As New BLL.Security.TamperProofQueryString64

                Dim strDecryptedResult As String = objTamperProof.QueryStringDecode(strToDecrypted, Me.HardKey)

                str = strDecryptedResult.Split("|")

            End If

            If Not Request.QueryString("action") Is Nothing AndAlso CType(Request.QueryString("action"), String).Trim = "sess" Then

                If Not Session("strToDecrypted") Is Nothing Then

                    strToDecrypted = Session("strToDecrypted")

                    Dim objTamperProof As New BLL.Security.TamperProofQueryString64

                    Dim strDecryptedResult As String = objTamperProof.QueryStringDecode(strToDecrypted, Me.HardKey)

                    str = strDecryptedResult.Split("|")


                End If

            End If

            SetSessionPanelValue(str)

        End If

    End Sub


    Private Sub SetSessionPanelValue(ByRef str() As String)

        If str.Length > 0 Then

            Select Case str.Length

                Case 2  ' SP or CP in parentChildFlag = N

                    If str(1) = "SP" Then
                        Session("SPID") = str(0)
                        Session("Category") = "SP"
                    Else
                        Session("CPID") = str(0)
                        Session("Category") = "CP"
                    End If

                Case 3 'ACT

                    Session("ACTID") = str(0)
                    Session("CPID") = str(1)
                    Session("Category") = "ACT"

                Case 4 'parentChildFlag = Y add SecurityRank

                    If str(1) = "SP" Then
                        Session("SPID") = str(0)
                        Session("Category") = "SP"
                        Session("CacheSPID") = str(2)
                        Session("SecurityRank") = str(3)
                    Else
                        Session("CPID") = str(0)
                        Session("Category") = "CP"
                        Session("ParentProject") = str(2)
                        Session("SecurityRank") = str(3)
                    End If

            End Select

        End If

    End Sub


    Private Sub DisableStartAtAccount()
        Dim pan As Panel = CType(HeaderControl.FindControl("panStartAt"), Panel)
        pan.Visible = False
    End Sub


    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        If Not Session("PageStart") Is Nothing Then

            'Me.lblResponseTime.Text = String.Format("{0}.{1}", Date.Now.Subtract(CType(Session("PageStart"), Date)).Seconds, Date.Now.Subtract(CType(Session("PageStart"), Date)).Milliseconds)

            Session.Remove("PageStart")

        End If
        ' added by David on 17 Jan 2014 to make Firefox work
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If 'end addition

        MyBase.Render(writer)

    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

        Dim dt As DataTable
        If (Not Session("dtAwardData") Is Nothing) Then
            dt = CType(Session("dtAwardData"), DataTable).Copy()
            dt.Rows.RemoveAt(dt.Rows.Count - 1)
            Dim row As DataRow = dt.NewRow()
            row.Item(0) = "-- Select Year --"
            dt.Rows.InsertAt(row, 0)
            Session("dtProjectYear") = dt

            HeaderControl.GetPHControlProjectYear().DataSource = dt
            HeaderControl.GetPHControlProjectYear().DataTextField = "fld_Year"
            HeaderControl.GetPHControlProjectYear().DataValueField = "fld_Year"
            HeaderControl.GetPHControlProjectYear().DataBind()
        End If

    End Sub
End Class
