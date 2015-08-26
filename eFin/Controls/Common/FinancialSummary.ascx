<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FinancialSummary.ascx.vb" Inherits="Controls_FinancialSummary" %>
<%@ Register Assembly="RadTabStrip.Net2" Namespace="Telerik.WebControls" TagPrefix="radTS" %>

<%@ Reference Control="~/Controls/Common/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectTeamMember" Src="ProjectTeamMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectActivityStatus" Src="~/Controls/Common/ProjectActivityStatus.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryProjectStatus" Src="~/Controls/Common/SummaryProjectStatus.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectChartField" Src="~/Controls/Common/ProjectChartField.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectAwardData" Src="~/Controls/Common/ProjectAwardData.ascx" %>

<radTS:RadTabStrip ID="rts1" runat="server" SelectedIndex="0" Skin="ClassicBlue"
	Width="80%" MultiPageID="rmp1">
	<Tabs>
		<radTS:Tab runat="server" Text="Project Header">
		</radTS:Tab>
		<radTS:Tab runat="server" Text="Financial Summary">
		</radTS:Tab>
		<radTS:Tab runat="server" Text="Spend Data">
		</radTS:Tab>
		<radTS:Tab runat="server" Text="Award Info">
		</radTS:Tab>
	</Tabs>
</radTS:RadTabStrip>

