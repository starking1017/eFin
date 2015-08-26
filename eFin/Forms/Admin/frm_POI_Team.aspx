
<%@ Page Title="POI Team Maintenance" Language="VB" MasterPageFile="~/EfinSecurity.master" AutoEventWireup="false" CodeFile="frm_POI_Team.aspx.vb" Inherits="Forms_Admin_frm_POI_Team" %>
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
                                Text="POI Team Member Maintenance"></asp:Label>
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
                                                                <asp:Button ID="btnNew" runat="server" CssClass="ButtonStyle" Font-Bold="True" 
                                                                    Text="Add New POI Team Member" />
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
                                                                                <asp:TextBox ID="TextBox1" runat="server" 
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
                                                                                <asp:TextBox ID="TextBox2" runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Given_Name") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Family Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblFamilyName" runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Family_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="TextBox3" runat="server" 
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Member_Family_Name") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Status" runat="server" 
                                                                                    
                                                                                    
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <EditItemTemplate>
                                                                                <asp:TextBox ID="TextBox6" runat="server" 
                                                                                    
                                                                                    
                                                                                    Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:TextBox>
                                                                            </EditItemTemplate>
                                                                        </asp:TemplateColumn>
                                                                        <asp:TemplateColumn HeaderText="Option">
                                                                            <itemstyle horizontalalign="Center" />
                                                                            <itemtemplate>
                                                                                <asp:LinkButton ID="lbtActive" runat="server" CausesValidation="false" 
                                                                                    CommandName="Edit" Text="[Activate/Inactivate]"></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            <edititemtemplate>
                                                                                &nbsp;
                                                                            </edititemtemplate>
                                                                        </asp:TemplateColumn>
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
                                <asp:Panel ID="Panel2" runat="server" Visible="False" HorizontalAlign="Center">
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table border="1" width="800">
                                                    <tr>
                                                        <td bgcolor="#3366FF" colspan="4">
                                                            <asp:Label ID="Label71" runat="server" Font-Bold="True" ForeColor="White" 
                                                                Text="Add New POI Team Member"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            UCID: *</td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtUCID" runat="server" CssClass="txtBoxStyle" 
                                                                Font-Bold="True" MaxLength="8" Width="147px"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Button ID="btnRetrieve" runat="server" CssClass="ButtonStyle" 
                                                                Text="Retrieve" />
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="Label72" runat="server" CssClass="blockLabel">Family Name: *</asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFamilyName" runat="server" CssClass="txtBoxStyle" 
                                                                Font-Bold="True" MaxLength="50" ReadOnly="True" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="Label73" runat="server" CssClass="blockLabel">Given Name: *</asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtGivenName" runat="server" CssClass="txtBoxStyle" 
                                                                Font-Bold="True" MaxLength="50" ReadOnly="True" Width="150px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Button ID="btnAddNew" runat="server" Enabled="False" Font-Bold="True" 
                                                                Text="Add " Width="62px" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Font-Bold="True" 
                                                                Text="Cancel" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                            </asp:Panel>
                            &nbsp;
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

