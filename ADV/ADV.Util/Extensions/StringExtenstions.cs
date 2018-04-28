using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class StringExtenstions
{
    public static bool IsNullOrEmpty(this string s)
    {
        return string.IsNullOrWhiteSpace(s);
    }

    public static string Join(string separator, params object[] args)
    {
        return string.Join(separator, args);
    }
    public static string Format(string format, params object[] args)
    {
        return string.Format(format, args);
    }
    public static string Format(string format, object arg0)
    {
        return string.Format(format, arg0);
    }
    public static string Format(string format, object arg0, object arg1)
    {
        return string.Format(format, arg0, arg1);

    }
    public static string Format(string format, object arg0, object arg1, object arg2)
    {
        return string.Format(format, arg0, arg1, arg2);
    }
    public static string Format(IFormatProvider provider, string format, params object[] args)
    {
        return string.Format(provider, format, args);
    }

    public static bool IsMatch(this string s, string pattern)
    {
        if (s == null) return false;
        else return Regex.IsMatch(s, pattern);
    }

    public static string Match(this string s, string pattern)
    {
        if (s == null) return "";
        return Regex.Match(s, pattern).Value;
    }

    public static bool IsInt(this string s)
    {
        int i;
        return int.TryParse(s, out i);
    }

    public static int ToInt(this string s)
    {
        int i = 0;
        int.TryParse(s, out i);
        return i;
    }
    public static bool IsLong(this string s)
    {
        long i;
        return long.TryParse(s, out i);
    }

    public static long ToLong(this string s)
    {
        long i = 0;
        long.TryParse(s, out i);
        return i;
    }

    public static bool IsDateTime(this string s)
    {
        DateTime d;
        return DateTime.TryParse(s, out d);
    }
    /// <summary>
    /// 将string时间，转换为DateTime
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string s)
    {
        DateTime d;
        DateTime.TryParse(s, out d);
        return d;
    }

    /// <summary>
    /// /// 时间格式化默认不传FormatDate yyyy/MM/dd 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formatDate"></param>
    /// <param name="emptyText"></param>
    /// <returns></returns>
    public static string NullDateTimeToStr(this string s, string formatDate = "", string emptyText = "/")
    {
        if (s.IsEmpty() || !s.IsDateTime())
        {
            return emptyText;
        }
        DateTime dt = new DateTime(2000, 1, 1);//默认值
        DateTime result = s.StrToDate(dt);
        if (formatDate.IsNotEmpty())
        {
            return result.ToString(formatDate);
        }
        else
        {
            return result.ToString("yyyy/MM/dd ");
        }

    }
    /// <summary>
    /// 时间格式化默认不传FormatDate yyyy-MM-dd
    /// </summary>
    /// <param name="s"></param>
    /// <param name="formatDate"></param>
    /// <param name="emptyText"></param>
    /// <returns></returns>
    public static string NullDateTimeToFormat(this string s, string formatDate = "", string emptyText = "/")
    {
        if (s.IsEmpty() || !s.IsDateTime())
        {
            return emptyText;
        }
        DateTime dt = new DateTime(2000, 1, 1);//默认值
        DateTime result = s.StrToDate(dt);
        if (formatDate.IsNotEmpty())
        {
            return result.ToString(formatDate);
        }
        else
        {
            return result.ToString("yyyy-MM-dd ");
        }

    }
    /// <summary>
    /// 将 “空” 转换自定义字符串
    /// </summary>
    /// <param name="s"></param>
    /// <param name="emptyText"></param>
    /// <returns></returns>
    public static string NullEnumToStr(this string s, string emptyText = "/")
    {
        if (s == "空")
        {
            return "/";
        }
        return s;

    }
    /// <summary>
    /// 字段为空转换为/
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string NullStringToStr(this string str)
    {
        if (str.IsEmpty())
        {
            return "/";
        }
        return str;

    }

    public static bool IsBoolean(this string input)
    {
        if (input.Equals("true", StringComparison.CurrentCultureIgnoreCase)) return true;
        else if (input.Equals("false", StringComparison.InvariantCultureIgnoreCase)) return true;
        else return false;
    }

    public static bool ToBoolean(this string input)
    {
        if (input.Equals("true", StringComparison.CurrentCultureIgnoreCase)) return true;
        else if (input.Equals("false", StringComparison.InvariantCultureIgnoreCase)) return false;
        else return false; // 考虑是否抛出异常?
    }

    public static object ToDBValue(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return DBNull.Value;
        }
        else
        {
            return input;
        }
    }

    /// <summary>
    /// 转换成字符串(转换不成功可设置默认值)
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="defaultVal">默认值，默认为空字符串""</param>
    /// <returns></returns>
    public static string ToStringExt(this string input, string defaultVal = "")
    {
        return Cast.ToStringExt(input, defaultVal);
    }

    /// <summary>
    /// 转换成字符串(转换不成功可设置默认值)
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="defaultVal">默认值，默认为空字符串""</param>
    /// <returns></returns>
    public static string ToStringExt(this object input, string defaultVal = "")
    {
        return Cast.ToStringExt(input, defaultVal);
    }

    /// <summary>
    /// 转换成Int类型(转换不成功可设置默认值)
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="defaultVal">默认值，默认为0</param>
    /// <returns></returns>
    public static int ToIntExt(this string input, int defaultVal = 0)
    {
        return Cast.ToIntExt(input, defaultVal);
    }

    /// <summary>
    /// 替换新的字符串
    /// </summary>
    /// <param name="inputStr">输入字符串</param>
    /// <param name="replaceStr">替换的字符</param>
    /// <param name="startIndex">替换起始索引</param>
    /// <param name="endIndex">替换结束索引</param>
    /// <returns></returns>
    public static string Replace(this string inputStr, string replaceStr, int startIndex, int endIndex)
    {
        int strLen = inputStr.Length;
        if (strLen <= startIndex + 1)
        {
            return inputStr;
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < strLen; i++)
        {
            if (i >= startIndex && i <= endIndex)
            {
                sb.Append(replaceStr);
            }
            else
            {
                sb.Append(inputStr[i]);

            }
        }

        return sb.ToString();
    }

}
