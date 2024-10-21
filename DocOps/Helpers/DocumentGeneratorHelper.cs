using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocOps.Models;

namespace DocOps.Helpers
{
    public class DocumentGeneratorHelper
    {

        public static int InitCommand(InitOptions opts)
        {
            Console.WriteLine("Initializing DocOps...");
            // Add your initialization logic here
            return 0;
        }


        public static int GenerateCommand(GenerateOptions opts)
        {
            // Call DocFX
            var processInfo = new ProcessStartInfo("docfx", "build")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            var process = Process.Start(processInfo);
            process.WaitForExit();
            return process.ExitCode;
        }


    }
}
