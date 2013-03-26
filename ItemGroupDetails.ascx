<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemGroupDetails.ascx.cs" Inherits="ItemGroupDetails" %>
<div class="grouping">
			<h3 ><asp:Literal ID="groupNameLitr" runat="server"></asp:Literal></h3>           
			<div class="progress" id="progress" >
				<div class="progress_bg" ID="programProgress" runat="server">
				</div>
                <asp:Repeater ID="stepRp" runat="server">
				<ItemTemplate>
                <span><%# Container.DataItem%></span>
                </ItemTemplate>     
                </asp:Repeater>
               <asp:Literal ID="finishLitr" runat="server"><span class="groupFinish">完成</span></asp:Literal>
			</div>
            <h3 class="h3_03">留言</h3>
			<div class="message" id="message" runat="server">
                  <span class="message_date"><asp:Literal ID="postDateLitr" runat="server"></asp:Literal></span>
				<p><asp:Literal ID="messageLitr" runat="server"></asp:Literal>
				</p>
            </div><!--end of message-->    
		</div><!--end of grouping-->
