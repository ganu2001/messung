using System;
using System.IO;
using System.Linq;
using CodeAnalyzer.Models;
using CodeAnalyzer.Services;
using Newtonsoft.Json;

namespace CodeAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== AI QA Framework - Code Analyzer ===");
            Console.WriteLine();

            try
            {
                // Parse command line arguments
                var options = ParseArguments(args);

                if (string.IsNullOrEmpty(options.FilePath))
                {
                    ShowHelp();
                    Environment.Exit(1);
                }

                if (!File.Exists(options.FilePath))
                {
                    Console.WriteLine($"Error: File not found: {options.FilePath}");
                    Environment.Exit(1);
                }

                // Analyze the file
                var analyzer = new RoslynCodeAnalyzer();
                var context = analyzer.AnalyzeFile(options.FilePath);

                // If it's a Windows Forms file, also analyze the Designer file
                if (context.IsWindowsFormsFile && !context.IsDesignerFile)
                {
                    var formsAnalyzer = new WindowsFormsAnalyzer();
                    var designerFile = formsAnalyzer.FindDesignerFile(options.FilePath);
                    
                    if (!string.IsNullOrEmpty(designerFile) && File.Exists(designerFile))
                    {
                        context.UIControls = formsAnalyzer.AnalyzeDesignerFile(designerFile);
                        Console.WriteLine($"Found {context.UIControls.Count} UI controls in Designer file.");
                    }
                }

                // Display results
                DisplayResults(context);

                // Save to JSON if output path specified
                if (!string.IsNullOrEmpty(options.OutputPath))
                {
                    var json = JsonConvert.SerializeObject(context, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    File.WriteAllText(options.OutputPath, json);
                    Console.WriteLine();
                    Console.WriteLine($"Results saved to: {options.OutputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }

        private static void DisplayResults(CodeContext context)
        {
            Console.WriteLine($"File: {context.FilePath}");
            Console.WriteLine($"Namespace: {context.Namespace ?? "N/A"}");
            Console.WriteLine($"Is Windows Forms: {context.IsWindowsFormsFile}");
            Console.WriteLine();

            if (context.Classes.Any())
            {
                Console.WriteLine($"Classes ({context.Classes.Count}):");
                Console.WriteLine(new string('-', 80));
                
                foreach (var classInfo in context.Classes)
                {
                    Console.WriteLine($"  {GetAccessModifier(classInfo)} {classInfo.Name}");
                    if (!string.IsNullOrEmpty(classInfo.BaseType))
                    {
                        Console.WriteLine($"    Base: {classInfo.BaseType}");
                    }
                    Console.WriteLine($"    Methods: {classInfo.Methods.Count}");
                    Console.WriteLine($"    Properties: {classInfo.Properties.Count}");
                    
                    if (classInfo.Methods.Any())
                    {
                        Console.WriteLine("    Method List:");
                        foreach (var method in classInfo.Methods.Where(m => m.IsPublic))
                        {
                            var parameters = string.Join(", ", method.Parameters.Select(p => $"{p.Type} {p.Name}"));
                            Console.WriteLine($"      - {method.ReturnType} {method.Name}({parameters})");
                        }
                    }
                    Console.WriteLine();
                }
            }

            if (context.UIControls.Any())
            {
                Console.WriteLine($"UI Controls ({context.UIControls.Count}):");
                Console.WriteLine(new string('-', 80));
                foreach (var control in context.UIControls)
                {
                    Console.WriteLine($"  {control.Type} {control.Name}");
                    if (!string.IsNullOrEmpty(control.Text))
                    {
                        Console.WriteLine($"    Text: {control.Text}");
                    }
                    if (control.Events.Any())
                    {
                        Console.WriteLine($"    Events: {string.Join(", ", control.Events)}");
                    }
                }
                Console.WriteLine();
            }
        }

        private static string GetAccessModifier(ClassInfo classInfo)
        {
            if (classInfo.IsPublic) return "public";
            return "internal";
        }

        private static Options ParseArguments(string[] args)
        {
            var options = new Options();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--file":
                    case "-f":
                        if (i + 1 < args.Length)
                            options.FilePath = args[++i];
                        break;
                    case "--output":
                    case "-o":
                        if (i + 1 < args.Length)
                            options.OutputPath = args[++i];
                        break;
                    case "--help":
                    case "-h":
                        ShowHelp();
                        Environment.Exit(0);
                        break;
                }
            }

            // If no -f flag, use first argument as file path
            if (string.IsNullOrEmpty(options.FilePath) && args.Length > 0 && !args[0].StartsWith("-"))
            {
                options.FilePath = args[0];
            }

            return options;
        }

        private static void ShowHelp()
        {
            Console.WriteLine("CodeAnalyzer - Analyzes C# code for AI test generation");
            Console.WriteLine();
            Console.WriteLine("Usage: CodeAnalyzer [options] <file>");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  -f, --file <path>        C# file to analyze");
            Console.WriteLine("  -o, --output <file>      Output JSON file path");
            Console.WriteLine("  -h, --help               Show this help message");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine("  CodeAnalyzer -f MyClass.cs -o output.json");
        }

        private class Options
        {
            public string FilePath { get; set; }
            public string OutputPath { get; set; }
        }
    }
}

