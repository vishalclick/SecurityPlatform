using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Risk
{
    public class RiskEngine
    {
        // Risk weights per data type (policy driven)
        private readonly Dictionary<string, int> RiskWeights =
            new Dictionary<string, int>
            {
                { "AADHAAR", 40 },
                { "PAN", 30 },
                { "PASSPORT", 35 },
                { "VOTER_ID", 25 },

                { "FINANCIAL_DATA", 40 },
                { "CREDENTIALS", 50 },

                { "CONTACT_INFO", 15 },

                { "HEALTH_DATA", 45 }
            };

        public RiskResult CalculateRisk(List<string> classifications)
        {
            int score = 0;
            var reasons = new List<string>();

            foreach (var item in classifications.Distinct())
            {
                if (RiskWeights.ContainsKey(item))
                {
                    score += RiskWeights[item];
                    reasons.Add($"{item} detected (+{RiskWeights[item]})");
                }
            }

            // Cap score at 100
            score = score > 100 ? 100 : score;

            return new RiskResult
            {
                RiskScore = score,
                Level = GetRiskLevel(score),
                Reasons = reasons
            };
        }

        private RiskLevel GetRiskLevel(int score)
        {
            if (score >= 80)
                return RiskLevel.CRITICAL;

            if (score >= 50)
                return RiskLevel.HIGH;

            if (score >= 25)
                return RiskLevel.MEDIUM;

            return RiskLevel.LOW;
        }
    }
}
