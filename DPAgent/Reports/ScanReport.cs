using DPAgent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Reports
{
    public class ScanReport
    {
        public string ReportId { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string AgentVersion { get; set; }

        public DeviceInfo Device { get; set; }

        public List<ScanResult> ScanResults { get; set; }
    }
}
