using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Readers
{
    public interface IWordsReader
    {
        string[] SupportedFileExtensions { get; }

        bool CanHandle(string filename);

        List<string> GetAllWords(string filepath);
    }
}
