<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="frm_ReportGeneration.aspx.vb" Inherits="Forms_Researcher_frm_ReportGeneration" title="Untitled Page" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../../Controls/Common/Banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommonXmlFooterMenu" Src="../../Controls/Common/XmlFooterMenu/CommonXmlFooterMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../../Controls/Common/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccountsHeader" Src="../../Controls/Common/AccountsHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../../Controls/Common/Header.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center" >
                        <asp:Button ID="Button1" runat="server" Text="Direct to Extranet Portal" />&nbsp;<br />
                        <asp:Button ID="Button2" runat="server" Text="Direct to Report" /></TD>
				</TR>
				<TR>
					<TD align="center">
						<P align="left"><FONT face="Verdana" size="2"><BR>
							</FONT>
						</P>
						<P>
                            &nbsp;</P>
						<P>
							<uc1:Footer id="FooterControl" runat="server"></uc1:Footer></P>
					</TD>
				</TR>
			</TABLE>
</asp:Content>

