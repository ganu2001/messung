using System;
using System.Drawing;
using System.Linq;

namespace LadderDrawing
{
    public class LadderBlock : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;
            this.Element = element;

            if (element.customDrawing == null)
                element.CreateCustom(new LadderBlock(), x, y, 50, 25);

            return element;
        }

        public void Paint(Pen newPen, Graphics graphics, LadderElement element)
        {
            int fixwidth;
            if (element.getX() < 50)
            {
                fixwidth = 50;
            }
            else
            {
                fixwidth = element.getX();
            }
            double elLegth = Convert.ToDouble(element.Attributes["caption"].ToString().Trim().Length) / 15;
            elLegth = (Convert.ToDouble(element.Attributes["caption"].ToString().Trim().Length) % 15) > 0 ? elLegth + 1 : elLegth;
            ///Changing x position of element to shift it's selection area with the element
            element.Position.X = fixwidth;
            DrawArea area = new DrawArea(fixwidth, element.getY() + 15, LadderDesign.ControlWidth * Convert.ToInt32(elLegth), LadderDesign.ControlHeight);

            area.Fill(Brushes.White, graphics);

            DrawArea centerarea = area.SplitY(0, 60);

            DrawArea center = centerarea;
            DrawArea ccenter = new DrawArea(center.x, center.y, center.width, center.height);
            DrawArea selectarea = ccenter.SplitX(80, 0);
            if (newPen.Color.B == Color.Blue.B && !CheckRungIsCommented(element))
                selectarea.Fill(new SolidBrush(Color.Blue), graphics);
            else
            {
                if (CheckRungIsCommented(element))
                    selectarea.Fill(Brushes.Gray, graphics);
                else
                    selectarea.Fill(Brushes.AliceBlue, graphics);

                selectarea.Border(new Pen(Color.DarkBlue), graphics);

            }
            string captiontext = element.Attributes["caption"].ToString().Trim();

            graphics.DrawString(captiontext.ToString(), LadderDesign.Font, new SolidBrush(Color.Black), new Point(element.getX() + 10, element.getY() + 25));
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

        public bool CheckRungIsCommented(LadderElement ladderElement)
        {
            // TO get rootElement 
            LadderElement rootElement = ladderElement.getRoot();
            //Find if First Ladder Element from rootLadderElement is Commented or Not
            LadderElement firstLadderElement = rootElement.Elements.First();
            foreach (LadderElement ld in rootElement.Elements)
            {
                foreach (Attribute attribute in ld.Attributes.ToList())
                {
                    if (attribute.Name == "isCommented")
                    {
                        Attribute newAttribute = new Attribute();
                        newAttribute.Name = "isCommented";
                        return true;
                    }
                }
            }
            return false;
        }
        public string toString()
        {
            return "LadderBlock";
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
        }
    }
}
