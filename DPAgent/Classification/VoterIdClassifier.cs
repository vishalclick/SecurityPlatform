using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class VoterIdClassifier : IDataClassifier
    {
        public string Name => "VOTER_ID";

        private Regex regex =
            new Regex(@"\b[A-Z]{3}[0-9]{7}\b");

        public bool IsMatch(string content)
        {
            return regex.IsMatch(content);
        }
    }
}
