using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Extensions;

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

        public Rectangle FindSpaceForRectangle(Point center, List<Rectangle> existingRectangles, Size rectangleSize)
        {
            int radius = 0;
            int startAngle = useRandomAngle? Rnd.Next(0, 361): 0;
            while (true)
            {
                for (int angleOffset = 0; angleOffset < 360; angleOffset++)
                {
                    var point = GetCoordinates(center, startAngle + angleOffset, radius);
                    if (!existingRectangles.ContainsPoint(point))
                    {
                        var newRect = CreateRectangle(point, rectangleSize);
                        if (!existingRectangles.IntersectsWith(newRect))
                        {
                            return newRect;
                        }
                    }
                }
                radius++;
            }
        }

        private Point GetCoordinates(Point center, int angle, int radius)
        {
            return new Point
            {
                X = (int)Math.Round(center.X + radius * Math.Cos(angle)),
                Y = (int)Math.Round(center.Y + radius * Math.Sin(angle))
            };
        }

        private Rectangle CreateRectangle(Point center, Size size)
        {
            var topLeft = new Point(center.X - size.Width / 2, center.Y - size.Height / 2);
            return new Rectangle(topLeft, size);
        }
    }
}
