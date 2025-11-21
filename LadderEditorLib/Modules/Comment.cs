using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LadderDrawing
{
    internal class Comment : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;

            if (element.customDrawing == null)
                element.CreateCustom(new Contact(), x, y , 100, 100);

            return element;
        }

        public void Paint(Pen newPen, Graphics graphics, LadderElement element)
        {
            foreach (Attribute attribute in element.Attributes)
            {
                if (attribute.Name.Equals("isCommented"))
                {
                    newPen = new Pen(new SolidBrush(Color.DarkGray), element.LineWidth);
                }
            }
            //Avioiding painting of Comment as instructed by Sagar
            DrawArea area = new DrawArea(15, element.getY(), 1000, 17);                    
            area.Fill(Brushes.WhiteSmoke, graphics); //GhostWhite
            string ctext = "    Rung  " + ((int)element.getRoot().Position.Index + 1  + " " +element.Attributes["caption"].ToString()).ToString() ;
            area.DrawString(2, ctext, Brushes.Green, new Font(new FontFamily("Arial"), 10, FontStyle.Italic), graphics);
            
        }

        public void Draw(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.Green), element.LineWidth);
            Paint(newPen, graphics, element);
        }

        public void OnSelect(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.Green), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }

        public string toString()
        {
            return "Comment";
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.Green), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }
    }

}

