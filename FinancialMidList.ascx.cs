using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FinancialMidList : System.Web.UI.UserControl
{
    BaseDAL<OA_finance> financeDal = new BaseDAL<OA_finance>();
    public string whereString
    {
        get { return (string)ViewState["finWhereStr"]; }
        set { ViewState["finWhereStr"] = value; }
    }
     int finCount;
    public bool finManagerFla
    {
        get{return  (bool)ViewState["finManagerFla"];}
        set{ViewState["finManagerFla"]=value;}
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void DataBind(List<OA_finance> financeList,int currentPage,bool financeManagerFlag,int financeCount,string whereStr)
    {
        whereString = whereStr;
        finManagerFla = financeManagerFlag;
        if (financeList.Count == 0)
            mid_list_financial.InnerText = "暂无数据";
        else
        {
            FinanceRptList.DataSource = financeList;
            FinanceRptList.DataBind();
            financeAspNetPager.CurrentPageIndex = currentPage;
            financeAspNetPager.RecordCount = financeCount;
            if (financeManagerFlag)
            {
                foreach (CheckBox checkBox in GetCheckboxs())
                    checkBox.Style.Remove("display");
            }
        }
    }
    public List<CheckBox> GetCheckboxs()
    {
        List<CheckBox> checkBoxList = new List<CheckBox>();
        for (int i = 0; i < FinanceRptList.Items.Count; i++)
        {
            checkBoxList.Add((CheckBox)FinanceRptList.Items[i].FindControl("FinanceCheckBox"));
        }
        return checkBoxList;
    }

    public string GetCheckdId()
    {
        List<CheckBox> checkBoxList = GetCheckboxs();
        string ids = "";
        foreach (CheckBox cBox in checkBoxList)
        {
            if (cBox.Checked)
            {
                if (ids == "") ids = cBox.Text;
                else ids += "," + cBox.Text;
            }
        }
        return ids;
    }
    protected void OnPageChanged(object sender, EventArgs e)
    { 
        List<OA_finance> finList = financeDal.GetList(financeAspNetPager.CurrentPageIndex, 10, whereString, "[actionDate] DESC", out finCount);
        DataBind(finList, financeAspNetPager.CurrentPageIndex, finManagerFla, finCount,whereString);
    }
}