using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.WordFormatters
{
    public class SimpleWordFormatter: IWordFormatter
    {
        public List<string> StopWordsList { get; }

        public SimpleWordFormatter()
        {
            StopWordsList = new List<string>();
        }

        public SimpleWordFormatter(IEnumerable<string> stopWordsList)
        {
            StopWordsList = stopWordsList.ToList();
        }

        private Func<string, string>[] GetFormattingSteps()
        {
            return new Func<string, string>[]
            {
                WhitespaceCropping,
                word => word.ToLower(),
                word => StopWordsList.Contains(word, StringComparer.OrdinalIgnoreCase) ? string.Empty : word
            };
        }

        public string GetFormatted(string originalWord)
        {
            if (string.IsNullOrEmpty(originalWord))
            {
                return string.Empty;
            }

            return GetFormattingSteps().Aggregate(originalWord, (current, step) => step(current));
        }

        private string WhitespaceCropping(string originalWord)
        {
            var trimmedWord = originalWord.Trim();
            var whitespaceIndex = trimmedWord.IndexOf(" ", StringComparison.Ordinal);
            if (whitespaceIndex >= 0)
            {
                return trimmedWord.Substring(0, whitespaceIndex);
            }
            return trimmedWord;
        }
    }
}
