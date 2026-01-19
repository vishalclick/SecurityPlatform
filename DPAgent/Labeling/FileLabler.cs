using DPAgent.Models;
using DPAgent.Risk;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace DPAgent.Labeling
{
    public static class FileLabeler
    {
        private const string AdsName = "SudarshanLabels";

        /// <summary>
        /// Stores labels for a file.
        /// Tries NTFS ADS first (Windows only), falls back to JSON sidecar.
        /// </summary>
        public static void StoreLabels(ScanResult scanResult)
        {
            // Skip if no risk labels
            if (scanResult.Risk == null || scanResult.Risk.Level == RiskLevel.NONE ||
                scanResult.Labels == null || scanResult.Labels.Count == 0)
                return;

            string serialized = JsonSerializer.Serialize(new
            {
                scanResult.ScanId,
                scanResult.FileHash,
                RiskLevel = scanResult.Risk.Level.ToString(),
                Labels = scanResult.Labels.ConvertAll(l => l.Code),
                Timestamp = DateTime.UtcNow
            }, new JsonSerializerOptions { WriteIndented = true });

            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && IsNtfs(scanResult.Source))
                {
                    WriteToAds(scanResult.Source, AdsName, serialized);
                }
                else
                {
                    WriteSidecar(scanResult.Source, serialized);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to store labels for {scanResult.Source}: {ex.Message}");
                // fallback to sidecar in case of ADS failure
                WriteSidecar(scanResult.Source, serialized);
            }
        }

        #region NTFS ADS Methods

        private static bool IsNtfs(string filePath)
        {
            try
            {
                var drive = Path.GetPathRoot(filePath);
                var driveInfo = new DriveInfo(drive);
                return driveInfo.DriveFormat.Equals("NTFS", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private static void WriteToAds(string filePath, string streamName, string content)
        {
            // ADS syntax: filename:streamname
            string adsPath = $"{filePath}:{streamName}";

            using var writer = new StreamWriter(adsPath, false);
            writer.Write(content);
        }

        #endregion

        #region Sidecar Methods

        private static void WriteSidecar(string filePath, string content)
        {
            string sidecarPath = filePath + ".sudarshan.json";
            File.WriteAllText(sidecarPath, content);
        }

        #endregion
    }
}
