using System;
using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualization.Algorithms;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.Tests
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
            int width = _rnd.Next(50, 200);
            int height = _rnd.Next(50, 100);
            return new Size(width, height);
        }

        [TearDown]
        public void TearDown()
        {
            Utils.SaveImageAsTestSample(TestContext.CurrentContext, TestContext.Out, MazeVisualizer.GetImage(_layouter),
                _layouter.Rectangles.Count, "MazeSamples");
        }

        [TestCase(80)]
        [TestCase(70)]
        [TestCase(60)]
        [TestCase(40)]
        [TestCase(30)]
        [TestCase(20)]
        [TestCase(10)]
        public void CreateMaze(int tagsCount)
        {
            _layouter = new CircularCloudLayouter(new Point(640, 360), new SimpleRadialAlgorithm());

            for (int i = 0; i < tagsCount; i++)
            {
                _layouter.PutNextRectangle(GetRandomSize());
            }
        }
    }
}
