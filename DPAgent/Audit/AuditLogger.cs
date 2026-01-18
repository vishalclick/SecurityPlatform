using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DPAgent.Audit
{
    public class AuditLogger
    {
        private static string logFile = "audit.log";

        public void Log(string message)
        {
            string previousHash = GetLastHash();
            string timestamp = DateTime.UtcNow.ToString("o");
            string record = $"{timestamp}|{message}|{previousHash}";
            string currentHash = ComputeHash(record);

            File.AppendAllText(logFile, $"{record}|{currentHash}\n");
        }

        private string GetLastHash()
        {
            if (!File.Exists(logFile)) return "GENESIS";

            var lastLine = File.ReadLines(logFile).Last();
            return lastLine.Split('|').Last();
        }

        private string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}
