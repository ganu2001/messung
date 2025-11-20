using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using XMPS2000.Core;
using XMPS2000.Core.Base;

namespace LadderDrawing
{
    public class Contact : CustomDrawing, ICustomDrawing
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
            if (CheckRungIsCommented(element))
            {
                newPen = new Pen(new SolidBrush(Color.DarkGray), element.LineWidth);
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
            DrawArea area = new DrawArea(element.getX(), element.getY(), LadderDesign.ControlWidth, LadderDesign.ControlHeight);
            DrawArea showArea = new DrawArea(element.getX(), element.getY(), LadderDesign.ControlWidth, LadderDesign.ControlHeight);
            int startcx = 0, startcy = 0;
            ///Reduce the crearing are so that parallel lines intersecting the area won't get cleared
            showArea.x += 5;
            showArea.width -= 10;
            showArea.Fill(Brushes.White, graphics);

            DrawArea caption = area.SplitY(0, 40);
            DrawArea center = area.SplitY(0, 30);
            ////Add another variable eCenter to mark selection are it should be bit small to tackle line earasing issue with parallel contact
            DrawArea eCenter = area.SplitY(0, 30);
            eCenter.x += 5;
            eCenter.y -= 20;
            eCenter.width -= 10;
            if (newPen.Color.B == Color.Blue.B)
                eCenter.Fill(new SolidBrush(Color.LightCyan), graphics);
            else
            {
                //foreach(string ele in )
                if (XMPS.Instance.FindList.Where(T => T.Item1.Equals(element.Id)).Any() && element.Attributes["Caption"].ToString() == XMPS.Instance.FindList.Select(T => T.Item2).First())
                    eCenter.Fill(Brushes.Red, graphics);
                else
                    eCenter.Fill(Brushes.White, graphics);
            }
            int x1 = center.x;
            int x2 = x1 + (center.width / 2) - 10;
            int x3 = x1 + (center.width / 2) + 10;
            int x4 = x1 + center.width;
            int y = center.y + (center.height / 2);

            if (element.Position.RelateTo.Count > 0)
            {
                LadderElement useelement = element.Position.RelateTo[0];  //GetFirstElementInRelates(element); 
                x1 = useelement.getX();
                x2 = x1 + (center.width / 2) - 10;
                x3 = x1 + (center.width / 2) + 10;
                x4 = x1 + center.width;
                if (element.Position.Parent.customDrawing.toString() == "DummyParallelParent")
                {
                    ////If this is first dummy parallel which is placed on rung then adjust x of this parallel
                    ///to x of first parent otherwise set this to 0 so that all following parallels consider 
                    ///first parent and get drawn properly
                    if (element.Position.Parent.Position.Parent.customDrawing.toString() == "Rung")
                        element.Position.Parent.Position.X = x1;
                    else
                        element.Position.Parent.Position.X = 0;
                }
                element.Position.X = 0;
                int connectLength = element.Position.ConnectTo.Count;
                if (connectLength < element.Position.RelateTo.Count)
                {
                    connectLength = element.Position.RelateTo.Count;
                }
                startcx = useelement.getX();
                startcy = useelement.getY() + (area.height / 2);
                /////Plot the line conntecting to parent
                graphics.DrawLine(newPen, new Point(startcx, startcy + 3), new Point(useelement.getX(), center.y + (center.height / 2)));
                useelement = element.Position.RelateTo[element.Position.RelateTo.Count - 1]; // GetLastElementInRelates(element); //
                ////Check for all Horizontal lines added after last element and point to last horizontal lines last line to map parallel properly
                if ((element.getRoot().Elements.Count > useelement.Position.Index + 1) && connectLength >= 1)
                {
                    while (element.getRoot().Elements[useelement.Position.Index + 1].customDrawing.toString() == "HorizontalLine")
                        useelement = element.getRoot().Elements[useelement.Position.Index + 1];
                }
                startcx = useelement.getX() + useelement.Position.Width;
                startcy = useelement.getY() + (area.height / 2);
                ///Check if this contact is having more parents than siblings
                ////////////Line to connect end of parallel contact to the end of last parent ______(|)
                int maxConnected = (LadderDesign.GetMaxConnect(element));
                if ((element.Position.ConnectTo.Count < element.Position.RelateTo.Count) && (element.Position.RelateTo.Count > 1 && maxConnected > 0))
                {
                    if (maxConnected > element.Position.RelateTo.Count)
                        //(startcx + element.Position.Width) + ((maxConnected - element.Position.RelateTo.Count) * element.Position.Width)
                        graphics.DrawLine(newPen, new Point(x4 + (element.Position.ConnectTo.Count * element.Position.Width), y), new Point(startcx, y));
                    //startcx = startcx;
                    else
                        graphics.DrawLine(newPen, new Point(x4 + (element.Position.ConnectTo.Count * element.Position.Width), y), new Point(startcx, y));
                }
                else
                {
                    if (maxConnected > element.Position.RelateTo.Count && maxConnected > element.Position.ConnectTo.Count && x4 == startcx)
                        graphics.DrawLine(newPen, new Point(x4 + (element.Position.ConnectTo.Count * element.Position.Width), y), new Point((startcx) + (maxConnected * element.Position.Width), y));
                    else if (element.Position.ConnectTo.Count == 0 || maxConnected != element.Position.ConnectTo.Count || (x4 + (element.Position.ConnectTo.Count * element.Position.Width)) < startcx)
                        graphics.DrawLine(newPen, new Point(x4 + (element.Position.ConnectTo.Count * element.Position.Width), y), new Point(startcx, y));
                }
                ////Draw Last Line of Parallel
                if (element.Position.ConnectTo.Count == 0)
                {
                    int maxconnct = LadderDesign.GetMaxConnect(element);
                    if (maxconnct == 0)
                    {
                        /// Last line of Parallel joining to the root 
                        graphics.DrawLine(newPen, new Point(startcx, startcy + 3), new Point(startcx, center.y + (center.height / 2)));
                    }
                    else
                    {
                        maxconnct = maxconnct - element.Position.ConnectTo.Count;
                        /// Last line of Parallel joining to the root 
                        int newx = element.getX() + element.Position.Width + (maxconnct * (element.Position.Width));
                        ///If horizontal line is added in main rung then startcx will get increased and if the contact is having lesser siblings 
                        ///then connect to the line after horizontal line and not only after the end of siblings of this contact or
                        ///contacts added parallel in parent of this contact
                        if (startcx > newx)
                            newx = startcx;
                        graphics.DrawLine(newPen, new Point(newx, element.getY() + (element.Position.Height / 2) + 2), new Point(newx, startcy + 3));
                        ///Add Line to fix the gap _| between ending of last contact and start of upgoing line
                        graphics.DrawLine(newPen, new Point(newx, element.getY() + (element.Position.Height / 2) + 3), new Point(((element.getX() + element.Position.Width)), element.getY() + (element.Position.Height / 2) + 3));
                    }
                }
                else
                {
                    int maxconnct = LadderDesign.GetMaxConnect(element);
                    if (maxconnct < element.Position.RelateTo.Count - 1)
                    {
                        maxconnct = element.Position.RelateTo.Count - 1;
                    }
                    ///Get last point of contact of last element added in the parallel
                    int xOfLastcontact = GetLastElement(element);
                    /// Last line of Parallel joining to the root 
                    int newx = element.getX() + element.Position.Width + ((maxconnct) * (element.Position.Width));
                    if (startcx > newx)
                        newx = startcx;
                    graphics.DrawLine(newPen, new Point(newx, element.getY() + 3 + (LadderDesign.ControlHeight / 2)), new Point(newx, startcy + 3));
                }
            }
            //// Draw contact design --| |--
            graphics.DrawLine(newPen, new Point(x1, y), new Point(x2, y));
            graphics.DrawLine(newPen, new Point(x2, center.y), new Point(x2, center.y + (center.height)));
            graphics.DrawLine(newPen, new Point(x3, center.y), new Point(x3, center.y + (center.height)));
            graphics.DrawLine(newPen, new Point(x3, y), new Point(x4, y));


            string ctext = element.Attributes["caption"].ToString();
            var x = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == ctext).FirstOrDefault();
            if (x != null)
                ctext = (x.ShowLogicalAddress == true || x.LogicalAddress.StartsWith("'")) ? x.LogicalAddress.Replace("'", "") : ctext;
            else
                ctext = !string.IsNullOrEmpty(element.Attributes["LogicalAddress"].ToString().Replace(" ", "").Replace("'", "")) ?
                        element.Attributes["LogicalAddress"].ToString().Replace(" ", "").Replace("'", "") : ctext;
            string PNStatus = element.Attributes["PNStatus"].ToString();
            //If showLogicalAddress == true then chanege the Color of text.
            //string ctext = element.Attributes["caption"].ToString();

