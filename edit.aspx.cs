using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class edit : System.Web.UI.Page
{
    int itemId;
    OA_users user = new OA_users();
    OA_item item = new OA_item();
    BaseDAL<OA_users> userDal = new BaseDAL<OA_users>();
    BaseDAL<OA_item> itemDal = new BaseDAL<OA_item>();
    BaseDAL<OA_step> stepDal = new BaseDAL<OA_step>();
    BaseDAL<OA_itemMessage> itemMessageDal = new BaseDAL<OA_itemMessage>();
    protected void Page_Load(object sender, EventArgs e)
    {
        user = (OA_users)Session["user"];
        if (user == null) Response.Redirect("sign.aspx");
        if (string.IsNullOrEmpty(Request.QueryString.Get("id"))) Response.Redirect("home.aspx");
        itemId = Convert.ToInt32(Request.QueryString["id"]);
        item = itemDal.GetModel(itemId, "id");//不考虑项目负责人没有参与项目小组负责人的情况
        if (!IsPostBack)
        {
            if (item!=null&&item.status < 100 && (item.leaderName == user.realName || item.frontendName == user.realName || item.planeName == user.realName || item.programName == user.realName|| item.flashName == user.realName))
            {
                if (item.status < 90)
                {
                    if (item.leaderName == user.realName)
                    {
                        leaderInput.Attributes.Remove("readOnly");
                        leaderInput.Attributes.Remove("readOnly");
                        linkInput.Attributes.Remove("readOnly");
                        clientInput.Attributes.Remove("readOnly");
                        moneyInput.Attributes.Remove("readOnly");
                        yearIn.Attributes.Remove("readOnly");
                        monthIn.Attributes.Remove("readOnly");
                        dayIn.Attributes.Remove("readOnly");
                        itemDetialsIn.Disabled =false;
                    }
                }
                else scripthelp.RunScript(this, "var inputs=document.getElementsByTagName('input');for (var i = 0; i < inputs.length; i++) {if(inputs[i].type=='text')inputs[i].readOnly=true}");
                DataBind(item);
            }
            else Response.Redirect("home.aspx");
        }
    }
    //返回用户正在进行的项目数
    protected int DataBind(OA_item item)
    {
        itemNameLitr.Text = item.itemName;
        leaderInput.Value = item.leaderName;
        clientInput.Value = item.itemName;
        moneyInput.Value = item.money.ToString();
        yearIn.Value = item.completeDate.Year.ToString();
        monthIn.Value = item.completeDate.Month.ToString();
        dayIn.Value = item.completeDate.Day.ToString();
        programProgress.InnerText = item.status.ToString() + "%";
        if (item.linkUrl == null | item.linkUrl == "") linkInput.Value = "暂无";//如果linUrl没有则显示“暂无”
        else linkInput.Value = item.linkUrl;
        itemDetialsIn.Value = item.itemDetials;
        itemProgress.Style.Add("width", 5.36 * item.status + "px");
        if (item.status >= 90 &&itemDetialsIn.Disabled ==false)
        {
            submitBtn.Visible = false;
            itemComplete.Visible = true;
        }
        if (user.groups == "程序") groupNameLitr.Text = "后台" + "组：" + user.realName;
        else groupNameLitr.Text = user.groups + "组：" + user.realName;
        OA_itemMessage programMess = new OA_itemMessage();
        List<OA_itemMessage> itemMessList = itemMessageDal.GetList(1, 1, "[itemId]=" + item.id + " and [userName]='" + user.realName + "'", "");
        if (itemMessList != null & itemMessList.Count != 0)
        {
            programMess = itemMessList[0];//把符合项目id和参与人名字的项目信息赋值到programMess
        }
        if (programMess != null)
        {
            groupProgress.Style.Add("width", 5.54 * programMess.groupStatus + "px");
            OA_step steps = stepDal.GetModel(programMess.id, "[itemMessageId]");
            string[] stepsOrigiArray;
            if (steps == null)
            {
                stepsOrigiArray = Common.setOrigiStepList(user.groups);
            }
            else stepsOrigiArray = Common.StringToArray(steps.step);
            stepRpt.DataSource = stepsOrigiArray;
            stepRpt.DataBind();
        }
        if (programMess.id != 0)//如果读取的programMess存在
        {
            messageTextarea.Value = programMess.message;
            messageDateLtr.Text = programMess.postDate.ToString("yyy.MM.d");
        }
        itemMessageIdHidden.Value = programMess.id.ToString();
        List<OA_item> itemList = itemDal.GetList(1, 2000);
        return Header1.DataBind(itemList, user.realName, user.loginName);
    }
    public void submitBtn_Click(object sender, EventArgs e)
    {
        try
        {
            user = userDal.GetModel(user.id, "id");
            int addFlag = 0;//记录保存的操作是否成功
            int itemMessId = Convert.ToInt32(itemMessageIdHidden.Value);//绑定用户组信息时读取出来的itemMessage.id
            OA_itemMessage groupMess = new OA_itemMessage();
            int origiGroupStatus = -1;
            if (itemMessId == 0)//用户在该项目的小组信息不存在,初始化groupMess
            {
                groupMess.userName = user.realName;
                groupMess.itemId = item.id;
                groupMess.groupStatus = 0;
                groupMess.postDate = DateTime.Now;
            }
            else groupMess = itemMessageDal.GetModel(itemMessId, "id");
            groupMess.message = messageTextarea.Value;
            if (groupProLenHid.Value != (5.54 * groupMess.groupStatus + "px;"))//组的进度条有变化
            {
                int statusLength = groupProLenHid.Value.Length - 2;
                origiGroupStatus = groupMess.groupStatus;
                groupMess.groupStatus = Convert.ToInt32(Convert.ToDouble(groupProLenHid.Value.Substring(0, statusLength)) / 5.54);
                if (itemMessageIdHidden.Value == "0")
                {
                    itemMessageDal.Add(groupMess);//改组信息不存在则add
                    groupMess = itemMessageDal.GetList(1, 1, "[itemId]=" + item.id + " and [userName]='" + user.realName + "'", "")[0];
                    if (groupMess.id != 0) addFlag = 1;
                }
                else
                {
                    groupMess.id = Convert.ToInt32(itemMessageIdHidden.Value);
                    addFlag = itemMessageDal.Update(groupMess);//改组信息存在则update
                }
                if (addFlag == 0) scripthelp.Alert("项目小组进度和留言保存失败！", this.Page);
                else addFlag = 0;
                int groupCount = 0;//计算该项目的参与的小组数0~4
                if (item.frontendName != null & item.frontendName != "")
                    groupCount++;
                if (item.planeName != null & item.planeName != "")
                    groupCount++;
                if (item.programName != null & item.programName != "")
                    groupCount++;
                if (item.flashName != null & item.flashName != "")
                    groupCount++;
                if(groupCount>0)item.status += (groupMess.groupStatus - origiGroupStatus) * 9 / (groupCount * 10);//根据用户更改后自己的进度而计算项目的总进度0~90
            }
            else//用户进度条无变化
            {
                itemMessageDal.Add(groupMess);
                groupMess = itemMessageDal.GetList(1, 1, "[itemId]=" + item.id + " and [userName]='" + user.realName + "'", "")[0];//获取刚add的itemMessage对象
                if (groupMess.id != 0) addFlag = 1;
            }
            OA_step steps = stepDal.GetModel(groupMess.id, "[itemMessageId]");
            string[] stepsArray;
            if (steps == null)//数据库不存在该组步骤信息则加载原始步骤
                stepsArray = Common.setOrigiStepList(user.groups);
            else stepsArray = Common.StringToArray(steps.step);
            if (stepHiddens.Value.Trim() != Common.ArrayToString(stepsArray))//隐藏的步骤记录input的值有变化
            {
                try
                {
                    if (steps == null)
                    {
                        OA_step newStep = new OA_step();
                        newStep.step = stepHiddens.Value.Trim();
                        newStep.itemMessageId = groupMess.id;
                        stepDal.Add(newStep);
                        addFlag = 1;
                    }
                    else
                    {
                        steps.step = stepHiddens.Value.Trim();
                        addFlag += stepDal.Update(steps);
                    }
                }
                catch(Exception ex)
                {
                    ControlLog controlLog = new ControlLog("edit");
                    controlLog.WriteDebugLog("submitBtn_Click-项目步骤保存失败！", ex.ToString(), user.realName);
                    scripthelp.Alert("项目步骤保存失败！", this.Page);
                }
                if (addFlag == 0)
                    scripthelp.Alert("项目步骤保存失败！", this.Page);
                else addFlag = 0;
            }
            if (user.realName == item.leaderName)//是项目负责人则要保存其对项目信息的更改
            {
                item.leaderName = leaderInput.Value.Trim();
                if (linkInput.Value.Trim() == "暂无")
                {
                    item.linkUrl = "";
                }
                else item.linkUrl = linkInput.Value.Trim();
                item.client = clientInput.Value.Trim();
                item.money = Convert.ToInt32(moneyInput.Value.Trim());
                item.completeDate = Convert.ToDateTime(yearIn.Value.Trim() + "-" + monthIn.Value.Trim() + "-" + dayIn.Value.Trim());
                item.itemDetials = itemDetialsIn.InnerText.Trim();
                item.zipUrl = "";
            }
            addFlag = itemDal.Update(item);
            int userItemsCount = DataBind(item);
            if (origiGroupStatus != -1 && userItemsCount != 0)
            {
                user.status += (groupMess.groupStatus - origiGroupStatus) / userItemsCount;
                if (user.status > 100) user.status = 100;
                else if (user.status < 0) user.status = 0;
                userDal.Update(user);
            }
            else DataBind(item);
            if (addFlag != 0) scripthelp.Alert("保存成功！", this.Page);
        }
        catch (Exception ex)
        {
            ControlLog controlLog = new ControlLog("edit");
            controlLog.WriteDebugLog("submitBtn_Click-项目详细信息保存失败！", ex.ToString(), user.realName);
            scripthelp.Alert("项目详细信息保存失败！", this.Page);
        }
    }
    public void uploading_Click(object sender, EventArgs e)
    {
        if (this.InputFile.HasFile)
        {
            string fileName = InputFile.FileName;
            string extendName = System.IO.Path.GetExtension(fileName);
            //前后台都验证，以防客户浏览器禁用js或更改js
            if (extendName == ".rar" || extendName == ".zip" || extendName == ".gz" || extendName == ".7z" || extendName == ".7Z" || extendName == ".RAR" || extendName == ".ZIP" || extendName == ".GZ")
            {
                try
                {
                    string Path = "upFile/" + DateTime.Now.Year + "/" + item.itemName + "/";
                    string saveFileUrl = System.Web.HttpContext.Current.Request.MapPath(Path);
                    string saveFileName = System.IO.Path.Combine(saveFileUrl, fileName);
                    item.zipUrl = Path + fileName;
                    if (!Directory.Exists(saveFileUrl))
                    {
                        Directory.CreateDirectory(saveFileUrl);
                    }
                    InputFile.MoveTo(saveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                    itemDal.Update(item);
                }
                catch(Exception ex)
                {   ControlLog controlLog = new ControlLog("edit");
                controlLog.WriteDebugLog("uploading_Click", ex.ToString(), user.realName);
                scripthelp.Alert("上传文件失败！", Page);
                }
            }
        }
    }
    public void consignBtn_Click(object sender, EventArgs e)
    {
            item.status = 100;
            item.completeDate = DateTime.Now;
            item.itemDetials = itemDetialsIn.InnerText;
            if (itemDal.Update(item) == 1)
            {
                if (item.money != 0 & item.money != null)
                {
                    BaseDAL<OA_finance> financeDal = new BaseDAL<OA_finance>();
                    OA_finance finance = new OA_finance();
                    finance.money = item.money;
                    finance.actionDate = DateTime.Now;
                    finance.actionDetails = "委托方：" + item.client + "，项目负责人：" + item.leaderName; ;
                    finance.actionName = item.itemName;
                    financeDal.Add(finance);
                }
                moneyInput.Value = item.money.ToString();
                yearIn.Value = item.completeDate.Year.ToString();
                monthIn.Value = item.completeDate.Month.ToString();
                dayIn.Value = item.completeDate.Day.ToString();
                itemDetialsIn.Value = item.itemDetials;
                programProgress.InnerText = item.status + "%";
                itemProgress.Style.Add("width", 5.36 * item.status + "px");
                List<OA_item> itemList = itemDal.GetList(1, 2000);
                if (Header1.DataBind(itemList, user.realName, user.loginName) == 0)
                {
                    user.status = 100;
                    userDal.Update(user);
                }
                scripthelp.Alert("项目交付成功！", this.Page);
            }
            else scripthelp.Alert("项目交付失败！", Page);
        }
}