using System.Collections.Generic;

namespace TestGenerator.Models
{
    public class CodeContext
    {
        public string FilePath { get; set; }
        public string Namespace { get; set; }
        public bool IsWindowsFormsFile { get; set; }
        public List<ClassContext> Classes { get; set; } = new();
        public List<UIControlContext> UIControls { get; set; } = new();
    }

    public class ClassContext
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string BaseType { get; set; }
        public List<MethodContext> Methods { get; set; } = new();
        public List<string> Dependencies { get; set; } = new();
        public string SourceCode { get; set; }
    }

    public class MethodContext
    {
        public string Name { get; set; }
        public string Signature { get; set; }
        public string ReturnType { get; set; }
        public List<ParameterContext> Parameters { get; set; } = new();
        public string Body { get; set; }
    }

    public class ParameterContext
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
    }

    public class UIControlContext
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public List<string> Events { get; set; } = new();
    }
}