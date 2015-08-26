<%@ Page Language="VB" MasterPageFile="~/EfinSecurity.master" AutoEventWireup="false" CodeFile="frm_Team_Security.aspx.vb" Inherits="Forms_Researcher_frm_Security" title="Project Team Based Authorization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <script type="text/javascript">
	function goback()
	{ history.back(); }

	function SubmitConfirm()
	{ if (confirm("Do you want to submit this form?")==true) return true; else return false; }
	 
	function ResetConfirm()
	{ if (confirm("Do you want to clear all data you put in?")==true) return true; else return false; }

	function CancelConfirm()
	{ if (confirm("Any Changes made on the form will not be saved.Do you want to cancel?")==true) return true; else return false; }

	function SaveConfirm()
	{ if (confirm("Do you want to save this form?")==true) return true; else return false; }

	function popUp(URL) 
	{
		day = new Date();
		id = day.getTime();
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=0,width=700,height=800,left = 290,top = 112');");
	}
    
	function doconfirm()
	{ if (confirm("Please save all changes before going to another page. Do you want to continue?")==true) return true; else return false; }
    
	function getconfirm()
	{ if (confirm("Do you want to delete this?")==true) return true; else return false; }

	function ReactivateInactivateConfirm()
	{ if (confirm("All Authorizations for this team member will be deleted. Do you want to change the status?")==true) return true; else return false; }
	
	function CheckOffDepartmentVerification(cbDepartmentVerificationID)
	{
		if (cbDepartmentVerificationID != null && cbDepartmentVerificationID != "")
			document.getElementById(cbDepartmentVerificationID).checked="";
	}

	function maxWidth(mySelect)
    {
        var maxlength = 0;
        for (var i=0; i<mySelect.options.length;i++)
        {
            if (mySelect[i].text.length > maxlength)
            {
                maxlength = mySelect[i].text.length;
            }
        }
        mySelect.style.width = maxlength * 7;
    }

    function restoreWidth(mySelect)
    { mySelect.style.width="95%"; }
    
    function accessItem(selObj)
    {
        var selectedText = selObj.options[selObj.selectedIndex].text;
        
        if (selectedText.substring(0, 10) == "(INACTIVE)")
        {
            alert("This option is inactive. Please selected another one.");
            selObj.selectedIndex = 0;
            return false;
        }
    }
