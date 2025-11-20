using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    public class CommonParallelNegation : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;

            if (element.customDrawing == null)
                element.CreateCustom(new CommonParallelNegation(), x, y, 50, 25);

            return element;
        }

        public void Paint(Pen newPen, Graphics graphics, LadderElement element)
        {
            int midpoint = (LadderDesign.ControlSpacing / 2);
            DrawArea area = new DrawArea(element.getX(), element.getY(), LadderDesign.ControlWidth, LadderDesign.ControlHeight);

            area.Fill(Brushes.White, graphics);

            DrawArea caption = area.SplitY(0, 40);
            DrawArea centerarea = area.SplitY(0, 30);
            area.SplitY(0, 30);

            DrawArea center = centerarea;

            if (newPen.Color.B == Color.Blue.B)
                center.Fill(new SolidBrush(Color.LightCyan), graphics);
            else
                center.Fill(Brushes.White, graphics);

            int x1 = center.x;
            int x2 = center.x + (center.width / 2) - 10;

            int x3 = center.x + (center.width / 2) + 10;
            int x4 = center.x + center.width;

            int y1 = center.y + (center.height / 2);
            int y2 = center.y + (center.height / 2);

            int y3 = center.y + (center.height / 2);
            int y4 = center.y + (center.height / 2);

            graphics.DrawLine(newPen, new Point(x1 - (LadderDesign.ControlSpacing / 2), y1), new Point(x2, y2));
            graphics.DrawLine(newPen, new Point(x2, center.y), new Point(x2, center.y + (center.height)));
            graphics.DrawLine(newPen, new Point(x3, y3), new Point(x4 + (LadderDesign.ControlSpacing / 2), y4));
            graphics.DrawLine(newPen, new Point(x3, center.y), new Point(x3, center.y + (center.height)));

            graphics.DrawLine(newPen, new Point(x3, center.y), new Point(x2, center.y + center.height));

            string ctext = element.Attributes["caption"].ToString();
            caption.DrawString(1, ctext, Brushes.Black, new Font(new FontFamily("Arial"), 10), graphics);

            if (element.Position.RelateTo.Count > 0)
            {
                int startcx = 0, startcy = 0;

                LadderElement useelement = element.Position.RelateTo[0];
                startcx = useelement.getX() - midpoint;
                startcy = useelement.getY() + (area.height / 2);
                graphics.DrawLine(newPen, new Point(startcx, startcy), new Point(useelement.getX() - midpoint, center.y + (center.height / 2)));
                graphics.DrawLine(newPen, new Point(useelement.getX() - midpoint, center.y + (center.height / 2)), new Point(x1, y1));

                useelement = element.Position.RelateTo[element.Position.RelateTo.Count - 1];
                startcx = useelement.getX() + useelement.Position.Width + midpoint;
                startcy = useelement.getY() + (useelement.Position.Height / 2);
                graphics.DrawLine(newPen, new Point(area.x + element.Position.Width + midpoint, center.y + (center.height / 2)), new Point(startcx, startcy));
            }


            /*
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(element.getX() + element.LineWidth, element.getY() - 20, element.Position.Width - element.LineWidth, element.Position.Height + 20));

            int x1 = element.getX();
            int x2 = element.getX() + (element.Position.Width / 2) - 10;

            int x3 = element.getX() + (element.Position.Width / 2) + 10;
            int x4 = element.getX() + element.Position.Width;

            int y1 = element.getY() + (element.Position.Height / 2);
            int y2 = element.getY() + (element.Position.Height / 2);

            int y3 = element.getY() + (element.Position.Height / 2);
            int y4 = element.getY() + (element.Position.Height / 2);


            graphics.DrawLine(newPen, new Point(x1, y1), new Point(x2, y2));
            graphics.DrawLine(newPen, new Point(x2, element.getY()), new Point(x2, element.getY() + (element.Position.Height)));
            graphics.DrawLine(newPen, new Point(x3, y3), new Point(x4, y4));
            graphics.DrawLine(newPen, new Point(x3, element.getY()), new Point(x3, element.getY() + (element.Position.Height)));

            graphics.DrawLine(newPen, new Point(x3, element.getY()), new Point(x2, element.getY() + element.Position.Height));

            if (element.Position.RelateTo.Count > 0)
            {
                int startcx = 0, startcy = 0;
                LadderElement useelement = element.Position.RelateTo[0];
                startcx = useelement.getX();
                startcy = useelement.getY() + (element.Position.Height / 2);
                graphics.DrawLine(newPen, new Point(startcx, startcy), new Point(useelement.getX(), element.getY() + (element.Position.Height / 2)));
                graphics.DrawLine(newPen, new Point(useelement.getX(), element.getY() + (element.Position.Height / 2)), new Point(x1, y1));

                useelement = element.Position.RelateTo[element.Position.RelateTo.Count - 1];
                startcx = useelement.getX() + element.Position.Width;
                startcy = useelement.getY() + (element.Position.Height / 2);
                graphics.DrawLine(newPen, new Point(element.getX() + element.Position.Width, element.getY() + (element.Position.Height / 2)), new Point(startcx, startcy));
            }

            //draw text
            /*
            object caption = element.Attributes["caption"];
            if (caption != null)
            {
                caption = caption.ToString();
                graphics.DrawString(caption.ToString(), LadderDesign.Font, new SolidBrush(Color.Black), new Point(element.Position.ConnectedTo.getX(), element.getY() - 20));
            }*/
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
            return "CommonParallelNegation";
        }
    }
}
