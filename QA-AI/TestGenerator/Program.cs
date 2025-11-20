using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TestGenerator.Models;
using TestGenerator.Services;

namespace TestGenerator
{
    internal static class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("=== AI QA Framework - Test Generator (Phase 3) ===");
            Console.WriteLine();

            try
            {
                var options = Options.Parse(args);
                var contexts = LoadCodeContexts(options.ContextPath);

                var llmOptions = new LLMOptions
                {
                    Provider = options.Provider,
                    Model = options.Model,
                    ApiKey = options.ApiKey ?? Environment.GetEnvironmentVariable("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"),
                    Temperature = options.Temperature
                };

                ILLMService llmService = new OpenAIService(llmOptions);
                var promptBuilder = new PromptBuilder(options.TemplateDirectory);
                var codeValidator = new CodeValidator();
                var generator = new TestCodeGenerator(llmService, promptBuilder, codeValidator, options);

                foreach (var context in contexts)
                {
                    Console.WriteLine($"Generating tests for {context.FilePath}...");
                    generator.GenerateTests(context);
                }

                Console.WriteLine();
                Console.WriteLine("Test generation completed.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Fatal error: {ex.Message}");
                Console.Error.WriteLine(ex.StackTrace);
                return 1;
            }
        }

        private static List<CodeContext> LoadCodeContexts(string contextPath)
        {
            if (!File.Exists(contextPath))
                throw new FileNotFoundException("CodeAnalyzer context file not found.", contextPath);

            var json = File.ReadAllText(contextPath);
            return JsonConvert.DeserializeObject<List<CodeContext>>(json) ?? new List<CodeContext>();
        }

        private sealed class Options
        {
            public string ContextPath { get; private set; } = "code-context.json";
            public string OutputRoot { get; private set; } = @"..\..\tests";
            public string TemplateDirectory { get; private set; } = @"Templates";
            public string Provider { get; private set; } = "OpenAI";
            public string Model { get; private set; } = "gpt-4.1";
            public string ApiKey { get; private set; }
            public double Temperature { get; private set; } = 0.1;
            public bool SkipValidation { get; private set; }
            public int ChunkSize { get; private set; } = 5500;

            public static Options Parse(string[] args)
            {
                var options = new Options();

                for (var i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "--context":
                        case "-c":
                            options.ContextPath = args[++i];
                            break;
                        case "--output":
                        case "-o":
                            options.OutputRoot = args[++i];
                            break;
                        case "--templates":
                            options.TemplateDirectory = args[++i];
                            break;
                        case "--provider":
                            options.Provider = args[++i];
                            break;
                        case "--model":
                            options.Model = args[++i];
                            break;
                        case "--apikey":
                            options.ApiKey = args[++i];
                            break;
                        case "--temperature":
                            options.Temperature = double.Parse(args[++i]);
                            break;
                        case "--skip-validation":
                            options.SkipValidation = true;
                            break;
                        case "--chunk-size":
                            options.ChunkSize = int.Parse(args[++i]);
                            break;
                        case "--help":
                        case "-h":
                            ShowHelp();
                            Environment.Exit(0);
                            break;
                    }
                }

                return options;
            }

            private static void ShowHelp()
            {
                Console.WriteLine("TestGenerator usage:");
                Console.WriteLine("  TestGenerator.exe --context <path> --output <folder> [options]");
                Console.WriteLine();
                Console.WriteLine("Options:");
                Console.WriteLine("  -c, --context         CodeAnalyzer JSON (default: code-context.json)");
                Console.WriteLine("  -o, --output          Root folder for generated tests");
                Console.WriteLine("      --templates       Prompt template directory (default: Templates)");
                Console.WriteLine("      --provider        LLM provider name (default: OpenAI)");
                Console.WriteLine("      --model           LLM model id (default: gpt-4.1)");
                Console.WriteLine("      --apikey          Explicit API key (fallback: env var)");
                Console.WriteLine("      --temperature     Sampling temperature (default: 0.1)");
                Console.WriteLine("      --chunk-size      Max characters per prompt chunk (default: 5500)");
                Console.WriteLine("      --skip-validation Disable local Roslyn validation");
            }
        }
    }
}