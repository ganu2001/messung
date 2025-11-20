using System;
using System.Collections.Generic;

namespace CodeAnalyzer.Models
{
    /// <summary>
    /// Represents information about a class
    /// </summary>
    public class ClassInfo
    {
        /// <summary>
        /// The class name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The full name including namespace
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The base class or interface
        /// </summary>
        public string BaseType { get; set; }

        /// <summary>
        /// Whether the class is public
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Whether the class is static
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Whether the class is abstract
        /// </summary>
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Whether the class is partial
        /// </summary>
        public bool IsPartial { get; set; }

        /// <summary>
        /// List of methods in the class
        /// </summary>
        public List<MethodInfo> Methods { get; set; }

        /// <summary>
        /// List of properties in the class
        /// </summary>
        public List<PropertyInfo> Properties { get; set; }

        /// <summary>
        /// List of fields in the class
        /// </summary>
        public List<string> Fields { get; set; }

        /// <summary>
        /// Dependencies (other classes used)
        /// </summary>
        public List<string> Dependencies { get; set; }

        /// <summary>
        /// The source code of the class
        /// </summary>
        public string SourceCode { get; set; }

        /// <summary>
        /// Line number where the class starts
        /// </summary>
        public int StartLine { get; set; }

        /// <summary>
        /// Line number where the class ends
        /// </summary>
        public int EndLine { get; set; }

        public ClassInfo()
        {
            Methods = new List<MethodInfo>();
            Properties = new List<PropertyInfo>();
            Fields = new List<string>();
            Dependencies = new List<string>();
        }
    }
}

