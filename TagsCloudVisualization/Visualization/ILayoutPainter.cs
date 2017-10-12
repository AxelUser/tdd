using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Visualization
{
    public interface ILayoutPainter
    {
        Bitmap GetImage(Dictionary<string, Tuple<int, Rectangle>> wordContainers, int width, int height);
    }
}
