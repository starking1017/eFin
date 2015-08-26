<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frm_ProjectSummaryByAccount.aspx.vb" Inherits="Forms_Researcher_frm_ProjectSummaryByAccount" title="eFIN Project Reporting" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../Controls/Common/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../../Controls/Common/Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../Controls/Common/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommonXmlFooterMenu" Src="../../Controls/Common/XmlFooterMenu/CommonXmlFooterMenu.ascx" %>
<%@ Register tagprefix="uc1" tagname="ProjectChartField" src="../../Controls/Common/ProjectChartField.ascx"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD vAlign="middle" align="center">
				<uc1:commonxmlfootermenu id="CommonXmlHeaderMenu" runat="server"></uc1:commonxmlfootermenu><BR>
				<uc1:header id="HeaderControl" runat="server"></uc1:header><BR>
			</TD>
		</TR>
		<TR>
			<TD vAlign="middle" noWrap align="center">
				<P align="center"><FONT face="Verdana" size="2">The information on this screen reflects 
						the display options selected.</FONT></P>
				<div style="margin-left: auto; margin-right: auto; width: 80%; height: 30px;">
					<div id="export">
						<asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn" />
					</div>
				</div>
				<uc1:ProjectChartField ID="ProjectChartField1" runat="server" />
				<asp:datagrid id="dgProjects" runat="server" AutoGenerateColumns="False" PageSize="50" Font-Names="Verdana"
					Width="80%" BorderColor="Black" BorderWidth="1px" BackColor="White" CellPadding="3"
					HorizontalAlign="Center" CssClass="GridViewStyle">
					<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
					<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
					<AlternatingItemStyle BackColor="#EEEEEE"></AlternatingItemStyle>
					<ItemStyle Font-Size="X-Small" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
					<HeaderStyle Font-Size="X-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
						BackColor="#699BCD"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Account">
							<HeaderStyle Width="15%"></HeaderStyle>
							<ItemTemplate>
								<asp:HyperLink id="hyplnkAccount" runat="server"></asp:HyperLink>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="fld_Desc" HeaderText="Description">
							<HeaderStyle Width="25%"></HeaderStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn HeaderText="Budget&lt;br&gt;($)">
							<HeaderStyle Width="15%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblBudget" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Actual&lt;br&gt;($)">
							<HeaderStyle Width="15%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblActual" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Encumbrances&lt;br&gt;($)">
							<HeaderStyle Width="15%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblEncumbrances" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Balance&lt;br&gt;(ACT+ENC)&lt;br&gt;($)">
							<HeaderStyle Width="15%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblBalance" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
					<PagerStyle Font-Size="X-Small" HorizontalAlign="Center" ForeColor="Black" Position="TopAndBottom"
						BackColor="White" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
				<br />
				<P><uc1:commonxmlfootermenu id="CommonXmlFooterMenuControl" runat="server"></uc1:commonxmlfootermenu></P>
				<P><uc1:footer id="FooterControl" runat="server"></uc1:footer></P>
			</TD>
		</TR>
	</TABLE>
</asp:Content>

