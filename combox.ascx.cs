using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class combox : System.Web.UI.UserControl
{
    private string _text;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        DropDownList1.Items.Insert(0, "请选择或输入");
    }
    public void Focus()
    {
        this.TextBox1.Focus();
    }
     // Text 重写了TextBox的Text属性 
        public String Text
        {
            get
            {
                _text = this.TextBox1.Text;
                return _text;
            }
            set
            {
                this.TextBox1.Text = value;
                _text = this.TextBox1.Text;
            }
        }

        //为控件增加一个可选择的项
        public void AddItem(String item)
        {
            this.DropDownList1.Items.Add(item);
            
        }

        //为控件增加一个可选择的项
        public void AddItem(ListItem item)
        {
            this.DropDownList1.Items.Add(item);
        }

        //为控件增删除一个可选择的项
        public void RemoveItem(ListItem item)
        {
            this.DropDownList1.Items.Remove(item);
        }
        //为控件增删除一个可选择的项
        public void RemoveItem(String item)
        {
            this.DropDownList1.Items.Remove(item);
        }
        //清除控件数据
        public void Clear()
        {
            this.DropDownList1.Items.Clear();
            this.TextBox1.Text = "";
        }
        //返回选项数量
        public int Count
        {
            get
            {
                return this.DropDownList1.Items.Count;
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //选择某项
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.TextBox1.Text = this.DropDownList1.SelectedItem.Text;
        }
}