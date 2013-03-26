using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
     //绑定用户正在进行的项目
    public int DataBind(List<OA_item> itemList, string userName,string loginName)
    {
        List<OA_item> userItemList = new List<OA_item>();
            userNameLiteral.Text = loginName;
        foreach (OA_item item in itemList)
        {
            if (item.status < 100 & (item.leaderName == userName | item.frontendName == userName | item.planeName == userName | item.programName == userName | item.flashName == userName))
            {
                userItemList.Add(item);
            }
        }
            if (userItemList != null)
            {
                userItemRptList.DataSource = userItemList;
                userItemRptList.DataBind();
            }
            return userItemList.Count;
    }
    protected void OutButton_Click(object sender, EventArgs e)
    {
           Session["user"] = null;
           FormsAuthentication.SignOut();
           Response.Redirect("sign.aspx");
    }
}

