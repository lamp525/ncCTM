﻿using System;
using System.IO;
using CTM.Core;
using CTM.Core.Util;
using CTM.Win.Models;

namespace CTM.Win.Util
{
    public class LoginHelper
    {
        private static string _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CTM");
        private static string _fileName = "87CFDD5F4EBA497C88B196ABD09AAA42.ini";

        /// <summary>
        /// 读取登录信息
        /// </summary>
        /// <returns></returns>
        public static LoginInfo Load()
        {
            LoginInfo loginInfo = new LoginInfo();

            var filePath = Path.Combine(_directoryPath, _fileName);

            if (!File.Exists(filePath))
                return null;

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            byte[] arrByte = new byte[1024];
            fs.Read(arrByte, 0, 1024);
            fs.Close();
            fs.Dispose();

            int nLength = CommonHelper.byteToInt(arrByte);
            byte[] arrEncryptByte = new byte[nLength];

            for (int i = 0; i < nLength; i++)
                arrEncryptByte[i] = arrByte[i + 4];

            loginInfo = (LoginInfo)(Serialize.DecryptToObject(arrEncryptByte));

            return loginInfo;
        }

        /// <summary>
        /// 保存登录信息
        /// </summary>
        /// <param name="loginInfo"></param>
        public static void Save(LoginInfo loginInfo)
        {
            if (!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            var filePath = Path.Combine(_directoryPath, _fileName);

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);

            byte[] arrEncryptByte = Serialize.EncryptToBytes(loginInfo);

            //将长度（整数）保存在4个元素的字节数组中
            byte[] arrLength = CommonHelper.intToByte(arrEncryptByte.Length);

            fs.Write(arrLength, 0, arrLength.Length);
            fs.Write(arrEncryptByte, 0, arrEncryptByte.Length);

            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// 获取默认设置
        /// </summary>
        /// <returns></returns>
        public static LoginInfo LoadDefault()
        {
            return new LoginInfo();
        }
    }
}