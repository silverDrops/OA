<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manager.aspx.cs" Inherits="managerspx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="usersByGroup.ascx" tagname="usersByGroup" tagprefix="uc1" %>
<%@ Register src="Header.ascx" tagname="Header" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>人员管理</title>
<link rel="stylesheet" type="text/css" href="css/head.css" />
<link rel="stylesheet" type="text/css" href="css/reset.css" />
<link rel="stylesheet" type="text/css" href="css/manager.css" />
</head>
<body>
    <form id="form1" runat="server">
    <input id="menberHid" type="hidden" runat="server" />
    <input id="editFlag" type="hidden" runat="server" />
    <input id="changeMenHid" type="hidden" runat="server" />
    <div id="edit"><!--这个是隐藏的项目创建页面-->
		<div id="create">
			<h1><img src="images/cancle.png" onclick="cancleCreate()"/>添加人员</h1>
			<ul >
				<li><label>姓名</label><input id="UserIn" type="text" runat="server" onblur="CheckNull(this)"/>
                <asp:RequiredFieldValidator ID="UserInSpan" runat="server" ControlToValidate="UserIn" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></li>
				<li><label>组别</label><asp:DropDownList ID="GroupDropList" runat="server"></asp:DropDownList></li>
				<li><label>届</label><input id="GradeIn" type="text" runat="server"  onblur="CheckNull(this)"/>
                <asp:RequiredFieldValidator ID="GradeInSpan" runat="server" ControlToValidate="GradeIn" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator></li>
				<li><label>职务</label><asp:DropDownList ID="RolesDropList" runat="server" ></asp:DropDownList></li>
                <li><label>学院</label><input ID="SchoolIn" type="text" runat="server" /></li>
				<li><label>专业</label><input ID="MajorIn" runat="server" type="text" /></li>
				<li><label>电话/短号</label><input type="text" id="LongNumIn" runat="server" class="LongNumIn" onblur="isDigit(this.value,NumSpan);"  />&nbsp;/&nbsp;
                <input type="text" id="ShortNumIn" runat="server" class="ShortNumIn" onblur="isDigit(this.value,NumSpan);"   />
                <span id="NumSpan" style=" visibility:hidden;color:Red;float:right;">电话号码只能是数字</span> </li>
				<li class="finishLi"><input type="image" src="images/finish.png" class="submit"  id="submit" runat="server" onserverclick="SubmitBtn_Click" onclick="return isDigit(LongNumIn.value,NumSpan)&&isDigit(ShortNumIn.value,NumSpan);"/></li>
			</ul>
		</div><!--end of create -->
	</div><!--end of edit -->	
    <div id="editPerson">
		<div id="create">
			<h1><img src="images/cancle.png" onclick="cancleChange()"/>修改个人资料</h1>
			<ul >
				<li><label>登录昵称</label><input id="loginNameIn" type="text" runat="server" onblur="CheckNull(this)"/>
                <span id="loginNameInSpan">*</span></li>
				<li><label>旧密码</label><input id="oldPwIn" type="password" runat="server"  onblur="CheckNull(this)"/>
                <span id="oldPwInSpan"> *</span></li>
				<li><label>新密码</label><input id="newPwIn" type="password" runat="server"  onblur="CheckNull(this)"/>
                <span id="newPwInSpan">*</span></li>
                <li><label>重复输入新密码</label><input id="reNewPwIn" type="password" runat="server"  onblur="CheckRePw(this,newPwIn)"/>
                <span id="reNewPwInSpan" >*</span><span id="PwCompareSpan" style="float:right; ">两次输入的密码不一致</span></li>
                <li><label>组别</label><asp:DropDownList ID="groupDrDL" runat="server"></asp:DropDownList></li>
				<li><label>电话/短号</label><input type="text" id="rLongNumIn" runat="server" class="LongNumIn" onblur="isDigit(this.value,rNumSpan);" />&nbsp;/&nbsp;              
                <input type="text" id="rShortNumIn" runat="server" class="ShortNumIn" onblur="isDigit(this.value,rNumSpan);" />
                <span id="rNumSpan">电话号码只能是数字</span> </li>
				<li class="finishLi">
                <input type="image" src="images/save.png" id="changPersonSubmit" runat="server" onserverclick="ChangPersonSubmitBtn_Click" class="submit" onclick="return CheckChangAll();"/>
                </li>
			</ul>
		</div><!--end of create -->
	</div><!--end of edit -->	
    <uc2:Header ID="Header1" runat="server" />
	<div id="main">
		<h1>管理页面</h1>
		<div id="left_side">
			<ul id="member">
				<li onclick="showUl(this,0)"><span></span><input type="button" causesvalidation="false" id="LeaderBtn" runat="server"  onserverclick="GroupBtn_Click" value="负责人"/>
                </li><uc1:usersByGroup ID="usersByLeaders" runat="server" />
				<li onclick="showUl(this,1)"><span></span><input type="button" causesvalidation="false" id="PingMainBtn" runat="server" onserverclick="GroupBtn_Click" value="平面组"/>
				</li> <uc1:usersByGroup ID="usersByPingMian" runat="server" />  
				<li onclick="showUl(this,2)"><span></span><input type="button" causesvalidation="false" id="QianDuanBtn" runat="server" onserverclick="GroupBtn_Click" value="前端组" />
                </li> <uc1:usersByGroup ID="usersByQianDuan" runat="server" />                			           
				<li onclick="showUl(this,3)"><span></span><input type="button" causesvalidation="false" id="FlashBtn" runat="server" onserverclick="GroupBtn_Click" value="flash组" />
				</li><uc1:usersByGroup ID="usersByFlash" runat="server" />  
				<li onclick="showUl(this,4)"><span></span><input type="button" causesvalidation="false" id="HouTaiBtn" runat="server" onserverclick="GroupBtn_Click" value="程序组" />
				</li><uc1:usersByGroup ID="usersByHouTai" runat="server" />  
				<li onclick="showUl(this,5)"><span></span><input type="button" causesvalidation="false" id="OldUserBtn" runat="server" onserverclick="GroupBtn_Click" value="往届人员" />
				</li> <uc1:usersByGroup ID="usersByOld" runat="server" />  
			</ul>
		</div><!--end of left -->
		<div id="right_side">
			<h4><asp:Literal ID="GroupNameLitr" runat="server"></asp:Literal>组
            <span id="bigSpan">
            <input type="button" id="ChangePersonalBtn" value="修改个人资料"  onclick="showEditPerson();"/>
            <span id="editDiv" runat="server" visible="false">
            |<input type="submit" id="DeleteBtn" runat="server" onserverclick="DeleteBtn_Click" causesvalidation="false"  value="删除"
             onclick="DeleteBtn_OnClick();return confirm('确定要删除所选人员？'); "/>
            |<input id="AddMenberBtn" type="button"  value="添加"/>
            </span>
            </span>
            </h4>
			<div id="information">
				<table>            
					<tr>
                    <th></th>
					<th>姓名</th><th>职务</th><th>年级</th><th>学院</th><th>专业</th><th>电话</th>
					</tr>
                    <asp:Repeater ID="userListRpt" runat="server">
                    <ItemTemplate>
					<tr>
				    <td style="width:2%;">
                     <input id="<%#Eval("id") %>" type="checkbox" style="display:none;width:20px"/>
                    </td>
                    <td><input type="text" id="realName" value='<%#Eval("realName") %>' readonly="readonly" runat="server"/></td>
                    <td><input id="roles" runat="server" value='<%#Eval("roles")%>' readonly="readonly" /></td><td>
                    <input id="grade" runat="server" value='<%#Eval("grade")%>' readonly="readonly" style="width:28px;"  />届&nbsp;</td>
                    <td><input id="school" runat="server"  value='<%#Eval("school")==""?"暂无":Eval("school")%>' readonly="readonly" /></td>
                    <td><input id="major" runat="server"  value='<%#Eval("major")==""?"暂无":Eval("major") %>' readonly="readonly"/></td>
                    <td style="width:19%;"><input id="longNumber" runat="server" value='<%#Eval("longNumber").ToString().Trim()==""?"暂无":Eval("longNumber")%>' readonly="readonly" style="width:96px;" />
                    <input id="shortNumber" runat="server" value='<%#Eval("shortNumber")==""?"暂无":Eval("shortNumber")%>' readonly="readonly" style=" width:50px;"/></td>
                    </tr>
  					</ItemTemplate>
                    </asp:Repeater>
				</table>
			</div><!--end of information-->
            <div id="pager" runat="server" visible="false">
           <webdiyer:aspnetpager ID="oldUserPager" runat="server"  HorizontalAlign="Right"  AlwaysShowFirstLastPageNumber="true" NumericButtonType="Text" 
                     MoreButtonType="Text" PageSize="8" AlwaysShow="true"  PagingButtonType="Image" ButtonImageNameExtension="n" OnPageChanged="OnPageChanged"  
            CurrentPageButtonClass="currentPageBtn"  width="100%" ImagePath="../OA/images/"  ButtonImageExtension=".png" DisabledButtonImageNameExtension="D" >
                </webdiyer:aspnetpager>
         </div>
			<input type="image" src="images/save.png" name="保存" id="save" runat="server"  onclick="return confirm('确定要保存所有的修改？'); " onserverclick="save_Click" />
		</div><!--end of right-->
	</div><!--end of main-->
    </form>
    <script type="text/javascript" src="js/manager.js"></script>
</body>
</html>
