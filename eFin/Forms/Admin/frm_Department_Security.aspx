<%@ Page Language="VB" MasterPageFile="~/EfinSecurity.master" AutoEventWireup="false" CodeFile="frm_Department_Security.aspx.vb" Inherits="Forms_Admin_frm_Department_Security" title="Faculty Department Viewing Authorization" %>

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
                                Text="Faculty/Department Viewing Authority"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="panSearch" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="6">
                                            <asp:Label ID="lblMsg" runat="server" CssClass="SiteDefault" Font-Bold="True" 
                                                ForeColor="Maroon" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            UCID:</td>
                                        <td width="10%">
                                            <asp:TextBox ID="txtSearchUCID" runat="server" CssClass="txtBoxStyle" 
                                                Font-Bold="True" MaxLength="8" tabIndex="2" Width="200px"></asp:TextBox>
                                        </td>
                                        <td width="20%">
                                            Family Name:</td>
                                        <td width="20%">
                                            <asp:TextBox ID="txtSearchFamilyName" runat="server" CssClass="txtBoxStyle" 
                                                Font-Bold="True" tabIndex="2" Width="200px"></asp:TextBox>
                                        </td>
                                        <td width="15%">
                                            Given Name:</td>
                                        <td width="35%">
                                            <asp:TextBox ID="txtSearchGivenName" runat="server" CssClass="txtBoxStyle" 
                                                Font-Bold="True" tabIndex="3" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            Faculty:</td>
                                        <td width="10%">
                                            <asp:DropDownList ID="cboSearchFaculty" runat="server" 
                CssClass="ContentText" Font-Bold="True" Width="230px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%">
                                            Department:</td>
                                        <td width="20%">
                                            <asp:DropDownList ID="cboSearchDepartment" runat="server" 
                                                CssClass="ContentText" Font-Bold="True" Width="230px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="15%" colspan="2" style="width: 50%">
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnSearch" runat="server" 
                CausesValidation="False" CssClass="ButtonStyle" Font-Bold="True" 
                Text="Search" />
                                            &nbsp;
                                            <asp:Button ID="btnReset" runat="server" 
                CausesValidation="False" CssClass="ButtonStyle" Font-Bold="True" Text="Reset" />
                                            &nbsp;
                                            <asp:Button ID="btnAddNew" runat="server" 
                CausesValidation="False" CssClass="ButtonStyle" Font-Bold="True" 
                Text="Add New" />
                                            <asp:Label ID="lblSort" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" height="20">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" height="20">
                                            <asp:DataGrid ID="dgList" runat="server" AllowPaging="True" AllowSorting="True" 
                                                AutoGenerateColumns="False" CssClass="gridStyle" Width="100%" 
                                                PageSize="20">
                                                <ItemStyle CssClass="itemStyle" />
                                                <HeaderStyle CssClass="headerStyle" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="UCID" SortExpression="UCID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUCID" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.UCID") %>'>
				    </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="GivenName" HeaderText="Given Name" 
                                                        SortExpression="GivenName"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FamilyName" HeaderText="Family Name" 
                                                        SortExpression="FamilyName"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Faculty ID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFacultyID" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.Dept_L5_Code") %>'>
				    </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.Dept_L5_Code") %>'>
				    </asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Faculty" SortExpression="FacultyName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFaculty" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.FacultyName") %>'>
				    </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.FacultyName") %>'>
				    </asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Department ID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDepartmentID" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.Dept_ID") %>'>
				    </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox4" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.Dept_ID") %>'>
				    </asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Department" SortExpression="DepartmentName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDepartment" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.DepartmentName") %>'>
				    </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox2" runat="server" 
                                                                Text='<%# DataBinder.Eval(Container, "DataItem.DepartmentName") %>'>
				    </asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Options">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" 
                                                                CommandName="Edit" Visible="False">[Edit]</asp:LinkButton>
                                                            &nbsp;
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" 
                                                                CommandName="Delete">[Delete]</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnAddNew" runat="server" CausesValidation="False" 
                                                                CommandName="AddNew">[Add New]</asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <PagerStyle CssClass="selectedPageStyle" Mode="NumericPages" 
                                                    HorizontalAlign="Center" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="PanAddNew" runat="server" Visible="False">
                                <asp:Label ID="lblTitle" runat="server" Visible="False"></asp:Label>
                                <br />
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="10pt" 
                                    ForeColor="DarkRed" Text="Required fields are marked with *"></asp:Label>
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="6">
                                            <asp:Label ID="lblAddNewMsg" runat="server" CssClass="SiteDefault" 
                                                Font-Bold="True" ForeColor="Red" Visible="False">Message</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            UCID:</td>
                                        <td width="10%">
                                            <asp:TextBox ID="txtUCID" runat="server" CssClass="txtBoxStyle" 
                                                Font-Bold="True" MaxLength="8" tabIndex="1" Width="150px"></asp:TextBox>
                                            <asp:Button ID="btnRetrieve" runat="server" CssClass="ButtonStyle" 
                                                Text="Retrieve" />
                                        </td>
                                        <td width="20%">
                                            Family Name:</td>
                                        <td width="20%">
                                            <asp:TextBox ID="txtFamilyName" runat="server" CssClass="txtBoxStyle" 
                                                Enabled="False" Font-Bold="True" MaxLength="50" tabIndex="2" Width="200px"></asp:TextBox>
                                        </td>
                                        <td width="15%">
                                            Given Name:</td>
                                        <td width="35%">
                                            <asp:TextBox ID="txtGivenName" runat="server" CssClass="txtBoxStyle" 
                                                Enabled="False" Font-Bold="True" MaxLength="50" tabIndex="3" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            Faculty:</td>
                                        <td width="10%">
                                            <asp:DropDownList ID="cboFaculty" runat="server" CssClass="ContentText" 
                                                Font-Bold="True" tabIndex="4" Width="230px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20%">
                                            Department:</td>
                                        <td width="20%">
                                            <asp:DropDownList ID="cboDepartment" runat="server" CssClass="ContentText" 
                                                Font-Bold="True" tabIndex="5" Width="230px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="15%" colspan="2" style="width: 50%">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSave" runat="server" CausesValidation="False" 
                                                CssClass="ButtonStyle" Font-Bold="True" tabIndex="6" Text="Save" 
                                                Enabled="False" />
                                            &nbsp;
                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                                                CssClass="ButtonStyle" Font-Bold="True" tabIndex="7" Text="Cancel" />
                                            <asp:Label ID="lblEditState" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" height="20">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" height="20">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
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

