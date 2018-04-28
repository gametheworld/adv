using System;

namespace ADV.Entity
{
    public class adv_a_GeneralizeAccount
    {

        /// <summary>
        /// 帐号编号
        /// </summary>		
        public Guid GAID { get; set; }
        /// <summary>
        /// 推官帐号
        /// </summary>		
        public string AccountName { get; set; }
        /// <summary>
        /// 复制次数
        /// </summary>		
        public int CopyCount { get; set; }
        /// <summary>
        /// 推官渠道编号
        /// </summary>		
        public Guid ChannelID { get; set; }
        /// <summary>
        /// 是否启用，默认启用
        /// </summary>		
        public bool isStart { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime CreatedTime { get; set; }

    }
}
