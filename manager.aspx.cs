using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Text.RegularExpressions;

public partial class managerspx : System.Web.UI.Page
{
    OA_users user = new OA_users();
    List<OA_users> usersList = new List<OA_users>();
    List<OA_users> leadersList = new List<OA_users>();
    List<OA_users> qianDuanUserList = new List<OA_users>();
    List<OA_users> pingMainList = new List<OA_users>();
    List<OA_users> houTaiList = new List<OA_users>();
    List<OA_users> flashList = new List<OA_users>();
    List<OA_users> oldUserList = new List<OA_users>();
    BaseDAL<OA_users> userDal = new BaseDAL<OA_users>();
    BaseDAL<OA_item> dalItem = new BaseDAL<OA_item>();
    protected void Page_Load(object sender, EventArgs e)
    {
        user = (OA_users)Session["user"];
        usersList = userDal.GetList(1, 2000, "", "[grade]");
        leadersList = SetUsersByGroup("负责人", usersList);
        qianDuanUserList = SetUsersByGroup("前端", usersList);
        pingMainList = SetUsersByGroup("平面", usersList);
        houTaiList = SetUsersByGroup("程序", usersList);
        flashList = SetUsersByGroup("flash", usersList);
        oldUserList = SetUsersByGroup("往届", usersList);
        if (user == null) Response.Redirect("sign.aspx");
        if (!IsPostBack)
        {
            GetList(user.groups);
            DropListDataBind();
        }
    }
    //绑定数据
    protected void GetList(string groupNow)
    {
        List<OA_item> allItemList = dalItem.GetList(1, 2000);
        Header1.DataBind(allItemList, user.realName, user.loginName);
        usersByLeaders.DataBind(leadersList);
        usersByQianDuan.DataBind(qianDuanUserList);
        usersByPingMian.DataBind(pingMainList);
        usersByFlash.DataBind(flashList);
        usersByHouTai.DataBind(houTaiList);
        usersByOld.DataBind(oldUserList);
        rightListDataBindByGroup(groupNow);
        loginNameIn.Value = user.loginName;
        rLongNumIn.Value = user.longNumber == null ? "" : user.longNumber;
        rShortNumIn.Value = user.shortNumber == null ? "" : user.shortNumber;
    }
    protected void GetListAfterGet(string groupNow)
    {
        usersList = userDal.GetList(1, 2000, "", "[grade]");
        leadersList = SetUsersByGroup("负责人", usersList);
        qianDuanUserList = SetUsersByGroup("前端", usersList);
        pingMainList = SetUsersByGroup("平面", usersList);
        houTaiList = SetUsersByGroup("程序", usersList);
        flashList = SetUsersByGroup("flash", usersList);
        oldUserList = SetUsersByGroup("往届", usersList);
        GetList(groupNow);
    }
    //添加人员和修改个人资料里的dropDownList的绑定和根据用户的身份设置权限
    protected void DropListDataBind()
    {
        ArrayList dropItemAl = new ArrayList { "平面", "前端", "程序", "flash", "往届" };
        groupDrDL.DataSource = dropItemAl;
        groupDrDL.DataBind();
        groupDrDL.SelectedValue = user.groups;
        if (user.roles == "负责人")
        {
            editDiv.Visible = true;
            editFlag.Value = "true";
            GroupDropList.DataSource = dropItemAl;
            GroupDropList.DataBind();
            dropItemAl = new ArrayList { "组员", "组长", "往届", "负责人", "财务管理" };
            RolesDropList.DataSource = dropItemAl;
            RolesDropList.DataBind();
        }
        else
        {
            editFlag.Value = "false";
            save.Visible = false;
        }
    }

