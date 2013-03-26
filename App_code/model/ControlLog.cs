 using System;                                                
using System.IO;                                                
using System.Data;                                                
using System.Configuration;                                                
using System.Web;                                                
                                              
/// <summary>                                                
/// ContrilLog 的摘要说明 
///  实例化对象，ContrilLog为程序名；                                                
    ///  private ContrilLog lg = new ContrilLog("[类名]");                                                
   ///  得到程序所在的路径                                                
   ///  ContrilLog.serPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase; ;                                                
   ///  写日志                                                
   ///  info.WriteDebugLog(methodName,ex.ToString());    
/// </summary>                                                
public class ControlLog                                                
{                                        
    public static string serPath;   //日志文件路径,事先指定                                                
    private string className;       //出错的类名                                                
                                                
    public ControlLog(string className)                                                
    {                                            
        this.className = className;                                                
                                                
        //获取网站在服务器的物理位置                                                
        serPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;                                   
    }                                                
                                                
    #region "写日志"                                                
    /// <summary>                                                
    /// 写日志                                                
    /// </summary>                                                
    /// <param name="methodName">方法名称</param>                                                
    /// <param name="errorInfo">错误信息</param>                                                
    public void WriteDebugLog(string methodName,string errorInfo,string userName)                                                
    {                                                
        try                                                
        {                                              
            string fname = serPath + "\\ControlLog.txt"; ///指定日志文件的目录                                       
            FileInfo finfo = new FileInfo(fname);///定义文件信息对象                                                         
            if (finfo.Exists && finfo.Length > 100 * 1024 * 1024) ///判断文件是否存在以及是否大于512K                                                
            {                                              
                finfo.Delete();                                                
            }                                                    
            using (FileStream fs = finfo.OpenWrite()) ///创建只写文件流                                                  
            {                                                                       
                StreamWriter w = new StreamWriter(fs);///根据上面创建的文件流创建写数据流                                              
                w.BaseStream.Seek(0, SeekOrigin.End);///设置写数据流的起始位置为文件流的末尾                              
                w.Write("<------------------------------------------------------------------------------------->\r\n");                     
                ///写入“Debug Info: ”                                                
                w.Write("Debug时间　　：");                     
                ///写入当前系统时间                                                
                w.Write("{0} {1}\r\n", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());                            
                //写入出错的类名称                                                
                w.Write("Debug类名　　：" + className +" 操作人:"+userName+ "\r\n");                   
                //写入出错的方法名称                                                
                w.Write("Debug方法名　：" + methodName + "\r\n");                        
                //写入错误信息                                                
                w.Write("Debug错误信息：" + errorInfo + "\r\n");                  
                w.Write("<------------------------------------------------------------------------------------->\r\n");                          
                ///清空缓冲区内容，并把缓冲区内容写入基础流                                                
                w.Flush();                                 
                ///关闭写数据流                                                
                w.Close();                                                
            }                                                
        }                                                
        catch (Exception ex)                                                
        {                                                
            string strEx = ex.ToString();                                                
        }                                                
    }                                                
    #endregion                                                
}                