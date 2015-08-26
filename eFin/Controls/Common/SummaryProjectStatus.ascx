<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SummaryProjectStatus.ascx.vb" Inherits="Controls_SummaryProjectStatus" %>
<%@ Register Src="~/Controls/Common/ProjectChartField.ascx" TagPrefix="uc1" TagName="ProjectChartField" %>
<P style="FONT-SIZE: x-small; FONT-FAMILY: Verdana" align="center"><STRONG>Summary 
		Project Status:
		<asp:label id="lblProjectStatus" Width="56px" runat="server"></asp:label></STRONG>
</P>
<uc1:ProjectChartField runat="server" ID="ProjectChartField" />
<TABLE id="Table1" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: xx-small; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse"
	borderColor="black" cellSpacing="0" cellPadding="2" width="80%" align="center" bgColor="#e6ffff"
	border="1">
	<TR>
		<TD style="WIDTH: 142px; HEIGHT: 33px" vAlign="middle" noWrap align="left" bgColor="#C0C0C0"><STRONG>Summary Project 
				Holder</STRONG></TD>
		<TD style="HEIGHT: 33px" vAlign="middle" noWrap align="left">
			<TABLE id="Table2" style="FONT-SIZE: xx-small" cellSpacing="1" cellPadding="0" width="100%"
				border="0">
				<TR>
					<TD style="WIDTH: 50px"><STRONG>Name:</STRONG></TD>
					<TD><asp:label id="lblName" runat="server"></asp:label><STRONG></STRONG></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 50px"><STRONG>Status:</STRONG></TD>
					<TD><asp:label id="lblStatus" runat="server"></asp:label><STRONG></STRONG></TD>
				</TR>
			</TABLE>
		</TD>
		<TD style="HEIGHT: 33px" vAlign="middle" noWrap align="left"><STRONG>Dept code/desc:
				<BR>
			</STRONG>
			<asp:label id="lblDeptCode" runat="server"></asp:label></TD>
		<TD style="HEIGHT: 33px" vAlign="middle" noWrap align="left"><STRONG>Contact Information:
				<BR>
			</STRONG>
			<asp:label id="lblContactInfo" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 142px" vAlign="middle" noWrap align="left" bgColor="#C0C0C0"><STRONG>Summary Project Title</STRONG></TD>
		<TD noWrap align="left"><asp:label id="lblTitle" runat="server"></asp:label></TD>
		<TD noWrap align="left"><STRONG></STRONG></TD>
		<TD noWrap align="left"><STRONG></STRONG></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 142px" vAlign="middle" noWrap align="left" bgColor="#C0C0C0"><STRONG>Summary Project Effective Dates</STRONG></TD>
		<TD noWrap align="left"><STRONG>Begin Date:
				<BR>
			</STRONG>
			<asp:label id="lblBeginDate" runat="server"></asp:label></TD>
		<TD noWrap align="left"><STRONG>End Date:
				<BR>
			</STRONG>
			<asp:label id="lblEndDate" runat="server"></asp:label></TD>
		<TD noWrap align="left"><STRONG>Review Date:
				<BR>
			</STRONG>
			<asp:label id="lblReviewDate" runat="server"></asp:label></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 142px" vAlign="middle" noWrap align="left" bgColor="#C0C0C0"><STRONG>Agency Reporting</STRONG></TD>
		<TD noWrap align="left"><STRONG>Frequency of Financial Reporting 1:
				<BR>
			</STRONG><asp:label id="lblAgencyReporting" runat="server"></asp:label>
			<br />
			<STRONG>Frequency of Financial Reporting 2:
				<BR>
			</STRONG><asp:label id="lblAgencyReporting2" runat="server"></asp:label></TD>
		<TD noWrap align="left"><STRONG>Agency Reporting Format:
				<BR>
			</STRONG>
			<asp:label id="lblReportFormat" runat="server"></asp:label>
			<br />
			<STRONG>Agency 2nd Reporting Format:
				<BR>
			</STRONG><asp:label id="lblReportFormat2" runat="server"></asp:label></TD>
		<TD noWrap align="left"><STRONG></STRONG></TD>
	</TR>
</TABLE>
<!-- ******************** END Page Content ******************* -->
