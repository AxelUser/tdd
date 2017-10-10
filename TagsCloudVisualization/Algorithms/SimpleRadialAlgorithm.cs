using System;
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

        public Rectangle FindSpaceForRectangle(ICloudLayouter layouter, Size rectangleSize)
        {
            bool hasSpace = true;
            int radius = 0;
            int startAngle = useRandomAngle? Rnd.Next(0, 361): 0;
            while (hasSpace)
            {
                for (int angleDelta = 0; angleDelta < 360; angleDelta++)
                {
                    var point = GetCoordinates(layouter.Center, startAngle + angleDelta, radius);
                    if (layouter.Maze.Contains(point) && !layouter.Rectangles.ContainsPoint(point))
                    {
                        var newRect = new Rectangle(point, rectangleSize);
                        if (layouter.Maze.Contains(newRect) && !layouter.Rectangles.IntersectsWith(newRect))
                        {
                            return newRect;
                        }
                    }
                }
                hasSpace = radius < layouter.Maze.Height || radius < layouter.Maze.Width;
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
