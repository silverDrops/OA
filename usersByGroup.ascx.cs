using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usersByGroup : System.Web.UI.UserControl
{
    BaseDAL<OA_users> userDal = new BaseDAL<OA_users>();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void DataBind(List<OA_users> userList)
    {
        foreach (OA_users user in userList)
        {
            if (user.roles == "组长")
            {
                leaderLitr.Text = user.realName;
                userList.Remove(user);
                break;
            }
        }
        if (leaderLitr.Text == null | leaderLitr.Text == "")
        {
            leaderLi.InnerHtml = "";
            this.RemovedControl(leaderLi);
        }
        menberRpt.DataSource = userList;
        menberRpt.DataBind();
    }
}