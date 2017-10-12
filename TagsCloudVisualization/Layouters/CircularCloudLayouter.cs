using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Algorithms;

namespace TagsCloudVisualization.Layouters
{
    public class CircularCloudLayouter: ICloudLayouter
    {
        private readonly ILayouterAlgorithm algorithm;

        public List<Rectangle> Rectangles { get; }

        public Rectangle Maze { get; }

        public Point Center { get; }

        public CircularCloudLayouter(Point center, ILayouterAlgorithm algorithm)
        {
            if (center.X < 0 || center.Y < 0)
            {
                throw new ArgumentException("Coordinates of center must be positive numbers", nameof(center));
            }
            this.algorithm = algorithm;
            Center = center;
            Maze = CreateMaze(center);
            Rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var newRectangle = algorithm.FindSpaceForRectangle(Maze, Center, Rectangles, rectangleSize);
            if (newRectangle != Rectangle.Empty)
            {
                Rectangles.Add(newRectangle);
            }

            return newRectangle;
        }

        private Rectangle CreateMaze(Point center)
        {
            int height = center.Y * 2;
            int width = center.X * 2;
            return new Rectangle(0, 0, width, height);
        }
    }
}
