using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChangeDetector.Models;
using ChangeDetector.Services;
using Newtonsoft.Json;

namespace ChangeDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== AI QA Framework - Change Detector ===");
            Console.WriteLine();

            try
            {
                // Parse command line arguments
                var options = ParseArguments(args);
                
                List<CodeChange> changes;

                // Try Git-based detection first
                if (options.UseGit && IsGitRepository(options.RepositoryPath))
                {
                    Console.WriteLine("Using Git-based change detection...");
                    using (var detector = new GitChangeDetector(options.RepositoryPath))
                    {
                        if (options.DetectStaged)
                        {
                            changes = detector.DetectStagedChanges();
                            Console.WriteLine($"Detected {changes.Count} staged changes.");
                        }
                        else if (options.DetectWorkingDirectory)
                        {
                            changes = detector.DetectWorkingDirectoryChanges();
                            Console.WriteLine($"Detected {changes.Count} working directory changes.");
                        }
                        else
                        {
                            changes = detector.DetectChanges(options.FromCommit, options.ToCommit);
                            Console.WriteLine($"Detected {changes.Count} changes between commits.");
                        }
                    }
                }
                else
                {
                    // Fallback to file-based detection
                    Console.WriteLine("Using file-based change detection (Git not available)...");
                    var detector = new FileChangeDetector(options.RepositoryPath);
                    changes = detector.DetectChanges();
                    Console.WriteLine($"Detected {changes.Count} file changes.");
                }

                // Filter C# files only
                changes = changes.Where(c => c.IsCSharpFile && !c.IsDesignerFile).ToList();

                // Output results
                if (changes.Any())
                {
                    Console.WriteLine();
                    Console.WriteLine("Detected Changes:");
                    Console.WriteLine(new string('-', 80));
                    
                    foreach (var change in changes)
                    {
                        Console.WriteLine($"[{change.ChangeType}] {change.FilePath}");
                        if (change.ModifiedClasses.Any())
                        {
                            Console.WriteLine($"  Classes: {string.Join(", ", change.ModifiedClasses)}");
                        }
                        if (change.ModifiedMethods.Any())
                        {
                            Console.WriteLine($"  Methods: {string.Join(", ", change.ModifiedMethods)}");
                        }
                    }

                    // Save to JSON file if output path specified
                    if (!string.IsNullOrEmpty(options.OutputPath))
                    {
                        var json = JsonConvert.SerializeObject(changes, Formatting.Indented);
                        File.WriteAllText(options.OutputPath, json);
                        Console.WriteLine();
                        Console.WriteLine($"Results saved to: {options.OutputPath}");
                    }
                }
                else
                {
                    Console.WriteLine("No changes detected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }

        private static bool IsGitRepository(string path)
        {
            if (string.IsNullOrEmpty(path))
                path = Directory.GetCurrentDirectory();

            var gitPath = Path.Combine(path, ".git");
            return Directory.Exists(gitPath) || File.Exists(gitPath);
        }

        private static Options ParseArguments(string[] args)
        {
            var options = new Options
            {
                RepositoryPath = Directory.GetCurrentDirectory(),
                UseGit = true
            };

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "--path":
                    case "-p":
                        if (i + 1 < args.Length)
                            options.RepositoryPath = args[++i];
                        break;
                    case "--from":
                    case "-f":
                        if (i + 1 < args.Length)
                            options.FromCommit = args[++i];
                        break;
                    case "--to":
                    case "-t":
                        if (i + 1 < args.Length)
                            options.ToCommit = args[++i];
                        break;
                    case "--staged":
                    case "-s":
                        options.DetectStaged = true;
                        break;
                    case "--working":
                    case "-w":
                        options.DetectWorkingDirectory = true;
                        break;
                    case "--output":
                    case "-o":
                        if (i + 1 < args.Length)
                            options.OutputPath = args[++i];
                        break;
                    case "--no-git":
                        options.UseGit = false;
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
            Console.WriteLine("ChangeDetector - Detects code changes for AI test generation");
            Console.WriteLine();
            Console.WriteLine("Usage: ChangeDetector [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  -p, --path <path>        Repository or project path (default: current directory)");
            Console.WriteLine("  -f, --from <commit>      From commit hash (default: previous commit)");
            Console.WriteLine("  -t, --to <commit>        To commit hash (default: HEAD)");
            Console.WriteLine("  -s, --staged             Detect staged changes");
            Console.WriteLine("  -w, --working            Detect working directory changes");
            Console.WriteLine("  -o, --output <file>      Output JSON file path");
            Console.WriteLine("  --no-git                 Use file-based detection instead of Git");
            Console.WriteLine("  -h, --help               Show this help message");
        }

        private class Options
        {
            public string RepositoryPath { get; set; }
            public string FromCommit { get; set; }
            public string ToCommit { get; set; }
            public bool DetectStaged { get; set; }
            public bool DetectWorkingDirectory { get; set; }
            public string OutputPath { get; set; }
            public bool UseGit { get; set; }
        }
    }
}

