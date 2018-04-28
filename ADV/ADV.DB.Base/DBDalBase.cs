using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.DB
{
    /// <summary>
    /// DB操作类
    /// </summary>
    public class DBDalBase : DBBase
    {


        /// <summary>
        /// 连接字符串
        /// </summary>
        protected static string myConnectionString;
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        protected SqlConnection myConnection;
        /// <summary>
        /// 命令执行对象
        /// </summary>
        protected SqlCommand myCommand;
        /// <summary>
        /// 数据适配器
        /// </summary>
        protected SqlDataAdapter myAdapter;
        /// <summary>
        /// 数据读取器
        /// </summary>
        protected SqlDataReader myReader;
        /// <summary>
        /// 数据集对象
        /// </summary>
        protected DataSet myDataSet;
        /// <summary>
        /// sql参数对象
        /// </summary>
        protected SqlParameter[] myParameters;
        /// <summary>
        /// sql语句
        /// </summary>
        protected string mySql;

        private const string defaultConnectionString = "connectionString";

        /// <summary>
        /// DB操作类构造函数

        /// </summary>
        public DBDalBase()
        {


            if (ConfigurationManager.ConnectionStrings[defaultConnectionString] != null)
            {
                myConnectionString = ConfigurationManager.ConnectionStrings[defaultConnectionString].ConnectionString;

            }
        }

        public static string GetConnString()
        {


            if (ConfigurationManager.ConnectionStrings[defaultConnectionString] != null)
            {
                //myConnectionString = DEncrypt.Decrypt(ConfigurationManager.ConnectionStrings["XiangMuGu"].ConnectionString,
                //   DEncrypt.KeySql);
                myConnectionString = ConfigurationManager.ConnectionStrings[defaultConnectionString].ConnectionString;

            }
            return myConnectionString;
        }


        /// <summary>
        /// 重载构造函数

        /// </summary>
        /// <param name="p_strSqlConnect">webconfig配置的数据库连接字符串</param>
        public DBDalBase(string p_strSqlConnect)
        {
            if (p_strSqlConnect == null || p_strSqlConnect == "")
            {
                myConnectionString = ConfigurationManager.ConnectionStrings[defaultConnectionString].ConnectionString;
            }
            else
            {
                myConnectionString = ConfigurationManager.ConnectionStrings[p_strSqlConnect].ConnectionString;
            }
        }

        /// <summary>
        /// open DB connection
        /// </summary>
        protected void ConnectionOpen()
        {
            if (myConnection == null)
            {
                myConnection = new SqlConnection(myConnectionString);
            }
            if (myConnection.State.Equals(ConnectionState.Closed))
            {
                myConnection.Open();
            }
        }

        /// <summary>
        /// close DB connection
        /// </summary>
        protected void ConnectionClose()
        {
            if (myConnection != null)
            {
                if (myConnection.State.Equals(ConnectionState.Open))
                {
                    myConnection.Close();
                    myConnection.Dispose();
                }
                else
                {
                    myConnection.Dispose();
                }
            }
        }

        /// <summary>
        /// close DB command
        /// </summary>
        protected void CommandClose()
        {
            if (myCommand != null)
            {
                myCommand.Dispose();
            }
        }

        /// <summary>
        /// close DB adapter
        /// </summary>
        protected void DataAdapterClose()
        {
            if (myAdapter != null)
            {
                myAdapter.Dispose();
            }
        }

        /// <summary>
        /// close DB datareader
        /// </summary>
        protected void DataReaderClose()
        {
            if (myReader != null)
            {
                myReader.Close();
                myReader.Dispose();
            }
        }


        public DataTable GetInTable()
        {
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            return dt;
        }
    }
}
