<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frm_ProjectEmployee.aspx.vb" Inherits="Forms_Researcher_frm_ProjectEmployee" title="eFIN Project Reporting" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../Controls/Common/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccountsHeader" Src="../../Controls/Common/AccountsHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../Controls/Common/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommonXmlFooterMenu" Src="../../Controls/Common/XmlFooterMenu/CommonXmlFooterMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../../Controls/Common/Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectChartField" Src="../../Controls/Common/ProjectChartField.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD align="center" width="100%">
				<uc1:commonxmlfootermenu id="CommonXmlFooterMenu" runat="server"></uc1:commonxmlfootermenu><BR>
				<uc1:header id="HeaderControl" runat="server"></uc1:header></TD>
		</TR>
		<TR>
			<TD align="center" width="100%">
				<P align="left"><FONT face="Verdana" size="2"><BR>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="80%" align="center" border="0">
							<TR>
								<TD>
									<P align="left"><FONT size="2">Please note: Amounts are for the period selected.&nbsp; 
											This may not reflect the entire balance in your project.</FONT></P>
								</TD>
							</TR>
						</TABLE>
					</FONT>
				</P>
				<div style="margin-left: auto; margin-right: auto; width: 80%; height: 30px;">
					<div id="export">
						<asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn" />
					</div>
				</div>
				<uc1:ProjectChartField ID="ProjectChartField1" runat="server" />
				<asp:datagrid id="dgrdEmployees" runat="server" CellPadding="3" BackColor="White" BorderWidth="1px"
					BorderColor="Black" Width="80%" Font-Names="Verdana" PageSize="50" AutoGenerateColumns="False"
					AllowPaging="True" CssClass="GridViewStyle" AllowSorting="True">
					<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
					<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
					<AlternatingItemStyle BackColor="#CCCCCC"></AlternatingItemStyle>
					<ItemStyle Font-Size="X-Small" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
					<HeaderStyle Font-Size="X-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
						BackColor="#699BCD"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="UCID" Visible="False">
							<ItemTemplate>
								<asp:Label id=lblUCID runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EMPLID") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=TextBox6 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EMPLID") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Employee_Name" HeaderText="Name_old" Visible="False"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Name" SortExpression="Employee_Name">
							<ItemStyle HorizontalAlign="Left"></ItemStyle>
							<ItemTemplate>
								<asp:HyperLink ID="hyplnkEmployeeName" runat="server">HyperLink</asp:HyperLink>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" HeaderText="Activity"></asp:BoundColumn>
						<asp:BoundColumn Visible="False" HeaderText="Reference"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Salary&lt;br&gt;($)">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id=lblSalary runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Salary") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Salary") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Benefits&lt;br&gt;($)">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id=lblBenefit runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Benefit") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=TextBox2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Benefit") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Total&lt;br&gt;Sal &amp; Ben&lt;br&gt;($)">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id=lblSalBen runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Salben") %>'>
								</asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id=TextBox3 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Salben") %>'>
								</asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Encumbrances&lt;br&gt;(does not include Benefits)&lt;br&gt;($)" Visible="true">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblEncumbrance" runat="server"></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="TextBox4" runat="server"></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Total Actual &amp; Enc.&lt;br&gt;($)" Visible="False">
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblActEncum" runat="server"></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="TextBox5" runat="server"></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle Font-Size="X-Small" HorizontalAlign="Center" ForeColor="Black" Position="TopAndBottom"
						BackColor="White" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
				<P><uc1:commonxmlfootermenu id="CommonXmlFooterMenuControl" runat="server"></uc1:commonxmlfootermenu></P>
				<P><uc1:footer id="FooterControl" runat="server"></uc1:footer></P>
			</TD>
		</TR>
	</TABLE>
</asp:Content>