    //根据组别绑定右边的管理页面
    protected void rightListDataBindByGroup(string groupName)
    {
        switch (groupName)
        {
            case "平面": rightListDataBind("平面", pingMainList); break;
            case "前端": rightListDataBind("前端", qianDuanUserList); break;
            case "程序": rightListDataBind("程序", houTaiList); break;
            case "flash": rightListDataBind("flash", flashList); break;
            case "往届": rightListDataBind("往届", oldUserList); oldUserPager.RecordCount = oldUserList.Count; break;
            default: break;
        }
    }
    protected void rightListDataBind(string groupName, List<OA_users> usersList)
    {
        if (groupName == "往届")
        {
            pager.Visible = true;
            if (usersList.Count > 8) usersList.RemoveAt(8);
        }
        else pager.Visible = false;
        GroupNameLitr.Text = groupName;
        userListRpt.DataSource = usersList;
        userListRpt.DataBind();
    }
    //根据组别获得treeview里要绑定的数据
    protected List<OA_users> SetUsersByGroup(string groupName, List<OA_users> usersList)
    {
        List<OA_users> groupList = new List<OA_users>();
        if (groupName == "负责人" | groupName == "往届")
        {
            foreach (OA_users menber in usersList)
            {
                if (menber.roles == groupName)
                    groupList.Add(menber);
            }
        }
        else
        {
            foreach (OA_users menber in usersList)
            {
                if (menber.groups == groupName & menber.roles != "往届") groupList.Add(menber);
            }
        }
        return groupList;
    }
    //treeview里的根据触发onclick的button的id而绑定右边的管理页面
    protected void GroupBtn_Click(object sender, EventArgs e)
    {
        String btnID;
        HtmlInputButton btn = sender as HtmlInputButton;
        if (btn != null)
        {
            btnID = btn.ID;
            switch (btnID)
            {
                case "LeaderBtn": rightListDataBind("负责人", leadersList); break;
                case "PingMainBtn": rightListDataBind("平面", pingMainList); break;
                case "QianDuanBtn": rightListDataBind("前端", qianDuanUserList); break;
                case "HouTaiBtn": rightListDataBind("程序", houTaiList); break;
                case "FlashBtn": rightListDataBind("flash", flashList); break;
                case "OldUserBtn": rightListDataBind("往届", oldUserList); break;
                default: break;
            }
        }
    }
    protected void DeleteBtn_Click(object sender, EventArgs e)
    {
        if (menberHid.Value != null)
        {
            try
            {
                userDal.Delete(menberHid.Value.Trim());
                scripthelp.Alert("删除人员成功!", Page);
                GetListAfterGet(GroupNameLitr.Text);
            }
            catch(Exception ex)
            {
                ControlLog controlLog = new ControlLog("manager");
                controlLog.WriteDebugLog("DeleteBtn_Click-删除人员失败", ex.ToString(), user.realName);
                scripthelp.Alert("删除人员失败!", Page);
            }
        }
        else scripthelp.Alert("没有选中任何人员,不可以进行删除。", Page);
    }
    //添加人员
    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
            if (UserIn.Value != "" & GradeIn.Value != "")
            {
                OA_users newUser = new OA_users();
                newUser.realName = UserIn.Value.Trim();
                if (userDal.GetUser("realName", newUser.realName) != null)
                {
                    scripthelp.Alert("工作室已经存在" + newUser.realName + ",若真的要添加，请为该同学加不同的标识，如加上届数：10小花！", Page);
                    scripthelp.RunScript(this.Page, "document.getElementById('edit').style.display='block'");
                    UserIn.Focus();
                }
                else
                {
                    newUser.loginName = newUser.realName;
                    newUser.groups = this.GroupDropList.SelectedValue.Trim();
                    newUser.grade = GradeIn.Value.Trim();
                    newUser.roles = RolesDropList.SelectedValue.Trim();
                    newUser.school = SchoolIn.Value.Trim();
                    newUser.major = MajorIn.Value.Trim();
                    newUser.longNumber = LongNumIn.Value.Trim();
                    newUser.shortNumber = ShortNumIn.Value.Trim();
                    string[] columnArray = { "loginName", "realName", "groups", "roles", "grade", "school", "major", "longNumber", "shortNumber" };
                    ArrayList al = new ArrayList { newUser.loginName, newUser.realName, newUser.groups, newUser.roles, newUser.grade, newUser.school, newUser.major, newUser.longNumber, newUser.shortNumber };
                    try
                    {
                        userDal.insert(columnArray, newUser, al);
                        scripthelp.Alert("添加" + newUser.realName + "成功！", Page);
                    }
                    catch(Exception ex)
                    {
                        ControlLog controlLog = new ControlLog("manager");
                        controlLog.WriteDebugLog("SubmitBtn_Click-添加"+newUser.realName+"人员失败", ex.ToString(), user.realName);
                        scripthelp.Alert("添加" + newUser.realName + "失败!", Page);
                    }
                    GetListAfterGet(newUser.groups);
                }
            }
            else
            {
                scripthelp.Alert("姓名和届都不能空哦。", this.Page);
                scripthelp.RunScript(this.Page, "document.getElementById('edit').style.display='block'");
                UserIn.Focus();
            }//else
        }
    //修改个人资料
    protected void ChangPersonSubmitBtn_Click(object sender, EventArgs e)
    {
        string loginName = loginNameIn.Value.Trim();
        string oldPw = oldPwIn.Value.Trim();
        string newPw = newPwIn.Value.Trim();
        OA_users reUser = user;
        if (!Regex.IsMatch(loginName, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']") && !Regex.IsMatch(oldPw, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']") && !Regex.IsMatch(newPw, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']"))
        {
            string oldEncodePw = FormsAuthentication.HashPasswordForStoringInConfigFile(oldPw, "md5").Substring(0, 30);
            if (oldEncodePw == user.pw)
            {
                if (loginName!=user.loginName&&userDal.GetUser("loginName", loginName) != null)
                {
                    scripthelp.Alert("登录昵称" + loginName + "已经存在啦，请换昵称啦！", Page);
                    scripthelp.RunScript(Page, "document.getElementById('editPerson').style.display ='block';;document.getElementById('loginNameIn').focus();");
                }
                else
                {
                    reUser.pw = FormsAuthentication.HashPasswordForStoringInConfigFile(newPw, "md5").Substring(0, 30);
                    reUser.loginName = loginName;
                    reUser.longNumber = rLongNumIn.Value.Trim();
                    reUser.shortNumber = rShortNumIn.Value.Trim();
                    reUser.groups = groupDrDL.Text;
                    if (userDal.Update(user) == 1)
                    {
                        scripthelp.Alert("个人资料修改成功！", Page);
                        user = reUser;
                        DropListDataBind();
                        GetListAfterGet(reUser.groups);
                    }
                    else scripthelp.Alert("个人资料修改失败！", Page);
                }
            }
            else
            {
                scripthelp.Alert("输入的旧密码不正确，请重新输入！", Page);
            }
        }
    }

    protected void OnPageChanged(object sender, EventArgs e)
    {
       List< OA_users> oldUserList = userDal.GetList(oldUserPager.CurrentPageIndex, 8, "[roles]='往届'", "[grade]");
       rightListDataBind("往届", oldUserList);
    }

    protected void save_Click(object sender, EventArgs e)
    {
        string[] ids = Common.StringToArray(changeMenHid.Value);
        string[] colArray = { "roles", "grade", "school", "major", "longNumber", "shortNumber" };
        int gross = 0;
        OA_users cUser = new OA_users();
        try
        {
            for (int i = 0; i < userListRpt.Items.Count; i++)
            {
                cUser.id = Convert.ToInt32(ids[i]);
                string roles = ((HtmlInputText)userListRpt.Items[i].FindControl("roles")).Value.Trim();
                if (roles == "负责人" | roles == "组长" | roles == "组员" | roles == "往届")
                {
                    ArrayList al = new ArrayList {roles,((HtmlInputText)userListRpt.Items[i].FindControl("grade")).Value.Trim(),
                    ((HtmlInputText)userListRpt.Items[i].FindControl("school")).Value.Trim(),
                             ((HtmlInputText)userListRpt.Items[i].FindControl("major")).Value.Trim(),
                             ((HtmlInputText)userListRpt.Items[i].FindControl("longNumber")).Value.Trim(),
                         ((HtmlInputText)userListRpt.Items[i].FindControl("shortNumber")).Value.Trim()};
                    gross += userDal.Update(colArray, cUser, al);
                }
                else
                {
                    scripthelp.Alert("职务只能是负责人或组长或组员或往届,请重新修改再保存!", Page);
                    ((HtmlInputText)userListRpt.Items[i].FindControl("roles")).Focus();
                    return;
                }
            }
            if (gross == userListRpt.Items.Count)
            {
                scripthelp.Alert("保存修改成功!", Page);
                GetListAfterGet(GroupNameLitr.Text);
                DropListDataBind();
                oldUserPager.CurrentPageIndex = 1;
            }
        }
        catch(Exception ex)
        {
            ControlLog controlLog = new ControlLog("manager");
            controlLog.WriteDebugLog("save_Click-保存修改失败", ex.ToString(), user.realName);
            scripthelp.Alert("保存修改失败!", Page);
        }
    }
}