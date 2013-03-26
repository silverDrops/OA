using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Net;
using System.Drawing;
using System.Collections.Generic;

/// <summary>
/// Common 的摘要说明
/// </summary>
public class Common
{
    #region 取得远程的IP地址和浏览器类型
    /// <summary>
    /// 取得远程的IP地址和浏览器类型
    /// </summary>
    /// <returns></returns>
    public static string GetIp()
    {
        string str = "";
        //穿过代理服务器取远程用户真实IP地址：
        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            str = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        else
            str = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();

        /*HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
        bc = System.Web.HttpContext.Current.Request.Browser;
        str += "&nbsp;你的操作系统:" + bc.Platform + " 浏览器:" + bc.Type;*/
        return str;
    }
    #endregion

    #region 返回一个小时内登录错误的次数
    public static int FailLoginCount(string ip)
    {
        int count = 0;
        string sql;
        try
        {
            sql = "SELECT COUNT(id) FROM OA_log WHERE ip=@ip AND DATEDIFF(hour,getdate(),addtime)=0";
            SqlParameter[] para ={ new SqlParameter("@ip", ip) };
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql, para));
        }
        catch (Exception)
        {
            throw;
        }
        return count;
    }

    #endregion

    #region 判断一年半以前的日志是否超过了200条
    public static bool isDeleteOldLog()
    {
        int count = 0;
        string sql;
        try
        {
            sql = "SELECT COUNT(id) FROM OA_log WHERE DATEDIFF(month,getdate(),addtime)>18";
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql));
            if (count > 200)
            {
                sql = "Delete FROM OA_log WHERE DATEDIFF(month,getdate(),addtime)>18";
                Convert.ToInt32(SqlHelper.ExecuteScalar(SqlHelper.ConnString, CommandType.Text, sql));
                return true;
            }
        }
        catch (Exception)
        {
            throw;
        }
            return false;
    }
    #endregion

    #region 截取字符串
    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="inputString"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string CutString(string inputString, int len)
    {

        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        string tempString = "";
        byte[] s = ascii.GetBytes(inputString);
        for (int i = 0; i < s.Length; i++)
        {
            if ((int)s[i] == 63)
            {
                tempLen += 2;
            }
            else
            {
                tempLen += 1;
            }

            try
            {
                tempString += inputString.Substring(i, 1);
            }
            catch
            {
                break;
            }

            if (tempLen > len)
                break;
        }
        //如果截过则加上半个省略号
        byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
        if (mybyte.Length > len)
            tempString += "...";

        return tempString;
    }

    #endregion

    #region 清除HTML
    public static string DelHTML(string Htmlstring)      //截取字符时将HTML去除
    {
        //删除脚本
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //删除HTML
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
       // Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"-->", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"<!--.*", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<A>.*</A>","");
        //Htmlstring =System.Text.RegularExpressions. Regex.Replace(Htmlstring,@"<[a-zA-Z]*=\.[a-zA-Z]*\?[a-zA-Z]+=\d&\w=%[a-zA-Z]*|[A-Z0-9]","");
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(amp|#38);", "&", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(lt|#60);", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(gt|#62);", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring = System.Text.RegularExpressions.Regex.Replace(Htmlstring, @"&#(\d+);", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        Htmlstring.Replace("<", "");
        Htmlstring.Replace(">", "");
        //Htmlstring.Replace("\r\n", "");
        //Htmlstring=HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

        return Htmlstring;
    }
#endregion

    #region BaseDAL方法
    /// <summary>
    /// 字符串转数组
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] StringToArray(string str)
    {
        if (str.StartsWith(",")) str = str.Substring(1, str.Length - 1);
        if (str.EndsWith(",")) str = str.Substring(0, str.Length - 1);
        str = str.Replace(",,", ",");
        string[] ary = str.Split(',');
        return ary;
    }
    /// <summary>
    /// 数组转字符串
    /// </summary>
    /// <param name="ary"></param>
    /// <returns></returns>
    public static string ArrayToString(string[] ary)
    {
        string temp = "";
        for (int i = 0; i < ary.Length; i++)
        {
            if (i == 0) temp = ary[0];
            else temp += ("," + ary[i]);
        }
        return temp;
    }
    /// <summary>
    /// 数组转带@的字符串
    /// </summary>
    /// <param name="ary"></param>
    /// <returns></returns>
    public static string ArrayToParam(string[] ary)
    {
        string temp = "";
        for (int i = 0; i < ary.Length; i++)
        {
            if (i == 0) temp = "@" + ary[0];
            else temp += (",@" + ary[i]);
        }
        return temp;
    }
    public static string ArrayToSetStr(string[] ary)
    {
        string temp = "";
        for (int i = 0; i < ary.Length; i++)
        {
            if (i == 0) temp = ary[0] + "=@" + ary[0];
            else temp += ("," + ary[i] + "=@" + ary[i]);
        }
        return temp;
    }
    #endregion

    #region 设定初始的各组制作项目的步骤
    public static string[] setOrigiStepList(string groupName)
    {
        if (groupName == "平面")
        {   
           return new string[]{"信息框架","风格配色", "页头页脚","功能画面","细修"};
        }
        else if (groupName == "前端")
        {
            return new string[] { "命名&布局规则", "公用部件制作", "具体页面制作", "细修" };
        }
        else if (groupName == "后台")
        {
            return new string[] { "数据库", "逻辑层搭建", "数据绑定", "总测试&细修" };
        }
        else
        {
            return new string[] { "风格设计", "动态效果设计", "代码设计", "细修" };
        }
    }
    #endregion
}
