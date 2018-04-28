using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#region 参数自动化
/// <summary>
/// 参数自动化
/// </summary>
public class AutoPar
{
    //public static AutoPar operator +(AutoPar c1, string c2)
    //{
    //    return c1.Add(c2);
    //}

    //public static explicit operator bool(AutoPars c1)
    //{
    //    c1.End();
    //    return false;
    //}

    public StringBuilder strSql { get; set; }
    public string sql { get; set; }
    public SqlParameter[] arrpar { get; set; }
    public List<PropertyInfo> proList { get; set; }

    public ParCollection pars { get; set; }
    public object oMdl { get; set; }

    public AutoPar(List<PropertyInfo> plist = null, object o = null, string str = "")
    {
        strSql = new StringBuilder();
        strSql.Append(str);
        if (plist == null) { plist = new List<PropertyInfo>(); }
        proList = plist;
        oMdl = o;
        pars = new ParCollection();
    }
    /// <summary>
    /// 通过实体绑定 如 UserMdl
    /// </summary>
    public static AutoPar Mdl(object o)
    {
        return new AutoPar(o.GetType()._GetProperties(), o);
    }
    /// <summary>
    /// 通过匿名类型帮顶 如 {id=11,name="bb"}
    /// </summary>
    public static AutoPar Var(object o)
    {
        return new AutoPar(o.GetType().GetProperties().ToList(), o);
    }
    public static AutoPar AddNew(string str)
    {
        return new AutoPar(str: str);
    }
    /// <summary>
    /// 添加语句
    /// </summary>
    public AutoPar Add(string str)
    {
        this.strSql.Append(str);
        return this;
    }

    public AutoPar AddFormat(string str, params object[] args)
    {
        this.strSql.AppendFormat(str, args);
        return this;
    }
    public AutoPar AddPar(string name, object value)
    {
        this.pars.Add(name, value);
        return this;
    }
    public AutoPar AddPars(ParCollection pars)
    {
        this.pars.AddRange(pars);
        return this;
    }
    /// <summary>
    /// 语句结束生成参数
    /// </summary>
    public AutoPar End()
    {
        var strResult = strSql.ToString();
        // pars = new ParCollection();

        foreach (var item in proList)
        {

            var val = item.GetValue(oMdl, null);
            if (val != null
                && strResult.IndexOf(item.Name, StringComparison.OrdinalIgnoreCase) != -1
                && strResult.IndexOf("@" + item.Name) != -1
                )
            {
                MemberInfo[] mi = oMdl.GetType().GetMember(item.Name);
                if (mi != null && mi.Length > 0)
                {

                    ORMFieldsAttribute attr = Attribute.GetCustomAttribute(mi[0], typeof(ORMFieldsAttribute)) as ORMFieldsAttribute;
                    if (attr != null)
                    {
                        //如果存在 DalAttribute 则根据其设置配置参数类型、长度、名称
                        var par = new SqlParameter();
                        par.ParameterName = item.Name;
                        par.Value = val;
                        par.SqlDbType = attr.FiledType;
                        if (!string.IsNullOrEmpty(attr.Name))
                        {
                            par.ParameterName = attr.Name;
                        }
                        if (attr.Size != 0)
                        {
                            par.Size = attr.Size;
                        }
                        pars.Add(par);
                    }
                    else
                    {
                        pars.Add(item.Name, val);
                    }
                }
            }
            else
            {
                pars.Add(item.Name, DBNull.Value);
            }
        }
        this.sql = this.strSql.ToString();
        this.arrpar = this.pars.ToArray();
        return this;
    }
}
#endregion

#region 参数集合
/// <summary>
/// 参数集合
/// </summary>
public class ParCollection : List<SqlParameter>
{

    public static ParCollection AddNew(SqlParameter par)
    {
        return new ParCollection().Add(par);
    }

    public static ParCollection AddNew(string name, object value)
    {
        return new ParCollection().Add(name, value);
    }

    public static ParCollection AddNew(string name, SqlDbType type, object value)
    {
        return new ParCollection().Add(name, type, value);
    }
    public static ParCollection AddNew(string name, SqlDbType type, object value, int size)
    {
        return new ParCollection().Add(name, type, value, size);
    }

    public static ParCollection AddNew(string name, string value, int size)
    {
        return new ParCollection().Add(name, value, size);
    }
    public ParCollection Add(SqlParameter par)
    {
        base.Add(par);
        return this;
    }
    public ParCollection Add(string name, object value)
    {
        base.Add(DBExtensions.GetSqlPar(name, value));
        return this;
    }
    public ParCollection Add(string name, string value, int size)
    {
        var par = new SqlParameter(name, SqlDbType.NVarChar, size);
        par.Value = value;
        base.Add(par);
        return this;
    }
    public ParCollection Add(string name, SqlDbType type, object value)
    {
        var par = new SqlParameter(name, type);
        par.Value = value;
        base.Add(par);
        return this;

    }
    public ParCollection Add(string name, SqlDbType type, object value, int size)
    {
        var par = new SqlParameter(name, type, size);
        par.Value = value;
        base.Add(par);
        return this;

    }
}

#endregion

#region 事物集合
/// <summary>
/// 事物集合
/// </summary>
public class TranCollection : List<TranMdl>
{

    public void Add(int order, AutoPar auto)
    {
        auto.End();
        base.Add(new TranMdl()
        {
            Order = order,
            Sql = auto.strSql,
            Pars = auto.pars
        });
    }
    public void Add(int order, StringBuilder sql, ParCollection pars = null)
    {
        base.Add(new TranMdl()
        {
            Order = order,
            Sql = sql,
            Pars = pars
        });
    }
}

#endregion

#region 事物实体
/// <summary>
/// 事物实体
/// </summary>
public class TranMdl
{
    //public TranMdl(int order, string sql, List<SqlParameter> pars)
    //{
    //    Order = order;
    //    Sql = sql;
    //    Pars = pars;
    //}
    public int Order { get; set; }
    public StringBuilder Sql { get; set; }
    public ParCollection Pars { get; set; }
}
#endregion
