<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sign.aspx.cs" Inherits="sign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link type="text/css" rel="stylesheet" href="css/reset.css" />
<link type="text/css" rel="stylesheet" href="css/sign.css" />
<title>维生数工作室内部网登录页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="header">
	<p>Vitanmin Source Studio</p>
	<img src="images/sign_header.png" alt="维生数工作室"/>
</div>
<img src="images/vtmer.jpg" alt="VTMER" id="vtmer" />
<div id="sign_in">
	<div id="explain">
		<p> 此网站只供维生数工作室内部人员使用，不对外开放.如有不便之处请见谅。</p>
	</div>
	<div id="sign_box">
	<label for="user_name">用户名</label><asp:TextBox ID="user_name" runat="server" name="user_name" TextMode="SingleLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="user_name" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
	<label for="user_password">密码</label><asp:TextBox name="user_password" ID="user_password" runat="server" TextMode="password" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPw" runat="server" ControlToValidate="user_password" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
    <label for="user_checkCode">验证码</label><asp:TextBox name="user_checkCode" ID="user_checkCode" runat="server"  TextMode="SingleLine" onblur="CheckNull(this)"></asp:TextBox>
    <asp:RequiredFieldValidator ID="user_checkCodeSpan" runat="server" ControlToValidate="user_checkCode" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
    <img src ="CheckImage.aspx" id="CheckImage" alt="看不清，换一张" title="看不清，换一张"  height="26" width="70" onclick="this.src='CheckImage.aspx?flag=' + Math.random();" />
   <button type="submit" ID="loginBtn" runat="server" onserverclick="loginBtn_Click">登录</button>
	</div>
</div>
    </form>
    <script type="text/javascript">
    //<![CDATA[
        function CheckNull(element) {
            var notice = document.getElementById(element.id + 'Span');
            if (element.value == "") {
                notice.style.visibility = 'visible';
            }
            else {
                notice.style.visibility = 'hidden';
            }
        }
        //]]>
    </script>
</body>
</html>
