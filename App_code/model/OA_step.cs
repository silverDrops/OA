using System;
using System.Collections;

public class OA_step : BaseModel
{
    public OA_step()
    {
        base.TableName = "OA_step";
        base.ColumnArray = new string[] { "itemMessageId", "step" };
    }
    #region Model
    public int id
    {
        set { base._id = value; }
        get { return base._id; }
    }


    private int _itemMessageId;
    public int itemMessageId
    {
        set { _itemMessageId = value; }
        get { return _itemMessageId; }
    }

    private string _step;
    public string step
    {
        set { _step = value; }
        get { return _step; }
    }
    #endregion Model
    public override ArrayList ValueArray
    {
        set
        {
            ArrayList al = new ArrayList(value);
            this.itemMessageId = Convert.ToInt32(al[0]);
            this.step = Convert.ToString(al[1]);
        }
        get
        {
            ArrayList al = new ArrayList();
            al.Add(this.itemMessageId);
            al.Add(this.step);
            return al;
        }
    }
}