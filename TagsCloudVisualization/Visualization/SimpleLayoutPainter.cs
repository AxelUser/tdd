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
        public Bitmap GetImage(Dictionary<string, Tuple<int, Rectangle>> wordContainers, int width, int height)
        {
            var bitmap = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
	            graphics.FillRectangle(Brushes.Azure, new Rectangle(Point.Empty, new Size(width, height)));
                foreach (var wordContainer in wordContainers)
                {
					var font = new Font("Arial", wordContainer.Value.Item1);
                    graphics.DrawString(wordContainer.Key, font, Brushes.Black, wordContainer.Value.Item2);
                    graphics.DrawRectangle(Pens.Gray, wordContainer.Value.Item2);
                }
            }
            return bitmap;
        }
    }
}
