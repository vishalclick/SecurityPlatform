using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class PassportClassifier : IDataClassifier
    {
        public string Name => "PASSPORT";

        private Regex regex =
            new Regex(@"\b[A-PR-WYa-pr-wy][0-9]{7}\b");

        public bool IsMatch(string content)
        {
            return regex.IsMatch(content);
        }
    }
}
