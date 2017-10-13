using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Layouters;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class WordsLayouterTests
    {
        private ICloudLayouter fakeLayouter;
        private WordsLayouter wordsLayouter;

		private readonly Dictionary<string, int> sampleWordsSizes = new Dictionary<string, int>()
	    {
		    {"css", 89},
		    {"html", 38},
		    {"csharp", 140},
		    {"php", 38},
		    {"datamining", 2},
		    {"wpf", 1},
		    {"python", 50},
		    {"tensorflow", 1}
	    };

		[SetUp]
        public void SetUp()
        {
            fakeLayouter = A.Fake<ICloudLayouter>();
            A.CallTo(() => fakeLayouter.PutNextRectangle(A<Size>.Ignored))
                .ReturnsLazily((Size size) => new Rectangle(Point.Empty, size));
            A.CallTo(() => fakeLayouter.NormalizedRectangles)
                .Returns(Enumerable.Repeat(Rectangle.Empty, sampleWordsSizes.Count).ToList());

            wordsLayouter = new WordsLayouter(fakeLayouter);
        }


        [Test]
        public void GetWordsLayout_PassWords_AddedToDictionary()
        {
            Rectangle maze;
	        wordsLayouter.GetWordsLayout(sampleWordsSizes, out maze).Select(t=>t.Item1)
		        .Should().BeEquivalentTo(sampleWordsSizes.Keys);
        }
	}
}
