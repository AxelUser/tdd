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
            var newRectangle = new Rectangle(Center, rectangleSize);
            Rectangles.Add(newRectangle);
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
