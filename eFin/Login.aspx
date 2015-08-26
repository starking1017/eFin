<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<%@ Register Src="Controls/Common/Banner.ascx" TagName="Banner" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" align="center" border="0" cellpadding="0" cellspacing="0" width="80%">
            <tr>
                <td align="center">
                    &nbsp;
                    <uc1:Banner ID="Banner1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    &nbsp;<table id="Table2" align="center" cellpadding="0" cellspacing="0" style="border-right: black 1px solid;
                        border-top: black 1px solid; border-left: black 1px solid; width: 300px; border-bottom: black 1px solid;
                        border-collapse: collapse; height: 16px">
                        <tr>
                            <td align="left">
                                <asp:TextBox ID="txtUCID" runat="server" Visible="False" Width="152px"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
