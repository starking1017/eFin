<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Banner.ascx.vb" Inherits="Controls_Banner" %>
<TABLE WIDTH="80%" BORDER="0" CELLPADDING="0" CELLSPACING="0">
	<TR>
		<TD align="left">
			<asp:LinkButton id="lnkbtnHome" runat="server" Font-Names="Verdana" Font-Size="Small" CausesValidation="False">Home</asp:LinkButton>&nbsp;&nbsp;&nbsp;
			<asp:LinkButton id="lnkbtnLogout" runat="server" Font-Names="Verdana" Font-Size="Small" CausesValidation="False">Logout</asp:LinkButton>
		</TD>
		<TD>&nbsp;
		</TD>
		<TD align="right"><font color="#ff0000" face="Arial, Helvetica, sans-serif"><strong>e<font color="#000000">FIN</font></strong><font color="#000000">
				</font></font>
		</TD>
	</TR>
	<TR>
		<TD background="<% =Utils.ImagesPath() %>/BannerBackGroundBlue.jpg" valign="bottom">
			<a href="http://ereports.ucalgary.ca/ereports/" target="_blank"><img width="156" height="78" src="<% =Utils.ImagesPath() %>/UofCERLogo-small.png"></a></TD>
		<TD width="100%" background="<% =Utils.ImagesPath() %>/BannerBackGroundBlue.jpg">&nbsp;</TD>
		<TD align="right"><img src="<% =Utils.ImagesPath() %>/eFIN.jpg" width="263" height="89"></TD>
	</TR>
	<TR>
		<TD colspan="3" background="<% =Utils.ImagesPath() %>/UnderBannerDottedLine.jpg">&nbsp;
		</TD>
	</TR>
</TABLE>