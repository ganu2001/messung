using System;
using System.Collections.Generic;

namespace CodeAnalyzer.Models
{
    /// <summary>
    /// Represents the complete context of a code file for test generation
    /// </summary>
    public class CodeContext
    {
        /// <summary>
        /// The file path
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The namespace of the file
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// List of classes in the file
        /// </summary>
        public List<ClassInfo> Classes { get; set; }

        /// <summary>
        /// List of interfaces in the file
        /// </summary>
        public List<ClassInfo> Interfaces { get; set; }

        /// <summary>
        /// List of UI controls (for Windows Forms)
        /// </summary>
        public List<UIControlInfo> UIControls { get; set; }

        /// <summary>
        /// The full source code
        /// </summary>
        public string SourceCode { get; set; }

        /// <summary>
        /// Whether this is a Windows Forms file
        /// </summary>
        public bool IsWindowsFormsFile { get; set; }

        /// <summary>
        /// Whether this is a Designer file
        /// </summary>
        public bool IsDesignerFile { get; set; }

        /// <summary>
        /// Dependencies/using statements
        /// </summary>
        public List<string> Usings { get; set; }

        public CodeContext()
        {
            Classes = new List<ClassInfo>();
            Interfaces = new List<ClassInfo>();
            UIControls = new List<UIControlInfo>();
            Usings = new List<string>();
        }
    }
}

