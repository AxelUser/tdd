using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization
{
    public class WordsLayouter
    {
        private readonly ICloudLayouter layouter;

        public WordsLayouter(ICloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        public List<Tuple<string, int, Rectangle>> GetWordsLayout(Dictionary<string, int> wordsSizes, out Rectangle maze, int minFontSize = 0)
        {
            var sizesTuples = wordsSizes.Select(s => Tuple.Create(s.Key, s.Value)).ToList();

            sizesTuples.Sort((t1, t2) => t1.Item2 > t2.Item2 ? -1 : t1.Item2 < t2.Item2 ? 1 : 0);

            foreach (var tuple in sizesTuples)
            {
                var fontSize = tuple.Item2 < minFontSize ? minFontSize : tuple.Item2;
                var geoSizeF = GetSizeForWord(tuple.Item1, fontSize);
                var geoSize = Size.Round(geoSizeF);
                layouter.PutNextRectangle(geoSize);
            }
            var wordsContainer =
                layouter.NormalizedRectangles.Zip(sizesTuples, (rct, tp) => Tuple.Create(tp.Item1, tp.Item2, rct))
                    .ToList();

            maze = layouter.Maze;
            return wordsContainer;
        }

        private SizeF GetSizeForWord(string word, int fontSize)
        {                        
            using (var g = Graphics.FromImage(new Bitmap(100, 100)))
            {
                Font wordFont = new Font("Arial", fontSize);
                return g.MeasureString(word, wordFont);
            }
        }
    }
}
