using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DocOps.Options
{
    public class GenerateOptions
    {
        public GenerateOptions() { }

        [Option('g', "generate", Required = false, HelpText = "Generate the documentation.")]
        public bool Generate { get; set; }
    }
}
