using CommandLine;
using DocOps.Helpers;
using DocOps.Options;
using DocOps.Results;
using NLog;
using NLog.Targets;

namespace DocOps.Cli
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var logFileLocation = CreateLog(Directory.GetCurrentDirectory());
            ExecuteCommandLine(args);
        }

        private static string CreateLog(string currentPath)
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logConsole = new ColoredConsoleTarget("logconsole")
            {
                Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}",
                DetectConsoleAvailable= true,
                DetectOutputRedirected = true,
                EnableAnsiOutput = true,
                UseDefaultRowHighlightingRules = true,
                
            };

            var logFileName = Path.Combine(currentPath, $"logs/log_{DateTime.Now:yyMMdd}.txt");
            var logFile = new FileTarget("logfile")
            {
                FileName = logFileName,
                ArchiveAboveSize = 5000000,
                MaxArchiveFiles = 10,
                MaxArchiveDays = 5
            };

            var logErrorFile = new FileTarget("errorLogfile")
            {
                FileName = Path.Combine(currentPath, $"logs/ErrorLog_{DateTime.Now:yyMMdd}.txt"),
                ArchiveAboveSize = 5000000,
                MaxArchiveFiles = 10,
                MaxArchiveDays = 5
            };

            config.AddRule(LogLevel.Debug, LogLevel.Warn, logFile);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logErrorFile);
            config.AddRule(LogLevel.Warn, LogLevel.Fatal, logConsole);

            LogManager.Configuration = config;

            return logFileName;
        }

        public static CommandResult ExecuteCommandLine(string[] args)
        {
            bool isHuman = false;


            CommandResult commandResult = null;


            Console.WriteLine("Initializing DocOps...");
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide the command: init or generate.");
                var command = Console.ReadLine();

                if (command == "init" || command == "generate")
                {
                    isHuman = true;
                    args = new[] { command };
                }
            }

            Logger.Info($"Executing DocOps with command {string.Join(" ", args)}.");

            Parser.Default.ParseArguments<InitOptions, GenerateOptions>(args)
                .MapResult<InitOptions, GenerateOptions, CommandResult>(
                    (InitOptions opts) =>
                    {
                        DocumentGeneratorHelper.InitCommand(opts, Logger);
                        return CommandResult.SuccessResult("DocOps initialized successfully.");
                    },
                    (GenerateOptions opts) => { 
                        DocumentGeneratorHelper.GenerateCommand(opts); 
                        return CommandResult.SuccessResult("Documentation generated successfully.");
                    },
                    errs =>
                    {
                        Logger.Error($"Invalid command. Please use 'init' or 'generate'. {errs.ToString()}");
                        commandResult = CommandResult.ErrorResult("Invalid command. Please use 'init' or 'generate'", "");
                        return commandResult;
                    }
                );


            if (isHuman)
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            }

            if(commandResult == null) commandResult = CommandResult.ErrorResult("Invalid command. Please use 'init' or 'generate'", "");
            return commandResult;
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("DocOps CLI - Command-Line Interface for DocOps");
            Console.WriteLine("Usage:");
            Console.WriteLine("  docops init       Initialize DocOps in the current project directory.");
            Console.WriteLine("  docops generate   Generate documentation for the project.");
            Console.WriteLine("  docops --help     Display this help message.");
            Console.WriteLine();
        }
    }
}
