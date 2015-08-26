<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frm_ProjectList.aspx.vb" Inherits="Forms_Researcher_frm_ProjectList" title="eFIN Project Reporting" %>

<%@ Register Src="../../Controls/Common/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../../Controls/Common/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<%@ Reference Control="~/Controls/Common/Header.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD vAlign="middle" align="center">
				<table width="80%">
					<tr>
						<!-- comment out by David Zeng on 20 Nov 2013
						<td align="left">
							<asp:LinkButton CausesValidation="False" Font-Names="Verdana" 
								Font-Size="X-Small" ID="lbSecurity" runat="server">Maintain eFIN Security</asp:LinkButton>
						</td>
						<td>
						</td>-->
						<td align="left">
							<asp:LinkButton CausesValidation="False" 
								CommandArgument="https://ereports2.ucalgary.ca/cognosUNcas/login.aspx?b_action=cognosViewer&amp;ui.action=run&ui.object=%2fcontent%2fpackage%5b%40name%3d%27PS%20Research%20and%20Trust%27%5d%2freport%5b%40name%3d%27Research%20Accounting%20Reports%20Menu%27%5d&ui.name=Research%20Accounting%20Reports%20Menu&run.outputFormat=&run.prompt=true" 
								Font-Names="Verdana" Font-Size="X-Small" Font-Bold="true" ID="lbGenerateReport"
								 runat="server" ToolTip="Showing reports list menu">Report Generation</asp:LinkButton>
						</td>
					</tr>
				</table>
				<br />
				<uc1:Header ID="HeaderControl" runat="server" />
			</TD>
		</TR>
		<TR>
			<TD vAlign="middle" noWrap>
				<table style="width:100%;">
					<tr>
						<td align="center">
							<asp:Panel ID="Panel1" runat="server" Visible="False">
						     <br />
				                <table style="width: 80%;" border="1" cellspacing="0">
					                <tr >
						                <td align="left" >
											<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" 
												Font-Size="X-Small" Text="Optional columns of the list"></asp:Label>
											</td>
						                <td align="left">
							                <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
								                RepeatDirection="Horizontal" Font-Names="Verdana" Font-Size="X-Small">
								                <asp:ListItem>Project Type</asp:ListItem>
								                <asp:ListItem>General Classification</asp:ListItem>
								                <asp:ListItem>Purpose of Funds</asp:ListItem>
								                <asp:ListItem>Payroll Sub-Type</asp:ListItem>
								                <asp:ListItem>Sponsor</asp:ListItem>
							                </asp:CheckBoxList>
							
						                </td>
						                <td>
							                <asp:linkbutton id="lnkBtnRefresh" runat="server" Font-Size="10" Font-Bold="true" ToolTip="Retrieve the information for selected time periord">Refresh</asp:linkbutton>
						                </td>
					                </tr>
				                </table>
			                </asp:Panel>
						</td>
					</tr>
				</table>
				<div style="margin-left: auto; margin-right: auto; width: 80%; height: 30px;">
					<div id="export">
						<asp:Button ID="btnExcel" runat="server" Text="Excel" CssClass="btn" Visible="False" />
					</div>
				</div>	
				<!--- change added to GridLines=="both"-->					
				 <asp:datagrid id="dgProjects" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="300"
					Font-Names="Verdana" Width="80%" BorderColor="Black" BorderWidth="1px"
					BackColor="White" CellPadding="3" HorizontalAlign="Center" cssclass="GridViewStyle" GridLines="Both">
					<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
					<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
					<AlternatingItemStyle BackColor="#EEEEEE"></AlternatingItemStyle>
					<ItemStyle Font-Size="X-Small" ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
					<HeaderStyle Font-Size="X-Small" Font-Bold="True" HorizontalAlign="Center" ForeColor="White"
						BackColor="#699BCD"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Project">
							<HeaderStyle Width="13%"></HeaderStyle>
							<ItemTemplate>
								<asp:HyperLink id="hyplnkProject" runat="server"></asp:HyperLink>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Project Type">
							<ItemTemplate>
								<asp:Label ID="lblProjectType" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Activity">
							<HeaderStyle></HeaderStyle>
							<ItemTemplate>
								<asp:HyperLink id="hyplnkActivity" runat="server"></asp:HyperLink>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Description">
							<HeaderStyle></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblDescription" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Other Attributes">
							<ItemTemplate>
								<asp:Label ID="lblOtherAttributes" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Reference">
							<HeaderStyle></HeaderStyle>
							<ItemTemplate>
								<asp:Label id="lblReference" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Balance&lt;br&gt;(ACT+ENC)&lt;br&gt;($)">
							<HeaderStyle Width="14%"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblBalance" runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn Visible="False" DataField="fld_Project_Code" HeaderText="Project Code"></asp:BoundColumn>
					</Columns>
					<PagerStyle Font-Size="X-Small" HorizontalAlign="Center" ForeColor="Black" Position="TopAndBottom"
						BackColor="White" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
				<P>
					<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="80%" border="0" align="center">
						<TR>
							<TD align="center">
								<P align="left"><FONT face="Verdana" size="2">NOTES:</FONT></P>
								<BLOCKQUOTE dir="ltr" style="MARGIN-RIGHT: 0px"><P align="left"><FONT face="Verdana" size="2">1. 
											N/A appears when you do not have full access to a project.</FONT></P>
								</BLOCKQUOTE>
								<p align="left"><FONT face="Verdana" size="2">Costs for salary related benefits have not been committed. Please allow for 
										these costs where applicable.</FONT></p>
								<P dir="ltr" align="center">
									&nbsp;<uc2:Footer ID="Footer1" runat="server" />
							</TD>
						</TR>
					</TABLE>
				</P>
			</TD>
		</TR>
	</TABLE>
</asp:Content>

