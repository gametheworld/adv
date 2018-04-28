using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class ORMEnumAttribute : System.Attribute
{
    readonly string _englishName;
    readonly string _colorClass;
    public ORMEnumAttribute(string englishName, string colorClass = "")
    {
        this._englishName = englishName;
        this._colorClass = colorClass;

    }

    /// <summary>
    /// 枚举显示英文名称
    /// </summary>
    public string EnglishName
    {
        get { return _englishName; }
    }
    /// <summary>
    /// 枚举对应的样式名称
    /// </summary>
    public string ColorClass
    {
        get { return _colorClass; }
    }
}

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
public class ORMFieldsAttribute : System.Attribute
{
    public bool IsPrimary { get; set; }
    public string Name { get; set; }

    public SqlDbType FiledType { get; set; }

    public int Size { get; set; }

    public ORMFieldsAttribute(bool primiary = false, int size = 0, SqlDbType type = SqlDbType.NVarChar, string name = "")
    {
        FiledType = type;
        Size = size;
        Name = name;
        IsPrimary = primiary;
    }

}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class ORMModelsAttribute : Attribute
{
    // See the attribute guidelines at 
    //  http://go.microsoft.com/fwlink/?LinkId=85236
    readonly string tableName;

    // This is a positional argument
    public ORMModelsAttribute(string tableName)
    {
        this.tableName = tableName;

        // TODO: Implement code here
        //throw new NotImplementedException();
    }

    public string TableName
    {
        get { return tableName; }
    }

    // This is a named argument
    public int NamedInt { get; set; }
}