using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class SystemConfiguration
{
    public static string GetValues(this string str)
    {
        return System.Configuration.ConfigurationManager.AppSettings[str].ToStringExt("");
    }

    /// <summary>
    /// 项目状态系统父ID
    /// </summary>
    public static string GetProStatusList(string strValues)
    {
        return System.Configuration.ConfigurationManager.AppSettings[strValues];
    }
}
