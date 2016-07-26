using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GR.Core.Security
{
    /// <summary>
    /// AES加密
    /// </summary>
    /// <remarks>
    /// AES 是一个新的可以用于保护电子数据的加密算法。明确地说，AES 是一个迭代的、对称密钥分组的密码，它可以使用128、192 和 256 位密钥，并且用 128 位（16字节）分组加密和解密数据。与公共密钥密码使用密钥对不同，对称密钥密码使用相同的密钥加密和解密数据。通过分组密码返回的加密数据 的位数与输入数据相同
    /// </remarks>
    public sealed class AESEncryptProvider
    {
        private static readonly string _key = "grgqyaniin~@#$%^";//必须是16位

        //默认密钥向量
        private static readonly byte[] _iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string Encrypt(string input, Encoding encoding)
        {
            //分组加密算法
            SymmetricAlgorithm aes = Aes.Create();
            var inputByteArray = encoding.GetBytes(input);
            //设置密钥及密钥向量
            aes.Key = encoding.GetBytes(_key);
            aes.IV = _iv;
            byte[] cipherBytes = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cipherBytes = ms.ToArray();//得到加密后的字节数组
                    //cs.Close();
                    //ms.Close();
                }
            }
            return Convert.ToBase64String(cipherBytes);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            return Encrypt(input, Encoding.UTF8);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns></returns>
        public static string Decrypt(string input, Encoding encoding)
        {
            var cipherBytes = Convert.FromBase64String(input);
            SymmetricAlgorithm aes = Aes.Create();
            aes.Key = encoding.GetBytes(_key);
            aes.IV = _iv;
            var decryptBytes = new byte[cipherBytes.Length];
            using (var ms = new MemoryStream(cipherBytes))
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    cs.Read(decryptBytes, 0, decryptBytes.Length);
                    //cs.Close();
                    //ms.Close();
                }
            }
            return encoding.GetString(decryptBytes).Replace("\0", "");//将字符串后尾的'\0'去掉
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            return Decrypt(input, Encoding.UTF8);
        }
    }
}
