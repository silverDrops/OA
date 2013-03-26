using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NoticeMidList : System.Web.UI.UserControl
{
    BaseDAL<OA_announcement> dalNotice = new BaseDAL<OA_announcement>();
    List<OA_announcement> noticesList = new List<OA_announcement>();
    public bool leaderF
    {
        get { return (bool)ViewState["leaderFlag"]; }
        set { ViewState["leaderFlag"] = value; }
    }
    private  int _noticeCuPage;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int noticeCuPage
    {
        get
        {
            _noticeCuPage = this.noticeAspNetPager.CurrentPageIndex;
            return _noticeCuPage;
        }
        set
        {
             this.noticeAspNetPager.CurrentPageIndex = value;
             _noticeCuPage = this.noticeAspNetPager.CurrentPageIndex;
        }
    }
    public void DataBind(int noticeId, List<OA_announcement> noticeList, int noticeCount,bool leaderFlag)
    {
        if (noticesList==null) mid_list_notice.InnerText = "暂无数据";
        else
        {
            noticesList = noticeList;
            noticeRptList.DataSource = noticeList;
            noticeRptList.DataBind();
            noticeAspNetPager.RecordCount = noticeCount;
            if (noticeId != 0)
            {
                noticeContentDataBind(noticeId);
                change.Visible = false;
            }
            else
            {
                noticeContent.Visible = false;
                mid_list_notice.Visible = !noticeContent.Visible;
                change.Visible = !noticeContent.Visible;
            }
            leaderF = leaderFlag;
            if (leaderF)
            {
                foreach (CheckBox checkbox in GetCheckboxs())
                {
                    checkbox.Style.Remove("display");
                }
            }
        }
    }

    protected void noticeContentDataBind(int noticeId)
    {
        change.Visible = false;
        mid_list_notice.Visible = false;
        OA_announcement notice = dalNotice.GetModel(noticeId, "id");
        if (notice != null)
        {
            noticeTitleSpan.InnerText = notice.title;
            posterSpan.InnerText = notice.poster;
            postDateSpan.InnerText = notice.postDate.ToString("yyy.MM.d");
            if (notice.content == "")
                notice.content = "无";
            contentP.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + notice.content;
            noticeContent.Visible = !mid_list_notice.Visible;
        }
        else noticeContent.InnerHtml = "<strong>尝试查看的公告不存在！</strong>";
    }

    public List<CheckBox> GetCheckboxs()
    {
        List<CheckBox> checkBoxList=new List<CheckBox>();
        for(int i=0;i<noticeRptList.Items.Count;i++)
        {
                checkBoxList.Add((CheckBox)noticeRptList.Items[i].FindControl("NoticeCheckBox"));
        }
        return checkBoxList;
    }

    public string GetCheckdId()
    {
        List<CheckBox> checkBoxList = GetCheckboxs();
        string ids="";
        foreach(CheckBox cBox in checkBoxList)
        {
            if(cBox.Checked) 
            {
                if (ids == "") ids = cBox.Text;
                else ids += "," + cBox.Text;
            }
            }
        return ids;
    }

    protected void noticePpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       int noticeId = int.Parse(e.CommandArgument.ToString());
       noticeContentDataBind(noticeId);
    }

    protected void OnPageChanged(object sender, EventArgs e)
    {
        List<OA_announcement> nextNoticeList = dalNotice.GetList(noticeAspNetPager.CurrentPageIndex, 10,"","[postDate] DESC");
        DataBind(0, nextNoticeList, noticeAspNetPager.RecordCount,leaderF );        
    }

    protected void backList_Click(object sender, EventArgs e)
    {
        mid_list_notice.Visible = true;
        change.Visible = true;
        noticeContent.Visible = !mid_list_notice.Visible;
    }
}