using System;
using System.Collections;

/// <summary>
/// OA_log 登录错误用户日志
/// </summary>
public class OA_log : BaseModel
{
        public OA_log()
    {
        base.TableName = "OA_log";
        base.ColumnArray = new string[] { "id", "ip","addtime","wrongPw" };
    }
        #region Model
        private int _id;
    private string _ip;
    private DateTime _addtime;
    private string _wrongPw;

    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string ip
    {
        get { return _ip; }
        set { _ip = value; }
    }
    public DateTime addtime
    {
        get { return _addtime; }
        set { _addtime = value; }
    }
    public string wrongPw
    {
        get { return _wrongPw; }
        set { _wrongPw = value; }
    }
        #endregion
    public override ArrayList ValueArray
    {
        set
        {
            ArrayList al = new ArrayList(value);
            this.id = Convert.ToInt32(al[0]);
            this.ip= Convert.ToString(al[1]);
            this.addtime = Convert.ToDateTime(al[2]);
            this.wrongPw = Convert.ToString(al[3]);
        }
        get
        {
            ArrayList al = new ArrayList();
            al.Add(this.id);
            al.Add(this.ip);
            al.Add(this.addtime);
            al.Add(this.wrongPw);
            return al;
        }
    }
}
        
