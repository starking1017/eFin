<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectTeamMember.ascx.vb" Inherits="Controls_ProjectTeamMember" %>

<P align="center"><B><FONT face="Verdana" size="2">Project Team</FONT></B></P>
<P align="center">
	<asp:DataGrid id="dgrdTeamMember" BorderWidth="1px" BorderColor="Black" AutoGenerateColumns="False"
		runat="server" Width="80%" Font-Names="Verdana" Font-Size="X-Small" CssClass="GridViewStyle">
		<Columns>
			<asp:TemplateColumn HeaderText="Name">
				<EditItemTemplate>
					<asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Member_Name") %>'>
					</asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Member_Name") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Type">
				<EditItemTemplate>
					<asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Member_Type") %>'>
					</asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:HyperLink ID="hypType" runat="server" Target="_top" Visible="False">HyperLink</asp:HyperLink>
					<asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Member_Type") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="Contact Information">
				<EditItemTemplate>
					<asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Member_Contact") %>'>
					</asp:TextBox>
				</EditItemTemplate>
				<ItemTemplate>
					<asp:HyperLink ID="hypEmail" runat="server">hypEmail</asp:HyperLink>
					<br />
					<asp:Label ID="lblContact" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Member_Contact") %>'>
					</asp:Label>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
		<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
		<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" BackColor="#699BCD"></HeaderStyle>
		<ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
		<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
		<SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
	</asp:DataGrid>