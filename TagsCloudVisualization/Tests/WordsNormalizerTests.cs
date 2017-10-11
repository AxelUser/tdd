using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
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
        [Ignore("Not implemented")]
        public void NormalizeToWordsSizes_PassWords_GetExpectedSizes()
        {
            
        }

    }
}
