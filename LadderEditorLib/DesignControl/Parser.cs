using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    public class Parser
    {
        public static Font Font;
        public static string GetNextString(string text,ref int index)
        {
            string breakChars = "`~!@#$%^&*()-=+{}[]\\;:'\"<>,./? \r\n\t";
            string rtnStr = "";
            int length = text.Length;
            while (index < length)
            {
                if (breakChars.IndexOf(text[index]) >= 0)
                {
                    index++;
                    break;
                }
                else
                    rtnStr += text[index];
                index++;
            }
            return rtnStr;
        }
        public static int SkipSpace(string text, ref int index)
        {
            string breakChars = " \r\n\t";
            int length = text.Length;
            while (index < length)
            {
                if (breakChars.IndexOf(text[index]) >= 0)
                {
                    index++;
                }
                else
                {
                    break;
                }
            }
            return index;
        }

        public static LadderDesign Parse(string text)
        { 
            LadderDesign design = new LadderDesign();
            //design.Font = Font;
            int index = 0;
            int length = text.Length;
            string tokenstr = "";

            while (index < length)
            {
                tokenstr = GetNextString(text,ref index).ToLower();
                SkipSpace(text, ref index);
                if (tokenstr == "rung")
                {
                    LadderElement newrung = design.InsertRung();
                    LadderElement lastelement = newrung.Elements[0];
                    while (index < length)
                    {
                        SkipSpace(text, ref index);

                        tokenstr = GetNextString(text, ref index).ToLower();

                        if (tokenstr == "contact")
                        {
                            LadderElement lastcontact = design.InsertContactAfter(ref lastelement);
                            lastelement = lastcontact;
                        }
                        else if (tokenstr == "functionblock")
                        {
                            LadderElement lastfunctionblock = design.InsertFBAfter(ref lastelement);
                            lastelement = lastfunctionblock;
                        }
                        else if (tokenstr == "end")
                            break;
                    }
                }
            }

            return design;
        }
    }
}
