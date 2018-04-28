using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADV.DB;

/// <summary>
/// 数据库操作类
/// </summary>
public static class DBHelper
{
    #region 扩展方法
    /// <summary>
    /// 获取对象实体
    /// </summary>
    public static T GetModel<T>(this StringBuilder sql, ParCollection prams = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.GetModel<T>(sql, prams);
        }
    }

    /// <summary>
    /// 获取对象实体
    /// </summary>
    public static T GetModel<T>(this  AutoPar auto)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.GetModel<T>(auto.strSql, auto.pars);
        }
    }
    /// <summary>
    /// 存储过程获取实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Proc_Name"></param>
    /// <param name="prams"></param>
    /// <returns></returns>
    public static T GetProcModel<T>(string Proc_Name, ParCollection prams = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.GetProcModel<T>(Proc_Name, prams);
        }
    }
    /// <summary>
    /// 获取对象集合
    /// </summary>
    public static List<T> GetList<T>(this StringBuilder sql, ParCollection prams = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.GetList<T>(sql, prams);
        }
    }
    /// <summary>
    /// 获取对象集合
    /// </summary>
    public static List<T> GetList<T>(this  AutoPar auto)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.GetList<T>(auto.strSql, auto.pars);
        }
    }

    public static List<T> GetProcList<T>(string Proc_Name, ParCollection prams = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.GetProcList<T>(Proc_Name, prams);
        }
    }

    public static List<T> GetProcList<T>(string Proc_Name, out int OutPutValue, ParCollection prams = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.GetProcList<T>(Proc_Name, out OutPutValue, prams);
        }
        //ExecProcQuery(Proc_Name, prams, out OutPutValue).GetList<T>();
    }

    public static List<T> GetProcList<T>(string Proc_Name, ParCollection prams, string outPutParamName, out string OutPutValue)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.GetProcList<T>(Proc_Name, prams, outPutParamName, out OutPutValue);
        }
    }
    #endregion

    #region 检查是否存在
    /// <summary>
    /// 检查是否存在 (count)
    /// </summary>
    public static bool Exists(this StringBuilder sql, ParCollection prams)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.Exists(sql, prams);
        }
    }
    /// <summary>
    /// 检查是否存在 (count)
    /// </summary>
    public static bool Exists(this AutoPar auto)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.Exists(auto.strSql, auto.pars);
        }
    }
    #endregion

    #region 获取单条记录

    /// <summary>
    /// 获取ID (long:bigint)
    /// </summary>
    public static long GetSingleForID(this StringBuilder sql, ParCollection prams)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.GetSingleForID(sql, prams);
        }
    }

    /// <summary>
    /// 获取ID (long:bigint)
    /// </summary>
    public static long GetSingleForID(this AutoPar auto)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.GetSingleForID(auto.strSql, auto.pars);
        }
    }
    /// <summary>
    /// 获取Count (int:int)
    /// </summary>
    public static int GetSingleForCount(this StringBuilder sql, ParCollection prams)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.GetSingleForCount(sql, prams);
        }
    }
    /// <summary>
    /// 获取Count (int:int)
    /// </summary>
    public static int GetSingleForCount(this AutoPar auto)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.GetSingleForCount(auto.strSql, auto.pars);
        }
    }
    /// <summary>
    /// 获取第一个行第一列结果
    /// </summary>
    public static object GetSingle(this StringBuilder sql, ParCollection prams)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {

            return dal.GetSingle(sql, prams);
        }
    }
    /// <summary>
    /// 获取第一个行第一列结果
    /// </summary>
    public static object GetSingle(this AutoPar auto)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.GetSingle(auto.strSql, auto.pars);
        }
    }
    #endregion

    #region 存储过程
    /// <summary>
    /// 执行查询存储过程[带输入参数]
    /// </summary>
    /// <param name="procName"></param>
    /// <param name="parms"></param>
    /// <returns></returns>
    public static DataSet SelectProc(string procName, ParCollection parms = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.ExecProcQuery(procName, parms);
        }
    }
    #endregion

    #region 执行语句
    /// <summary>
    /// 执行数据更新的sql语句[带输入参数]
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parms"></param>
    /// <returns></returns>
    public static bool Sql(this StringBuilder sql, ParCollection prams = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.ExecSqlNoQuery(sql, prams) > 0 ? true : false;
        }
    }
    /// <summary>
    /// 执行数据更新的sql语句[带输入参数]
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parms"></param>
    /// <returns></returns>
    public static bool Sql(this AutoPar auto)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.ExecSqlNoQuery(auto.strSql, auto.pars) > 0 ? true : false;
        }
    }
    #endregion

    #region 查询语句
    /// <summary>
    /// 执行查询的sql语句[带输入参数]
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="prams"></param>
    /// <returns></returns>
    public static DataSet Select(this StringBuilder sql, ParCollection prams = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {

            return dal.ExecSqlQuery(sql, prams);
        }
    }
    /// <summary>
    /// 执行查询的sql语句[带输入参数]
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="prams"></param>
    /// <returns></returns>
    public static DataSet Select(this AutoPar auto)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.ExecSqlQuery(auto.strSql, auto.pars);
        }
    }
    /// <summary>
    /// 执行分页查询
    /// </summary>
    /// <param name="auto"></param>
    /// <param name="strOrderBy"></param>
    /// <param name="iPageIndex"></param>
    /// <param name="iPageSize"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public static DataSet SelectPaging(this AutoPar auto, string strOrderBy, int iPageIndex, int iPageSize, out int iCount)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.Paging(auto.strSql, strOrderBy, iPageIndex, iPageSize, auto.pars, out iCount);
        }
    }

    /// <summary>
    /// 执行分页查询（不反回总页数）
    /// </summary>
    /// <param name="auto"></param>
    /// <param name="strOrderBy"></param>
    /// <param name="iPageIndex"></param>
    /// <param name="iPageSize"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public static DataSet SelectPaging(this AutoPar auto, string strOrderBy, int iPageIndex, int iPageSize)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            auto.End();
            return dal.Paging(auto.strSql, strOrderBy, iPageIndex, iPageSize, auto.pars);
        }
    }

    #endregion

    #region 执行事物
    /// <summary>
    ///  执行多条SQL语句，实现数据库事务。
    /// </summary>
    /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
    /// <returns>事务是否成功</returns>
    public static bool Tran(this TranCollection ltTran)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.ExecSqlTran(ltTran);
        }

    }
    //public static bool Tran(this TranCollection ltTran, out int iCount)
    //{
    //    using (DBDalOperate dal = new DBDalOperate())
    //    {
    //        return dal.ExecSqlTran(ltTran, out iCount);
    //    }

    //}
    #endregion

    #region 分页查询
    /// <summary>
    /// 分页查询 
    /// </summary>
    public static DataSet Paging(this StringBuilder strSql, string strOrderby, int iPageIndex, int iPageSize, ParCollection ltEx = null)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            int iCount = 0;
            return dal.Paging(strSql.ToString(), strOrderby, iPageIndex, iPageSize, out iCount, ltEx, false);
        }
    }
    /// <summary>
    /// 分页查询 返回总数
    /// </summary>
    public static DataSet Paging(this StringBuilder strSql, string strOrderby, int iPageIndex, int iPageSize, ParCollection ltEx, out int iCount)
    {
        using (DBDalOperate dal = new DBDalOperate())
        {
            return dal.Paging(strSql.ToString(), strOrderby, iPageIndex, iPageSize, out iCount, ltEx, true);
        }
    }
    #endregion

}
