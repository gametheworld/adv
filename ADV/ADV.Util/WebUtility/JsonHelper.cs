using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;

namespace ADV.Util.WebUtility
{
    public class JsonHelper
    {
        /// <summary>
        /// 将对象转换成Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string ModelToJson<T>(T jsonObject) 
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            string json = null;
            using (MemoryStream ms = new MemoryStream()) //定义一个stream用来存发序列化之后的内容
            {
                serializer.WriteObject(ms, jsonObject);
                json = Encoding.UTF8.GetString(ms.GetBuffer()); //将stream读取成一个字符串形式的数据，并且返回
                ms.Close();
            }
            return json;

        }

        /// <summary>
        /// 将DataTable装换成Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt) 
        {
            return JsonConvert.SerializeObject(dt, new DataTableConverter());
        }

        /// <summary>
        /// 将list对象集合转换为Json
        /// </summary>
        /// <param name="listModel"></param>
        /// <returns></returns>
        public static string ListToJson<T>(T listModel)
        {
            try
            {
                DataContractJsonSerializer dataJson = new DataContractJsonSerializer(listModel.GetType());
                using (MemoryStream ms = new MemoryStream())
                {
                    dataJson.WriteObject(ms, listModel);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return null;
            }
        }

        public static string ResponseList(string Result, int Code, string Details, string Message, bool IsSuccess)
        {
            return "{\"Result\":" + Result + ",\"Code\":" + Code + ",\"Details\":" + Details + ",\"Message\":" + Message + ",\"Success\":" + IsSuccess + "}";
        }

        public static string ResponseMsg(string Result, int Code, string Details, string Message, bool IsSuccess)
        {
            return "{\"Result\":" + Result + ",\"Code\":" + Code + ",\"Details\":" + Details + ",\"Message\":" + Message + ",\"Success\":" + IsSuccess + "}";
        }
    }
}
