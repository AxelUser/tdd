using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Layouters
{
    public interface ICloudLayouter
    {
        List<Rectangle> Rectangles { get; }

        Rectangle Maze { get; }

        Point Center { get; }

        Rectangle PutNextRectangle(Size rectangleSize);
    }
}