<radTS:RadMultiPage ID="rmp1" runat="server" SelectedIndex="0">
	<radTS:PageView ID="PageView1" runat="server" Width="100%">
		<asp:Panel ID="panSummaryProject" runat="server"><uc1:SummaryProjectStatus ID="SummaryProjectStatusControl" runat="server" /></asp:Panel>
		<asp:Panel ID="PanDetailStatus" runat="server"><uc1:ProjectActivityStatus ID="ProjectActivityStatusControl" runat="server" /></asp:Panel>
		<asp:Panel ID="panTeamMember" runat="server"><uc1:ProjectTeamMember ID="ProjectTeamMemberControl" runat="server" /></asp:Panel>
		<asp:Panel ID="PanelEmplLink" runat="server" HorizontalAlign="Left" Width="80%"><br />Team Authorization Form:<br /> <asp:HyperLink ID="hypTAForm" runat="server" NavigateUrl="http://wcm.ucalgary.ca/finance/files/finance/team_authorization_form_dec2013.pdf" Target="_blank">http://wcm.ucalgary.ca/finance/files/finance/team_authorization_form_dec2013.pdf</asp:HyperLink><br /><br />Procedures for Closing Clinical Trials:<br /> <asp:HyperLink ID="hypPforCCT" runat="server" NavigateUrl="https://www.ucalgary.ca/cccr/clinical-research/forms" Target="_blank">https://www.ucalgary.ca/cccr/clinical-research/forms</asp:HyperLink><br /><br />Project Attachment:<br /> <asp:HyperLink ID="hpyAttachment" runat="server" Target="_blank">Attachment List</asp:HyperLink></asp:Panel>
	</radTS:PageView>

	<radTS:PageView ID="PageView2" runat="server">
		<b><font face="Verdana" size="2">
		<br />
		From <b><font face="Verdana" size="2">
		<asp:Label ID="lblDateFinancialFrom" runat="server"></asp:Label>
		</font></b>&nbsp;To
		<asp:Label ID="lblDate" runat="server"></asp:Label>
		&nbsp;Refreshed On
		<asp:Label ID="lblRefresh" runat="server"></asp:Label>
		&nbsp;</font></b>
		<div style="margin-left: auto; margin-right: auto; width: 80%;">
			<div id="export" style="margin-bottom: 5px; text-align: right;" width="80%">
				<table style="table-layout: fixed" style="text-align: left" width="100%">
					<tr>
						<td align="left"><b style="text-align: left"><font face="Verdana" size="2">
							<asp:Label ID="lblParentProject" runat="server" Text="Parent Project:" Visible="False"></asp:Label>
							<asp:HyperLink ID="hypParentProject" runat="server" Visible="false">HyperLink</asp:HyperLink>
							</font></b>
							<td></td>
						</td>
					</tr>
					<tr>
						<td align="left"><b style="text-align: left"><font face="Verdana" size="2">
							<asp:Label ID="lblChildProject" runat="server" Text="Child Project:"></asp:Label>
							</font>
							<asp:DropDownList ID="ddlChildproject" runat="server" AutoPostBack="True">
							</asp:DropDownList>
							</b>
							<td align="right"><b style="text-align: right">
								<asp:Button ID="btnExcel" runat="server" CssClass="btn" Text="Excel" />
								</b></td>
						</td>
					</tr>
				</table>
			</div>
		</div>
		<uc1:ProjectChartField runat="server" ID="ProjectChartField1" />
		<asp:DataGrid ID="dgrdFinancialSummary" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" CssClass="GridViewStyle" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Black" HorizontalAlign="Center" Width="80%"><FooterStyle BackColor="#CCCCCC" /><SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" /><AlternatingItemStyle BackColor="#CCCCCC" HorizontalAlign="Right" /><ItemStyle HorizontalAlign="Right" /><HeaderStyle BackColor="#699BCD" BorderColor="Black" BorderStyle="solid" BorderWidth="1px" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" /><Columns><asp:TemplateColumn HeaderText="ExpandCollapse"><HeaderStyle Width="1%" /><HeaderTemplate>
			<asp:ImageButton ID="imgExpand" runat="server" CommandName="ExpandAll" Height="16px" ImageUrl="~/Images/gv_expand.png" Visible="true" /><asp:ImageButton ID="imgCollapse" runat="server" CommandName="CollapseAll" Height="17px" ImageUrl="~/Images/gv_collapse.png" Visible="false" />
			</HeaderTemplate>
			<ItemTemplate>
				<asp:ImageButton ID="imgExpand" runat="server" CommandName="Expand" Height="16px" ImageUrl="~/Images/gv_expand.png" />
				<asp:ImageButton ID="imgCollapse" runat="server" CommandName="Collapse" Height="17px" ImageUrl="~/Images/gv_collapse.png" Visible="false" />
			</ItemTemplate>
			</asp:TemplateColumn><asp:BoundColumn DataField="fld_Title" HeaderText="Title"><HeaderStyle Width="20%" /><ItemStyle HorizontalAlign="Left" /></asp:BoundColumn><asp:TemplateColumn HeaderText="Child Project"><ItemStyle HorizontalAlign="Center" /><HeaderStyle Width="5%" /><ItemTemplate><asp:HyperLink ID="hyplnkChildProject" runat="server"></asp:HyperLink>
			</ItemTemplate>
			</asp:TemplateColumn><asp:TemplateColumn HeaderText="Account"><ItemStyle HorizontalAlign="Center" /><HeaderStyle Width="5%" /><ItemTemplate><asp:HyperLink ID="hyplnkAccount" runat="server"></asp:HyperLink>
			</ItemTemplate>
			</asp:TemplateColumn><asp:BoundColumn DataField="fld_Desc" HeaderText="Description"><ItemStyle HorizontalAlign="Center" /><HeaderStyle Width="20%" /></asp:BoundColumn><asp:TemplateColumn HeaderText="Budget&lt;br&gt;($)"><ItemTemplate><asp:Label ID="lblBudget" runat="server"></asp:Label>
			</ItemTemplate>
			</asp:TemplateColumn><asp:TemplateColumn HeaderText="Actual&lt;br&gt;($)"><HeaderStyle Width="10%" /><ItemTemplate><asp:Label ID="lblActual" runat="server"></asp:Label>
			</ItemTemplate>
			</asp:TemplateColumn><asp:TemplateColumn HeaderText="Encumbrances&lt;br&gt;($)"><HeaderStyle Width="10%" /><ItemTemplate><asp:Label ID="lblEncumbrance" runat="server"></asp:Label>
			</ItemTemplate>
			</asp:TemplateColumn><asp:TemplateColumn HeaderText="Total Committed&lt;br&gt;(ACT+ENC)&lt;br&gt;($)"><HeaderStyle Width="10%" /><ItemTemplate><asp:Label ID="lblBalance" runat="server"></asp:Label>
			</ItemTemplate>
			</asp:TemplateColumn><asp:TemplateColumn HeaderText="Budget Variance&lt;br&gt;(BUD-TOT)&lt;br&gt;($)"><HeaderStyle Width="10%" /><ItemTemplate><asp:Label ID="lblBudgetVariance" runat="server"></asp:Label>
			</ItemTemplate>
			</asp:TemplateColumn>
			</Columns>
			<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
		</asp:DataGrid>
		<br>
		<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Width="80%"><p align="right"><asp:Label ID="lblOverspentOrOvercommitted" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor="Red"></asp:Label></p></asp:Panel>
		<br>
		<asp:Panel ID="panBurnRatePeriod" runat="server" HorizontalAlign="Center" Width="80%"><table align="Left" style="border: 1px #000000 solid;" width="50%"><font face="Verdana" size="2"><tr><td align="left" colspan="2" style="text-align: center; background-color: #699bcd;"><b style="color: rgb(255, 255, 255);"><font face="Verdana" size="2" style="text-align: center">Period Information<strong> <span style="font-size: 10pt; font-family: Verdana">( <asp:Label ID="lblStartDate" runat="server"></asp:Label>&#160;to <asp:Label ID="lblEndDate" runat="server"></asp:Label>)</span> </strong></font></b></td></tr><tr><td align="left"><font face="Verdana" size="2">Burn Rate:</font></td>
			<td align="left"><font face="Verdana" size="2"><asp:Label ID="lblBurnRate" runat="server"></asp:Label></font></td></tr><tr><td align="left"><font face="Verdana" size="2">Projected Zero Balance Date:</font></td><td align="left"><font face="Verdana" size="2"><asp:Label ID="lblZeroBalDate" runat="server"></asp:Label></font></td></tr></font></table></asp:Panel>
		<br>
		<br>
		<br>
		<br>
		<br>
		<hr width="80%">
		<br>
		<br>
		<br>
		<asp:Panel ID="panBurnRate" runat="server" HorizontalAlign="Center" Width="80%"><table align="Center" style="border: 1px #000000 solid;" width="50%"><font face="Verdana" size="2"><tr><td align="left" colspan="2" style="text-align: center; background-color: #575757;"><b style="color: rgb(255, 255, 255);"><font face="Verdana" size="2" style="text-align: center">Whole Project Period Information<strong> <span style="font-size: 10pt; font-family: Verdana">( <asp:Label ID="lblStartDate1" runat="server"></asp:Label>&#160;to <asp:Label ID="lblEndDate1" runat="server"></asp:Label>)</span> </strong></font></b></td></tr><tr><td align="left"><font face="Verdana" size="2">Burn Rate:</font></td>
			<td align="left"><font face="Verdana" size="2"><asp:Label ID="lblBurnRate1" runat="server"></asp:Label></font></td></tr><tr><td align="left"><font face="Verdana" size="2">Projected Zero Balance Date:</font></td><td align="left"><font face="Verdana" size="2"><asp:Label ID="lblZeroBalDate1" runat="server"></asp:Label></font></td></tr></font></table></asp:Panel>
		<br>
		<br>
		<br>
		<br>
		<asp:Panel ID="panProject" runat="server" HorizontalAlign="center" Width="80%"><p align="left"><font face="Verdana" size="2">The current authorized over-expenditure period is <asp:Label ID="lblPeriod" runat="server"></asp:Label></font></p><p align="left"><font face="Verdana" size="2">The current authorized over-expenditure amount for this project is <asp:Label ID="lblAmount" runat="server"></asp:Label></font></p></asp:Panel>
		<asp:Panel ID="panActivity" runat="server" HorizontalAlign="Center" Width="80%"><p align="center"><font face="Verdana" size="2">This report is for one activity. It may not include all expenses and revenues for this project.</font></p></asp:Panel>
		<p align="center">
			<font face="Verdana" size="2">Over-spent is actual amounts.&nbsp; Over-committed is actual plus encumbrances.<br />
			&nbsp;<br />
			&nbsp;<asp:Label ID="lblFinancialMessage" runat="server" Text="Salary encumbrance is calculated based 12 month."></asp:Label></font></p>

	</radTS:PageView>
 
	<radTS:PageView ID="PageView3" runat="server" Width="100%">
		<br />
		<br />
		<uc1:ProjectChartField ID="ProjectChartField2" runat="server" />
		 
		<p align="center">
			<font color="#ff0033" face="Arial"> <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label></font> <table align="center" width="80%">
				<tr align="center">
					<td>
						<asp:Chart ID="ChartActualOverAll" runat="server" EnableViewState="True" Height="400px" Width="600px">
							<chartareas>
								<asp:ChartArea BackImageAlignment="Center" Name="ChartArea1">
								</asp:ChartArea>
							</chartareas>
							<titles>
								<asp:Title Font="Verdana, 12pt, style=Bold" Name="Actual Overall" Text="Actual Overall">
								</asp:Title>
							</titles>
						</asp:Chart>
						<asp:Chart ID="ChartSpendbyCategory" runat="server" EnableViewState="True" Height="400px" Width="600px">
							<series>
								<asp:Series ChartArea="ChartArea1" ChartType="Pie" Color="Black" CustomProperties="PieDrawingStyle=Concave, PieLabelStyle=Outside" Font="Verdana, 8.25pt" Label="#VALX: #PERCENT{P1}" LabelToolTip="#VAL{C2}" MarkerStyle="Star4" Name="Series1">
								</asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1">
								</asp:ChartArea>
							</chartareas>
							<titles>
								<asp:Title Font="Verdana, 12pt, style=Bold" Name="Spend by Category" Text="Spend by Category">
								</asp:Title>
							</titles>
						</asp:Chart>
						<asp:Chart ID="ChartTotalActuals" runat="server" EnableViewState="True" Height="400px" Width="500px">
							<series>
								<asp:Series ChartArea="ChartArea1" ChartType="Pie" CustomProperties="PieLabelStyle=Outside" Label="#VALX: #PERCENT{P1}" LabelToolTip="#VALX" Name="Series1">
								</asp:Series>
							</series>
							<chartareas>
								<asp:ChartArea Name="ChartArea1">
								</asp:ChartArea>
							</chartareas>
							<titles>
								<asp:Title Font="Verdana, 12pt, style=Bold" Name="Title1" Text="Top Ten Expend of PI Projects">
								</asp:Title>
							</titles>
						</asp:Chart>
					</td>
				</tr>
			</table>
		</p>
	</radTS:PageView>

	 <radTS:PageView ID="PageView4" runat="server" Width="100%">
		<br />
		 <asp:Panel ID="panAwardInfo" runat="server"><uc1:ProjectAwardData ID="ProjectAwardData" runat="server" /></asp:Panel>
	</radTS:PageView>
</radTS:RadMultiPage>

