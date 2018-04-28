using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.Service
{
    public class adv_c_ChanelService
    {
        /// <summary>
        /// 提交复制数据信息
        ///     拓展
        ///         记录当前设备、IP、时间、即微信号等相关信息
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <param name="AccountName"></param>
        /// <returns></returns>
        public DBResultInfo PushData(string ChannelID, string AccountName)
        {
            ParCollection par = new ParCollection();
            par.Add("@ChannelID", ChannelID)
                .Add("@AccountName", AccountName);
            return DBHelper.GetProcModel<DBResultInfo>("adv_Save_PushData", par);
        }
        /// <summary>
        /// 访问信息数据信息
        ///     拓展
        ///         记录当前设备、IP、时间、即微信号等相关信息
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <param name="AccountName"></param>
        /// <returns></returns>
        public DBResultInfo Request(string ChannelID)
        {
            ParCollection par = new ParCollection();
            par.Add("@ChannelID", ChannelID);
            return DBHelper.GetProcModel<DBResultInfo>("adv_Save_Request", par);
        }
    }
}
