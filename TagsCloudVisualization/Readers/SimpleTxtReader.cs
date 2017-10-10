using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Readers
{
    public class SimpleTxtReader: IWordsReader
    {
        public string[] SupportedFileExtensions { get; } = {"txt"};

        public bool CanHandle(string filename)
        {
            var fileInfo = new FileInfo(filename);
            return fileInfo.Exists &&
                   SupportedFileExtensions.Contains(fileInfo.Extension, StringComparer.OrdinalIgnoreCase);

        }

        public List<string> GetAllWords(string filepath)
        {
            if (!CanHandle(filepath))
            {
                return null;
            }

            var words = new List<string>();
            using (var sr = new StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    words.Add(sr.ReadLine());
                }
            }
            return words;
        }
    }
}
