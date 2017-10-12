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

        [SetUp]
        public void SetUp()
        {
            fakeWordFormatter = A.Fake<IWordFormatter>();
            normalizer = new WordsNormalizer(new []{fakeWordFormatter});
            A.CallTo(() => fakeWordFormatter.GetFormatted(A<string>.Ignored)).ReturnsLazily((string word) => word);
        }

        [Test]        
        public void NormalizeToWordsSizes_PassWords_FormatterWillRunForEach()
        {
            var words = new[] {"css", "css", "html"};
            normalizer.NormalizeToWordsSizes(words);

            A.CallTo(() => fakeWordFormatter.GetFormatted(A<string>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Times(words.Length));
        }

        [Test]
        public void NormalizeToWordsSizes_PassWords_AddWordsToDictionary()
        {
            var words = new[] { "css", "css", "css", "css", "css", "css", "html", "html" };
            var sizes = normalizer.NormalizeToWordsSizes(words);

            sizes.Keys.Select(x => x).Should().BeEquivalentTo("css", "html");
        }

        [Test]
        public void NormalizeToWordsSizes_PassWords_GetBiggestSizeForPopularWord()
        {
            var words = new[] { "css", "css", "css", "css", "css", "css", "html", "html" };
            var sizes = normalizer.NormalizeToWordsSizes(words);

            sizes["css"].Should().BeGreaterThan(sizes["html"]);
        }


        [Test]
        public void NormalizeToWordsSizes_PassNotRepeatedWords_GetEqualSizes()
        {
            var words = new[] { "css", "wpf", "csharp", "dotnet", "windows", "chrome", "markdown", "html" };
            var sizes = normalizer.NormalizeToWordsSizes(words);

            var sampleSize = sizes["css"];
            Assert.IsTrue(sizes.Values.All(i => i == sampleSize));
        }
    }
}
