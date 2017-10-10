using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ICloudLayouter
    {
        List<Rectangle> Rectangles { get; }

        Rectangle Maze { get; }

        Point Center { get; }
    }
}
