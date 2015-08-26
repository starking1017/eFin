<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_ProgressPage.aspx.vb" Inherits="frm_ProgressPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<HEAD>
		<title>Retrieving, please wait...</title>
		<script>	
		var ctr = 1;
		var ctrMax = 50; // how many is up to you-how long does your end page take?
		var intervalId;		
		
		function Begin() 
		{
			//set this page's window.location.href to the target page
			window.location.href = "<%= Request.QueryString("destPage")%>";
			// but make it wait while we do our progress...
			
			intervalId = window.setInterval("ctr=UpdateIndicator(ctr, ctrMax)", 500);
		} 

		function End() {
		// once the interval is cleared, we yield to the result page (which has been running)
			window.clearInterval(intervalId);			
		}
	
		function UpdateIndicator(curCtr, ctrMaxIterations) 
		{		
			curCtr += 1;			
			if (curCtr <= ctrMaxIterations) {
				indicator.style.width =curCtr*10 +"px";
				return curCtr;
			}
			else 
			{
				indicator.style.width =0;
				return 1;
			}
		}
		</script>
	</HEAD>
	<body onload="Begin()" onunload="End()">
		<form id="Form1" method="post" runat="server">
			<font face="Verdana" size="3"><STRONG>
					<div align="center">
						<asp:Image id="Image1" runat="server" ImageUrl="Images/eFINLoadingImage.jpg"></asp:Image></div>
					<div align="center">&nbsp;</div>
				</STRONG></font>
			<DIV align="center"><FONT face="Verdana" size="3"><STRONG>Retrieving project information. 
						Please wait...</STRONG></FONT></DIV>
			<DIV align="center"><STRONG><FONT face="Verdana"></FONT></STRONG>&nbsp;</DIV>
			<table id="indicator" border="0" cellpadding="0" cellspacing="0" width="0" height="20"
				align="center">
				<tr>
					<td align="center" bgcolor="#699bcd" width="100%"></td>
				</tr>
			</table>
			<DIV></DIV>
		</form>
	</body>
</HTML>
