using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Models
{
    public class DeviceInfo
    {
        public string DeviceId { get; set; }       // Stable ID
        public string Hostname { get; set; }
        public string OperatingSystem { get; set; }
        public string OSVersion { get; set; }
        public string IPAddress { get; set; }
        public string LoggedInUser { get; set; }
    }
}
