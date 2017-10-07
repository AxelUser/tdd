using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public static class RectangleListExtensions
    {
        public static bool IntersectsWith(this List<Rectangle> list, Rectangle rectangle)
        {
            return list.Any(r => r.IntersectsWith(rectangle));
        }

        public static bool ContainsPoint(this List<Rectangle> list, Point point)
        {
            return list.Any(r => r.Contains(point));
        }
    }
}
