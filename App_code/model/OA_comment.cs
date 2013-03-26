using System;
using System.Collections;

public class OA_comment : BaseModel
{
    public OA_comment()
    {
        base.TableName = "OA_comment";
        base.ColumnArray = new string[] { "userName", "itemId", "comment", "postDate" };
    }
    #region Model
    public int id
    {
        set { base._id = value; }
        get { return base._id; }
    }


    private string _userName;
    public string userName
    {
        set { _userName = value; }
        get { return _userName; }
    }

    private int _itemId;
    public int itemId
    {
        set { _itemId = value; }
        get { return _itemId; }
    }

    private string _comment;
    public string comment
    {
        set { _comment = value; }
        get { return _comment; }
    }

    private DateTime _postDate;
    public DateTime postDate
    {
        set { _postDate = value; }
        get { return _postDate; }
    }
    #endregion Model
    public override ArrayList ValueArray
    {
        set
        {
            ArrayList al = new ArrayList(value);
            this.userName = Convert.ToString(al[0]);
            this.itemId = Convert.ToInt32(al[1]);
            this.comment = Convert.ToString(al[2]);
            this.postDate = Convert.ToDateTime(al[3]);
        }
        get
        {
            ArrayList al = new ArrayList();
            al.Add(this.userName);
            al.Add(this.itemId);
            al.Add(this.comment);
            al.Add(this.postDate);
            return al;
        }
    }
}