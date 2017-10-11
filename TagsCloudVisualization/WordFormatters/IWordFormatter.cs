using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.WordFormatters
{
    public interface IWordFormatter
    {
        string GetFormatted(string originalWord);
    }
}
