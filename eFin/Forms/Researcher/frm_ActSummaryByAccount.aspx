<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frm_ActSummaryByAccount.aspx.vb" Inherits="Forms_Researcher_frm_ActSummaryByAccount" title="eFIN Project Reporting" %>
<%@ Register TagPrefix="uc1" TagName="CommonXmlFooterMenu" Src="../../Controls/Common/XmlFooterMenu/CommonXmlFooterMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../Controls/Common/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../../Controls/Common/Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../Controls/Common/Footer.ascx" %>

<%@ Register src="../../Controls/Common/ProjectChartField.ascx" tagname="ProjectChartField" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD vAlign="middle" align="center">
						<uc1:CommonXmlFooterMenu id="CommonXmlHeaderMenu" runat="server"></uc1:CommonXmlFooterMenu><BR>
						<uc1:Header id="HeaderControl" runat="server"></uc1:Header>
						<BR>
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
						<P>
                            <uc2:ProjectChartField ID="ProjectChartField1" runat="server" />
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
							</asp:datagrid></P>
						<P>
							<uc1:CommonXmlFooterMenu id="CommonXmlFooterMenuControl" runat="server"></uc1:CommonXmlFooterMenu></P>
						<P>
							<uc1:Footer id="FooterControl" runat="server"></uc1:Footer></P>
					</TD>
				</TR>
			</TABLE>
</asp:Content>

