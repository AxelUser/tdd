using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class SimpleRadialAlgorithm : ILayouterAlgorithm
    {
        public Rectangle FindSpaceForRectangle(ICloudLayouter layouter, Size rectangleSize)
        {
            bool hasSpace = true;
            int radius = 0;
            while (hasSpace)
            {
                for (int a = 0; a < 360; a++)
                {
                    var point = GetCoordinates(layouter.Center, a, radius);
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
