using System;

namespace CTM.Core.Util
{
    public class VersionHelper
    {
        /// <summary>
        /// 取得程序集版本
        /// </summary>
        /// <returns></returns>
        public static Version GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        /// <summary>
        /// 取得产品版本
        /// </summary>
        /// <returns></returns>
        public static string GetProductVersion()
        {
            return System.Windows.Forms.Application.ProductVersion;
        }

        /// <summary>
        /// 取得部署版本
        /// </summary>
        /// <returns></returns>
        public static Version GetDeploymentVersion()
        {
            try
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}