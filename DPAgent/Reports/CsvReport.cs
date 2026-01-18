using DPAgent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Reports
{
    public class CsvReport
    {
        public void Generate(List<ScanResult> results)
        {
            var sb = new StringBuilder();
            sb.AppendLine("FilePath,Hash,DataType,Risk");

            foreach (var r in results)
            {
                sb.AppendLine($"{r.FilePath},{r.FileHash},{r.DataType},{r.RiskLevel}");
            }

            File.WriteAllText("sudarshan_report.csv", sb.ToString());
        }
    }
}
