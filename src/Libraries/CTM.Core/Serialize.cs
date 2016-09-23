using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace CTM.Core
{
    public class Serialize
    {
        // 用于初始化对称密钥
        static private string _encryptKey = "E68A1CB6-A7DB-4631-B397-3CD010C4F918";

        static private byte[] _key = Encoding.ASCII.GetBytes(_encryptKey.Substring(0, 8));
        static private byte[] _IV = Encoding.ASCII.GetBytes(_encryptKey);

        /// <summary>
        /// 将对象加密到字节数据
        /// </summary>
        /// <param name="obj">要加密的对象</param>
        /// <returns>处理后生成的字节数组</returns>
        public static byte[] EncryptToBytes(object obj)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream msPlaneText = new MemoryStream();
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(msPlaneText, obj);
                byte[] inputByteArray = msPlaneText.ToArray();
                msPlaneText.Close();
                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream cs = new CryptoStream(msEncrypt, des.CreateEncryptor(_key, _IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                byte[] byteEncrypt = msEncrypt.ToArray();
                cs.Close();
                return byteEncrypt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将字节数组进行解密还原成对象
        /// </summary>
        /// <param name="ary">要处理的字节数组</param>
        /// <returns>被还原的对象</returns>
        public static object DecryptToObject(byte[] ary)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(_key, _IV), CryptoStreamMode.Write);
                cs.Write(ary, 0, ary.Length);
                cs.FlushFinalBlock();
                cs.Close();
                byte[] byteDecrypt = ms.ToArray();
                MemoryStream msDecrypt = new MemoryStream(byteDecrypt);
                BinaryFormatter serializer = new BinaryFormatter();
                Object obj = serializer.Deserialize(msDecrypt);
                msDecrypt.Close();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}