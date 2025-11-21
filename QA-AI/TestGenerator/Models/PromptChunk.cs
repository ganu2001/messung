using System.Collections.Generic;

namespace TestGenerator.Models
{
    public class PromptChunk
    {
        public int Index { get; init; }
        public int Total { get; init; }
        public string Prompt { get; init; }
        public string ScopeDescription { get; init; }
        public IReadOnlyList<string> ClassNames { get; init; }
    }
}
