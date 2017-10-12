using System;
using System.Collections.Generic;
using System.Drawing;
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

        public List<Tuple<string, int, Rectangle>> GetWordsLayout(Dictionary<string, int> wordsSizes, int minFontSize = 0)
        {
            var wordsContainers = new List<Tuple<string, int, Rectangle>>();

            foreach (var wordSize in wordsSizes)
            {
                var fontSize = wordSize.Value < minFontSize ? minFontSize : wordSize.Value;
                var geoSizeF = GetSizeForWord(wordSize.Key, fontSize);
                var geoSize = Size.Round(geoSizeF);
                var rect = layouter.PutNextRectangle(geoSize);
                if (!rect.IsEmpty)
                {
                    wordsContainers.Add(Tuple.Create(wordSize.Key, fontSize, rect));
                }
            }

            wordsContainers.Sort((t1, t2) => t1.Item2 > t2.Item2 ? 1 : t1.Item2 < t2.Item2 ? -1 : 0);
            return wordsContainers;
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
