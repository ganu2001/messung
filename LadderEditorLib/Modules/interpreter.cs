using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using LadderDrawing;

namespace LadderEditorLib.Modules
{
    public struct Objvalue
    {
        public string value;
        public string op;
    }

    public class interpreter
    {
        public static Stack<string> stack = new Stack<string>();
        public static List<Stack<string>> rungs = new List<Stack<string>>();

        public static List<string> _elements = new List<string>();
        public static List<string> _coil = new List<string>();
        public static Tuple<List<string>, List<string>> _curRung = new Tuple<List<string>, List<string>>(_coil, _elements);
        public static List<Tuple<List<string>, List<string>>> _curBlockRungs = new List<Tuple<List<string>, List<string>>>();
        //public static string getcurop(string str)
        //{
        //    if (str == "LadderDrawing.Contact") return " AND ";
        //    if (str == "LadderDrawing.ContactParallel") return " OR ";
        //    if (str == "LadderDrawing.Coil") return " =(";
        //    if (str == "LadderDrawing.FunctionBlock") return "fn";
        //    return "Error";
        //}

        public static Dictionary<string, string> getcurop = new Dictionary<string, string>()
        {
            {"LadderDrawing.Contact","AND" },
            {"LadderDrawing.ContactParallel","OR" },
            {"LadderDrawing.Coil","COIL" },
            {"LadderDrawing.FunctionBlock","fn" },
        };

        public static Objvalue getcur(LadderElement element)
        {
            Objvalue curElement;
            curElement.value = (string)element.Attributes.ElementAtOrDefault(0).Value ?? "";
            curElement.op = getcurop[element.CustomType] ?? "";
            return curElement;
        }

        public static void getparallel(LadderElement element)
        {
            stack.Push(getcur(element).value);
            if (element.Elements.Any())
            {
                stack.Push(getcur(element.Elements[0]).op);
                getparallel(element.Elements[0]);
            }
        }

        public static void getseries(LadderElement element)
        {
            stack.Push(getcur(element).op);
            stack.Push(getcur(element).value);
        }

        public static List<Tuple<List<string>, List<string>>> Run(LadderDesign data)
        {
            rungs.Clear();
            foreach (LadderElement rung in data.Elements)
            {
                stack.Clear();
                stack.Push(")");
                foreach (LadderElement dataelement in rung.Elements)
                {
                    if (dataelement.Elements.Any())
                    {
                        stack.Push(getcur(dataelement).op);
                        stack.Push(")");
                        getparallel(dataelement);
                        stack.Push("(");
                    }
                    else
                    {
                        getseries(dataelement);
                    }
                }
                Stack<string> tmp = new Stack<string>(stack.Reverse());
                rungs.Add(tmp);
            }

            _curBlockRungs.Clear();
            foreach (LadderElement rung in data.Elements)
            {
                _curRung.Item1.Clear();
                _curRung.Item2.Clear();
                foreach (LadderElement dataelement in rung.Elements)
                {
                    if (dataelement.Elements.Any())
                    {

                    }
                    else
                    {
                        Objvalue _curElement = getcur(dataelement);
                        if (_curElement.op == "COIL") { _curRung.Item1.Add(_curElement.value); break; }
                        _curRung.Item2.Add(_curElement.value);
                    }
                }
                Tuple<List<string>, List<string>> _temp = _curRung;
                _curBlockRungs.Add(_temp);
            }

            //data
            return _curBlockRungs;
        }

        public static string GetEquation(Tuple<List<string>, List<string>> _rung)
        {
            //string _equation = "";
            List<string> _equation = new List<string>();
            List<string> _coils = new List<string>();
            List<string> _elements = new List<string>();

            _equation.Add("(");
            foreach(string _curcoil in _rung.Item1)
            {
                _coils.Add(_curcoil);
            }
            string _coilEquation = string.Join(" OR ", _coils);
            _equation.Add(_coilEquation);
            _equation.Add(")");

            _equation.Add(" = ");

            _equation.Add("(");
            foreach(string _curelement in _rung.Item2)
            {
                _elements.Add(_curelement);
            }
            string _elementsEquation = string.Join(" AND ", _elements);
            _equation.Add(_elementsEquation);
            _equation.Add(")");

            return String.Join("", _equation);
        }
    }
}
