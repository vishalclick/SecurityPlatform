using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Risk
{
    public class RiskResult
    {
        public int RiskScore { get; set; }
        public RiskLevel Level { get; set; }
        public List<string> Reasons { get; set; }
    }
}
