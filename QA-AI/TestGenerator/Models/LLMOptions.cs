namespace TestGenerator.Models
{
    public class LLMOptions
    {
        public string Provider { get; set; }
        public string Model { get; set; }
        public string ApiKey { get; set; }
        public double Temperature { get; set; } = 0.1;
        public int MaxTokens { get; set; } = 2000;
        public int RetryCount { get; set; } = 3;
    }
}