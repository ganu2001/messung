using System;
using System.Collections.Generic;

namespace CodeAnalyzer.Models
{
    /// <summary>
    /// Represents information about a method
    /// </summary>
    public class MethodInfo
    {
        /// <summary>
        /// The method name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The return type
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// List of parameter names and types
        /// </summary>
        public List<ParameterInfo> Parameters { get; set; }

        /// <summary>
        /// Whether the method is public
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Whether the method is static
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Whether the method is async
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        /// Whether the method is virtual
        /// </summary>
        public bool IsVirtual { get; set; }

        /// <summary>
        /// Whether the method is abstract
        /// </summary>
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Whether the method is an override
        /// </summary>
        public bool IsOverride { get; set; }

        /// <summary>
        /// The method body/source code
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// XML documentation comments
        /// </summary>
        public string Documentation { get; set; }

        /// <summary>
        /// Line number where the method starts
        /// </summary>
        public int StartLine { get; set; }

        /// <summary>
        /// Line number where the method ends
        /// </summary>
        public int EndLine { get; set; }

        public MethodInfo()
        {
            Parameters = new List<ParameterInfo>();
        }
    }

    /// <summary>
    /// Represents a method parameter
    /// </summary>
    public class ParameterInfo
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameter type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Whether the parameter has a default value
        /// </summary>
        public bool HasDefaultValue { get; set; }

        /// <summary>
        /// Default value if any
        /// </summary>
        public string DefaultValue { get; set; }
    }
}

