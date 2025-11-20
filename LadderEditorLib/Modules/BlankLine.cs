using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    internal class BlankLine : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;
            this.Element = element;

            if (element.customDrawing == null)
                element.CreateCustom(new Coil(), x, y, 50, 25);

            return element;
        }

        public void Paint(Pen newPen, Graphics graphics, LadderElement element)
        {
            foreach (Attribute attribute in element.Attributes)
            {
                if (attribute.Name.Equals("isCommented"))
                {
                    newPen = new Pen(new SolidBrush(Color.Gray), element.LineWidth);
                }
            }
            DrawArea area = new DrawArea(element.getX(), element.getY(), 800, 50);


            //extended line
            graphics.DrawLine(newPen, new Point(area.x, area.y + (area.height / 2)), new Point(area.x + area.width - 30, area.y + (area.height / 2)));
            graphics.FillRectangle(new SolidBrush(newPen.Color), new Rectangle(new Point(area.x + area.width - 30, area.y), new Size(10, area.height)));

           /*
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(element.getX() + element.LineWidth, element.getY() - 20, element.Position.Width - element.LineWidth, element.Position.Height + 20));

            graphics.DrawArc(newPen, new Rectangle(element.getX(), element.getY() + (element.Position.Height / 4), element.Position.Width / 3, element.Position.Height / 2), 270, -180);
            graphics.DrawArc(newPen, new Rectangle(element.getX() + (element.Position.Width / 3), element.getY() + (element.Position.Height / 4), element.Position.Width / 3, element.Position.Height / 2), 270, 180);

            graphics.DrawLine(newPen, new Point(element.getX(), element.getY() + (element.Position.Height / 2)), new Point(element.getX() - 70, element.getY() + (element.Position.Height / 2)));

            //draw text
            object caption = element.Attributes["caption"].ToString();
            if (caption != null)
            {
                caption = caption.ToString();
                graphics.DrawString(caption.ToString(), LadderDesign.Font, new SolidBrush(Color.Black), new Point(element.getX(), element.getY() - 20));
            }
            */
        }
        public void Draw(Graphics graphics, LadderElement element)
        {
            Element = element;
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
            return "Coil";
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.DarkOrange), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }
    }
}
