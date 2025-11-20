# Quick Start Guide - ChangeDetector & CodeAnalyzer

This guide will help you get started with the first two components of the AI QA Framework.

## Prerequisites

1. **Visual Studio 2019+** or **VS Code** with C# extension
2. **.NET Framework 4.8 SDK**
3. **Git** (optional, for Git-based change detection)
4. **NuGet Package Manager**

## Setup

### Step 1: Restore NuGet Packages

Open the solution in Visual Studio and restore NuGet packages:

```bash
# From the solution root directory
nuget restore XMPS2000.sln

# Or use Visual Studio: Right-click solution -> Restore NuGet Packages
```

### Step 2: Build the Projects

Build both projects:

```bash
# Build ChangeDetector
msbuild QA-AI/ChangeDetector/ChangeDetector.csproj /p:Configuration=Debug

# Build CodeAnalyzer
msbuild QA-AI/CodeAnalyzer/CodeAnalyzer.csproj /p:Configuration=Debug
```

Or build from Visual Studio:
- Right-click solution -> Build Solution

## Testing ChangeDetector

### Test 1: Detect Working Directory Changes

```bash
# Navigate to the ChangeDetector output directory
cd QA-AI/ChangeDetector/bin/Debug

# Run ChangeDetector to detect working directory changes
ChangeDetector.exe --working
```

### Test 2: Detect Staged Changes

```bash
# Make some changes to a C# file, then stage it
git add XMPS2000/frmMain.cs

# Run ChangeDetector
ChangeDetector.exe --staged --output changes.json
```

### Test 3: Compare Commits

```bash
# Get commit hashes
git log --oneline -5

# Compare two commits
ChangeDetector.exe --from <commit-hash-1> --to <commit-hash-2> --output changes.json
```

## Testing CodeAnalyzer

### Test 1: Analyze a Simple Class

```bash
# Navigate to the CodeAnalyzer output directory
cd QA-AI/CodeAnalyzer/bin/Debug

# Analyze a C# file
CodeAnalyzer.exe --file ../../../../XMPS2000/Program.cs --output program-analysis.json
```

### Test 2: Analyze a Windows Forms File

```bash
# Analyze a form file (will also analyze Designer file if present)
CodeAnalyzer.exe --file ../../../../XMPS2000/frmMain.cs --output form-analysis.json
```

### Test 3: View Analysis Results

```bash
# The output JSON file contains detailed analysis
# Open it in a text editor or use a JSON viewer
cat form-analysis.json
```

## Integration Example

Here's how to use both tools together:

```bash
# Step 1: Detect changes
cd QA-AI/ChangeDetector/bin/Debug
ChangeDetector.exe --staged --output ../../../../changes.json

# Step 2: Analyze each changed file
cd ../../CodeAnalyzer/bin/Debug

# Read changes.json and analyze each file
# (This would typically be automated in the TestGenerator)
CodeAnalyzer.exe --file ../../../../XMPS2000/frmMain.cs --output ../../../../frmMain-analysis.json
```

## Troubleshooting

### Issue: LibGit2Sharp not found

**Solution**: Make sure NuGet packages are restored. The native dependencies for LibGit2Sharp may need to be downloaded.

### Issue: Roslyn analysis fails

**Solution**: Ensure the target file is valid C# code. Check for syntax errors first.

### Issue: Designer file not found

**Solution**: The CodeAnalyzer will work without Designer files. UI control analysis is optional.

### Issue: Build errors

**Solution**: 
1. Ensure .NET Framework 4.8 is installed
2. Restore NuGet packages
3. Check that all project references are correct

## Next Steps

Once both components are working:

1. **TestGenerator** - Will use CodeAnalyzer output to generate tests
2. **TestExecutor** - Will compile and run generated tests
3. **CI/CD Integration** - Automate the entire pipeline

## Command Reference

### ChangeDetector

```bash
ChangeDetector.exe [options]

Options:
  -p, --path <path>        Repository path
  -f, --from <commit>      From commit hash
  -t, --to <commit>        To commit hash
  -s, --staged             Detect staged changes
  -w, --working            Detect working directory changes
  -o, --output <file>      Output JSON file
  --no-git                 Use file-based detection
  -h, --help               Show help
```

### CodeAnalyzer

```bash
CodeAnalyzer.exe [options] <file>

Options:
  -f, --file <path>        C# file to analyze
  -o, --output <file>      Output JSON file
  -h, --help               Show help
```

## Project Structure

```
QA-AI/
├── ChangeDetector/
│   ├── Models/
│   │   ├── CodeChange.cs
│   │   └── FileChange.cs
│   ├── Services/
│   │   ├── GitChangeDetector.cs
│   │   └── FileChangeDetector.cs
│   └── Program.cs
│
└── CodeAnalyzer/
    ├── Models/
    │   ├── CodeContext.cs
    │   ├── ClassInfo.cs
    │   ├── MethodInfo.cs
    │   ├── PropertyInfo.cs
    │   └── UIControlInfo.cs
    ├── Services/
    │   ├── RoslynCodeAnalyzer.cs
    │   └── WindowsFormsAnalyzer.cs
    └── Program.cs
```

## Support

For issues or questions:
1. Check the individual README files in each project folder
2. Review the main strategy document: `AI_QA_Framework_Strategy.md`
3. Check build output for detailed error messages

