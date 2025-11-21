namespace TestGenerator.Models
{
    public class TestGenerationRequest
    {
        public CodeContext Context { get; set; }
        public string TestType { get; set; } // unit | integration | ui
        public string TargetProjectPath { get; set; }
    }
}