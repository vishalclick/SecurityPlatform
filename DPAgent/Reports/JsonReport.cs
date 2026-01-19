using DPAgent.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DPAgent.Reports
{
    public static class JsonReport
    {
        public static void Write(
            List<ScanResult> scanResults,
            DeviceInfo device,
            string outputPath)
        {
            var report = new ScanReport
            {
                ReportId = Guid.NewGuid().ToString(),
                GeneratedAt = DateTime.UtcNow,
                AgentVersion = "1.0.0",
                Device = device,
                ScanResults = scanResults
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(report, options);
            File.WriteAllText(outputPath, json);
        }
    }
}
