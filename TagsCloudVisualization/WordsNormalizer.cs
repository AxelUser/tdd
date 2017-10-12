﻿using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.WordFormatters;

namespace TagsCloudVisualization
{
    public class WordsNormalizer
    {
        private readonly IWordFormatter[] wordFormatters;

        public WordsNormalizer(IWordFormatter[] wordFormatters)
        {
            this.wordFormatters = wordFormatters;
        }

        public Dictionary<string, int> NormalizeToWordsSizes(IEnumerable<string> words)
        {            
            var formattedWords = RunAllFormatters(words);

            int maxFrequency;
            int minFrequency;
            var wordsFrequencies = GetWordsFrequencies(formattedWords, out minFrequency, out maxFrequency);

            return wordsFrequencies.ToDictionary(x => x.Key, x => GetFontSize(x.Value, minFrequency, maxFrequency));
        }

        private List<string> RunAllFormatters(IEnumerable<string> words)
        {
            return words.Select(word =>
                wordFormatters.Aggregate(word, (current, formatter) => formatter.GetFormatted(current)))
                .Where(w => !string.IsNullOrEmpty(w))
                .ToList();
        }

        private Dictionary<string, int> GetWordsFrequencies(IEnumerable<string> words, out int minFrequency, out int maxFrequency)
        {
            var frequencies = new Dictionary<string, int>();
            int min = int.MaxValue;
            int max = 0;

            foreach (var word in words)
            {
                int currentFrequency;
                if (!frequencies.TryGetValue(word, out currentFrequency))
                {
                    currentFrequency = 1;
                }
                else
                {
                    currentFrequency++;
                }

                frequencies[word] = currentFrequency;

                if (currentFrequency < min)
                {
                    min = currentFrequency;
                }
                else if(currentFrequency > max)
                {
                    max = currentFrequency;
                }
            }
            minFrequency = min;
            maxFrequency = max;

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
