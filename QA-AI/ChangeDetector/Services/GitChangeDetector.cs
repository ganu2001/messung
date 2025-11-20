using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using ChangeDetector.Models;

namespace ChangeDetector.Services
{
    /// <summary>
    /// Detects code changes using Git repository
    /// </summary>
    public class GitChangeDetector : IDisposable
    {
        private readonly string _repositoryPath;
        private Repository _repository;

        public GitChangeDetector(string repositoryPath = null)
        {
            _repositoryPath = repositoryPath ?? FindRepositoryRoot(Directory.GetCurrentDirectory());
            
            if (string.IsNullOrEmpty(_repositoryPath) || !Repository.IsValid(_repositoryPath))
            {
                throw new InvalidOperationException($"Invalid Git repository path: {_repositoryPath}");
            }

            _repository = new Repository(_repositoryPath);
        }

        /// <summary>
        /// Detects changes between two commits
        /// </summary>
        public List<CodeChange> DetectChanges(string fromCommitHash = null, string toCommitHash = null)
        {
            var changes = new List<CodeChange>();

            try
            {
                Commit fromCommit = null;
                Commit toCommit = _repository.Head.Tip;

                if (!string.IsNullOrEmpty(fromCommitHash))
                {
                    fromCommit = _repository.Lookup<Commit>(fromCommitHash);
                }
                else
                {
                    // Compare with previous commit
                    if (_repository.Head.Tip.Parents.Any())
                    {
                        fromCommit = _repository.Head.Tip.Parents.First();
                    }
                }

                if (!string.IsNullOrEmpty(toCommitHash))
                {
                    toCommit = _repository.Lookup<Commit>(toCommitHash);
                }

                if (fromCommit == null)
                {
                    // No previous commit, return all files as added
                    return GetAllFilesAsAdded(toCommit);
                }

                var treeChanges = _repository.Diff.Compare<TreeChanges>(fromCommit.Tree, toCommit.Tree);

                foreach (var change in treeChanges)
                {
                    if (IsRelevantFile(change.Path))
                    {
                        var codeChange = new CodeChange
                        {
                            FilePath = change.Path,
                            ChangeType = MapChangeType(change.Status),
                            CommitHash = toCommit.Sha,
                            PreviousCommitHash = fromCommit.Sha
                        };

                        // Try to extract class and method information from diff
                        ExtractChangeDetails(change, codeChange);

                        changes.Add(codeChange);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error detecting changes: {ex.Message}");
                throw;
            }

            return changes;
        }

        /// <summary>
        /// Detects changes in the working directory (unstaged changes)
        /// </summary>
        public List<CodeChange> DetectWorkingDirectoryChanges()
        {
            var changes = new List<CodeChange>();

            try
            {
                var status = _repository.RetrieveStatus();

                foreach (var item in status)
                {
                    if (IsRelevantFile(item.FilePath))
                    {
                        var codeChange = new CodeChange
                        {
                            FilePath = item.FilePath,
                            ChangeType = MapStatusChangeType(item.State),
                            CommitHash = "WORKING_DIRECTORY"
                        };

                        changes.Add(codeChange);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error detecting working directory changes: {ex.Message}");
                throw;
            }

            return changes;
        }

        /// <summary>
        /// Detects changes in staged files (index)
        /// </summary>
        public List<CodeChange> DetectStagedChanges()
        {
            var changes = new List<CodeChange>();

            try
            {
                var status = _repository.RetrieveStatus(new StatusOptions
                {
                    IncludeIgnored = false,
                    Show = StatusShowOption.IndexOnly
                });

                foreach (var item in status)
                {
                    if (IsRelevantFile(item.FilePath))
                    {
                        var codeChange = new CodeChange
                        {
                            FilePath = item.FilePath,
                            ChangeType = MapStatusChangeType(item.State),
                            CommitHash = "STAGED"
                        };

                        changes.Add(codeChange);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error detecting staged changes: {ex.Message}");
                throw;
            }

            return changes;
        }

        /// <summary>
        /// Gets all C# files in the repository as added changes
        /// </summary>
        private List<CodeChange> GetAllFilesAsAdded(Commit commit)
        {
            var changes = new List<CodeChange>();

            if (commit == null) return changes;

            foreach (var entry in commit.Tree)
            {
                if (entry.TargetType == TreeEntryTargetType.Blob && 
                    entry.Path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    changes.Add(new CodeChange
                    {
                        FilePath = entry.Path,
                        ChangeType = ChangeType.Added,
                        CommitHash = commit.Sha
                    });
                }
            }

            return changes;
        }

        /// <summary>
        /// Checks if a file is relevant for test generation
        /// </summary>
        private bool IsRelevantFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return false;

            // Only process C# files
            if (!filePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                return false;

            // Skip test files
            if (filePath.Contains("\\Tests\\") || filePath.Contains("/Tests/") ||
                filePath.Contains("\\Test\\") || filePath.Contains("/Test/"))
                return false;

            // Skip generated files
            if (filePath.Contains("\\obj\\") || filePath.Contains("/obj/") ||
                filePath.Contains("\\bin\\") || filePath.Contains("/bin/"))
                return false;

            return true;
        }

        /// <summary>
        /// Maps LibGit2Sharp ChangeKind to our ChangeType
        /// </summary>
        private ChangeType MapChangeType(ChangeKind changeKind)
        {
            switch (changeKind)
            {
                case ChangeKind.Added:
                    return ChangeType.Added;
                case ChangeKind.Deleted:
                    return ChangeType.Deleted;
                case ChangeKind.Modified:
                    return ChangeType.Modified;
                case ChangeKind.Renamed:
                    return ChangeType.Renamed;
                default:
                    return ChangeType.Modified;
            }
        }

        /// <summary>
        /// Maps LibGit2Sharp FileStatus to ChangeType
        /// </summary>
        private ChangeType MapStatusChangeType(FileStatus status)
        {
            if (status.HasFlag(FileStatus.NewInIndex) || status.HasFlag(FileStatus.NewInWorkdir))
                return ChangeType.Added;
            if (status.HasFlag(FileStatus.DeletedFromIndex) || status.HasFlag(FileStatus.DeletedFromWorkdir))
                return ChangeType.Deleted;
            if (status.HasFlag(FileStatus.ModifiedInIndex) || status.HasFlag(FileStatus.ModifiedInWorkdir))
                return ChangeType.Modified;
            
            return ChangeType.Modified;
        }

        /// <summary>
        /// Extracts class and method information from a tree change
        /// </summary>
        private void ExtractChangeDetails(TreeEntryChanges change, CodeChange codeChange)
        {
            // This is a simplified version - CodeAnalyzer will do detailed analysis
            // Here we just mark that the file has changes
            if (change.Status == ChangeKind.Modified || change.Status == ChangeKind.Added)
            {
                // The CodeAnalyzer will parse the actual file to extract classes and methods
                codeChange.ModifiedClasses.Add("ALL"); // Placeholder - will be refined by CodeAnalyzer
            }
        }

        /// <summary>
        /// Finds the Git repository root by traversing up the directory tree
        /// </summary>
        private string FindRepositoryRoot(string startPath)
        {
            var directory = new DirectoryInfo(startPath);

            while (directory != null)
            {
                var gitPath = Path.Combine(directory.FullName, ".git");
                if (Directory.Exists(gitPath) || File.Exists(gitPath))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            return null;
        }

        /// <summary>
        /// Gets the full path of a file relative to repository root
        /// </summary>
        public string GetFullPath(string relativePath)
        {
            return Path.Combine(_repositoryPath, relativePath);
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}

