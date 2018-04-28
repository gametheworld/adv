using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class StringExtionsion
{
    public static String DisplayForHtml(this string str)
    {
        if (str != null)
        {
            str = str.Replace("\n", " <br/>");
        }
        else { str = ""; }
        return str;
    }

    public static string SubStrNoHtml_Char(this string str, int len, string other = "")
    {
        return str.StripHTML().StrSubStringForChar(len, other);
    }

    public static String CheckSql(String InString)
    {

        InString = InString.Replace("'", "");
        InString = InString.Replace("\"", "");
        InString = InString.Replace("or", "");
        InString = InString.Replace("and", "");

        return InString;
    }

    public static string StripHTML(this string strHtml)
    {
        string[] aryReg =
    {
      @"<script[^>]*?>.*?</script>",
      @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", @"([\r\n])[\s]+", @"&(quot|#34);", @"&(amp|#38);", @"&(lt|#60);", @"&(gt|#62);", @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);",
        @"&(copy|#169);", @"&#(\d+);", @"-->", @"<!--.*\n"
    };
        string[] aryRep =
    {
      "", "", "", "\"", "&", "<", ">", "   ", "\xa1",  //chr(161),
      "\xa2",  //chr(162),
      "\xa3",  //chr(163),
      "\xa9",  //chr(169),
      "", "\r\n", ""
    };
        string newReg = aryReg[0];
        string strOutput = strHtml;
        for (int i = 0; i < aryReg.Length; i++)
        {
            Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
            strOutput = regex.Replace(strOutput, aryRep[i]);
        }
        strOutput.Replace("<", "");
        strOutput.Replace(">", "");
        strOutput.Replace("\r\n", "");
        return strOutput;
    }


    public static T ValueParse<T>(string strValue, T defaultValue)
    {
        T returnValue = defaultValue;
        if (!string.IsNullOrEmpty(strValue))
        {
            try
            {
                return (T)Convert.ChangeType(strValue, typeof(T));
            }
            catch
            {
                return returnValue;
            }
        }
        else
        {
            return returnValue;
        }
    }
    #region  SubString
    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="value"></param>
    /// <param name="length"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static string StrSubString(this string value, int length, string other)
    {
        if (!string.IsNullOrEmpty(value) && value.Length > length)
        {
            value = value.Substring(0, length);
            return value += other;
        }
        return value;
    }


    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="value"></param>
    /// <param name="length"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static string StrSubString(this  string value, int length, string other, string nullstr)
    {
        if (string.IsNullOrEmpty(value))
        {
            return nullstr;
        }
        if (!string.IsNullOrEmpty(value) && value.Length > length)
        {
            value = value.Substring(0, length);
            return value += other;
        }
        return value;
    }
    public static string StrSubStringForChar(this string aOrgStr, int aLength, string other)
    {
        if (aOrgStr.IsEmpty())
        {
            return "";
        }
        int intLen = aOrgStr.Length;
        int start = 0;
        int end = intLen;
        int single = 0;
        char[] chars = aOrgStr.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (System.Convert.ToInt32(chars[i]) > 255)
            {
                start += 2;
            }
            else
            {
                start += 1;
                single++;
            }
            if (start >= aLength)
            {

                if (end % 2 == 0)
                {
                    if (single % 2 == 0)
                    {
                        end = i + 1;
                    }
                    else
                    {
                        end = i;
                    }
                }
                else
                {
                    end = i + 1;
                }
                break;
            }
        }
        string temp = aOrgStr.Substring(0, end);
        if (!string.IsNullOrEmpty(temp) && end < aOrgStr.Length)
        {
            return temp += other;
        }
        //  string temp2 = aOrgStr.Remove(0, end);
        //  aAfterStr = temp2; //剩余字符串
        return temp;
    }



    public static string SubstringToHTML(string param, int length, string end)
    {
        bool isCut = false;
        return SubstringToHTML(param, length, end, out isCut);
    }
    /// <summary>
    /// 按字节长度截取字符串(支持截取带HTML代码样式的字符串)
    /// </summary>
    /// <param name="param">将要截取的字符串参数</param>
    /// <param name="length">截取的字节长度</param>
    /// <param name="end">字符串末尾补上的字符串</param>
    /// <returns>返回截取后的字符串</returns>
    public static string SubstringToHTML(string param, int length, string end, out bool isCut)
    {
        string Pattern = null;
        MatchCollection m = null;
        StringBuilder result = new StringBuilder();
        int n = 0;
        char temp;
        isCut = false;
        bool isCode = false; //是不是HTML代码
        bool isHTML = false; //是不是HTML特殊字符,如&nbsp;
        char[] pchar = param.ToCharArray();
        if (pchar.Length < length) { return param; }//判断是否需要截取
        isCut = true;
        for (int i = 0; i < pchar.Length; i++)
        {
            temp = pchar[i];
            if (temp == '<')
            {
                isCode = true;
            }
            else if (temp == '&')
            {
                isHTML = true;
            }
            else if (temp == '>' && isCode)
            {
                n = n - 1;
                isCode = false;
            }
            else if (temp == ';' && isHTML)
            {
                isHTML = false;
            }

            if (!isCode && !isHTML)
            {
                n = n + 1;
                //UNICODE码字符占两个字节
                if (System.Text.Encoding.Default.GetBytes(temp + "").Length > 1)
                {
                    n = n + 1;
                }
            }

            result.Append(temp);
            if (n >= length)
            {
                break;
            }
        }
        result.Append(end);
        //取出截取字符串中的HTML标记
        string temp_result = result.ToString().Replace("(>)[^<>]*(<?)", "$1$2");
        //去掉不需要结素标记的HTML标记
        temp_result = temp_result.Replace(@"</?(AREA|BASE|BASEFONT|BODY|BR|COL|COLGROUP|DD|DT|FRAME|HEAD|HR|HTML|IMG|INPUT|ISINDEX|LI|LINK|META|OPTION|P|PARAM|TBODY|TD|TFOOT|TH|THEAD|TR|area|base|basefont|body|br|col|colgroup|dd|dt|frame|head|hr|html|img|input|isindex|li|link|meta|option|p|param|tbody|td|tfoot|th|thead|tr)[^<>]*/?>",
         "");
        //去掉成对的HTML标记
        temp_result = temp_result.Replace(@"<([a-zA-Z]+)[^<>]*>(.*?)</\1>", "$2");
        //用正则表达式取出标记
        Pattern = ("<([a-zA-Z]+)[^<>]*>");
        m = Regex.Matches(temp_result, Pattern);
        ArrayList endHTML = new ArrayList();
        foreach (Match mt in m)
        {
            endHTML.Add(mt.Result("$1"));
        }
        //补全不成对的HTML标记
        for (int i = endHTML.Count - 1; i >= 0; i--)
        {
            result.Append("</");
            result.Append(endHTML[i]);
            result.Append(">");
        }
        return result.ToString();
    }
    public static string StrSubStringForChar(string value, int startindex, int length)
    {
        string ret = "";
        int start = 0;
        System.Text.Encoding sjis = System.Text.Encoding.GetEncoding("Shift-JIS");
        if (startindex < 0) { startindex = 0; }
        if (length < 0) { length = 0; }
        if (startindex == 0)
        {
            start = 0;
        }
        else
        {
            int bytecnt = 0;
            for (int i = 0; i < value.Length; i++)
            {
                bytecnt += sjis.GetByteCount(value.Substring(i, 1));
                if (bytecnt >= startindex)
                {
                    start = i + 1;
                    break;
                }
            }
        }
        for (int i = 0; i < value.Length; i++)
        {
            if (i >= start)
            {
                if ((sjis.GetByteCount(ret + value.Substring(i, 1)) <= length) || (length == 0))
                {
                    ret += value.Substring(i, 1);
                }
            }
        }
        return ret;
    }

    #endregion


    public static int GetLength(String aOrgStr)
    {
        int intLen = aOrgStr.Length;
        int i;
        char[] chars = aOrgStr.ToCharArray();
        for (i = 0; i < chars.Length; i++)
        {
            if (System.Convert.ToInt32(chars[i]) > 255)
            {
                intLen++;
            }
        }
        return intLen;
    }

    //private static bool status = false;
    ///// <summary>
    ///// 分词
    ///// </summary>
    ///// <param name="word"></param>
    ///// <returns></returns>
    //public static List<string> WordSplit(string word)
    //{

    //    if (!status) { PanGu.Segment.Init(); status = true; }

    //    PanGu.Segment sg = new PanGu.Segment();
    //    ICollection<PanGu.WordInfo> words = sg.DoSegment(word);
    //    List<string> list = new List<string>();
    //    foreach (PanGu.WordInfo wd in words)
    //    {
    //        list.Add(wd.Word);
    //    }
    //    return list;
    //}





    public static List<string> GetStrArray(string str, char speater, bool toLower)
    {
        List<string> list = new List<string>();
        string[] ss = str.Split(speater);
        foreach (string s in ss)
        {
            if (!string.IsNullOrEmpty(s) && s != speater.ToString())
            {
                string strVal = s;
                if (toLower)
                {
                    strVal = s.ToLower();
                }
                list.Add(strVal);
            }
        }
        return list;
    }
    public static string[] GetStrArray(string str)
    {
        return str.Split(new char[',']);
    }
    public static string GetArrayStr(List<string> list, string speater)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            if (i == list.Count - 1)
            {
                sb.Append(list[i]);
            }
            else
            {
                sb.Append(list[i]);
                sb.Append(speater);
            }
        }
        return sb.ToString();
    }


    #region 删除最后一个字符之后的字符

    /// <summary>
    /// 删除最后结尾的一个逗号
    /// </summary>
    public static string DelLastComma(string str)
    {
        return str.Substring(0, str.LastIndexOf(","));
    }

    /// <summary>
    /// 删除最后结尾的指定字符后的字符
    /// </summary>
    public static string DelLastChar(string str, string strchar)
    {
        return str.Substring(0, str.LastIndexOf(strchar));
    }

    #endregion




    /// <summary>
    /// 转全角的函数(SBC case)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToSBC(string input)
    {
        //半角转全角：
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 32)
            {
                c[i] = (char)12288;
                continue;
            }
            if (c[i] < 127)
                c[i] = (char)(c[i] + 65248);
        }
        return new string(c);
    }

    /// <summary>
    ///  转半角的函数(SBC case)
    /// </summary>
    /// <param name="input">输入</param>
    /// <returns></returns>
    public static string ToDBC(string input)
    {
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }
            if (c[i] > 65280 && c[i] < 65375)
                c[i] = (char)(c[i] - 65248);
        }
        return new string(c);
    }

    public static List<string> GetSubStringList(string o_str, char sepeater)
    {
        List<string> list = new List<string>();
        string[] ss = o_str.Split(sepeater);
        foreach (string s in ss)
        {
            if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
            {
                list.Add(s);
            }
        }
        return list;
    }


    #region 将字符串样式转换为纯字符串
    public static string GetCleanStyle(string StrList, string SplitString)
    {
        string RetrunValue = "";
        //如果为空，返回空值
        if (StrList == null)
        {
            RetrunValue = "";
        }
        else
        {
            //返回去掉分隔符
            string NewString = "";
            NewString = StrList.Replace(SplitString, "");
            RetrunValue = NewString;
        }
        return RetrunValue;
    }
    #endregion

    #region 将字符串转换为新样式
    public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
    {
        string ReturnValue = "";
        //如果输入空值，返回空，并给出错误提示
        if (StrList == null)
        {
            ReturnValue = "";
            Error = "请输入需要划分格式的字符串";
        }
        else
        {
            //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
            int strListLength = StrList.Length;
            int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
            if (strListLength != NewStyleLength)
            {
                ReturnValue = "";
                Error = "样式格式的长度与输入的字符长度不符，请重新输入";
            }
            else
            {
                //检查新样式中分隔符的位置
                string Lengstr = "";
                for (int i = 0; i < NewStyle.Length; i++)
                {
                    if (NewStyle.Substring(i, 1) == SplitString)
                    {
                        Lengstr = Lengstr + "," + i;
                    }
                }
                if (Lengstr != "")
                {
                    Lengstr = Lengstr.Substring(1);
                }
                //将分隔符放在新样式中的位置
                string[] str = Lengstr.Split(',');
                foreach (string bb in str)
                {
                    StrList = StrList.Insert(int.Parse(bb), SplitString);
                }
                //给出最后的结果
                ReturnValue = StrList;
                //因为是正常的输出，没有错误
                Error = "";
            }
        }
        return ReturnValue;
    }
    #endregion

    public static bool IsEmpty(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
    public static bool IsNotEmpty(this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }
    public static string Fomart(this string format, params object[] args)
    {
        return string.Format(format: format, args: args);
    }

    public static string GetShortPY(string str)
    {
        char[] chas = str.ToCharArray();
        string strpy = "";

        foreach (char cha in chas)
        {
            if (!ChineseChar.IsValidChar(cha))
            {
                strpy += cha;
            }
            else
            {
                ChineseChar china = new ChineseChar(cha);
                strpy += china.Pinyins[0][0].ToString().ToUpper();
            }
        }
        return strpy;
    }

    public static string GetFulltPY(string str)
    {
        char[] chas = str.ToCharArray();
        string strpy = "";
        foreach (char cha in chas)
        {
            if (ChineseChar.IsValidChar(cha))
            {

                ChineseChar china = new ChineseChar(cha);
                if (china.Pinyins.Count > 0)
                {
                    for (int i = 0; i < china.Pinyins[0].Length - 1; i++)
                    {
                        if (i == 0)
                        {
                            strpy += china.Pinyins[0][0].ToString().ToUpper();
                        }
                        else
                        {
                            strpy += china.Pinyins[0][i].ToString().ToLower();
                        }
                    }
                }
            }
        }
        return strpy;
    }
    public static bool HasCn(string str)
    {
        char[] chars = str.ToCharArray();
        foreach (char cha in chars)
        {
            if (ChineseChar.IsValidChar(cha))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsCn(string str)
    {
        char[] chars = str.ToCharArray();
        foreach (char cha in chars)
        {
            if (!ChineseChar.IsValidChar(cha))
            {
                return false;
            }
        }
        return true;
    }

    public static string ParseTags(this string HTMLStr)
    {
        if (HTMLStr == null)
        {
            return null;
        }

        return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");


    }

    public static string ToString(this DateTime? date, string format = "")
    {
        if (date == null)
        {
            return "";
        }

        if (string.IsNullOrWhiteSpace(format))
        {
            return date.Value.ToString();

        }
        else
        {
            return date.Value.ToString(format);

        }

    }







}