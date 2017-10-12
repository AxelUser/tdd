using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            wordsLayouter = new WordsLayouter(fakeLayouter);
        }


        [Test]
        public void GetWordsLayout_PassWords_AddedToDictionary()
        {
	        wordsLayouter.GetWordsLayout(sampleWordsSizes).Keys
		        .Should().BeEquivalentTo(sampleWordsSizes.Keys);
        }

	    [Test]
	    public void GetWordsLayout_PassWords_DoNotAddVeryBigWords()
	    {
		    A.CallTo(() => fakeLayouter.PutNextRectangle(A<Size>.Ignored)).Returns(Rectangle.Empty).Once();

		    wordsLayouter.GetWordsLayout(sampleWordsSizes).Should().HaveCount(sampleWordsSizes.Count - 1);
	    }
	}
}
