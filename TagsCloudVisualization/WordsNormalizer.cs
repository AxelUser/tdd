using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.WordFormatters;

namespace TagsCloudVisualization
{
    public class WordsNormalizer
    {
        private IWordFormatter[] WordFormatters;

        public WordsNormalizer(IWordFormatter[] wordFormatters)
        {
            WordFormatters = wordFormatters;
        }
    }
}
