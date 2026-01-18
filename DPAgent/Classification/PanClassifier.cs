using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class PanClassifier : IDataClassifier
    {
        public string Name => "PAN";

        private Regex regex =
            new Regex(@"\b[A-Z]{5}[0-9]{4}[A-Z]\b");

        public bool IsMatch(string content)
        {
            return regex.IsMatch(content);
        }
    }
}
