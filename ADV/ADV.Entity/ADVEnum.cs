using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.Entity
{
    /// <summary>
    /// 图片验证码
    /// </summary>
    public enum ImageCode 
    {
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册")]
        Register = 1,
        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login,
        /// <summary>
        /// 绑定银行卡
        /// </summary>
        [Description("绑定银行卡")]
        BindCard,
        /// <summary>
        /// 购买理财产品
        /// </summary>
        [Description("购买理财产品")]
        BuyPurchase,
        /// <summary>
        /// 忘记密码
        /// </summary>
        [Description("忘记密码")]
        RetrievePassword,
        /// <summary>
        /// 提现
        /// </summary>
        [Description("提现")]
        Withdrawal,

    }
}
