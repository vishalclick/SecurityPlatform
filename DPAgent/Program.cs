// See https://aka.ms/new-console-template for more information
using DPAgent.Audit;
using DPAgent.Classification;
using DPAgent.Extraction;
using DPAgent.Models;
using DPAgent.Reports;
using DPAgent.Risk;
using DPAgent.Scanner;

Console.WriteLine("Hello, World!");
Console.WriteLine("SUDARSHAN – Sensitive Data Audit System");

var scanner = new FileScanner();
var classifier = new ClassificationManager();
var riskEngine = new RiskEngine();
var audit = new AuditLogger();

string reportPath = ReportsUtil.GetReportFilePath("DPAgent","Reports.csv");
var report = new CsvReport(reportPath);
var imageExtractor = new ImageExtractor();
var pdfExtractor = new PdfExtractor();

var results = new List<ScanResult>();
string path = args.Length > 0 ? args[0] : ".";
path = Console.ReadLine();
audit.Log("Scan started");

foreach (var file in scanner.ScanDirectory(path))
{
    try
    {
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

        if (dataTypes.Count == 0) continue;

        var hash = scanner.ComputeHash(file);
        var risk = riskEngine.CalculateRisk(dataTypes);

        results.Add(new ScanResult
        {
            Source = file,
            SourceType = IsImage(file) ? "IMAGE" :
                         IsPdf(file) ? "PDF" : "TEXT",
            FileHash = hash,
            DetectedDataTypes = dataTypes,
            Risk = risk
        });
    }
    catch { /* skip unreadable files */ }
}

audit.Log("Scan completed");
report.WriteReport(results);

Console.WriteLine("Scan complete. Report generated.");