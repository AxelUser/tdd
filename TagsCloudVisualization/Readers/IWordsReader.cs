using System.Collections.Generic;

namespace TagsCloudVisualization.Readers
{
    public interface IWordsReader
    {
        string[] SupportedFileExtensions { get; }

        bool CanHandle(string filename);

        List<string> GetAllWords(string filepath);
    }
}
