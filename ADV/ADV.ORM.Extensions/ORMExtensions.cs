using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 实体反射字段
/// </summary>
public static class DBExtensions
{
    public static SqlParameter GetSqlPar(string name, object value)
    {
        var par = new SqlParameter(name, value);
        if (value == null)
            value = DBNull.Value;
        var ty = value.GetType();
        var typeName = ty.Name;
        if (ty.BaseType.Equals(typeof(Enum)))
        {
            typeName = "Enum";
        }
        //参数类型
        switch (typeName)
        {
            case "Single":
                par.SqlDbType = SqlDbType.Float;
                break;
            case "Int32":
                par.SqlDbType = SqlDbType.Int;
                break;
            case "Int64":
                par.SqlDbType = SqlDbType.BigInt;
                break;
            case "String":
                par.SqlDbType = SqlDbType.VarChar;
                var strLen = value.ToString().Length;
                if (strLen < 50) { par.Size = 50; }
                if (strLen < 100) { par.Size = 100; }
                else if (strLen < 500) { par.Size = 500; }
                else if (strLen < 1000) { par.Size = 1000; }
                else if (strLen < 2000) { par.Size = 2000; }
                else if (strLen < 4000) { par.Size = 4000; }
                else { par.Size = -1; }
                break;
            case "Enum":
                par.SqlDbType = SqlDbType.Int;
                par.Value = (int)value;
                break;
            case "DateTime":
                par.SqlDbType = SqlDbType.DateTime;
                break;

        }
        return par;
    }

    /// <summary>
    /// 反射属性缓存
    /// </summary>
    public static Dictionary<string, List<PropertyInfo>> PropertyDictionary = new Dictionary<string, List<PropertyInfo>>();

    /// <summary>
    /// 获取属性集合
    /// </summary>
    /// <param name="modelType"></param>
    /// <returns></returns>
    public static List<PropertyInfo> _GetProperties(this Type modelType)
    {
        List<PropertyInfo> properties = null;
        string typeName = modelType.FullName;
        if (PropertyDictionary.TryGetValue(typeName, out properties))
        {
            return properties;
        }

        properties = modelType.GetProperties().ToList();
        if (properties != null)
        {
            lock (PropertyDictionary)
            {
                PropertyDictionary.Add(typeName, properties);
            }
        }
        return properties;
    }

    /// <summary>
    /// 设置值
    /// </summary>
    public static void _SetValue(this PropertyInfo p, object o, object v)
    {
        var vType = v.GetType();
        var oType = o.GetType();

        if (vType == typeof(DBNull))
        {
            if (oType == typeof(string))
            {
                v = "";
            }
            else if (oType == typeof(bool))
            {
                v = false;
            }
            else
            {
                v = 0;
            }
        }

        else if (vType == typeof(System.Single)
                || vType == typeof(System.Double))
        {
            p.SetValue(o, Convert.ChangeType(v, typeof(float)), null);
        }
        else
        {
            if (p.PropertyType.IsEnum)
            {
                int enumVal = 0;
                if (vType == typeof(Boolean))
                {
                    enumVal = Boolean.Parse(v.ToString()) ? 1 : 0;
                }
                else if (vType == typeof(Int16)
                    || vType == typeof(Int32)
                    )
                {
                    enumVal = int.Parse(v.ToString());
                }
                else
                {
                    return;
                }
                object enumName = Enum.ToObject(p.PropertyType, enumVal);
                p.SetValue(o, enumName, null); //获取枚举值，设置属性值
            }
            else
            {
                SetValue(o, p, v);
            }
            //    if (IsTypeNullable(p.PropertyType))
            //    {
            //         Convert.ConvertFromInvariantString
            //    }
            //    //// System.Nullable.
            //    ////if (!IsNullable(v.GetType()))
            //    ////{

            // // p.SetValue(o, Convert.ChangeType(v, p.PropertyType),null);
            //SetValue(o,
            //    ////}

        }

    }
    public static void SetValue(object inputObject, PropertyInfo propertyInfo, object propertyVal)
    {
        //find out the type
        //Type type = inputObject.GetType();

        ////get the property information based on the type
        //System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

        //find the property type
        Type propertyType = propertyInfo.PropertyType;

        //Convert.ChangeType does not handle conversion to nullable types
        //if the property type is nullable, we need to get the underlying type of the property
        var targetType = IsNullableType(propertyInfo.PropertyType)
            ? Nullable.GetUnderlyingType(propertyInfo.PropertyType)
            : propertyInfo.PropertyType;

        //Returns an System.Object with the specified System.Type and whose value is
        //equivalent to the specified object.
        propertyVal = Convert.ChangeType(propertyVal, targetType);

        //Set the value of the property
        propertyInfo.SetValue(inputObject, propertyVal, null);

    }
    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
    }

    /// <summary>
    /// 转换为实体
    /// </summary>
    public static T GetModel<T>(this DataSet ds)
    {
        if (ds == null) { return default(T); }
        if (ds.Tables == null || ds.Tables.Count == 0) { return default(T); }
        using (ds)
        {
            var dt = ds.Tables[0];
            if (dt.Rows == null || dt.Rows.Count == 0) { return default(T); }
            var list = new List<T>();
            Type t = typeof(T);
            var plist = typeof(T)._GetProperties();
            var item = dt.Rows[0];
            var cl = dt.Columns;
            T m = System.Activator.CreateInstance<T>();
            BindModel<T>(m, plist, cl, item);
            return m;
        }
    }

    /// <summary>
    /// 转换为List
    /// </summary>
    public static List<T> GetList<T>(this DataSet ds)
    {
        if (ds == null) { return null; }

        using (ds)
        {
            var dt = ds.Tables[0];
            var list = new List<T>();
            Type t = typeof(T);
            var plist = typeof(T)._GetProperties();

            var cl = dt.Columns;
            foreach (DataRow item in dt.Rows)
            {
                T m = System.Activator.CreateInstance<T>();
                BindModel<T>(m, plist, cl, item);
                list.Add(m);
            }
            return list;
        }
    }

    private static void BindModel<T>(T m, List<PropertyInfo> plist, DataColumnCollection cols, DataRow item)
    {
        foreach (var info in plist)
        {
            if (info.PropertyType.BaseType == typeof(System.Object)
                && info.PropertyType != typeof(System.String))
            {
                var s2 = System.Activator.CreateInstance(info.PropertyType);
                var plist2 = info.PropertyType._GetProperties();
                foreach (var info2 in plist2)
                {
                    if (cols.Contains(info2.Name))
                    {
                        info2._SetValue(s2, item[info2.Name]);
                    }
                }
                info.SetValue(m, s2, null);
            }
            else
            {
                if (cols.Contains(info.Name))
                {
                    info._SetValue(m, item[info.Name]);
                }
            }
        }

    }


}

