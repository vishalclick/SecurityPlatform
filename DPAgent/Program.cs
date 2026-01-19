// See https://aka.ms/new-console-template for more information
using DPAgent.Audit;
using DPAgent.Classification;
using DPAgent.Extraction;
using DPAgent.Labeling;
using DPAgent.Models;
using DPAgent.Reports;
using DPAgent.Risk;
using DPAgent.Scanner;
using DPAgent.Utils;

Console.WriteLine("Hello, World!");
Console.WriteLine("SUDARSHAN – Sensitive Data Audit System");

var scanner = new FileScanner();
var classifier = new ClassificationManager();
var riskEngine = new RiskEngine();
var audit = new AuditLogger();

string reportFilePath = PathLocator.GetReportFilePath();
string scanStoreFilePath = PathLocator.GetScanStoreFilePath();
var stateStore = new ScanStateStore(scanStoreFilePath);

var labelEngine = new DataLabelEngine();



var classifierFoundResults = new List<ScanResult>();
string path = args.Length > 0 ? args[0] : ".";
path = Console.ReadLine();
audit.Log("Scan started");
var deviceInfo = DeviceInfoCollector.Collect();

foreach (var file in scanner.ScanDirectory(path))
{
    try
    {
        var hash = scanner.ComputeHash(file);
        
        // SKIP LOGIC
        if (stateStore.ShouldSkipScan(hash))
        {
            Console.WriteLine($"Skipping unchanged file: {file}");
            continue;
        }

        Console.WriteLine($"Scanning {file}...");
        

        string content = ExtractionManager.GetContent(file.ToLower());        
        var dataTypes = classifier.Classify(content);


        var risk = riskEngine.CalculateRisk(dataTypes);
        FileType fileType = ExtractionManager.GetFileType(file);

        var result = new ScanResult
        {
            ScanId = Guid.NewGuid().ToString(),
            Source = file,
            SourceType = fileType.ToString(),
            FileHash = hash,
            DetectedDataTypes = dataTypes,
            Risk = risk,
            ScanTimestamp = DateTime.UtcNow
        };

        // Apply labels AFTER risk calculation
        result.Labels = labelEngine.ApplyLabels(result);
        // Store labels using ADS or sidecar fallback
        FileLabeler.StoreLabels(result);

        // RECORD SUCCESSFUL SCAN
        
        stateStore.RecordScan(hash, result);

        if (dataTypes.Count == 0) continue;

        classifierFoundResults.Add(result);

    }
    catch { /* skip unreadable files */ }
}

audit.Log("Scan completed");
JsonReport.Write(classifierFoundResults, deviceInfo, PathLocator.GetReportFilePath());

Console.WriteLine("Scan complete. Report generated.");