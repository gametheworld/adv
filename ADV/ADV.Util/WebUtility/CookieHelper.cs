using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VPEA.WebUtility;
namespace Util
{
    public class CookieHelper
    {
        /// <summary>  
        /// 清除指定Cookie  
        /// </summary>  
        /// <param name="cookiename">cookiename</param>  
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>  
        /// 获取指定Cookie值  
        /// </summary>  
        /// <param name="cookiename">cookiename</param>  
        /// <returns></returns>  
        public static string GetCookiesValue(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }
        /// <summary>
        /// 获取Cookie 中键对应的值
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetCookiesValue(string cookiename,string Key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie[Key];
            }
            return str;
        }

        /// <summary>
        /// 返回一个HttpCookie
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        public static HttpCookie GetCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                return cookie;
            }
            return null;
        }
        /// <summary>  
        /// 添加一个Cookie（24小时过期）  
        /// </summary>  
        /// <param name="cookiename"></param>  
        /// <param name="cookievalue"></param>  
        public static void SetCookie(string cookiename, string cookievalue)
        {
            SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
        }
        /// <summary>  
        /// 添加一个Cookie  
        /// </summary>  
        /// <param name="cookiename">cookie名</param>  
        /// <param name="cookievalue">cookie值</param>  
        /// <param name="expires">过期时间 DateTime</param>  
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = cookievalue,
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        //@@--xiachong--ADD-2017-03-24-- 设置和获取Cookie值，进行加密
        ///<summary>
        ///创建cookie值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>
        ///<param name="cookieValue">cookie值</param>
        ///<param name="cookieTime">cookie有效时间</param>
        public static void CreateCookieValue(string cookieName, string cookieValue, DateTime cookieTime)
        {
            EncryptHelper encryptHelper = new EncryptHelper();
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = HttpContext.Current.Server.UrlEncode(encryptHelper.EncryptString(cookieValue, "yunchuangjinfu"));
            cookie.Expires = cookieTime;
            //cookie.Domain = ConfigHelper.GetConfigString("CookieDoMain");//将cookie与域名关联，暂时用不到！
            HttpContext.Current.Response.Cookies.Add(cookie);
        }


        ///<summary>
        ///取得cookie的值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>
        ///<returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            EncryptHelper encryptHelper = new EncryptHelper();
            string cookieValue = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookieValue = "";
            }
            else
            {
                try
                {
                    cookieValue = encryptHelper.DecryptString(HttpContext.Current.Server.UrlDecode(cookie.Value), "yunchuangjinfu");
                }
                catch (Exception)
                {
                    cookieValue = "";
                }
            }
            return cookieValue;
        }
    }
}
