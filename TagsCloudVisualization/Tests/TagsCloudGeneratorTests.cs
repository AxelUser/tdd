using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Bogus;
using NUnit.Framework;
using SimpleInjector;
using TagsCloudVisualization.Algorithms;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Readers;
using TagsCloudVisualization.Visualization;
using TagsCloudVisualization.WordFormatters;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class TagsCloudGeneratorTests
    {
        private TagsCloudGenerator generator;
        private string inputFile;
        private string outputFile;

        [SetUp]
        public void SetUp()
        {
            var container = new Container();
            container.RegisterSingleton<ILayouterAlgorithm>(() => new SimpleRadialAlgorithm());
            container.RegisterSingleton<ICloudLayouter>(() =>
                new CircularCloudLayouter(new Point(1024, 1024), container.GetInstance<ILayouterAlgorithm>()));
            container.RegisterSingleton<IWordsReader, SimpleTxtReader>();
            container.RegisterSingleton<ILayoutPainter, SimpleLayoutPainter>();
            container.RegisterCollection<IWordFormatter>(new SimpleWordFormatter());
            container.RegisterSingleton<WordsNormalizer>();
            container.RegisterSingleton<WordsLayouter>();
            container.RegisterSingleton<TagsCloudGenerator>();

            container.Verify();

            generator = container.GetInstance<TagsCloudGenerator>();

            inputFile = CreateInputFile();
        }

        [TearDown]
        public void TearDown()
        {
            //DeleteFile(inputFile);
        }

        [Test]
        public void GetImage_ReturnsCorrectImage()
        {
            var ctx = TestContext.CurrentContext;
            outputFile = Path.Combine(ctx.TestDirectory, "TestFiles", $"{ctx.Test.Name}_{Guid.NewGuid():N}.png");
            generator.SaveCloud(inputFile, 2048, 2048, outputFile, ImageExtensions.Png);
        }

        private string CreateInputFile()
        {            
            if (!string.IsNullOrEmpty(inputFile) && File.Exists(inputFile))
            {
                File.Delete(inputFile);
            }
            var c = TestContext.CurrentContext;

            var directory = Path.Combine(c.TestDirectory, "TestFiles");
            Directory.CreateDirectory(directory);

            var filepath = Path.Combine(directory, $"{c.Test.Name}_{Guid.NewGuid():N}.txt");            
            var faker = new Faker();
            var words = faker.Random.WordsArray(100).Distinct(StringComparer.OrdinalIgnoreCase);

            using (var sw = new StreamWriter(File.Create(filepath)))
            {
                foreach (var word in words)
                {
                    var times = faker.Random.Int(30, 100);
                    for (int i = 0; i < times; i++)
                    {
                        sw.WriteLine(word);
                    }
                }
            }

            TestContext.WriteLine($"File with words created at {filepath}");
            return filepath;
        }

        private void DeleteFile(string file)
        {
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                File.Delete(file);
                TestContext.WriteLine($"File was deleted at {file}");
            }
        }
    }
}
