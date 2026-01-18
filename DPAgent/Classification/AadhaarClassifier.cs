using DPAgent.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class AadhaarClassifier : IDataClassifier
    {
        public string Name => "AADHAAR";

        private Regex regex = new Regex(@"\d{12}");

        public bool IsMatch(string content)
        {
            string normalized = TextNormalizer.NormalizeForIdDetection(content);

            var match = regex.Match(normalized);
            return match.Success && AadhaarValidator.IsValid(match.Value);
        }
    }
}
