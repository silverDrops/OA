using System;
using System.Collections;

public class OA_users : BaseModel
{
    public OA_users()
    {
        base.TableName = "OA_users";
        base.ColumnArray = new string[] { "loginName", "realName", "pw", "groups", "status", "roles", "grade", "school", "major", "longNumber", "shortNumber" };
    }
    #region Model
    public int id
    {
        set { base._id = value; }
        get { return base._id; }
    }


    private string _loginName;
    public string loginName
    {
        set { _loginName = value; }
        get { return _loginName; }
    }

    private string _realName;
    public string realName
    {
        set { _realName = value; }
        get { return _realName; }
    }

    private string _pw;
    public string pw
    {
        set { _pw = value; }
        get { return _pw; }
    }

    private string _groups;
    public string groups
    {
        set { _groups = value; }
        get { return _groups; }
    }

    private int _status;
    public int status
    {
        set { _status = value; }
        get { return _status; }
    }

    private string _roles;
    public string roles
    {
        set { _roles = value; }
        get { return _roles; }
    }

    private string _grade;
    public string grade
    {
        set { _grade = value; }
        get { return _grade; }
    }

    private string _school;
    public string school
    {
        set { _school = value; }
        get { return _school; }
    }

    private string _major;
    public string major
    {
        set { _major = value; }
        get { return _major; }
    }

    private string _longNumber;
    public string longNumber
    {
        set { _longNumber = value; }
        get { return _longNumber; }
    }

    private string _shortNumber;
    public string shortNumber
    {
        set { _shortNumber = value; }
        get { return _shortNumber; }
    }
    #endregion Model
    public override ArrayList ValueArray
    {
        set
        {
            ArrayList al = new ArrayList(value);
            this.loginName = Convert.ToString(al[0]);
            this.realName = Convert.ToString(al[1]);
            this.pw = Convert.ToString(al[2]);
            this.groups = Convert.ToString(al[3]);
            this.status = Convert.ToInt32(al[4]);
            this.roles = Convert.ToString(al[5]);
            this.grade = Convert.ToString(al[6]);
            this.school = Convert.ToString(al[7]);
            this.major = Convert.ToString(al[8]);
            this.longNumber = Convert.ToString(al[9]);
            this.shortNumber = Convert.ToString(al[10]);
        }
        get
        {
            ArrayList al = new ArrayList();
            al.Add(this.loginName);
            al.Add(this.realName);
            al.Add(this.pw);
            al.Add(this.groups);
            al.Add(this.status);
            al.Add(this.roles);
            al.Add(this.grade);
            al.Add(this.school);
            al.Add(this.major);
            al.Add(this.longNumber);
            al.Add(this.shortNumber);
            return al;
        }
    }
}