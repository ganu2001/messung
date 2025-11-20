using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using XMPS2000.Core;
using XMPS2000.Core.Base;

namespace LadderDrawing
{
    public class FunctionBlock : CustomDrawing, ICustomDrawing
    {
        int rightx;
        public LadderElement Create(int x, int y)
        {
            LadderElement functionblock = new LadderElement();
            functionblock.Position.X = x;
            functionblock.Position.Y = y;

            if (functionblock.customDrawing == null)
                functionblock.CreateCustom(new FunctionBlock(), x, y, 0, 45);

            return functionblock;
        }

        public void Paint(Pen newPen, Graphics graphics, LadderElement functionblock)
        {
            //To get Root Element 
            LadderElement rootLadderElement = functionblock.getRoot();
            Pen penShowLogicalAddress = new Pen(new SolidBrush(Color.RoyalBlue), functionblock.LineWidth);
            //Find if First Ladder Element from rootLadderElement is Commented or Not
            LadderElement firstLadderElement = rootLadderElement.Elements.First();
            foreach (Attribute attribute in firstLadderElement.Attributes)
            {
                if (attribute.Name == "isCommented")
                {
                    Attribute newAttribute = new Attribute();
                    newAttribute.Name = "isCommented";
                    functionblock.Attributes.Add(newAttribute);
                }
            }
            foreach (Attribute attribute in functionblock.Attributes)
            {
                if (attribute.Name.Equals("isCommented"))
                {
                    newPen = new Pen(new SolidBrush(Color.DarkGray), functionblock.LineWidth);
                }
            }
            int fixwidth = 0, endOfLastElement = (functionblock.getRoot().Elements.Count > 2) ? LadderDesign.GetXofLastElement(functionblock) + LadderDesign.ControlWidth : LadderDesign.StartX;
            if (functionblock.getX() < 800)
            {
                fixwidth = 800;
            }
            else
            {
                if (endOfLastElement > 600)
                {
                    fixwidth = functionblock.getX();
                }
                else
                {
                    fixwidth = 800;
                }
            }
            if (fixwidth - (endOfLastElement) < 100)
            {
                fixwidth = fixwidth + LadderDesign.ControlWidth + (LadderDesign.ControlWidth / 2);
            }
            var udfbInfo = XMPS.Instance.LoadedProject.UDFBInfo
                          .FirstOrDefault(u => u.UDFBName.IndexOf(functionblock.Attributes["function_name"].ToString(),
                          StringComparison.OrdinalIgnoreCase) >= 0);
            //creating Dictionary for saving outputs and its postion of string.
            Dictionary<int, int> outputsAndHeight = new Dictionary<int, int>();

            //functionblock.Position.Parent.Position.Width += 100;
            int ToalInputs = GetInputs(functionblock);
            int fixHeight = 140;
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(fixwidth, functionblock.getY(), functionblock.Position.Width - functionblock.LineWidth - 2 + 25, functionblock.Position.Height + 20));
            if (ToalInputs < 1) ToalInputs = 2;
            functionblock.Position.Height = fixHeight - ((4 - ToalInputs) * 25) + 25;
            functionblock.Position.X = fixwidth;
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(fixwidth, functionblock.getY(), functionblock.Position.Width - functionblock.LineWidth - 2 + 25, functionblock.Position.Height + 10));
            graphics.DrawRectangle(new Pen(Color.Black, functionblock.LineWidth + 1), new Rectangle(fixwidth + functionblock.LineWidth, functionblock.getY(), functionblock.Position.Width - functionblock.LineWidth - 2 + 20, fixHeight - ((4 - ToalInputs) * 25) + 30));
            rightx = fixwidth + functionblock.LineWidth + (functionblock.Position.Width - functionblock.LineWidth - 2 + 20);
            Font font = LadderDesign.Font;

            SizeF size = graphics.MeasureString(functionblock.Attributes["function_name"].ToString().ToUpper(), font);
            string functionBlockName = functionblock.Attributes["function_name"].ToString();
            if (functionBlockName.Length > 10)
            {
                graphics.DrawString(functionblock.Attributes["function_name"].ToString().Substring(0, 10).ToUpper(), font, new SolidBrush(newPen.Color),
                        new PointF(fixwidth + 10 + (functionblock.Position.Width / 2)
                        - (graphics.MeasureString(functionblock.Attributes["function_name"].ToString().Substring(0, 10)
                        .ToUpper(), font).Width / 2), functionblock.getY() + 4));
                graphics.DrawString(functionblock.Attributes["function_name"].ToString().Substring(10).ToUpper(), font, new SolidBrush(newPen.Color),
                    new PointF(fixwidth + 10 + (functionblock.Position.Width / 2)
                    - (graphics.MeasureString(functionblock.Attributes["function_name"].ToString().Substring(10).ToUpper(), font).Width / 2),
                    functionblock.getY() + 15));
            }
            else
                graphics.DrawString((functionblock.Attributes["function_name"].ToString().StartsWith("MES_PID_") && XMPS.Instance.CurrentScreen.StartsWith("UDFLadderForm")) ? "MES_PID_#" :
                                     functionblock.Attributes["function_name"].ToString().ToUpper(), font, new SolidBrush(newPen.Color), new PointF(fixwidth + 10 + (functionblock.Position.Width / 2) - (size.Width / 2), functionblock.getY() + 4));

