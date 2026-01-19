using DPAgent.Scanner;
using System;
using System.Collections.Generic;
using System.Text;

namespace DPAgent.Extraction
{
    public static class ExtractionManager
    {
        private static bool IsImage(string file) =>
                                    file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".tiff");

        private static bool IsPdf(string file) => file.EndsWith(".pdf");

        public static string GetContent(string file)
        {
            string content;

            FileType ext = GetFileType(file);

            switch (ext)
            {
                case FileType.DOCX:
                    content = DocxExtractor.ExtractText(file);
                    break;

                case FileType.PDF:
                    content = PdfExtractor.Extract(file);
                    break;

                case FileType.IMAGE:
                    content = ImageExtractor.Extract(file);
                    break;

                default:
                    var scanner = new FileScanner();
                    content = scanner.ReadSample(file);
                    break;
            }
            return content;
        }

        public static FileType GetFileType(string file)
        {
            string ext = Path.GetExtension(file).ToLower();
            return ext switch
            {
                ".docx" => FileType.DOCX,
                ".pdf" => FileType.PDF,
                ".png" or ".jpg" or ".jpeg" => FileType.IMAGE,
                _ => FileType.TEXT,
            };
        }
    }
}
