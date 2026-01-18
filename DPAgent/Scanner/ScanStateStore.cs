using DPAgent.Risk;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DPAgent.Scanner
{
    public class ScanStateStore
    {
        private readonly string _storePath;
        private Dictionary<string, ScanStateRecord> _hashIndex;

        public ScanStateStore(string storePath)
        {
            _storePath = storePath;
            Load();
        }

        private void Load()
        {
            if (!File.Exists(_storePath))
            {
                _hashIndex = new Dictionary<string, ScanStateRecord>();
                return;
            }

            var json = File.ReadAllText(_storePath);
            _hashIndex = JsonSerializer.Deserialize<Dictionary<string, ScanStateRecord>>(json)
                         ?? new Dictionary<string, ScanStateRecord>();
        }

        public bool ShouldSkipScan(string fileHash)
        {
            if (!_hashIndex.ContainsKey(fileHash))
                return false;

            var record = _hashIndex[fileHash];

            // Skip ONLY if previous risk NONE
            return record.RiskLevel == "NONE";
        }

        public void RecordScan(string fileHash, string scanId, string riskLevel)
        {
            // Normalize null → NONE
            if (string.IsNullOrEmpty(riskLevel))
                riskLevel = "NONE";

            _hashIndex[fileHash] = new ScanStateRecord
            {
                ScanId = scanId,
                RiskLevel = riskLevel
            };

            Save();
        }

        private void Save()
        {
            var json = JsonSerializer.Serialize(
                _hashIndex,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_storePath, json);
        }
    }
}
