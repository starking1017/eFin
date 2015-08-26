<%@ Page Language="VB" MasterPageFile="~/EfinSecurity.master" AutoEventWireup="false" CodeFile="frm_Security_Upload.aspx.vb" Inherits="Forms_Admin_frm_Security_Upload" title="eFIN Security Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" style="WIDTH: 1258px">
                <table border="0" cellpadding="0" cellspacing="0" width="70%">
                    <tr>
                        <td align="left" bgcolor="#3366FF" colspan="2" height="25">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" 
                                Text="Upload Batch File"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" height="10">
                        </td>
                        <td height="10" >
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="10">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="10">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="width:100%;" width="500px">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbMessage" runat="server" Font-Bold="False" 
                                                    Text="Choose Batch File to Upload:" Width="176px"></asp:Label>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;<input ID="upldBatchUploadFile" runat="server" style="WIDTH: 400px" 
                                                    type="file" />&nbsp;&nbsp;
                                                <asp:Button ID="btnUpload" runat="server" CssClass="ButtonStyle" Text="Upload" 
                                                    Width="74px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" height="20">
                                                
                                                <asp:Label ID="lblUploadResult" runat="server"></asp:Label>
                                                <br />
                                                <asp:Panel ID="pnlProcess" runat="server">
                                                    <div style="text-align: center">
                                                        <asp:Button ID="btnProcess" runat="server" CssClass="ButtonStyle" 
                                                            Text="Process" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="btnNotProcess" runat="server" CssClass="ButtonStyle" 
                                                            Text="Don't Process" />
                                                    </div>
                                                    <br />
                                                    <asp:DataGrid ID="dgSigningAuthorities" runat="server" AllowPaging="True" 
                                                        AllowSorting="True" AutoGenerateColumns="False" CssClass="gridStyle" 
                                                        DataKeyField="UCID" Width="100%">
                                                        <itemstyle CssClass="itemStyle" />
                                                        <headerstyle CssClass="headerStyle" />
                                                        <columns>
                                                            <asp:TemplateColumn HeaderText="Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbType" runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_Type") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_Type") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="UCID" HeaderText="UCID"></asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="Given Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGivenName" runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_Given_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_Given_Name") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Family Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFamilyName" runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_Family_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_Family_Name") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Start Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbStartDate" runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_Start_Date", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_Start_Date", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="End Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbEndDate" runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_End_Date", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox runat="server" 
                                                                        Text='<%# DataBinder.Eval(Container, "DataItem.Member_End_Date", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:templatecolumn HeaderText="Option">
                                                                <itemstyle horizontalalign="Center" />
                                                                <itemtemplate>
                                                                    <asp:LinkButton ID="lbtEdit" runat="server" CausesValidation="false" 
                                                                        CommandName="Edit" Text="[Edit]"></asp:LinkButton>
                                                                </itemtemplate>
                                                                <edititemtemplate>
                                                                    &nbsp;
                                                                </edititemtemplate>
                                                            </asp:templatecolumn>
                                                        </columns>
                                                        <pagerstyle CssClass="selectedPageStyle" HorizontalAlign="Center" 
                                                            mode="NumericPages" />
                                                    </asp:DataGrid>
                                                </asp:Panel>
                                                
                                            </td>
                                        </tr>                                        <tr>
                                            <td align="right" colspan="2" height="20">
                                                <hr />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnUpload" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnProcess" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="2" style="HEIGHT: 63px">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
                                <ProgressTemplate>
                                    <img alt="d" src="../../Images/animated.gif" /><br />
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red" 
                                        Text="It is uploading ... ..."></asp:Label>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                           </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="HEIGHT: 16px">
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

