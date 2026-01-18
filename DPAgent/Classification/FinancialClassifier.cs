using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class FinancialClassifier : IDataClassifier
    {
        public string Name => "FINANCIAL_DATA";

        private Regex creditCard =
            new Regex(@"\b(?:\d[ -]*?){13,16}\b");

        private Regex ifsc =
            new Regex(@"\b[A-Z]{4}0[A-Z0-9]{6}\b");

        public bool IsMatch(string content)
        {
            return creditCard.IsMatch(content) || ifsc.IsMatch(content);
        }
    }
}
