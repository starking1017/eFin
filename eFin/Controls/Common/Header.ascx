<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Header.ascx.vb" Inherits="Controls_Common_Header" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script src="../../Forms/Researcher/headerLoading.js"></script>
<asp:Panel id="Panel1" runat="server">

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
		<TD vAlign="middle" bgcolor="#cccccc" noWrap align="center"><asp:label id="lblHeaderTitle" runat="server" Font-Bold="True" Font-Size="Small"></asp:label></TD>
	</TR>
	<asp:Panel id="panDisplayOptions" runat="server" HorizontalAlign="Center">
		<TR>
			<TD style="HEIGHT: 63px" vAlign="middle" noWrap align="center" colSpan="2">
				<asp:Panel id="panHeaderDetails" runat="server" HorizontalAlign="Center">
					<asp:label id="lblProjectSummaryTitle" Font-Bold="True" runat="server" Visible="False">Summary Project:</asp:label>
					<asp:label id="lblProjectTitle" Font-Bold="True" runat="server">Project:</asp:label>&nbsp; 
					<asp:label id="lblProject" Font-Bold="True" runat="server"></asp:label><BR>
					<asp:label id="lblActivityTitle" Font-Bold="True" runat="server">For Activity:</asp:label>&nbsp; 
					<asp:label id="lblActivity" Font-Bold="True" runat="server"></asp:label><BR></asp:Panel>
					<STRONG>For The Period:&nbsp;&nbsp;</STRONG>
					<asp:label id="lblPeriod" Font-Bold="True" runat="server"></asp:label><BR>
			</TD>
		</TR>
		<TR>
			<TD vAlign="middle" noWrap align="center" colSpan="2">
				<asp:label id="lblError" runat="server" ForeColor="Red" CssClass="errMessage"></asp:label></TD>
		</TR>
		<TR vAlign="middle">
			<TD vAlign="middle" noWrap align="left"><STRONG>Display 
					Options:</STRONG></TD>
			<TD vAlign="top" noWrap>
				<TABLE id="Table2" style="FONT-SIZE: x-small; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse"
					cellSpacing="0" cellPadding="0" width="100%" border="1">
					<TR>
						<TD vAlign="middle" noWrap align="center" >
							<asp:linkbutton id="lnkBtnFullview" runat="server" Font-Size="10" Font-Bold="true" ToolTip="Sets project Start Year / Month to start date of project and End Year / Month to current month." OnClientClick="ShowProgressBar();">Full View</asp:linkbutton>
						</TD>
						<TD noWrap bgColor="silver">
							<P align="center">Start Year / Month</P>
						</TD>
						<TD noWrap bgColor="silver">
							<P align="center">End Year / Month</P>
						</TD>
					</TR>
					<TR>
						<TD vAlign="middle" noWrap align="center" >
						    Projet Year:
							<asp:dropdownlist id="ddlProjectYear" runat="server" AutoPostBack="True" onchange="ShowProgressBar();"></asp:dropdownlist>
						</TD>
						<TD style="HEIGHT: 11px" noWrap>
							<P align="center">
								<asp:dropdownlist id="ddlStartYear" runat="server"></asp:dropdownlist>
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
						<TD style="HEIGHT: 11px" noWrap>
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
							<asp:linkbutton id="lnkBtnRefresh" runat="server" Font-Size="10" Font-Bold="true"  ToolTip="Retrieve the information for selected time periord" OnClientClick="ShowProgressBar();">Refresh</asp:linkbutton></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</asp:Panel>
	<asp:Panel id="panStartAt" runat="server" HorizontalAlign="Center">
		<!--/asp:Panel-->
		<tr height="30">
			<TD vAlign="middle" noWrap align="left">
				<asp:label id="lblStartAtProject" Font-Bold="True" runat="server">Start At Project:</asp:label><br/>
				<asp:label id="lblmessage1" Font-Bold="False" Visible="false" runat="server">(Enter Child Project Number or Parent Project Number)</asp:label>				
				</TD>
			<TD noWrap align="center">
				&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
				<asp:textbox id="txtStartAtProject" runat="server" Width="272px"></asp:textbox>
				&nbsp;
				<asp:LinkButton id="lnkBtnGo" runat="server" Font-Bold="true" visible="false" OnClientClick="ShowProgressBar();">Go</asp:LinkButton>&nbsp;&nbsp;
				<asp:LinkButton id="lnkbtnReset" runat="server" Font-Bold="true" visible="false">Reset</asp:LinkButton>&nbsp;&nbsp;
				<asp:ImageButton ID="ibHead" runat="server" ImageUrl="~/Images/collapse.gif" 
					ToolTip="Show Advanced Search Options" Visible="true" />
			</TD>
		</tr>
		<asp:Panel id="panAdvanceSearch" runat="server" HorizontalAlign="Center" Visible="true">
		<!--/asp:Panel-->

			<tr height="30">
				<td vAlign="middle" noWrap align="left">
					<asp:label id="Label1" Font-Bold="True" runat="server">Advanced Search:</asp:label><br/>
					</td>
				<td noWrap align="center">
					<table style="width:100%;">
						<tr>
							<td align="left">
					<asp:label id="lbPI0" Font-Bold="False" runat="server" Font-Size="X-Small">Project Status:</asp:label>
							</td>
							<td>
								<asp:DropDownList ID="ddlStatus" runat="server">
									<asp:ListItem Selected="True" Value="ALL">--- ALL ---</asp:ListItem>
									<asp:ListItem>Active</asp:ListItem>
									<asp:ListItem>Closed</asp:ListItem>
									<asp:ListItem>Operatioanally Closed</asp:ListItem>
									<asp:ListItem>Suspended</asp:ListItem>
									<asp:ListItem>Substantial Completion</asp:ListItem>
								</asp:DropDownList>
							</td>
							<td align="left">
					<asp:label id="lbPI1" Font-Bold="False" runat="server" Font-Size="X-Small">Faculty:</asp:label>
							</td>
							<td>
								<asp:DropDownList ID="ddlFaculty" runat="server">
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td align="left">
					<asp:label id="lbPI2" Font-Bold="False" runat="server" Font-Size="X-Small">Department:</asp:label>
							</td>
							<td>
								<asp:DropDownList ID="ddlDepartment" runat="server">
								</asp:DropDownList>
							</td>
							<td align="left">
								&nbsp;</td>
							<td>
								&nbsp;</td>
						</tr>
						<tr>
							<td align="left">
					<asp:label id="lbPI4" Font-Bold="False" runat="server" Font-Size="X-Small">Key Words:</asp:label>
							</td>
							<td>
								<asp:TextBox ID="txtKeyWord" runat="server" Width="180px" MaxLength="40"></asp:TextBox>
							</td>
							<td>
								&nbsp;</td>
							<td>
								&nbsp;</td>
						</tr>
						<tr>
							<td align="left" colspan="4" style="font-size: x-small">
								(Enter key words to search against project description/Reference/Sponsor/RSO ID/Project Holder/Ethics Certification)
					</table>
				 </td>
			</tr>
		</asp:Panel>
		<asp:Panel id="panPI" runat="server" HorizontalAlign="Center" Visible="false">
		<!--/asp:Panel-->

			<TR height="30">
				<TD vAlign="middle" noWrap align="left">
					<asp:label id="lbPI" Font-Bold="True" runat="server">Project Holder:</asp:label><br/>
					</TD>
				<TD noWrap align="center">
					<asp:DropDownList ID="ddlPI2" runat="server" AutoPostBack="True"></asp:DropDownList>

					<%--<telerik:RadComboBox ID="ddlPI" runat="server" Width="250px"  AllowCustomText="True" Filter="StartsWith" CollapseDelay="1" ExpandDelay="1" >
						<ExpandAnimation Duration="1" Type="None" />
						<CollapseAnimation Duration="1" Type="OutQuint" />
					</telerik:RadComboBox>--%>
				 </TD>
			</TR>
		</asp:Panel>

		<asp:Panel id="panGoReset" runat="server" HorizontalAlign="Center">
			<tr height="30">
				<TD noWrap align="center" colspan ="2">
					<asp:LinkButton id="lnkBtnGo2" runat="server" Font-Bold="true" OnClientClick="ShowProgressBar();">Go</asp:LinkButton>&nbsp;&nbsp;
					<asp:LinkButton id="lnkbtnReset2" runat="server" Font-Bold="true">Reset</asp:LinkButton>&nbsp;&nbsp;
				</TD>
			</tr>
		</asp:Panel>
	</asp:Panel>
</TABLE>
</asp:Panel>