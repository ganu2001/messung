using System;
using System.Collections.Generic;

namespace ChangeDetector.Models
{
    /// <summary>
    /// Represents a code change detected in the repository
    /// </summary>
    public class CodeChange
    {
        /// <summary>
        /// The file path relative to repository root
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The type of change (Added, Modified, Deleted, Renamed)
        /// </summary>
        public ChangeType ChangeType { get; set; }

        /// <summary>
        /// List of modified classes in this file
        /// </summary>
        public List<string> ModifiedClasses { get; set; }

        /// <summary>
        /// List of modified methods in this file
        /// </summary>
        public List<string> ModifiedMethods { get; set; }

        /// <summary>
        /// The commit hash where this change was detected
        /// </summary>
        public string CommitHash { get; set; }

        /// <summary>
        /// The previous commit hash (for comparison)
        /// </summary>
        public string PreviousCommitHash { get; set; }

        /// <summary>
        /// Timestamp when the change was detected
        /// </summary>
        public DateTime DetectedAt { get; set; }

        /// <summary>
        /// Whether this is a C# file
        /// </summary>
        public bool IsCSharpFile => FilePath != null && FilePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Whether this is a Windows Forms Designer file
        /// </summary>
        public bool IsDesignerFile => FilePath != null && FilePath.EndsWith(".Designer.cs", StringComparison.OrdinalIgnoreCase);

        public CodeChange()
        {
            ModifiedClasses = new List<string>();
            ModifiedMethods = new List<string>();
            DetectedAt = DateTime.Now;
        }
    }

    /// <summary>
    /// Types of changes that can be detected
    /// </summary>
    public enum ChangeType
    {
        Added,
        Modified,
        Deleted,
        Renamed
    }
}

