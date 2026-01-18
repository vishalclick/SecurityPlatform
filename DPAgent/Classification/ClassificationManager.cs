using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Classification
{
    public class ClassificationManager
    {
        private List<IDataClassifier> classifiers =
            new List<IDataClassifier>
            {
                new AadhaarClassifier(),
                new PanClassifier(),
                new PassportClassifier(),
                new VoterIdClassifier(),
                new FinancialClassifier(),
                new ContactClassifier(),
                new CredentialClassifier()
            };

        public List<string> Classify(string content)
        {
            return classifiers
                .Where(c => c.IsMatch(content))
                .Select(c => c.Name)
                .ToList();
        }
    }
}
