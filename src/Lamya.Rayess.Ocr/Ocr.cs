using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Tesseract;

namespace Lamya.Rayess.Ocr
{
    public class Ocr
    {
        public async Task<List<OcrResult>> ReadAsync(List<byte[]> images)
        {
            var results = new List<OcrResult>();
            foreach (var image in images)
            {
                await Task.Run(() =>
                {
                    // Code
                    var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
                    var executingPath = Path.GetDirectoryName(executingAssemblyPath);
                    using var engine = new TesseractEngine(Path.Combine(executingPath,
                        @"tessdata"), "fra", EngineMode.Default);
                    using var pix = Pix.LoadFromMemory(image);
                    var test = engine.Process(pix);
                    var Text = test.GetText();
                    var Confidence = test.GetMeanConfidence();
                    results.Add(new OcrResult {Text = Text, Confidence = Confidence});
                });
            }

            return results;
        }
    }
}