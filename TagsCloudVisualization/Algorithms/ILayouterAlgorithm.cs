using System.Drawing;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Algorithms
{
    public interface ILayouterAlgorithm
    {
        Rectangle FindSpaceForRectangle(ICloudLayouter layouter, Size rectangleSize);
    }
}
