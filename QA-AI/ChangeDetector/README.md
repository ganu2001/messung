# ChangeDetector

The ChangeDetector component detects code changes in the repository using Git or file system monitoring.

## Features

- **Git-based detection**: Detects changes between commits, staged changes, or working directory changes
- **File-based detection**: Fallback option when Git is not available
- **C# file filtering**: Only processes relevant C# files (excludes tests, bin, obj folders)
- **JSON output**: Exports detected changes to JSON format for integration with other tools

## Usage

### Command Line

```bash
# Detect changes between commits
ChangeDetector.exe --from <commit-hash> --to <commit-hash>

# Detect staged changes
ChangeDetector.exe --staged

# Detect working directory changes
ChangeDetector.exe --working

# Specify repository path
ChangeDetector.exe --path "C:\Projects\XMPS2000" --staged

# Output to JSON file
ChangeDetector.exe --staged --output changes.json

# Use file-based detection (no Git)
ChangeDetector.exe --no-git
```

### Options

- `-p, --path <path>` - Repository or project path (default: current directory)
- `-f, --from <commit>` - From commit hash (default: previous commit)
- `-t, --to <commit>` - To commit hash (default: HEAD)
- `-s, --staged` - Detect staged changes
- `-w, --working` - Detect working directory changes
- `-o, --output <file>` - Output JSON file path
- `--no-git` - Use file-based detection instead of Git
- `-h, --help` - Show help message

## Output Format

The tool outputs a list of `CodeChange` objects in JSON format:

```json
[
  {
    "FilePath": "XMPS2000/frmMain.cs",
    "ChangeType": "Modified",
    "ModifiedClasses": ["ALL"],
    "ModifiedMethods": [],
    "CommitHash": "abc123...",
    "PreviousCommitHash": "def456...",
    "DetectedAt": "2024-01-15T10:30:00Z"
  }
]
```

## Integration

The ChangeDetector is designed to work with the CodeAnalyzer component:

1. ChangeDetector identifies changed files
2. CodeAnalyzer analyzes each changed file
3. TestGenerator uses the analysis to generate tests

## Dependencies

- **LibGit2Sharp** - Git repository access
- **Newtonsoft.Json** - JSON serialization

## Examples

### Example 1: Detect staged changes before commit

```bash
ChangeDetector.exe --staged --output staged-changes.json
```

### Example 2: Compare two commits

```bash
ChangeDetector.exe --from abc123 --to def456 --output changes.json
```

### Example 3: Monitor working directory

```bash
ChangeDetector.exe --working
```

