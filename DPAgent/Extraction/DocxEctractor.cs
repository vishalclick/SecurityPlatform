using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DPAgent.Extraction
{
    public static class DocxExtractor
    {
        public static string ExtractText(string filePath)
        {
            var sb = new StringBuilder();

            using (WordprocessingDocument doc =
                   WordprocessingDocument.Open(filePath, false))
            {
                var body = doc.MainDocumentPart?.Document?.Body;
                if (body == null) return string.Empty;

                foreach (var text in body.Descendants<Text>())
                {
                    sb.Append(text.Text);
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }
    }
}
