using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderEditorLib.DesignControl
{
    public class ExpressionToken
    { 
        public string Expression { get; set; }
        public string Token { set; get; }
        public string Address { get; set; }
        public ExpressionToken Parent { set; get; }
        public List<ExpressionToken> Tokens { get; set; }
        public ExpressionToken()
        { 
            Tokens = new List<ExpressionToken>();
        }
    }

    public class ExpressionReader
    {
        static string[] breakstrs = new string[] { "(", ")", " and ", " or ", "=", "," };

        public static string GetNextString(string text, ref int index)
        {
            string rtnstr = "";
            int length = text.Length;

            while (index < length)
            {
                bool found = false;
                for (int x = 0; x < breakstrs.Length; x++)
                { 
                    string match = "";

                    if(index + breakstrs[x].Length < length)
                        match = text.Substring(index, breakstrs[x].Length);

                    if (match.ToLower().Equals(breakstrs[x]))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
                else
                {
                    rtnstr += text[index];
                }
                index++;
            }

            return rtnstr;
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

        public static string GetNextToken(string text, int index)
        {
            string found = "";
            int length = text.Length;

            for (int x = 0; x < breakstrs.Length; x++)
            {
                string match = "";

                if (index + breakstrs[x].Length < length)
                    match = text.Substring(index, breakstrs[x].Length);

                if (match.ToLower().Equals(breakstrs[x]))
                {
                    found = breakstrs[x];
                    break;
                }
            }
            return found;
        }

        public static List<ExpressionToken> Read(string expressionstr)
        { 
            int index = 0;
            int length = expressionstr.Length;
            string assignto = "";
            string functioncall = "";
            int counter = 0;
            List<ExpressionToken> elements = new List<ExpressionToken>();
            ExpressionToken currentelement = null;

            while (index < length)
            { 
                string expression = GetNextString(expressionstr, ref index);
                string nexttoken = GetNextToken(expressionstr, index);
                bool start_bracket = false;
                bool end_bracket = false;
                char nexttokench = '\0';

                if (nexttoken.Length > 0)
                    nexttokench = nexttoken[0];

                if (nexttokench == '=')
                {
                    index++;
                    assignto = expression;
                }
                else if (nexttokench == '(')
                {
                    functioncall = expression;
                    start_bracket = true;
                    index++;
                    counter++;
                }
                else if (nexttokench == ')')
                {
                    functioncall = expression;
                    index++;
                    counter--;
                    end_bracket = true;

                    ExpressionToken lastelement = new ExpressionToken();
                    lastelement.Expression = expression;
                    lastelement.Token = ")";

                    currentelement.Tokens.Add(lastelement);

                    if (currentelement != null)
                    currentelement = currentelement.Parent;

                    if (counter == 0)
                    {
                        break;
                    }
                }
                SkipSpace(expressionstr, ref index);

                ExpressionToken element = null;
                functioncall = functioncall.Trim();
                nexttoken = nexttoken.Trim();
                string nexttokennt = nexttoken;

                if (start_bracket)
                {
                    element = new ExpressionToken();
                    element.Expression = functioncall;
                    element.Token = "Function";
                }

                if (nexttoken == "and" && !end_bracket)
                {
                    element = new ExpressionToken();
                    element.Expression = expression;
                    element.Token = "and";
                    index += 3;
                }
                else if (nexttoken == "or" && !end_bracket)
                {
                    element = new ExpressionToken();
                    element.Expression = expression;
                    element.Token = "or";
                    index += 2;
                }

                if (nexttoken == "," && expression == "")
                {
                    element = new ExpressionToken();
                    element.Expression = expression;
                    element.Token = ",";
                    index+= nexttokennt.Length;
                }
                if (nexttoken == "," && expression != "")
                {
                    element = new ExpressionToken();
                    element.Expression = expression;
                    element.Token = ",";
                    index += 1;
                }

                if (element != null)
                {
                    if (currentelement == null)
                    {
                        element.Parent = currentelement;
                        elements.Add(element);
                        currentelement = element;
                    }
                    else
                    {
                        element.Parent = currentelement;
                        currentelement.Tokens.Add(element);
                        currentelement = element;
                    }
                }
            }
            return elements;
        }
    }
}
