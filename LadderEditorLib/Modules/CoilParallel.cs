using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000.Core;

namespace LadderDrawing
{
    public class CoilParallel : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;

            if (element.customDrawing == null)
                element.CreateCustom(new CoilParallel(), x, y, LadderDesign.ControlSpacing, LadderDesign.ControlSpacing / 2);

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
            //Checking for Tag Name Changed.
            //foreach (Attribute attribute in element.Attributes.ToList())
            //{
                if (element.Attributes.Where(T => T.Name == "LogicalAddress").Any())
                {
                    string logicalAddress = element.Attributes["LogicalAddress"].ToString();
                    string tagname = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == logicalAddress).Select(T => T.Tag.Trim()).FirstOrDefault();
                    string currrentTagName = element.Attributes["caption"].ToString();
                    if (currrentTagName != tagname && tagname != null)
                    {
                        element.Attributes["caption"] = tagname;
                    }
                }
                else
                {
                    Attribute attribute1 = new Attribute();
                    attribute1.Name = "LogicalAddress";
                    string currrentTagName = element.Attributes["caption"].ToString();
                    string tagname = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag.Trim() == currrentTagName.Trim()).Select(T => T.LogicalAddress).FirstOrDefault();
                    if (tagname != null)
                    {
                        element.Attributes.Add(attribute1);
                        element.Attributes["LogicalAddress"] = tagname;
                    }
                }

            //}
            foreach (LadderElement checkCoil in element.getRoot().Elements)
            {
                if (checkCoil.customDrawing.toString() == "Coil")
                {
                    int fixwidth = 0;
                    if (checkCoil.getX() < 1000)
                    {
                        fixwidth = 1000;
                    }
                    else
                    {
                        fixwidth = checkCoil.getX();
                    }
                    if (fixwidth - LadderDesign.GetXofLastElement(checkCoil) < 150)
                    {
                        fixwidth = fixwidth + 150;
                    }
                    ///////// Changing x position of element to shift it's selection area with the element
                    checkCoil.Position.X = fixwidth;
                }
            }
            DrawArea area = new DrawArea(element.getX(), element.getY(), LadderDesign.ControlWidth, LadderDesign.ControlHeight);

            area.Fill(Brushes.White, graphics);

            DrawArea caption = area.SplitY(0, 25);
            DrawArea centerarea = area.SplitY(0, 60);
            DrawArea footer = area.SplitY(0, 15);

            DrawArea center = centerarea;
            DrawArea ccenter = new DrawArea(center.x, center.y, center.width, center.height);
            DrawArea selectarea = ccenter.SplitX(45, 0);
            if (newPen.Color.B == Color.Blue.B)
                selectarea.Fill(new SolidBrush(Color.LightCyan), graphics);
            else
            {
                if (XMPS.Instance.FindList.Where(T => T.Item1.Equals(element.Id)).Any() && element.Attributes["Caption"].ToString() == XMPS.Instance.FindList.Select(T => T.Item2).First())
                    selectarea.Fill(Brushes.Red, graphics);
                else
                    selectarea.Fill(Brushes.White, graphics);

            }
            //else
            //    selectarea.Fill(Brushes.White, graphics);

            graphics.DrawArc(newPen, new Rectangle(center.x, center.y + (center.height / 4), center.height / 2, center.height / 2), 270, -180);
            graphics.DrawArc(newPen, new Rectangle(center.x + 10, center.y + (center.height / 4), center.height / 2, center.height / 2), 270, 180);

            int x1 = element.getX();
            int x2 = element.getX() + (element.Position.Width / 2) - 20;

            int x3 = element.getX() + (element.Position.Width / 2) + 20;
            int x4 = element.getX() + element.Position.Width;

            int y1 = element.getY() + (element.Position.Height / 2);
            int y2 = element.getY() + (element.Position.Height / 2);

            int y3 = element.getY() + (element.Position.Height / 2);
            int y4 = element.getY() + (element.Position.Height / 2);

            x1 = element.getX() - LadderDesign.ControlSpacing;

            graphics.DrawLine(newPen, new Point(x1 + (LadderDesign.ControlSpacing / 2), y1), new Point(x1 + (LadderDesign.ControlSpacing / 2), y2 - ((LadderDesign.ControlSpacing * 2) - (center.height / 2) + 5)));
            graphics.DrawLine(newPen, new Point(element.getX(), element.getY() + (element.Position.Height / 2)), new Point(element.getX() - (LadderDesign.ControlSpacing / 2), element.getY() + (element.Position.Height / 2)));

            string SetResetStatus = element.Attributes["SetReset"].ToString();
            if (SetResetStatus != null)
            {
                //graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF(fixwidth - (int)strsz.Width - 30, TextY));
                graphics.DrawString(SetResetStatus.ToString(), LadderDesign.Font, new SolidBrush(Color.Black), new Point(center.x + 10, center.y + 12));
            }
            string captiontext = element.Attributes["caption"].ToString();
            if (captiontext.Contains("{"))
            {
                captiontext = captiontext.Substring(0, captiontext.IndexOf("{")).Trim();
            }
            var x = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == captiontext).FirstOrDefault();
            if (x != null) captiontext = x.ShowLogicalAddress == true ? x.LogicalAddress : captiontext;
            if (captiontext.Contains(":"))
            {
                caption.DrawString(0, captiontext, Brushes.RoyalBlue, LadderDesign.Font, graphics);
            }
            else if (captiontext != null)
            {
                if (LadderDesign.CheckinUDFB(element.Attributes["Caption"].ToString()))
                    caption.DrawString(0, captiontext.ToString(), new SolidBrush(Color.Blue), LadderDesign.Font, graphics);
                else
                    caption.DrawString(0, captiontext.ToString(), new SolidBrush(Color.Black), LadderDesign.Font, graphics);
            }


            /*
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(element.getX() + element.LineWidth, element.getY() - 20, element.Position.Width - element.LineWidth, element.Position.Height + 20));

            //graphics.DrawLine(newPen, new Point(x3, y3), new Point(x4, y4));

            //graphics.DrawEllipse(newPen, element.getX(), element.getY(), element.Position.Width / 2, element.Position.Height);
            graphics.DrawArc(newPen, new Rectangle(element.getX(), element.getY(), element.Position.Width / 3, element.Position.Height), 270, -180);
            graphics.DrawArc(newPen, new Rectangle(element.getX() + (element.Position.Width / 3), element.getY(), element.Position.Width / 3, element.Position.Height), 270, 180);

            /*
            if (element.Position.ConnectedTo != null)
            {
                int startcx = 0, startcy = 0;
                startcx = element.Position.ConnectedTo.getX();
                startcy = element.Position.ConnectedTo.getY() + (element.Position.Height / 2);
                graphics.DrawLine(newPen, new Point(element.getX() - 10, element.getY() + (element.Position.Height / 2)), new Point(startcx - 10, startcy));
                graphics.DrawLine(newPen, new Point(element.getX() - 10, element.getY() + (element.Position.Height / 2)), new Point(element.getX(), element.getY() + (element.Position.Height / 2)));
            }

            //draw text
            object caption = element.Attributes["caption"];
            if (caption != null)
            {
                caption = caption.ToString();
                graphics.DrawString(caption.ToString(), LadderDesign.Font, new SolidBrush(Color.Black), new Point(element.getX(), element.getY() - 20));
            }
            */
            //Checking if Rung is Commented or Not by taking first Elememt form the Rung
            LadderElement parentRung = element.getRoot();
            LadderElement firstElement = parentRung.Elements.Where(T => !T.CustomType.Equals("LadderDrawing.Comment")).FirstOrDefault();

            if (OnlineMonitoringStatus.isOnlineMonitoring)
            {
                //()
                //Checking Element is commented or not
                if(!firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any())
                {
                    captiontext = captiontext.Contains(":") ? XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == captiontext).Select(t => t.Tag).FirstOrDefault() : captiontext;
                    if (OnlineMonitoringStatus.AddressValues.TryGetValue(captiontext, out _) && OnlineMonitoringStatus.AddressValues[captiontext] == "1")
                    {
                        //graphics.DrawString("■", LadderDesign.Font, Brushes.Black, new Point(x2, y))
                        graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(center.x + 8, center.y + (center.height / 4) + 3, 13, 13));
                        if (SetResetStatus != null)
                        {
                            //graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF(fixwidth - (int)strsz.Width - 30, TextY));
                            graphics.DrawString(SetResetStatus.ToString(), LadderDesign.Font, new SolidBrush(Color.Black), new Point(center.x + 10, center.y + 12));
                        }
                    }
                    else
                        graphics.DrawRectangle(newPen, new Rectangle(center.x + 8, center.y + (center.height / 4) + 3, 13, 13));
                }
                
            }
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
            return "Parallel Coil";
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.DarkOrange), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }
    }
}
