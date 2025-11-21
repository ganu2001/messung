using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMPS2000.Core.Base.Helpers
{
    public class BaseComboBoxObject
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public static string GetTextById(int id)
        {
            return List.Find(i => i.ID == id)?.Text;
        }
        public static int GetIdByText(string text)
        {
            return List.Find(i => i.Text.Equals(text)).ID;
        }

        public static readonly List<BaseComboBoxObject> List;
    }
}
