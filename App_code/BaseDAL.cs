using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// BaseDAL 的摘要说明
/// </summary>
public class BaseDAL<T> where T : BaseModel, new()
{
    public string SQLString = SqlHelper.ConnString;
    #region 构造函数
    public BaseDAL()
    { }

    public BaseDAL(string SQL)
    {
        SQLString = SQL;
    }
    #endregion

    #region 添加一条数据
    /// <summary>
    /// 添加一条数据
    /// </summary>
    /// <param name="model">泛型对象</param>
    /// <returns>返回受影响行第一条数据</returns>
    public int Add(T model)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into ");
        strSql.Append(model.TableName);
        strSql.Append("(");
        strSql.Append(Common.ArrayToString(model.ColumnArray));
        strSql.Append(") values (");
        strSql.Append(Common.ArrayToParam(model.ColumnArray));
        strSql.Append(")");
        SqlParameter[] parameters = new SqlParameter[model.ColumnArray.Length];
        ArrayList al = model.ValueArray;
        for (int i = 0; i < model.ColumnArray.Length; i++)
        {
            SqlParameter sp = new SqlParameter(model.ColumnArray[i], al[i]);//把全部属性都add进sqlparameter
            //HttpContext.Current.Response.Write(model.ColumnArray[i] + "," + sp.Value.ToString());
            //HttpContext.Current.Response.Write("," + i.ToString() + "<br/>");
            parameters[i] = sp;
        }
        //HttpContext.Current.Response.Write(strSql);
        object obj = SqlHelper.ExecuteScalar(SQLString, CommandType.Text, strSql.ToString(), parameters);
        return Convert.ToInt32(obj);
    }
    #endregion

    #region 插入某些列值
        /// <summary>
    /// 插入某些列值
    /// </summary>
    /// <param name="columnArray">列名数组</param>
    /// <param name="model">泛型对象</param>
    /// <param name="al">对应列的值的数组</param>
    public void insert(string[] columnArray,T model,ArrayList al)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("insert into ");
        strSql.Append(model.TableName);
        strSql.Append("(");
        strSql.Append(Common.ArrayToString(columnArray));
        strSql.Append(") values (");
        strSql.Append(Common.ArrayToParam(columnArray));
        strSql.Append(")");
        SqlParameter[] parameters = new SqlParameter[columnArray.Length];
       
        for (int i = 0; i <columnArray.Length; i++)
        {
            SqlParameter sp = new SqlParameter(columnArray[i], al[i]);//把全部属性都add进sqlparameter
            //HttpContext.Current.Response.Write(model.ColumnArray[i] + "," + sp.Value.ToString());
            //HttpContext.Current.Response.Write("," + i.ToString() + "<br/>");
            parameters[i] = sp;
        }
        //HttpContext.Current.Response.Write(strSql);
        object obj = SqlHelper.ExecuteScalar(SQLString, CommandType.Text, strSql.ToString(), parameters);
    }
    #endregion

    #region 更新一条数据
    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// <param name="model">泛型对象</param>
    /// <returns>受影响行数</returns>
    public int Update(T model)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update " + model.TableName + " set ");
        strSql.Append(Common.ArrayToSetStr(model.ColumnArray));
        strSql.Append(" where id=@id ");
        SqlParameter[] parameters = new SqlParameter[model.ColumnArray.Length + 1];//因为ColumnArray里面不带id，所以要加一
        ArrayList al = model.ValueArray;
        for (int i = 0; i < model.ColumnArray.Length; i++)
        {
            SqlParameter sp = new SqlParameter(model.ColumnArray[i], al[i]);
            //HttpContext.Current.Response.Write(model.ColumnArray[i] + "," + sp.Value.ToString());
            //HttpContext.Current.Response.Write("," + i.ToString() + "<br/>");
            parameters[i] = sp;
        }
        parameters[model.ColumnArray.Length] = new SqlParameter("id", model._id);//把id也add进去
        int flag = SqlHelper.ExecuteNonQuery(SQLString, CommandType.Text, strSql.ToString(), parameters);
        return flag;
    }
    #endregion

    #region 更新一条数据的某些列值
    /// <summary>
    /// 更新一条数据的某些列值
    /// </summary>
    /// <param name="columnArray">列名数组</param>
    /// <param name="model">泛型对象</param>
    /// <param name="al">对应列的值的数组</param>
    public int Update(string[] ColumnArray,T model,ArrayList al)
    {
        StringBuilder strSql = new StringBuilder();
        strSql.Append("update " + model.TableName + " set ");
        strSql.Append(Common.ArrayToSetStr(ColumnArray));
        strSql.Append(" where id=@id ");
        SqlParameter[] parameters = new SqlParameter[ColumnArray.Length + 1];//因为ColumnArray里面不带id，所以要加一
        for (int i = 0; i <ColumnArray.Length; i++)
        {
            SqlParameter sp = new SqlParameter(ColumnArray[i], al[i]);
           // HttpContext.Current.Response.Write(model.ColumnArray[i] + "," + sp.Value.ToString());
           // HttpContext.Current.Response.Write("," + i.ToString() + "<br/>");
            parameters[i] = sp;
        }
        parameters[ColumnArray.Length] = new SqlParameter("id", model._id);//把id也add进去
        int flag = SqlHelper.ExecuteNonQuery(SQLString, CommandType.Text, strSql.ToString(), parameters);
        return flag;
    }
    #endregion

    #region 删除一条数据
    /// <summary>
    /// 删除一条数据
    /// </summary>
    ///<param name="model">要删除的id序列，用逗号分隔的id字符串</param>
    /// <returns>受影响行数</returns>
    public int Delete(string ids)
    {
        T obj = new T();
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from  ");
        strSql.Append(obj.TableName);
        strSql.Append(" where id in (" + ids + ")");
        SqlParameter[] parameters = { };
        int flag = SqlHelper.ExecuteNonQuery(SQLString, CommandType.Text, strSql.ToString(), parameters);
        return flag;
    }
    #endregion

    #region 得到一个对象实体
    /// <summary>
    /// 得到一个对象实体
    /// </summary>
    /// <param name="id">根据id获取实体对象</param>
    /// <param name="idName">id列的名字</param>
    /// <returns>泛型实体对象</returns>
    public T GetModel(int id,string idName)
    {
        T obj = new T();
        StringBuilder strSql = new StringBuilder(); 
        strSql.Append("select  top 1 * from  ");
        strSql.Append(obj.TableName);
        strSql.Append(" where "+idName+"=@id ");
        SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
        parameters[0].Value = id;
        SqlDataReader dr = SqlHelper.ExecuteReader(SQLString, CommandType.Text, strSql.ToString(), parameters);
        if (dr!=null&dr.Read())
        {
            obj._id = dr.GetInt32(0);
            ArrayList al = new ArrayList();
            for (int i = 0; i < obj.ColumnArray.Length; i++)
            {
                if (dr[i + 1] == DBNull.Value) al.Add(null);
                else al.Add(dr[i + 1]);
            }
            obj.ValueArray = al;
            //obj.SetAll();
            return obj;
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region 根据用户名和密码获取用户实体对象
    /// <summary>
    /// 得到一个用户对象实体
    /// </summary>
    /// <param name="columnUserValue">根据用户名获取实体对象</param>
    /// <returns>用户泛型实体对象</returns>
    public T GetUser(string columnUserName, string columnUserValue, string columnPwName, string columnPwValue)
    {
        if (Regex.IsMatch(columnUserValue, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']")) return null;
        if (Regex.IsMatch(columnPwValue, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|\*|!|\']")) return null;
        T obj = new T();
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from  ");
        strSql.Append(obj.TableName);
        strSql.Append(" where " + columnUserName + "=@userName and " + columnPwName + "=@userPW");
        SqlParameter[] parameters = {
					new SqlParameter("@userName", columnUserValue),new SqlParameter("@userPW",columnPwValue)};
        SqlDataReader dr = SqlHelper.ExecuteReader(SQLString, CommandType.Text, strSql.ToString(), parameters);
        if (dr != null & dr.Read())
        {
            obj._id = dr.GetInt32(0);
            ArrayList al = new ArrayList();
            for (int i = 0; i < obj.ColumnArray.Length; i++)
            {
                if (dr[i + 1] == DBNull.Value) al.Add(null);
                else al.Add(dr[i + 1]);
            }
            obj.ValueArray = al;
            //obj.SetAll();
            return obj;
        }
        else return null;
    }
    #endregion

    #region 根据用户名获取用户实体对象
    /// <summary>
    /// 得到一个用户对象实体
    /// </summary>
    /// <param name="columnUserValue">根据用户名获取实体对象</param>
    /// <returns>用户泛型实体对象</returns>
    public T GetUser(string columnUserName, string columnUserValue)
    {
        if (Regex.IsMatch(columnUserValue, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']")) return null;
        T obj = new T();
        StringBuilder strSql = new StringBuilder();
        strSql.Append("select * from  ");
        strSql.Append(obj.TableName);
        strSql.Append(" where " + columnUserName + "=@userName" );
        SqlParameter[] parameters = {
					new SqlParameter("@userName", columnUserValue)};
        SqlDataReader dr = SqlHelper.ExecuteReader(SQLString, CommandType.Text, strSql.ToString(), parameters);
        if (dr != null & dr.Read())
        {
            obj._id = dr.GetInt32(0);
            ArrayList al = new ArrayList();
            for (int i = 0; i < obj.ColumnArray.Length; i++)
            {
                if (dr[i + 1] == DBNull.Value) al.Add(null);
                else al.Add(dr[i + 1]);
            }
            obj.ValueArray = al;
            //obj.SetAll();
            return obj;
        }
        else return null;
    }
    #endregion

    #region 获取分页DataSet
    /// <summary>
    /// 根据索引，页长，筛选语句，排序语句获取分页DataSet数据
    /// </summary>
    /// <param name="CurrentPageIndex">索引（第几页）</param>
    /// <param name="pagesize">页长（每页显示多少）</param>
    /// <param name="wherestr">筛选语句（where后面的sql）</param>
    /// <param name="orderbystr">排序语句（orderby后面的sql）</param>
    /// <param name="count">总共的页数（引用传送）</param>
    /// <returns>DataSet（Table[1]才是真正的数据）</returns>
    public DataSet GetDataSet(int CurrentPageIndex, int pagesize, string wherestr, string orderbystr, out int count)
    {

        T obj = new T();
        string sql;
        DataSet ds = null;
        sql = "SELECT TOP 100 PERCENT * FROM " + obj.TableName;
        if (wherestr.Trim() != "") sql += " WHERE " + wherestr;
        if (orderbystr.Trim() != "") sql += "  ORDER BY " + orderbystr;
        //dr = SqlHelper.ExecuteReader(SqlHelper.ConnString,CommandType.Text,sql,null);
        ds = SqlHelper.DataSplit(sql, CurrentPageIndex, pagesize);
        if (ds != null)
            count = Convert.ToInt32(ds.Tables[2].Rows[0][0]);
        else count = 0;
        return ds;
    }
    #endregion

    #region 获取分页DataSet
    /// <summary>
    /// 根据索引，页长，筛选语句，排序语句获取分页DataSet数据
    /// </summary>
    /// <param name="CurrentPageIndex">索引（第几页）</param>
    /// <param name="pagesize">页长（每页显示多少）</param>
    /// <param name="selectStr">select top 100 precent 后面的sql</param>
    /// <param name="wherestr">筛选语句（where后面的sql）</param>
    /// <param name="orderbystr">排序语句（orderby后面的sql）</param>
    /// <param name="count">总共的页数（引用传送）</param>
    /// <returns>DataSet（Table[1]才是真正的数据）</returns>
    public DataSet GetDataSetBySelectStr(int CurrentPageIndex, int pagesize,string selectStr,string wherestr, string orderbystr, out int count)
    {

        T obj = new T();
        string sql;
        DataSet ds = null;
        sql = "SELECT TOP 100 PERCENT ";
       if(selectStr.Trim()!="") sql+=selectStr+" FROM " + obj.TableName;
       else sql += " FROM " + obj.TableName;
        if (wherestr.Trim() != "") sql += " WHERE " + wherestr;
        if (orderbystr.Trim() != "") sql += "  ORDER BY " + orderbystr;
        //dr = SqlHelper.ExecuteReader(SqlHelper.ConnString,CommandType.Text,sql,null);
        ds = SqlHelper.DataSplit(sql, CurrentPageIndex, pagesize);
        if (ds != null)
            count = Convert.ToInt32(ds.Tables[2].Rows[0][0]);
        else count = 0;
        return ds;
    }
    #endregion

    #region 获取分页List<T>,out count
    /// <summary>
    /// 根据索引，页长，筛选语句，排序语句获取分页泛型List数据
    /// </summary>
    /// <param name="CurrentPageIndex">索引（第几页）</param>
    /// <param name="pagesize">页长（每页显示多少）</param>
    /// <param name="wherestr">筛选语句（where后面的sql）</param>
    /// <param name="orderbystr">排序语句（orderby后面的sql）</param>
    /// <param name="count">总共的页数（引用传送）</param>
    /// <returns>泛型集合</returns>
    public List<T> GetList(int CurrentPageIndex, int pagesize, string wherestr, string orderbystr, out int count)
    {

        T obj = new T();
        string sql;
        SqlDataReader dr = null;
        sql = "SELECT TOP 100 PERCENT * FROM " + obj.TableName;
        if (wherestr.Trim() != "") sql += " WHERE " + wherestr;
        if (orderbystr.Trim() != "") sql += "  ORDER BY " + orderbystr;

        // dr = SqlHelper.ExecuteReader(SQLString ,CommandType.Text,sql,null);
       dr = SqlHelper.DataReaderSplit(sql, CurrentPageIndex, pagesize);
        List<T> objList = new List<T>();
        if (dr != null)
        {
            dr.NextResult();
            while (dr.Read())
            {
                obj = new T();
                obj._id = dr.GetInt32(0);
                ArrayList al = new ArrayList();
                for (int i = 0; i < obj.ColumnArray.Length; i++)
                {
                    if (dr[i + 1] == DBNull.Value) al.Add(null);
                    else al.Add(dr[i + 1]);
                }
                obj.ValueArray = al;
                objList.Add(obj);
            }
            dr.NextResult();
            dr.Read();
            count = dr.GetInt32(0);
            return objList;
        }
        else
        {
            count = 0;
            return null;
        }
     }
    #endregion

    #region 获取分页List<T>
    /// <summary>
    /// 根据索引，页长，筛选语句，排序语句获取分页泛型List数据
    /// </summary>
    /// <param name="CurrentPageIndex">索引（第几页）</param>
    /// <param name="pagesize">页长（每页显示多少）</param>
    /// <param name="wherestr">筛选语句（where后面的sql）</param>
    /// <param name="orderbystr">排序语句（orderby后面的sql）</param>
    /// <returns>泛型集合</returns>
    public List<T> GetList(int CurrentPageIndex, int pagesize, string wherestr, string orderbystr)
    {

        T obj = new T();
        string sql;
        SqlDataReader dr = null;
        sql = "SELECT TOP 100 PERCENT * FROM " + obj.TableName;
        if (wherestr.Trim() != "") sql += " WHERE " + wherestr;
        if (orderbystr.Trim() != "") sql += "  ORDER BY " + orderbystr;
        dr = SqlHelper.DataReaderSplit(sql, CurrentPageIndex, pagesize);
        List<T> objList = new List<T>();
        if (dr != null)
        {
            dr.NextResult();
            while (dr.Read())
            {
                obj = new T();
                obj._id = dr.GetInt32(0);
                ArrayList al = new ArrayList();
                for (int i = 0; i < obj.ColumnArray.Length; i++)
                {
                    if (dr[i + 1] == DBNull.Value) al.Add(null);
                    else al.Add(dr[i + 1]);
                }
                obj.ValueArray = al;
                objList.Add(obj);
            }
            dr.NextResult();
            dr.Read();
            return objList;
        }
        else return null;
    }
    #endregion

    #region 获取分页List<T>(简单型重载)
    /// <summary>
    /// 获取分页泛型List(简单型重载)
    /// </summary>
    /// <param name="CurrentPageIndex">索引（第几页）</param>
    /// <param name="pagesize">页长（每页显示多少）</param>
    /// <returns></returns>
    public List<T> GetList(int CurrentPageIndex, int pagesize)
    {
        int i;
        return GetList(CurrentPageIndex, pagesize, "", "", out i);
    }
    #endregion

    #region 获取分页List<T>(简单筛选型重载)
    /// <summary>
    /// 获取分页泛型List(简单筛选型重载)
    /// </summary>
    /// <param name="CurrentPageIndex">索引（第几页）</param>
    /// <param name="pagesize">页长（每页显示多少）</param>
    /// <param name="columnName">列名</param>
    /// <param name="columnValue">列值</param>
    /// <returns></returns>
    public List<T> GetListByCondition(int CurrentPageIndex, int pagesize, string columnName, object columnValue)
    {
        int i;
        return GetList(CurrentPageIndex, pagesize, columnName + "='" + columnValue.ToString() + "'", "", out i);
    }
    #endregion

    #region 搜索（全字段搜索）
    /// <summary>
    /// 搜索（全字段搜索）
    /// </summary>
    /// <param name="CurrentPageIndex">索引（第几页）</param>
    /// <param name="pagesize">页长（每页显示多少）</param>
    /// <param name="keyword">关键字</param>
    /// <param name="count">搜索结果</param>
    /// <returns></returns>
    public List<T> Search(int CurrentPageIndex, int pagesize, string keyword, out int count)
    {
        string matchStr = "";
        T obj = new T();
        for (int i = 0; i < obj.ColumnArray.Length; i++)
        {
            if (i == 0) matchStr += (obj.ColumnArray[i] + " LIKE '%" + keyword + "%'");
            else matchStr += (" OR " + obj.ColumnArray[i] + " LIKE '%" + keyword + "%'");
        }
        return GetList(CurrentPageIndex, pagesize, matchStr, "", out count);
    }
    #endregion

    #region 批量更新
    /// <summary>
    /// 批量更新
    /// </summary>
    /// <param name="columnName1">列名</param>
    /// <param name="columnvalue">列值</param>
    /// <param name="ids">以逗号分隔的id字符串</param>
    /// <returns>受影响行数</returns>
    public int Set(string columnName1, object columnvalue1,string columnName2,object columnvalue2, string ids)
    {
        T obj = new T();
        int flag = 0;
        string sql;
        try
        {
            sql = "UPDATE " + obj.TableName + " SET " + columnName1 + "=@columnValue1," + columnName2 + "=@columnValue2" + " WHERE id IN (" + ids + ")";
            SqlParameter[] param ={ 
                new SqlParameter("@columnValue1",columnvalue1),new SqlParameter("@columnValue2",columnvalue2),
                };
            flag = SqlHelper.ExecuteNonQuery(SQLString, CommandType.Text, sql, param);
        }
        catch (Exception)
        {
            flag = 0;
            throw;
        }
        return flag;
    }
    #endregion
}
