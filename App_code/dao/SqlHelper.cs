using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Data.SqlClient;

    /// <summary>
    /// ���ݿ��ͨ�÷��ʴ���
    /// ����Ϊ�����࣬������ʵ��������Ӧ��ʱֱ�ӵ��ü���
    /// </summary>
    public abstract class SqlHelper
    {
        //��ȡ���ݿ������ַ����������ھ�̬������ֻ������Ŀ�������ĵ�����ֱ��ʹ�ã��������޸�
        //���13
        public static readonly string ConnString =
        ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        // ��ϣ�������洢����Ĳ�����Ϣ����ϣ����Դ洢�������͵Ĳ���
        //���8
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());//���9

        /// <summary>
        ///ִ��һ������Ҫ����ֵ��SqlCommand���ͨ��ָ��ר�õ������ַ�����
        /// ʹ�ò���������ʽ�ṩ�����б� 
        /// </summary>
        /// <remarks>
        /// ʹ��ʾ����
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure,
        ///  "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="commandType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�) 
        /// </param>
        /// <param name="commandText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�
        /// </param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType,
        string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //ͨ��PrePareCommand����������������뵽SqlCommand�Ĳ���������
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //���SqlCommand�еĲ����б�
                cmd.Parameters.Clear();
                return val;
            }
        }
        /// <summary>
        ///ִ��һ�������ؽ����SqlCommand��ͨ��һ���Ѿ����ڵ����ݿ����� 
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <remarks>
        /// ʹ��ʾ����  
        /// int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, 
        /// "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">һ�����е����ݿ�����</param>
        /// <param name="commandType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�) 
        /// </param>
        /// <param name="commandText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�
        /// </param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>

        //���5
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ִ��һ�������ؽ����SqlCommand��ͨ��һ���Ѿ����ڵ����ݿ����ﴦ�� 
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <remarks>
        /// ʹ��ʾ���� 
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, 
        /// "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">һ�����ڵ� sql ���ﴦ��</param>
        /// <param name="commandType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�) 
        /// </param>
        /// <param name="commandText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�
        /// </param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>

        //���12
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// ִ��һ�����ؽ������SqlCommand���ͨ��ר�õ������ַ�����
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <remarks>
        /// ʹ��ʾ����  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, 
        /// "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="commandType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�) 
        /// </param>
        /// <param name="commandText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�
        /// </param>
        /// <returns>����һ�����������SqlDataReader</returns>

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            // ������ʹ��try/catch��������Ϊ������������쳣����SqlDataReader�Ͳ����ڣ�
            //CommandBehavior.CloseConnection�����Ͳ���ִ�У��������쳣��catch����
            //�ر����ݿ����ӣ���ͨ��throw�ٴ�������׽�����쳣��  
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch 
            {
                conn.Close();
                return null;
                throw;  //���7
            }
        }
        
        /// <summary>
        /// ����DataSet���ݼ�
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns>����DataSet���ݼ�</returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        public static DataSet DataSplit(string sql, int CurrentPageIndex, int pagesize)
        {
            DataSet ds = null;
            SqlParameter[] para = {
                                new SqlParameter("@sql", SqlDbType.NVarChar,40000),
                                new SqlParameter("@currentpage", SqlDbType.Int),
                                new SqlParameter("@pagesize", SqlDbType.Int)};
            para[0].Value = sql;
            para[1].Value = CurrentPageIndex;
            para[2].Value = pagesize;

            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnString, CommandType.StoredProcedure, "storePage", para);
            }
            catch (Exception)
            {
                ds = null;
                throw;
            }
            return ds;
        }
        public static SqlDataReader DataReaderSplit(string sql, int CurrentPageIndex, int pagesize)
        {
            SqlDataReader dr = null;
            SqlParameter[] para = {
                                new SqlParameter("@sql", SqlDbType.NVarChar,40000),
                                new SqlParameter("@currentpage", SqlDbType.Int),
                                new SqlParameter("@pagesize", SqlDbType.Int)};
            para[0].Value = sql;
            para[1].Value = CurrentPageIndex;
            para[2].Value = pagesize;

            try
            {
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnString, CommandType.StoredProcedure, "storePage", para);
            }
            catch (Exception)
            {
                dr = null;
                throw;
            }
            return dr;
        }

        /// <summary>
        /// ִ��һ�����ص�һ����¼��һ�е�SqlCommand���ͨ��ר�õ������ַ����� 
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <remarks>
        /// ʹ��ʾ����  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, 
        /// "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="commandType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�) 
        /// </param>
        /// <param name="commandText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�
        /// </param>
        /// <returns>����һ��object���͵����ݣ�����ͨ�� Convert.To{Type}����ת������</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }

        }

        /// <summary>
        /// ִ��һ�����ص�һ����¼��һ�е�SqlCommand���ͨ���Ѿ����ڵ����ݿ����ӡ�
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <remarks>
        /// ʹ��ʾ���� 
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure,
        /// "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">һ���Ѿ����ڵ����ݿ�����</param>
        /// <param name="commandType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�) 
        /// </param>
        /// <param name="commandText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�
        /// </param>
        /// <returns>����һ��object���͵����ݣ�����ͨ�� Convert.To{Type}����ת������
        /// </returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="cacheKey">��������ļ�ֵ</param>
        /// <param name="cmdParms">������Ĳ����б�</param>

        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {

            //���10
            parmCache[cacheKey] = commandParameters;

        }

        /// <summary>
        /// ��ȡ������Ĳ���
        /// </summary>
        /// <param name="cacheKey">���ڲ��Ҳ�����KEYֵ</param>
        /// <returns>���ػ���Ĳ�������</returns>

        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {

            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            //�½�һ�������Ŀ�¡�б�
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            //ͨ��ѭ��Ϊ��¡�����б�ֵ
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                //ʹ��clone�������Ʋ����б��еĲ���
                //���11
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        }

        /// <summary>
        /// Ϊִ������׼������
        /// </summary>
        /// <param name="cmd">SqlCommand ����</param>
        /// <param name="conn">�Ѿ����ڵ����ݿ�����</param>
        /// <param name="trans">���ݿ����ﴦ��</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣�T-SQL��䣬�ȵȡ�) </param>
        /// <param name="cmdText">Command text��T-SQL��� ���� Select * from 
        /// Products</param>
        /// <param name="cmdParms">���ش�����������</param>

        //���6
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[]
             cmdParms)
        {

            //�ж����ݿ�����״̬
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //�ж��Ƿ���Ҫ���ﴦ��
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
               
                foreach (SqlParameter parm in cmdParms)
                {
                    if (parm != null)
                    {
                        SqlParameter pp = (SqlParameter)((ICloneable)parm).Clone();
                        cmd.Parameters.Add(pp);
                    }
                }
            }
        }


    }
