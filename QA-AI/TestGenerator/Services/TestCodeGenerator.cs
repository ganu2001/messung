using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestGenerator.Models;

namespace TestGenerator.Services
{
    public class TestCodeGenerator
    {
        private readonly ILLMService _llmService;
        private readonly PromptBuilder _promptBuilder;
        private readonly CodeValidator _validator;
        private readonly dynamic _options;

        public TestCodeGenerator(
            ILLMService llmService,
            PromptBuilder promptBuilder,
            CodeValidator validator,
            object options)
        {
            _llmService = llmService;
            _promptBuilder = promptBuilder;
            _validator = validator;
            _options = options;
        }

        public void GenerateTests(CodeContext context)
        {
            var testType = ResolveTestType(context);
            var promptChunks = _promptBuilder.BuildPromptChunks(context, testType, (int)_options.ChunkSize);

            var accumulatedCode = string.Empty;
            foreach (var chunk in promptChunks)
            {
                Console.WriteLine($"    Sending chunk {chunk.Index}/{chunk.Total} ({chunk.ScopeDescription})...");
                var response = Task.Run(() => _llmService.GenerateTextAsync(chunk.Prompt)).GetAwaiter().GetResult();
                accumulatedCode += ExtractCode(response) + Environment.NewLine + Environment.NewLine;
            }

            if (!_options.SkipValidation)
            {
                var diagnostics = _validator.Validate(accumulatedCode);
                if (!diagnostics.IsValid)
                    throw new InvalidOperationException($"Generated code failed validation: {diagnostics.Message}");
            }

            var fileName = $"{context.Classes[0].Name}{testType}Tests.cs";
            var targetFolder = Path.Combine(_options.OutputRoot, testType switch
            {
                "unit" => "Unit",
                "integration" => "Integration",
                "ui" => "UI",
                _ => "Unit"
            });

            Directory.CreateDirectory(targetFolder);
            var path = Path.Combine(targetFolder, fileName);
            File.WriteAllText(path, accumulatedCode.Trim());
            Console.WriteLine($"  ✔ Saved {fileName} -> {targetFolder}");
        }

        private static string ResolveTestType(CodeContext context)
        {
            if (context.IsWindowsFormsFile) return "ui";
            return context.Classes.Count > 1 ? "integration" : "unit";
        }

        private static string ExtractCode(string response)
        {
            var fenceMatch = Regex.Match(response, "```csharp(?<code>.*?)```", RegexOptions.Singleline);
            if (fenceMatch.Success)
                return fenceMatch.Groups["code"].Value.Trim();

            return response.Trim();
        }
    }
}