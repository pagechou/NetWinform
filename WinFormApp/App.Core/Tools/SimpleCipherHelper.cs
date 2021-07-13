using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Tools
{
    /// <summary>
	/// 常见的加密解密散列等
	/// </summary>
	public class SimpleCipherHelper
    {
        private static string _prefix;
        private static int MAX_LENGTH = 20971520;

        static SimpleCipherHelper()
        {
            _prefix = "encrypt:";
        }

        public SimpleCipherHelper()
        {
        }

        /// <summary>
        /// AES解密函数
        /// </summary>
        /// <param name="toDecrypt">待解密字符串</param>
        /// <returns>解密后字符串</returns>
        public static string AesDecrypt(string toDecrypt)
        {
            string str;
            if (IsEncryptString(toDecrypt))
            {
                toDecrypt = toDecrypt.Substring(_prefix.Length);
                byte[] bytes = Encoding.UTF8.GetBytes(Get16ByteMd5("A234567890123456789012345678901B"));
                byte[] numArray = Convert.FromBase64String(toDecrypt);
                ICryptoTransform cryptoTransform = (new RijndaelManaged()
                {
                    Key = bytes,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                }).CreateDecryptor();
                byte[] bt = cryptoTransform.TransformFinalBlock(numArray, 0, numArray.Length);
                str = Encoding.UTF8.GetString(bt);
            }
            else
            {
                str = toDecrypt;
            }
            return str;
        }

        /// <summary>
        /// AES加密函数
        /// </summary>
        /// <param name="toEncrypt">待加密字符串</param>
        /// <returns>加密后字符串</returns>
        public static string AesEncrypt(string toEncrypt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Get16ByteMd5("A234567890123456789012345678901B"));
            byte[] numArray = Encoding.UTF8.GetBytes(toEncrypt);
            ICryptoTransform cryptoTransform = (new RijndaelManaged()
            {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            }).CreateEncryptor();
            byte[] num = cryptoTransform.TransformFinalBlock(numArray, 0, numArray.Length);
            string str = string.Concat(_prefix, Convert.ToBase64String(num, 0, num.Length));
            return str;
        }
        /// <summary>
        /// 获取utf-16格式的字符串通过md5验证
        /// </summary>
        /// <param name="str">解密</param>
        /// <returns></returns>
        private static string Get16ByteMd5(string str)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            string upper = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(str)), 4, 8).Replace("-", "").ToUpper();
            return upper;
        }

        /// <summary>
        /// 判断是否为加密字符串
        /// </summary> 
        /// <param name="str">待加密字符串</param>
        /// <returns></returns>
        public static bool IsEncryptString(string str)
        {
            return (string.IsNullOrEmpty(str) ? false : str.StartsWith("encrypt:", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="strSource">待加密字符串</param>
        /// <returns>加密后字符串</returns>
        public static string MD5Encrypt(string strSource)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            string upper = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(strSource))).Replace("-", "").ToUpper();
            return upper;
        }

        /// <summary>
        /// MD5加密(加盐)
        /// </summary> 
        public static string MD5EncryptWithSalt(string strSource, string salt = "system")
        {
            string str = MD5Encrypt(string.Concat(SHA512Encrypt(strSource), SHA512Encrypt(salt)));
            return str;
        }
        /// <summary>
        ///  对MD5加密后的文件进行签名
        /// </summary>
        /// <param name="fileInfo">文件</param>
        /// <returns>签名值</returns>
        public static string MD5Signature(FileInfo fileInfo)
        {

            string str;
            if (fileInfo.Length <= MAX_LENGTH)
            {
                MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
                using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] numArray = mD5CryptoServiceProvider.ComputeHash(fileStream);
                    str = BitConverter.ToString(numArray).Replace("-", "");
                }
            }
            else
            {
                str = Guid.Empty.ToString("N");
            }
            return str;
        }
        /// <summary>
        /// SHA512 加密(加密的一种方式)
        /// </summary>
        /// <param name="strSource">待加密字符串</param>
        /// <returns>加密后字符串</returns>
        public static string SHA512Encrypt(string strSource)
        {
            string base64String;
            using (SHA512Managed sHA512Managed = new SHA512Managed())
            {
                byte[] numArray = sHA512Managed.ComputeHash(Encoding.Default.GetBytes(strSource));
                base64String = Convert.ToBase64String(numArray);
            }
            return base64String;
        }
    }
}