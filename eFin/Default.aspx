<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" title="eFIN Project Reporting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<TABLE id="Table1" style="border: black 1px solid;" cellSpacing="0" cellPadding="20" width="80%" align="center">
		<TR>
			<TD>
				<p style="text-align: center; font: bold 12pt Arial,sans-serif;">
					**** UPDATED EVERY 24 HOURS ****
				</p>
				<p style="text-align: center; color: #E32726; font: italic bold 12pt Arial,sans-serif;">
					NEW
				</p>                
				<p style="text-align: center; color: blue; font: italic bold 12pt Arial,sans-serif;">
					<%Display()%>
					
				</p>                
				<!--p style="text-align: left; color: blue; font: bold 12pt Arial,sans-serif;">
				   Click <a href="./Year_end_2012.htm" target="_blank">here</a> for important 2012 year-end information <br />
				</p-->
				<p style="font: 12pt Arial,sans-serif">
					eFIN is the University of Calgary's <strong>view only</strong> Research Accounting 
					project reporting system.
				</p>
				

				<p style="font: bold 12pt Arial,sans-serif;">
					eFIN Enhancements:
				</p>
				<ul style="font: 12pt Arial,sans-serif;">
					<li><img src="Images/new.jpg" width="25px" alt="New!" style="vertical-align: middle" />
						Need help using eFIN?  Click <a href="https://breeze.ucalgary.ca/efinoverview/" target="_blank">
						here</a> to get an online overview of the eFIN Application</li>
					<li><img src="Images/new.jpg" width="25px" alt="New!" style="vertical-align: middle" />
						Ability to export data to MS Excel</li>
					<li><img src="Images/new.jpg" width="25px" alt="New!" style="vertical-align: middle" />
						Project status displayed as active, closed, or suspended on the Project List page</li>
					<li><img src="Images/new.jpg" width="25px" alt="New!" style="vertical-align: middle" />
						Printer-friendly colors</li>
					<li><img src="Images/new.jpg" width="25px" alt="New!" style="vertical-align: middle" />
						Wage encumbrances are now available</li>
				</ul>
				<p style="font: bold 12pt Arial,sans-serif">
					Helpful Hints:
				</p>
				<ul style="font: 12pt Arial,sans-serif;">
					<li>Closed projects with a zero balance and no transactions for 2 years are archived 
						and will not show up on eFIN’s Project List page.</li>
					<li>A virtual demonstration about the new Research Accounting reports is available
						<a href="https://breeze.ucalgary.ca/rareports" target="_blank">here</a></li>
				</ul>                
				<p style="font: bold 12pt Arial,sans-serif;">
					Delays in Development or Delivery:
				</p>
				<ul style="font: 12pt Arial,sans-serif;">
					<li>An enhanced version of the information regarding agency reporting, invoicing and progress reporting is currently under development.</li>
				</ul>
				<ul style="font: 12pt Arial,sans-serif;">
					<li>The University of Calgary is currently experiencing problems where encumbrance 
						balances on closed PO’s and cancelled requisitions are being shown on eFIN. 
						Also, encumbrances are not present on account 18900 due to a problem with the 
						Commitment Control ledger. There is a dedicated team of individuals that seek to 
						have these problems rectified during fiscal 2015.</li>
				</ul>                
				<p style="font: bold 12pt Arial,sans-serif;">
					Please refer back to this page for up-to-date information on the above known delays.
				</p>
				<br />
				
				<p style="font: bold 12pt Arial,sans-serif;">
					Need Help?
				</p>
				<p style="font: 12pt Arial,sans-serif;">
					The Integrated Service Centre (ISC) can help you with any queries you have pertaining to Research Accounting, Human Resources, or Supply Chain Management.  
					You can walk in at Mackimmie Library Tower, Ground Floor to meet with a Service Agent, or at Health Sciences G 383 at Foothills Campus.  
					Service Agents can  assist you with questions on projects, procurement, accounts payable, or HR. The ISC also provides one on one training for new eFIN users,
					 as well as users of PeopleSoft.  To contact the ISC, please e-mail at <a href="mailto:rtahelp@ucalgary.ca">rtahelp@ucalgary.ca</a>, <a href="mailto:scmhelp@ucalgary.ca">scmhelp@ucalgary.ca</a>, or <a href="mailto:hr@ucalgary.ca">hr@ucalgary.ca</a>; or phone (403) 220-5611; or drop by.  
				</p>               
			</TD>
		</TR>
		<tr>
			<td align="center" style="font-family: Arial">
				<asp:button id="btnContinue" runat="server" Text="Continue"></asp:button></td>
		</tr>
	</TABLE>
</asp:Content>

