using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Algorithms;

namespace TagsCloudVisualization.Layouters
{
    public class CircularCloudLayouter: ICloudLayouter
    {
        private readonly ILayouterAlgorithm algorithm;

        public List<Rectangle> Rectangles { get; }

        public List<Rectangle> NormalizedRectangles => GetNormalizedRectangles();

        private List<Rectangle> GetNormalizedRectangles()
        {
            var maze = Maze;
            var xStride = -maze.X;
            var yStride = -maze.Y;
            return Rectangles.Select(r => new Rectangle(r.X + xStride, r.Y + yStride, r.Width, r.Height)).ToList();
        }

        public Rectangle Maze => GetMaze();

        public Point Center { get; }

        public CircularCloudLayouter(Point center, ILayouterAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            Center = center;
            Rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var newRectangle = algorithm.FindSpaceForRectangle(Center, Rectangles, rectangleSize);
            if (newRectangle != Rectangle.Empty)
            {
                Rectangles.Add(newRectangle);
            }

            return newRectangle;
        }

        private Rectangle GetMaze()
        {
            if (!Rectangles.Any())
            {
                return Rectangle.Empty;
            }

            var top = Rectangles.Min(rectangle => rectangle.Top);
            var bottom = Rectangles.Max(rectangle => rectangle.Bottom);
            var left = Rectangles.Min(rectangle => rectangle.Left);
            var right = Rectangles.Max(rectangle => rectangle.Right);
            return new Rectangle(left, top, Math.Abs(right - left), Math.Abs(bottom - top));
        }
    }
}
