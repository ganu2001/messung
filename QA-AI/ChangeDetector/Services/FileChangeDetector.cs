using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ChangeDetector.Models;

namespace ChangeDetector.Services
{
    /// <summary>
    /// Detects changes by comparing file timestamps (fallback when Git is not available)
    /// </summary>
    public class FileChangeDetector
    {
        private readonly string _basePath;
        private readonly Dictionary<string, DateTime> _fileTimestamps;

        public FileChangeDetector(string basePath)
        {
            _basePath = basePath ?? Directory.GetCurrentDirectory();
            _fileTimestamps = new Dictionary<string, DateTime>();
            LoadFileTimestamps();
        }

        /// <summary>
        /// Detects modified files since last check
        /// </summary>
        public List<CodeChange> DetectChanges()
        {
            var changes = new List<CodeChange>();

            var csharpFiles = Directory.GetFiles(_basePath, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.Contains("\\bin\\") && !f.Contains("\\obj\\") && 
                           !f.Contains("\\Tests\\") && !f.Contains("\\Test\\"))
                .ToList();

            foreach (var filePath in csharpFiles)
            {
                var relativePath = Path.GetRelativePath(_basePath, filePath);
                var lastWriteTime = File.GetLastWriteTime(filePath);

                if (_fileTimestamps.ContainsKey(relativePath))
                {
                    if (_fileTimestamps[relativePath] < lastWriteTime)
                    {
                        changes.Add(new CodeChange
                        {
                            FilePath = relativePath,
                            ChangeType = ChangeType.Modified,
                            DetectedAt = lastWriteTime
                        });
                    }
                }
                else
                {
                    changes.Add(new CodeChange
                    {
                        FilePath = relativePath,
                        ChangeType = ChangeType.Added,
                        DetectedAt = lastWriteTime
                    });
                }
            }

            return changes;
        }

        /// <summary>
        /// Updates the file timestamp cache
        /// </summary>
        public void UpdateCache()
        {
            LoadFileTimestamps();
        }

        private void LoadFileTimestamps()
        {
            _fileTimestamps.Clear();

            if (!Directory.Exists(_basePath))
                return;

            var csharpFiles = Directory.GetFiles(_basePath, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.Contains("\\bin\\") && !f.Contains("\\obj\\") && 
                           !f.Contains("\\Tests\\") && !f.Contains("\\Test\\"))
                .ToList();

            foreach (var filePath in csharpFiles)
            {
                var relativePath = Path.GetRelativePath(_basePath, filePath);
                _fileTimestamps[relativePath] = File.GetLastWriteTime(filePath);
            }
        }
    }
}

