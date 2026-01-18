using DPAgent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Reports
{
    public class CsvReport
    {
        private readonly string _outputPath;

        public CsvReport(string outputPath)
        {
            _outputPath = outputPath;
        }

        public void WriteReport(List<ScanResult> results)
        {
            var sb = new StringBuilder();

            // CSV Header
            sb.AppendLine(
                "FilePath,FileHash,SourceType,IsSensitive,DetectedDataTypes,RiskScore,RiskLevel,RiskReasons");

            foreach (var result in results)
            {
                sb.AppendLine(ToCsvLine(result));
            }

            File.WriteAllText(_outputPath, sb.ToString(), Encoding.UTF8);
        }

        private string ToCsvLine(ScanResult result)
        {
            string dataTypes = string.Join("|", result.DetectedDataTypes);

            string reasons = result.Risk?.Reasons != null
                ? string.Join("|", result.Risk.Reasons)
                : "";

            return string.Join(",",
                Escape(result.Source),
                Escape(result.FileHash),
                Escape(result.SourceType),
                result.IsSensitive,
                Escape(dataTypes),
                result.Risk?.RiskScore ?? 0,
                result.Risk?.Level.ToString() ?? "NONE",
                Escape(reasons)
            );
        }

        private string Escape(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "\"\"";

            // Escape quotes for CSV
            value = value.Replace("\"", "\"\"");

            return $"\"{value}\"";
        }
    }
}
