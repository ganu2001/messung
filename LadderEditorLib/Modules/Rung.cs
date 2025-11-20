using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    internal class Rung : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;

            if (element.customDrawing == null)
                element.CreateCustom(new Rung(), x, y, 0, 0);

            return element;
        }

        public void Paint(Color color, Graphics graphics, LadderElement element)
        {

            //graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(new Point(element.getX(), element.getY()), new Size(element.getWidth(), element.getHeight())));
            element.Position.Width = 1000;  
            graphics.FillRectangle(new SolidBrush(color), new Rectangle(new Point(element.getX(), element.getY()), new Size(10, (int)(element.getHeight() + (LadderDesign.ControlHeight / 2)))));

            Pen newsaperator = new Pen(Color.LightGray);
            newsaperator.Width = 4;
            graphics.FillRectangle(Brushes.White, new Rectangle(new Point(element.getX() + 10, element.getY() + element.getHeight() + (LadderDesign.ControlSpacing / 2) - 2), new Size(element.getWidth() + 1000, 5)));
            graphics.DrawLine(newsaperator, new Point(element.getX() + 10, element.getY() + element.getHeight() + (LadderDesign.ControlSpacing / 2)), new Point(element.getWidth() + 1000, element.getY() + element.getHeight() + (LadderDesign.ControlSpacing / 2)));
            Font font = LadderDesign.Font;
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            //graphics.DrawString("Rung No. " +  ((int) element.Position.Index + 1), font, new SolidBrush(Color.White), new PointF(element.getX()- 2, element.getY()), drawFormat);
            //graphics.DrawString("RUNG " + ((int)element.Position.Index + 1), font, new SolidBrush(Color.Black), new PointF(element.getX() + 10, element.getY() - 5));
            //graphics.DrawString("Comments :" + element.Attributes["Comment"].ToString().ToUpper(), font, new SolidBrush(Color.Black), new PointF(element.getX() + 10, element.getY() + 10));
        }

        public void Draw(Graphics graphics, LadderElement element)
        {
            Element = element;
            Paint(Color.Black, graphics, element);
        }

        public void OnSelect(Graphics graphics, LadderElement element)
        {
            //Paint(Color.Blue, graphics, element);
        }

        public string toString()
        {
            return "Rung";
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
            Paint(Color.DarkOrange, graphics, element);
        }
    }
}
