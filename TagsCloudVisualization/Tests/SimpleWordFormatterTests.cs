using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TagsCloudVisualization.WordFormatters;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class SimpleWordFormatterTests
    {
        private SimpleWordFormatter wordFormatter;

        [SetUp]
        public void SetUp()
        {
            wordFormatter = new SimpleWordFormatter();
        }


        [TestCase("text will be cropped", ExpectedResult = "text")]
        [TestCase(" foo  bar ", ExpectedResult = "foo")]
        [TestCase("  ", ExpectedResult = "")]        
        public string GetFormatted_PassWordsWithWhitespaces_ReturnsWordBeforeFirstWhitespace(string originalWord)
        {
            return wordFormatter.GetFormatted(originalWord);
        }


    }
}
