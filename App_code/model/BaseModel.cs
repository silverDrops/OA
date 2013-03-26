using System;
using System.Collections;
using System.Web;

/// <summary>
///BaseModel 的摘要说明
/// </summary>
public abstract class BaseModel
{
    public BaseModel()
    { }
    public int _id;
    public string TableName = "BaseTable";
    public string[] ColumnArray = new string[1];
    public abstract ArrayList ValueArray { get; set; }
}