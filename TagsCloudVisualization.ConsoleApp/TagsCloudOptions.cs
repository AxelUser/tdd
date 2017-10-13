using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace TagsCloudVisualization.ConsoleApp
{
    public class TagsCloudOptions
    {
        [Option('i', "inputFile", Required = true, HelpText = "Input file with words for tags cloud")]
        public string InputFile { get; set; }

        [Option('w', "width", Required = false, HelpText = "Width of output image")]
        public int Width { get; set; }

        [Option('h', "heigth", Required = false, HelpText = "Height of output image")]
        public int Height { get; set; }

        [Option('o', "outputFile", Required = false, HelpText = "Output file with words for tags cloud")]
        public string OutputFile { get; set; }

        [Option('f', Default = ImageExtensions.Png)]
        public ImageExtensions Format { get; set; }
    }
}
