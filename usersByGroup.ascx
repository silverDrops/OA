<%@ Control Language="C#" AutoEventWireup="true" CodeFile="usersByGroup.ascx.cs" Inherits="usersByGroup" %>
				<ul>
					<li id="leaderLi"  runat="server"><asp:Literal ID="leaderLitr" runat="server"></asp:Literal><span>组长</span></li>                      
                    <asp:Repeater ID="menberRpt" runat="server">
                   <ItemTemplate>
					<li><%#Eval("realName") %></li>
                    </ItemTemplate> 
                    </asp:Repeater>
				</ul>