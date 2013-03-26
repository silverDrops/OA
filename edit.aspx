<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="edit" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"  TagPrefix="Upload" %>
<%@ Register src="Header.ascx" tagname="Header" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>项目编辑</title>
<link rel="stylesheet" type="text/css" href="css/head.css" />
<link rel="stylesheet" type="text/css" href="css/reset.css" />
<link rel="stylesheet" type="text/css" href="css/edit.css" />
</head>
<body>
    <form id="form1" runat="server">
     <input id="groupProLenHid" type="hidden" runat="server" />
     <input id="stepHiddens" type="hidden" runat="server" />
     <input id="itemMessageIdHidden" type="hidden" runat="server" />
        <uc2:Header ID="Header1" runat="server" />
	<div id="main">   
		<h1>编辑项目-<span><asp:Literal ID="itemNameLitr" runat="server"></asp:Literal></span></h1>		
		<h3>基本信息</h3>
        <div id="detail">      
		<p><span>任务负责人：<input id="leaderInput" runat="server" onkeyup= "AddLength(this)" size="8" readonly="readonly" onchange="CheckNull(this, leaderW)"/></span><span id="leaderW" class="warning">*</span>
        <span>网站链接：<input type="text" id="linkInput" runat="server" onkeyup= "AddLength(this)" size= "20" readonly="readonly" onchange="CheckNull(this,linkW)"/></span><span  id="linkW" class="warning">*</span></p>
		<p><span>委托方：<input type="text" id="clientInput" runat="server" onkeyup= "AddLength(this)"  size= "12" readonly="readonly" onchange="CheckNull(this, clientW)"/></span><span id="clientW" class="warning">*</span>
        <span>金额：<input id="moneyInput" type="text" runat="server" onkeyup= "AddLength(this)" readonly="readonly" onchange="CheckNull(this,moneyW)&&isDigit(this,moneyW,0)"/>￥</span><span id="moneyW" class="warning">*</span>
        <span>完成时间：<input id="yearIn" type="text" runat="server" class="ten" readonly="readonly" onchange="CheckNull(this,dateW)&&isDigit(this,dateW,0)"/>.<input id="monthIn" type="text" runat="server" class="five" readonly="readonly" onchange="CheckNull(this,dateW)&&isDigit(this,dateW,12)"/>.<input id="dayIn" type="text" runat="server" class="five" readonly="readonly" onchange="CheckNull(this,dateW)&&isDigit(this,dateW,31)" /></span><span id="dateW" class="warning">*</span></p>
		<h3 id="for_itemDetials">项目描述:</h3>
		<p>
        <textarea  id="itemDetialsIn" type="text" runat="server" onblur="CheckGrossChar(this,for_itemDetials,'项目描述', 200);" disabled="disabled"></textarea>
		</p>
        </div>
		<h3>总进度条:<span class="program_progress" id="programProgress" runat="server"></span></h3>
            <div class="all_progress">
			<div class="all_progress_bg" id="itemProgress" runat="server"></div>
		</div>
		<h3><asp:Literal ID="groupNameLitr" runat="server"></asp:Literal></h3>
		<div class="progress" id="progress">
			<div class="progress_bg" id="groupProgress" runat="server">          
			</div>
            <div id="stepDiv">
            <asp:Repeater ID="stepRpt" runat="server">
            <ItemTemplate>
			 <input type="text" value="<%#Container.DataItem %>" onkeyup= "AddLength(this)" ondblclick="SetGroupProgressLength(this);" onblur="SetInputWidth(this);" title="双击改变进度条" />
             </ItemTemplate>
              </asp:Repeater>
              </div>  
             <span class="groupFinish" ondblclick="SetGroupFinish();" title="双击改变进度条">完成</span>
		</div>
		<h3 id="h3_03">留言</h3>
		<div class="message">
			<span class="message_date"><asp:Literal ID="messageDateLtr" runat="server"></asp:Literal></span>
			<p><textarea id="messageTextarea" type="text" runat="server" disabled="disabled" onfocus="select()" onblur="CheckGrossChar(this,h3_03,'留言', 200); "></textarea>
			<span class="message_edit"><a id="deleteMess" onclick="DeleteMess();">删除</a>|<a id="editMessge" onclick="SetAbled();">编辑</a></span>
			<span class="clear" ></span>
			</p>
		</div><!--end of message-->
        <div id="itemComplete" runat="server" visible="false">
           <p><Upload:InputFile ID="InputFile" runat="server" onchange="fileSpan.innerText='附件：'+value"/>
          <a id="selectFile" href="javascript:void(0)" class="button2">选择文件</a><span id="fileSpan" runat="server">附件：未选择文件</span>      
		<input type="image" src="images/uploading.png" id="uploading" runat="server" onserverclick="uploading_Click" onclick="return CheckFile();"/></p> 
         <p><Upload:ProgressBar ID="ProgressBar" runat='server' Inline="true"></Upload:ProgressBar></p>      
		<input type="image" src="images/consign.png" id="consign" alt="交付项目" runat="server" onserverclick="consignBtn_Click"/>
        </div>
		<input type="image" src="images/save.png" id="submitBtn" runat="server" onserverclick="submitBtn_Click" 
        onclick="SetHidden();return CheckGrossChar(itemDetialsIn,for_itemDetials,'项目描述', 200)&&CheckGrossChar(messageTextarea,h3_03,'留言', 200)&&CheckNull(leaderInput,leaderW)&&CheckNull(clientInput,clientW)&&CheckNull(moneyInput,moneyW)&&isDigit(moneyInput,moneyW,0);"/>
		<br class="clear" />
	</div>
</form>
 <script type="text/javascript" src="js/edit.js"></script>
</body>
</html>