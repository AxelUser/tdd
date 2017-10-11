using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TagsCloudVisualization.Algorithms;

namespace TagsCloudVisualization.Tests
{

    [TestFixture]
    public class SimpleRadialAlgorithmTests
    {
        private SimpleRadialAlgorithm algorithm;

        [SetUp]
        public void SetUp()
        {
            algorithm = new SimpleRadialAlgorithm(false);
        }


        [Test, Timeout(100)]
        public void FindSpaceForRectangle_AddBigRectangle_ReturnsEmptyRectangleAtOnce()
        {
            var center = new Point(100000, 100000);
            var maze = new Rectangle(0, 0, center.X * 2, center.Y * 2);
            var exisingRectangles = new List<Rectangle>();

            var rectangleSize = new Size(30000000, 30000000);

            Assert.AreEqual(Rectangle.Empty,
                algorithm.FindSpaceForRectangle(maze, center, exisingRectangles, rectangleSize));

        }
    }
}
