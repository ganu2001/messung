# AI-Based QA Framework for XMPS2000

This directory contains the AI-powered automated testing framework for the XMPS2000 project.

## Overview

The AI QA Framework automatically generates and maintains test cases (unit, integration, and UI) whenever code changes are detected in the repository.

## Documentation

- **[AI_QA_Framework_Strategy.md](./AI_QA_Framework_Strategy.md)** - Comprehensive strategy document covering architecture, implementation phases, and best practices
- **[Implementation_Guide.md](./Implementation_Guide.md)** - Quick start guide for setting up and using the framework

## Key Features

✅ **Automated Test Generation** - AI generates tests based on code changes  
✅ **Multi-Level Testing** - Unit, Integration, and UI test generation  
✅ **Change Detection** - Automatically detects code modifications  
✅ **CI/CD Integration** - Seamless integration with build pipelines  
✅ **Continuous Maintenance** - Tests are updated as code evolves  

## Quick Start

1. Read the [Strategy Document](./AI_QA_Framework_Strategy.md) for comprehensive understanding
2. Follow the [Implementation Guide](./Implementation_Guide.md) for setup
3. Configure your LLM API key
4. Run the test generator on your first change

## Architecture

The framework consists of four main components:

1. **Change Detector** - Monitors Git for code changes
2. **Code Analyzer** - Parses and analyzes C# code using Roslyn
3. **Test Generator** - Uses LLM to generate test code
4. **Test Executor** - Compiles and runs generated tests

## Technology Stack

- **Testing:** xUnit, Moq, FluentAssertions, FlaUI
- **Code Analysis:** Roslyn (Microsoft.CodeAnalysis)
- **AI/LLM:** OpenAI GPT-4 / Claude 3 / Local LLM
- **CI/CD:** Azure DevOps / GitHub Actions

## Status

🚧 **In Planning Phase** - Framework is being designed and implemented

## Contributing

When implementing this framework:
1. Follow the phased approach outlined in the strategy document
2. Start with foundation setup (Phase 1)
3. Iterate and refine based on results
4. Document any deviations or improvements

## Support

For questions or issues, refer to the strategy document or contact the development team.

