using System.Collections.Generic;

namespace XMPS2000.Core.Base
{
    public class Block
    {
        private string _name;
        private string _type;
        private string _elements;
        private List<string> _Elements = new List<string>();
        private List<string> _Comments = new List<string>();

        public string Name { get => _name; set => _name = value; }
        public string Type { get => _type; set => _type = value; }
        //byte[] elements_str;
        //public byte[] Elements { get { return elements_str; } set { elements_str = value; } }
        public string elements { get { return _elements; } set { _elements = value; } }
        public List<string> Elements { get { return _Elements; } }
        public void AddContent(List<string> value)
        {
            _Elements.AddRange(value);
        }
        public void ClearContent()
        {
            _Elements.Clear();
        }
        public List<string> Comments { get { return _Comments; } }

        public void ClearCommentContent()
        {
            Comments.Clear();
        }
        public void AddContentComment(List<string> value1)
        {
            _Comments.AddRange(value1);
        }
    }
}