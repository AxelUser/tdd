using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter _layouter;

        private CircularCloudLayouter CreateCircularCloudLayouter(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            return new CircularCloudLayouter(center);
        }

        [TearDown]
        public void TearDown()
        {
            var state = TestContext.CurrentContext.Result.Outcome;
            if (state.Status == TestStatus.Failed && _layouter != null)
            {
                _layouter.Rectangles.ForEach(r=>TestContext.WriteLine($"Rectangle at {r}"));

                var filename = $"{Guid.NewGuid():N}.png";
                var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "MazeResults");
                Directory.CreateDirectory(path);
                path = Path.Combine(path, filename);

                var testImage = MazeVisualizer.GetImage(_layouter);

                testImage.Save(path, ImageFormat.Png);
                TestContext.WriteLine($"Test image was saved at {path}");
            }
        }

        [Test]
        public void Ctor_CorrectCenter_CreatesObject()
        {
            var center = new Point(8, 10);
            var cloud = new CircularCloudLayouter(center);
            Assert.AreEqual(cloud.Center, center);
        }

        [TestCase(-8, -10)]
        [TestCase(8, -10)]
        [TestCase(-8, 10)]
        public void Ctor_CenterWithNegCoors_ThrowsException(int x, int y)
        {
            Assert.Throws<ArgumentException>(() => CreateCircularCloudLayouter(x, y));
        }

        [TestCase(8, 10)]
        public void Ctor_CreateMaze_CorrectSizes(int x, int y)
        {
            _layouter = CreateCircularCloudLayouter(x, y);
            Assert.AreEqual(y * 2, _layouter.Maze.Height);
            Assert.AreEqual(x * 2, _layouter.Maze.Width);
        }

        [Test]
        public void PutNextRectangle_AddSmallRectangle_AppearInMaze()
        {
            _layouter = CreateCircularCloudLayouter(200, 150);            
            var rectSize = new Size(50, 50);
            _layouter.PutNextRectangle(rectSize);
            Assert.IsNotEmpty(_layouter.Rectangles);
            Assert.AreEqual(1, _layouter.Rectangles.Count);
            Assert.AreEqual(rectSize, _layouter.Rectangles.First().Size);
        }

        [Test]
        public void PutNextRectangle_AddSmallRectangle_ReturnsRectangle()
        {
            _layouter = CreateCircularCloudLayouter(200, 150);
            var rectSize = new Size(50, 50);
            var rect = _layouter.PutNextRectangle(rectSize);
            Assert.AreEqual(rect, _layouter.Rectangles.First());
        }

        [Test]
        public void PutNextRectangle_AddBigRectangle_ThrowsException()
        {
            _layouter = CreateCircularCloudLayouter(50, 50);
            var rectSize = new Size(250, 250);
            Assert.Throws<ArgumentException>(() => _layouter.PutNextRectangle(rectSize));
        }

        [Test]
        public void PutNextRectangle_AddToRectangles_MustNotCollide()
        {
            _layouter = CreateCircularCloudLayouter(250, 250);
            var rectSize = new Size(100, 50);
            var first = _layouter.PutNextRectangle(rectSize);
            var sec = _layouter.PutNextRectangle(rectSize);

            Assert.IsFalse(first.IntersectsWith(sec), "Rectangles must not overlap");
        }

        [Test]
        public void PutNextRectangle_TooMuchRectangles_DonNotAddAdd()
        {
            _layouter = CreateCircularCloudLayouter(50, 50);
            var rectSize = new Size(49, 49);
            for (int i = 0; i < 4; i++)
            {
                var newRect =_layouter.PutNextRectangle(rectSize);
                Assert.AreNotEqual(Rectangle.Empty, newRect, $"Empty rectangle on {i+1} iteration");
            }
            
            var noSpaceForThisRect = _layouter.PutNextRectangle(rectSize);
            Assert.AreEqual(4, _layouter.Rectangles.Count);
            Assert.AreEqual(Rectangle.Empty, noSpaceForThisRect);
        }
    }
}
