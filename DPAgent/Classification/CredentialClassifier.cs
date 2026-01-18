using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class CredentialClassifier : IDataClassifier
    {
        public string Name => "CREDENTIALS";

        private Regex password =
            new Regex(@"password\s*[:=]\s*\S+", RegexOptions.IgnoreCase);

        private Regex apiKey =
            new Regex(@"(api[_-]?key|token)\s*[:=]\s*\S+",
                RegexOptions.IgnoreCase);

        public bool IsMatch(string content)
        {
            return password.IsMatch(content) || apiKey.IsMatch(content);
        }
    }
}
