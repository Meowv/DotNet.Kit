using System;
using System.Security.Cryptography;
using System.Text;

namespace DotNet.Kit
{
    /// <summary>
    /// 字符串加密解密
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// 获取MD5加密值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string MD5_Encryption(string input, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input", "MD5加密的字符串不能为空！");
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var data = encoding.GetBytes(input);
            var encryData = MD5_Encryption(data);

            var builder = new StringBuilder(encryData.Length * 2);
            for (int i = 0; i < encryData.Length; i++)
            {
                builder.Append(encryData[i].ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// 获取MD5加密值
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] MD5_Encryption(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException("bytes", "MD5加密的字节不能为空！");
            }

            using (var md5Hash = MD5.Create())
            {
                return md5Hash.ComputeHash(bytes);
            }
        }

        /// <summary>
        /// 获取16位MD5值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5Hex_Encryption(string input)
        {
            var result = MD5_Encryption(input);

            return !string.IsNullOrEmpty(result) ? result.Substring(0, 16) : result;
        }

        /// <summary>
        /// 获取SHA1加密值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string SHA1_Encryption(string input, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "SHA1加密的字符串不能为空！");
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var data = encoding.GetBytes(input);
            var encryData = SHA1_Encryption(data);

            var builder = new StringBuilder(encryData.Length * 2);
            foreach (var t in encryData)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// 获取SHA1加密值
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] SHA1_Encryption(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentNullException(nameof(bytes), "SHA1加密的字符串不能为空！");
            }

            using (var sha1Hash = SHA1.Create())
            {
                return sha1Hash.ComputeHash(bytes);
            }
        }

        /// <summary>
        /// 获取Base64加密值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Base64_Encryption(string input)
        {
            return Base64_Encryption(input, Encoding.UTF8);
        }

        /// <summary>
        /// 获取Base64加密值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64_Encryption(string input, Encoding encoding = null)
        {
            var result = string.Empty;

            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Base64加密的字符串不能为空！");
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var bytes = encoding.GetBytes(input);
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                result = input;
            }
            return result;
        }

        /// <summary>
        /// 获取Base64解密值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Base64_Decrypting(string input)
        {
            return Base64_Decrypting(input, Encoding.UTF8);
        }

        /// <summary>
        /// 获取Base64解密值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Base64_Decrypting(string input, Encoding encoding = null)
        {
            var result = string.Empty;

            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input), "Base64解密的字符串不能为空！");
            }
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var bytes = Convert.FromBase64String(input);
            try
            {
                result = encoding.GetString(bytes);
            }
            catch
            {
                result = input;
            }

            return result;
        }
    }
}