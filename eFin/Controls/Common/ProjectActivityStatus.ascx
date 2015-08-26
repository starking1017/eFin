<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectActivityStatus.ascx.vb" Inherits="Controls_ProjectActivityStatus" %>
<%@ Register src="ProjectChartField.ascx" tagname="ProjectChartField" tagprefix="uc1" %>

<asp:Panel id="panStatus" runat="server">
	<p align="center" style="FONT-SIZE: x-small; FONT-FAMILY: Verdana">
		<strong>Project&nbsp;Status:
			<asp:Label ID="lblProjectStatus" runat="server"></asp:Label>
		</strong><strong></strong>
	</p>
	<uc1:ProjectChartField ID="ProjectChartField1" runat="server" />
	<TABLE ID="Table1" align="center" bgColor="#e6ffff" border="1" borderColor="black" cellPadding="2" cellSpacing="0" width="80%"
		style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: xx-small; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; 
			   FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse; word-wrap: break-word; text-overflow:ellipsis; overflow: hidden; table-layout:auto;" >
		<TR>
			<TD align="left" bgcolor="#C0C0C0" nowrap style="WIDTH: 142px;" valign="middle"><strong>Project Title</strong></td>
			<TD align="left" nowrap style="WIDTH: 200px;"><strong>Title:</strong>&nbsp;<br />
				<asp:Label ID="lblTitle" runat="server"></asp:Label>
			</td>
			<TD align="left" style="WIDTH:400px; word-wrap:break-word; text-overflow:ellipsis; overflow: hidden;"><strong>Description:</strong>&nbsp;<br />
				<asp:Label ID="lblDescription" runat="server"></asp:Label>
				<br />
			</td>
			<TD align="left" nowrap><strong>Holder Name:<br /> </strong>
				<asp:Label ID="lblName" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap><strong>Holder Status:<br /> </strong>
				<asp:Label ID="lblStatus" runat="server"></asp:Label>
			</td>
		</tr>
		<TR>
			<TD align="left" bgcolor="#C0C0C0" nowrap style="WIDTH: 142px;" valign="middle"><strong>Project Chart Fields</strong></td>
			<TD align="left" nowrap valign="middle"><strong>Fund:<br /> </strong>
				<asp:Label ID="lblFund" runat="server"></asp:Label>
			</td>
			<TD align="left" valign="middle"><strong>Project DeptID/Desc:<br /> </strong>
				<asp:Label ID="lbDeptCode" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap valign="middle"><strong>PC Business Unit:<br /> </strong>
				<asp:Label ID="lblBusinessUnit" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrapvalign="middle">&nbsp;</td>
		</tr>
		<TR>
			<TD align="left" bgcolor="#C0C0C0" nowrap style="WIDTH: 142px" valign="middle"><strong>Project Effective Dates</strong></td>
			<TD align="left" nowrap><strong>Start Date:<br /> </strong>
				<asp:Label ID="lblBeginDate" runat="server"></asp:Label>
			</td>
			<TD align="left" ><strong>End Date:</strong>
				<br />
				<asp:Label ID="lblEndDate" runat="server"></asp:Label>
				<br />
			</td>
			<TD align="left" nowrap><strong>Expiry Date:
				<br />
				</strong>
				<asp:Label ID="lblExpiryDate" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap><strong>Retain Fund:<br /> </strong>
				<asp:Label ID="lblRetainFund" runat="server"></asp:Label>
			</td>
		</tr>
		<TR>
			<TD align="left" bgcolor="#C0C0C0" nowrap style="WIDTH: 142px" valign="middle"><strong>Project Funding</strong></td>
			<TD align="left" style ="word-wrap:break-word; text-overflow:ellipsis; overflow: hidden;"><strong>Sponsor:<br /> </strong>
				<asp:Label ID="lblSponsor" runat="server"></asp:Label>
				<br />
				<strong>Tri-Council Agency: </strong>
				<br>
				<asp:Label ID="lbTricouncil" runat="server" Font-Bold="True" ForeColor="#CD4545"></asp:Label>
			</td>
			<TD align="left"><strong>Reference #1:<br /> </strong>
				<asp:Label ID="lblReference1" runat="server"></asp:Label>
				<br />
				<strong>Reference #2:<br /> </strong>
				<asp:Label ID="lblReference2" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap><strong>Payment Method:<br /> </strong>
				<asp:Label ID="lblPaymentMethod" runat="server"></asp:Label>
				<br />
				<strong>Holdback Amount:<br /> </strong>$<asp:Label ID="lblHoldbackAmount" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap><strong>Interest Bearing:<br /> </strong>
				<asp:Label ID="lblInterestBearing" runat="server"></asp:Label>
				<br />
				<strong>Overhead Amount:<br /> </strong>$<asp:Label ID="lblOverheadAmount" runat="server"></asp:Label>
			</td>
		</tr>
		<TR>
			<TD align="left" bgcolor="#C0C0C0" nowrap style="WIDTH: 142px" valign="middle"><strong>Overexpenditures</strong></td>
			<TD align="left" nowrap><strong>Authorized Overexpenditure Amt:</strong><br />
				<asp:Label ID="lblAOAmount" runat="server"></asp:Label>
			</td>
			<TD align="left"><strong>Authorized Overexpenditure Start Date:</strong><br />
				<asp:Label ID="lblAOStartDate" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap><strong>Authorized Overexpenditure End Date:</strong><br />
				<asp:Label ID="lblAOEndDate" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap><strong>Parent Project:<br /> </strong>
				<asp:HyperLink ID="hpyParentProject" runat="server">[hpyParentProject]</asp:HyperLink>
			</td>
		</tr>
		<TR>
			<TD align="left" bgcolor="#C0C0C0" nowrap style="WIDTH: 142px" valign="middle"><strong>Certifications</strong></td>
			<TD align="left" nowrap><strong>Human Certification Number:<br /> </strong>
				<asp:Label ID="lblHuman" runat="server"></asp:Label>
			</td>
			<TD align="left"><strong>Animal Certification Number:<br /> </strong>
				<asp:Label ID="lblAnimal" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap><strong>Biosafety Number:<br /> </strong>
				<asp:Label ID="lblBiosafety" runat="server"></asp:Label>
			</td>
			<TD align="left" nowrap><strong>RSO #:<br /> </strong>
				<asp:Label ID="lbRSO" runat="server"></asp:Label>
				<br />
				<strong>CRO:<br /> </strong>
				<asp:Label ID="lbCRO" runat="server"></asp:Label>
			</td>
		</tr>
	</TABLE>
	<br />
