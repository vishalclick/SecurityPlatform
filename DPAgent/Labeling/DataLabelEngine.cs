using DPAgent.Models;
using DPAgent.Risk;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Labeling
{
    public class DataLabelEngine
    {
        public List<DataLabel> ApplyLabels(ScanResult scanResult)
        {
            var labels = new List<DataLabel>();

            // No labels if no risk
            if (scanResult.Risk == null ||
                scanResult.Risk.Level == RiskLevel.LOW ||
                scanResult.Risk.Level == RiskLevel.NONE)
            {
                return labels;
            }

            // Apply labels based on detected data types
            foreach (var dataType in scanResult.DetectedDataTypes)
            {
                if (LabelPolicy.DataTypeLabels.ContainsKey(dataType))
                {
                    labels.Add(LabelPolicy.DataTypeLabels[dataType]);
                }
            }

            // Escalation labels based on risk
            if (scanResult.Risk.Level == RiskLevel.HIGH ||
                scanResult.Risk.Level == RiskLevel.CRITICAL)
            {
                labels.Add(new DataLabel
                {
                    Code = "SENSITIVE.HIGH_RISK",
                    Description = "High risk sensitive content detected"
                });
            }

            return labels;
        }
    }
}
