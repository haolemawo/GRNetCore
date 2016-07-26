using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GR.Core.Security
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class MD5EncryptProvider
    {
        /// <summary>
        /// 混淆输入
        /// </summary>
        private static readonly string[] _inputFormat = { "~*a{0}.(_", ",&{0}*)", "@#{0}%^&." };

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input">要需要加密的字段</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="index">混淆索引（0-2）</param>
        /// <returns></returns>
        public static string Encrypt(string input, Encoding encoding, int index)
        {
            var md5 = MD5.Create();
            var data = md5.ComputeHash(encoding.GetBytes(string.Format(_inputFormat[index], input)));
            var result = new StringBuilder();
            foreach (var b in data)
            {
                result.AppendFormat("{0:X2}", b);
            }
            return result.ToString();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input">要需要加密的字段</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            return Encrypt(input, Encoding.UTF8, 1);
        }


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param> 
        /// <returns></returns>
        public static string EncryptHash(string input)
        {
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(input), 0, input.Length);
            var temp = new char[data.Length];
            Array.Copy(data, temp, data.Length);
            return new string(temp);
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns></returns>
        public static string Encrypt(Stream stream)
        {
            var md5 = MD5.Create();
            var buffer = md5.ComputeHash(stream);
            var sb = new StringBuilder();
            foreach (var b in buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