            //Checking if Rung is Commented or Not by taking first Elememt form the Rung
            LadderElement parentRung = element.getRoot();
            LadderElement firstElement = parentRung.Elements.Where(T => !T.CustomType.Equals("LadderDrawing.Comment")).FirstOrDefault();

            if (ctext.Contains(":"))
            {
                caption.DrawString(1, ctext, Brushes.Black, LadderDesign.Font, graphics);
            }
            else
            {
                ctext = element.Attributes["caption"].ToString();
                if (LadderDesign.CheckinUDFB(element.Attributes["Caption"].ToString()))
                    caption.DrawString(1, ctext, Brushes.Red, LadderDesign.Font, graphics);
                else
                    caption.DrawString(1, ctext, Brushes.Black, LadderDesign.Font, graphics);
                if (LadderDesign.CheckIfForced(ctext) && OnlineMonitoringStatus.isOnlineMonitoring && !firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any()) graphics.DrawString("F", LadderDesign.Font, Brushes.Red, new PointF(x2 + 3, y + 5));
                //caption.DrawString(1, ctext, Brushes.Black, LadderDesign.Font, graphics);
            }
            //caption.DrawString(1, ctext, Brushes.Black, LadderDesign.Font, graphics);
            if (OnlineMonitoringStatus.isOnlineMonitoring && ((XMPS.Instance.LoadedProject.MainLadderLogic.Where(R => !R.StartsWith("'") && R.Contains(XMPS.Instance.CurrentScreen.Split('#')[1])).Count() > 0) || XMPS.Instance.CurrentScreen.Split('#')[1] == "Main") &&
                (OnlineMonitoringStatus.AddressValues.ContainsKey(ctext) || OnlineMonitoringStatus.AddressValues.ContainsKey(element.Attributes["caption"].ToString())))
            {
                //()
                //Checking Element is commented or not
                if (!firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any())
                {
                    ctext = ctext.Contains(":") ? XMPS.Instance.LoadedProject.Tags.Where(t => t.LogicalAddress == ctext).Select(t => t.Tag).FirstOrDefault() : ctext;
                    if (OnlineMonitoringStatus.AddressValues.TryGetValue(ctext, out _) &&  (OnlineMonitoringStatus.AddressValues[ctext] == "0" && (!element.Negation && PNStatus != "N")) || (OnlineMonitoringStatus.AddressValues[ctext] == "1" && (element.Negation || PNStatus == "N")))
                    {
                        //graphics.DrawString("■", LadderDesign.Font, Brushes.Black, new Point(x2, y));
                        graphics.DrawRectangle(newPen, new Rectangle(x2 + 5, y - 6, 10, 10));
                        //graphics.FillRectangle(Brushes.SpringGreen, new Rectangle(x2 + 5, y - 6, 11, 11));
                    }
                    else
                        graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(x2 + 5, y - 6, 11, 11));
                }
            }

            if (PNStatus != null)
            {
                graphics.DrawString(PNStatus.ToString(), LadderDesign.Font, new SolidBrush(Color.Black), new Point(x2 + 5, center.y + 2));
            }
            ///Check for negation and draw line / if negation is applied
            if (element.Negation)
            {
                graphics.DrawLine(newPen, new Point(x3, center.y), new Point(x2, center.y + center.height));
            }
        }

        private int GetLastElement(LadderElement element)
        {
            int x = element.getX();
            for (int cnt = 0; cnt < element.Position.ConnectTo.Count; cnt++)
            {
                if (x < (element.Position.ConnectTo[cnt].getX() + element.Position.ConnectTo[cnt].Position.Width))
                    x = element.Position.ConnectTo[cnt].getX() + element.Position.ConnectTo[cnt].Position.Width;
            }
            return x;
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
            return "Contact";
        }

        public bool CheckRungIsCommented(LadderElement ladderElement)
        {
            // TO get rootElement 
            LadderElement rootElement = ladderElement.getRoot();
            //Find if First Ladder Element from rootLadderElement is Commented or Not
            LadderElement firstLadderElement = rootElement.Elements.First();
            foreach (Attribute attribute in firstLadderElement.Attributes.ToList())
            {
                if (attribute.Name == "isCommented")
                {
                    Attribute newAttribute = new Attribute();
                    newAttribute.Name = "isCommented";
                    return true;
                }
            }
            return false;
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.DarkOrange), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }
    }

}
