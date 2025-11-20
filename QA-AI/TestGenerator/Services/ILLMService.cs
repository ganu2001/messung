using System.Threading.Tasks;

namespace TestGenerator.Services
{
    public interface ILLMService
    {
        Task<string> GenerateTextAsync(string prompt);
    }
}