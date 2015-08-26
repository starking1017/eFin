Option Compare Text

Partial Class Controls_ProjectTeamMember
    Inherits System.Web.UI.UserControl
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Check to see if session expired, if expired redirected to expiration page
        If Session("RandomKey") Is Nothing Then
            HttpContext.Current.Response.Redirect(Utils.ApplicationPath.Trim + "/" + "frm_SessionExpirationMsg.aspx")
        End If

        If Not Me.IsPostBack Then

            If Session("Category") = "CP" Then

                LoadProjectTeamMembers()

            End If

        End If

    End Sub

    ' Retrieve the team members for a particular child project
    Private Sub LoadProjectTeamMembers()

        Dim dt As DataTable
        Dim arrParams As New ArrayList
        Dim strCPID As String = CType(Session("CPID"), String)

        arrParams.Add(strCPID)

        dt = BLL.ProjectTeamMembers.LoadProjectTeamMembers("SELECT", "dbo.eFinsp_LoadProjectTeamMembers_1", arrParams)

        If Not dt Is Nothing Then

            Dim dt2 As New DataTable
            dt2 = dt.Clone()
            RestructureTeamMembers(dt, dt2)

            Me.dgrdTeamMember.DataSource = dt2
            Me.dgrdTeamMember.DataBind()

        End If

    End Sub


    Private Sub RestructureTeamMembers(ByRef dt As DataTable, ByRef dt2 As DataTable)
        Dim dtIndex As Integer = 0
        Dim drOldDT As DataRow
        'Dim drNewDT As DataRow

        Dim projectCode As String = ""
        Dim memberName As String = ""
        Dim memberType As String = ""
        Dim memberContact As String = ""
        Dim memberAuth As String = ""

        Dim currentprojectCode As String = ""
        Dim currentmemberName As String = ""
        Dim currentmemberType As String = ""
        Dim currentmemberContact As String = ""
        Dim currentmemberAuth As String = ""

        Dim count As Integer = 0
        Dim drNewDT As DataRow

        drOldDT = dt.Rows(dtIndex)


        projectCode = drOldDT("Project_Code").ToString.Trim
        memberName = drOldDT("Member_Name").ToString.Trim
        memberType = drOldDT("Member_Type").ToString.Trim
        memberContact = drOldDT("Member_Contact").ToString.Trim
        memberAuth = drOldDT("Member_Authorization").ToString.Trim

        For dtIndex = 0 To dt.Rows.Count - 1
            drOldDT = dt.Rows(dtIndex)

            currentprojectCode = drOldDT("Project_Code").ToString.Trim
            currentmemberName = drOldDT("Member_Name").ToString.Trim
            currentmemberType = drOldDT("Member_Type").ToString.Trim
            currentmemberContact = drOldDT("Member_Contact").ToString.Trim
            currentmemberAuth = drOldDT("Member_Authorization").ToString.Trim

            If count = 0 Then
                dt2.ImportRow(drOldDT)
                count = count + 1
            Else
                If (memberName <> currentmemberName) Then
                    count = 1
                    ' Add for separate the different member
                    drNewDT = dt2.NewRow
                    drNewDT(0) = ""
                    drNewDT(1) = ""
                    drNewDT(2) = ""
                    drNewDT(3) = ""
                    drNewDT(4) = ""
                    dt2.Rows.Add(drNewDT)
                    
                    dt2.ImportRow(drOldDT)
                Else
                    If currentmemberAuth = "" Then
                        If (memberType <> currentmemberType) Then ' And memberContact <> currentmemberContact) Then
                            dt2.Rows(dt2.Rows.Count() - 1).Item(2) = dt2.Rows(dt2.Rows.Count() - 1).Item(2) + "<BR>" + currentmemberType
                        End If

                        If (memberContact <> currentmemberContact) Then ' And memberContact <> currentmemberContact) Then
                            dt2.Rows(dt2.Rows.Count() - 1).Item(3) = dt2.Rows(dt2.Rows.Count() - 1).Item(3) + "<BR>" + currentmemberContact
                        End If
                    Else
                        dt2.ImportRow(drOldDT)
                    End If
                End If
            End If

            projectCode = currentprojectCode
            memberName = currentmemberName
            memberType = currentmemberType
            memberContact = currentmemberContact
            memberAuth = currentmemberAuth
        Next

    End Sub

    Private Sub dgrdTeamMember_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgrdTeamMember.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
        
            Dim strMemberName As String = CType(e.Item.DataItem("Member_Name"), String)
            Dim strMemberType As String = CType(e.Item.DataItem("Member_Type"), String)
            Dim strMemberInfo As String = CType(e.Item.DataItem("Member_Contact"), String)
            Dim strMemberAuth As String = CType(e.Item.DataItem("Member_Authorization"), String)
            Dim lbl As Label
            Dim hpy As HyperLink

            If strMemberName = "" Then
                e.Item.Style("background-color") = "#FFFFFF"
                e.Item.Style("line-height") = "0px"
            End If

            If strMemberAuth = " " Then
                lbl = CType(e.Item.FindControl("lblName"), Label)
                lbl.Text = strMemberName

                hpy = CType(e.Item.FindControl("hypType"), HyperLink)
                hpy.Text = strMemberType
                hpy.NavigateUrl = "javascript:void window.open('../../memberType.html','','menubar=no,status=no,scrollbars=no,top=0,left=0,toolbar=no,width=550,height=530');"

                ' Deal with email hyperlink
                Dim str() As String = strMemberInfo.Split(CType(",", Char))

                ' Have email and phone
                If str.Length > 1 Then
                    hpy = CType(e.Item.FindControl("hypEmail"), HyperLink)
                    hpy.Text = "E-mail: " + str(0)
                    hpy.NavigateUrl = "mailto:" + str(0)
                    lbl = CType(e.Item.FindControl("lblContact"), Label)
                    lbl.Text = "Phone: " + str(1)
                Else
                    ' Only email or phone
                    If str(0).Contains("@") Then
                        ' Only email
                        hpy = CType(e.Item.FindControl("hypEmail"), HyperLink)
                        hpy.Text = "E-mail: " + str(0)
                        hpy.NavigateUrl = "mailto:" + str(0)
                        lbl = CType(e.Item.FindControl("lblContact"), Label)
                        lbl.Text = ""
                    Else
                        ' Only phone
                        If str(0).Trim <> "" Then
                            lbl = CType(e.Item.FindControl("lblContact"), Label)
                            lbl.Text = "Phone: " + str(0)
                        End If
                        hpy = CType(e.Item.FindControl("hypEmail"), HyperLink)
                        hpy.Text = ""
                    End If
                End If

                e.Item.Style("background-color") = "#CCCCCC"
                e.Item.Style("font-weight") = "bold"
                e.Item.Style("border-top") = "1px solid #000"

            Else
                ' deal with View Authorizations
                lbl = CType(e.Item.FindControl("lblName"), Label)
                lbl.Text = strMemberAuth

                lbl = CType(e.Item.FindControl("lblType"), Label)
                lbl.Text = ""

                hpy = CType(e.Item.FindControl("hypType"), HyperLink)
                hpy.Text = ""

                hpy = CType(e.Item.FindControl("hypEmail"), HyperLink)
                hpy.Text = ""

                lbl = CType(e.Item.FindControl("lblContact"), Label)
                lbl.Text = ""
            End If
        End If

    End Sub
End Class
