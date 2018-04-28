//using System;
//using System.Web;
//using ADV.Entity;
//using model = ADV.Entity.rp_usr_Admin;
//using ADV.Service;
//using VPEA.WebUtility;

//namespace Common
//{
//    public class ManagerCookieMation
//    {
//        public static string SESSIONNAME = "YUNCHUANGJINFU_USERINFO";
//        private const string COOKIEKEY = "6a6b2h4aP6g7D62R7Rz9bwK8ti36Rj9q";
//        public static string COOKIENAME = "YUNCHUANGJINFUCOOKIEADMIN";
//        /// <summary>
//        /// 获取IP地址
//        /// </summary>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        private string GetCookie(HttpContext context)
//        {
//            string IPAddress = "";
//            if (context.Request.ServerVariables["HTTP_VIA"] != null) // using proxy
//            {
//                IPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();  // Return real client IP.
//            }
//            else// not using proxy or can't get the Client IP
//            {
//                IPAddress = context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
//            }
//            if (IPAddress.IsEmpty())
//            {
//                IPAddress = context.Request.UserHostAddress;
//            }
//            return IPAddress;
//        }
//        /// <summary>
//        /// 保存登陆信息到cookie中
//        /// </summary>
//        /// <param name="email"></param>
//        public void GenerateCookie(string UserName, int day)
//        {
//            var context = System.Web.HttpContext.Current;
//            string IPAddress = GetCookie(context);


//            string encryptedString = "{0}|{1}|{2}";//{email}|{IP}|{datetime}

//            encryptedString = encryptedString.Fomart(UserName, IPAddress, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

//            var EncrypedResult = Util.DEncrypt.DEncrypt.Encrypt(encryptedString, COOKIEKEY);
//            HttpCookie ck = new HttpCookie("ZHONFXHOUJIA");
//            ck["account"] = UserName;
//            ck["token"] = EncrypedResult;
//            //ck.Domain = context.Request.Url.Host;
//            ck.Expires = DateTime.Now.AddDays(day);
//            ck.HttpOnly = true;
//            context.Response.Cookies.Add(ck);
//        }

//        /// <summary>
//        /// 保存后台登陆信息到cookie中
//        /// </summary>
//        /// <param name="email"></param>
//        public void GenerateAdminCookie(string UserName, int day)
//        {
//            var context = System.Web.HttpContext.Current;
//            string IPAddress = GetCookie(context);
//            string encryptedString = "{0}|{1}|{2}";//{email}|{IP}|{datetime}
//            encryptedString = encryptedString.Fomart(UserName, IPAddress, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
//            var EncrypedResult = Util.DEncrypt.DEncrypt.Encrypt(encryptedString, COOKIEKEY);
//            HttpCookie ck = new HttpCookie(COOKIENAME);
//            ck["loginName"] = UserName;
//            ck["token"] = EncrypedResult;
//            //ck.Domain = context.Request.Url.Host;
//            ck.Expires = DateTime.Now.AddDays(day);
//            ck.HttpOnly = true;
//            context.Response.Cookies.Add(ck);
//        }

//        /// <summary>
//        /// 通过cookie获取当前实体
//        /// </summary>
//        /// <returns></returns>
//        public model GetAdminModelForCookie()
//        {
//            var context = System.Web.HttpContext.Current;
//            string IPAddress = GetCookie(context);
//            var cookie = context.Request.Cookies[COOKIENAME];
//            if (cookie != null && cookie.HasKeys)
//            {
//                string UserName = cookie["loginName"],
//                        token = cookie["token"];
//                var decryptResult = Util.DEncrypt.DEncrypt.Decrypt(token, COOKIEKEY);
//                var tokenArray = decryptResult.Split('|');
//                if (tokenArray.Length > 0)
//                {
//                    string tokenUserName = tokenArray[0],
//                        tokenIP = tokenArray[1],
//                        tokenTime = tokenArray[2];
//                    //这里可以加上时间判断
//                    if (tokenUserName == UserName && tokenIP == IPAddress)
//                    {
//                        //return new UserDAL().GetUserByName(UserName);
//                        return new rp_user_AdminDAL().GetAdminInfo(UserName);
//                    }
//                }
//            }
//            return null;
//        }



//        //@@--xiachong--ADD-2017-03-24-- 设置和获取Cookie值，进行加密
//        ///<summary>
//        ///创建cookie值
//        ///</summary>
//        ///<param name="cookieName">cookie名称</param>
//        ///<param name="cookieValue">cookie值</param>
//        ///<param name="cookieTime">cookie有效时间</param>
//        public static void CreateCookieValue(string cookieName, string cookieValue, DateTime cookieTime)
//        {
//            EncryptHelper encryptHelper = new EncryptHelper();
//            HttpCookie cookie = new HttpCookie(cookieName);
//            cookie.Value = HttpContext.Current.Server.UrlEncode(encryptHelper.EncryptString(cookieValue, "yunchuangjinfu"));
//            cookie.Expires = cookieTime;
//            //cookie.Domain = ConfigHelper.GetConfigString("CookieDoMain");//将cookie与域名关联，暂时用不到！
//            HttpContext.Current.Response.Cookies.Add(cookie);
//        }


//        ///<summary>
//        ///取得cookie的值
//        ///</summary>
//        ///<param name="cookieName">cookie名称</param>
//        ///<returns></returns>
//        public static string GetCookieValue(string cookieName)
//        {
//            EncryptHelper encryptHelper = new EncryptHelper();
//            string cookieValue = "";
//            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
//            if (cookie == null)
//            {
//                cookieValue = "";
//            }
//            else
//            {
//                try
//                {
//                    cookieValue = encryptHelper.DecryptString(HttpContext.Current.Server.UrlDecode(cookie.Value), "yunchuangjinfu");
//                }
//                catch (Exception)
//                {
//                    cookieValue = "";
//                }
//            }
//            return cookieValue;
//        }
//    }
//}
