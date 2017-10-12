using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Visualization
{
    public class SimpleLayoutPainter: ILayoutPainter
    {
        public Bitmap GetImage(List<Tuple<string, int, Rectangle>> wordContainers, int width, int height)
        {
            var bitmap = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
	            graphics.FillRectangle(Brushes.Azure, new Rectangle(Point.Empty, new Size(width, height)));
                foreach (var wordContainer in wordContainers)
                {
					var font = new Font("Arial", wordContainer.Item2);
                    graphics.DrawString(wordContainer.Item1, font, Brushes.Black, wordContainer.Item3);
                    graphics.DrawRectangle(Pens.Gray, wordContainer.Item3);
                }
            }
            return bitmap;
        }
    }
}
