using DPAgent.Risk;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Models
{
    public class ScanResult
    {
        // What was scanned
        public string ScanId { get; set; }
        public string Source { get; set; }        // file name / db table / image
        public string SourceType { get; set; }    // IMAGE / PDF / DB / TEXT
        public string FileHash { get; set; }

        // Classification results
        public List<string> DetectedDataTypes { get; set; }
            = new List<string>();

        // Risk evaluation
        public RiskResult Risk { get; set; }

        // Metadata
        public bool IsSensitive => DetectedDataTypes.Count > 0;
    }
}
