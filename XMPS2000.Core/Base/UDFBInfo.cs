using System.Collections.Generic;

namespace XMPS2000.Core.Base
{
    public struct UserDefinedFunctionBlock
    {
        public string Type;
        public string DataType;
        public string Text;
        public string Name;

        public UserDefinedFunctionBlock(string _type, string _datatype, string _text, string _name) : this()
        {
            this.Type = _type;
            this.DataType = _datatype;
            this.Text = _text;
            this.Name = _name;
        }
    }
    public class UDFBInfo
    {
        public string UDFBName;
        public int Inputs;
        public int Outputs;
        public List<UserDefinedFunctionBlock> userDefinedFunctionBlocks = new List<UserDefinedFunctionBlock> { };
        public List<UserDefinedFunctionBlock> UDFBlocks { get; set; }

        public UDFBInfo()
        {
            UDFBlocks = new List<UserDefinedFunctionBlock>();
        }
        public bool AddUDFB(List<UserDefinedFunctionBlock> udfbinfo,string name)
        {
            bool _success = false;
            UDFBlocks.RemoveAll(s => s.Name == name);
            foreach(UserDefinedFunctionBlock udfb in udfbinfo)
            {
                var obj = new UserDefinedFunctionBlock(udfb.Type, udfb.DataType, udfb.Text, name);
                UDFBlocks.Add(obj);
            }
            return _success;  
        }
    }
}