//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ADV.Entity;
//using model = ADV.Entity.rp_usr_Admin;

//namespace Common
//{
//    public class ManageGetAdmin
//    {
//        /// <summary>
//        /// 获取当前登录的用户对象
//        /// </summary>
//        /// <returns></returns>
//        public static model GetUser()
//        {
//            rp_usr_Admin admin = null;
//            var Session = System.Web.HttpContext.Current.Session;
//            var user = Session[ManagerCookieMation.SESSIONNAME];
//            if (user != null)
//            {
//                admin = user as ADV.Entity.rp_usr_Admin;
//            }
//            else
//            {
//                //跳转至登录界面
//                //System.Web.HttpContext.Current.Response.Redirect("/Login.aspx"); 
//            }
//            return admin;
//        }

//        /// <summary>
//        /// 获取当前管理员的GUID
//        /// </summary>
//        public static string GetAdminUserID
//        {
//            get
//            {
//                string userId = string.Empty;
//                rp_usr_Admin admin = GetUser();
//                if (admin != null)
//                {
//                    userId = admin.UserID.ToString();
//                }
//                else 
//                { 
//                    //跳转至登录界面
//                    //System.Web.HttpContext.Current.Response.Redirect("/Login.aspx"); 
//                }
//                return userId;
//            }
//        }

//        /// <summary>
//        /// 获取当前管理员的真实姓名
//        /// </summary>
//        public static string GetAdminUserName
//        {
//            get
//            {
//                string userName = string.Empty;
//                rp_usr_Admin admin = GetUser();
//                if (admin != null)
//                {
//                    userName = admin.LoginName.ToString();
//                }
//                else
//                {
//                    System.Web.HttpContext.Current.Response.Redirect("/Login.aspx");
//                }
//                return userName;
//            }
//        }
//    }
//}
