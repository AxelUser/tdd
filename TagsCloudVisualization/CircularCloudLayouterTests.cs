using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter CreateCircularCloudLayouter(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            return new CircularCloudLayouter(center);
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
            var cloud = CreateCircularCloudLayouter(x, y);
            Assert.AreEqual(y * 2, cloud.Maze.Height);
            Assert.AreEqual(x * 2, cloud.Maze.Width);
        }

        [Test]
        public void PutNextRectangle_AddSmallRectangle_AppearInMaze()
        {
            var cloud = CreateCircularCloudLayouter(200, 150);            
            var rectSize = new Size(50, 50);
            cloud.PutNextRectangle(rectSize);
            Assert.IsNotEmpty(cloud.Rectangles);
            Assert.AreEqual(1, cloud.Rectangles.Count);
            Assert.AreEqual(rectSize, cloud.Rectangles.First().Size);
        }

        [Test]
        public void PutNextRectangle_AddSmallRectangle_ReturnsRectangle()
        {
            var cloud = CreateCircularCloudLayouter(200, 150);
            var rectSize = new Size(50, 50);
            var rect = cloud.PutNextRectangle(rectSize);
            Assert.AreEqual(rect, cloud.Rectangles.First());
        }
    }
}
