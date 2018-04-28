using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ADV.Util.DEncrypt
{
    public class MD5_SHA1
    {
        /// <summary>
        /// 获取加密盐
        /// </summary>
        /// <param name="size">大小(24)</param>
        /// <returns></returns>
        public static string CreateSalt(int size)
        {
            //使用加密服务提供程序 (CSP) 提供的实现来实现加密随机数生成器 (RNG).
            //using System.Security.Cryptography;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            //返回Base64字符串表示形式的随机数 
            return Convert.ToBase64String(buff);
        }

        #region ---SHA1+Salt进行加密
        public static string createPwdHashSHA1(string pwd, string salt)
        {
            string saltAndPwd = string.Concat(pwd, salt);
            SHA1 sh = new SHA1CryptoServiceProvider();
            byte[] s = sh.ComputeHash(UnicodeEncoding.UTF8.GetBytes(saltAndPwd));

            return BitConverter.ToString(s).Replace("-", "");
        }
        #endregion

        #region ---MD5+Salt进行加密
        public static string createPwdHashMD5(string pwd, string salt)
        {
            string saltAndPwd = string.Concat(HashMD5(pwd), salt);

            return HashMD5(saltAndPwd);
        }


        public static string HashMD5(string str)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(str));
            return BitConverter.ToString(s).Replace("-", "").ToLower();
        }
        #endregion
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strToBeEncrypt"></param>
        /// <returns></returns>
        public static string Md5Encrypt(string strToBeEncrypt)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] FromData = System.Text.Encoding.GetEncoding("utf-8").GetBytes(strToBeEncrypt);
            Byte[] TargetData = md5.ComputeHash(FromData);
            string Byte2String = "";
            for (int i = 0; i < TargetData.Length; i++)
            {
                Byte2String += TargetData[i].ToString("x2");
            }
            return Byte2String.ToLower();
        }
    }
}
