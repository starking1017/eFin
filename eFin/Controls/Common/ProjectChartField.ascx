<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectChartField.ascx.vb" Inherits="Controls_Common_ProjectChartField" %>
	<TABLE id="Table2" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: x-small; BORDER-LEFT:#000000 1px solid; 
				BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana; text-align:center; BORDER-COLLAPSE: collapse; table-layout: fixed;" borderColor="black" cellSpacing="0" cellPadding="2" 
				width="80%" align="center" border="0" text-align: left>
		<TR>
			<TD>
				Fund:
				<asp:Label ID="lblFund" runat="server"></asp:Label>
			</TD>
			<TD>
				Project DeptID/Desc:
				<asp:Label ID="lbDeptCode" runat="server"></asp:Label>
			</TD>                               
			<TD>
				PC Business Unit:
				<asp:Label ID="lblBusinessUnit" runat="server"></asp:Label>
			</TD>
		</TR>
	</TABLE>
						
