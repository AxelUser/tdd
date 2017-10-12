using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization
{
    public class WordsLayouter
    {
        private ICloudLayouter layouter;

        public WordsLayouter(ICloudLayouter layouter)
        {
            this.layouter = layouter;
        }

        public Dictionary<string, Rectangle> GetWordsLayout(Dictionary<string, int> wordsSizes)
        {
            var wordsContainers = new Dictionary<string, Rectangle>();

            foreach (var wordSize in wordsSizes)
            {
                var geoSize = GetSizeForWord(wordSize.Key, wordSize.Value);
            }

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
