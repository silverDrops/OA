<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NoticeMidList.ascx.cs" Inherits="NoticeMidList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
           <div id="mid_list"class="notice_content">
			    <ul id="mid_list_notice" runat="server">
                <asp:Repeater ID="noticeRptList" runat="server" OnItemCommand="noticePpt_ItemCommand" >
                <ItemTemplate>
                        <li>
                        <asp:CheckBox ID="NoticeCheckBox" runat="server" Text='<%#Eval("id") %>'  style="display:none; color:rgba(0, 0, 0,0);"/>
                        <asp:Button CommandArgument='<%#Eval("id") %>' CommandName="noticeId" class="mid_list01" runat="server" Text='<%#Eval( "title")%>'/>
                        <span class="mid_list02"><%#Eval( "poster") %></span>
                        <span class="mid_list03"><%#DataBinder.Eval(Container.DataItem, "postDate", "{0:yyy.MM.d}")%></span>
                        </li>
                </ItemTemplate>
                </asp:Repeater>
                </ul>
              <div class="notice_content" id="noticeContent" runat="server" visible="false">				
					<h1  id="noticeTitleSpan" runat="server" style="text-align:center; font-size:x-large;"></h1>
                   <h2 style="float:right;"><span id="posterSpan" runat="server" style="margin-right:20px;" ></span><span id="postDateSpan" runat="server"></span> </h2><br />
                    <p id="contentP" runat="server">
                    </p>
                    dasfasdfasdf【史上最“堕落”的警局官网】看看美国州立密尔沃基警局的官网，一个好端端的警局网站，弄得跟拍电影似的，不务正业，花里胡哨，完全没有人民公仆的样子，大家都去批判下美帝的堕落吧！（据报道，这是由密尔沃基当地一家创意设计公司的公益作品，免费提供给警局使用）
					【史上最“堕落”的警局官网】看看美国州立密尔沃基警局的官网，一个好端端的警局网站，弄得跟拍电影似的，不务正业，花里胡哨，完全没有人民公仆的样子，大家都去批判下美帝的堕落吧！（据报道，这是由密尔沃基当地一家创意设计公司的公益作品，免费提供给警局使用）
					【史上最“堕落”的警局官网】看看美国州立密尔沃基警局的官网，一个好端端的警局网站，弄得跟拍电影似的，不务正业，花里胡哨，完全没有人民公仆的样子，大家都去批判下美帝的堕落吧！（据报道，这是由密尔沃基当地一家创意设计公司的公益作品，免费提供给警局使用）
					【史上最“堕落”的警局官网】看看美国州立密尔沃基警局的官网，一个好端端的警局网站，弄得跟拍电影似的，不务正业，花里胡哨，完全没有人民公仆的样子，大家都去批判下美帝的堕落吧！（据报道，这是由密尔沃基当地一家创意设计公司的公益作品，免费提供给警局使用）
                    <a href="#" class="notice_content" runat="server" onserverclick="backList_Click">返回列表页</a>
				</div>
                </div>
			<ul id="change" runat="server" class="change"><!--分页工具，ButtonNameExtension是无效时的按钮图片名称最后的字 如firstD.png的D -->
                <webdiyer:AspNetPager ID="noticeAspNetPager" runat="server" AlwaysShowFirstLastPageNumber="true" NumericButtonType="Text"
                     MoreButtonType="Text" PageSize="10" AlwaysShow="true" PagingButtonType="Image" ButtonImageNameExtension="n" DisabledButtonImageNameExtension="D"
                  OnPageChanged="OnPageChanged"  CurrentPageButtonClass="currentPageBtn"  width="100%" ImagePath="../OA/images/" ButtonImageExtension=".png" >
                </webdiyer:AspNetPager>
			</ul>
