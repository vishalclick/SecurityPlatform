using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Scanner
{
    public class ScanStateRecord
    {
        public string ScanId { get; set; }
        public string? RiskLevel { get; set; }  // LOW / MEDIUM / HIGH / CRITICAL
    }
}
