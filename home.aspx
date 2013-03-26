<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>
<%@ Register src="combox.ascx" tagname="combox" tagprefix="uc1" %>
<%@ Register src="Header.ascx" tagname="Header" tagprefix="uc2" %>
<%@ Register src="ItemMidList.ascx" tagname="ItemMidList" tagprefix="uc3" %>
<%@ Register src="NoticeMidList.ascx" tagname="NoticeMidList" tagprefix="uc4" %>
<%@ Register src="FinancialMidList.ascx" tagname="FinancialMidList" tagprefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>数字中心内部网</title>
<link rel="stylesheet" type="text/css" href="css/head.css" />
<link rel="stylesheet" type="text/css" href="css/reset.css" />
<link rel="stylesheet" type="text/css" href="css/home.css" />
</head>
<body>
    <form id="form1" runat="server">
    <input id="userRoleHi" runat="server" type="hidden" />
    	<div id="edit"><!--这个是隐藏的项目创建页面-->
		<div id="create">
			<h1><img alt="cancel" src="images/cancle.png" onclick="cancleCreate()"/>创建项目</h1>
			<div id="project_list">
        <asp:ScriptManager ID="scriptManager" runat="server"></asp:ScriptManager>
       <asp:UpdatePanel ID="createItemList" runat="server">
       <ContentTemplate>
				<table border="none">        
				<tr>
				<td></td>
				<td><label for="principle">任务负责人</label><uc1:combox  name="principle" id="principle" runat="server" />
               <span id="principle_TextBox1Span">*</span>
                </td>
				<td><label for="project_name">项目名</label><asp:TextBox name="project_name" id="project_name" runat="server" onblur="CheckNull(this)"/>
                <span id="project_nameSpan">*</span>
            	</td>
                <td> </td>
				</tr>
				<tr>
				<td> </td>
				<td><label for="bailer">委托方</label><asp:TextBox name="bailer" id="bailer" runat="server" onblur="CheckNull(this)" />
                <span id="bailerSpan">*</span>
                </td>
				<td><label for="money">金额<span id="moneyDigitSpan">只能是数字</span></label>
                <asp:TextBox name="money" id="money" runat="server" onblur="CheckNull(this)&&isDigit(money,moneyDigitSpan,0)"/>￥
                <span id="moneySpan">*</span>
                </td>
				<td> </td>
				</tr>
				<tr>
				<td><label for="pingmian">平面组人选</label><uc1:combox  name="pingmian" id="pingmian" runat="server"/></td>
				<td><label for="qianduan">前端组人选</label><uc1:combox name="qianduan" id="qianduan" runat="server" /></td>
				<td><label for="flashzu">flash组人选</label><uc1:combox name="flashzu" id="flashzu" runat="server" /></td>
				<td><label for="houtai">后台组人选</label><uc1:combox name="houtai" id="houtai" runat="server" /></td>
				</tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                               <label for="year"> 完成时间<span id="yearDigitSpan">只能是数字</span></label><asp:TextBox  name="year" id="year" runat="server" onblur="return CheckNull(year)&&isDigit(year,yearDigitSpan,0);"/><span id="yearSpan">*</span></td>
                            <td>
                                -<asp:TextBox  name="month" class="due_date" id="month" runat="server" onblur="return CheckNull(month)&&isDigit(month,yearDigitSpan,12);"/>-<asp:TextBox  name="day" id="day" class="due_date" runat="server" onblur="return CheckNull(day)&&isDigit(day,yearDigitSpan,31);" /><span id="daySpan">*</span><span id="monthSpan">*</span>
                            </td>
                            <td>
                            </td>
                        </tr>
                </table>  
            </ContentTemplate>
                </asp:UpdatePanel>            
				<div>
				<label for="decoration" id="for_decoration">项目描述</label>
				<textarea rows="7" cols="60" id="decoration" runat="server" onblur="CheckGrossChar(this,for_decoration,'项目描述',200);"></textarea>
                <asp:ImageButton ID="itemSubmitBtn" name="finish"  ImageUrl="images/finish.png" runat="server" OnClick="itemSubmitBtn_click" OnClientClick="return CheckAll()&&isDigit(money,moneyDigitSpan,0)&&CheckGrossChar(decoration,for_decoration,'项目描述',200);" />
				</div>
			</div>
		</div><!--end of create -->
        <div id="create_financial">
			<h1><img alt="cancle" src="images/cancle.png" onclick="cancleCreate()"/>财务添加</h1>
			<div>
			<label for="financial_event">事件</label>
			<input type="text" id="financial_event"  onblur="CheckNull(this)" runat="server"/>
            <span id="financial_eventSpan" >*</span>
			<label for="financial_decoration" id="for_financial_decoration">清单</label>
			<textarea rows="5" cols="39" id="financial_decoration" runat="server" onblur="CheckGrossChar(this,for_financial_decoration,'清单',50);"></textarea>
			<label>时间</label>
			<input type="text" id="get_year" runat="server"/>
			<input type="text"  id="get_month" runat="server"  />
			<input type="text"  id="get_day" runat="server"  />
            <label>金额</label>
            <asp:DropDownList ID="inOrOutDropL" runat="server">
             </asp:DropDownList>
            <input type="text" id="moneyIn" runat="server" onblur="CheckNull(this)&&isDigit(e, moneyInSpan,0)" />￥
            <span  id="moneyInSpan">*只能是数字</span>
			<input type="image" id="FinancialSubmit" src="images/finish.png" runat="server" onserverclick="FinancialSubmit_Click" onclick="return CheckTwo(financial_event,moneyIn)&&CheckGrossChar(financial_decoration,for_financial_decoration,'清单',50);"/>
			</div>
		</div><!--end of create_financial -->
		<div id="create_notice">
			<h1><img alt="cancle" src="images/cancle.png" onclick="cancleCreate()"/>发布公告</h1>
			<div>
				<label for="notice_heading">标题</label>
				<input type="text" id="notice_heading" runat="server" onblur="CheckNull(this)" />
                <span id="notice_headingSpan">*</span>
				<label for="notice_decoration">正文</label>
				<textarea rows="13" cols="45" id="notice_decoration" runat="server"></textarea>
				<input type="image" id="NoticeSubmit" src="images/finish.png" runat="server" onclick="return CheckNull(notice_heading);" onserverclick="NoticeSubmit_Click"/>
			</div>
		</div><!--end of create_notice-->  
	</div><!--end of edit -->
    <uc2:Header ID="Header1" runat="server" />
	<div id="main">
		<div id="left_sidebar">                        
         <asp:UpdatePanel ID="userPreOrNext" runat="server">
                        <ContentTemplate>
			<div id="condition">
				<ul>
					<li id="allLi" runat="server"><a id="allUserA" runat="server" onserverclick="conditionA_Click" href="javascript:void(0)">全部</a></li>
					<li id="freeLi" runat="server"><a id="freeUserA" runat="server" onserverclick="conditionA_Click" href="javascript:void(0)">空闲</a></li>
					<li id="busyLi" runat="server"><a id="busyUserA" runat="server" onserverclick="conditionA_Click" href="javascript:void(0)">忙碌</a></li>
				</ul>
			</div>
			<div id="member">
				<h3 id="managerH3" onclick="window.location='manager.aspx'" title="前往人员管理" onmouseover="this.innerText='前往人员管理';" onmouseout="this.innerText='成员状态';" >成员状态</h3>
				<ul>
                <asp:Repeater ID="userRptList" runat="server">
                <ItemTemplate>
					<li class="member<%#Eval("status") %>"><%#Eval("groups")%>/<%#Eval("realName") %></li>
                    </ItemTemplate>
               </asp:Repeater>
				</ul>
				<div class="gray_bottom">
					<span>
                        <asp:ImageButton ID="prePageBtn" runat="server" ImageUrl="~/images/change_prev.png"
                        onclick="prePageBtn_Click"></asp:ImageButton></span>
					<span><asp:ImageButton  ID="nextPageBtn" runat="server" ImageUrl="~/images/change_next.png"
                        onclick="nextPageBtn_Click"></asp:ImageButton></span>
				</div>
			</div><!--end of member-->
              </ContentTemplate>
                        </asp:UpdatePanel>
		</div><!--end of left_sidebar-->
		<div id="middle">
			<div id="middle_head"><!--中间的上面公用部分-->
				<span id="complete" class="new_project">
                <asp:Button ID="completedBtn" class="new_project" Text="查看已完成" runat="server" OnClick="completedBtn_Click"/>
                </span>
				<span class="new_project" id="showEdit">创建新的项目</span>
                <span id="deleteFinanceSpan" class="new_project">
                <input class="new_project" id="deleteFinanceBtn" type="submit" value="删除" runat="server" onserverclick="deleteFinanceBtn_Click" />
                </span>
                <span id="notComeFinanceSpan" class="new_project">
                <input class="new_project" id="notComeFinance" type="submit" value="查看未发放" runat="server" onserverclick="notComeFinance_Click" />
                </span>
				<div id="mid_heading"><!--中间的页首-->
					<div id="project"></div>
                    <span class="project_head project01">项目负责人</span><span class="project_head project02">剩余时间</span><span class="project_head project03">进度</span>
				</div>
			</div><!--end of middle_head-->           
         <asp:UpdatePanel ID="miItemUpdatePanel" runat="server">
            <ContentTemplate>
                   <uc3:ItemMidList ID="itemMidList1" runat="server" />
                <uc4:NoticeMidList ID="NoticeMidList1" runat="server" Visible="false"/>
                <uc5:FinancialMidList ID="FinancialMidList1" runat="server" Visible="false" />
        </ContentTemplate>
            </asp:UpdatePanel>
		</div><!--end of middle-->
		<div id="right_sidebar">
        <asp:UpdatePanel ID="noticeUPanel" runat="server">
        <ContentTemplate>
			<div id="notice">
				<button type="button" id="noticeBtn" runat="server" onclick="changeNotice();"  onserverclick="midListChange_Click" causesvalidation="false"><h3>公告板</h3></button>
				<ul>
                <asp:Repeater ID="announceRptList" runat="server" OnItemCommand="noticeRpt_ItemCommand">
                <ItemTemplate>
					<li><asp:Button CommandArgument='<%#Eval("id") %>' Text='<%#Eval("title") %>' runat="server"  OnClientClick="changeNotice();"/><span><%#DataBinder.Eval(Container.DataItem,"postDate","{0:MM.d}") %></span></li>
                    </ItemTemplate>
                    </asp:Repeater>
				</ul>
				<div id="new_notice"> 
					<p><button type="button" name="notice" onclick="ShowCreate_notice()" >发布</button>
                    <a id="moreNoticeA" onclick="changeNotice();"  onserverclick="midListChange_Click" causesvalidation="false" runat="server">MORE</a></p>
				</div>
			</div><!--end of notice-->
			<div id="finance">
				<button type="button" id="financeBtn" runat="server" onclick="changeFinance();"  onserverclick="midListChangeFinance_Click" causesvalidation="false"><h3>财务状况</h3></button>
				<div  id="in_finance">
					<p onclick="showIncome( )" class="on">收入</p><p onclick="showOutlay( )">支出</p>
					<br class="clear" />
					<ul id="income" class="finance_list">
                    <asp:Repeater ID="financeInRptList" runat="server">
                    <ItemTemplate>
						<li><a href="javascript:void(0)"><%#Eval("actionName") %></a><span><%#Eval("money") %>￥</span></li>
						</ItemTemplate>
                       </asp:Repeater>
                        <li><a href="javascript:void(0)" class="more" id="financeInMore" runat="server" onclick="changeFinance();"  onserverclick="midListChangeFinance_Click" >More</a></li>
					</ul>
					<ul id="outlay" class="finance_list">
                    <asp:Repeater ID="financeOutRptList" runat="server">
                    <ItemTemplate>
						<li><a href="javascript:void(0)"><%#Eval("actionName") %></a><span><%#Eval("money") %>￥</span></li>
                        </ItemTemplate>
                        </asp:Repeater>
                        <li><a href="javascript:void(0)" class="more" id="financeOutMore" runat="server" onclick="changeFinance();"  onserverclick="midListChangeOutFinance_Click">More</a></li>
					</ul>
				</div><!--end of in_finance-->
				<span><b>余额：<asp:Literal ID="remainLiteral" Mode="PassThrough" runat="server"></asp:Literal></b>￥</span>
				<div class="gray_bottom clear"></div>
			</div>
       </ContentTemplate>
            </asp:UpdatePanel>
		</div><!--end of right_sidebar-->
		<br class="clear" />
	</div><!--end of main-->
	<script src="js/home.js" type="text/javascript"></script>
    </form>
</body>
</html>

