using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

    /// <summary>
	/// 脚本辅助方法
	/// </summary>

    public class scripthelp
    {
		/// <summary>
		/// 
		/// </summary>
		public static Random rnd = new Random();

		/// <summary>
		/// 
		/// </summary>
        public scripthelp()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//

        }

        /// <summary>
        /// 在客户端提示信息并转到指定URL
        /// <example>
        /// ScriptHelper.AlertAndRedirect("保存成功 !","http://www.163.com",this);
        /// </example>
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="url">需要跳转到的地址</param>
        /// <param name="p">当前页面对象</param>
        public static void AlertAndRedirect(string msg,string url,Page p)
        {
            Alert(msg,p);
            Redirect(url, p);
        }


        /// <summary>
		/// 弹出提示信息
		/// </summary>
		/// <param name="str">信息内容</param>
		/// <param name="page">需要提示信息的PAGE对象</param>
		public static void Alert(string str,Page page)
		{
			string js = string.Format(@" alert('{0}');",str);

            RegisterStartupScript(js, "win_alert", page);
		}

        /// <summary>
        /// 跳转到指定的URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="p"></param>
        public static void Redirect(string url,Page p)
        {
            string js = string.Format(@"window.location='{0}';",url);
            RegisterStartupScript(js,"Sript_Redirect",p);
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="p"></param>
        public static void Close(Page p)
        {
            string js = "window.close();";
            RegisterStartupScript(js,"win_close",p);
        }
        
		/// <summary>
		/// 打开新窗口
		/// </summary>
		/// <param name="sURL">地址</param>
		/// <param name="sName">网页名称</param>
		/// <param name="sFeatures">打开的窗体设置</param>
		/// <param name="page"></param>
		public static void Open(string sURL,string sName, string sFeatures,Page page)
		{
			string js = @"
						<Script language='JavaScript'>
							window.open(""{0}"",""{1}"",""{2}"");
						</Script>"
						;

            ClientScriptManager script = page.ClientScript;
            script.RegisterStartupScript(typeof(scripthelp), "Open",  string.Format(js, sURL, sName, sFeatures));
		}

		

		/// <summary>
		/// 绑定控件onclick跳转到指定URL的脚本
		/// </summary>
		/// <param name="ctrl">Button\LinkButton\ImageButton   Control</param>
		/// <param name="url">连接</param>
		public static void BindControlTurnToUrl(Control ctrl ,string url)
		{
			string js = "window.location.href = '{0}';";
			js = string.Format(js,url);

			if(ctrl is Button)
			{
				((Button)ctrl).Attributes.Add("onclick",js);
			}

			if(ctrl is LinkButton)
			{
				((LinkButton)ctrl).Attributes.Add("onclick",js);
			}

			if(ctrl is ImageButton)
			{
				((ImageButton)ctrl).Attributes.Add("onclick",js);
			}
		}

		/// <summary>
        /// 注册脚本块
		/// </summary>
		/// <param name="script">脚本内容</param>
		/// <param name="key"></param>
		/// <param name="page"></param>
		public static void RegisterStartupScript(string script,string key,Page page)
		{
            script = BuildScriptBlock(script);
            page.ClientScript.RegisterStartupScript(typeof(scripthelp),key, script);
		}

        /// <summary>
        /// 生成JS脚本块
        /// </summary>
        /// <param name="js"></param>
        /// <returns></returns>
        public static string BuildScriptBlock(string js)
        {
            return string.Format(@"<Script language='JavaScript'>{0}</Script>", js);
        }

		/// <summary>
		/// 为控件绑定一个提示内容框
		/// </summary>
		/// <param name="control">控件</param>
		/// <param name="msg">提示信息</param>
		public static void ConfirmControl(System.Web.UI.Control control,string msg)
		{
			string js = @"return confirm('{0}')";
			js = string.Format(js,msg);
			
			if(control is Button)
			{
				((Button)control).Attributes.Add("onclick",js);
			}

			if(control is LinkButton)
			{
				((LinkButton)control).Attributes.Add("onclick",js);
			}

			if(control is ImageButton)
			{
				((ImageButton)control).Attributes.Add("onclick",js);
			}
		}

		/// <summary>
		/// 封装执行脚本
		/// <code>
		/// RunScript(this, "window.top.location='alter.aspx'");
		/// </code>
		/// </summary>
		/// <param name="pagParent"></param>
		/// <param name="strScript"></param>
		public static void RunScript(Page pagParent, String strScript)
		{
			string key = "Key_Runscipt" + scripthelp.rnd.Next().ToString();
			strScript = String.Format("<script language='javascript'>{0}</script>", strScript);
            ClientScriptManager script = pagParent.ClientScript;    
			script.RegisterStartupScript(typeof(scripthelp), key, strScript);
		
		}

		/// <summary>
		/// 确认，各自转向页面
		/// <code>
		/// ConfirmAndRedirect(this, "ABC,是否确定?", "WebForm2.aspx", "WebForm3.aspx");
		/// </code>
		/// </summary>
		public static void ConfirmAndRedirect(Page pagParent, String strMessage, String strTruePage, String strFalsePage)
		{
			string strFormat=@"
				<script language='javascript'>
					var bFlag=confirm('{0}');
					var strUrl='';
					if (bFlag) strUrl='{1}';
					else strUrl='{2}';
					window.location=strUrl;
				</script>";

			string strScript=String.Format(strFormat, strMessage, strTruePage, strFalsePage);
            ClientScriptManager script = pagParent.ClientScript;  
			script.RegisterStartupScript(typeof(scripthelp), "Key_ConfirmAndRedirect", strScript);
		}

		/// <summary>
		/// 确认，各自执行脚本
		/// <code>
		/// ConfirmAndRunScript(this, "ABC,是否确定?", "alert('ok');", "alert('cancel')");
		/// </code>
		/// </summary>
		/// <param name="pagParent"></param>
		/// <param name="strMessage"></param>
		/// <param name="strTrueScript"></param>
		/// <param name="strFalseScript"></param>
		public static void ConfirmAndRunScript(Page pagParent, String strMessage, String strTrueScript, String strFalseScript)
		{
			string strFormat=@"
				<script language='javascript'>
					var bFlag=confirm('{0}');					
					if (bFlag) {1}; else {2};
				</script>";

			string strScript=String.Format(strFormat, strMessage, strTrueScript, strFalseScript);
            ClientScriptManager script = pagParent.ClientScript;
            script.RegisterStartupScript(typeof(scripthelp),"Key_ConfirmAndRunScript", strScript);
		}

		/// <summary>
		/// 控件添加有确认删除按钮的提醒消息
		/// <code>
		/// AddConfirmDelete(ImageButton1, "确认要删除吗？");
		/// </code>
		/// </summary>
		/// <param name="cmdButton"></param>
		/// <param name="strMessage"></param>
		public static void AddConfirmDelete(ImageButton cmdButton, String strMessage)
		{
			string strScript=String.Format("return confirm('{0}');", strMessage);
			cmdButton.Attributes.Add("onclick", strScript); 
		}

		/// <summary>
		/// 控件添加有确认删除按钮的提醒消息
		/// <code>
		/// AddConfirmDelete(LinkButton1, "确认要删除吗？");
		/// </code>
		/// </summary>
		/// <param name="cmdButton"></param>
		/// <param name="strMessage"></param>
		public static void AddConfirmDelete(LinkButton cmdButton, String strMessage)
		{
			string strScript=String.Format("return confirm('{0}');", strMessage);
			cmdButton.Attributes.Add("onclick", strScript); 
		}

		/// <summary>
		/// 控件添加有确认删除按钮的提醒消息
		/// <code>
		/// AddConfirmDelete(Button1, "确认要删除吗？");
		/// </code>
		/// </summary>
		/// <param name="cmdButton"></param>
		/// <param name="strMessage"></param>
		public static void AddConfirmDelete(Button cmdButton, String strMessage)
		{
			string strScript=String.Format("return confirm('{0}');", strMessage);
			cmdButton.Attributes.Add("onclick", strScript); 
		}

		/// <summary>
		/// 列表控件添加自定义操作
		/// <code>
		/// AddCustomAction(ListBox1,"ondblclick", "document.Form1.Button1.click()");
		/// </code>
		/// </summary>
		public static void AddCustomAction(ListBox lbListBox, String strEvent, String strAction)
		{
			lbListBox.Attributes.Add(strEvent, strAction);
		}
    }

