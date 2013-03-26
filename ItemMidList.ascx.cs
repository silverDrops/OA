using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ItemMidList : System.Web.UI.UserControl
{
    int itemCount;
    BaseDAL<OA_item> itemDal = new BaseDAL<OA_item>();
    private string whereString
    {
        get
        {
            return (string)ViewState["where"];
        }
        set
        {
            ViewState["where"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void DataBind(int currentPage,string whereStr)
    {
        whereString = whereStr;
        DataSet itemDataSet = itemDal.GetDataSetBySelectStr(currentPage, 10, "[id],[itemName],[leaderName],[status],[client],DATEDIFF(day,'" + DateTime.Now.ToString("yyyy-MM-dd") + "',[completeDate]) as remainDay",whereString, "[status]", out itemCount);
        DataTable itemTable = itemDataSet.Tables[1];
        itemAspNetPager.CurrentPageIndex = currentPage;
        itemAspNetPager.RecordCount = itemCount;
        if (itemTable != null)
        {
            ItemRptList.DataSource = itemTable;
            ItemRptList.DataBind();
        }
        else mid_list_project.InnerText = "暂无数据";
    }
    protected void OnPageChanged(object sender, EventArgs e)
    {
        DataBind(itemAspNetPager.CurrentPageIndex,whereString);
    }
}