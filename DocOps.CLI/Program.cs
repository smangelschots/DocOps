// See https://aka.ms/new-console-template for more information
using CommandLine;
using DocOps;
using DocOps.Helpers;

Console.WriteLine("Hello, World!");




Parser.Default.ParseArguments<InitOptions, GenerateOptions>(args)
             .MapResult(
                 (InitOptions opts) => DocumentGeneratorHelper.InitCommand(opts),
                 (GenerateOptions opts) => DocumentGeneratorHelper.GenerateCommand(opts),
                 errs => 1);




