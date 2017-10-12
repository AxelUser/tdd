using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TagsCloudVisualization.WordFormatters;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class WordsNormalizerTests
    {
        private IWordFormatter fakeWordFormatter;
        private WordsNormalizer normalizer;
        private Dictionary<string, int> wordsSizes;

        [SetUp]
        public void SetUp()
        {
            wordsSizes = new Dictionary<string, int>();
            fakeWordFormatter = A.Fake<IWordFormatter>();
            normalizer = new WordsNormalizer(new []{fakeWordFormatter});
            A.CallTo(() => fakeWordFormatter.GetFormatted(A<string>.Ignored)).ReturnsLazily((string word) => word);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var wordsSize in wordsSizes)
            {
                TestContext.WriteLine($"Word: \"{wordsSize.Key}\", Size: {wordsSize.Value}");
            }
        }


        [Test]        
        public void NormalizeToWordsSizes_PassWords_FormatterWillRunForEach()
        {
            var words = new[] {"css", "css", "html"};
            wordsSizes = normalizer.NormalizeToWordsSizes(words);

            A.CallTo(() => fakeWordFormatter.GetFormatted(A<string>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Times(words.Length));
        }

        [Test]
        public void NormalizeToWordsSizes_PassWords_AddWordsToDictionary()
        {
            var words = new[] { "css", "css", "css", "css", "css", "css", "html", "html" };
            wordsSizes = normalizer.NormalizeToWordsSizes(words);

            wordsSizes.Keys.Select(x => x).Should().BeEquivalentTo("css", "html");
        }

        [Test]
        public void NormalizeToWordsSizes_PassWords_GetBiggestSizeForPopularWord()
        {
            var words = new List<string>();
            words.AddRange(Enumerable.Repeat("css", 100));
            words.AddRange(Enumerable.Repeat("html", 60));
            words.AddRange(Enumerable.Repeat("csharp", 140));
            words.AddRange(Enumerable.Repeat("php", 60));
            words.AddRange(Enumerable.Repeat("datamining", 32));
            words.AddRange(Enumerable.Repeat("wpf", 30));
            words.AddRange(Enumerable.Repeat("python", 70));
            words.AddRange(Enumerable.Repeat("tensorflow", 30));

            wordsSizes = normalizer.NormalizeToWordsSizes(words);

            wordsSizes["csharp"]
                .Should().BeGreaterThan(wordsSizes["css"])
                .And.BeGreaterThan(wordsSizes["html"])
                .And.BeGreaterThan(wordsSizes["php"])
                .And.BeGreaterThan(wordsSizes["wpf"])
                .And.BeGreaterThan(wordsSizes["datamining"])
                .And.BeGreaterThan(wordsSizes["tensorflow"])
                .And.BeGreaterThan(wordsSizes["python"]);
        }


        [Test]
        public void NormalizeToWordsSizes_PassNotRepeatedWords_GetEqualSizes()
        {
            var words = new[] { "css", "wpf", "csharp", "dotnet", "windows", "chrome", "markdown", "html" };
            wordsSizes = normalizer.NormalizeToWordsSizes(words);

            var sampleSize = wordsSizes["css"];
            Assert.IsTrue(wordsSizes.Values.All(i => i == sampleSize));
        }
    }
}
