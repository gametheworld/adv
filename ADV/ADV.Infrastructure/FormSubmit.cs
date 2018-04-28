using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.Infrastructure
{
    public class FormSubmit
    {
        /// <summary>
        /// 建立请求表单，以HTMl形式构造
        /// </summary>
        /// <param name="StrJson">序列化对象字符串，</param>
        /// <param name="strMethod"></param>
        /// <param name="strButtonValue"></param>
        /// <returns></returns>
        public static string BuildRequest(string StrJson, string RequestURL,string strMethod, string strButtonValue,string CharSet="UTF-8") 
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + RequestURL + "'  _input_charset='" + CharSet + "' method='" + strMethod.ToLower().Trim() + "'>");
            if (!StrJson.IsNullOrEmpty()) 
            {
                var KeyValueItem = StrJson.Replace("{", "").Replace("}", "").Replace("\"", "").Split(',');
                foreach (var item in KeyValueItem)
                {
                    //item.Split(item.IndexOf(":"));
                    var Key = item.ToStringExt("").Substring(0, item.IndexOf(":"));
                    var Value = item.ToStringExt("").Replace("，", ",").Substring(item.IndexOf(":") + 1, item.Length - Key.Length - 1);
                    sbHtml.Append("<input type='text' name='" + Key + "' value='" + Value + "'/></br>");
                }
            }
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");

            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }
        /// <summary>
        /// 建立请求表单，以HTMl形式构造
        /// </summary>
        /// <param name="StrJson">序列化对象字符串，</param>
        /// <param name="strMethod"></param>
        /// <param name="strButtonValue"></param>
        /// <returns></returns>
        public static string BuildRequestParam(string StrJson)
        {
            StringBuilder sbHtml = new StringBuilder();
            //sbHtml.Append("?");
            if (!StrJson.IsNullOrEmpty())
            {
                var KeyValueItem = StrJson.Replace("{", "").Replace("}", "").Replace("\"", "").Split(',');
                foreach (var item in KeyValueItem)
                {
                    //item.Split(item.IndexOf(":"));
                    var Key = item.Substring(0, item.IndexOf(":"));
                    var Value = item.Substring(item.IndexOf(":") + 1, item.Length - Key.Length - 1);
                    sbHtml.Append(Key + "=" + Value + "&");
                }
                sbHtml.Append("1=1");
            }
            return sbHtml.Replace("&1=1", "").ToString();
        }
    }
}
