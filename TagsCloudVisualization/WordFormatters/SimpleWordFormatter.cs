using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.WordFormatters
{
    public class SimpleWordFormatter: IWordFormatter
    {
        public string GetFormatted(string originalWord)
        {
            return WhitespaceCropping(originalWord);
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
