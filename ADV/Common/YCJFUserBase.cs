//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ADV.Entity.User;

//namespace Common
//{
//    public class YCJFUserBase : BaseController
//    {
//        public t_usr_Users GetUserInfo
//        {
//            get
//            {
//                var StrUserInfo = new ClaimsADVSession().UserInfo;
//                t_usr_Users u = null;
//                if (StrUserInfo != null)
//                {
//                    u = JsonConvert.DeserializeObject<t_usr_Users>(StrUserInfo);

//                }
//                return u;
//            }
//        }
//    }
//}
