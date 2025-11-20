using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000.Core.Base
{
    public class HSIOFunctionBlock
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string DataType { get; set; }
        public string Value { get; set; }

        public HSIOFunctionBlock() { }
        public HSIOFunctionBlock(string _type,string _text,string _dataType,string _value)
        {
            this.Type = _type;
            this.Text = _text;
            this.DataType = _dataType;
            this.Value = _value;
        }
    }
    public class HSIO
    {
        public string HSIOFunctionBlockName { get; set; }
        public string HSIOFunctionBlockType { get; set; }
        public List<HSIOFunctionBlock> HSIOBlocks { get; set; }
        public HSIO()
        {
            HSIOBlocks = new List<HSIOFunctionBlock>();
        }
        public void SaveHSIOFunctionBlock(List<HSIOFunctionBlock> hSIOFunctionBlock,string HsioName)
        {
           this.HSIOFunctionBlockName = HsioName;
            List<HSIOFunctionBlock> itemsToAdd = new List<HSIOFunctionBlock>();

            foreach (HSIOFunctionBlock hSIO in hSIOFunctionBlock)
            {
                var obj = new HSIOFunctionBlock(hSIO.Type, hSIO.Text, hSIO.DataType, hSIO.Value);
                itemsToAdd.Add(obj);
            }
            HSIOBlocks.AddRange(itemsToAdd);

        }
    }

    public struct HSIODrawing
    {
        public string Type { get; }
        public string Text { get; }
        public string DataType { get; }

    }
    public class HSIOFunctionBlockDraw
    {
        public string HSIOFunctionBlockName { get;}
        public string HSIOFunctionBlockType { get; }
        public List<HSIODrawing> hSIODrawings { get; set; }
        public HSIOFunctionBlockDraw()
        {
            hSIODrawings = new List<HSIODrawing>();
        }
    }
    public class CustomLinkLabel
    {
        public LinkLabel LinkLabel { get; set; }
        public int Index { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string DataType { get; set; }
    }
}