            size = graphics.MeasureString("op", font);

            font = new Font(font.FontFamily, 8);
            int x1 = 0, y1 = 0;
            x1 = endOfLastElement;//(LadderDesign.ControlSpacing * (functionblock.getRoot().Elements.Count() - 1) * 2);
            //GetLastElement(functionblock); //
            y1 = functionblock.getY() + (LadderDesign.ControlHeight / 2);

            graphics.DrawLine(newPen, new Point(endOfLastElement, y1 + 3), new Point(fixwidth, y1 + 3));

            //functionblock.Attributes["input1"].ToString()
            int TextY = functionblock.getY() + 25;

            graphics.DrawString("EN", font, new SolidBrush(newPen.Color), new PointF(fixwidth + 4, TextY));
            if (functionblock.Attributes["TCName"].ToString() != "-" && !XMPS.Instance.CurrentScreen.StartsWith("UDFLadderForm"))
            {
                graphics.DrawString(functionblock.Attributes["TCName"].ToString(), font, new SolidBrush(newPen.Color), new PointF(fixwidth + 40, TextY));
            }
            TextY = TextY + 25;

            //Checking if Rung is Commented or Not by taking first Elememt form the Rung
            LadderElement parentRung = functionblock.getRoot();
            LadderElement firstElement = parentRung.Elements.Where(T => !T.CustomType.Equals("LadderDrawing.Comment")).FirstOrDefault();

            List<string> lstInput = new List<string> { };
            lstInput.AddRange(functionblock.Attributes.Where(t => t.Name.StartsWith("in") && t.Value != null && !t.Value.ToString().Equals("-")).Select(t => t.Name).ToList());
            List<string> lstOutput = new List<string> { };
            lstOutput.AddRange(functionblock.Attributes.Where(t => t.Name.StartsWith("out") && t.Value != null && !t.Value.ToString().Equals("-") && !t.Value.ToString().Contains("A5:999")).Select(t => t.Name).ToList());
            int maxcnt = Math.Max(lstInput.Count(), lstOutput.Count());
            if (maxcnt == 0)
            {
                lstInput.Add("input1");
                lstInput.Add("input2");
                lstOutput.Add("output1");
                maxcnt = 2;

            }
            int printed_output = 0;
            int output = 0;
            for (int i = 0; i < maxcnt; i++)
            {
                string input = "";
                if (i < lstInput.Count)
                {
                    input = lstInput[i];
                    if (functionblock.Attributes[input].ToString().Length > 0 || int.TryParse(functionblock.Attributes[input].ToString(), out int ip1))
                    {
                        string str = "";
                        string curInput = functionblock.Attributes[input].ToString();
                        if (curInput.Contains("~"))
                        {
                            curInput = curInput.Replace("~", "");
                            str = "~";
                        }

                        str += XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == curInput).Count() > 0 ? XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == curInput).Select(L => L.Tag).First().ToString() : curInput;
                        var x = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == str.Replace("~", "")).FirstOrDefault();
                        if (x != null) str = x.ShowLogicalAddress == true ? x.LogicalAddress : str;
                        SizeF strsz = graphics.MeasureString(str, font, 0, StringFormat.GenericTypographic);

                        //// Show back colour of address value in red if it is used in find list or clear the background of earlier value
                        if (x != null && (XMPS.Instance.FindList.Where(T => T.Item1.Equals(functionblock.Id)).Any() && str.Replace("~", "") == XMPS.Instance.FindList.Select(T => T.Item2).First()
                                         || XMPS.Instance.FindList.Where(T => T.Item1.Equals(functionblock.Id)).Any() && x.Tag.ToString() == XMPS.Instance.FindList.Select(T => T.Item2).First()))
                        {
                            graphics.FillRectangle(Brushes.Red, new Rectangle(new Point(fixwidth - 150, TextY), new Size(149, 15)));
                        }
                        else
                            graphics.FillRectangle(Brushes.White, new Rectangle(new Point(fixwidth - 150, TextY), new Size(149, 15)));

                        graphics.DrawLine(newPen, new Point(fixwidth - 15, TextY + 5), new Point(fixwidth, TextY + 5));

                        //// Show "F" with address value if the value is forced
                        if (LadderDesign.CheckIfForced(str) && OnlineMonitoringStatus.isOnlineMonitoring && !firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any())
                            graphics.DrawString("F", font, Brushes.Red, new PointF(fixwidth - 25, TextY - 7));

