using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ADV.DB;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
/// <summary>
/// 日志管理类
/// </summary>
public class LogMethod : DBBase
{
    private static readonly ILog logger;

    /// <summary>
    /// 日志管理类静态构造函数
    /// </summary>
    static LogMethod()
    {
        logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }

    /// <summary>
    /// 写入日志方法
    /// </summary>
    /// <param name="sMessage">日志信息</param>
    public void WriteLog(object sMessage)
    {
        logger.Info(sMessage);
    }


    /// <summary>
    /// 写入日志方法
    /// </summary>
    /// <param name="sMessage">日志信息</param>
    /// <param name="ex">异常信息</param>
    public void WriteLog(object sMessage, Exception ex)
    {

        logger.Error(sMessage, ex);
    }
    /// <summary>
    /// 写入日志方法
    /// </summary>
    /// <param name="sMessage">日志信息</param>
    /// <param name="ex">异常信息</param>
    public void WriteLogForMail(object sMessage, Exception ex)
    {
        return;
        // logger.Info(sMessage, ex);
        StringBuilder strContent = new StringBuilder();
        //System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(ex, true);

        //System.Diagnostics.StackFrame[] sfs = st.GetFrames();
        ////string stackIndent = "";
        //for (int i = 0; i < st.FrameCount; i++)
        //{
        //    System.Diagnostics.StackFrame sf = st.GetFrame(i);

        //    //得到错误的文件名
        //    strContent.Append(string.Format("<br/>  File: {0}",
        //        sf.GetFileName()));

        //    //得到错误的方法
        //    strContent.Append(string.Format("<br/> Method: {0}",
        //        sf.GetMethod().ToString()));

        //    //得到文件错误的行号
        //    strContent.Append(string.Format("<br/> Line Number: {0}",
        //        sf.GetFileLineNumber()));

        //    ////得到错误的列
        //    //strContent.Append(string.Format(" Column Number: {0}", sf.GetFileColumnNumber()));
        //    strContent.Append("<br/>");
        //}

        strContent.AppendFormat(" Message:{0} ", ex.Message);
        strContent.Append("<br/>");
        strContent.AppendFormat(" Source:{0} ", ex.Source);
        strContent.Append("<br/>");
        strContent.AppendFormat(" Method:{0} ", ex.TargetSite.Name);
        strContent.Append("<br/>");
        strContent.AppendFormat(" StackTrace:{0} ", ex.StackTrace.Replace("在", "<br/>在").Replace("at", "<br/>at"));
        strContent.Append("<br/>");

        //string emailTemplate="Message:{0}<br/>Source:{1}<br/>StackTrace:{2} <br/>Method:{3}";

        Send(strContent.ToString());
        //Send(string.Format(emailTemplate, ex.Message,
        //ex.Source,
        //ex.StackTrace,
        //ex..Name));
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="emailTo"></param>
    /// <returns></returns>
    public void Send(String content)
    {
        /*
         * admission@at0086.com
         * newsletter@at0086.com.cn
         * 211.157.101.179
         * guojiren
         */
        return;
        String emailTo = "***", subject = "网站错误日志";
        MailAddress EmailFrom = new MailAddress("***", "***");
        MailAddress EmailTo = new MailAddress(emailTo);

        MailMessage Email = new MailMessage(EmailFrom, EmailTo);


        Email.Subject = subject;
        Email.Body = content;
        Email.SubjectEncoding = System.Text.Encoding.Default;
        Email.BodyEncoding = System.Text.Encoding.UTF8;
        Email.IsBodyHtml = true;
        Email.Priority = System.Net.Mail.MailPriority.Normal;

        SmtpClient SmtpPC = new SmtpClient("***", 25);  //发送邮件服务器
        SmtpPC.DeliveryMethod = SmtpDeliveryMethod.Network;

        SmtpPC.Credentials = new System.Net.NetworkCredential(EmailFrom.Address, "***");   //用户名和密码
        //   SmtpPC.EnableSsl = true;
        //SmtpPC.UseDefaultCredentials=false;

        // try
        //  {
        SmtpPC.SendAsync(Email, null);
        // }
        // catch (Exception ex) { }
    }

}



