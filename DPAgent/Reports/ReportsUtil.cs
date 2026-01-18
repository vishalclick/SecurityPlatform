using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Reports
{
    public static class ReportsUtil
    {
        public static string GetReportsDirectoryPath(string appName)
        {
            // Get the path to a common application data folder. 
            // LocalApplicationData maps to %LOCALAPPDATA% on Windows and ~/.local/share on Linux.
            string baseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            // Ensure the directory is created if it doesn't exist. This is crucial for Linux.
            string reportDirectory = Path.Combine(baseDirectory, appName, "Reports");

            // The application is responsible for creating the directory.
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }

            return reportDirectory;
        }

        public static string GetReportFilePath(string appName, string reportFileName)
        {
            string reportsDirectory = GetReportsDirectoryPath(appName);
            return Path.Combine(reportsDirectory, reportFileName);
        }
    }
}