</script>
    <table width="100%" align="center">
        <tr>
            <td align="center">
                <table style="width: 80%;">
                    <tr>
                        <td align="left" bgcolor="#3366FF">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Project team based authority"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                                    <table style="width:100%;" __designer:mapid="181">
                                        <tr __designer:mapid="182">
                                            <td width="100" __designer:mapid="183">
                                                Project Code:</td>
                                            <td width="40%" __designer:mapid="184">
                                                <asp:TextBox ID="txtNumber" runat="server" Width="245px" MaxLength="10"></asp:TextBox>
                                                &nbsp;
                                                <asp:Button ID="btnSearch" runat="server" CausesValidation="False" 
                                                    CssClass="ButtonStyle" Font-Bold="True" Text="Search" />
                                            </td>
                                            <td width="100" __designer:mapid="187">
                                                Project Desc:</td>
                                            <td width="35%" __designer:mapid="188">
                                                <asp:Label ID="lblProjectDesc" runat="server" Font-Bold="False"></asp:Label>
                                                <asp:Label ID="lbParent" runat="server" Text="n" Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr __designer:mapid="18a">
                                            <td colspan="4" height="20" __designer:mapid="18b">
                                                <hr __designer:mapid="18c" />
                                            </td>
                                        </tr>
                                        <tr __designer:mapid="18d">
                                            <td colspan="4" height="20" __designer:mapid="18e">
                                                <asp:Panel ID="Panel1" runat="server" Visible="False">
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" CssClass="headerStyle" Font-Bold="True" 
                                                                    ForeColor="White" Height="20px" Text="View Authorization" Visible="False" 
                                                                    Width="131px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font-weight: bold">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DataGrid ID="dgSigningAuthorities" runat="server" AllowPaging="True" 
                                                                    AllowSorting="True" AutoGenerateColumns="False" CssClass="gridStyle" 
                                                                    DataKeyField="UCID" Width="100%">
                                                                    <itemstyle CssClass="itemStyle" />
                                                                    <headerstyle CssClass="headerStyle" />
                                                                    <columns>
                                                                        <asp:TemplateColumn HeaderText="Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbType" runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Type") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Type") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:BoundColumn DataField="UCID" HeaderText="UCID"></asp:BoundColumn>
                                                                        <asp:TemplateColumn HeaderText="Given Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGivenName" runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Given_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Given_Name") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Family Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFamilyName" runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Family_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Family_Name") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Start Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbStartDate" runat="server" 
                                                                                    
                                                                                    
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Start_Date", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox runat="server" 
                                                                                    
                                                                                    
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Start_Date", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="End Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbEndDate" runat="server" 
                                                                                    
                                                                                    
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_End_Date", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox runat="server" 
                                                                                    
                                                                                    
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_End_Date", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Status" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Status" runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:templatecolumn HeaderText="Option">
                                                                            <itemstyle horizontalalign="Center" />
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lbtEdit" runat="server" CausesValidation="false" 
                                                                                    CommandName="Edit" Text="[Edit]"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <edititemtemplate>
                                                                                &nbsp;
                                                                            </edititemtemplate>
                                                                        </asp:templatecolumn>
                                                                    </columns>
                                                                    <pagerstyle CssClass="selectedPageStyle" mode="NumericPages" 
                                                                        HorizontalAlign="Center" />
                                                                </asp:DataGrid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                <asp:Panel ID="Panel2" runat="server" Visible="False">
                                    <asp:Label ID="lblViewAuthHeader" runat="server" 
    CssClass="SiteDefault" Font-Bold="True" Visible="False">lblViewiingAuthorization</asp:Label>
                                    <br />
                                    <asp:DataGrid ID="dgViewingAuth" runat="server" AutoGenerateColumns="False" 
                                        CssClass="gridStyle" ShowFooter="True" Width="100%">
                                        <footerstyle CssClass="footerStyle" />
                                        <headerstyle CssClass="headerStyle" />
                                        <columns>
                                            <asp:templatecolumn HeaderText="Account Group">
                                                <itemtemplate>
                                                    <asp:Label ID="lblAccountGroup" runat="server" 
                                                        Text='<%# DataBinder.Eval(Container, "DataItem.AccountGroup") %>'>
								</asp:Label>
                                                </itemtemplate>
                                                <footertemplate>
                                                    <asp:DropDownList ID="cboNewAccGroup" runat="server" CssClass="ContentText" 
                                                        Font-Bold="True" Width="168px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="Account Group1">Account Group1</asp:ListItem>
                                                        <asp:ListItem Value="Account Group2">Account Group2</asp:ListItem>
                                                    </asp:DropDownList>
                                                </footertemplate>
                                                <edititemtemplate>
                                                    <asp:DropDownList ID="cboEditAccGroup" runat="server" CssClass="ContentText" 
                                                        Font-Bold="True" Width="168px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="Account Group1">Account Group1</asp:ListItem>
                                                        <asp:ListItem Value="Account Group2">Account Group2</asp:ListItem>
                                                    </asp:DropDownList>
                                                </edititemtemplate>
                                            </asp:templatecolumn>
                                            <asp:templatecolumn HeaderText="Account">
                                                <itemtemplate>
                                                    <asp:Label ID="lblAccount" runat="server" 
                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Acct_Code") %>'>
								</asp:Label>
                                                </itemtemplate>
                                                <footertemplate>
                                                    <asp:TextBox ID="txtNewAccount" runat="server" CssClass="txtBoxStyle" 
                                                        Font-Bold="True" MaxLength="5" Width="168px"></asp:TextBox>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                                                        ControlToValidate="txtNewAccount" Display="Dynamic" 
                                                        ErrorMessage="Please enter 5 numbers or leave blank" 
                                                        ValidationExpression="\d{5}" ValidationGroup="31"></asp:RegularExpressionValidator>
                                                </footertemplate>
                                                <edititemtemplate>
                                                    <asp:TextBox ID="txtEditAccount" runat="server" MaxLength="5" 
                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Acct_Code") %>'></asp:TextBox>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" 
                                                        ControlToValidate="txtEditAccount" Display="Dynamic" 
                                                        ErrorMessage="Please enter 5 numbers or leave blank" 
                                                        ValidationExpression="\d{5}" ValidationGroup="30"></asp:RegularExpressionValidator>
                                                </edititemtemplate>
                                            </asp:templatecolumn>
                                            <asp:templatecolumn HeaderText="Activity">
                                                <itemtemplate>
                                                    <asp:Label ID="lblActivity" runat="server" 
                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Activity_Code") %>'>
								</asp:Label>
                                                </itemtemplate>
                                                <footertemplate>
                                                    <asp:TextBox ID="txtNewActivity" runat="server" CssClass="txtBoxStyle" 
                                                        Font-Bold="True" MaxLength="5" Width="168px"></asp:TextBox>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" 
                                                        runat="server" ControlToValidate="txtNewActivity" Display="Dynamic" 
                                                        ErrorMessage="Please enter 5 numbers or leave blank" 
                                                        ValidationExpression="\d{5}" ValidationGroup="31"></asp:RegularExpressionValidator>
                                                </footertemplate>
                                                <edititemtemplate>
                                                    <asp:TextBox ID="txtEditActivity" runat="server" MaxLength="5" 
                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Activity_Code") %>'></asp:TextBox>
                                                    <br />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                                                        ControlToValidate="txtEditActivity" Display="Dynamic" 
                                                        ErrorMessage="Please enter 5 numbers or leave blank" 
                                                        ValidationExpression="\d{5}" ValidationGroup="30"></asp:RegularExpressionValidator>
                                                </edititemtemplate>
                                            </asp:templatecolumn>
                                            <asp:templatecolumn HeaderText="Options">
                                                <headerstyle width="20%" />
                                                <itemstyle horizontalalign="Center" />
                                                <itemtemplate>
                                                    &nbsp;
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="Edit">[Edit]</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" 
                                                        CommandName="Delete">[Delete]</asp:LinkButton>
                                                </itemtemplate>
                                                <footerstyle horizontalalign="Center" />
                                                <footertemplate>
                                                    <asp:LinkButton ID="lnkbtnAddNew" runat="server" CausesValidation="False" 
                                                        CommandName="Add New" ValidationGroup="31">[Add New]</asp:LinkButton>
                                                </footertemplate>
                                                <edititemtemplate>
                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CommandName="Update" 
                                                        ValidationGroup="30">[Update]</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" 
                                                        CommandName="EditCancel">[Cancel]</asp:LinkButton>
                                                </edititemtemplate>
                                            </asp:templatecolumn>
                                        </columns>
                                        <pagerstyle CssClass="selectedPageStyle" mode="NumericPages" />
                                    </asp:DataGrid>
                            </asp:Panel>
                                    <asp:Label ID="lblViewAuthError" runat="server" CssClass="SiteDefault" 
                                        Font-Bold="True" ForeColor="Red"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</asp:Content>

