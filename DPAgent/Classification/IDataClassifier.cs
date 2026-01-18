using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Classification
{
    public interface IDataClassifier
    {
        string Name { get; }
        bool IsMatch(string content);
    }
}
