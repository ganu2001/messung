# CodeAnalyzer

The CodeAnalyzer component analyzes C# source code using Roslyn to extract detailed information about classes, methods, properties, and UI controls for test generation.

## Features

- **Roslyn-based analysis**: Uses Microsoft.CodeAnalysis for accurate C# parsing
- **Class extraction**: Identifies classes, interfaces, methods, properties, and fields
- **Windows Forms support**: Analyzes Designer files to extract UI control information
- **Dependency detection**: Identifies class dependencies and relationships
- **JSON output**: Exports analysis results in structured JSON format

## Usage

### Command Line

```bash
# Analyze a C# file
CodeAnalyzer.exe --file MyClass.cs

# Analyze and output to JSON
CodeAnalyzer.exe --file MyClass.cs --output analysis.json

# Short form
CodeAnalyzer.exe MyClass.cs -o analysis.json
```

### Options

- `-f, --file <path>` - C# file to analyze
- `-o, --output <file>` - Output JSON file path
- `-h, --help` - Show help message

## Output Format

The tool outputs a `CodeContext` object in JSON format:

```json
{
  "FilePath": "XMPS2000/frmMain.cs",
  "Namespace": "XMPS2000",
  "Classes": [
    {
      "Name": "frmMain",
      "FullName": "frmMain",
      "BaseType": "Form",
      "IsPublic": true,
      "IsStatic": false,
      "Methods": [
        {
          "Name": "InitializeComponent",
          "ReturnType": "void",
          "Parameters": [],
          "IsPublic": false,
          "IsStatic": false
        }
      ],
      "Properties": [...],
      "Dependencies": ["System.Windows.Forms", "XMPS2000.Core"]
    }
  ],
  "UIControls": [
    {
      "Name": "button1",
      "Type": "System.Windows.Forms.Button",
      "Text": "Click Me",
      "IsVisible": true,
      "IsEnabled": true,
      "Events": ["Click"]
    }
  ],
  "IsWindowsFormsFile": true
}
```

## Analysis Capabilities

### Class Analysis
- Class name, namespace, and full name
- Base class and interfaces
- Access modifiers (public, private, etc.)
- Modifiers (static, abstract, partial)
- Methods with signatures and parameters
- Properties with getters/setters
- Fields and their types
- Dependencies on other classes

### Method Analysis
- Method name and return type
- Parameters with types and default values
- Access modifiers and modifiers (static, async, virtual, etc.)
- Method body source code
- XML documentation comments
- Line numbers for location tracking

### Windows Forms Analysis
- UI control extraction from Designer files
- Control properties (Text, Name, Visible, Enabled)
- Event handlers
- Control hierarchy

## Integration

The CodeAnalyzer works with the ChangeDetector:

1. ChangeDetector identifies changed files
2. CodeAnalyzer analyzes each file to extract code structure
3. TestGenerator uses the analysis to generate appropriate tests

## Dependencies

- **Microsoft.CodeAnalysis.CSharp** - Roslyn C# compiler
- **Newtonsoft.Json** - JSON serialization

## Examples

### Example 1: Analyze a class file

```bash
CodeAnalyzer.exe --file XMPS2000/Core/XMPS.cs --output xmps-analysis.json
```

### Example 2: Analyze a Windows Forms file

```bash
CodeAnalyzer.exe --file XMPS2000/frmMain.cs --output form-analysis.json
```

The analyzer will automatically detect and analyze the corresponding `.Designer.cs` file if it exists.

## Limitations

- Currently focuses on C# files only
- Designer file analysis uses regex patterns (may not catch all edge cases)
- Complex generic types may not be fully parsed
- Partial classes are analyzed separately (not merged)

