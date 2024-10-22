using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DocOps.Models;
using DocOps.Options;
using NLog;

namespace DocOps.Helpers
{
    public class DocumentGeneratorHelper
    {


        public static ApplicationConfiguration ApplicationConfiguration { get; set; }



        public static ApplicationConfiguration LoadConfiguration(string fullPathAndName)
        {
            var jsonConfig = File.ReadAllText(fullPathAndName);
            return JsonSerializer.Deserialize<ApplicationConfiguration>(jsonConfig);
        }



        public static int InitCommand(InitOptions opts, Logger logger)
        {
            Console.WriteLine("Initializing DocOps...");


            string newProjectName = "Default";
            string defaultPath = @"c:\temp";
            string defaultConfigFileName = "DocOpsConfiguration.json";
            string fullSavePath = Path.Combine(defaultPath, "DocOps");

            string fullConfigPath = Path.Combine(fullSavePath, defaultConfigFileName);

            if (Path.Exists(fullSavePath) == false)
            {
                logger.Info($"Create directory {fullSavePath}");
                Directory.CreateDirectory(fullSavePath);
            }
            if (File.Exists(fullConfigPath) == false)
            {
                var projectConfiguration = new ProjectConfiguration
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = newProjectName,
                    Path = fullSavePath,
                    OutputFormat = OutputFormat.HTML
                };
                var applicationConfiguration = new ApplicationConfiguration
                {
                    Id = Guid.NewGuid().ToString(),
                    Path = fullSavePath,
                    SelectedProject = newProjectName,
                    ProjectConfigurations = new List<ProjectConfiguration> { projectConfiguration }
                };
                var jsonConfig = JsonSerializer.Serialize(applicationConfiguration);

                logger.Info($"Save config file {fullConfigPath}");
                File.WriteAllText(fullConfigPath, jsonConfig);
            }
            else
            {
                logger.Info($"Load config file {fullSavePath}");
                ApplicationConfiguration = LoadConfiguration(fullConfigPath);
            }

            Console.WriteLine("DocOps initialized successfully.");
            Console.WriteLine($"Config: {fullConfigPath}");

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
