using System;

namespace ADV.Entity
{
    //渠道信息表
    public class adv_c_Channel
    {

        /// <summary>
        /// 编号
        /// </summary>		
        public Guid ChannelID { get; set; }
        /// <summary>
        /// 渠道名称
        /// </summary>		
        public string ChannelName { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>		
        public Guid AccountsID { get; set; }
        /// <summary>
        /// 帐号名称
        /// </summary>		
        public string AccountsName { get; set; }
        /// <summary>
        /// 广告编号
        /// </summary>		
        public Guid ADVID { get; set; }
        /// <summary>
        /// 访问ip次数(单个IP第一次访问)
        /// </summary>		
        public int IPCount { get; set; }
        /// <summary>
        /// 复制微信次数
        /// </summary>		
        public int CopyCount { get; set; }
        /// <summary>
        /// 推官连接(生成渠道最终连接)
        /// </summary>		
        public string URL { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>		
        public Guid CreatedUserID { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>		
        public DateTime UpdateTime { get; set; }

    }
}
