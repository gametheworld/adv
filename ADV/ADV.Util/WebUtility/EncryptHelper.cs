using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace VPEA.WebUtility
{
    public class EncryptHelper
    {
        private SymmetricAlgorithm mobjCryptoService;
        private string Key;
        /// <summary>
        /// 加密解密类的构造函数
        /// </summary>
        public EncryptHelper()
        {
            mobjCryptoService = new RijndaelManaged();
            Key = "yunchuangencrypt";
        }

        public string EncryptString(string strText, string strEncrKey)
        {
            if (!string.IsNullOrEmpty(strText))
            {
                byte[] byKey = { };
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            else
            {
                return "";
            }
        }

        public string DecryptString(string strText, string sDecrKey)
        {
            if (!string.IsNullOrEmpty(strText))
            {
                byte[] byKey = { };
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byte[] inputByteArray = new byte[strText.Length];
                byKey = System.Text.Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            else
            {
                return "";
            }
        }
    }
}
