using System.Collections.Generic;

namespace CodeAnalyzer.Models
{
    /// <summary>
    /// Represents information about a UI control in Windows Forms
    /// </summary>
    public class UIControlInfo
    {
        /// <summary>
        /// The control name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The control type (e.g., Button, TextBox, Form)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The AutomationId if set
        /// </summary>
        public string AutomationId { get; set; }

        /// <summary>
        /// The text/content of the control
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Whether the control is visible
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// Whether the control is enabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Child controls
        /// </summary>
        public List<UIControlInfo> Children { get; set; }

        /// <summary>
        /// Common events on the control
        /// </summary>
        public List<string> Events { get; set; }

        public UIControlInfo()
        {
            Children = new List<UIControlInfo>();
            Events = new List<string>();
        }
    }
}

