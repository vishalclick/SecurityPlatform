using DPAgent.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace DPAgent.Scanner
{
    public static class DeviceInfoCollector
    {
        public static DeviceInfo Collect()
        {
            return new DeviceInfo
            {
                DeviceId = GetStableDeviceId(),
                Hostname = Environment.MachineName,
                OperatingSystem = RuntimeInformation.OSDescription,
                OSVersion = Environment.OSVersion.VersionString,
                IPAddress = GetLocalIPAddress(),
                LoggedInUser = Environment.UserName
            };
        }

        private static string GetStableDeviceId()
        {
            string rawId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? GetWindowsMachineGuid()
                : GetLinuxMachineId();

            return Hash(rawId);
        }

        #region Windows

        private static string GetWindowsMachineGuid()
        {
            try
            {
                using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography");
                return key?.GetValue("MachineGuid")?.ToString() ?? "UNKNOWN";
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        #endregion

        #region Linux

        private static string GetLinuxMachineId()
        {
            try
            {
                if (File.Exists("/etc/machine-id"))
                    return File.ReadAllText("/etc/machine-id").Trim();
            }
            catch { }

            return "UNKNOWN";
        }

        #endregion

        private static string Hash(string input)
        {
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToHexString(bytes);
        }

        private static string GetLocalIPAddress()
        {
            try
            {
                return Dns.GetHostEntry(Dns.GetHostName())
                    .AddressList
                    .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?
                    .ToString();
            }
            catch
            {
                return "UNKNOWN";
            }
        }
    }
}
