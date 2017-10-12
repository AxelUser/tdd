using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Algorithms
{
    public interface ILayouterAlgorithm
    {
        Rectangle FindSpaceForRectangle(Point center, List<Rectangle> existingRectangles, Size rectangleSize);
    }
}
