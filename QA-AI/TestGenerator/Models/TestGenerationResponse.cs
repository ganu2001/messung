namespace TestGenerator.Models
{
    public class TestGenerationResponse
    {
        public bool Success { get; set; }
        public string GeneratedCode { get; set; }
        public string FileName { get; set; }
        public string Diagnostics { get; set; }
    }
}