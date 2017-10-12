using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization
{
    public class TagsCloudGenerator
    {
        private ICloudLayouter cloudLayouter;
        private IWordsReader wordsReader;
        private WordsNormalizer normalizer;
        private ILayoutPainter layoutPainter;

        public TagsCloudGenerator(ICloudLayouter cloudLayouter, IWordsReader wordsReader, WordsNormalizer normalizer, ILayoutPainter layoutPainter)
        {
            this.cloudLayouter = cloudLayouter;
            this.wordsReader = wordsReader;
            this.normalizer = normalizer;
            this.layoutPainter = layoutPainter;
        }        
    }
}
