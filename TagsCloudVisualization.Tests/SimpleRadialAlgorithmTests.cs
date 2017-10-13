using System;
using System.Collections.Generic;
using System.Drawing;
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

        [Test]
        public void FindSpaceForRectangle_AddRectangle_ReturnsRectangle()
        {
            var center = new Point(25, 25);
            var exisingRectangles = new List<Rectangle>();

            var rectangleSize = new Size(50, 50);

            Assert.AreNotEqual(Rectangle.Empty,
                algorithm.FindSpaceForRectangle(center, exisingRectangles, rectangleSize));
        }

        [TestCase(30, 50)]
        [TestCase(50, 30)]
        public void FindSpaceForRectangle_AddedRectangleTwice_ReturnsTwoRectangles(int rectWidth, int rectHeight)
        {
            var center = new Point(rectWidth, rectHeight);

            var rectangleSize = new Size(rectWidth, rectHeight);

            var firstRectangle = algorithm.FindSpaceForRectangle(center, new List<Rectangle>(), rectangleSize);
            var secondRectangle =
                algorithm.FindSpaceForRectangle(center, new List<Rectangle>() {firstRectangle}, rectangleSize);
            
            Assert.AreEqual(rectangleSize, firstRectangle.Size);
            Assert.AreEqual(rectangleSize, secondRectangle.Size);
        }

        [TestCase(30, 50)]
        [TestCase(50, 30)]
        public void FindSpaceForRectangle_AddedRectangleTwice_RectanglesDoesNotOverlap(int rectWidth, int rectHeight)
        {
            var center = new Point(rectWidth, rectHeight);
            var maze = new Rectangle(0, 0, center.X * 2, center.Y * 2);

            var rectangleSize = new Size(rectWidth, rectHeight);

            var firstRectangle = algorithm.FindSpaceForRectangle(center, new List<Rectangle>(), rectangleSize);
            var secondRectangle =
                algorithm.FindSpaceForRectangle(center, new List<Rectangle>() { firstRectangle }, rectangleSize);

            Assert.IsFalse(firstRectangle.IntersectsWith(secondRectangle), "Rectangles must not overlap");
        }
    }
}
