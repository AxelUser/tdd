using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Visualization
{
    public class SimpleLayoutPainter: ILayoutPainter
    {
        private readonly ICloudLayouter layouter;

        public SimpleLayoutPainter(ICloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        public Bitmap GetImage()
        {
            var bitmap = new Bitmap(layouter.Maze.Width, layouter.Maze.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(Brushes.Azure, layouter.Maze);
                foreach (var rectangle in layouter.Rectangles)
                {
                    graphics.FillRectangle(Brushes.DarkGoldenrod, rectangle);
                    graphics.DrawRectangle(Pens.LightSalmon, rectangle);
                }
            }
            return bitmap;
        }
    }
}
