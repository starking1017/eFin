<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectAwardData.ascx.vb" Inherits="Controls_ProjectAwardData" %>
<%@ Register TagPrefix="uc1" TagName="ProjectChartField" Src="ProjectChartField.ascx" %>

<P align="center"><B><FONT face="Verdana" size="2">Project Award Info</FONT></B></P>

<uc1:ProjectChartField ID="ProjectChartField1" runat="server" />

<asp:datagrid id="dgProjects" runat="server" AutoGenerateColumns="False" PageSize="50" Font-Names="Verdana" Width="80%" BorderColor="Black" BorderWidth="1px" 
											 BackColor="White" CellPadding="3" HorizontalAlign="Center" CssClass="GridViewStyle">
											<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
											<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
											<AlternatingItemStyle BackColor="#FFFFFF"></AlternatingItemStyle>
											<ItemStyle Font-Size="X-Small" ForeColor="Black" BackColor="#CCCCCC" HorizontalAlign="Right" ></ItemStyle>
   
	<HeaderStyle Font-Size="X-Small" Font-Bold="True" ForeColor="White" BackColor="#699BCD" HorizontalAlign="Center" ></HeaderStyle>
		<Columns>
			<asp:BoundColumn DataField="fld_Year" HeaderText="Project Year">
				<ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
			</asp:BoundColumn>
			<asp:TemplateColumn HeaderText="Budgeted">
				<ItemTemplate>
					<asp:Label ID="lblBudget" runat="server"></asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Received">
				<ItemTemplate>
					<asp:Label ID="lblReceived" runat="server" ></asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Variance">
				<ItemTemplate>
					<asp:Label ID="lblVariance" runat="server"></asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	<PagerStyle Font-Size="X-Small" HorizontalAlign="Center" ForeColor="Black" Position="TopAndBottom" BackColor="White" Mode="NumericPages"></PagerStyle>
</asp:datagrid>

			<p>
                &nbsp;</p>
		<asp:Panel ID="panProject" runat="server" HorizontalAlign="center" Width="80%"><p align="left"><font face="Verdana" size="2">Notes: </font></p>
            <p align="left">
                <font face="Verdana" size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 1.&nbsp;&nbsp;&nbsp; Top out a January 1, 2000.</font></p>
            </asp:Panel>
		