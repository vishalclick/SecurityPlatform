using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class DataClassifier
    {
        private readonly Regex AadhaarRegex =
            new Regex(@"\b\d{12}\b");

        private readonly Regex PanRegex =
            new Regex(@"\b[A-Z]{5}[0-9]{4}[A-Z]\b");

        public string Classify(string content)
        {
            if (AadhaarRegex.IsMatch(content))
                return "AADHAAR";

            if (PanRegex.IsMatch(content))
                return "PAN";

            return "NONE";
        }
    }
}
