using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SystemParamters
    {
        /// <summary>
        /// 默认空的GUID字符串
        /// </summary>
        public const string DefaultGuidString = "00000000-0000-0000-0000-000000000000";

        /// <summary>
        /// 日期时间格式(短日期)
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// 日期时间格式(yyyy-MM-dd hh:mm:ss)
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 长日期时间格式(yyyy-MM-dd HH:mm:ss:fff)
        /// </summary>
        public const string DateTimeFormat2 = "yyyy-MM-dd HH:mm:ss:fff";

        /// <summary>
        /// 长日期时间格式(yyyyMMddHHmmssfff)
        /// </summary>
        public const string DateTimeFormat3 = "yyyyMMddHHmmssfff";

        /// <summary>
        /// 默认排序号(999)
        /// </summary>
        public const int DefaultOrderIndex = 999;

        /// <summary>
        /// 默认商品购买数量:10
        /// </summary>
        public const int DefaultGoodsAmount = 10;
    }
}
