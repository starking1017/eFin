<%@ Page Language="VB" MasterPageFile="~/EfinSecurity.master" AutoEventWireup="false" CodeFile="frm_Admin_Maintain.aspx.vb" Inherits="Forms_Admin_frm_Admin_Maintain" title="administrator Maintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table width="100%" align="center">
        <tr>
            <td align="center">
                <table style="width: 80%;">
                    <tr>
                        <td align="left" bgcolor="#3366FF">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="RA Administrator Maintenance"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Panel ID="panSearch" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="btnAddNew" runat="server" CausesValidation="False" 
                                                CssClass="ButtonStyle" Font-Bold="True" Text="Add New User" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20">
                                            <asp:DataGrid ID="dgList" runat="server" AllowPaging="True" AllowSorting="True" 
                                                AutoGenerateColumns="False" CssClass="gridStyle" Width="100%" 
                                                DataKeyField="UCID">
                                                <ItemStyle CssClass="itemStyle" />
                                                <HeaderStyle CssClass="headerStyle" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="UCID" HeaderText="UCID"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FirstName" HeaderText="First Name" 
                                                        SortExpression="FirstName"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="LastName" HeaderText="Last Name" 
                                                        SortExpression="LastName"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="Department" HeaderText="Department">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="RoleName" HeaderText="Role"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Options">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" 
                                                                CommandName="Edit">[Edit]</asp:LinkButton>
                                                            &nbsp;
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" 
                                                                CommandName="Delete">[Delete]</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
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
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="10pt" 
                                    ForeColor="DarkRed" Text="Required fields are marked with *"></asp:Label>
                                <table width="70%">
                                    <tr>
                                        <td align="left" colspan="2" width="30%">
                                            <asp:Label ID="lbMessage" runat="server" Font-Bold="True" Font-Size="12pt" 
                                                ForeColor="Black" Text="Add New User"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="FONT-SIZE: 10pt">
                                        <td align="left" height="10" width="30%">
                                            <asp:Label ID="lblAddNewMsg" runat="server" CssClass="SiteDefault" 
                                                Font-Bold="True" ForeColor="Red" Visible="False">Message</asp:Label>
                                        </td>
                                        <td align="left" height="10" width="70%">
                                        </td>
                                    </tr>
                                    <tr style="FONT-SIZE: 10pt">
                                        <td align="left" class="Row2_S2" width="30%">
                                            <asp:Label ID="lbUcidTitle" runat="server" Text="UCID: *"></asp:Label>
                                        </td>
                                        <td align="left" class="Row2_S2" width="70%">
                                            <asp:TextBox ID="txtUcid" runat="server" MaxLength="10" Width="104px"></asp:TextBox>
                                            &nbsp;<asp:Button ID="btSearch" runat="server" Text="Search by UCID" Width="120px" />
                                            <asp:Label ID="lbUcid" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="Row3_S2" width="30%">
                                            <asp:Label ID="lbFirstName" runat="server" Text="Given Name: *"></asp:Label>
                                        </td>
                                        <td align="left" class="Row3_S2" width="70%">
                                            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" Width="232px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="Row2_S2" width="30%">
                                            <asp:Label ID="lbLastName" runat="server" Text="Surname: *"></asp:Label>
                                        </td>
                                        <td align="left" class="Row2_S2" width="70%">
                                            <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" Width="232px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="Row3_S2" height="25" width="30%">
                                            <asp:Label ID="lbDepartment" runat="server" Text="Department: *"></asp:Label>
                                        </td>
                                        <td align="left" class="Row3_S2" height="25" width="70%">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True" 
                                                Width="240px">
                                                <asp:ListItem Value="0">--Choose Department--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="Row2_S2" height="25" width="30%">
                                            Role:</td>
                                        <td align="left" class="Row2_S2" height="25" width="70%">
                                            <asp:RadioButtonList ID="rblRole" runat="server" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="2">Administrator</asp:ListItem>
                                                <asp:ListItem Value="1">Super User</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="Row3_S2" height="25" width="30%">
                                            Active:
                                        </td>
                                        <td align="left" class="Row3_S2" height="25" width="70%">
                                            <asp:CheckBox ID="cbActive" runat="server" Checked="True" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2" width="30%">
                                            <hr />
                                            <asp:Button ID="btSave" runat="server" Text="Save" Enabled="False" />
                                            &nbsp; &nbsp; &nbsp;
                                            <asp:Button ID="btCancel" runat="server" Text="Cancel" />
                                        </td>
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

