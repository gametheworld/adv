using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ADV.Util.WebUtility
{
    public class MD5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string pwd) 
        {
            MD5 MD5 = new MD5CryptoServiceProvider();
            byte[] result = Encoding.Default.GetBytes(pwd);
            byte[] output = MD5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
    }
}
