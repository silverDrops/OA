using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class home : System.Web.UI.Page
{
    OA_users user = new OA_users();
    const int userPageCount = 8;
    int userCount, noticeCount, financeCount;
    BaseDAL<OA_users> dalUser = new BaseDAL<OA_users>();
    BaseDAL<OA_item> dalItem = new BaseDAL<OA_item>();
    BaseDAL<OA_announcement> dalAnnounement = new BaseDAL<OA_announcement>();
    BaseDAL<OA_finance> dalFinance = new BaseDAL<OA_finance>();
    private int userCuPage
    {
        get
        {  return (int)ViewState["userCuPage"]; }
        set
        { ViewState["userCuPage"] = value;}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        user = (OA_users)Session["user"];
        if (user == null) Response.Redirect("sign.aspx");
        if (!IsPostBack)
        {
            ViewState["userCuPage"] = 1;
            GetList(0, false);
        }
    }
    //绑定数据
    protected void GetList(int noticeId, bool noticeMidFlag)
    {
        List<OA_users> originalList = dalUser.GetList(1, userPageCount, "", "[status]", out userCount);
        List<OA_users> allUserList = dalUser.GetList(1, 2000, "", "");
        GetItemList(1, "", true);
        GetAnnounceList();
        GetFinanceList();
        GetUserList(1, originalList, userCount);
        allLi.Attributes.Add("class", "on_condition");
        allUserA.Attributes.Add("class", "on_condition");
        userRoleHi.Value = user.roles;
        ListItem itemIn = new ListItem("收入");
        ListItem itemOut = new ListItem("支出");
        inOrOutDropL.Items.Add(itemIn);
        inOrOutDropL.Items.Add(itemOut);
        if (originalList != null)
        {
            //创建项目里的人员下拉菜单选项
            foreach (OA_users users in allUserList)
            {
                ListItem pListItem = new ListItem(users.realName, user.id.ToString());
                principle.AddItem(pListItem.ToString());
                switch (users.groups)
                {
                    case "平面": ListItem fListItem = new ListItem(users.realName, user.id.ToString());
                        this.pingmian.AddItem(fListItem.ToString()); break;
                    case "程序": ListItem bListItem = new ListItem(users.realName, user.id.ToString());
                        houtai.AddItem(bListItem.ToString()); break;
                    case "flash": ListItem flashLItem = new ListItem(users.realName, user.id.ToString());
                        flashzu.AddItem(flashLItem.ToString()); break;
                    case "前端": ListItem qianLItem = new ListItem(users.realName, user.id.ToString());
                        qianduan.AddItem(qianLItem.ToString()); break;
                    default: break;
                }
            }//foreach
        }//if
    }
    //人员列表数据
    protected void GetUserList(int currentPage, List<OA_users> oldList, int pageCount)
    {
        List<OA_users> userList = UserListByStatus(oldList);
        if (userList != null)
        {
            userRptList.DataSource = userList;
            userRptList.DataBind();
        }
        if (userCuPage == 1)
        {
            prePageBtn.ImageUrl = "~/images/change_prevD.png";
            prePageBtn.Enabled = false;
        }
        else
        {
            prePageBtn.ImageUrl = "~/images/change_prev.png";
            prePageBtn.Enabled = true;

        }
        if (userCuPage == (pageCount / userPageCount + 1))
        {
            nextPageBtn.ImageUrl = "~/images/change_nextD.png";
            nextPageBtn.Enabled = false;
        }
        else
        {
            nextPageBtn.ImageUrl = "~/images/change_next.png";
            nextPageBtn.Enabled = true;
        }
    }

    protected void GetItemList(int currentPage, string _whereStr, bool headerFlag)
    {
        itemMidList1.DataBind(currentPage,_whereStr); 
        List<OA_item> allItemList = dalItem.GetList(1, 2000);
        if (headerFlag)  Header1.DataBind(allItemList, user.realName, user.loginName);
    }

    protected void GetFinanceList()
    {
        List<OA_finance> financeAllInList = dalFinance.GetList(1, 1000, "money>0 AND [actionStatus]=1", "actionDate DESC");
        List<OA_finance> financeAllOutList = dalFinance.GetList(1, 1000, "money<0 AND [actionStatus]=1", "actionDate DESC");
        List<OA_finance> financeInList = new List<OA_finance>();
        List<OA_finance> financeOutList = new List<OA_finance>();
        for (int i = 0; i < (financeAllInList.Count < 6 ? financeAllInList.Count : 6); i++)
        {
            financeInList.Add(financeAllInList[i]);
        }
        for (int i = 0; i < (financeAllOutList.Count < 6 ? financeAllOutList.Count : 6); i++)
        {
            financeOutList.Add(financeAllOutList[i]);
        }
        if (financeInList != null)
            FinanceDataBind(financeInRptList, financeInList);
        if (financeOutList != null)
            FinanceDataBind(financeOutRptList, financeOutList);
        if (financeInList != null & financeOutList != null)
            remainLiteral.Text = Convert.ToString(FinanceRemain(financeAllInList, financeAllOutList));
        else
            remainLiteral.Text = "无法读取财务数据";
    }

    protected void GetAnnounceList()
    {
        List<OA_announcement> announceList = dalAnnounement.GetList(1, 6, "", "[postDate] DESC");
        if (announceList != null)
        {
            announceRptList.DataSource = announceList;
            announceRptList.DataBind();
        }
    }

    protected void GetFinanceMidList(string whereStr, int currentPage)
    {
        List<OA_finance> financeList = dalFinance.GetList(currentPage, 10, whereStr, "[actionDate] DESC", out financeCount);
        FinancialMidList1.DataBind(financeList, currentPage, user.roles == "财务管理" ? true : false, financeCount, whereStr);
        NoticeMidList1.Visible = false;
        itemMidList1.Visible = false;
        FinancialMidList1.Visible = !NoticeMidList1.Visible;
    }

    protected void GetNoticeMidList(int noticeId)
    {
        List<OA_announcement> noticeList = dalAnnounement.GetList(1, 10, "", "[postDate] DESC", out noticeCount);
        itemMidList1.Visible = false;
        FinancialMidList1.Visible = false;
        NoticeMidList1.Visible = !itemMidList1.Visible;
        NoticeMidList1.noticeCuPage = 1;
        NoticeMidList1.DataBind(noticeId, noticeList, noticeCount, user.roles == "负责人" ? true : false);
    }
    //根据用户的忙碌状态而改变用户对应的class的编号1~4，返回改变后的userlist
    protected List<OA_users> UserListByStatus(List<OA_users> userOriginalLt)
    {
        int statusClassId = 100;
        OA_users userS = new OA_users();
        List<OA_users> userList = new List<OA_users>();
        foreach (OA_users user in userOriginalLt)
        {
            if (0 <= user.status & user.status <= 25) statusClassId = 25;
            else if (25 < user.status & user.status <= 50) statusClassId = 50;
            else if (50 < user.status & user.status < 100) statusClassId = 75;
            else if (user.status == 100) statusClassId = 100;
            userS = user;
            userS.status = statusClassId;
            userList.Add(userS);
        }
        return userList;
    }
    //计算财务的余额
    protected int FinanceRemain(List<OA_finance> financeInList, List<OA_finance> financeOutList)
    {
        int remain = 0;
        foreach (OA_finance inFinance in financeInList)
        {
            remain += inFinance.money;
        }
        foreach (OA_finance outFinance in financeOutList)
        {
            remain += outFinance.money;
        }
        return remain;
    }
    //绑定财务栏的repeater
    protected void FinanceDataBind(Repeater repeaterList, List<OA_finance> list)
    {
        if (repeaterList != null)
        {
            repeaterList.DataSource = list;
            repeaterList.DataBind();
        }
    }
    //根据创建项目后填写的各组负责人名字提示用户是否输入正确并设置空项的值为“”
    protected string SetTipsByText(string name, string boxText, List<string> failName)
    {
        if (boxText != "" && name == null && boxText != "请选择或输入")
        {
            failName.Add(boxText);
        }
        if ((boxText == "" || boxText == "请选择或输入") & name == null)
            name = "";
        return name;
    }
    //单击创建项目的创建button
    protected void itemSubmitBtn_click(object sender, EventArgs e)
    {
        if (principle.Text != "" & project_name.Text != "" & bailer.Text != "" & money.Text != "")
        {
            OA_item item = new OA_item();
            List<string> failNameList = new List<string>();
            string[] itemMenbers = { item.leaderName, item.planeName, item.frontendName, item.programName, item.flashName };
            string[] itemTextMenbers = { principle.Text.Trim(), pingmian.Text.Trim(), qianduan.Text.Trim(), flashzu.Text.Trim(), houtai.Text.Trim() };
            for (int i = 0; i < 5; i++)
            {
                if (dalUser.GetUser("realName", itemTextMenbers[i]) != null)
                {
                    itemMenbers[i] = itemTextMenbers[i];
                }
            }
            item.leaderName = SetTipsByText(itemMenbers[0], principle.Text.Trim(), failNameList);
            item.planeName = SetTipsByText(itemMenbers[1], pingmian.Text.Trim(), failNameList);
            item.frontendName = SetTipsByText(itemMenbers[2], qianduan.Text.Trim(), failNameList);
            item.flashName = SetTipsByText(itemMenbers[3], flashzu.Text.Trim(), failNameList);
            item.programName = SetTipsByText(itemMenbers[4], houtai.Text.Trim(), failNameList);
            if (failNameList.Count != 0)
            {
                string failNameStr = "";
                foreach (string failName in failNameList)
                {
                    failNameStr += failName + ",";
                }
                string tips = string.Format("工作室不存在{0}哦，请重新填写别人的名字啦。", failNameStr.Remove(failNameStr.LastIndexOf(','))).Trim();
                scripthelp.Alert(tips, this.Page);
                scripthelp.RunScript(this.Page, "document.getElementById('edit').style.display='block';document.getElementById('create').style.display = 'block';");
            }
            else
            {
                item.itemName = project_name.Text.Trim();
                item.client = bailer.Text.Trim();
                item.money = Convert.ToInt32(money.Text.Trim());
                try
                {
                item.completeDate = Convert.ToDateTime(year.Text.Trim() + "-" + month.Text.Trim() + "-" + day.Text.Trim());
                item.itemDetials = decoration.InnerText.Trim();
                string[] columnArray = { "itemName", "client", "money", "status", "itemDetials", "leaderName", "planeName", "frontendName", "programName", "flashName", "completeDate" };
                ArrayList al = new ArrayList { item.itemName, item.client, item.money, item.status, item.itemDetials, item.leaderName, item.planeName, item.frontendName, item.programName, item.flashName, item.completeDate };               
                    dalItem.insert(columnArray, item, al);
                    SetNewItemUserStatus(new string[] { item.planeName, item.frontendName, item.programName, item.flashName });
                    scripthelp.Alert("创建" + item.itemName + "项目成功！", Page);
                }
                catch(Exception ex)
                {
                    ControlLog controlLog = new ControlLog("home");
                    controlLog.WriteDebugLog("itemSubmitBtn_click", ex.ToString(),user.realName);
                    scripthelp.Alert("创建" + item.itemName + "项目失败!", Page);
                }
                GetItemList(1, "", true);
                GetUserList(1, dalUser.GetList(1, userPageCount,"", "[status]", out userCount), userCount);

            }//end of if (failNameList.Count == 0)
        }
        else
        {
            scripthelp.Alert("负责人,项目名称,委托方,金额都不能空哦。", this.Page);
            scripthelp.RunScript(this.Page, "document.getElementById('edit').style.display='block';document.getElementById('create').style.display = 'block';");
            principle.Focus();
        }//end of if(principle.Text != "".... 
    }
    //设置新建项目中参与人员的状态
    protected void SetNewItemUserStatus(string[] itemUsersName)
    {
        foreach (string itemUserName in itemUsersName)
        {
            if (itemUserName != "")
            {
                OA_users itemUser = dalUser.GetUser("realName", itemUserName);
                if (itemUser != null && itemUser.status >= 90)
                {
                    itemUser.status = 0;
                    dalUser.Update(itemUser);
                }
            }
        }
    }
    //右边的公告板标题的单击
    protected void midListChange_Click(object sender, EventArgs e)
    {
        GetNoticeMidList(0);
    }
    //右边的财务标题单击
    protected void midListChangeFinance_Click(object sender, EventArgs e)
    {
        GetFinanceMidList("money>0 AND [actionStatus]=1", 1);
    }
    protected void midListChangeOutFinance_Click(object sender, EventArgs e)
    {
        GetFinanceMidList("money<0 AND [actionStatus]=1", 1);
    }
    //公告栏的item命令触发
    protected void noticeRpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int noticeId = int.Parse(e.CommandArgument.ToString());
        GetNoticeMidList(noticeId);
    }
    //发布公告
    protected void NoticeSubmit_Click(object sender, EventArgs e)
    {
        if (notice_heading.Value != "")
        {
            OA_announcement newNotice = new OA_announcement();
            newNotice.title = notice_heading.Value.Trim();
            newNotice.poster = user.realName;
            newNotice.content = notice_decoration.Value.Trim() == "" ? "无" : notice_decoration.Value.Trim();
            string[] columnA = { "poster", "title", "content" };
            try
            {
                dalAnnounement.insert(columnA, newNotice, new ArrayList { newNotice.poster, newNotice.title, newNotice.content });
                scripthelp.Alert("创建" + newNotice.title + "公告成功！", Page);
            }
            catch(Exception ex)
            {
                ControlLog controlLog = new ControlLog("home");
                controlLog.WriteDebugLog("NoticeSubmit_Click", ex.ToString(), user.realName);
                scripthelp.Alert("创建" + newNotice.title + "公告失败!", Page);
            }
            GetAnnounceList();
            if (NoticeMidList1.Visible)
                GetNoticeMidList(0);
        }
    }
    //查看未发放财务
    protected void notComeFinance_Click(object sender, EventArgs e)
    {
        GetFinanceMidList("[actionStatus]=0", 1);
    }
    //添加财务
    protected void FinancialSubmit_Click(object sender, EventArgs e)
    {
        if (financial_event.Value != "" & moneyIn.Value != "")
        {
            OA_finance newFinance = new OA_finance();
            if (inOrOutDropL.SelectedValue == "收入") newFinance.money = int.Parse(moneyIn.Value.Trim());
            else newFinance.money = int.Parse("-" + moneyIn.Value.Trim());
            newFinance.actionName = financial_event.Value.Trim();
            newFinance.actionDetails = financial_decoration.Value.Trim();
            newFinance.actionStatus = 1;
            try
            {
                newFinance.actionDate = Convert.ToDateTime(get_year.Value.Trim() + "-" + get_month.Value.Trim() + "-" + get_day.Value.Trim());
                dalFinance.Add(newFinance);
                scripthelp.Alert("添加" + newFinance.actionName + "财务事件成功！", Page);
                GetFinanceList();
                string bigOrSmall = newFinance.money > 0 ? ">" : "<";
                GetFinanceMidList("money" + bigOrSmall + "0 AND [actionStatus]=1", 1);
            }
            catch(Exception ex)
            {
                ControlLog controlLog = new ControlLog("home");
                controlLog.WriteDebugLog("FinancialSubmit_Click", ex.ToString(), user.realName);
                scripthelp.Alert("添加" + newFinance.actionName + "财务失败！", Page);
            }
        }
    }
    //删除财务事件
    protected void deleteFinanceBtn_Click(object sender, EventArgs e)
    {
        string deleteIds = FinancialMidList1.GetCheckdId();
        if (deleteIds.Length != 0)
        {
            try
            {
                this.dalFinance.Delete(deleteIds);
                GetFinanceList();
                GetFinanceMidList("money>0 AND [actionStatus]=1", 1);
                scripthelp.Alert("删除财务成功！", Page);
            }
            catch(Exception ex)
            {
                ControlLog controlLog = new ControlLog("home");
                controlLog.WriteDebugLog("deleteFinanceBtn_Click", ex.ToString(), user.realName);
                scripthelp.Alert("删除财务失败！", Page);
            }
        }
        else scripthelp.Alert("没有选中任何财务,不可以进行删除。", Page);
    }
    //人员列表的上下页按钮单击
    protected void prePageBtn_Click(object sender, EventArgs e)
    {
        List<OA_users> origiList = new List<OA_users>();
        if (allLi.Attributes["class"] == "on_condition") origiList = dalUser.GetList(--userCuPage, userPageCount, "", "[status]", out userCount);
        else if (freeLi.Attributes["class"] == "on_condition") origiList = dalUser.GetList(--userCuPage, userPageCount, "[status]=100", "", out userCount);
        else if (busyLi.Attributes["class"] == "on_condition") origiList = dalUser.GetList(--userCuPage, userPageCount, "[status]<100", "[status]", out userCount);
        if (origiList != null) GetUserList(userCuPage, origiList, userCount);
    }
    protected void nextPageBtn_Click(object sender, EventArgs e)
    {
        List<OA_users> origiList = new List<OA_users>();
        if (allLi.Attributes["class"] == "on_condition") origiList = dalUser.GetList(++userCuPage, userPageCount, "", "[status]", out userCount);
        else if (freeLi.Attributes["class"] == "on_condition") origiList = dalUser.GetList(++userCuPage, userPageCount, "[status]=100", "", out userCount);
        else if (busyLi.Attributes["class"] == "on_condition") origiList = dalUser.GetList(++userCuPage, userPageCount, "[status]<100", "[status]", out userCount);
        if (origiList != null) GetUserList(userCuPage, origiList, userCount);
    }
    //单击已完成项目、发放、删除按钮
    protected void completedBtn_Click(object sender, EventArgs e)
    {
        if (NoticeMidList1.Visible)
        {
            string deleteIds = NoticeMidList1.GetCheckdId();
            if (deleteIds.Length != 0)
            {
                try
                {
                    dalAnnounement.Delete(deleteIds);
                    GetNoticeMidList(0);
                    GetAnnounceList();
                    scripthelp.Alert("删除公告成功！", Page);
                }
                catch(Exception ex)
                {
                    ControlLog controlLog = new ControlLog("home");
                    controlLog.WriteDebugLog("completedBtn_Click-删除公告", ex.ToString(), user.realName);
                    scripthelp.Alert("删除公告失败！", Page);
                }
            }
            else scripthelp.Alert("没有选中任何公告,不可以进行删除。", Page);
        }
        else if (FinancialMidList1.Visible)
        {
            string giveFIds = FinancialMidList1.GetCheckdId();
            if (giveFIds.Length != 0)
            {
                string[] ids = Common.StringToArray(giveFIds);
                try
                {
                    if (dalFinance.Set("actionStatus", 1, "actionDate", DateTime.Now, giveFIds) == ids.Length)
                    {
                        GetAnnounceList();
                        GetFinanceMidList("[actionStatus]=0", 1);
                        scripthelp.Alert("发放财务成功！", Page);
                    }
                    else scripthelp.Alert("发放财务失败！", Page);
                }
                catch(Exception ex)
                {
                    ControlLog controlLog = new ControlLog("home");
                    controlLog.WriteDebugLog("completedBtn_Click", ex.ToString(), user.realName);
                    scripthelp.Alert("发放财务失败！", Page);
                }
            }
            else scripthelp.Alert("没有选中任何财务,不可以进行发放。", Page);
        }
        else GetItemList(1, "[status]=100", false);
    }
    //人员列表的选项卡单击
    protected void conditionA_Click(object sender, EventArgs e)
    {
        String aID;
        List<OA_users> origiList = new List<OA_users>();
        System.Web.UI.HtmlControls.HtmlAnchor a = sender as System.Web.UI.HtmlControls.HtmlAnchor;
        userCuPage = 1;
        if (a != null)
        {
            aID = a.ID;
            switch (a.ID)
            {
                case "allUserA":
                    {
                        allLi.Attributes.Add("class", "on_condition");
                        allUserA.Attributes.Add("class", "on_condition");
                        freeUserA.Attributes.Remove("class");
                        freeLi.Attributes.Remove("class");
                        busyUserA.Attributes.Remove("class");
                        busyLi.Attributes.Remove("class");
                        origiList = dalUser.GetList(1, userPageCount, "", "[status]", out userCount);
                        if (origiList != null) GetUserList(1, origiList, userCount);
                        break;
                    }
                case "freeUserA":
                    {
                        freeLi.Attributes.Add("class", "on_condition");
                        freeUserA.Attributes.Add("class", "on_condition");
                        allUserA.Attributes.Remove("class");
                        allLi.Attributes.Remove("class");
                        busyUserA.Attributes.Remove("class");
                        busyLi.Attributes.Remove("class");
                        origiList = dalUser.GetList(1, userPageCount, "[status]=100", "", out userCount);
                        if (origiList != null) GetUserList(1, origiList, userCount);
                        break;
                    }
                case "busyUserA":
                    {
                        busyLi.Attributes.Add("class", "on_condition");
                        busyUserA.Attributes.Add("class", "on_condition");
                        allUserA.Attributes.Remove("class");
                        allLi.Attributes.Remove("class");
                        freeUserA.Attributes.Remove("class");
                        freeLi.Attributes.Remove("class");
                        origiList = dalUser.GetList(1, userPageCount, "[status]<100", "[status]", out userCount);
                        if (origiList != null) GetUserList(1, origiList, userCount);
                        break;
                    }
                default: break;
            }
        }
    }
}