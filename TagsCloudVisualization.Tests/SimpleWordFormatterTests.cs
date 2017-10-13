using FluentAssertions;
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

        [TestCase("С", new []{ "С", "Про", "Это" })]
        [TestCase("с", new[] { "С", "Про", "Это" })]
        [TestCase("про", new[] { "С", "Про", "Это" })]
        [TestCase("Про", new[] { "С", "Про", "Это" })]
        [TestCase("пРо", new[] { "С", "Про", "Это" })]
        [TestCase("ЭТО", new[] { "С", "Про", "Это" })]
        [TestCase("Это", new[] { "С", "Про", "Это" })]
        public void GetFormatted_PassStopWords_ReturnsEmptyString(string originalWord, string[] stopWords)
        {
            wordFormatter = new SimpleWordFormatter(stopWords);
            wordFormatter.GetFormatted(originalWord).Should()
                .BeEmpty($"Word {originalWord} is in stop-words list: {string.Join(", ", stopWords)}");
        }

        [TestCase("Word")]
        [TestCase("UPPER")]
        [TestCase("case")]
        public void GetFormatted_PassWords_ReturnsInLowerCase(string originalWord)
        {
            wordFormatter.GetFormatted(originalWord).Should().Be(originalWord.ToLower());
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetFormatted_PassNullOrEmptyWords_ReturnsEmpty(string originalWord)
        {
            wordFormatter.GetFormatted(originalWord).Should().BeEmpty();
        }
    }
}
