using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    public class Attribute
    {
        string m_Name = "";
        public string Name { set { m_Name = value; } get { return m_Name; } }

        Type m_Type = typeof(String);
        public Type Type { set { m_Type = value; } get { return m_Type; } }

        object m_Value = "";
        public object Value { set { m_Value = value; } get { return m_Value; } }
    }
}
