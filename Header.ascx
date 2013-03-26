<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="Header" %>
  	<div id="header">
        <div id="out"> <img src="images/exit_pic.png" /><asp:Button ID="OutButton" runat="server" Text="登出"  onclick="OutButton_Click"  OnClientClick="return confirm('确定要登出？')" /></div> 
		<span>Vitanmin Source Studio</span>
        <a href="home.aspx" class="home_page"  title="前往首页"></a>
		<div id="menu_block">
		<ul class="menu">
          <asp:Repeater ID="userItemRptList" runat="server">
           <ItemTemplate>
			<li><a href="edit.aspx?id=<%#Eval("id") %>"><%#Eval("itemName") %></a></li>
            </ItemTemplate>
            </asp:Repeater>
			<li><asp:Literal ID="userNameLiteral" runat="server" Mode="PassThrough"></asp:Literal></li><!--登录人员姓名-->
		</ul>
		</div>
	</div><!--end of header-->