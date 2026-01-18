using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DPAgent.Classification
{
    public class ContactClassifier : IDataClassifier
    {
        public string Name => "CONTACT_INFO";

        private Regex phone =
            new Regex(@"\b[6-9][0-9]{9}\b");

        private Regex email =
            new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}\b");

        public bool IsMatch(string content)
        {
            return phone.IsMatch(content) || email.IsMatch(content);
        }
    }
}
