<%@ Page Language="C#" AutoEventWireup="true" CodeFile="read.aspx.cs" Inherits="read" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="ItemGroupDetails.ascx" tagname="ItemGroupDetails" tagprefix="uc1" %>
<%@ Register src="Header.ascx" tagname="Header" tagprefix="uc2" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload"  TagPrefix="Upload" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>详细内容</title>
    <link rel="stylesheet" type="text/css" href="css/head.css" />
<link rel="stylesheet" type="text/css" href="css/reset.css" />
<link rel="stylesheet" type="text/css" href="css/read.css" />
</head>
<body>
    <form id="form1" runat="server">
    <uc2:Header ID="Header1" runat="server" />
    <div id="main">
		<h1><a href="home.aspx"><img alt="cancel" src="images/cancle.png" /></a>项目状态-<asp:Literal ID="itemNameLitr" runat="server"></asp:Literal></h1>
		<h3>基本信息</h3>  
		<p><span>任务负责人：<asp:Literal ID="leaderLitr" runat="server"></asp:Literal></span>
        <span>网站链接：<asp:HyperLink ID="link" runat="server"><asp:Literal ID="linkLitr" runat="server"></asp:Literal></asp:HyperLink></span></p>
		<p><span>委托方：<asp:Literal ID="clientLitr" runat="server"></asp:Literal></span>
        <span>金额：<asp:Literal ID="moneyLitr" runat="server"></asp:Literal>￥</span><span>完成时间：<asp:Literal ID="comDateLitr" runat="server"></asp:Literal></span></p>
		<h3>项目描述:</h3>
		<p>
		<asp:Literal ID="itemDetailLitr" runat="server"></asp:Literal>
		</p>
		<h3>总进度条:<span class="program_progress" id="programProgress" runat="server"></span></h3>
		<div class="all_progress">
			<div class="all_progress_bg" id="itemProgress" runat="server"></div>
		</div>
     <uc1:ItemGroupDetails ID="planeDetails" runat="server" />
     <uc1:ItemGroupDetails ID="frontendDetails" runat="server" />
     <uc1:ItemGroupDetails ID="programDetails" runat="server" />
    <uc1:ItemGroupDetails ID="flashDetails" runat="server" />
            <div id="itemComplete" runat="server" visible="false">
           <p><Upload:InputFile ID="InputFile" runat="server" onchange="fileSpan.innerText='附件：'+value"/>
          <a id="selectFile" href="javascript:void(0)" class="button2">选择文件</a><span id="fileSpan" runat="server">附件：未选择文件</span>      
		<input type="image" src="images/uploading.png" id="uploading" runat="server" onserverclick="uploading_Click" onclick="return CheckFile();"/></p> 
         <p><Upload:ProgressBar ID="ProgressBar" runat='server' Inline="true" Visible="false"></Upload:ProgressBar></p>      
        </div>
	</div><!--end of main-->
	<div id="message_area">
		<h1>留言区</h1>
        <asp:ScriptManager ID="commentScriptManager" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="commentUPn" runat="server">
        <ContentTemplate>
		<span class="message_edit"><a  id="fileDownLoadUrl" runat="server">文件下载</a>|<span onclick="showText();">添加留言</span></span>
		<div id="message_contant">
			<div id="message_contant_text" style="display: none; ">
			<textarea  id="message_box" runat="server" onblur="check(this);"></textarea>
			<button id="saveBtn" onserverclick="SaveBtn_click" runat="server">保存</button>
			<span id="message_alert"></span>
			</div>
            <asp:Repeater ID="commentRp" runat="server">
            <ItemTemplate>
			<div class="message">
				<span><%#Eval("userName") %>留言：</span><span class="message_date"><%#DataBinder.Eval(Container.DataItem, "postDate", "{0:yyy.MM.d}")%></span><p><%#Eval("comment") %></p>
                </div><!--end of message-->
            </ItemTemplate>
            </asp:Repeater>
            		</div>
           <webdiyer:aspnetpager ID="commentPager" runat="server"  HorizontalAlign="Right"  AlwaysShowFirstLastPageNumber="true" NumericButtonType="Text" 
                     MoreButtonType="Text" PageSize="4" AlwaysShow="true"  PagingButtonType="Image" ButtonImageNameExtension="n" OnPageChanged="OnPageChanged"  
            CurrentPageButtonClass="currentPageBtn"  width="100%" ImagePath="../OA/images/"  ButtonImageExtension=".png" DisabledButtonImageNameExtension="D" >
                </webdiyer:aspnetpager>
            </ContentTemplate>
            </asp:UpdatePanel>
	</div>
    </form>
    </body>
    	<script type="text/javascript">
		//<![CDATA[
    	    getText = document.getElementById('message_contant_text');
    	    getText.style.display = 'none';
    	    function showText() {
    	        var text = document.getElementById('message_contant_text');
    	        if (text.style.display == 'none') {
    	            text.style.display = 'block';
    	        }
    	        else { text.style.display = 'none'; }
    	    }
    	function check(decoration) {
    	        var getInner = decoration.value;
    	        if (getInner.length > 300) {
    	            overtext = getInner.length - 300;
    	            document.getElementById('message_alert').innerHTML = '(字数超过' + overtext + '字请修改）';
    	        }
    	        else if (getInner.length == 0) {
    	            var saveBtns = document.getElementsByTagName('button');
    	            var saveBtn = saveBtns[0];
    	            saveBtn.onclick = function () {
    	                alert("发表的内容不能为空!");
    	            }
    	        }
    	        else { document.getElementById('message_alert').innerHTML = ''; }
    	    }
    	    function CheckFile() {
    	        var fileFullName = document.getElementById('InputFile').value;
                var href=document.getElementById('fileDownLoadUrl').href;
    	        if (fileFullName == null || fileFullName == "") {
    	            alert("请选择文件!"); return false;
    	        }
    	        var fileIndex = fileFullName.lastIndexOf('\\');
    	        if (fileFullName.substr(fileIndex + 1, fileFullName.length) == href.substr(href.lastIndexOf('/') + 1, href.length)) return confirm('该项目已存在同名文件，确定要继续上传然后替换它吗？');
    	        var index = fileFullName.lastIndexOf(".");
    	        var extendName = fileFullName.substr(index + 1, fileFullName.length - index - 1);
    	        if (!(extendName == "rar" || extendName == "zip" || extendName == "gz" || extendName == "7z" || extendName == "7Z" || extendName == "RAR" || extendName == "ZIP" || extendName == "GZ")) {
    	            alert("老兄，只能上传压缩文件!" + extendName);
    	            return false;
    	        }
    	        else return true;
    	    }
			//]]>
	</script>
    </html>