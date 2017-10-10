using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Algorithms;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter _layouter;

        private CircularCloudLayouter CreateCircularCloudLayouter(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            return new CircularCloudLayouter(center, new SimpleRadialAlgorithm());
        }

        [TearDown]
        public void TearDown()
        {
            var state = TestContext.CurrentContext.Result.Outcome;
            if (state.Status == TestStatus.Failed && _layouter != null)
            {
                Utils.SaveImageAsTestSample(TestContext.CurrentContext, TestContext.Out,
                    MazeVisualizer.GetImage(_layouter), _layouter.Rectangles.Count, "MazeErrorsSamples");
            }
        }

        [Test]
        public void Ctor_CorrectCenter_CreatesObject()
        {
            var center = new Point(8, 10);
            var cloud = new CircularCloudLayouter(center, new SimpleRadialAlgorithm());
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
        public void PutNextRectangle_TooMuchRectangles_DoNotAdd()
        {
            _layouter = CreateCircularCloudLayouter(50, 50);
            var rectSize = new Size(49, 49);
            for (int i = 0; i < 4; i++)
            {
                _layouter.PutNextRectangle(rectSize);
            }
            
            var noSpaceForThisRect = _layouter.PutNextRectangle(rectSize);
            Assert.AreEqual(4, _layouter.Rectangles.Count);
            Assert.AreEqual(Rectangle.Empty, noSpaceForThisRect);
        }
    }
}
