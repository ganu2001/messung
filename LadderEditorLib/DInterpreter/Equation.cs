using System.Collections.Generic;
using System.Linq;

namespace LadderEditorLib.DInterpreter
{
    public class Equation
    {

        //public static string GetChidEquation(element _curElement)
        //{
        //    return _curElement.name + " OR " + (_curElement.child != null ? GetChidEquation((element)_curElement.child) : "");
        //}


        /// <summary>
        /// Convert string of expression into simple string (contains values from dictionary) and dictionary which holds values to the keys in the simple string
        /// </summary>
        /// <param name="_equation">Expression to be converted</param>
        /// <returns>string, Dictionary<string, string></returns>
        public static (string, Dictionary<string, string>) SimplifyEquation(string _equation)
        {
            string sample = _equation;
            string exp = _equation.Replace(';', ' ');
            exp = exp.Remove(0, 2).Trim();
            exp = exp.Remove(exp.Length - 1, 1).Trim();

            Dictionary<string, string> simplifyDictionary = new Dictionary<string, string>();
            var myExp = exp;
            List<string> rung = new List<string>();
            List<string> operatorList = new List<string> { " AND ", " OR " };
            int dictionaryKey = 1;
            int i = -1;

            while (myExp.Contains('(') || myExp.Contains(')') || myExp.Contains('[') || myExp.Contains(']'))
            {
                i++;
                if (myExp[i] == ' ')
                    continue;
                if (myExp[i] == '(')
                {
                    for (int j = i + 1; j < myExp.Length; j++)
                    {
                        if (myExp[j] == '(')
                            break;
                        if (myExp[j] == '[')
                            break;
                        else if (myExp[j] == ')')
                        {
                            var str = myExp.Substring(i + 1, j - i - 1);
                            var trimmedStr = str.Trim();
                            var flag = false;
                            foreach (string s in operatorList)
                            {
                                if (trimmedStr.Contains(s))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                                myExp = myExp.Replace(myExp.Substring(i, str.Length + 2), trimmedStr);
                            else
                            {
                                foreach (string s in operatorList)
                                {
                                    if (trimmedStr.Contains(s))
                                    {
                                        dictionaryKey = rung.Count();
                                        var p = $"KEY{dictionaryKey}";
                                        myExp = myExp.Remove(i, j - i + 1).Insert(i, p);
                                        rung.Add(p + " = " + trimmedStr);
                                        simplifyDictionary.Add(p, trimmedStr);
                                    }
                                }
                            }
                            i = -1;
                            break;
                        }
                    }
                    continue;
                }
                if (myExp[i] == '[')
                {
                    for (int j = i + 1; j < myExp.Length; j++)
                    {
                        if (myExp[j] == ']')
                        {
                            string str = myExp.Substring(i + 1, j - i - 1);
                            string trimmedStr = str.Trim();
                            dictionaryKey = rung.Count();
                            var p = $"FN_KEY{dictionaryKey}";
                            myExp = myExp.Replace(myExp.Substring(i, j - i + 1), p);
                            rung.Add(p + " = " + trimmedStr);
                            simplifyDictionary.Add(p, trimmedStr);
                            i = -1;
                            break;
                        }
                    }
                }
            }
            return (myExp, simplifyDictionary);
        }

        /// <summary>
        /// Validate the rung for input present
        /// </summary>
        /// <param name="str">The string for validation</param>
        /// <returns>(True/False) if rung contains inputs then true</returns>
        public static bool Validate(string str)
        {
            // Flags
            bool inputPresent = false;
            string curExp = str;

            if (curExp.Contains("="))
            {
                string coilExp = curExp.Split('=')[0];
                string inpExp = curExp.Split('=')[1];


                coilExp = coilExp.Replace("(", "").Replace(")", "").Trim();

                // To check if output is present
                if (coilExp.Contains(","))
                {
                }
                if (inpExp.Contains("["))
                {
                    inputPresent = true;
                }

                // To check if inputs are present
                if (!inputPresent)
                {
                    if (inpExp.Contains("AND") || inpExp.Contains("OR"))
                    {
                        inputPresent = true;
                    }
                    else
                    {
                        if (inpExp.Replace("(", "").Replace(")", "").Trim() == ";")
                            inputPresent = false;
                        else
                            inputPresent = true;
                    }
                }

                // Return result
                if (inputPresent)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
