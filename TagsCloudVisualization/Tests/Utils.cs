using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;

namespace TagsCloudVisualization.Tests
{
    public static class Utils
    {
        public static string SaveImageAsTestSample(TestContext context, TextWriter textWriter, Bitmap image, int rectCount, string folder)
        {
            var filename = $"{context.Test.Name}_{rectCount}rects_{Guid.NewGuid():N}.png";
            var path = Path.Combine(context.TestDirectory, folder);
            Directory.CreateDirectory(path);
            path = Path.Combine(path, filename);

            image.Save(path, ImageFormat.Png);
            textWriter.WriteLine($"Test image was saved at {path}");
            return path;
        }
    }
}
