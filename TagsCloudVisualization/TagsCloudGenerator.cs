using System;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization
{
    public class TagsCloudGenerator
    {
        private readonly IWordsReader wordsReader;
        private readonly WordsNormalizer normalizer;
        private readonly ILayoutPainter layoutPainter;
        private readonly WordsLayouter wordsLayouter;

        public TagsCloudGenerator(IWordsReader wordsReader, WordsNormalizer normalizer, ILayoutPainter layoutPainter, WordsLayouter wordsLayouter)
        {
            this.wordsReader = wordsReader;
            this.normalizer = normalizer;
            this.layoutPainter = layoutPainter;
            this.wordsLayouter = wordsLayouter;
        }

        public Bitmap CreateCloud(string inputFile, int width, int height)
        {
            if (!wordsReader.CanHandle(inputFile))
            {
                throw new Exception($"Could not read file {inputFile}");
            }
            var inputWords = wordsReader.GetAllWords(inputFile);
            var wordsSizes = normalizer.NormalizeToWordsSizes(inputWords);
            Rectangle maze;
            var wordsContainers = wordsLayouter.GetWordsLayout(wordsSizes, out maze);
            return layoutPainter.GetImage(wordsContainers, maze.Width, maze.Height);
        }

        public void SaveCloud(string inputFile, int width, int height, string outputFile, ImageExtensions format)
        {
            var image = CreateCloud(inputFile, width, height);
            if (image == null)
            {
                throw new Exception("Could not create image");
            }

            image.Save(outputFile, GetImageFormat(format));
        }

        private ImageFormat GetImageFormat(ImageExtensions ext)
        {
            switch (ext)
            {
                case ImageExtensions.Png: return ImageFormat.Png;
                default: return ImageFormat.Png;
            }
        }
    }
}
