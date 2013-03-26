using System;
using System.Collections;

public class OA_finance:BaseModel
{
public OA_finance(){
base.TableName = "OA_finance";
base.ColumnArray =  new string[]{"actionName","money","actionDetails","actionDate","actionStatus"};
}
#region Model
public int id 
{
set {base._id   = value; }
get { return base._id; }
}


private string _actionName;
public string actionName
{
	set{ _actionName=value;} 
	get{return _actionName;}
}

private int  _money;
public int money
{
	set{ _money=value;} 
	get{return _money;}
}

private string _actionDetails;
public string actionDetails
{
	set{ _actionDetails=value;} 
	get{return _actionDetails;}
}

private DateTime _actionDate;
public DateTime actionDate
{
	set{ _actionDate=value;} 
	get{return _actionDate;}
}

private int _actionStatus;
public int actionStatus
{
	set{ _actionStatus=value;} 
	get{return _actionStatus;}
}
#endregion Model
public override ArrayList ValueArray
{ 
set {
ArrayList al = new ArrayList(value);
this.actionName = Convert.ToString(al[0]);
this.money = Convert.ToInt32(al[1]);
this.actionDetails = Convert.ToString(al[2]);
this.actionDate = Convert.ToDateTime(al[3]);
this.actionStatus = Convert.ToInt32(al[4]);}
get {
ArrayList al = new ArrayList();
al.Add(this.actionName);
al.Add(this.money);
al.Add(this.actionDetails);
al.Add(this.actionDate);
al.Add(this.actionStatus);
return al; 
}
}
}