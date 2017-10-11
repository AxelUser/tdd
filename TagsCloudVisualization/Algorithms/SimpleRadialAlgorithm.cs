using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Algorithms
{
    public class SimpleRadialAlgorithm : ILayouterAlgorithm
    {
        private static readonly Random Rnd = new Random();
        private readonly bool useRandomAngle;

        public SimpleRadialAlgorithm(bool useRandomAngle = true)
        {
            this.useRandomAngle = useRandomAngle;
        }

        public Rectangle FindSpaceForRectangle(Rectangle maze, Point center, List<Rectangle> existingRectangles, Size rectangleSize)
        {
            bool hasSpace = true;
            int radius = 0;
            int startAngle = useRandomAngle? Rnd.Next(0, 361): 0;
            while (hasSpace)
            {
                for (int angleOffset = 0; angleOffset < 360; angleOffset++)
                {
                    var point = GetCoordinates(center, startAngle + angleOffset, radius);
                    if (maze.Contains(point) && !existingRectangles.ContainsPoint(point))
                    {
                        var newRect = new Rectangle(point, rectangleSize);
                        if (maze.Contains(newRect) && !existingRectangles.IntersectsWith(newRect))
                        {
                            return newRect;
                        }
                    }
                }
                hasSpace = radius < maze.Height || radius < maze.Width;
                if (hasSpace)
                {
                    radius++;
                }
            }
            return Rectangle.Empty;
        }

        private Point GetCoordinates(Point center, int angle, int radius)
        {
            return new Point
            {
                X = (int)Math.Round(center.X + radius * Math.Cos(angle)),
                Y = (int)Math.Round(center.Y + radius * Math.Sin(angle))
            };
        }
    }
}
