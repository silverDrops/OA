<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemMidList.ascx.cs" Inherits="ItemMidList" %>  
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
 <div id="mid_list"class="notice_content">
			    <ul id="mid_list_project" runat="server">
                <asp:Repeater ID="ItemRptList" runat="server" >
                <ItemTemplate>
                        <li><a href="read.aspx?id=<%#Eval("id") %>" class="mid_list01"> <%#Eval("itemName")%></a><span class="mid_list02"><%#Eval("leaderName")%></span><span class="mid_list03"><%#Eval("remainDay") %>天</span><span class="mid_list04"><%#Eval("status") %>%</span></li>
			    </ItemTemplate>
                </asp:Repeater>
                </ul>
              </div>
			<ul id="change" runat="server" class="change"><!--分页工具，ButtonNameExtension是无效时的按钮图片名称最后的字 如firstD.png的D -->
                <webdiyer:AspNetPager ID="itemAspNetPager" runat="server" AlwaysShowFirstLastPageNumber="true" NumericButtonType="Text"
                     MoreButtonType="Text" PageSize="10" AlwaysShow="true" PagingButtonType="Image" ButtonImageNameExtension="n" DisabledButtonImageNameExtension="D"
                  OnPageChanged="OnPageChanged"  CurrentPageButtonClass="currentPageBtn"  width="100%" ImagePath="../OA/images/" ButtonImageExtension=".png" >
                </webdiyer:AspNetPager>
			</ul>
