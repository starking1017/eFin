Option Compare Text
Imports System.Xml
Imports System.Xml.XPath

Partial Class Controls_Common_XmlFooterMenu_CommonXmlFooterMenu
    Inherits System.Web.UI.UserControl
    Const SP As Integer = 1
    Const CP As Integer = 2
    Const ACT As Integer = 3
    Const PROJDETAILALLACCT As Integer = 4
    Const ACTDETAILALLACCT As Integer = 5
    Const PROJECTALLACCOUNT As Integer = 6
    Const PROJECTSINGLEACCOUNT As Integer = 7
    Const ACTIVITYALLACCOUNT As Integer = 8
    Const ACTIVITYSINGLEACCOUNT As Integer = 9
    Const PROJECTEMPLOYEE As Integer = 10
    Const PROJECTEMPLOYEEDETAIL As Integer = 11

    Dim intIndent As Integer = 0
    Dim xDoc As XPathDocument
    Dim xNav As XPathNavigator


    Private Function GenerateCommandFooterDataTable() As DataTable

        Dim objTable As New DataTable("CommandFooterDataTable")
        objTable.Columns.Add("fld_menuItem", Type.GetType("System.String"))
        objTable.Columns.Add("fld_menuLink", Type.GetType("System.String"))
        objTable.Columns.Add("fld_menuTooltip", Type.GetType("System.String"))
        Return objTable

    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="strMenuXmlPath"></param>
    ''' <param name="strXPathExpression"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Bruce Yang]	6/20/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub LoadCommandFooterMenuItems(ByRef strMenuXmlPath As String, ByRef strXPathExpression As String)

        Dim nMenuItems As Integer = GetCommandFooterMenuItemCounts(strMenuXmlPath, strXPathExpression)

        Dim strXPath As String = ""

        Dim nStep As Integer = 1

        If nMenuItems > 0 Then

            Dim dt As DataTable = GenerateCommandFooterDataTable()

            While nStep <= nMenuItems  ' 0 < 2

                strXPath = String.Format(strXPathExpression + "/MenuItem" + "[" + "{0}" + "]", nStep.ToString)

                Dim nResult As Integer = GetCommandFooterMenuItems(strMenuXmlPath, strXPath, dt)

                nStep += 1

            End While

            Me.dlFooterMenu.DataSource = dt
            Me.dlFooterMenu.DataBind()

        End If

    End Sub


    Private Function GetCommandFooterMenuItems(ByRef strMenuXmlPath As String, ByRef strXPathExpression As String, ByRef dt As DataTable) As Integer

        Dim xNodeIterator As XPathNodeIterator
        Dim objRow As DataRow
        Dim strAttribute As String
        Dim intTotalNode As Integer = 0

        Dim nMenuItems As Integer = 0

        Try
            'xDoc = New XPathDocument(Server.MapPath("/eFin/controls/Common/XmlFooterMenu/" + strMenuXmlPath))

            xDoc = New XPathDocument(Server.MapPath(Utils.ApplicationPath + "/controls/Common/XmlFooterMenu/" + strMenuXmlPath))

            'Instantiate XPathNavigator
            xNav = xDoc.CreateNavigator()

            xNodeIterator = xNav.Select(strXPathExpression)

            'Iterate through the resultant node set
            While xNodeIterator.MoveNext()

                'Get the current node as XPathNavigator object
                xNav = xNodeIterator.Current

                'Fill the third column of new row
                strAttribute = ""

                If xNav.NodeType = XPathNodeType.Attribute Then

                    'objRow("attribute") = "n/a"
                    strAttribute = "N/A"

                Else

                    'Iterate through attributes if any
                    If xNav.MoveToFirstAttribute() Then

                        objRow = dt.NewRow()

                        Do

                            Select Case xNav.Name.Trim

                                Case "Name"

                                    objRow("fld_menuItem") = xNav.Value.Trim

                                Case "Link"

                                    objRow("fld_menuLink") = xNav.Value.Trim
                                Case "Tooltip"

                                    objRow("fld_menuTooltip") = xNav.Value.Trim



                            End Select


                        Loop While xNav.MoveToNextAttribute()

                        dt.Rows.Add(objRow)

                        xNav.MoveToParent()

                    End If

                End If

            End While


            Return 1

        Catch ex As Exception

            Return -1

        End Try

    End Function


    Private Function GetCommandFooterMenuItemCounts(ByRef strMenuXmlPath As String, ByRef strXPathExpression As String) As Integer

        Dim xNodeIterator As XPathNodeIterator
        Dim strAttribute As String
        Dim intTotalNode As Integer = 0

        Dim nMenuItems As Integer = 0

        Try

            'Instantiate XPathDocument and load XML document
            'xDoc = New XPathDocument(Server.MapPath("/eFin/controls/Common/XmlFooterMenu/" + strMenuXmlPath))

            xDoc = New XPathDocument(Server.MapPath(Utils.ApplicationPath + "/controls/Common/XmlFooterMenu/" + strMenuXmlPath))

            'Instantiate XPathNavigator
            xNav = xDoc.CreateNavigator()

            xNodeIterator = xNav.Select(strXPathExpression)

            'Iterate through the resultant node set
            While xNodeIterator.MoveNext()

                'Get the current node as XPathNavigator object
                xNav = xNodeIterator.Current

                strAttribute = ""
                If xNav.NodeType = XPathNodeType.Attribute Then

                    strAttribute = "N/A"

                Else

                    'Iterate through attributes if any
                    If xNav.MoveToFirstAttribute() Then

                        Do

                            If xNav.Name = "COUNT" Then

                                Try

                                    nMenuItems = Integer.Parse(xNav.Value)

                                    Return nMenuItems

                                Catch ex As Exception

                                    nMenuItems = -1

                                End Try

                            End If

                            'strAttribute += xNav.Name & ": " & xNav.Value & "<br>"

                        Loop While xNav.MoveToNextAttribute()
                        xNav.MoveToParent()

                        'objRow("attribute") = strAttribute

                    End If
                End If

            End While

        Catch ex As Exception

            Return -1

        End Try

    End Function


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not Me.IsPostBack Then

            If Not Session("Category") Is Nothing Then

                Dim strCategory As String = CType(Session("Category"), String)

                'Added By Bruce Yang, get querystring from request object
                ' based on Xml Link String, July 6, 2005 
                If Not Request.QueryString("category") Is Nothing Then

                    strCategory = CType(Request.QueryString("category"), String).Trim

                End If

                Select Case strCategory

                    Case "SP"

                        Me.LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + SP.ToString + "]/MenuItems")

                    Case "CP"

                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + CP.ToString + "]/MenuItems")

                    Case "ACT"

                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + ACT.ToString + "]/MenuItems")

                    Case "PROJDETAILALLACCT"

                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + PROJDETAILALLACCT.ToString + "]/MenuItems")


                    Case "ACTDETAILALLACCT"

                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + ACTDETAILALLACCT.ToString + "]/MenuItems")

                    Case "PROJECTALLACCOUNT"

                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + PROJECTALLACCOUNT.ToString + "]/MenuItems")

                    Case "PROJECTSINGLEACCOUNT"

                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + PROJECTSINGLEACCOUNT.ToString + "]/MenuItems")

                    Case "ACTIVITYALLACCOUNT"

                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + ACTIVITYALLACCOUNT.ToString + "]/MenuItems")

                    Case "ACTIVITYSINGLEACCOUNT"

                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + ACTIVITYSINGLEACCOUNT.ToString + "]/MenuItems")

                    Case "PROJECTEMPLOYEE"
                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + PROJECTEMPLOYEE.ToString + "]/MenuItems")

                    Case "PROJECTEMPLOYEEDETAIL"
                        LoadCommandFooterMenuItems("CommandFooterMenu.xml", "/CommandFooterMenu/CommandFooter[" + PROJECTEMPLOYEEDETAIL.ToString + "]/MenuItems")
                End Select

            End If

        End If

    End Sub

    Private Sub dlFooterMenu_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlFooterMenu.ItemDataBound

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim lnkBtn As LinkButton
            lnkBtn = CType(e.Item.FindControl("lnkMenuItem"), LinkButton)
            If Not lnkBtn Is Nothing Then

                lnkBtn.Text = CType(e.Item.DataItem("fld_menuItem"), String)

                lnkBtn.CommandArgument = IIf(IsDBNull(e.Item.DataItem("fld_menuLink")), String.Empty, e.Item.DataItem("fld_menuLink"))

                lnkBtn.ToolTip = IIf(IsDBNull(e.Item.DataItem("fld_menuTooltip")), String.Empty, e.Item.DataItem("fld_menuTooltip"))

                If lnkBtn.CommandArgument = "" Then
                    lnkBtn.Enabled = False
                Else
                    'added by Jack on September 17, 2008, control the report generation button
                    If lnkBtn.Text = "Report Generation" Then
                        'added by jack on Oct 1, 2008
                        If BLL.EfinSecurityOverride.EfinSecurityOverride Then
                            Dim arrParams As New ArrayList

                            arrParams.Add(CType(Session("UCID"), String))
                            If Not BLL.EfinSecurityOverride.ShowReportGeneration("SELECT", "dbo.eFinsp_EfinOverrideUser", arrParams) Then
                                lnkBtn.Enabled = False
                            Else
                                'open new window for report generation
                                lnkBtn.Attributes.Add("onClick", "javascript:window.open('" + lnkBtn.CommandArgument + "')")
                                '------------------------------------------------------
                            End If
                        Else
                            If Not BLL.EfinSecurityOverride.UserHasProjectLevelAccess(CType(Session("UCID"), String)) Then
                                lnkBtn.Enabled = False
                            Else
                                'open new window for report generation
                                lnkBtn.Attributes.Add("onClick", "javascript:window.open('" + lnkBtn.CommandArgument + "')")
                                '------------------------------------------------------
                            End If
                        End If

                    End If
                    '-----------------------------------------------------------
                    'lnkBtn.Attributes.Add("onClick", "javascript:window.open('" + Utils.ApplicationPath + "/frm_ProgressBar.aspx','','width=250,height=100,left=300,top=100')")
                End If
            End If


        End If

    End Sub


    Private Sub dlFooterMenu_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlFooterMenu.ItemCommand

        If CType(e.CommandArgument, String) <> "" Then
            If (e.CommandSource.text = "Report Generation") Then
                'Response.Redirect(CType(e.CommandArgument, String))
            Else
                Server.Transfer(CType(e.CommandArgument, String), False)
            End If
        End If

    End Sub

End Class
