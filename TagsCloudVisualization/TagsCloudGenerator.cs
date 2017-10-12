using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy.Sdk;
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

        public Bitmap CreateCloud(string inputFile, int width, int height)
        {
            if (!wordsReader.CanHandle(inputFile))
            {
                throw new Exception($"Could not read file {inputFile}");
            }
            var inputWords = wordsReader.GetAllWords(inputFile);
            var wordsSizes = normalizer.NormalizeToWordsSizes(inputWords);
            return null;
        }

        public void SaveCloud(string inputFile, int width, int height, string outputFile)
        {
            var image = CreateCloud(inputFile, width, height);
        }
    }
}
