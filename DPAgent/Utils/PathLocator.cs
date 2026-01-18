using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Utils
{
    public static class PathLocator
    {
        // Get the path to a common application data folder. 
        // LocalApplicationData maps to %LOCALAPPDATA% on Windows and ~/.local/share on Linux.
        static string BaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static string appName = "DPAgent";
        static string ReportsFolderName = "Reports";
        static string ScanStoreFolderName = "ScanStore";
        static string ReportsFileName = "Reports.csv";
        static string ScanStoreFileName = "ScanStore.json";
        public static string GetDirectoryPath(string component)
        {
            // Ensure the directory is created if it doesn't exist. This is crucial for Linux.
            string reportDirectory = Path.Combine(BaseDirectory, appName, component);

            // The application is responsible for creating the directory.
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }

            return reportDirectory;
        }

        public static string GetReportFilePath()
        {
            return Path.Combine(GetDirectoryPath(ReportsFolderName), ReportsFileName);
        }

        public static string GetScanStoreFilePath()
        {
            return Path.Combine(GetDirectoryPath(ScanStoreFolderName), ScanStoreFileName);
        }
    }
}
