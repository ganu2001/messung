namespace CodeAnalyzer.Models
{
    /// <summary>
    /// Represents information about a property
    /// </summary>
    public class PropertyInfo
    {
        /// <summary>
        /// The property name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The property type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Whether the property has a getter
        /// </summary>
        public bool HasGetter { get; set; }

        /// <summary>
        /// Whether the property has a setter
        /// </summary>
        public bool HasSetter { get; set; }

        /// <summary>
        /// Whether the property is public
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Whether the property is static
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// XML documentation comments
        /// </summary>
        public string Documentation { get; set; }
    }
}

