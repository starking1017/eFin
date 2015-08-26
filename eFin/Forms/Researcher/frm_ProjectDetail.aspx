<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frm_ProjectDetail.aspx.vb" Inherits="Forms_Researcher_frm_ProjectDetail" title="eFIN Project Reporting" maintainScrollPositionOnPostBack="true" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="~/Controls/Common/Header.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommonXmlFooterMenu" Src="../../Controls/Common/XmlFooterMenu/CommonXmlFooterMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../Controls/Common/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectTeamMember" Src="../../Controls/Common/ProjectTeamMember.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../../Controls/Common/Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProjectActivityStatus" Src="~/Controls/Common/ProjectActivityStatus.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryProjectStatus" Src="~/Controls/Common/SummaryProjectStatus.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FinancialSummary" Src="~/Controls/Common/FinancialSummary.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" align="center">
				<TR>
					<TD vAlign="middle" noWrap align="center" width="100%"></TD>
				</TR>
				<TR>
					<TD vAlign="middle" noWrap align="center" width="100%">
						<P align="center">
							<uc1:commonxmlfootermenu id="CommonXmlHeaderMenuControl" runat="server"></uc1:commonxmlfootermenu><BR>
							<uc1:header id="HeaderControl" runat="server"></uc1:header><BR>
							<FONT face="Verdana" size="2">The information on this screen reflects the display 
								options selected.<BR>
								<BR>
							</FONT>
						</P>
					</TD>
				</TR>
				<TR>
					<TD vAlign="middle" noWrap align="center" width="100%"><BR>
						<uc1:financialsummary id="FinancialSummaryControl" runat="server" UpdateMode="Conditional"></uc1:financialsummary><BR>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 12px" vAlign="middle" noWrap align="center" width="100%">
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="80%" align="center" border="0">
							<TR>
								<TD align="left"><FONT face="Verdana" size="2">Costs for salary related benefits have not been committed. Please allow for 
										these costs where applicable.</FONT></TD>
							</TR>
						</TABLE>
						<BR>
					</TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 23px" vAlign="middle" noWrap align="center" width="100%">
						<P><uc1:commonxmlfootermenu id="CommonXmlFooterMenuControl" runat="server"></uc1:commonxmlfootermenu><BR>
						</P>
					</TD>
				</TR>
				<TR>
					<TD vAlign="middle" noWrap align="center" width="100%"></TD>
				</TR>
				<TR>
					<TD vAlign="middle" noWrap align="center" width="100%">
						<P><uc1:footer id="FooterControl" runat="server"></uc1:footer></P>
					</TD>
				</TR>
			</TABLE>
</asp:Content>

