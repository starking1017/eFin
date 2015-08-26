<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CommonXmlFooterMenu.ascx.vb" Inherits="Controls_Common_XmlFooterMenu_CommonXmlFooterMenu" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="80%" align="center" border="0"
	bgcolor="#ffffff">
	<TR>
		<TD vAlign="middle" noWrap align="center">
			<asp:DataList id="dlFooterMenu" runat="server" RepeatDirection="Horizontal" CellPadding="1" GridLines="Both"
				BackColor="#ffffff" BorderColor="Black" HorizontalAlign="Center" Width="100%">
				<AlternatingItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></AlternatingItemStyle>
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
				<ItemTemplate>
					<asp:LinkButton id="lnkMenuItem" runat="server" Font-Names="Verdana" Font-Size="X-Small" CausesValidation="False"
						CommandName="REDIRECT"></asp:LinkButton>
				</ItemTemplate>
			</asp:DataList>
            <asp:LinkButton ID="LinkButton1" runat="server" Visible="False">LinkButton</asp:LinkButton>
		</TD>
	</TR>
</TABLE>
