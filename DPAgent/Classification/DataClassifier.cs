using DPAgent.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class DataClassifier
    {
        private readonly Regex AadhaarRegex =
            new Regex(@"\d{12}");

        private readonly Regex PanRegex =
            new Regex(@"[A-Z]{5}[0-9]{4}[A-Z]");

        public string Classify(string content)
        {
            // Normalize OCR / PDF / Text
            string normalized = TextNormalizer.NormalizeForIdDetection(content);

            if (AadhaarRegex.IsMatch(normalized))
            {
                var match = AadhaarRegex.Match(normalized).Value;
                if (AadhaarValidator.IsValid(match))
                    return "AADHAAR";
            }

            if (PanRegex.IsMatch(normalized))
                return "PAN";

            return "NONE";
        }
    }
}