                        //// Show outer text of function block inputs i.e. address name or address values 
                        if (IsShowLogicalAddress(str))
                            graphics.DrawString(str, font, new SolidBrush(penShowLogicalAddress.Color), new PointF(fixwidth - (int)strsz.Width - 30, TextY));
                        else if (LadderDesign.CheckinUDFB(str))
                            graphics.DrawString(str, font, Brushes.DarkGreen, new PointF(fixwidth - (int)strsz.Width - 30, TextY)); //new PointF(fixwidth + 4, TextY)
                        else
                            graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF(fixwidth - (int)strsz.Width - 30, TextY));
                        //// Show inner text of function block i.e. input names
                        if (udfbInfo != null)
                        {
                            DrawText(graphics, udfbInfo, functionblock, input, "IN", fixwidth + 6, TextY, font, newPen);
                        }
                        else
                            graphics.DrawString(GetHeading("IN" + (i + 1), functionblock.Attributes["function_name"].ToString(), (i + 1), "Input"), font, new SolidBrush(newPen.Color), new PointF(fixwidth + 4, TextY));
                    }
                    else
                    {
                        if (functionblock.Attributes[input].ToString() == "input1")
                        {
                            SizeF strsz = graphics.MeasureString("???", font, 0, StringFormat.GenericTypographic);
                            graphics.FillRectangle(Brushes.White, new Rectangle(new Point(fixwidth - ((int)strsz.Width + 50), TextY + output), new Size((int)strsz.Width + 50, (int)strsz.Height)));
                            graphics.DrawLine(newPen, new Point(fixwidth - 15, TextY + output + 5), new Point(fixwidth, TextY + output + 5));
                            graphics.DrawString("???", font, new SolidBrush(newPen.Color), new PointF(fixwidth - 50, TextY));
                            if (udfbInfo != null)
                            {
                                DrawText(graphics, udfbInfo, functionblock, input, "IN", fixwidth + 4, TextY, font, newPen);
                            }
                            else
                                graphics.DrawString(GetHeading("IN1", functionblock.Attributes["function_name"].ToString(), 1, "Input"), font, new SolidBrush(newPen.Color), new PointF(fixwidth + 4, TextY + output));
                        }
                    }
                }
                if (i < lstOutput.Count())
                {
                    input = lstOutput[i];
                    if (input == "output2" && functionblock.Attributes["function_name"].ToString().StartsWith("MQTT"))
                    {
                        input = "";
                    }
                    if ((functionblock.Attributes[input].ToString().Length > 0 || int.TryParse(functionblock.Attributes[input].ToString(), out int op1)) && !functionblock.Attributes[input].ToString().Equals("A5:999"))
                    {

                        string str = XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == functionblock.Attributes[input].ToString()).Count() > 0 ? XMPS.Instance.LoadedProject.Tags.Where(L => L.LogicalAddress == functionblock.Attributes[input].ToString()).Select(L => L.Tag).First().ToString() : functionblock.Attributes[input].ToString();
                        var x = XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == str).FirstOrDefault();
                        if (x != null) str = x.ShowLogicalAddress == true ? x.LogicalAddress : str;
                        SizeF strsz = graphics.MeasureString(str, font, 0, StringFormat.GenericTypographic);
                        graphics.FillRectangle(Brushes.White, new Rectangle(new Point((fixwidth + (int)(functionblock.Position.Width) + 20), TextY + output + printed_output), new Size(170, 30)));

                        //Checking if UDFB variable is bool then showing coil in output section.
                        bool isUDFBBoolVarible = false;
                        if (XMPS.Instance.CurrentScreen.StartsWith("UDFLadderForm"))
                        {
                            string screenNamePart = XMPS.Instance.CurrentScreen.Split('#').ElementAtOrDefault(1)?
                                .Replace(" Logic", "") ?? string.Empty;

                            var currentUDFB = XMPS.Instance.LoadedProject.UDFBInfo
                                .FirstOrDefault(u => u.UDFBName.IndexOf(screenNamePart) >= 0);

                            if (currentUDFB != null && currentUDFB.UDFBlocks != null)
                            {
                                string inputAttr = functionblock.Attributes[input]?.ToString();
                                if (!string.IsNullOrEmpty(inputAttr))
                                {
                                    isUDFBBoolVarible = currentUDFB.UDFBlocks
                                        .Any(t => t.Text.Equals(inputAttr, StringComparison.OrdinalIgnoreCase) &&
                                                  t.DataType.Equals("Bool", StringComparison.OrdinalIgnoreCase));
                                }
                            }
                        }

                        if (functionblock.Attributes[input].ToString().Contains(".") || functionblock.Attributes[input].ToString().StartsWith("F") || isUDFBBoolVarible)
                        {
                            output = input != "output1" ? output + 10 : output;
                            if (XMPS.Instance.FindList.Where(T => T.Item1.Equals(functionblock.Id)).Any() && str == XMPS.Instance.FindList.Select(T => T.Item2).First())
                            {
                                graphics.FillRectangle(Brushes.Red, new Rectangle(new Point((fixwidth + (int)(functionblock.Position.Width - size.Width) + 90), TextY + output + printed_output - 20), new Size((int)strsz.Width + 20, (int)strsz.Height)));
                            }
                            else
                                graphics.FillRectangle(Brushes.White, new Rectangle(new Point((fixwidth + (int)(functionblock.Position.Width - size.Width) + 90), TextY + output + printed_output - 20), new Size((int)strsz.Width + 20, (int)strsz.Height)));
                            graphics.DrawLine(newPen, new PointF((fixwidth + functionblock.Position.Width + 20 - size.Width) + 15, TextY + output + printed_output + 5), new PointF((fixwidth + functionblock.Position.Width + 20 - size.Width) + 107, TextY + output + printed_output + 5));
                            int coilheight = LadderDesign.ControlHeight / 2;
                            graphics.DrawArc(newPen, new Rectangle((fixwidth + functionblock.Position.Width + 110), TextY + output + printed_output - 10 + (coilheight / 4), coilheight / 2, coilheight / 2), 270, -180);
                            graphics.DrawArc(newPen, new Rectangle((fixwidth + functionblock.Position.Width + 110) + 10, TextY + output + printed_output - 10 + (coilheight / 4), coilheight / 2, coilheight / 2), 270, 180);
                            //graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 90, TextY + output + printed_output - 20));
                            if (IsShowLogicalAddress(str))
                            {
                                graphics.DrawString(str, font, new SolidBrush(penShowLogicalAddress.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 90, TextY - 20));
                            }
                            if (LadderDesign.CheckIfForced(str) && !firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any()) graphics.DrawString("F", font, Brushes.Red, new PointF((fixwidth + functionblock.Position.Width) + 20, TextY + output + printed_output - 7));
                            // FOR THE RESOLVE OVERLAPPING ADDESS IN PULSE AND SCALE FUNCTION BLOCK
                            if (input == "output2" && functionblock.Attributes["function_name"].ToString().StartsWith("PLSV"))
                            {
                                if (LadderDesign.CheckinUDFB(str))
                                    graphics.DrawString(str, font, Brushes.Blue, new PointF((fixwidth + functionblock.Position.Width - size.Width) + 80, TextY + output + printed_output - 20));
                                else
                                    graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 80, TextY + output + printed_output - 20));
                            }
                            else if (input == "output3" && functionblock.Attributes["function_name"].ToString().StartsWith("Scale"))
                            {
                                if (LadderDesign.CheckinUDFB(str))
                                    graphics.DrawString(str, font, Brushes.Blue, new PointF((fixwidth + functionblock.Position.Width - size.Width) + 80, TextY + output + printed_output - 18));
                                else
                                    graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 80, TextY + output + printed_output - 18));
                            }
                            else if (input == "output2" && functionblock.Attributes["function_name"].ToString().StartsWith("Scale"))
                            {
                                if (LadderDesign.CheckinUDFB(str))
                                    graphics.DrawString(str, font, Brushes.Blue, new PointF((fixwidth + functionblock.Position.Width - size.Width) + 80, TextY + output + printed_output - 25));
                                else
                                    graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 80, TextY + output + printed_output - 25));
                            }
                            else if (LadderDesign.CheckinUDFB(str))
                                graphics.DrawString(str, font, Brushes.Blue, new PointF((fixwidth + functionblock.Position.Width - size.Width) + 80, TextY + output + printed_output - 25)); //new PointF(fixwidth + 4, TextY)
                            else
                            {
                                graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 90, TextY + output + printed_output - 18));
                            }
                            if (udfbInfo != null)
                            {
                                DrawText(graphics, udfbInfo, functionblock, input, "OP",
                                        fixwidth + functionblock.Position.Width - (int)size.Width - 12,
                                         TextY + output + printed_output, font, newPen);
                            }
                            else
                            {
                                int ioNumber = (i + 1);
                                string InputString = functionblock.Attributes["function_name"].ToString();
                                InputString = InputString.StartsWith("MES_PID_") ? "MES_PID" : InputString;
                                string parentInstruction = XMPS.Instance.instructionsList
                                    .FirstOrDefault(t => t.Text.Equals(InputString))?.InstructionType ?? string.Empty;
                                if (!string.IsNullOrEmpty(parentInstruction) && parentInstruction.Equals("ReadProperty"))
                                {
                                    ioNumber = int.Parse(new string(input.Where(char.IsDigit).ToArray()));
                                }
                                string opname = GetHeading("OP" + (i + 1), functionblock.Attributes["function_name"].ToString(), ioNumber, "Output");
                                graphics.DrawString(opname, font, new SolidBrush(newPen.Color), new PointF(fixwidth + functionblock.Position.Width + 20 - size.Width - (opname.Length > 3 ? 12 + ((opname.Length - 2) * 4) : 12), TextY + output + printed_output));
                            }
                            outputsAndHeight.Add(i, TextY + output + printed_output - 18);
                            printed_output += 3;
                        }
                        else
                        {
                            if (XMPS.Instance.FindList.Where(T => T.Item1.Equals(functionblock.Id)).Any() && str == XMPS.Instance.FindList.Select(T => T.Item2).First())
                                graphics.FillRectangle(Brushes.Red, new Rectangle(new Point((fixwidth + (int)(functionblock.Position.Width - size.Width) + 90), TextY + output + printed_output), new Size((int)strsz.Width + 20, (int)strsz.Height)));
                            else
                                graphics.FillRectangle(Brushes.White, new Rectangle(new Point((fixwidth + (int)(functionblock.Position.Width - size.Width) + 90), TextY + output + printed_output), new Size((int)strsz.Width + 20, (int)strsz.Height)));

                            graphics.DrawLine(newPen, new PointF((fixwidth + functionblock.Position.Width - size.Width) + 35, TextY + output + printed_output + 5), new PointF((fixwidth + functionblock.Position.Width + 20 - size.Width) + 70, TextY + output + printed_output + 5));
                            if (IsShowLogicalAddress(str))
                                graphics.DrawString(str, font, new SolidBrush(penShowLogicalAddress.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 110, TextY + output + printed_output));
                            else if (LadderDesign.CheckinUDFB(str))
                                graphics.DrawString(str, font, Brushes.DarkGreen, new PointF((fixwidth + functionblock.Position.Width - size.Width) + 110, TextY + output + printed_output));
                            else
                                graphics.DrawString(str, font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 110, TextY + output + printed_output));

                            //showing force "F" character for output address also which is non bool address.
                            if (LadderDesign.CheckIfForced(str) && !firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any()) graphics.DrawString("F", font, Brushes.Red, new PointF((fixwidth + functionblock.Position.Width) + 20, TextY + output + printed_output - 7));

                            if (udfbInfo != null)
                            {
                                DrawText(graphics, udfbInfo, functionblock, input, "OP",
                                        fixwidth + functionblock.Position.Width /*+ 20*/ - (int)size.Width - 12,
                                         TextY + output + printed_output, font, newPen);
                            }
                            else
                            {
                                int ioNumber = (i + 1);
                                string InputString = functionblock.Attributes["function_name"].ToString();
                                InputString = InputString.StartsWith("MES_PID_") ? "MES_PID" : InputString;
                                string parentInstruction = XMPS.Instance.instructionsList
                                                                   .FirstOrDefault(t => t.Text.Equals(InputString))?.InstructionType ?? string.Empty; if (!string.IsNullOrEmpty(parentInstruction) && parentInstruction.Equals("ReadProperty"))
                                {
                                    ioNumber = int.Parse(new string(input.Where(char.IsDigit).ToArray()));
                                }
                                string opname = GetHeading("OP" + (i + 1), functionblock.Attributes["function_name"].ToString(), ioNumber, "Output");
                                graphics.DrawString(opname, font, new SolidBrush(newPen.Color), new PointF(fixwidth + functionblock.Position.Width + 20 - size.Width - (opname.Length > 3 ? 12 + ((opname.Length - 2) * 4) : 12), TextY + output + printed_output));
                            }
                            outputsAndHeight.Add(i, (int)(TextY + output + printed_output + 5));
                            printed_output += 5;
                        }
                    }
                    else if (input == "output1")
                    {
                        graphics.DrawLine(newPen, new PointF((fixwidth + functionblock.Position.Width - size.Width) + 35, TextY + output + 5), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 60, TextY + output + 5));
                        graphics.DrawString("???", font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 70, TextY + output));
                        if (udfbInfo != null)
                        {
                            DrawText(graphics, udfbInfo, functionblock, input, "OP",
                                    fixwidth + functionblock.Position.Width /*+ 20*/ - (int)size.Width - 12,
                                    Convert.ToInt32(TextY + output), font, newPen);
                        }
                        else
                            graphics.DrawString(GetHeading("OP1", functionblock.Attributes["function_name"].ToString(), 1, "Output"), font, new SolidBrush(newPen.Color), new PointF(fixwidth + functionblock.Position.Width + 20 - size.Width - 12, TextY + output));
                    }
                    else
                    {
                        if (functionblock.Attributes.Where(T => T.Name == "PreviousOutputs").Count() > 0)
                        {
                            if (Convert.ToInt32(functionblock.Attributes["PreviousOutputs"].ToString()) >= 1)
                                graphics.FillRectangle(Brushes.White, new Rectangle(new Point((fixwidth + (int)(functionblock.Position.Width + 20 - size.Width) + 32), TextY + output), new Size(50, 20)));
                        }
                    }
                }
                TextY = TextY + 25;
                //Adding Condition for the Showing All Used 16 Tags shown on UI For Pack and Unpack
                if (functionblock.Attributes["function_name"].ToString().Equals("Pack") && functionblock.Attributes.Count > 2)
                {
                    string firstInput = functionblock.Attributes["input1"].ToString();
                    if (!string.IsNullOrEmpty(firstInput))
                    {
                        string[] parts = firstInput.Split(':');
                        int lastAdd = int.Parse(parts[1]) + 15;
                        string lastInput = $"{parts[0]}:{lastAdd:000}";
                        graphics.DrawString("(" + lastInput + "..." + firstInput + ")", font, new SolidBrush(newPen.Color), new PointF(fixwidth - 90, TextY));
                    }
                }
                //ADDING LOGIC FOR THE SHOWING USED ADDRESS FOR USED TO UN-PACK INSTRUCTION
                if (functionblock.Attributes["function_name"].ToString().Equals("UnPack") && functionblock.Attributes.Count > 2)
                {
                    string firstInput = functionblock.Attributes["output1"].ToString();
                    if (!string.IsNullOrEmpty(firstInput))
                    {
                        string[] parts = firstInput.Split(':');
                        int lastAdd = int.Parse(parts[1]) + 15;
                        string lastInput = $"{parts[0]}:{lastAdd:000}";
                        graphics.DrawString("(" + lastInput + "..." + firstInput + ")", font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width - size.Width) + 110, TextY + 10));
                    }
                }
            }

            TextY = functionblock.getY() + 125;

            if (OnlineMonitoringStatus.isOnlineMonitoring && XMPS.Instance.LoadedProject.MainLadderLogic.Where(R => !R.StartsWith("'") && R.Contains(XMPS.Instance.CurrentScreen.Split('#')[1])).Count() > 0)
            {
                TextY = TextY - 25;
                string FunctionType = functionblock.Attributes["function_name"].ToString();
                int inputsCount = functionblock.Attributes.Where(t => t.Name.StartsWith("input")).Count();
                int outputsCount = functionblock.Attributes.Where(t => t.Name.StartsWith("output")).Count();
                //&& !functionblock.Attributes[input].ToString().Equals("A5:999")
                //Checking Element is commented or not
                if (!firstElement.Attributes.Where(T => T.Name.Equals("isCommented")).Any())
                {
                    //showing function block multiple inputs in online monitoring.
                    for (int i = 1; i <= inputsCount; i++)
                    {
                        int offset = 0;
                        if (i == 1) offset = -65;
                        if (i == 2) offset = -40;
                        if (i == 3) offset = -15;
                        if (i == 4) offset = +10;
                        if (i == 5) offset = +35;
                        if (i >= 6)
                        {
                            offset = 25 * (i - 4) + 10;
                        }
                        string inputAttr = "input" + i;
                        if (functionblock.Attributes[inputAttr].ToString().Length > 2 || int.TryParse(functionblock.Attributes[inputAttr].ToString(), out int _))
                        {
                            string IN = string.Empty;
                            string inputKey = functionblock.Attributes[inputAttr].ToString().Replace("~", "");
                            if (OnlineMonitoringStatus.AddressValues.ContainsKey(inputKey))
                            {
                                IN = OnlineMonitoringStatus.AddressValues[inputKey];
                            }
                            else
                            {
                                var tag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(inputKey))?.Tag;
                                if (tag != null && OnlineMonitoringStatus.AddressValues.ContainsKey(tag))
                                {
                                    IN = OnlineMonitoringStatus.AddressValues[tag];
                                }
                                else
                                {
                                    IN = inputKey;
                                }
                            }

                            if ((IN == "1" || IN == "0") && (functionblock.Attributes["DataType"].ToString() == "0000" || (FunctionType.EndsWith("TON") || FunctionType.EndsWith("TOFF") || FunctionType.EndsWith("TP") || FunctionType == "Timer" || FunctionType == "CTU" || FunctionType == "CTD" || (FunctionType.StartsWith("MES_PID_") && i > 11))))
                            {
                                int offsetBool = TextY - 57 + 25 * (i - 1);
                                if (IN == "0" ^ functionblock.Attributes[inputAttr].ToString().Contains("~"))
                                {
                                    if (FunctionType != "Pack")
                                    {
                                        graphics.DrawRectangle(newPen, new Rectangle(fixwidth - 13, offsetBool, 10, 10));
                                    }
                                }
                                else
                                {
                                    //Not showing Green Block for the pack instruction
                                    if (FunctionType != "Pack")
                                    {
                                        graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(fixwidth - 13, offsetBool, 11, 11));
                                    }
                                }
                            }
                            else
                            {
                                graphics.DrawString($"[ {IN} ]", LadderDesign.Font, new SolidBrush(newPen.Color), new PointF(fixwidth - 16 - (int)graphics.MeasureString(IN, font, 0, StringFormat.GenericTypographic).Width, TextY + offset));
                            }

                            // Get all 16 addresses for the Pack and UnPack instruction
                            if (FunctionType == "Pack" && i == 1)
                            {
                                string packfirstAdd = functionblock.Attributes[inputAttr].ToString();
                                XMIOConfig firstTag = (XMIOConfig)XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == packfirstAdd.Replace("~", "")).FirstOrDefault();
                                string[] parts = packfirstAdd.Split(':');
                                int lastTagAdd = int.Parse(parts[1]) + 15;
                                string endAdd = $"{parts[0]}:{lastTagAdd:000}";

                                List<XMIOConfig> packTagList = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")
                                                                   && Convert.ToInt32(T.LogicalAddress.Split(':')[1]) >= Convert.ToInt32(packfirstAdd.Split(':')[1])
                                                                   && Convert.ToInt32(T.LogicalAddress.Split(':')[1]) <= Convert.ToInt32(endAdd.Split(':')[1])).ToList();

                                string packedActualAdd = "[";
                                string atualAdd = "";
                                packTagList.Reverse();
                                foreach (XMIOConfig packTag in packTagList)
                                {
                                    atualAdd = OnlineMonitoringStatus.AddressValues[packTag.LogicalAddress];
                                    atualAdd = packfirstAdd.StartsWith("~") ? (atualAdd == "1" ? "0" : "1") : atualAdd;
                                    packedActualAdd = packedActualAdd + atualAdd + ",";
                                }
                                packedActualAdd = packedActualAdd + "]";
                                graphics.DrawString($"{packedActualAdd}", LadderDesign.Font, new SolidBrush(newPen.Color), new PointF(fixwidth - 16 - (int)graphics.MeasureString(packedActualAdd, font, 0, StringFormat.GenericTypographic).Width, TextY - 35));
                            }
                        }
                    }


                    int ignoredOP = 0;
                    for (int i = 1; i <= outputsCount; i++)
                    {
                        int outputOffeset = 0;
                        outputsAndHeight.TryGetValue(i - 1 - ignoredOP, out outputOffeset);

                        string outputAttr = "output" + i;
                        if ((functionblock.Attributes[outputAttr].ToString().Length > 2 || int.TryParse(functionblock.Attributes[outputAttr].ToString(), out int _)) && !functionblock.Attributes[outputAttr].ToString().Equals("A5:999"))
                        {
                            string ActVal = string.Empty;
                            string outputKey = functionblock.Attributes[outputAttr].ToString().Replace("~", "");
                            if (OnlineMonitoringStatus.AddressValues.ContainsKey(outputKey))
                            {
                                ActVal = OnlineMonitoringStatus.AddressValues[outputKey];
                            }
                            else
                            {
                                var tag = XMPS.Instance.LoadedProject.Tags.FirstOrDefault(t => t.LogicalAddress.Equals(outputKey))?.Tag;
                                if (tag != null && OnlineMonitoringStatus.AddressValues.ContainsKey(tag))
                                {
                                    ActVal = OnlineMonitoringStatus.AddressValues[tag];
                                }
                                else
                                {
                                    ActVal = outputKey;
                                }
                            }

                            if ((ActVal == "1" || ActVal == "0") && (functionblock.Attributes[outputAttr].ToString().Replace("~", "").Contains('.') || functionblock.Attributes[outputAttr].ToString().Replace("~", "").StartsWith("F2")))
                            {
                                if (FunctionType != "UnPack")
                                {
                                    if (ActVal == "0" ^ functionblock.Attributes[outputAttr].ToString().Contains("~"))
                                    {
                                        graphics.DrawRectangle(newPen, new Rectangle((fixwidth + functionblock.Position.Width + 110) + 7, outputOffeset + 17, 10, 10));
                                    }
                                    else
                                    {
                                        //Not showing Green Block for the pack instruction
                                        graphics.FillRectangle(Brushes.DarkGreen, new Rectangle((fixwidth + functionblock.Position.Width + 110) + 7, outputOffeset + 17, 11, 11));
                                    }
                                }
                                TextY = TextY + 13;
                            }
                            else
                            {
                                if (FunctionType != "UnPack")
                                {
                                    graphics.DrawString($"[ {ActVal} ]", LadderDesign.Font, new SolidBrush(newPen.Color), new PointF(fixwidth + functionblock.Position.Width + 30, outputOffeset - 15));

                                }
                            }

                            // Get all 16 addresses for the Pack and UnPack instruction
                            if (FunctionType == "UnPack" && i == 1)
                            {
                                string packfirstAdd = functionblock.Attributes["output1"].ToString();
                                XMIOConfig firstTag = (XMIOConfig)XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress == packfirstAdd).FirstOrDefault();
                                string actualNameFisrtTag = firstTag.ActualName;
                                //string[] parts = actualNameFisrtTag.Split('_');
                                string[] parts = packfirstAdd.Split(':');
                                int lastTagAdd = int.Parse(parts[1]) + 15;
                                string endAdd = $"{parts[0]}:{lastTagAdd:000}";
                                List<XMIOConfig> packTagList = XMPS.Instance.LoadedProject.Tags.Where(T => T.LogicalAddress.StartsWith("F2")
                                                               && Convert.ToInt32(T.LogicalAddress.Split(':')[1]) >= Convert.ToInt32(packfirstAdd.Split(':')[1])
                                                               && Convert.ToInt32(T.LogicalAddress.Split(':')[1]) <= Convert.ToInt32(endAdd.Split(':')[1])).ToList();

                                string packedActualAdd = "[";
                                string atualAdd = "";
                                packTagList.Reverse();
                                foreach (XMIOConfig packTag in packTagList)
                                {
                                    atualAdd = OnlineMonitoringStatus.AddressValues[packTag.LogicalAddress];
                                    packedActualAdd = packedActualAdd + atualAdd + ",";
                                }
                                packedActualAdd = packedActualAdd + "]";
                                //graphics.DrawString($"{packedActualAdd}", LadderDesign.Font, new SolidBrush(newPen.Color), new PointF(fixwidth - 16 - (int)graphics.MeasureString(packedActualAdd, font, 0, StringFormat.GenericTypographic).Width, TextY - 40));
                                graphics.DrawString($"{packedActualAdd}", LadderDesign.Font, new SolidBrush(newPen.Color), new PointF((fixwidth + functionblock.Position.Width + 12) + 6, TextY - 40));
                            }
                        }
                        else if (functionblock.Attributes[outputAttr].ToString().Equals("A5:999"))
                            ignoredOP++;

                    }
                }
            }
        }

        private bool IsShowLogicalAddress(string TagName)
        {
            return XMPS.Instance.LoadedProject.Tags.Where(T => T.Tag == TagName).Select(T => T.ShowLogicalAddress).FirstOrDefault();
        }

        private string GetHeading(string InputType, string FunctionType, int Number, string Type)
        {

            string InputString = InputType;
            FunctionType = FunctionType.StartsWith("MES_PID_") ? "MES_PID" : FunctionType;
            if (XMPS.Instance.instructionsList.Any(t => t.Text.Equals(FunctionType)))
            {
                var ioModel = XMPS.Instance.instructionsList?.FirstOrDefault(t => t.Text.Equals(FunctionType)).InputsOutputs.FirstOrDefault(a => a.Id == Number && a.Type.Equals(Type))?.TextInFB;
                return string.IsNullOrEmpty(ioModel?.ToString()) ? InputString : ioModel.ToString();
            }
            return InputType;
        }

        /// <summary>
        /// Get Last element of the Rung which is not Coil
        /// </summary>
        /// <param name="element"></param> Send the element of which root has to be searched
        /// <returns></returns> Return X of last element + width of that element

        /// <summary>
        /// Check how many iputs are added in the function block to adjust height accordingly
        /// </summary>
        /// <param name="functionblock"></param> Function Block 
        /// <returns></returns> Number of inputs entered
        private int GetInputs(LadderElement functionblock)
        {
            int inputs = functionblock.Attributes.Where(t => t.Name.StartsWith("in") && t.Value != null && !t.Value.ToString().EndsWith("-") && !t.Value.ToString().Equals("")).Count();
            int outputs = functionblock.Attributes.Where(t => t.Name.StartsWith("out") && t.Value != null && !t.Value.ToString().Contains('-') && !t.Value.ToString().Equals("") && !t.Value.Equals("A5:999")).Count();

            int bitouts = functionblock.Attributes.Where(t => t.Name.StartsWith("out") && !t.Value.ToString().Contains('-') && !t.Value.ToString().Equals("") && (t.Value.ToString().Contains('.') || t.Value.ToString().StartsWith("F2"))).Count();
            outputs = (inputs == outputs && bitouts > 0) ? outputs + (outputs / 5) : outputs;

            outputs = outputs + (bitouts / 2) + ((outputs - bitouts) / 5);
            return inputs > outputs ? inputs : outputs;
        }

        public void Draw(Graphics graphics, LadderElement functionblock)
        {
            Element = functionblock;
            Pen newPen = new Pen(new SolidBrush(Color.Black), functionblock.LineWidth);
            Paint(newPen, graphics, functionblock);
        }

        public void OnSelect(Graphics graphics, LadderElement functionblock)
        {
            Pen newPen = new Pen(new SolidBrush(Color.Blue), functionblock.LineWidth);
            Paint(newPen, graphics, functionblock);
            Global.SelectActive(functionblock.getRoot(), graphics);
        }

        public string toString()
        {
            return "FunctionBlock";
        }
        public void ForComment(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.Red), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }
        private void DrawText(Graphics graphics, UDFBInfo udfbInfo, LadderElement functionblock, string input, string defaultPrefix, int offsetX, int offsetY, Font font, Pen newPen)
        {
            if (udfbInfo != null)
            {
                string textValue = string.Empty;
                int inputIndex = -1;
                string type = defaultPrefix.Equals("IN") ? "Input" : "Output";
                int substringStartIndex = defaultPrefix.Equals("IN") ? 5 : 6;

                if (input.StartsWith(type.ToLower(), StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(input.Substring(substringStartIndex), out inputIndex) && inputIndex > 0)
                    {
                        textValue = udfbInfo.UDFBlocks
                            .Where(block => block.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                            .Skip(inputIndex - 1)
                            .Select(block => block.Text)
                            .FirstOrDefault();

                        if (!string.IsNullOrEmpty(textValue) && textValue.Length > 4)
                        {
                            textValue = textValue.Substring(0, 4);
                        }
                    }
                }

                string finalText = GetHeading(
                 textValue ?? defaultPrefix + inputIndex,
                 functionblock.Attributes["function_name"].ToString(),
                 inputIndex,
                 type);

                SizeF stringSize = graphics.MeasureString(finalText, font);

                float adjustedX = offsetX;

                if (type == "Output")
                {
                    adjustedX = rightx - stringSize.Width - 7;
                }

                graphics.DrawString(
                    GetHeading(textValue ?? defaultPrefix + inputIndex, functionblock.Attributes["function_name"].ToString(), inputIndex, type),
                    font,
                    new SolidBrush(newPen.Color),
                    new PointF(adjustedX, offsetY)
                );
            }
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
            Pen newPen = new Pen(new SolidBrush(Color.DarkOrange), element.LineWidth);
            Paint(newPen, graphics, element);
            Global.SelectActive(element.getRoot(), graphics);
        }
    }
}
