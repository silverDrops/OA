using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class ItemGroupDetails : System.Web.UI.UserControl
{
        OA_step step = new OA_step();
         OA_itemMessage itemMess=new OA_itemMessage();
         BaseDAL<OA_users> userDal = new BaseDAL<OA_users>();
        BaseDAL<OA_step> stepDal = new BaseDAL<OA_step>();
        BaseDAL<OA_itemMessage> itemMessDal = new BaseDAL<OA_itemMessage>();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public void DataBind(int itemId,string name,string groupName,int itemStatus)
    {
        
        List<OA_users> userList = userDal.GetList(1, 1000);
        List<OA_itemMessage> programMessList = itemMessDal.GetList(1, 1, "[itemId]=" + itemId.ToString() + " and [userName]='" + name + "'", "");
        if (userList != null)
        {
            foreach (OA_users user in userList)
            {
                if (user.realName == name)
                {
                    groupNameLitr.Text = groupName+ "组：" + user.realName;
                    break;
                }
            }
        }
        if (programMessList != null)
        {       
            if (programMessList.Count == 0)
                postDateLitr.Text = "";
            else
            {
                itemMess = programMessList[0] ;
                postDateLitr.Text = itemMess.postDate.ToString("yyy.MM.d");
                messageLitr.Text = itemMess.message;
            }         
        }
        if (itemMess != null)
        {
            OA_step steps = stepDal.GetModel(itemMess.id, "[itemMessageId]");
            string[] stepsOrigiArray;
            if (steps == null)
            {
                 stepsOrigiArray=Common.setOrigiStepList(groupName);
            }
            else
            {
                stepsOrigiArray = Common.StringToArray(steps.step);
            }
                stepRp.DataSource = stepsOrigiArray;
                stepRp.DataBind();
                if (itemStatus == 100) itemMess.groupStatus = 100;
            programProgress.Style.Add("width", 5.54 * itemMess.groupStatus + "px");
        }
    }
}