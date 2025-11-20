using System.Drawing;
using System.Linq;
using XMPS2000.Core;

namespace LadderDrawing
{
    public class Coil : CustomDrawing, ICustomDrawing
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
            int fixwidth = 0;
            if (element.getX() < 1000)
            {
                fixwidth = 1000;
            }
            else
            {
                fixwidth = element.getX();
            }
            if (fixwidth - LadderDesign.GetXofLastElement(element) < (LadderDesign.ControlWidth * 2))
            {
                fixwidth = fixwidth + (LadderDesign.ControlWidth * 2);
            }
            ///Changing x position of element to shift it's selection area with the element
            element.Position.X = fixwidth;
            DrawArea area = new DrawArea(fixwidth, element.getY(), LadderDesign.ControlWidth, LadderDesign.ControlHeight);

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

            graphics.DrawArc(newPen, new Rectangle(center.x, center.y + (center.height / 4), center.height / 2, center.height / 2), 270, -180);
            graphics.DrawArc(newPen, new Rectangle(center.x + 10, center.y + (center.height / 4), center.height / 2, center.height / 2), 270, 180);
            //extended line with all rung select connect to last contact
            int elementCount = element.getRoot().Elements.Count;
            int X = LadderDesign.GetXofLastElement(element) + (elementCount == 2 ? 0 : LadderDesign.ControlSpacing + (LadderDesign.ControlWidth / 2));
            int Y = center.y + (center.height / 2);
            graphics.DrawLine(newPen, new Point(X, Y), new Point(fixwidth, Y));
            string SetResetStatus = element.Attributes["SetReset"].ToString();
            if (SetResetStatus != null)
            {
                if (LadderDesign.CheckinUDFB(element.Attributes["Caption"].ToString()))
                    graphics.DrawString(SetResetStatus.ToString(), LadderDesign.Font, new SolidBrush(Color.Blue), new Point(center.x + 10, center.y + 12));
                else
                    graphics.DrawString(SetResetStatus.ToString(), LadderDesign.Font, new SolidBrush(Color.Black), new Point(center.x + 10, center.y + 12));
            }
            string captiontext = element.Attributes["caption"].ToString().Replace(" ", "");

            if (captiontext.Contains("{"))
            {
                captiontext = captiontext.Substring(0, captiontext.IndexOf("{")).Trim();
            }

            var x = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == captiontext).FirstOrDefault();
            if (x != null) 
                captiontext = (x.ShowLogicalAddress == true  || x.LogicalAddress.StartsWith("'")) ? x.LogicalAddress.Replace("'", "") : captiontext;
            else
                captiontext = !string.IsNullOrEmpty(element.Attributes["LogicalAddress"].ToString().Replace(" ", "").Replace("'", "")) ?
                        element.Attributes["LogicalAddress"].ToString().Replace(" ", "").Replace("'", "") : captiontext;
            if (captiontext.Contains(":"))
            {
                caption.DrawString(0, captiontext, Brushes.Black, LadderDesign.Font, graphics);
            }
            else if (captiontext != null)
            {
                if (LadderDesign.CheckinUDFB(element.Attributes["Caption"].ToString()))
                    caption.DrawString(0, captiontext.ToString(), new SolidBrush(Color.Blue), LadderDesign.Font, graphics);
                else
                    caption.DrawString(0, captiontext.ToString(), new SolidBrush(Color.Black), LadderDesign.Font, graphics);

            }

            //Checking if Rung is Commented or Not by taking first Elememt form the Rung
            LadderElement parentRung = element.getRoot();
            LadderElement firstElement = parentRung.Elements.Where(T => !T.CustomType.Equals("LadderDrawing.Comment")).FirstOrDefault();

            if (OnlineMonitoringStatus.isOnlineMonitoring && XMPS.Instance.LoadedProject.MainLadderLogic.Where(R => !R.StartsWith("'") && R.Contains(XMPS.Instance.CurrentScreen.Split('#')[1])).Count() > 0)
            {
                //()
                ///If show logical address is checked the pass actual tag on place of that logical address while showing OM result
                //Checking Element is commented or not
                if (!firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any())
                {
                    captiontext = captiontext.Contains(":") ? XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == captiontext).Select(t => t.Tag).FirstOrDefault() : captiontext;
                    if (OnlineMonitoringStatus.AddressValues.TryGetValue(captiontext, out _) &&  OnlineMonitoringStatus.AddressValues[captiontext] == "1")
                    {
                        graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(center.x + 8, center.y + (center.height / 4) + 3, 13, 13));
                        if (SetResetStatus != null)
                        {
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
