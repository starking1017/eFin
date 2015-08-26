<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" maintainScrollPositionOnPostBack="true" AutoEventWireup="false" CodeFile="frm_ProjectEmployeeDetails.aspx.vb" Inherits="Forms_Researcher_frm_ProjectEmployeeDetails" title="eFIN Project Reporting" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../Controls/Common/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../../Controls/Common/Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommonXmlFooterMenu" Src="../../Controls/Common/XmlFooterMenu/CommonXmlFooterMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../Controls/Common/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccountsHeader" Src="../../Controls/Common/AccountsHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectChartField" Src="../../Controls/Common/ProjectChartField.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center" width="100%">
						<uc1:commonxmlfootermenu id="CommonXmlFooterMenu" runat="server"></uc1:commonxmlfootermenu><BR>
						<uc1:Header runat="server" ID="HeaderControl" />
				</TR>
				<TR>
					<TD align="center" width="100%">
						<P align="left"><FONT face="Verdana" size="2"><BR>
								<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="80%" align="center" border="0">
									<TR>
										<TD>
											<P align="left"><FONT size="2">Please note: Amounts are for the period selected.&nbsp; 
													This may not reflect the entire balance in your project.</FONT>
											</P>
										</TD>
									</TR>
								</TABLE>
							</FONT>
						</P>
						<FONT size="2" face="Verdana">
							
						<asp:Panel ID="PanelSalaryDetail" runat="server" Width="100%">
							
							<TABLE id="tabTableHeader" style="BORDER-RIGHT: #000000 0px solid; BORDER-TOP: #000000 0px solid; FONT-SIZE: x-small; BORDER-LEFT:#000000 0px solid; 
									BORDER-BOTTOM: #000000 0px solid; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse; table-layout: fixed;" borderColor="black" cellSpacing="0" cellPadding="2" 
									width="80%" align="center" border="0" text-align: left>
								<tr>
									<td align="left" colspan="2"><font face="Verdana" size="2"><strong><span style="text-decoration: underline">Salary Details</span></strong></font></td>
									<td align="right" colspan="2"><font face="Verdana" size="2">
										<asp:Button ID="btnExcel" runat="server" CssClass="btn" Text="Excel" />
										</font></td>
								</tr>
								<tr>
									<td align="left">Employee Name:
										<asp:Label ID="lblEmployeeNameS" runat="server"></asp:Label>
									</td>
									<td colspan="2">&nbsp;</td>
									<td align="right">&nbsp;</td>
								</tr>
							</table>
						   
							<font face="Verdana" size="2">
								
							<uc1:ProjectChartField ID="ProjectChartField4" runat="server" />
								
							</font>
						   
							<asp:datagrid id="dgrdEmployeesSalary" runat="server" CellPadding="3" BackColor="White" BorderWidth="1px"
							BorderStyle="None" BorderColor="#999999" Font-Names="Verdana" PageSize="24" AutoGenerateColumns="False" CssClass="GridViewStyle" AllowPaging="True" Width ="80%">
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#CCCCCC"></AlternatingItemStyle>
								<ItemStyle Font-Size="X-Small" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
								<HeaderStyle Font-Size="X-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
								BackColor="#699BCD"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="UCID">
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblUCID" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="fld_Desc" HeaderText="Description" Visible="False"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Fiscal Period">
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fld_Fiscal_Period") %>'></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblFiscalPeriod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fld_Fiscal_Period") %>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Account" DataField="fld_Acct_Code"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Account Description" DataField="fld_Acct_Desc"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Activity" DataField="fld_Activity_Code"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Reporting Comment" DataField="fld_Reporting_Comment"></asp:BoundColumn>
									<asp:BoundColumn DataField="fld_Jrnl_Line_Ref" HeaderText="Journal Ref"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Actual&lt;br&gt;($)" >
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblSalary" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Benefit&lt;br&gt;($)" Visible="False">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate >
											<asp:Label ID="lblBenefit" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Encumbrances&lt;br&gt;($)">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblEncumbrance" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Font-Size="X-Small" HorizontalAlign="Center" ForeColor="Black" Position="TopAndBottom"
								BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</asp:Panel>
							<br>
						<asp:Panel ID="PanelBenfitDetail" runat="server" Width="100%">                           
							<TABLE id="tableBenefitHeader" style="BORDER-RIGHT: #000000 0px solid; BORDER-TOP: #000000 0px solid; FONT-SIZE: x-small; BORDER-LEFT:#000000 0px solid; 
									BORDER-BOTTOM: #000000 0px solid; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse; table-layout: fixed;" borderColor="black" cellSpacing="0" cellPadding="2" 
									width="80%" align="center" border="0" text-align: left>
								<tr>
									<td align="left" colspan="3"><font face="Verdana" size="2"><strong><span style="text-decoration: underline">Benefit Details</span></strong></font></td>
								</tr>
								<tr>
									<td align="left" style="height: 32px">Employee Name: <font face="Verdana" size="2">
										<asp:Label ID="lblEmployeeNameB" runat="server" style="font-size: x-small; "></asp:Label>
										</font></td>
									<td style="height: 32px">&nbsp;</td>
									<td align="right" style="height: 32px">&nbsp;</td>
								</tr>
							</TABLE>
							
							<uc1:ProjectChartField ID="ProjectChartField5" runat="server" />
							
							<asp:datagrid id="dgrdEmployeesBenefit" runat="server" CellPadding="3" BackColor="White" BorderWidth="1px"
							BorderStyle="None" BorderColor="#999999" Font-Names="Verdana" PageSize="24" AutoGenerateColumns="False" CssClass="GridViewStyle" AllowPaging="True" Width ="80%">
								<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
								<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
								<AlternatingItemStyle BackColor="#CCCCCC"></AlternatingItemStyle>
								<ItemStyle Font-Size="X-Small" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
								<HeaderStyle Font-Size="X-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
								BackColor="#699BCD"></HeaderStyle>
							   <Columns>
									<asp:TemplateColumn HeaderText="UCID">
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblUCID" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="fld_Desc" HeaderText="Description" Visible="False"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Fiscal Period">
										<EditItemTemplate>
											<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fld_Fiscal_Period") %>'></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblFiscalPeriod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fld_Fiscal_Period") %>'></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn HeaderText="Account" DataField="fld_Acct_Code"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Account Description" DataField="fld_Acct_Desc"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Activity" DataField="fld_Activity_Code"></asp:BoundColumn>
									<asp:BoundColumn HeaderText="Reporting Comment" DataField="fld_Reporting_Comment"></asp:BoundColumn>
									<asp:BoundColumn DataField="fld_Jrnl_Line_Ref" HeaderText="Journal Ref"></asp:BoundColumn>
									<asp:TemplateColumn HeaderText="Salary&lt;br&gt;($)" Visible="False">
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblSalary" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Actual&lt;br&gt;($)">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblBenefit" runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Encumbrances&lt;br&gt;($)">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<EditItemTemplate>
											<asp:TextBox runat="server"></asp:TextBox>
										</EditItemTemplate>
										<ItemTemplate>
											<asp:Label ID="lblEncumbrance" runat="server"></asp:Label>
											<ItemStyle HorizontalAlign="Right"></ItemStyle>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Font-Size="X-Small" HorizontalAlign="Center" ForeColor="Black" Position="TopAndBottom"
								BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid>
						</asp:Panel>
						</FONT>
						<P>
							<uc1:CommonXmlFooterMenu id="CommonXmlFooterMenuControl" runat="server"></uc1:CommonXmlFooterMenu></P>
						<P>
							<uc1:Footer id="FooterControl" runat="server"></uc1:Footer></P>
					</TD>
				</TR>
			</TABLE>
</asp:Content>

