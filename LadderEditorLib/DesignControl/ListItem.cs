using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    public class ListItem
    {
        public string Key { get; set; }
        public string Text{ get; set; }
    }

    public class ListItems : List<ListItem>
    {
        public void Add(string key,string text)
        { 
            ListItem item = new ListItem();
            item.Key = key; item.Text = text;
            base.Add(item);
        }
    }
}
