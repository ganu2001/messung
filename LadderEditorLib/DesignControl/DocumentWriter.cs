using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderEditorLib.DesignControl
{
    public interface Writer
    {
        StringBuilder Content { set; get; }
        LadderElements Elements { set; get; }
        string Write(LadderElement element);
    }

    public class DocumentWriter : Writer
    {
        StringBuilder content = new StringBuilder();
        public StringBuilder Content { get; set; }
        public LadderElements Elements { get; set; }

        public string Write(LadderElement element)
        {
            string rtnStr = "";

            //code to generate xml/csv/mcode goes here

            //write to content
            content.Append(rtnStr);

            //prepare child elements
            for (int k = 0; k < element.Elements.Count; k++)
            {
                Write(element.Elements[k]);
            }
            return rtnStr;
        }

        public void Write()
        {
            if(Elements != null)
            for (int i = 0; i < Elements.Count; i++)
            {
                Write(Elements[i]);
            }
        }
    }
}
