using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CodeAnalyzer.Models;

namespace CodeAnalyzer.Services
{
    /// <summary>
    /// Analyzes Windows Forms Designer files to extract UI control information
    /// </summary>
    public class WindowsFormsAnalyzer
    {
        /// <summary>
        /// Analyzes a Designer file and extracts UI control information
        /// </summary>
        public List<UIControlInfo> AnalyzeDesignerFile(string designerFilePath)
        {
            if (!File.Exists(designerFilePath))
            {
                throw new FileNotFoundException($"Designer file not found: {designerFilePath}");
            }

            var sourceCode = File.ReadAllText(designerFilePath);
            return AnalyzeDesignerCode(sourceCode);
        }

        /// <summary>
        /// Analyzes Designer source code and extracts UI controls
        /// </summary>
        public List<UIControlInfo> AnalyzeDesignerCode(string sourceCode)
        {
            var controls = new List<UIControlInfo>();

            try
            {
                // Pattern to match control declarations like: this.button1 = new System.Windows.Forms.Button();
                var controlPattern = @"this\.(\w+)\s*=\s*new\s+([\w.]+)\(\);";
                var matches = Regex.Matches(sourceCode, controlPattern);

                foreach (Match match in matches)
                {
                    var controlName = match.Groups[1].Value;
                    var controlType = match.Groups[2].Value;

                    var controlInfo = new UIControlInfo
                    {
                        Name = controlName,
                        Type = controlType
                    };

                    // Extract control properties
                    ExtractControlProperties(sourceCode, controlName, controlInfo);

                    controls.Add(controlInfo);
                }

                // Also look for form itself
                var formPattern = @"partial\s+class\s+(\w+)\s*:\s*Form";
                var formMatch = Regex.Match(sourceCode, formPattern);
                if (formMatch.Success)
                {
                    var formInfo = new UIControlInfo
                    {
                        Name = formMatch.Groups[1].Value,
                        Type = "Form"
                    };
                    ExtractControlProperties(sourceCode, "this", formInfo);
                    controls.Insert(0, formInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error analyzing designer file: {ex.Message}");
            }

            return controls;
        }

        /// <summary>
        /// Extracts properties for a specific control
        /// </summary>
        private void ExtractControlProperties(string sourceCode, string controlName, UIControlInfo controlInfo)
        {
            // Pattern to match property assignments like: this.button1.Text = "Click Me";
            var propertyPattern = $@"{Regex.Escape(controlName)}\.(\w+)\s*=\s*""?([^;]+)""?;";
            var matches = Regex.Matches(sourceCode, propertyPattern);

            foreach (Match match in matches)
            {
                var propertyName = match.Groups[1].Value;
                var propertyValue = match.Groups[2].Value.Trim('"');

                switch (propertyName.ToLower())
                {
                    case "text":
                        controlInfo.Text = propertyValue;
                        break;
                    case "name":
                        if (string.IsNullOrEmpty(controlInfo.Name))
                            controlInfo.Name = propertyValue;
                        break;
                    case "visible":
                        if (bool.TryParse(propertyValue, out bool visible))
                            controlInfo.IsVisible = visible;
                        break;
                    case "enabled":
                        if (bool.TryParse(propertyValue, out bool enabled))
                            controlInfo.IsEnabled = enabled;
                        break;
                }
            }

            // Look for event handlers
            var eventPattern = $@"{Regex.Escape(controlName)}\.(\w+)\s*\+=\s*new\s+[\w.]+\(([^)]+)\);";
            var eventMatches = Regex.Matches(sourceCode, eventPattern);
            foreach (Match match in eventMatches)
            {
                controlInfo.Events.Add(match.Groups[1].Value);
            }
        }

        /// <summary>
        /// Finds the corresponding Designer file for a Form class
        /// </summary>
        public string FindDesignerFile(string formFilePath)
        {
            if (string.IsNullOrEmpty(formFilePath))
                return null;

            var directory = Path.GetDirectoryName(formFilePath);
            var fileName = Path.GetFileNameWithoutExtension(formFilePath);
            var designerPath = Path.Combine(directory, $"{fileName}.Designer.cs");

            return File.Exists(designerPath) ? designerPath : null;
        }
    }
}