</asp:Panel>

<asp:Panel ID="panStatusAdmin" runat="server" Visible="False">
	<TABLE id="tabAdminTitle" style="BORDER-RIGHT: #000000 0px solid; BORDER-TOP: #000000 0px solid; FONT-SIZE: small; BORDER-LEFT:#000000 0px solid; 
			BORDER-BOTTOM: #000000 0px solid; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse" borderColor="black" cellSpacing="0" cellPadding="2" 
			width="80%" align="center" border="0" text-align: left>
		<TR><TD align="left"> <strong>Administrative View:</strong></TD></TR>
	</table>

	<TABLE id="tabAdminDetail" align="center" bgColor="#e6ffff" border="1" borderColor="black" cellPadding="2" cellSpacing="0" width="80%"
			style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: xx-small; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; 
			FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse; word-wrap: break-word; text-overflow:ellipsis;">
		<TR>
			<TD align="left" nowrap style="WIDTH: 142px;" valign="middle" bgColor="#C0C0C0"><strong>Project DeptID/Desc</strong></TD>
			<TD align="left" nowrap style="WIDTH: 200px;"><strong>Location:</strong><br />
				<asp:Label ID="lblLocation" runat="server"></asp:Label>
			</TD>
			<TD align="left" style="WIDTH: 250px; word-wrap:break-word; text-overflow:ellipsis; overflow: hidden;"></TD>
			<TD></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 142px" vAlign="middle" noWrap align="left" bgColor="#C0C0C0"><STRONG>Project Classifications</STRONG></TD>
			<TD noWrap align="left" >
						<strong>Purpose of Funds:<br /> </strong>
				<asp:Label ID="lblPurposeofFunds" runat="server"></asp:Label>
				</TD>
			<TD noWrap align="left">
				<STRONG>General Classification:<br /> </STRONG>
				<asp:Label ID="lblGeneralClassification" runat="server"></asp:Label>
				</TD>
			<TD noWrap align="left"><STRONG>Primary Source:<br /> </STRONG>
				<asp:Label id="lblPrimarySource" runat="server"></asp:Label></TD>
			<TD noWrap align="left">&nbsp;</TD>
		</TR>
		<TR>
			<TD style="WIDTH: 142px;" vAlign="middle" noWrap align="left" bgColor="#C0C0C0"><STRONG>Billing</STRONG></TD>
			<TD noWrap align="left" >
				<strong>Frequency of Invoice/Claim:<br /> </strong>
				<asp:label id="lblFrequencyofInvoice1" runat="server"></asp:label>
			</TD>
			<TD noWrap align="left" ><STRONG>Agency Invoice/Billing Format:</STRONG>&nbsp;<br>
				<asp:Label ID="lblAgencyInvoice1" runat="server"></asp:Label>
											
			</TD>
			<TD noWrap align="left">&nbsp;</TD>
			<TD noWrap align="left">&nbsp;</TD>
		</TR>
		<TR>
			<TD align="left" nowrap style="WIDTH: 142px;" valign="middle" bgColor="#C0C0C0"><strong>Financial Reporting</strong></TD>
			<TD align="left" nowrap ><strong>Frequency of Financial:<br /> </strong>
				<asp:Label ID="lblFrequencyofFinancial1" runat="server"></asp:Label>
			</TD>
			<TD align="left" nowrap ><strong>Agency Reporting Format:<br /> </strong>
				<asp:Label ID="lblAgencyReportingFormat1" runat="server"></asp:Label>
			</TD>
			<TD align="left" nowrap></TD>  
			<TD align="left"> &nbsp;</TD>                         
		</TR>
	</TABLE>
	<p></p>    
