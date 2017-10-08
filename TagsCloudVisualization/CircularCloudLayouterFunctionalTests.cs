using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.IO;

namespace TagsCloudVisualization
{
    [TestFixture(Category = "Functional")]
    public class CircularCloudLayouterFunctionalTests
    {        
        private CircularCloudLayouter _layouter;

        private static Random _rnd;

        static CircularCloudLayouterFunctionalTests()
        {
            _rnd = new Random();
        }

        private static Size GetRandomSize()
        {
            int width = _rnd.Next(100, 250);
            int height = _rnd.Next(50, 200);
            return new Size(width, height);
        }

        [TearDown]
        public void TearDown()
        {
            var filename = $"{Guid.NewGuid():N}.png";
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "MazeResults");
            Directory.CreateDirectory(path);
            path = Path.Combine(path, filename);

            var testImage = MazeVisualizer.GetImage(_layouter);

            testImage.Save(path, ImageFormat.Png);
            TestContext.WriteLine($"Test image was saved at {path}");
        }


        [TestCase(30)]
        [TestCase(20)]
        [TestCase(10)]
        public void CreateMaze(int tagsCount)
        {
            _layouter = new CircularCloudLayouter(new Point(640, 360));

            Size bigTag = new Size(200, 120);
            Size smallTag = new Size(100, 70);

            for (int i = 0; i < tagsCount; i++)
            {
                _layouter.PutNextRectangle(GetRandomSize());
            }
        }
    }
}
