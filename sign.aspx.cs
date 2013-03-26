using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections;

public partial class sign : System.Web.UI.Page
{
    BaseDAL<OA_log> logDal = new BaseDAL<OA_log>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void loginBtn_Click(object sender, EventArgs e)
    {
        string checkCode = user_checkCode.Text.Trim().ToLower();
        if (Session["CheckCode"] == null | !checkCode.Equals(Session["CheckCode"].ToString().ToLower()))
        {
            string script = "alert('老兄，验证码错误哦！')";
            Page.ClientScript.RegisterStartupScript(this.GetType(), null, script, true);
           user_checkCode.Text = "";
        }
        else
        {
        string userName = user_name.Text.Trim();
        string userPwd = user_password.Text.Trim();
        string encodePwd = FormsAuthentication.HashPasswordForStoringInConfigFile(userPwd, "md5").Substring(0, 30);
        int failCount=Common.FailLoginCount(Common.GetIp());
        if (failCount>5)
        {
            if (failCount < 11)
            {
                OA_log log = new OA_log();
                log.ip = Common.GetIp();
                log.wrongPw = "登录失败超五次：" + string.Format("{0}", userName) + "密码:" + userPwd;
                string[] columnA = { "ip", "wrongPw" };
                ArrayList al = new ArrayList { log.ip, log.wrongPw };
                logDal.insert(columnA, log, al);
            }
            scripthelp.Alert(userName + "登录失败超过五次！请联系管理员或一个小时后再登录", this.Page);
            return;
        }
        OA_users user = new OA_users();
        BaseDAL<OA_users> userDal = new BaseDAL<OA_users>();
        if (Regex.IsMatch(userName, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']") && Regex.IsMatch(userPwd, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']")) user = null;
        else
        {
            user = userDal.GetUser("loginName", userName, "pw", encodePwd);
        }
        if (user != null)
        {
            string userRole = user.roles;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), false, userRole, "/");
            string HashTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
            Context.Response.Cookies.Add(userCookie);
            Session["user"] = user;
            if(FormsAuthentication.GetRedirectUrl(userName,false)=="default.aspx") Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, false));
           else  Response.Redirect("home.aspx");
        }
        else
        {
            OA_log log = new OA_log();
            log.ip = Common.GetIp();
            log.wrongPw ="登录名:"+ string.Format("{0}", userName) + "密码:" + userPwd;
            string[] columnA = { "ip", "wrongPw" };
            ArrayList al = new ArrayList { log.ip, log.wrongPw };
            logDal.insert(columnA, log, al);
            Common.isDeleteOldLog();//防止日志文件太多所以删除过多过久的日志
            scripthelp.Alert("老兄，用户名或密码错误哦！", this.Page);
            user_checkCode.Text = "";
        }
    }
     }
}