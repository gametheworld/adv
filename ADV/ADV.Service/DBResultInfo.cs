using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADV.Service
{
    public class DBResultInfo
    {
        public int Status { get;set;}
        public string Msg { get; set; }
    }
    public class PurchaseDBResultInfo : DBResultInfo
    {
        public string UserName { get; set; }
        public string CardID { get; set; }
        public string TotalAmount { get; set; }
        public string ProductName { get; set; }
        public string Equity { get; set; }
        private string _ProductEndTime;
        public string ProductEndTime 
        {
            set { _ProductEndTime = value; }
            get { return Convert.ToDateTime(_ProductEndTime).ToString("yyyy年MM月dd号"); }
        }
        public string ProductPredictAmount { get; set; }
        private string _InvestTime;
        public string InvestTime 
        {
            set { _InvestTime = value; }
            get { return Convert.ToDateTime(_InvestTime).ToString("yyyy年MM月dd号"); }
        }
        public string CreateDate { get { return DateTime.Now.ToString("yyyy-MM-dd"); } }

        /// <summary>
        /// 用户在e签宝用户ID
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 用户签章图片
        /// </summary>
        public string SealData { get; set; }
        /// <summary>
        /// 预览合同
        /// </summary>
        public string ContractPreviewPath { get; set; }
        /// <summary>
        /// 平台敲章合同
        /// </summary>
        public string ContractSignPath { get; set; }
    }
}
