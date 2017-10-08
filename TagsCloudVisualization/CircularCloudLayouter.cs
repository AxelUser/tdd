using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public Point Center { get; }
        public Rectangle Maze { get; }

        public List<Rectangle> Rectangles { get; }


        public CircularCloudLayouter(Point center)
        {
            if (center.X < 0 || center.Y < 0)
            {
                throw new ArgumentException("Coordinates of center must be positive numbers", nameof(center));
            }
            Center = center;
            Maze = CreateMaze(center);
            Rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Width > Maze.Width || rectangleSize.Height > Maze.Height)
            {
                throw new ArgumentException("Size is too big to fit maze", nameof(rectangleSize));
            }

            var newRectangle = FindPlace(rectangleSize);
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

        private Rectangle FindPlace(Size rectangleSize)
        {
            bool hasSpace = true;
            int radius = 0;
            while (hasSpace)
            {
                for (int a = 0; a < 360; a++)
                {
                    var point = GetCoordinates(Center, a, radius);
                    if (Maze.Contains(point) && !Rectangles.ContainsPoint(point))
                    {
                        var newRect = new Rectangle(point, rectangleSize);
                        if (Maze.Contains(newRect) && !Rectangles.IntersectsWith(newRect))
                        {
                            return newRect;
                        }
                    }
                }
                hasSpace = radius < Maze.Height || radius < Maze.Width;
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
                X = (int) Math.Round(center.X + radius * Math.Cos(angle)),
                Y = (int) Math.Round(center.Y + radius * Math.Sin(angle))
            };
        }
    }
}
