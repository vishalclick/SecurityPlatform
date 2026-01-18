using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Models
{
    public class ScanResult
    {
        public string FilePath { get; set; }
        public string FileHash { get; set; }
        public string DataType { get; set; }
        public string RiskLevel { get; set; }
    }
}
