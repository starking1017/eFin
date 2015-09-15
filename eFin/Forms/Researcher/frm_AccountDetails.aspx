<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" maintainScrollPositionOnPostBack="true" CodeFile="frm_AccountDetails.aspx.vb" Inherits="Forms_Researcher_frm_AccountDetails" title="eFIN Project Reporting" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../../Controls/Common/Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommonXmlFooterMenu" Src="../../Controls/Common/XmlFooterMenu/CommonXmlFooterMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../Controls/Common/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccountsHeader" Src="../../Controls/Common/AccountsHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectChartField" Src="../../Controls/Common/ProjectChartField.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD style="HEIGHT: 49px" align="center" width="100%"><uc1:commonxmlfootermenu id="CommonXmlHeaderMenu" runat="server"></uc1:commonxmlfootermenu><BR>
						<uc1:accountsheader id="AccountsHeaderControl" runat="server"></uc1:accountsheader></TD>
				</TR>
				<TR>
					<TD align="center" width="100%">
						<P align="left"><FONT face="Verdana" size="2">
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
						<div style="margin-left: auto; margin-right: auto; width: 80%; height: 30px; text-align: left;">
							<div id="export">
								<asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn" />
							</div>
							<asp:DropDownList ID="ddlAccount" runat="server" Visible="false" AutoPostBack="True">
							</asp:DropDownList>
						</div>
						<uc1:ProjectChartField ID="ProjectChartField1" runat="server" />
						<asp:datagrid id="dgrdAccounts" runat="server" CellPadding="3" BackColor="White" BorderWidth="1px"
							BorderColor="Black" Width="80%" Font-Names="Verdana" PageSize="50" AutoGenerateColumns="False" AllowPaging="True" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CssClass="GridViewStyle" AllowSorting="True">
							<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
							<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
							<AlternatingItemStyle BackColor="#CCCCCC"></AlternatingItemStyle>
							<ItemStyle Font-Size="X-Small" ForeColor="Black" BackColor="#EEEEEE" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left"></ItemStyle>
							<HeaderStyle Font-Size="X-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
								BackColor="#699BCD"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Supplier / Customer / Employee" SortExpression="EMPLID">
									<ItemTemplate>
										<asp:Label id="lblSupplier" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox2" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Desc" HeaderText="Description" SortExpression="Desc"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Fiscal Period" SortExpression="Fiscal_Period">
									<ItemTemplate>
										<asp:Label id="lblFiscalPeriod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Fiscal_Period") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Fiscal_Period") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Acct_Code" HeaderText="Account Code" SortExpression="Acct_Code"></asp:BoundColumn>
								<asp:BoundColumn DataField="Acct_Desc" HeaderText="Account Description" SortExpression="Acct_Desc"></asp:BoundColumn>
								<asp:BoundColumn DataField="Activity_Code" HeaderText="Activity" SortExpression="Activity_Code"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Purchase Order #" SortExpression="Purchase_Order">
									<ItemTemplate>
										<asp:Label id="lblPurchaseOrder" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Purchase_Order") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Purchase_Order") %>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Reference_Number" HeaderText="Reference" Visible="False" SortExpression="Reference_Number"></asp:BoundColumn>
								<asp:BoundColumn DataField="Reporting_ID" HeaderText="Finance Reference" SortExpression="Reporting_ID"></asp:BoundColumn>
								<asp:BoundColumn DataField="Reporting_Comment" HeaderText="Reporting Comment" SortExpression="Reporting_Comment"></asp:BoundColumn>
								<asp:BoundColumn DataField="Jounal_Line_Ref" HeaderText="Journal Line Ref" SortExpression="Jounal_Line_Ref"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Journal Date" SortExpression="Journal_Date">
									<ItemTemplate>
										<asp:Label ID="lblJournalDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Purchase_Order") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Source" HeaderText="Source" SortExpression="Source"></asp:BoundColumn>
								<asp:BoundColumn DataField="OPR_Desc" HeaderText="OPR Desc" Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Actual&lt;br&gt;($)" SortExpression="Amount">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblActual" runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox1" runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Encumbrances&lt;br&gt;($)" SortExpression="Encumbrances">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblEncumbrance" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Encumbrances") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Encumbrances") %>'>
										</asp:TextBox>
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

