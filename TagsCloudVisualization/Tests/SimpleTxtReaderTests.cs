using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TagsCloudVisualization.Readers;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class SimpleTxtReaderTests
    {
        private SimpleTxtReader simpleTxtReader;

        private string filepath;
        private readonly List<string> textLines = new List<string> {"раз", "два", "два", "три", "три", "три"};


        private string CreateTestFile(TestContext context, List<string> lines, string extension = "txt")
        {
            filepath = Path.Combine(context.TestDirectory, $"{context.Test.Name}_{Guid.NewGuid():N}.{extension}");
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            using (StreamWriter sr = new StreamWriter(filepath))
            {
                lines.ForEach(w => sr.WriteLine(w));
            }
            TestContext.WriteLine($"Have created test file at {filepath}");

            return filepath;
        }

        private void DeleteTestFile(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                TestContext.WriteLine($"Have deleted test file at {file}");
            }
        }

        [SetUp]
        public void SetUp()
        {
            simpleTxtReader = new SimpleTxtReader();
        }


        [TearDown]
        public void TearDown()
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                DeleteTestFile(filepath);
            }
        }

        [Test]
        public void CanHandle_PassTxtFile_ReturnsTrue()
        {
            filepath = CreateTestFile(TestContext.CurrentContext, textLines);
            Assert.IsTrue(simpleTxtReader.CanHandle(filepath),
                $"{nameof(SimpleTxtReader)} must handle extensions: {string.Join(", ", simpleTxtReader.SupportedFileExtensions)}");
        }

        [Test]
        public void CanHandle_PassDocxFile_ReturnsFalse()
        {
            filepath = CreateTestFile(TestContext.CurrentContext, textLines, "docx");
            Assert.IsFalse(simpleTxtReader.CanHandle(filepath),
                $"{nameof(SimpleTxtReader)} must handle extensions: {string.Join(", ", simpleTxtReader.SupportedFileExtensions)}");
        }

        [Test]
        public void CanHandle_MissingFile_ReturnsFalse()
        {
            filepath = CreateTestFile(TestContext.CurrentContext, textLines);
            DeleteTestFile(filepath);

            Assert.IsFalse(simpleTxtReader.CanHandle(filepath),
                $"{nameof(SimpleTxtReader)} must handle only exising files");
        }
    }
}
