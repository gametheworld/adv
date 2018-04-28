using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;


namespace Util
{
    public class LogHelp
    {
        private volatile static LogHelp _instance = null;
        private static readonly object lockHelper = new object();
        private LogHelp() { }
        public static LogHelp CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new LogHelp();
                }
            }
            return _instance;
        }

        private static readonly ILog logger;
        private static string LogType = "";
        /// <summary>
        /// 日志管理类静态构造函数
        /// </summary>
        static LogHelp()
        {
           // LogType = System.Configuration.ConfigurationManager.AppSettings[""].ToString();
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        /// <summary>
        /// 写入日志方法
        /// </summary>
        /// <param name="sMessage">日志信息</param>
        public void Info(object sMessage)
        {
            logger.Info(sMessage);
        }
        /// <summary>
        /// 写入日志异常
        /// </summary>
        /// <param name="sMessage">日志信息</param>
        public void Error(object sMessage)
        {
            logger.Error(sMessage);
        }
        /// <summary>
        /// 写入日志方法
        /// </summary>
        /// <param name="sMessage">日志信息</param>
        /// <param name="ex">异常信息</param>
        public void Info(object sMessage, Exception ex)
        {
            logger.Error(sMessage, ex);
        }
        //test db日志
        public void Error(object sMessage, Exception e) 
        {
            logger.Error(sMessage, e);
            
        }

    }
}
