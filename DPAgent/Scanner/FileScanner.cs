using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DPAgent.Scanner
{
    public class FileScanner
    {
        public IEnumerable<string> ScanDirectory(string path)
        {
            return Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories);
        }

        public string ComputeHash(string filePath)
        {
            using var sha = SHA256.Create();
            using var stream = File.OpenRead(filePath);
            var hash = sha.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        public string ReadSample(string filePath)
        {
            return File.ReadAllText(filePath).Length > 5000
                ? File.ReadAllText(filePath).Substring(0, 5000)
                : File.ReadAllText(filePath);
        }
    }
}
