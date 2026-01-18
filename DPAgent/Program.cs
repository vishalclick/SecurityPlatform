// See https://aka.ms/new-console-template for more information
using DPAgent.Audit;
using DPAgent.Classification;
using DPAgent.Extraction;
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
var report = new CsvReport(reportFilePath);

string scanStoreFilePath = PathLocator.GetScanStoreFilePath();
var stateStore = new ScanStateStore(scanStoreFilePath);

var imageExtractor = new ImageExtractor();
var pdfExtractor = new PdfExtractor();

var classifierFoundResults = new List<ScanResult>();
string path = args.Length > 0 ? args[0] : ".";
path = Console.ReadLine();
audit.Log("Scan started");

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
        bool IsImage(string file) =>
                                    file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".tiff");

        bool IsPdf(string file) =>  file.EndsWith(".pdf");

        string content = "";

        if (IsImage(file))
            content = imageExtractor.Extract(file);
        else if (IsPdf(file))
            content = pdfExtractor.Extract(file);
        else
            content = scanner.ReadSample(file);
        
        var dataTypes = classifier.Classify(content);


        var risk = riskEngine.CalculateRisk(dataTypes);
        var result = new ScanResult
        {
            ScanId = Guid.NewGuid().ToString(),
            Source = file,
            SourceType = IsImage(file) ? "IMAGE" :
                         IsPdf(file) ? "PDF" : "TEXT",
            FileHash = hash,
            DetectedDataTypes = dataTypes,
            Risk = risk
        };

        // RECORD SUCCESSFUL SCAN
        string? riskLevelToStore = null;

        if (result.Risk != null)
        {
            riskLevelToStore = result.Risk.Level.ToString();
        }
        stateStore.RecordScan(hash, result.ScanId, riskLevelToStore);

        if (dataTypes.Count == 0) continue;

        classifierFoundResults.Add(result);

    }
    catch { /* skip unreadable files */ }
}

audit.Log("Scan completed");
report.WriteReport(classifierFoundResults);

Console.WriteLine("Scan complete. Report generated.");