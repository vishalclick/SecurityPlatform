using System;
using System.Collections.Generic;
using System.Text;
using Tesseract;

namespace DPAgent.Extraction
{
    public static class ImageExtractor
    {
        public static string Extract(string imagePath)
        {
            using var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            using var img = Pix.LoadFromFile(imagePath);
            using var page = engine.Process(img);
            return page.GetText();
        }
    }
}
