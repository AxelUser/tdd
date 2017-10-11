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

        public string GetFormatted(string originalWord)
        {
            if (string.IsNullOrEmpty(originalWord))
            {
                return string.Empty;
            }

            var word = WhitespaceCropping(originalWord);
            word = word.ToLower();
            word = StopWordsList.Contains(word, StringComparer.OrdinalIgnoreCase) ? string.Empty : word;            
            return word;
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
