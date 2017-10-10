using System.Drawing;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization
{
    public static class MazeVisualizer
    {
        public static Bitmap GetImage(CircularCloudLayouter layouter)
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
