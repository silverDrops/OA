using System;
using System.Collections;

public class OA_item:BaseModel
{
public OA_item(){
base.TableName = "OA_item";
base.ColumnArray =  new string[]{"itemName","client","money","status","itemDetials","linkUrl","zipName","leaderName","planeName","frontendName","programName","flashName","completeDate","zipUrl"};
}
#region Model
public int id 
{
set {base._id   = value; }
get { return base._id; }
}

private string _itemName;
public string itemName
{
	set{ _itemName=value;} 
	get{return _itemName;}
}

private string _client;
public string client
{
	set{ _client=value;} 
	get{return _client;}
}

private int  _money;
public int  money
{
	set{ _money=value;} 
	get{return _money;}
}

private int _status;
public int status
{
	set{ _status=value;} 
	get{return _status;}
}

private DateTime _completeDate;
public DateTime completeDate
{
	set{ _completeDate=value;} 
	get{return _completeDate;}
}

private string _itemDetials;
public string itemDetials
{
	set{ _itemDetials=value;} 
	get{return _itemDetials;}
}

private string _linkUrl;
public string linkUrl
{
	set{ _linkUrl=value;} 
	get{return _linkUrl;}
}

private string _zipUrl;
public string zipUrl
{
	set{ _zipUrl=value;} 
	get{return _zipUrl;}
}

private string _zipName;
public string zipName
{
	set{ _zipName=value;} 
	get{return _zipName;}
}

private string _leaderName;
public string leaderName
{
	set{ _leaderName=value;} 
	get{return _leaderName;}
}

private string _planeName;
public string planeName
{
	set{ _planeName=value;} 
	get{return _planeName;}
}

private string _frontendName;
public string frontendName
{
	set{ _frontendName=value;} 
	get{return _frontendName;}
}

private string _programName;
public string programName
{
	set{ _programName=value;} 
	get{return _programName;}
}

private string _flashName;
public string flashName
{
	set{ _flashName=value;} 
	get{return _flashName;}
}
#endregion Model
public override ArrayList ValueArray
{ 
set {
ArrayList al = new ArrayList(value);
this.itemName = Convert.ToString(al[0]);
this.client = Convert.ToString(al[1]);
this.money = Convert.ToInt32(al[2]);
this.status = Convert.ToInt32(al[3]);
this.itemDetials = Convert.ToString(al[4]);
this.linkUrl = Convert.ToString(al[5]);
this.zipName = Convert.ToString(al[6]);
this.leaderName = Convert.ToString(al[7]);
this.planeName = Convert.ToString(al[8]);
this.frontendName = Convert.ToString(al[9]);
this.programName = Convert.ToString(al[10]);
this.flashName = Convert.ToString(al[11]);
this.completeDate = Convert.ToDateTime(al[12]); 
this.zipUrl = Convert.ToString(al[13]);}
get {
ArrayList al = new ArrayList();
al.Add(this.itemName);
al.Add(this.client);
al.Add(this.money);
al.Add(this.status);
al.Add(this.itemDetials);
al.Add(this.linkUrl);
al.Add(this.zipName);
al.Add(this.leaderName);
al.Add(this.planeName);
al.Add(this.frontendName);
al.Add(this.programName);
al.Add(this.flashName);
al.Add(this.completeDate);
al.Add(this.zipUrl);
return al; 
}
}
}