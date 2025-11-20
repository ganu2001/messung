# AI QA Framework - Quick Start Implementation Guide

## Prerequisites

1. **API Keys:**
   - OpenAI API key (or alternative LLM provider)
   - Store in environment variable: `OPENAI_API_KEY`

2. **Development Environment:**
   - Visual Studio 2019+ or VS Code
   - .NET Framework 4.8 SDK
   - Git

3. **NuGet Packages:**
   - See package references in project files

## Project Structure

```
QA-AI/
├── ChangeDetector/          # Detects code changes
├── CodeAnalyzer/            # Analyzes C# code using Roslyn
├── TestGenerator/           # AI-powered test generation
├── TestExecutor/            # Executes generated tests
├── Shared/                  # Shared utilities
└── Templates/               # Prompt templates
```

## Step-by-Step Setup

### Step 1: Create Test Projects

Create the following test projects in your solution:
- `XMPS2000.UnitTests`
- `XMPS2000.IntegrationTests`
- `XMPS2000.UITests`

### Step 2: Install Core Packages

In each test project, install:
```bash
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Moq
dotnet add package FluentAssertions
dotnet add package FlaUI.Core
dotnet add package FlaUI.UIA3
```

### Step 3: Set Up Change Detection

The change detector monitors Git for code changes and triggers test generation.

### Step 4: Configure LLM Integration

Set up your chosen LLM provider (OpenAI, Claude, etc.) with API key.

### Step 5: Create First Test Generation

Run the test generator on a sample class to verify the setup.

## Example: Generating Tests for a Simple Class

### Input Class (Example)
```csharp
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
    
    public int Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException();
        return a / b;
    }
}
```

### Generated Test (Example Output)
```csharp
using Xunit;
using FluentAssertions;

namespace XMPS2000.UnitTests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_WithValidInputs_ReturnsSum()
        {
            // Arrange
            var calculator = new Calculator();
            
            // Act
            var result = calculator.Add(5, 3);
            
            // Assert
            result.Should().Be(8);
        }
        
        [Fact]
        public void Divide_WithValidInputs_ReturnsQuotient()
        {
            // Arrange
            var calculator = new Calculator();
            
            // Act
            var result = calculator.Divide(10, 2);
            
            // Assert
            result.Should().Be(5);
        }
        
        [Fact]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            // Arrange
            var calculator = new Calculator();
            
            // Act & Assert
            calculator.Invoking(x => x.Divide(10, 0))
                .Should().Throw<DivideByZeroException>();
        }
    }
}
```

## Integration with Git Hooks

### Pre-commit Hook Example

Create `.git/hooks/pre-commit`:
```bash
#!/bin/sh
# Run AI test generator before commit
dotnet run --project QA-AI/TestGenerator --changed-files $(git diff --cached --name-only)
```

## CI/CD Integration

See the strategy document for complete Azure DevOps and GitHub Actions examples.

## Monitoring and Optimization

1. **Track API Usage:**
   - Monitor token consumption
   - Track costs per test generation
   - Optimize prompts to reduce tokens

2. **Test Quality Metrics:**
   - Test pass rate
   - Code coverage percentage
   - False positive rate

3. **Continuous Improvement:**
   - Refine prompts based on results
   - Add project-specific patterns
   - Learn from manual corrections

