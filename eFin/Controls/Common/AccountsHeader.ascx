<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AccountsHeader.ascx.vb" Inherits="Controls_Common_AccountsHeader" %>

<script src="../../Forms/Researcher/headerLoading.js"></script>

<asp:Panel id="Panel1" runat="server" DefaultButton ="lnkBtnGo">
<div id="divProgress" style="text-align:center; display: none; position: fixed; top: 50%;  left: 50%;" >
	<asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/loading.gif" />        
</div>
<div id="divMaskFrame" style="background-color: white; display: none; left: 0px;
	position: absolute; top: 0px;">
</div>  

<TABLE style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: x-small; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse"
	id="Table1" borderColor="black" cellSpacing="0" cellPadding="0" width="80%" align="center"
	border="1">
	<TR>
		<TD vAlign="middle" bgcolor="#cccccc" noWrap align="center" colSpan="2"><asp:label id="lblHeaderTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:label></TD>
	</TR>
	<asp:Panel id="panDisplayOptions" runat="server">
		<TR>
			<TD style="HEIGHT: 83px" vAlign="middle" noWrap align="center" colSpan="2"><BR>
				&nbsp;
				<asp:Panel id="panHeaderDetails" runat="server" HorizontalAlign="Center">
				<asp:label id="lblProjectTitle" Font-Bold="True" runat="server">Project:</asp:label>&nbsp; 
				<asp:label id="lblProject" Font-Bold="True" runat="server"></asp:label><BR>
				<asp:label id="lblActivityTitle" Font-Bold="True" runat="server">For Activity:</asp:label>&nbsp; 
				<asp:label id="lblActivity" Font-Bold="True" runat="server"></asp:label><BR>&nbsp;</asp:Panel>
				<STRONG>For The Period:&nbsp;&nbsp;</STRONG>
				<asp:label id="lblPeriod" Font-Bold="True" runat="server"></asp:label><BR>
				&nbsp;<BR>
				&nbsp;
			</TD>
		</TR>
		<TR>
			<TD vAlign="middle" noWrap align="center" colSpan="2">
				<asp:label id="lblError" runat="server" ForeColor="Red" CssClass="errMessage"></asp:label></TD>
		</TR>
		<TR vAlign="middle">
			<TD style="WIDTH: 247px" vAlign="middle" noWrap align="left"><STRONG>Display Options:</STRONG></TD>
			<TD vAlign="top" noWrap>
				<TABLE id="Table2" style="FONT-SIZE: x-small; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="0" width="100%" border="1">
					<TR>
						<TD vAlign="middle" noWrap align="center" rowspan ="1">
						<asp:linkbutton id="lnkBtnFullview" runat="server" Font-Size="10" Font-Bold="true" ToolTip="Sets project Start Year / Month to start date of project and End Year / Month to current month." OnClientClick="ShowProgressBar();">Full View</asp:linkbutton>
						</TD>
						<TD style="HEIGHT: 15px" noWrap bgColor="silver">
							<P align="center">Start Year / Month</P>
						</TD>
						<TD style="HEIGHT: 15px" noWrap bgColor="silver">
							<P align="center">End Year / Month</P>
						</TD>
					</TR>
					<TR>
					     <TD vAlign="middle" noWrap align="center" >
						    Projet Year:
							<asp:dropdownlist id="ddlProjectYear" runat="server" AutoPostBack="True" onchange="ShowProgressBar();"></asp:dropdownlist>
						</TD>
						<TD style="HEIGHT: 21px" noWrap>
							<P align="center">
								<asp:dropdownlist id="ddlStartYear"  runat="server"></asp:dropdownlist>
								<asp:dropdownlist id="ddlStartMonth" runat="server">
									<asp:ListItem Value="01">January</asp:ListItem>
									<asp:ListItem Value="02">February</asp:ListItem>
									<asp:ListItem Value="03">March</asp:ListItem>
									<asp:ListItem Value="04" Selected="True">April</asp:ListItem>
									<asp:ListItem Value="05">May</asp:ListItem>
									<asp:ListItem Value="06">June</asp:ListItem>
									<asp:ListItem Value="07">July</asp:ListItem>
									<asp:ListItem Value="08">August</asp:ListItem>
									<asp:ListItem Value="09">September</asp:ListItem>
									<asp:ListItem Value="10">October</asp:ListItem>
									<asp:ListItem Value="11">November</asp:ListItem>
									<asp:ListItem Value="12">December</asp:ListItem>
								</asp:dropdownlist></P>
						</TD>
						<TD style="HEIGHT: 21px" noWrap>
							<P align="center">
								<asp:dropdownlist id="ddlEndYear" runat="server"></asp:dropdownlist>
								<asp:dropdownlist id="ddlEndMonth" runat="server">
									<asp:ListItem Value="01">January</asp:ListItem>
									<asp:ListItem Value="02">February</asp:ListItem>
									<asp:ListItem Value="03">March</asp:ListItem>
									<asp:ListItem Value="04">April</asp:ListItem>
									<asp:ListItem Value="05">May</asp:ListItem>
									<asp:ListItem Value="06">June</asp:ListItem>
									<asp:ListItem Value="07">July</asp:ListItem>
									<asp:ListItem Value="08">August</asp:ListItem>
									<asp:ListItem Value="09">September</asp:ListItem>
									<asp:ListItem Value="10">October</asp:ListItem>
									<asp:ListItem Value="11">November</asp:ListItem>
									<asp:ListItem Value="12">December</asp:ListItem>
								</asp:dropdownlist></P>
						</TD>
					</TR>
					<TR>
						<TD vAlign="middle" noWrap align="center" colSpan="3">
							<asp:linkbutton id="lnkBtnRefresh" OnClick="lnkRefresh" runat="server" Font-Size="10" Font-Bold="true" ToolTip="Retrieve the information for selected time periord" OnClientClick="ShowProgressBar();">Refresh</asp:linkbutton></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</asp:Panel>
	<asp:Panel id="panStartAt" runat="server">
		<TR height="30">
			<TD style="WIDTH: 247px" vAlign="middle" noWrap align="left">
				<asp:label id="lblStartAtProject" Font-Bold="True" runat="server">Start At Account:</asp:label></TD>
			<TD noWrap align="center">
				<asp:textbox id="txtStartAtAccount" runat="server" Width="272px"></asp:textbox>&nbsp;
				<asp:LinkButton id="lnkBtnGo" runat="server" Font-Bold="true" OnClientClick="ShowProgressBar();">Go</asp:LinkButton></TD>
		</TR>
	</asp:Panel>
</TABLE>
</asp:Panel>
