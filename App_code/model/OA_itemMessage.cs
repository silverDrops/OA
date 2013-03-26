using System;
using System.Collections;

public class OA_itemMessage : BaseModel
{
    public OA_itemMessage()
    {
        base.TableName = "OA_itemMessage";
        base.ColumnArray = new string[] { "itemId", "userName", "message", "postDate", "groupStatus" };
    }
    #region Model
    public int id
    {
        set { base._id = value; }
        get { return base._id; }
    }


    private int _itemId;
    public int itemId
    {
        set { _itemId = value; }
        get { return _itemId; }
    }

    private string _userName;
    public string userName
    {
        set { _userName = value; }
        get { return _userName; }
    }

    private string _message;
    public string message
    {
        set { _message = value; }
        get { return _message; }
    }

    private DateTime _postDate;
    public DateTime postDate
    {
        set { _postDate = value; }
        get { return _postDate; }
    }

    private int _groupStatus;
    public int groupStatus
    {
        set { _groupStatus = value; }
        get { return _groupStatus; }
    }
    #endregion Model
    public override ArrayList ValueArray
    {
        set
        {
            ArrayList al = new ArrayList(value);
            this.itemId = Convert.ToInt32(al[0]);
            this.userName = Convert.ToString(al[1]);
            this.message = Convert.ToString(al[2]);
            this.postDate = Convert.ToDateTime(al[3]);
            this.groupStatus = Convert.ToInt32(al[4]);
        }
        get
        {
            ArrayList al = new ArrayList();
            al.Add(this.itemId);
            al.Add(this.userName);
            al.Add(this.message);
            al.Add(this.postDate);
            al.Add(this.groupStatus);
            return al;
        }
    }
}