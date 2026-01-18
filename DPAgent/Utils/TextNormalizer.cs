using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Utils
{
    public static class TextNormalizer
    {
        public static string NormalizeForIdDetection(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            // Remove spaces, newlines, tabs
            string normalized = Regex.Replace(input, @"\s+", "");

            // Remove common OCR separators
            normalized = normalized
                .Replace("-", "")
                .Replace("_", "")
                .Replace(".", "");

            return normalized;
        }
    }
}
