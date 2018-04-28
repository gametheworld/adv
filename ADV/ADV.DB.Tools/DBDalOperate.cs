using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.DB
{
    /// <summary>
    /// 数据操作类
    /// </summary>
    public class DBDalOperate : DBDalBase
    {
        #region 初始化
        /// <summary> 
        /// 数据操作类构造函数
        /// </summary>
        public DBDalOperate()
        {
        }
        /// <summary>
        /// 重载构造函数
        /// </summary>
        public DBDalOperate(string p_strConnetstring)
            : base(p_strConnetstring)
        {

        }
        //手动指定链接
        public DBDalOperate(bool isSet, string p_strConnetstring)
        {
            DBDalBase.myConnectionString = p_strConnetstring;
        }

        #endregion

        #region 扩展方法

        public T GetModel<T>(StringBuilder sql, ParCollection prams = null)
        {
            return ExecSqlQuery(sql, prams).GetModel<T>();
        }
        /// <summary>
        /// 存储过程获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Proc_Name"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public T GetProcModel<T>(string Proc_Name, ParCollection prams = null)
        {
            return ExecProcQuery(Proc_Name, prams).GetModel<T>();
        }
        public List<T> GetList<T>(StringBuilder sql, ParCollection prams = null)
        {
            return ExecSqlQuery(sql, prams).GetList<T>();
        }

        public List<T> GetProcList<T>(string Proc_Name, ParCollection prams = null)
        {
            return ExecProcQuery(Proc_Name, prams).GetList<T>();
        }

        public List<T> GetProcList<T>(string Proc_Name, out int OutPutValue, ParCollection prams = null)
        {
            return ExecProcQuery(Proc_Name, prams, out OutPutValue).GetList<T>();
        }

        public List<T> GetProcList<T>(string Proc_Name, ParCollection prams, string outPutParamName, out string OutPutValue)
        {
            return ExecProcQuery(Proc_Name, prams, outPutParamName, out OutPutValue).GetList<T>();
        }

        #endregion

        #region 检查是否存在
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public bool Exists(StringBuilder sql, ParCollection prams)
        {

            object objResult = "";
            try
            {
                base.ConnectionOpen();
                base.myCommand = new SqlCommand(sql.ToString(), base.myConnection);
                if (prams != null)
                {
                    foreach (SqlParameter paremeter in prams)
                    {
                        if (paremeter != null)
                        {
                            base.myCommand.Parameters.Add(paremeter);
                        }
                    }
                }
                objResult = base.myCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.CommandClose();
                base.ConnectionClose();
            }

            int cmdresult;
            if ((Object.Equals(objResult, null)) || (Object.Equals(objResult, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(objResult.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 获取单条记录
        public long GetSingleForID(StringBuilder sql, ParCollection prams)
        {
            return GetSingleForID(sql, prams);
        }

        public int GetSingleForCount(StringBuilder sql, ParCollection prams)
        {
            var obj = GetSingle(sql, prams);
            if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
            {
                return int.Parse(obj.ToString());
            }
            else { return 0; }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public object GetSingle(StringBuilder sql, ParCollection prams)
        {

            object objResult = new object();
            try
            {
                base.ConnectionOpen();
                base.myCommand = new SqlCommand(sql.ToString(), base.myConnection);
                if (prams != null)
                {
                    foreach (SqlParameter paremeter in prams)
                    {
                        if (paremeter != null)
                        {
                            base.myCommand.Parameters.Add(paremeter);
                        }
                    }
                }
                objResult = base.myCommand.ExecuteScalar();

            }
            catch (Exception ex)
            {
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.CommandClose();
                base.ConnectionClose();
            }
            if ((Object.Equals(objResult, null)) || (Object.Equals(objResult, System.DBNull.Value)))
            {
                return null;
            }
            else
            {
                return objResult;
            }
        }
        #endregion

        #region 存储过程
        /// <summary>
        /// 执行查询的存储过程[带输入参数]
        /// </summary>
        /// <param name="storeProcName"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public void ExecProcQueryNoReutrn(out string msg, string storeProcName, ParCollection prams = null)
        {
            //@@@--2014/11/27-yekang--DEL--测试
            //#region 记录调用存储过程日志
            //try
            //{
            //    LogMethod lM1 = new LogMethod();
            //    StringBuilder strContent = new StringBuilder();
            //    strContent.Append("-------------------------------------------------");
            //    strContent.AppendFormat("\r\n时间:{0} ", DateTime.Now.ToString());
            //    strContent.Append("\r\n");
            //    strContent.AppendFormat("PROCEDURE_NAME:{0} ", storeProcName);
            //    foreach (var item in prams)
            //    {
            //        strContent.Append("\r\n");
            //        strContent.AppendFormat("\tParameterName:{0}\t\t\tSqlVelue:{1}", item.ParameterName, item.SqlValue);
            //    }
            //    strContent.Append("\r\n");
            //    lM1.WriteLog(strContent);
            //}
            //catch (Exception ex)
            //{
            //    using (LogMethod lM = new LogMethod())
            //    {
            //        lM.WriteLog(ex.Message, ex);
            //    }
            //    throw new Exception(ex.Message, ex);
            //}
            //#endregion
            try
            {
                base.myAdapter = new SqlDataAdapter(storeProcName, DBDalBase.myConnectionString);
                base.myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (prams != null)
                {
                    foreach (SqlParameter parameter in prams)
                    {
                        if (parameter != null)
                        {
                            base.myAdapter.SelectCommand.Parameters.Add(parameter);
                        }

                    }
                }
                base.myAdapter.SelectCommand.Parameters.Add(new SqlParameter("@msg", SqlDbType.NVarChar, 5000, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default, DBNull.Value));
                base.myDataSet = new DataSet();
                base.myAdapter.Fill(base.myDataSet);
                msg = (string)base.myAdapter.SelectCommand.Parameters["@msg"].Value;
            }
            catch (Exception ex)
            {
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.DataAdapterClose();
            }
        }


        /// <summary>
        /// 执行查询的存储过程[带输入参数]
        /// </summary>
        /// <param name="storeProcName"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public void ExecProcQueryNoReutrn(string storeProcName, ParCollection prams, string paramName, out string msg)
        {
            try
            {
                base.myAdapter = new SqlDataAdapter(storeProcName, DBDalBase.myConnectionString);
                base.myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (prams != null || prams.Count > 0)
                {
                    foreach (SqlParameter parameter in prams)
                    {
                        if (parameter != null)
                        {
                            base.myAdapter.SelectCommand.Parameters.Add(parameter);
                        }

                    }
                }
                base.myAdapter.SelectCommand.Parameters.Add(new SqlParameter(paramName, SqlDbType.NVarChar, 5000, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default, DBNull.Value));
                base.myDataSet = new DataSet();
                base.myAdapter.Fill(base.myDataSet);
                msg = (string)base.myAdapter.SelectCommand.Parameters[paramName].Value;
            }
            catch (Exception ex)
            {
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.DataAdapterClose();
            }
        }

        /// <summary>
        /// 执行查询的存储过程[带输入参数]
        /// </summary>
        /// <param name="storeProcName"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public DataSet ExecProcQuery(string storeProcName, ParCollection prams = null)
        {
            try
            {
                base.myAdapter = new SqlDataAdapter(storeProcName, DBDalBase.myConnectionString);
                base.myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (prams != null)
                {
                    foreach (SqlParameter parameter in prams)
                    {
                        if (parameter != null)
                        {
                            base.myAdapter.SelectCommand.Parameters.Add(parameter);
                        }

                    }
                }
                base.myDataSet = new DataSet();
                base.myAdapter.Fill(base.myDataSet);
            }
            catch (Exception ex)
            {
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.DataAdapterClose();
            }
            return base.myDataSet;
        }

        /// <summary>
        /// 执行查询的存储过程[带输入参数],[带int型输出参数]
        /// </summary>
        /// <param name="storeProcName"></param>
        /// <param name="prams"></param>
        /// <param name="OutPutValue"></param>
        /// <returns></returns>
        public DataSet ExecProcQuery(string storeProcName, ParCollection prams, out int OutPutValue)
        {
            string strOutPutValue = string.Empty;
            DataSet myData = ExecProcQuery(storeProcName, prams, "@outputvalue", out  strOutPutValue);
            OutPutValue = Convert.ToInt32(strOutPutValue);
            return myData;
        }

        /// <summary>
        /// 执行查询的存储过程[带输入参数],[输出参数自定义]
        /// </summary>
        /// <param name="storeProcName">存储过程名称</param>
        /// <param name="prams">参数集合</param>
        /// <param name="outPubParamName">输出参数名称，必须带@</param>
        /// <param name="OutPutValue">输出参数</param>
        /// <returns></returns>
        public DataSet ExecProcQuery(string storeProcName, ParCollection prams, string outPubParamName, out string OutPutValue)
        {
            try
            {
                base.myAdapter = new SqlDataAdapter(storeProcName, DBDalBase.myConnectionString);
                base.myAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (prams != null)
                {
                    foreach (SqlParameter parameter in prams)
                    {
                        if (parameter != null)
                        {
                            base.myAdapter.SelectCommand.Parameters.Add(parameter);
                        }
                    }
                }
                base.myAdapter.SelectCommand.Parameters.Add(new SqlParameter(outPubParamName, SqlDbType.NVarChar, 500, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default, DBNull.Value));
                base.myDataSet = new DataSet();
                base.myAdapter.Fill(base.myDataSet);
                object objParamVal = base.myAdapter.SelectCommand.Parameters[outPubParamName].Value;
                OutPutValue = string.Empty;
                if (objParamVal != DBNull.Value)
                {
                    OutPutValue = objParamVal.ToString();
                }

            }
            catch (Exception ex)
            {
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.DataAdapterClose();
            }
            return base.myDataSet;
        }


        #endregion

        #region 执行语句
        /// <summary>
        /// 执行数据更新的sql语句[带输入参数]
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public int ExecSqlNoQuery(StringBuilder sql, ParCollection prams = null)
        {
            int intResult = 0;
            try
            {
                base.ConnectionOpen();
                base.myCommand = new SqlCommand(sql.ToString(), base.myConnection);
                if (prams != null)
                {
                    foreach (SqlParameter paremeter in prams)
                    {
                        if (paremeter != null)
                        {
                            base.myCommand.Parameters.Add(paremeter);
                        }
                    }
                }
                intResult = base.myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                intResult = -1000;
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.CommandClose();
                base.ConnectionClose();
            }
            return intResult;
        }
        #endregion

        #region  查询语句
        /// <summary>
        /// 执行查询的sql语句[带输入参数]
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public DataSet ExecSqlQuery(StringBuilder sql, ParCollection prams = null)
        {
            try
            {
                base.myAdapter = new SqlDataAdapter(sql.ToString(), DBDalBase.myConnectionString);
                if (prams != null)
                {
                    foreach (SqlParameter parameter in prams)
                    {
                        if (parameter != null)
                        {
                            base.myAdapter.SelectCommand.Parameters.Add(parameter);
                        }
                    }
                }
                base.myDataSet = new DataSet();
                base.myAdapter.Fill(base.myDataSet);
            }
            catch (Exception ex)
            {
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.DataAdapterClose();
            }
            if (base.myDataSet != null && base.myDataSet.Tables[0].Rows.Count == 0)
            {
                base.myDataSet.Dispose();
                return null;
            }
            return base.myDataSet;
        }
        #endregion

        #region 执行事物
        /// <summary>
        ///  执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        /// <returns>事务是否成功</returns>
        public bool ExecSqlTran(TranCollection ltTran)
        {
            bool flag = true;
            base.ConnectionOpen();
            base.myCommand = new SqlCommand();
            myCommand.Connection = base.myConnection;
            string CurrentSql = "";
            var curList = ltTran.OrderBy(ss => ss.Order).ToList();
            using (SqlTransaction trans = base.myConnection.BeginTransaction())
            {
                try
                {
                    //循环
                    foreach (var tran in curList)
                    {
                        CurrentSql = tran.Sql.ToString();
                        SqlParameter[] cmdParms = tran.Pars != null ? tran.Pars.ToArray() : null;
                        PrepareCommand(base.myCommand, base.myConnection, trans, CurrentSql, cmdParms);
                        int val = myCommand.ExecuteNonQuery();
                        myCommand.Parameters.Clear();
                    }
                    trans.Commit();
                    return flag;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    using (LogMethod lM = new LogMethod())
                    {
                        lM.WriteLog(ex.Message + "SQL:" + CurrentSql, ex);
                    }
                    throw ex;
                }
                finally
                {
                    base.CommandClose();
                    base.ConnectionClose();
                }
            }
        }
        #endregion

        #region 分页查询

        const string SQL_PAGE = "Select * From (Select  Row_Number() over(order by {0}) as rowNum ,{1}) as T where T.rowNum between @Begin And @End";

        public DataSet Paging(StringBuilder strSql, string strOrderby, int iPageIndex, int iPageSize, ParCollection ltEx = null)
        {
            int iCount = 0;
            return Paging(strSql.ToString(), strOrderby, iPageIndex, iPageSize, out iCount, ltEx, false);
        }
        public DataSet Paging(StringBuilder strSql, string strOrderby, int iPageIndex, int iPageSize, ParCollection ltEx, out int iCount)
        {
            return Paging(strSql.ToString(), strOrderby, iPageIndex, iPageSize, out iCount, ltEx, true);
        }
        public DataSet Paging(string strSql, string strOrderby, int iPageIndex, int iPageSize, out int iCount, ParCollection ltEx = null, bool isGetCount = true)
        {
            try
            {
                iCount = 0;
                int iBegin = (iPageIndex - 1) * iPageSize + 1;
                int iEnd = iPageIndex * iPageSize;

                //去除select
                var hasSelect = strSql.IndexOf("select", 0, 20, StringComparison.CurrentCultureIgnoreCase);
                if (hasSelect != -1)
                {
                    strSql = strSql.Remove(hasSelect, 6);
                }
                StringBuilder strCount = new StringBuilder();
                if (isGetCount)
                {
                    //统计总数
                    strCount.AppendFormat(" Select @OutputCount=Count(*) {0} ;", strSql.Substring(strSql.IndexOf(" from", StringComparison.CurrentCultureIgnoreCase)));
                }
                StringBuilder strSelect = new StringBuilder();
                strSelect.AppendFormat(SQL_PAGE, strOrderby, strSql);
                List<SqlParameter> ltpar = new List<SqlParameter>();
                ltpar.Add(new SqlParameter("@Begin", SqlDbType.Int) { Value = iBegin });
                ltpar.Add(new SqlParameter("@End", SqlDbType.Int) { Value = iEnd });
                if (ltEx != null) { ltpar.AddRange(ltEx); }
                if (isGetCount)
                {
                    ltpar.Add(new SqlParameter("@OutputCount", SqlDbType.Int) { Direction = ParameterDirection.Output });
                }

                base.myAdapter = new SqlDataAdapter(strSelect.Append(strCount).ToString(), DBDalBase.myConnectionString);
                base.myAdapter.SelectCommand.CommandType = CommandType.Text;
                base.myAdapter.SelectCommand.Parameters.AddRange(ltpar.ToArray());
                base.myDataSet = new DataSet();
                base.myAdapter.Fill(base.myDataSet);
                iCount = isGetCount ? (int)base.myAdapter.SelectCommand.Parameters["@OutputCount"].Value : 0;

            }
            catch (Exception ex)
            {
                using (LogMethod lM = new LogMethod())
                {
                    lM.WriteLog(ex.Message, ex);
                }
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                base.DataAdapterClose();
            }
            return base.myDataSet;
        }
        #endregion

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

    }
}
