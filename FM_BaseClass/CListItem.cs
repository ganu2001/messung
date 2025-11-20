using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_BaseClass
{
    public class CListItem
    {
        public string m_sName;

        public string m_sComment;

        public uint m_dwID;

        public int m_nbIn;

        public int m_nbOut;

        public bool m_bInstanciable;

        public bool m_bDBObject;

        public CListItem()
        {
        }

        public override string ToString()
        {
            string str = string.Concat(this.m_sName, " (*", this.m_sComment, "*)");
            return str;
        }

    }
}
