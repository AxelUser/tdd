using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordFormatters;

namespace TagsCloudVisualization
{
    public class WordsNormalizer
    {
        private readonly IEnumerable<IWordFormatter> wordFormatters;

        public WordsNormalizer(IEnumerable<IWordFormatter> wordFormatters)
        {
            this.wordFormatters = wordFormatters;
        }

        public Dictionary<string, int> NormalizeToWordsSizes(IEnumerable<string> words)
        {            
            var formattedWords = RunAllFormatters(words);

            var wordsFrequencies = GetWordsFrequencies(formattedWords);

            int maxFrequency = wordsFrequencies.Values.Max();
            int minFrequency = wordsFrequencies.Values.Min();

            return wordsFrequencies.ToDictionary(x => x.Key, x => GetFontSize(x.Value, minFrequency, maxFrequency));
        }

        private List<string> RunAllFormatters(IEnumerable<string> words)
        {
            return words.Select(word =>
                wordFormatters.Aggregate(word, (current, formatter) => formatter.GetFormatted(current)))
                .Where(w => !string.IsNullOrEmpty(w))
                .ToList();
        }

        private Dictionary<string, int> GetWordsFrequencies(IEnumerable<string> words)
        {
            var frequencies = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!frequencies.ContainsKey(word))
                {
                    frequencies[word] = 1;
                }
                else
                {
                    frequencies[word]++;
                }
            }

            return frequencies;
        }

        private int GetFontSize(int frequency, int minFrequency, int maxFrequency)
        {
            return frequency > minFrequency
                ? maxFrequency * (frequency - minFrequency) / (maxFrequency - minFrequency)
                : 1;
        }
    }
}
