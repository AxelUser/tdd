using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Algorithms
{
    public interface ILayouterAlgorithm
    {
        Rectangle FindSpaceForRectangle(Rectangle maze, Point center, List<Rectangle> existingRectangles, Size rectangleSize);
    }
}
