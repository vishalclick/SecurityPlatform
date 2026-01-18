using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Risk
{
    public class RiskEngine
    {
        public string CalculateRisk(string dataType)
        {
            return dataType switch
            {
                "AADHAAR" => "CRITICAL",
                "PAN" => "HIGH",
                _ => "LOW"
            };
        }
    }
}
