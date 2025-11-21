using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestGenerator.Models;

namespace TestGenerator.Services
{
    public class PromptBuilder
    {
        private readonly string _templateRoot;

        public PromptBuilder(string templateRoot)
        {
            _templateRoot = templateRoot ?? throw new ArgumentNullException(nameof(templateRoot));
        }

        public IReadOnlyList<PromptChunk> BuildPromptChunks(CodeContext context, string testType, int chunkSize)
        {
            var templatePath = Path.Combine(_templateRoot, $"{testType}TestPrompt.txt");
            if (!File.Exists(templatePath))
                throw new FileNotFoundException("Prompt template not found.", templatePath);

            chunkSize = Math.Max(chunkSize, 1000);
            var template = File.ReadAllText(templatePath);
            var sections = BuildSections(context).ToList();

            if (!sections.Any())
                sections.Add((context.Classes.FirstOrDefault()?.Name ?? "UnknownClass", "No code context supplied."));

            var payloads = new List<(string Prompt, string Scope, List<string> Names)>();
            var chunkBuilder = new StringBuilder();
            var chunkNames = new List<string>();

            foreach (var section in sections)
            {
                var normalizedSection = section.Content;
                var willOverflow = chunkBuilder.Length + normalizedSection.Length > chunkSize && chunkBuilder.Length > 0;
                if (willOverflow)
                {
                    payloads.Add(CreatePayload(template, context, chunkBuilder.ToString(), chunkNames));
                    chunkBuilder.Clear();
                    chunkNames.Clear();
                }

                chunkBuilder.Append(normalizedSection);
                chunkNames.Add(section.Name);
            }

            if (chunkBuilder.Length > 0)
            {
                payloads.Add(CreatePayload(template, context, chunkBuilder.ToString(), chunkNames));
            }

            var total = payloads.Count;
            var chunks = new List<PromptChunk>(total);
            for (var i = 0; i < payloads.Count; i++)
            {
                var payload = payloads[i];
                chunks.Add(new PromptChunk
                {
                    Index = i + 1,
                    Total = total,
                    Prompt = payload.Prompt,
                    ScopeDescription = payload.Scope,
                    ClassNames = payload.Names.ToArray()
                });
            }

            return chunks;
        }

        private static (string Prompt, string Scope, List<string> Names) CreatePayload(
            string template,
            CodeContext context,
            string chunkContent,
            List<string> chunkNames)
        {
            var builder = new StringBuilder(template);
            builder.Replace("{FilePath}", context.FilePath);
            builder.Replace("{Namespace}", context.Namespace);
            builder.Replace("{IsWindowsForms}", context.IsWindowsFormsFile.ToString());
            builder.Replace("{ClassSummaries}", chunkContent);

            var scope = chunkNames.Count > 0 ? string.Join(", ", chunkNames) : context.FilePath;
            return (builder.ToString(), scope, new List<string>(chunkNames));
        }

        private static IEnumerable<(string Name, string Content)> BuildSections(CodeContext context)
        {
            foreach (var @class in context.Classes)
            {
                var builder = new StringBuilder();
                builder.AppendLine($"Class: {@class.FullName}");
                builder.AppendLine($"BaseType: {@class.BaseType}");
                builder.AppendLine($"Dependencies: {string.Join(", ", @class.Dependencies)}");
                builder.AppendLine("Methods:");

                foreach (var method in @class.Methods)
                {
                    builder.AppendLine($"- {method.Signature}");
                    builder.AppendLine(method.Body);
                }

                builder.AppendLine("Source:");
                builder.AppendLine(@class.SourceCode);
                builder.AppendLine();

                yield return (@class.Name, builder.ToString());
            }

            if (context.IsWindowsFormsFile && context.UIControls.Any())
            {
                var builder = new StringBuilder();
                builder.AppendLine("UI Controls:");
                foreach (var control in context.UIControls)
                {
                    builder.AppendLine($"- {control.Name} ({control.Type}) Visible={control.IsVisible} Enabled={control.IsEnabled}");
                }

                yield return ("UIControls", builder.ToString());
            }
        }
    }
}