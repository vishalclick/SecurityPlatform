using System;
using System.Collections.Generic;
using System.Text;
using UglyToad.PdfPig;

namespace DPAgent.Extraction
{
    public class PdfExtractor
    {
        public string Extract(string pdfPath)
        {
            var text = "";
            using var doc = PdfDocument.Open(pdfPath);
            foreach (var page in doc.GetPages())
            {
                text += page.Text;
            }
            return text;
        }
    }
}
