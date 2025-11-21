using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    internal class HorizontalLine : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;

            if (element.customDrawing == null)
                element.CreateCustom(new Contact(), x, y, 100, 100);

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
            DrawArea area = new DrawArea(element.getX(), element.getY(), LadderDesign.ControlWidth, LadderDesign.ControlHeight);
            DrawArea showArea = new DrawArea(element.getX(), element.getY(), LadderDesign.ControlWidth, LadderDesign.ControlHeight);
            showArea.x += 5;
            showArea.width -= 10;
            showArea.Fill(Brushes.White, graphics);

            DrawArea caption = area.SplitY(0, 40);
            DrawArea centerarea = area.SplitY(0, 30);
            //area.SplitY(0, 30);

            DrawArea center = centerarea;
            ////Add another variable eCenter to mark selection are it should be bit small to tackle line earasing issue with parallel contact
            DrawArea eCenter = area.SplitY(0, 30);
            eCenter.x += 5;
            eCenter.y -= 20;
            eCenter.width -= 10;
            if (newPen.Color.B == Color.Blue.B)
                eCenter.Fill(new SolidBrush(Color.LightCyan), graphics);
            else
                eCenter.Fill(Brushes.White, graphics);

            graphics.DrawLine(newPen, new Point(center.x, center.y + 9), new Point(center.x + LadderDesign.ControlWidth , center.y + 9));

            //extended line use this for testing to check where the horizontal line is ending 
            //graphics.DrawLine(newPen, new Point(element.getX() + LadderDesign.ControlWidth, element.getY() - 10), new Point(element.getX() + LadderDesign.ControlWidth , element.getY() + 10));

            string ctext = element.Attributes["caption"].ToString();
            caption.DrawString(1, ctext, Brushes.Black, new Font(new FontFamily("Arial"), 10), graphics);
        }

        public void Draw(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.Black), element.LineWidth);
            Paint(newPen, graphics, element);
        }

        public void OnSelect(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.Blue), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }

        public string toString()
        {
            return "HorizontalLine";
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.DarkOrange), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }
    }

}

