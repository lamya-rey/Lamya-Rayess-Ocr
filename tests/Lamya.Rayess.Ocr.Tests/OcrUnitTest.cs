using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Lamya.Rayess.Ocr;
using Xunit;

namespace Lamya.Rayess.Ocr.Tests;

public class UnitTest1
{
    [Fact]
    public async Task ImagesShouldBeReadCorrectly()
    {
        var executingPath = GetExecutingPath();
        var images = new List<byte[]>();
        foreach (var imagePath in
            Directory.EnumerateFiles(Path.Combine(executingPath, "images")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            images.Add(imageBytes);
        }

        var ocrResults =  await new Ocr().ReadAsync(images);
        
        Assert.Equal("Comme toujours les fourchettes sont\nlarges et nous retrouvons l'écart région\nparisienne et province. Nous sommes\n", ocrResults[0].Text);
        Assert.InRange(ocrResults[0].Confidence, 0.80, 1.0);
        Assert.Equal("rémunération est assez Variable selon le\ncandidat et le recruteur. En gros, quand\non sort de reconversion, on démarre entre\n", ocrResults[1].Text);
        Assert.InRange(ocrResults[1].Confidence, 0.80, 1.0);
        Assert.Equal("Des entreprises, particulièrement les ESN,\npeuvent être prêtes à proposer des bonus\nélevés, des conditions de travail\n", ocrResults[2].Text);
        Assert.InRange(ocrResults[2].Confidence, 0.80, 1.0);
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath =
            Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}