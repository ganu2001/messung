using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace TestGenerator.Services
{
    public class CodeValidator
    {
        public ValidationResult Validate(string sourceCode)
        {
            try
            {
                var scriptOptions = ScriptOptions.Default
                    .AddReferences("System", "System.Core", "System.Windows.Forms", "FluentAssertions", "Moq", "xunit")
                    .AddImports("System", "System.Linq");

                CSharpScript.Create(sourceCode, scriptOptions).Compile();
                return ValidationResult.Success();
            }
            catch (CompilationErrorException ex)
            {
                return ValidationResult.Fail(string.Join("\n", ex.Diagnostics));
            }
        }
    }

    public record ValidationResult(bool IsValid, string Message)
    {
        public static ValidationResult Success() => new(true, string.Empty);
        public static ValidationResult Fail(string message) => new(false, message);
    }
}