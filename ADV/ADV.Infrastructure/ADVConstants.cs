public static class ADVConstants
{
    public static class Localization
    {
        public static class SourceName
        {
            public const string Messages = "Messages";
            public const string Events = "Events";
            public const string Views = "Views";
            public const string Scopes = "Scopes";
        }

        public static class RootNamespace
        {
            public const string Messages = "UFO.Web.Configuration.Localization.Messages";
            public const string Events = "UFO.Web.Configuration.Localization.Events";
            public const string Views = "UFO.Web.Configuration.Localization.Views";
            public const string Scopes = "UFO.Web.Configuration.Localization.Scopes";
        }
        public static class LocalizationCategories
        {
            public const string Messages = "Messages";
            public const string Events = "Events";
            public const string Scopes = "Scopes";
            public const string Views = "Views";
        }
        public static class MessageIds
        {


            public const string OPERATION_SUCCESS = "0001";                 //操作成功

            public const string ALREADY_EXISTS_MOBILE = "";

            public const string SAVE_SUCCESS = "SAVE_SUCCESS";
            public const string SAVE_FAILED = "SAVE_FAILED";
            public const string DELETE_SUCCESS = "DELETE_SUCCESS";
            public const string DELETE_FAILED = "DELETE_FAILED";
            public const string NOT_FIND_BY_PRIMARY_KEY = "NOT_FIND_BY_PRIMARY_KEY";
            public const string IIIEFAL_REQUEST = "IIIEFAL_REQUEST";
            public const string REQUEST_ERROR = "REQUEST_ERROR";

            public const string BANK_BIND_AUTHERIZED = "BANK_BIND_AUTHERIZED";
            public const string BANK_BIND_REPEAT = "BANK_BIND_REPEAT";
            public const string BANK_BIND_AUDITING = "BANK_BIND_AUDITING";
            public const string BANK_BIND_FAILED = "BANK_BIND_FAILED";

            public const string BANK_NOT = "BANK_NOT";
        }
    }
    /// <summary>
    /// 错误码
    /// </summary>
    public static class ErrorCode
    {
        public static class System
        {
            public const string UNCAUGHT_EXCEPTIONS = "1000";               //未捕获异常
            public const string VALIDATION_PARAMETERS_FAILED = "1001";      //验证参数失败
            public const string VALIDATE_CODE_INCORRECT = "1002";           //验证码不正确

            public const string NOT_FIND_BY_PRIMARY_KEY = "1009";           //找不到主键
            public const string ILLEGAL_REQUEST = "1010";                   //非法请求
            public const string VALIDATION_SIGNATURE_FAILED = "1011";       //签名验证失败

            public const string DEVICE_NOT_LAST_VERSION = "1012";           //设备不是最后的版本


        }

        public static class User
        {
            public const string ALREADY_EXISTS_MOBILE = "2000";   
            public const string INVALID_ACCOUNT_OR_PASSWORD = "2001";
            public const string USER_NOT_EXIST = "2004";
            public const int LOGIN_EXPIRED = 2002;
            public const int NOT_EXIST_PASSWORD = 2003;
        }
        public static class Other
        {
            public const string ERROR_CAPTCHA = "3000";
            public const string ERROR_IMAGE_CAPTCHA = "3001";
            public const string MESSAGE_CAPTCHA_SEND_FAILED = "3002";
            public const string MESSAGE_CAPTCHA_SEND_SUCCESS = "3003";
            public const string MESSAGE_NOT_GET_MOBILE_CAPTCHA = "3004";
        }

    }
    /// <summary>
    /// 缓存键,请以YCJF:作为键的开始
    /// </summary>
    public static class CacheNames
    {
        public const string SystemParameter = "YCJF:System_Parameter_Table:{0}_{1}";
    }
    public static class Settings
    {
        public static class User
        {
            public const string USER_DEFAULT_AVTAR_PATH = "USER_DEFAULT_AVTAR_PATH";
        }
        public static class Other
        {
            public const string IMAGE_CAPTCHA_SESSION_NAME = "IMAGE_CAPTCHA_SESSION_NAME";
            public const string TEL_CAPTCHA_SESSION_NAME = "TEL_CAPTCHA_SESSION_NAME";
        }
        public static class Pay
        {
            public const string PAY_PUBLIC_KEY = "PAY_PUBLIC_KEY";
            public const string PAY_PAYMENT_PUBLIC_KEY = "PAY_PAYMENT_PUBLIC_KEY";
            public const string DRAW_BATCH_PAY_KEY = "DRAW_BATCH_PAY_KEY";


            public const string PAY_VERSION = "PAY_VERSION";
            public const string PAY_SUBMIT_TIME_FORMAT = "PAY_SUBMIT_TIME_FORMAT";
            public const string PAY_MERID = "PAY_MERID";
            public const string PAY_CHARSET = "PAY_CHARSET";
            public const string PAY_TRAN_CODE = "PAY_TRAN_CODE";
            public const string PAY_TRAN_CODE_CA03 = "PAY_TRAN_CODE_CA03";


            public const string PAY_BANK_BIND_URL = "PAY_BANK_BIND_URL";
            public const string PAY_BANK_BIND_NOTICE_URL = "PAY_BANK_BIND_NOTICE_URL";

            
            public const string PAYMENT_VERSION = "PAYMENT_VERSION";
            public const string PAYMENT_TYPE = "PAYMENT_TYPE";
            public const string PAY_TYPE = "PAY_TYPE";
            public const string PAY_REQUEST_URL = "PAY_REQUEST_URL";
            public const string PAYMENT_RETURN_URL = "PAYMENT_RETURN_URL";
            public const string PAYMENT_NOTICE_URL = "PAYMENT_NOTICE_URL";

            public const string PAYMENT_RECHARGE_SUCCEED_JUMP = "PAYMENT_RECHARGE_SUCCEED_JUMP";

            public const string BATCH_PAU_REQUEST = "BATCH_PAU_REQUEST";

            //快捷支付
            public const string K_TRAN_CODE = "K_TRAN_CODE";
            public const string K_VERSION = "K_VERSION";
            public const string K_CHARSET = "K_CHARSET";
            public const string K_SIGN_TYPE = "K_SIGN_TYPE";
            public const string K_RETURN_URL = "K_RETURN_URL";
            public const string K_NOTIFY_URL = "K_NOTIFY_URL";

            public const string K_PAY_REQUEST_URL = "K_PAY_REQUEST_URL";





        }
        /// <summary>
        /// e签包相关配置
        /// </summary>
        public static class EVisa
        {
            public const string PROJECT_ID = "PROJECT_ID";
            public const string PROJECT_SECRET = "PROJECT_SECRET";
            public const string APIS_URL = "APIS_URL";
            public const string HTTP_TYEPS = "HTTP_TYEPS";
            public const string ALGORITHM = "ALGORITHM";
            public const string USER_TEMPLATE_TYPE = "USER_TEMPLATE_TYPE";
        }
        /// <summary>
        /// 短信相关配置
        /// </summary>
        public static class SendCode 
        {
            public const string APP_KEY = "APP_KEY";
            public const string APP_SECRET = "APP_SECRET";
            public const string TEMPLATE_ID = "TEMPLATE_ID";
            public const string APP_URL = "APP_URL";
        }

        /// <summary>
        /// 合同
        /// </summary>
        public static class Contract 
        {

            public const string RPLACE_CHARS = "RPLACE_CHARS";
            public const string USER_CONTRACT_KEY_WORD = "USER_CONTRACT_KEY_WORD";
            public const string USER_SIGNATURE_CONTRACT_PATH = "USER_SIGNATURE_CONTRACT_PATH";
            public const string USER_CONTRACT_PREVIEW_PATH = "USER_CONTRACT_PREVIEW_PATH";

            public const string INVESTMENT_SIGNATURE_CONTRACT_PATH = "INVESTMENT_SIGNATURE_CONTRACT_PATH";
            public const string INVESTMENT_PREVIEW_CONTRACT_PATH = "INVESTMENT_PREVIEW_CONTRACT_PATH";

        }

        /// <summary>
        /// 资讯、公告、广告位相关配置
        /// </summary>
        public static class InfoAndArea
        {
            public const string INFO_INDEX_TOPAREA = "INFO_INDEX_TOPAREA";
            public const string INFO_INDEX_NEWSAREA = "INFO_INDEX_NEWSAREA";
            public const string INFO_INDEX_NEWSINFO = "INFO_INDEX_NEWSINFO";
            public const string INFO_INDEX_NOTICEINFO = "INFO_INDEX_NOTICEINFO";
        }

        public static class Resources
        {

        }
    }
}

