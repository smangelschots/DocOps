using CommandLine;
using System.Runtime.InteropServices;

namespace DocOps.Models
{
    [Verb("init", HelpText = "Create a new DocOps solution.")]
    public class InitOptions
    {

        [Option('i', "init", Required = false, HelpText = "Create a new DocOps solution.")]
        public bool Init { get; set; }


    }
}
