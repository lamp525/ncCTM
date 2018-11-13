using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using DevExpress.Data.Extensions;

namespace CTM.Win.Util
{
    public class NetHelper
    {
        #region Stucts

        private struct MacIpPair
        {
            public string MacAddress;
            public string IpAddress;
        }

        #endregion Stucts

        #region Utilities

        private static IList<MacIpPair> GetAllMacAddressAndIpPairs()
        {
            IList<MacIpPair> mip = new List<MacIpPair>();
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a";
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();

            string cmdOutput = pProcess.StandardOutput.ReadToEnd();
            string pattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";
            foreach (Match m in Regex.Matches(cmdOutput, pattern, RegexOptions.IgnoreCase))
            {
                mip.Add(new MacIpPair
                {
                    IpAddress = m.Groups["ip"].Value,
                    MacAddress = m.Groups["mac"].Value,
                });
            }

            return mip;
        }

        #endregion Utilities

        #region Methods

        /// <summary>
        /// Get Local Ip Address
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIpAddress()
        {
            var localIp = "?";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                    break;
                }
            }

            return localIp;
        }

        /// <summary>
        /// Get local mac address
        /// </summary>
        /// <returns></returns>
        public static string GetLocalMacAddress()
        {
            var macAddress = "?";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet || nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddress = nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }
            }

            return macAddress;
        }

        /// <summary>
        /// Get Mac Address From Local Ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetMacAddressFromLocalIp(string ip)
        {
            var macAddress = "?";
            var mip = GetAllMacAddressAndIpPairs();
            int index = mip.FindIndex(x => x.IpAddress == ip);

            if (index >= 0)
                macAddress = mip[index].MacAddress.ToString();

            return macAddress;
        }

        #endregion Methods
    }
}