</asp:Panel>

<asp:Panel id="panActivity" runat="server">
	<P align="center"><STRONG><FONT face="Verdana" size="2">
		<asp:Label ID="lblActivityStatus" runat="server">Activity Status:</asp:Label>
		</FONT></STRONG>
		<asp:Label id="lblActStatus" runat="server" Font-Size="X-Small" Font-Bold="True" Font-Names="Verdana"></asp:Label>
	</P>
	<TABLE id="Table4" style="BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-SIZE: xx-small; BORDER-LEFT: #000000 1px solid; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Verdana; BORDER-COLLAPSE: collapse"
		borderColor="black" cellSpacing="1" cellPadding="2" width="80%" align="center" bgColor="#e6ffff"
		border="1">
		<TR>
			<TD align="left" bgColor="#C0C0C0"><STRONG>Activity Description</STRONG></TD>
			<TD align="left">
				<asp:Label id="lblActDescription" runat="server"></asp:Label></TD>
			<TD align="left"><STRONG></STRONG></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 142px" align="left" bgColor="#C0C0C0"><STRONG>Activity Effective Dates</STRONG></TD>
			<TD align="left"><STRONG>Begin Date:
					<BR>
				</STRONG>
				<asp:Label id="lblActBeginDate" runat="server"></asp:Label></TD>
			<TD align="left"><STRONG>End Date:
					<BR>
				</STRONG>
				<asp:Label id="lblActEndDate" runat="server"></asp:Label></TD>
		</TR>
	</TABLE>
	<p></p>
</asp:Panel>