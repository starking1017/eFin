<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_Error.aspx.vb" Inherits="frm_Error" %>

<%@ Register Src="Controls/Common/Banner.ascx" TagName="Banner" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
       <!--[if IE]>
    <style type="text/css">
    * html #RootConatainer{	height:100%;}
    </style>
    <![endif]-->
        <link href="Style.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 7.]>
    <script defer type="text/javascript" src="../../pngfix.js"></script>
    <![endif]-->
    <title>Error Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    			<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="80%" align="center" border="0">
				<TR>
					<TD align="center">
                        <uc1:Banner ID="Banner1" runat="server" />
                    </TD>
				</TR>
				<TR>
					<TD align="center">
						<P>&nbsp;</P>
						<P><FONT face="Arial" color="#ff0033">
								<asp:Label id="lblError" runat="server"></asp:Label></FONT></P>
						<P>
							<asp:LinkButton id="lnkbtnLogin" runat="server" Font-Names="Arial" Font-Size="X-Small">Back to Home Page</asp:LinkButton></P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
    </div>
    </form>
</body>
</html>
