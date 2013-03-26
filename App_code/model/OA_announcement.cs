using System;
using System.Collections;

public class OA_announcement : BaseModel
{
    public OA_announcement()
    {
        base.TableName = "OA_announcement";
        base.ColumnArray = new string[] { "poster", "title", "content", "postDate" };
    }
    #region Model
    public int id
    {
        set { base._id = value; }
        get { return base._id; }
    }


    private string _poster;
    public string poster
    {
        set { _poster = value; }
        get { return _poster; }
    }

    private string _title;
    public string title
    {
        set { _title = value; }
        get { return _title; }
    }

    private string _content;
    public string content
    {
        set { _content = value; }
        get { return _content; }
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
            this.poster = Convert.ToString(al[0]);
            this.title = Convert.ToString(al[1]);
            this.content = Convert.ToString(al[2]);
            this.postDate = Convert.ToDateTime(al[3]);
        }
        get
        {
            ArrayList al = new ArrayList();
            al.Add(this.poster);
            al.Add(this.title);
            al.Add(this.content);
            al.Add(this.postDate);
            return al;
        }
    }
}