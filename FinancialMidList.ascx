<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FinancialMidList.ascx.cs" Inherits="FinancialMidList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
 <div id="mid_list"class="notice_content">
			    <ul id="mid_list_financial" runat="server">
                <asp:Repeater ID="FinanceRptList" runat="server" >
                <ItemTemplate>
                        <li>
                        <asp:CheckBox ID="FinanceCheckBox" runat="server" Text='<%#Eval("id") %>'  style="display:none; color:rgba(0, 0, 0,0);"/>
                        <a href="javascript:void(0)" class="mid_list01"> <%#Eval("actionName")%>
                        <ul>
                        <li><%#Eval("actionDetails") == "" ? "暂无" : Eval("actionDetails").ToString().Length >= 25 ? Eval("actionDetails").ToString().Substring(0, 25) : Eval("actionDetails")%></li>
                        <li><%#Eval("actionDetails").ToString().Length>25? Eval("actionDetails").ToString().Substring(25):""%></li>
                        </ul>
                        </a>
                        <span class="mid_list02"><%#Eval("money") %>￥</span>
                        <span class="mid_list03"><%#DataBinder.Eval(Container.DataItem,"actionDate","{0:MM.d}")%></span></li>
			    </ItemTemplate>
                </asp:Repeater>
                </ul>
              </div>
              	<ul id="change" runat="server" class="change"><!--分页工具，ButtonNameExtension是无效时的按钮图片名称最后的字 如firstD.png的D -->
                <webdiyer:AspNetPager ID="financeAspNetPager" runat="server" AlwaysShowFirstLastPageNumber="true" NumericButtonType="Text"
                     MoreButtonType="Text" PageSize="10" AlwaysShow="true" PagingButtonType="Image" ButtonImageNameExtension="n" DisabledButtonImageNameExtension="D"
                  OnPageChanged="OnPageChanged"  CurrentPageButtonClass="currentPageBtn"  width="100%" ImagePath="~/images/" ButtonImageExtension=".png" >
                </webdiyer:AspNetPager>
			</ul>
