using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;

public partial class read : System.Web.UI.Page
{
    OA_users user = new OA_users();
    BaseDAL<OA_item> itemDal = new BaseDAL<OA_item>();
    BaseDAL<OA_comment> commentDal = new BaseDAL<OA_comment>();
    protected void Page_Load(object sender, EventArgs e)
    {
        user = (OA_users)Session["user"];
        if (Request.QueryString.Get("id") == null)
            Response.Redirect("home.aspx");
        if (!IsPostBack)
        {
            GetList(Convert.ToInt32(Request.QueryString["id"]));
        }
    }
    protected void GetList(int itemId)
    {
        OA_item item = new OA_item();
        List<OA_item> itemList = itemDal.GetList(1, 2000);
        Header1.DataBind(itemList, user.realName, user.loginName);
        item = itemDal.GetModel(itemId, "id");
        if (item != null)
        {
            if (item.leaderName == user.realName&&item.status==100) itemComplete.Visible = true;
            this.itemNameLitr.Text = item.itemName;
            GetCommentList(1, itemId);
            leaderLitr.Text = item.leaderName;
            if (item.linkUrl == null | item.linkUrl == "")
            {
                linkLitr.Text = "暂无";
                link.Enabled = false;
            }
            else
            {
                linkLitr.Text = item.linkUrl;
                link.NavigateUrl = item.linkUrl;
            }
            clientLitr.Text = item.client;
            moneyLitr.Text = item.money.ToString();
            comDateLitr.Text = item.completeDate.ToString("yyy.MM.d");
            itemDetailLitr.Text = item.itemDetials;
            programProgress.InnerText = item.status.ToString() + "%";
        }
        else Response.Redirect("home.aspx");
        if (string.IsNullOrEmpty(item.zipUrl))
            fileDownLoadUrl.Visible = false;
        else fileDownLoadUrl.Attributes.Add("href", item.zipUrl);
        itemProgress.Style.Add("width", 5.36 * item.status + "px");
        if (!string.IsNullOrEmpty(item.planeName))
            planeDetails.DataBind(itemId, item.planeName, "平面",item.status);
        else planeDetails.Visible = false;
        if (!string.IsNullOrEmpty(item.frontendName))
            frontendDetails.DataBind(itemId, item.frontendName, "前端", item.status);
        else frontendDetails.Visible = false;
        if (!string.IsNullOrEmpty(item.programName ))
            programDetails.DataBind(itemId, item.programName, "后台", item.status);
        else programDetails.Visible = false;
        if (!string.IsNullOrEmpty(item.flashName))
            flashDetails.DataBind(itemId, item.flashName, "flash", item.status);
        else flashDetails.Visible = false;
    }

    protected void GetCommentList(int currentPage, int itemId)
    {
        int commentPageCount;
        List<OA_comment> commentList = commentDal.GetList(currentPage, 4, "[itemId]=" + itemId.ToString(), "", out commentPageCount);
        if (commentList != null)
        {
            commentRp.DataSource = commentList;
            commentRp.DataBind();
        }
        commentPager.RecordCount = commentPageCount;
    }

    public void SaveBtn_click(object sender, EventArgs e)
    {
            if (message_box.InnerText != "")
            {
                int itemId = Convert.ToInt32(Request.QueryString["id"]);
                OA_comment comment = new OA_comment();
                comment.itemId = itemId;
                comment.userName = user.realName;
                comment.comment = message_box.InnerText;
                string[] columnName = { "userName", "itemId", "comment" };
                ArrayList columnValue = new ArrayList { comment.userName, comment.itemId, comment.comment };
                commentDal.insert(columnName, comment, columnValue);
                GetCommentList(commentPager.CurrentPageIndex, itemId);
            }
    }
    public void OnPageChanged(object sender, EventArgs e)
    {
        int itemId = Convert.ToInt32(Request.QueryString["id"]);
        GetCommentList(commentPager.CurrentPageIndex, itemId);
    }
    public void uploading_Click(object sender, EventArgs e)
    {
        if (this.InputFile.HasFile)
        {
            ProgressBar.Visible = true;
            OA_item item = new OA_item();
            item = itemDal.GetModel(Convert.ToInt32(Request.QueryString["id"]), "id");
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
                    if (!Directory.Exists(saveFileUrl))
                    {
                        Directory.CreateDirectory(saveFileUrl);
                    }
                    InputFile.MoveTo(saveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                    if (!item.zipUrl.Equals(Path + fileName))
                    {
                        item.zipUrl = Path + fileName;
                        itemDal.Update(item);
                    }
                }
                catch (Exception ex)
                {
                    ControlLog controlLog = new ControlLog("read");
                    controlLog.WriteDebugLog("uploading_Click", ex.ToString(), user.realName);
                    scripthelp.Alert("上传文件失败！",Page);
                }
            }
        }
    }
}