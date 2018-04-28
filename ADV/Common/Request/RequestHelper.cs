//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ADV.Entity.Pay;
//using Util;
//using System.Net;
//using System.IO;
//using System.Text.RegularExpressions;
//using System.IO.Compression;
//using System.Xml;
//using ADV.Entity;

//namespace Common
//{
//    public class NotifyMessage<T> where T : class 
//    {
//        public string Route { get; set; }

//        public string InputParamStr { get; set; }

//        public T OutPutParamStr { get; set; }

//        public string ClientIP { get; set; }

//        public string Response { get; set; }

//        public string StartTime { get; set; }

//        public string OutTime { get; set; }

//        public long Duration { get; set; }

//        public string DurationLevel { get { return GetLevel(Duration); } }


//        public string ErrorMsg { get; set; }

//        public bool Flag { get; set; }

//        public string UserId { get; set; }
//        /// <summary>
//        /// 根据返回的
//        /// </summary>
//        /// <param name="rf"></param>
//        /// <returns></returns>
//        public string GetCurrentObjectStr(string ResponseHtml,ResponseFormat rf)
//        {
//            switch (rf)
//            {
//                case ResponseFormat.XML: 
//                    {
//                        try
//                        {
//                            XmlDocument doc = new XmlDocument();
//                            doc.LoadXml(ResponseHtml);
//                            var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
//                            json = json.Replace("\"?xml\":{\"@version\":\"1.0\",\"@encoding\":\"UTF-8\"}", "");
//                            return json;
//                        }
//                        catch (Exception ex)
//                        {
//                            //记录Error日志，
//                            return "";
//                        }
                        
//                    }
//                case ResponseFormat.GETSTR: 
//                    {
//                        var json = ResponseHtml.Replace("&", "\",\"").Replace("=", "\":\"");
//                        json = "{\"" + json + "\"}";
//                        return json;
//                    }
//                case ResponseFormat.Default:
//                    {
//                        return "{\"XS_SB\":\"" + ResponseHtml + "\"}";
//                    }
//                default:
//                    {
//                        return ResponseHtml;
//                    }

//            }
//        }

//        public string GetLevel(long Duration)
//        {
//            if (Duration == 0)
//            {
//                return "0";
//            }
//            else
//            {
//                if (Duration > 0 && Duration <= 50)
//                {
//                    return "1-50";
//                }
//                else
//                {
//                    if (Duration > 50 && Duration <= 200)
//                    {
//                        return "51-200";
//                    }
//                    else
//                    {
//                        if (Duration > 200 && Duration <= 500)
//                        {
//                            return "201-500";
//                        }
//                        else
//                        {
//                            if (Duration > 500 && Duration <= 2000)
//                            {
//                                return "501-2000";
//                            }
//                            else
//                            {
//                                if (Duration > 2000 && Duration <= 5000)
//                                    return "2001-5000";
//                                else
//                                    return "5001+";
//                            }
//                        }
//                    }
//                }
//            }

//        }
//    }
//    public class RequestHelper<T> where T : class
//    {
//        public static T PostByTokenSignSync(string Url, PayRequestEmpty obj, string RequestMethod = "GET")
//        {
//            NotifyMessage<T> message = new NotifyMessage<T>();
//            var ParamValues = JsonConvert.SerializeObject(obj);
//            var bodyByteArray = Encoding.UTF8.GetBytes(ParamValues);
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
//            request.Method = RequestMethod;
//            request.ContentType = "application/x-www-form-urlencoded";
//            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
//            if (bodyByteArray.Length > 0)
//            {
//                using (var stream = request.GetRequestStream())
//                {
//                    stream.Write(bodyByteArray, 0, bodyByteArray.Length);
//                }
//            }
//            #region 新解析HTML方法
//            using (HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse())
//            {
//                byte[] ResponseByte = null;
//                using (MemoryStream _stream = new MemoryStream())
//                {
//                    //GZIIP处理
//                    if (httpResponse.ContentEncoding != null && httpResponse.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
//                    {
//                        //开始读取流并设置编码方式
//                        new GZipStream(httpResponse.GetResponseStream(), CompressionMode.Decompress).CopyTo(_stream, 10240);
//                    }
//                    else
//                    {
//                        //开始读取流并设置编码方式
//                        httpResponse.GetResponseStream().CopyTo(_stream, 10240);
//                    }
//                    //获取Byte
//                    ResponseByte = _stream.ToArray();
//                }
//                //默认的编码
//                Encoding htmlencoding = Encoding.UTF8;
//                if (ResponseByte != null & ResponseByte.Length > 0)
//                {
//                    Match meta = Regex.Match(Encoding.Default.GetString(ResponseByte), "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
//                    string c = (meta.Groups.Count > 1) ? meta.Groups[2].Value.ToLower().Trim() : string.Empty;
//                    if (c.Length > 2)
//                    {
//                        try
//                        {
//                            if (c.IndexOf(" ") > 0) c = c.Substring(0, c.IndexOf(" "));
//                            htmlencoding = Encoding.GetEncoding(c.Replace("\"", "").Replace("'", "").Replace(";", "").Replace("iso-8859-1", "gbk").Trim());
//                        }
//                        catch
//                        {
//                            if (string.IsNullOrEmpty(httpResponse.CharacterSet)) htmlencoding = Encoding.UTF8;
//                            else htmlencoding = Encoding.GetEncoding(httpResponse.CharacterSet);
//                        }
//                    }
//                    else
//                    {
//                        if (string.IsNullOrEmpty(httpResponse.CharacterSet)) htmlencoding = Encoding.UTF8;
//                        else htmlencoding = Encoding.GetEncoding(httpResponse.CharacterSet);
//                    }
//                }
//                //得到返回的HTML
//                var html = htmlencoding.GetString(ResponseByte);
                
//                //判断Unicode转码问题
//                if (!string.IsNullOrEmpty(html) && html.IsNormalized())  
//                {
//                    try
//                    {
//                        html = Regex.Unescape(html);
//                    }
//                    catch (Exception ex)
//                    {
//                        LogHelp.CreateInstance().Error("请求异常");
//                    }
//                }
//                var json = message.GetCurrentObjectStr(html, obj.ResponseFormat);
//                message.OutPutParamStr = JsonConvert.DeserializeObject<T>(json);
//            }
//            #endregion

//            return message.OutPutParamStr;
//        }



//        public static string PostByDraw(string Url, string postDataStr) 
//        {
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
//            request.Method = "POST";
//            request.ContentType = "application/x-www-form-urlencoded";
//            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
//            Stream myRequestStream = request.GetRequestStream();
//            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
//            myStreamWriter.Write(postDataStr);
//            myStreamWriter.Close();

//            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

//            Stream myResponseStream = response.GetResponseStream();
//            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
//            string retString = myStreamReader.ReadToEnd();
//            myStreamReader.Close();
//            myResponseStream.Close();

//            return retString;
//        }
//    }
//}
