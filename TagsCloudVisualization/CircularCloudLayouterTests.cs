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
            var center = new Point(x, y);
            Assert.Throws<ArgumentException>(() => new CircularCloudLayouter(center));
        }

        [TestCase(8, 10)]
        public void Ctor_CreateMaze_CorrectSizes(int x, int y)
        {
            var center = new Point(x, y);
            var cloud = new CircularCloudLayouter(center);
            Assert.AreEqual(cloud.Maze.Height, y * 2);
            Assert.AreEqual(cloud.Maze.Width, x * 2);
        }
    }
}
