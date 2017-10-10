using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ILayouterAlgorithm
    {
        Rectangle FindSpaceForRectangle(ICloudLayouter layouter, Size rectangleSize);
    }
}
