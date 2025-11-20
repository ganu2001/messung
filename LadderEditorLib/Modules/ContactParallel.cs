using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LadderDrawing
{
    public class ContactParallel : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;

            if (element.customDrawing == null)
                element.CreateCustom(new ContactParallel(), x, y, 50, 25);

            return element;
        }

        public void Paint(Pen newPen, Graphics graphics, LadderElement element)
        {
            int midpoint = (LadderDesign.ControlSpacing / 2);
            int x1 = 0, x2 = 0, x3 = 0, x4 = 0, y1 = 0, y2 = 0, y3 = 0, y4 = 0, maxHeight = 0;

            int startcx = 0, startcy = 0;

            //if (element.Position.RelateTo.Count > 0)
            //{
            //    maxHeight = element.Position.Height;
            //    element.Position.Y = maxHeight;
            //    element.Position._y = maxHeight;
            //}

            DrawArea area = new DrawArea(element.getX(), element.getY(), LadderDesign.ControlWidth, LadderDesign.ControlHeight);
            DrawArea caption = area.SplitY(0, 40);
            DrawArea centerarea = area.SplitY(0, 30);
            area.SplitY(0, 30);

            DrawArea center = centerarea;

            area.Fill(Brushes.White, graphics);

            if (newPen.Color.B == Color.Blue.B)
                center.Fill(new SolidBrush(Color.LightCyan), graphics);
            else
                center.Fill(Brushes.White, graphics);

            x1 = center.x;
            x2 = center.x + (center.width / 2) - 10;

            x3 = center.x + (center.width / 2) + 10;
            x4 = center.x + center.width;

            y1 = center.y + (center.height / 2);
            y2 = center.y + (center.height / 2);

            y3 = center.y + (center.height / 2);
            y4 = center.y + (center.height / 2);

            if (element.Position.RelateTo.Count > 0)
            {
                LadderElement useelement = element.Position.RelateTo[0]; //GetFirstElementInRelates(element); 
                startcx = useelement.getX() - midpoint;
                startcy = useelement.getY() + (area.height / 2);
                graphics.DrawLine(newPen, new Point(startcx, startcy), new Point(useelement.getX() - midpoint, center.y + (center.height / 2)));
                graphics.DrawLine(newPen, new Point(useelement.getX() - midpoint, center.y + (center.height / 2)), new Point(x1, y1));

                useelement = element.Position.RelateTo[element.Position.RelateTo.Count - 1]; //GetLastElementInRelates(element);
                startcx = useelement.getX() + useelement.Position.Width + midpoint;
                startcy = useelement.getY() + (area.height / 2);
            }
            else
            {
                startcx = element.Position.Parent.getX() - (LadderDesign.ControlSpacing / 2);
                startcy = element.Position.Parent.getY() + (element.Position.Height / 2);
                graphics.DrawLine(newPen, new Point(element.getX() - (LadderDesign.ControlSpacing / 2), element.getY() + (element.Position.Height / 2) + 2), new Point(startcx, startcy + 2));

                startcx = element.Position.Parent.getX() + element.Position.Width + (LadderDesign.ControlSpacing / 2);
                startcy = element.Position.Parent.getY() + (element.Position.Height / 2);
            }
            ////Draw Last Line of Parallel
            if (element.Position.ConnectTo.Count == 0)
            {
                int maxconnct = GetMaxConnect(element);

                if (maxconnct == 0)
                {
                    /// Last line of Parallel joining to the root 
                    graphics.DrawLine(newPen, new Point(element.getX() + element.Position.Width + (LadderDesign.ControlSpacing / 2), element.getY() + 6 + (LadderDesign.ControlSpacing / 2)), new Point(startcx, startcy + 5));
                }
                else
                {
                    /// Last line of Parallel joining to the root 
                    int newx = element.getX() + element.Position.Width + (LadderDesign.ControlSpacing) + (maxconnct * (element.Position.Width + (LadderDesign.ControlSpacing)));
                    graphics.DrawLine(newPen, new Point(newx, element.getY() + (element.Position.Height / 2) + 2), new Point(newx, startcy + 3));

                    ///Add Line to fix the gap _| between ending of last contact and start of upgoing line
                    graphics.DrawLine(newPen, new Point(newx, element.getY() + (element.Position.Height / 2) + 3), new Point(((element.getX() + element.Position.Width + LadderDesign.ControlSpacing) - 50), element.getY() + (element.Position.Height / 2) + 3));

                    if (element.Position.Parent.Position.Parent == element.Position.Parent.getRoot())
                    {
                        ///Add Line to fix the gap -- after parent and before next element 
                        int gapline = element.Position.Parent.Position.X + element.Position.Parent.Position.Width;
                        graphics.DrawLine(newPen, new Point(gapline, element.getY() - (element.Position.Height) + 13), new Point(gapline + 150, element.getY() - (element.Position.Height) + 13));
                    }
                }
            }
            else
            {
                int maxconnct = GetMaxConnect(element);
                ///Get last point of contact of last element added in the parallel
                int xOfLastcontact = GetLastElement(element);
                /// Last line of Parallel joining to the root 
                int newx = element.getX() + element.Position.Width + (LadderDesign.ControlSpacing) + (maxconnct * (element.Position.Width + (LadderDesign.ControlSpacing)));
                graphics.DrawLine(newPen, new Point(newx, element.getY() + 3 + (LadderDesign.ControlHeight / 2)), new Point(newx, startcy + 3));
                ///Add Line to fix the gap _| between ending of last contact and start of upgoing line or to connect last point of contact added in the parallel
                graphics.DrawLine(newPen, new Point(newx, element.getY() + (LadderDesign.ControlHeight / 2) + 3), new Point(xOfLastcontact - 50, element.getY() + (LadderDesign.ControlHeight / 2) + 3));
            }
            //// Draw contact design --| |--
            graphics.DrawLine(newPen, new Point(x1 - (LadderDesign.ControlSpacing / 2), y1), new Point(x2, y2));
            graphics.DrawLine(newPen, new Point(x2, center.y), new Point(x2, center.y + (center.height)));
            graphics.DrawLine(newPen, new Point(x3, y3), new Point(x4 + (LadderDesign.ControlSpacing / 2), y4));
            graphics.DrawLine(newPen, new Point(x3, center.y), new Point(x3, center.y + (center.height)));

            string ctext = Debugger.IsAttached ? element.Attributes["caption"].ToString() + "-" + element.getY().ToString() + "-" + element.getHeight().ToString() : element.Attributes["caption"].ToString();
            caption.DrawString(1, ctext, Brushes.Black, new Font(new FontFamily("Arial"), 10), graphics);
        }

        private LadderElement GetLastElementInRelates(LadderElement element)
        {
            LadderElements ladderElements = element.Position.RelateTo;
            LadderElement LastinRelate = new LadderElement();
            LastinRelate = element.Position.RelateTo[0];
            foreach (LadderElement CheckElement in ladderElements)
            {
                if (LastinRelate.getX() < CheckElement.getX())
                {
                    LastinRelate = CheckElement;
                }
            }
            return LastinRelate;
        }


        private LadderElement GetFirstElementInRelates(LadderElement element)
        {
            LadderElements ladderElements = element.Position.RelateTo;
            LadderElement LastinRelate = new LadderElement();
            LastinRelate = element.Position.RelateTo[0];
            foreach (LadderElement CheckElement in ladderElements)
            {
                if (LastinRelate.getX() > CheckElement.getX())
                {
                    LastinRelate = CheckElement;
                }
            }
            return LastinRelate;
        }

        private int GetMaxHeight(LadderElement element)
        {
            int maxHeight = 0;
            for (int x = 0; x < element.Position.RelateTo.Count; x++)
            {
                int height1 = element.Position.RelateTo[x].getHeight();
                if (maxHeight < height1)
                    maxHeight = height1;
            }

            return maxHeight;
        }

        private int GetLastElement(LadderElement element)
        {
            int x = element.getX();
            for (int cnt = 0; cnt < element.Position.ConnectTo.Count; cnt++)
            {
                if (x < (element.Position.ConnectTo[cnt].getX() + element.Position.ConnectTo[cnt].Position.Width + LadderDesign.ControlSpacing))
                    x = element.Position.ConnectTo[cnt].getX() + element.Position.ConnectTo[cnt].Position.Width + LadderDesign.ControlSpacing;
            }
            return x;
        }

        private int GetMaxConnect(LadderElement element)
        {
            int maxconnct = 0;
            LadderElement root = element.Position.Parent.getRoot();
            ///Get next element to the parent 
            if (GetParentIndex(element) >= 0)
            {
                LadderElement mainelement = root.Elements[GetParentIndex(element)];

                while (mainelement.Elements.Count > 0)
                {
                    if (mainelement.Elements.Count > 0)
                    {
                        for (int cnt = 0; cnt < mainelement.Elements.Count; cnt++)
                        {
                            if (maxconnct < mainelement.Elements[cnt].Position.ConnectTo.Count)
                                maxconnct = mainelement.Elements[cnt].Position.ConnectTo.Count;
                        }
                    }
                    mainelement = mainelement.Elements[0];
                }
            }
            return maxconnct;
        }

        private int GetParentIndex(LadderElement clickedElement)
        {
            if (clickedElement.Position.Parent != null)
            {
                if (clickedElement.Position.Parent.customDrawing.GetType() == typeof(Contact) || clickedElement.Position.Parent.customDrawing.GetType() == typeof(ContactNegation))
                {
                    return clickedElement.Position.Parent.Position.Index;
                }
                else
                {
                    return GetParentIndex(clickedElement.Position.Parent);
                }
            }
            return -1;
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
            return "ContactParallel";
        }
    }

}
