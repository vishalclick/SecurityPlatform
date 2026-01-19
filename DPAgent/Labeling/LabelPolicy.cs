using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Labeling
{
    public static class LabelPolicy
    {
        // DataType → Label
        public static readonly Dictionary<string, DataLabel> DataTypeLabels =
            new Dictionary<string, DataLabel>
            {
                {
                    "AADHAAR",
                    new DataLabel
                    {
                        Code = "PII.AADHAAR",
                        Description = "Contains Aadhaar Number"
                    }
                },
                {
                    "PAN",
                    new DataLabel
                    {
                        Code = "PII.PAN",
                        Description = "Contains PAN Number"
                    }
                },
                {
                    "PASSPORT",
                    new DataLabel
                    {
                        Code = "PII.PASSPORT",
                        Description = "Contains Passport Number"
                    }
                },
                {
                    "FINANCIAL_DATA",
                    new DataLabel
                    {
                        Code = "CONFIDENTIAL.FINANCIAL",
                        Description = "Contains Financial Information"
                    }
                },
                {
                    "CREDENTIALS",
                    new DataLabel
                    {
                        Code = "SECRET.CREDENTIALS",
                        Description = "Contains Credentials or Secrets"
                    }
                }
            };
    }
}
