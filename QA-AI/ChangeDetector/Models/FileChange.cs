using System;

namespace ChangeDetector.Models
{
    /// <summary>
    /// Represents a file-level change
    /// </summary>
    public class FileChange
    {
        /// <summary>
        /// The file path relative to repository root
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The type of change
        /// </summary>
        public ChangeType ChangeType { get; set; }

        /// <summary>
        /// The old file path (for renamed files)
        /// </summary>
        public string OldFilePath { get; set; }

        /// <summary>
        /// The commit hash
        /// </summary>
        public string CommitHash { get; set; }

        /// <summary>
        /// The diff content (if available)
        /// </summary>
        public string DiffContent { get; set; }

        public FileChange()
        {
        }

        public FileChange(string filePath, ChangeType changeType)
        {
            FilePath = filePath;
            ChangeType = changeType;
        }
    }
}

