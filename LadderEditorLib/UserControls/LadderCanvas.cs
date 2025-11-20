using LadderDrawing.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using XMPS2000.Core;
using XMPS2000.Core.Base;
using XMPS2000.Core.Devices.Slaves;
using ToolTip = System.Windows.Forms.ToolTip;

namespace LadderDrawing
{
    public partial class LadderCanvas : UserControl
    {
        List<int> columnsWidth = new List<int>();
        List<int> rowsHeight = new List<int>();
        private BufferedGraphicsContext bufferedContext;
        private BufferedGraphics bufferedGraphics;
        LadderDesign laderPattern = new LadderDesign();
        public LadderDesign getDesignView() { return laderPattern; }
        public void setDesignView(LadderDesign design) { laderPattern = design; }

        public MouseEvent mouseEvent;
        public List<LadderElement> selectedElements = new List<LadderElement>();
        //for fully select rungs
        public List<LadderElement> fullyRungElements = new List<LadderElement>();
        public static LadderCanvas Active;

        public event EventHandler<MouseEventArgs> OnDoubleClickEvent;
        public event EventHandler<DragEventArgs> OnDragCreate;
        public event EventHandler<KeyEventArgs> ItemDeleted;
        public event EventHandler<EventArgs> MainLogicRefresh;
        public event EventHandler<EventArgs> CrossReferanceClicked;


        //for mouseEnter Event
        private ToolTip toolTip;
        private ToolTip PackFunctioBlock;
        private ToolTip UnPackFunctioBlock;
        private ToolTip FunctionBlockTool;
        private bool isToolTipShowing = false;
        private bool isPackMethodCalled = false;
        private bool isUnPackMethodCalled = false;
        private bool isFBToolTipMethodCalled = false;
        private bool isDeleted = false;
        private readonly Dictionary<string, string> topicIdMap = new Dictionary<string, string>
        {
            { "Logical Instruction", "Files/logicalinstruction.htm" },{ "AND", "Files/AND.htm" }, { "OR", "Files/or.htm" },{ "XOR", "Files/xor.htm" }, { "NOT", "Files/not.htm" },
            { "Arithmetic Instruction", "Files/arithmeticinstruction.htm" },{ "ADD", "Files/add.htm" },{ "SUB", "Files/sub.htm" }, { "MUL", "Files/mul.htm" }, { "DIV", "Files/div.htm" }, { "MOD", "Files/mod.htm" },{ "MOV", "Files/mov.htm" }, { "EXP", "Files/exp.htm" },
            { "Bit Shift Instruction", "Files/bitshiftinstruction.htm" }, { "SHL", "Files/shl.htm" },{ "SHR", "Files/shr.htm" }, { "ROR", "Files/rol.htm" }, { "ROL", "Files/ror.htm" },
            { "LIMIT", "Files/limitinstruction.htm" },
            { "Compare Instruction", "Files/compareinstruction.htm" },{ "GT", "Files/greaterthangt.htm" }, { "GE", "Files/greaterthanorequaltoge.htm" },{ "LT", "Files/lessthanlt.htm" }, { "LE", "Files/lessthanorequaltole.htm" }, { "EQ", "Files/equaltoeq.htm" },{ "NE", "Files/notequaltone.htm" },
            { "Edge Detector", "Files/edgedetector.htm" }, { "Rising Edge", "Files/raisingedge.htm" },{ "Falling Edge", "Files/fallingedge.htm" },
            { "Counter Instruction", "Files/counterinstruction.htm" }, { "CTU", "Files/ctu.htm" },{ "CTD", "Files/ctd.htm" },
            { "Timer TON", "Files/timertoninstruction.htm" }, { "0.01s TON", "Files/ton001s.htm" }, { "0.1s TON", "Files/ton01s.htm" }, { "1s TON", "Files/ton1s.htm" },
            { "Timer TOFF", "Files/timertoffinstruction.htm" },{ "0.01s TOFF", "Files/toff001s.htm" },  { "0.1s TOFF", "Files/toff01s.htm" }, { "1s TOFF", "Files/toff1s.htm" },
            { "Timer TP", "Files/timertpinstruction.htm" },{ "0.01s TP", "Files/tp001s.htm" }, { "0.1s TP", "Files/tp01s.htm" }, { "1s TP", "Files/tp1s.htm" },
            { "Flip Flop", "Files/flipflopinstruction.htm" }, { "RS", "Files/rs.htm" }, { "SR", "Files/sr.htm" },
            { "Limit Alarm Instruction", "Files/limitalarminstruction.htm" },  { "Limit Alarm - O", "Files/limitalarm0.htm" }, { "Limit Alarm - U", "Files/limitalarmu.htm" },
            { "Swap Instruction", "Files/swapinstruction.htm" }, { "SWAP CDAB", "Files/cdab.htm" }, { "SWAP BADC", "Files/badc.htm" }, { "SWAP DCBA", "Files/dcba.htm" },
            { "Data Conversion Instruction", "Files/dataconversioninstruction.htm" }, { "ANY to BOOL", "Files/anytobool.htm" },{ "ANY to BYTE", "Files/anytobyte.htm" },  { "ANY to WORD", "Files/anytoword.htm" }, { "ANY to DWORD", "Files/anytodword.htm" },
            { "ANY to REAL", "Files/anytoreal.htm" },{ "ANY to INT", "Files/anytoint.htm" },
            { "Scale", "Files/scaleinstruction.htm" },
            { "Timer RTON", "Files/timerrtoninstruction.htm" }, { "0.01s RTON", "Files/rton001s1.htm" }, { "0.1s RTON", "Files/rton01s.htm" }, { "1s RTON", "Files/rton1s.htm" }, { "1m RTON", "Files/1mrton.htm" },
            { "Pack", "Files/packinstruction.htm" },
            { "UnPack", "Files/unpackinstruction.htm" },
        };
        public LadderCanvas()
        {
            InitializeComponent();
            Active = this;
            toolTip = new ToolTip();
            PackFunctioBlock = new ToolTip();
            UnPackFunctioBlock = new ToolTip();
            FunctionBlockTool = new ToolTip();
            toolTip.Hide(this);
            //  toolTip.ShowAlways = true;
            this.DoubleBuffered = true;
            this.SetStyle(
               System.Windows.Forms.ControlStyles.UserPaint |
               System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
               System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
               true);
            bufferedContext = BufferedGraphicsManager.Current;
        }

        public LadderElement getTableView()
        {
            LadderElement element = null;
            if (laderPattern != null)
                if (laderPattern.Elements.Count > 0 && laderPattern.Elements[0] != null)
                {
                    element = laderPattern.Elements[0];
                    return element;
                }
                else
                {
                    element = new LadderElement();
                    element.CreateTable(rowsHeight.Count, columnsWidth.Count, columnsWidth.ToArray(), rowsHeight.ToArray());
                    laderPattern.Elements.Add(element);
                    return element;
                }
            return null;
        }

        public void ApplyTable()
        {
            laderPattern.ApplyTable(columnsWidth.ToArray());
        }

        public void AddColumn(int width)
        {
            columnsWidth.Add(width);
        }
        public void InsertColumn(int index, int width)
        {
            columnsWidth.Insert(index, width);
        }

        public void AddRow(int height)
        {
            rowsHeight.Add(height);
        }

        public void InsertRow(int index, int height)
        {
            rowsHeight.Insert(index, height);
        }

        public LadderElement getTableCell(int columnindex, int rowindex)
        {
            LadderElement element = getTableView();
            LadderElement row = null;
            if (rowindex < element.Elements.Count)
            {
                row = element.Elements[rowindex];
                if (row != null && columnindex < row.Elements.Count)
                    return row.Elements[columnindex];
            }
            return null;
        }


        //other operations
        private void setGraphics(ref Graphics graphics, bool highQuality = false)
        {
            if (highQuality)
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            }
            else
            {
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            }
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.PageUnit = GraphicsUnit.Pixel;
        }

        private void setGraphics(Graphics graphics, bool highQuality = false)
        {
            if (highQuality)
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.CompositingQuality = CompositingQuality.GammaCorrected;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            }
            else
            {
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            }
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.PageUnit = GraphicsUnit.Pixel;
        }

        public Graphics getGraphics()
        {
            Graphics graphics = CreateGraphics();
            setGraphics(graphics);
            return graphics; // Caller must dispose
        }

        protected override void OnResize(EventArgs e)
        {
            int maxSize = 32767 * 2; // Practical maximum for Bitmap/Graphics
            int width = Math.Min(ClientSize.Width, maxSize);
            int height = Math.Min(ClientSize.Height, maxSize);
            if (width > 0 && height > 0)
            {
                Size = new Size(width, height);
                Invalidate();
            }
            base.OnResize(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            bufferedGraphics = bufferedContext.Allocate(e.Graphics, e.ClipRectangle);
            Graphics graphics = bufferedGraphics.Graphics;
            setGraphics(graphics, false);
            graphics.Clear(Color.White);
            laderPattern.Position.Width = ClientSize.Width;
            laderPattern.Position.Height = ClientSize.Height;
            laderPattern.Render(graphics, e.ClipRectangle.Y); // Render dynamic content within clip region
            bufferedGraphics.Render(e.Graphics);
            bufferedGraphics.Dispose();
            base.OnPaint(e);
        }

        public static void RefreshCanvas()
        {
            Active.Invalidate();
        }

        private void LadderDrawing_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Clicks <= 0)
            {

                this.toolTip.Active = true;
                this.timerToolTip.Enabled = true;
                LadderElement pattern = LadderDesign.HoverElement;
                Point LocalMousePosition = this.PointToClient(Cursor.Position);
                LadderDesign.HoverElement = laderPattern.Elements.MouseHover(null, e.X, LocalMousePosition.Y);
                LadderElement ladderElement = laderPattern.Elements.MouseHover(null, e.X, LocalMousePosition.Y);
                if (LadderDesign.HoverElement != null)
                {
                    if (LadderDesign.HoverElement.Attributes["Caption"].ToString() != null && LadderDesign.HoverElement.Attributes["Caption"].ToString() != "Comments" && LadderDesign.HoverElement.Attributes["Caption"].ToString() != "FunctionBlock")
                    {
                        this.UnPackFunctioBlock.Active = false;
                        this.UnPackFunctioBlock.Hide(this);
                        this.PackFunctioBlock.Active = false;
                        this.PackFunctioBlock.Hide(this);
                        this.FunctionBlockTool.Hide(this);
                        ToolTipText();
                        timerToolTip.Start();
                    }
                    else if (LadderDesign.HoverElement.Attributes["Caption"].ToString() == "FunctionBlock")
                    {
                        if (LadderDesign.HoverElement.Attributes["function_name"].ToString() != "Pack" &&
                            LadderDesign.HoverElement.Attributes["function_name"].ToString() != "UnPack")
                        {
                            this.isPackMethodCalled = false;
                            this.isUnPackMethodCalled = false;
                            this.UnPackFunctioBlock.Hide(this);
                            this.PackFunctioBlock.Hide(this);
                            if (!isToolTipShowing && !isFBToolTipMethodCalled)
                            {
                                FunctionBlockToolTipText();
                                this.isFBToolTipMethodCalled = true;
                            }
                            timerToolTip.Start();

                        }
                        if (LadderDesign.HoverElement.Attributes["function_name"].ToString() == "Pack")
                        {
                            this.isUnPackMethodCalled = false;
                            this.isFBToolTipMethodCalled = false;
                            this.UnPackFunctioBlock.Hide(this);
                            this.FunctionBlockTool.Hide(this);
                            if (!isToolTipShowing && !isPackMethodCalled)
                            {
                                PackTooltipText();
                                this.isPackMethodCalled = true;
                            }
                            timerToolTip.Start();

                        }
                        if (LadderDesign.HoverElement.Attributes["function_name"].ToString() == "UnPack")
                        {
                            this.isPackMethodCalled = false;
                            this.isFBToolTipMethodCalled = false;
                            this.PackFunctioBlock.Hide(this);
                            this.FunctionBlockTool.Hide(this);
                            if (!isToolTipShowing && !isUnPackMethodCalled)
                            {
                                UnPackToolTipText();
                                this.isUnPackMethodCalled = true;
                            }
                            timerToolTip.Start();
                        }

                    }
                    else
                    {
                        toolTip.Hide(this);
                        toolTip.RemoveAll();
                        this.isPackMethodCalled = false;
                        this.isUnPackMethodCalled = false;
                        this.isFBToolTipMethodCalled = false;
                        this.PackFunctioBlock.Hide(this);
                        this.UnPackFunctioBlock.Hide(this);
                        this.FunctionBlockTool.Hide(this);
                        timerToolTip.Stop();
                        timerToolTip.Dispose();  
                    }
                }
                else
                {
                    toolTip.Hide(this);
                    this.PackFunctioBlock.Hide(this);
                    this.UnPackFunctioBlock.Hide(this);
                    this.FunctionBlockTool.Hide(this);
                    toolTip.RemoveAll();
                    timerToolTip.Stop();
                    timerToolTip.Dispose();

                }
            }
            else
            {
                toolTip.Hide(this);
                toolTip.RemoveAll();
                this.isPackMethodCalled = false;
                this.isUnPackMethodCalled = false;
                this.isFBToolTipMethodCalled = false;
                this.PackFunctioBlock.Hide(this);
                this.UnPackFunctioBlock.Hide(this);
                this.FunctionBlockTool.Hide(this);
                timerToolTip.Stop();
                timerToolTip.Dispose();


            }
        }

        private void FunctionBlockToolTipText()
        {
            try
            {
                this.FunctionBlockTool.Active = true;
                var udfbInfo = XMPS.Instance.LoadedProject.UDFBInfo
                              .FirstOrDefault(u => u.UDFBName.IndexOf(LadderDesign.HoverElement.Attributes["function_name"].ToString(),
                              StringComparison.OrdinalIgnoreCase) >= 0);
                if (!LadderDesign.HoverElement.Attributes["function_name"].ToString().Contains("Pack") && !LadderDesign.HoverElement.Attributes["function_name"].ToString().Contains("MQTT"))
                {
                    List<ToolTipText> toolTipTexts = LadderDesign.HoverElement.Attributes
                                        .Where(attr => attr.Name.StartsWith("input"))
                                        .Select(attr => new ToolTipText
                                        {
                                            LogicalAddress = attr.Value.ToString(),
                                            Tag = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress.Equals(attr.Value.ToString().Replace("~", ""))),
                                            Type = "Input"
                                        }).Concat(LadderDesign.HoverElement.Attributes
                                       .Where(attr => attr.Name.StartsWith("output") && !attr.Value.ToString().Contains("A5:999"))
                                       .Select(attr => new ToolTipText
                                       {
                                           LogicalAddress = attr.Value.ToString(),
                                           Tag = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress.Equals(attr.Value.ToString())),
                                           Type = "Output"
                                       })).ToList();

                    var datatype = LadderDesign.HoverElement.Attributes["DataType_Nm"].ToString();

                    int inputCounter = 1;
                    int outputCounter = 1;

                    string toolTipText = string.Join("\n", toolTipTexts
                        .Where(t => t.LogicalAddress != "-" && !string.IsNullOrEmpty(t.LogicalAddress))
                        .Select(t =>
                        {
                            string tagInitialValue = t.Tag != null ? t.Tag.InitialValue : string.Empty;
                            string tagRetentive = t.Tag != null && t.Tag.Retentive ? "RET" : string.Empty;
                            string actualDataType = t.Tag != null ? GetDataTypeName(t.Tag.Label) : datatype;
                            string prefix;
                            int index;

                            if (t.Type == "Input")
                            {
                                prefix = "IN";
                                index = inputCounter++;
                                if (udfbInfo != null)
                                {
                                    string textValue = string.Empty;
                                    if (index > 0)
                                    {
                                        textValue = udfbInfo.UDFBlocks
                                            .Where(block => block.Type.Equals("Input", StringComparison.OrdinalIgnoreCase))
                                            .Skip(index - 1)
                                            .Select(block => block.Text)
                                            .FirstOrDefault();
                                    }
                                    prefix = textValue;
                                }
                            }
                            else
                            {
                                prefix = "OP";
                                index = outputCounter++;
                                if (udfbInfo != null)
                                {
                                    string textValue = string.Empty;
                                    if (index > 0)
                                    {
                                        textValue = udfbInfo.UDFBlocks
                                            .Where(block => block.Type.Equals("Output", StringComparison.OrdinalIgnoreCase))
                                            .Skip(index - 1)
                                            .Select(block => block.Text)
                                            .FirstOrDefault();
                                    }
                                    prefix = textValue;
                                }
                            }
                            if (udfbInfo != null)
                                return $"{prefix}:{t.LogicalAddress}:{actualDataType}:{tagInitialValue}:{tagRetentive}";
                            else
                                return $"{prefix}{index}:{t.LogicalAddress}:{actualDataType}:{tagInitialValue}:{tagRetentive}";
                        }));

                    FunctionBlockTool.Show(toolTipText, this);
                }
                else if (LadderDesign.HoverElement.Attributes["function_name"].ToString().Contains("MQTT"))
                {
                    Publish pubdata = new Publish();
                    Subscribe subdata = new Subscribe();
                    bool isPublishFB = false;
                    bool isSubscribeFB = false;
                    if (LadderDesign.HoverElement.Attributes["function_name"].ToString().Contains("Publish") && LadderDesign.HoverElement.Attributes["output2"].ToString() != "")
                    {
                        var publst = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Publish").Cast<Publish>().ToList();
                        pubdata = publst.Where(t => t.keyvalue == Convert.ToInt32(LadderDesign.HoverElement.Attributes["output2"].ToString())).FirstOrDefault();
                        subdata = null;
                        isPublishFB = true;
                    }
                    else
                    {
                        if (LadderDesign.HoverElement.Attributes["output2"].ToString() != "")
                        {
                            pubdata = null;
                            var sublst = xm.LoadedProject.Devices.Where(d => d.GetType().Name == "Subscribe").Cast<Subscribe>().ToList();
                            subdata = sublst.Where(t => t.key == Convert.ToInt32(LadderDesign.HoverElement.Attributes["output2"].ToString())).FirstOrDefault();
                            isSubscribeFB = true;
                        }
                    }

                    if (LadderDesign.HoverElement.Attributes.Count > 2 && ((isPublishFB && pubdata != null) || (isSubscribeFB && subdata != null)))
                    {
                        var Input1 = LadderDesign.HoverElement.Attributes["input1"].ToString();
                        var Input1Tags = pubdata != null ? pubdata.topic.ToString() : subdata.topic.ToString();

                        var Input2 = LadderDesign.HoverElement.Attributes["input2"].ToString();
                        var Input2Tags = subdata != null ? subdata.qos.ToString() : "";

                        var Input3 = LadderDesign.HoverElement.Attributes["input3"].ToString();
                        var Input3Tags = pubdata != null ? pubdata.qos.ToString() : "";

                        var Input4 = LadderDesign.HoverElement.Attributes["input4"].ToString();
                        var Input4Tags = pubdata != null ? pubdata.retainflag.ToString() : "";

                        var Input5 = LadderDesign.HoverElement.Attributes["input5"].ToString();
                        var Input5Tags = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Input5);

                        var Output1 = LadderDesign.HoverElement.Attributes["output1"].ToString();
                        var Output1Tags = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output1);

                        var Output2 = LadderDesign.HoverElement.Attributes["output2"].ToString();
                        var Output2Tags = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output2);

                        var Output3 = LadderDesign.HoverElement.Attributes["output3"].ToString();
                        var Output3Tags = XMPS.Instance.LoadedProject.Tags.Find(d => d.LogicalAddress == Output3);

                        var datatype = LadderDesign.HoverElement.Attributes["DataType_Nm"].ToString();
                        string toolTipText =
                                   $"{((Input1 != "-" && Input1 != "") ? "IN1" + ":" + $"{Input1}" + ":" + $"{Input1Tags}" + "\n" : "")}"
                                 + $"{((Input2 != "-" && Input2 != "") ? "IN2" + ":" + $"{Input2}" + ":" + (pubdata == null ? "Bool" + ":" : "") + $"{Input2Tags}" + "\n" : "")}"
                                 + $"{((Input3 != "-" && Input3 != "") ? "IN3" + ":" + $"{Input3}" + ":" + "Bool" + ":" + $"{Input3Tags}" + "\n" : "")}"
                                 + $"{((Input4 != "-" && Input4 != "") ? "IN4" + ":" + $"{Input4}" + ":" + (subdata == null ? "Bool" + ":" : "") + $"{Input4Tags}" + "\n" : "")}"
                                 + $"{((Input5 != "-" && Input5 != "") ? "IN5" + ":" + $"{Input5}" + ":" + $"{Input5Tags}" + "\n" : "")}"
                                 + $"{((Output1 != "-" && Output1 != "") ? "OP1" + ":" + $"{Output1}" + ":" + $"{Output1Tags.Label}" + ":" + $"{(Output1Tags != null ? $"{Output1Tags.InitialValue}" + ":" + $"{(Output1Tags.Retentive ? "RET" : "")}" : "")}" + "\n" : "")}";
                        FunctionBlockTool.Show(toolTipText, this);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string GetDataTypeName(string label)
        {
            if (label != "" && label.StartsWith("D") && !label.Equals("DINT") && !label.Equals("Double Word"))
                return "Bool";
            else if ((label != "" && label.StartsWith("A") || label.StartsWith("U")))
                return "";
            return label;
        }

        private void UnPackToolTipText()
        {
            try
            {
                string toolTipText = "";
                this.UnPackFunctioBlock.Active = true;
                if (LadderDesign.HoverElement.Attributes["function_name"].ToString() == "UnPack" && LadderDesign.HoverElement.Attributes.Count > 2)
                {
                    string firstOPTag = LadderDesign.HoverElement.Attributes["input1"].ToString();
                    if (!string.IsNullOrEmpty(firstOPTag))
                    {
                        XMIOConfig firstOutputTag = xm.LoadedProject.Tags.FirstOrDefault(T => T.LogicalAddress == firstOPTag);
                        if (firstOutputTag == null)
                        {
                            return;
                        }
                        var DefaultIOCheck1 = (firstOutputTag?.Tag.Contains("Digital") == true) ? "Bool" : firstOutputTag?.Label;

                        toolTipText += $"IN1:{firstOutputTag?.LogicalAddress}:{(DefaultIOCheck1 ?? "")}:{(firstOutputTag?.InitialValue ?? "")}:{(firstOutputTag?.Retentive == true ? "RET" : "")}\n";

                        // Fetching all output boolean Addresses from the function block
                        string unPackfirstAdd = LadderDesign.HoverElement.Attributes["output1"].ToString();
                        XMIOConfig firstTag = xm.LoadedProject.Tags.FirstOrDefault(T => T.LogicalAddress == unPackfirstAdd);
                        string actualNameFisrtTag = firstTag?.ActualName;
                        string[] parts = actualNameFisrtTag?.Split('_') ?? Array.Empty<string>();

                        int AddressPartSecond = int.Parse(unPackfirstAdd.Split(':')[1]) + 15;
                        string LastlogicalAddress = $"{parts[0]}:{AddressPartSecond:000}";

                        List<XMIOConfig> usedTagList = xm.LoadedProject.Tags
                            .Where(T => T.LogicalAddress.StartsWith("F2"))
                            .Where(T => int.Parse(T.LogicalAddress.Split(':')[1]) >= int.Parse(unPackfirstAdd.Split(':')[1]) && int.Parse(T.LogicalAddress.Split(':')[1]) <= int.Parse(LastlogicalAddress.Split(':')[1])).OrderBy(T => T.LogicalAddress).OrderBy(T => T.LogicalAddress).ToList();

                        int i = 1;
                        toolTipText += usedTagList
                            .Select(tag => $"OP{i++}:{tag.LogicalAddress}:{(tag.Tag.Contains("Digital") ? "Bool" : tag.Label)}:{(tag.InitialValue ?? "")}:{(tag.Retentive ? "RET" : "")}")
                            .Aggregate((current, next) => current + "\n" + next);
                        UnPackFunctioBlock.Show(toolTipText, this);
                    }
                }
            }
            catch
            {
            }

        }

        private void PackTooltipText()
        {
            try
            {
                this.PackFunctioBlock.Active = true;
                string toolTipText = "";
                if (LadderDesign.HoverElement.Attributes["function_name"].ToString() == "Pack" && LadderDesign.HoverElement.Attributes.Count > 2)
                {
                    string packfirstAdd = LadderDesign.HoverElement.Attributes["input1"].ToString().TrimStart('~');
                    if (!string.IsNullOrEmpty(packfirstAdd))
                    {
                        XMIOConfig firstTag = xm.LoadedProject.Tags.FirstOrDefault(T => T.LogicalAddress == packfirstAdd);
                        if(firstTag == null)
                        {
                            return;
                        }
                        string actualNameFisrtTag = firstTag?.ActualName;
                        string[] parts = actualNameFisrtTag?.Split('_') ?? Array.Empty<string>();
                        int AddressPartSecond = int.Parse(packfirstAdd.Split(':')[1]) + 15;
                        string LastlogicalAddress = $"{parts[0]}:{AddressPartSecond:000}";
                        List<XMIOConfig> usedTagList = xm.LoadedProject.Tags
                            .Where(T => T.LogicalAddress.StartsWith("F2"))
                            .Where(T => int.Parse(T.LogicalAddress.Split(':')[1]) >= int.Parse(packfirstAdd.Split(':')[1]) && int.Parse(T.LogicalAddress.Split(':')[1]) <= int.Parse(LastlogicalAddress.Split(':')[1])).OrderBy(T => T.LogicalAddress).OrderBy(T => T.LogicalAddress).ToList();

                        int i = 1;

                        toolTipText += usedTagList
                            .Select(tag => $"IN{i++}:{tag.LogicalAddress}:{(tag.Tag.Contains("Digital") ? "Bool" : tag.Label)}:{(tag.InitialValue ?? "")}:{(tag.Retentive ? "RET" : "")}")
                            .Aggregate((current, next) => current + "\n" + next);

                        string firstOPTag = LadderDesign.HoverElement.Attributes["output1"].ToString();
                        XMIOConfig firstOutputTag = xm.LoadedProject.Tags.FirstOrDefault(T => T.LogicalAddress == firstOPTag);
                        var DefaultIOCheck1 = firstOutputTag?.Tag.Contains("Digital") == true ? "Bool" : firstOutputTag?.Label;

                        toolTipText += $"\nOP1:{firstOutputTag?.LogicalAddress}:{(DefaultIOCheck1 ?? "")}:{(firstOutputTag?.InitialValue ?? "")}:{(firstOutputTag?.Retentive == true ? "RET" : "")}";
                        PackFunctioBlock.Show(toolTipText, this);
                    }
                }
            }
            catch
            {

            }
        }

        private void LadderCanvas_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.toolTip.Hide(this);
            this.toolTip.InitialDelay = 2000;
            this.toolTip.UseFading = false;
            this.toolTip.UseAnimation = false;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        public static int ScrollX = 0;
        public static int ScrollY = 0;
        private void LadderCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if(!hasShiftOn)
            {
                this.Refresh();
                fullyRungElements.Clear();
            }
                
            if (XMPS.Instance.PlcStatus == "LogIn" && e.Button == MouseButtons.Right)
                return;
            this.UnPackFunctioBlock.Active = false;
            this.PackFunctioBlock.Active = false;
            this.FunctionBlockTool.Active = false;
            //for ToolTip Hide after selecting the Ladder Element
            this.toolTip.Hide(this);
            this.toolTip.Active = false;
            this.timerToolTip.Enabled = false;


            //if (IsLoggedIn()) return;
            Point LocalMousePosition = this.PointToClient(Cursor.Position);
            LadderElement ladderElement = laderPattern.Elements.MouseHover(null, e.X, LocalMousePosition.Y);
            if(ladderElement == null)
            {
                if (LadderDesign.ClickedElement != null)
                {
                    this.Refresh();
                    LadderDesign.ClickedElement = null;
                }
                if (e.X < 10)
                {
                    this.Refresh();
                    selectedElements.Clear();
                    fullyRungElements.Clear();
                    laderPattern.Elements.ChangeBackGroundColor(this, e.X, LocalMousePosition.Y, hasShiftOn, ref fullyRungElements);
                }
                else
                {
                    //this.Refresh();
                    laderPattern.Elements.ChangeBackGroundColor(this, e.X, LocalMousePosition.Y, hasShiftOn, ref fullyRungElements, true);
                }
                return;
            }
            else
            {
                if (fullyRungElements.Count > 0)
                    this.Refresh();

                fullyRungElements.Clear();
            }
                
            
            LadderEditorControl controlEditor = (LadderEditorControl)this.Parent;
            Graphics graphics = this.CreateGraphics();
            setGraphics(ref graphics);

            if (ladderElement != null)
            {

                if (e.Clicks > 0)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (LadderDesign.ClickedElement == null) return;
                        LadderElement clickedLadderElement = LadderDesign.ClickedElement;
                        bool isCommented = ContexMenuLadderCanvas(clickedLadderElement);
                        CntxMenuCommtRung.Visible = isCommented ? false : true;
                        cntxMenuUncommentRung.Visible = isCommented ? true : false;
                        CntxMenu.Show(this, new Point(e.X, LocalMousePosition.Y));
                        return;
                    }
                    //add to selection
                    if (hasShiftOn)
                    {
                        if (ladderElement.customDrawing.toString() == "Contact" || ladderElement.customDrawing.toString() == "FunctionBlock")
                        {
                            selectedElements.Add(ladderElement);
                        }
                        LadderDesign.ClickedElement = null;
                    }

                    if (controlEditor != null)
                    {
                        ScrollX = Math.Abs(controlEditor.AutoScrollOffset.X);
                        ScrollY = Math.Abs(controlEditor.AutoScrollOffset.Y);
                    }

                    //remove focus
                    if (LadderDesign.ClickedElement != null && ladderElement.customDrawing != null)
                    {
                        try
                        {
                            ICustomDrawing customDrawing = LadderDesign.ClickedElement.customDrawing;
                            if (customDrawing != null)
                                customDrawing.Draw(graphics, LadderDesign.ClickedElement);
                            LadderDesign.ClickedElement = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    LadderDesign.ClickedElement = ladderElement;
                    if (ladderElement.customDrawing != null)
                    {
                        ladderElement.customDrawing.OnSelect(graphics, ladderElement);
                    }

                    if (OnDoubleClickEvent != null && e.Clicks >= 2)
                    {
                        OnDoubleClickEvent(this, e);
                    }
                }

                if (ladderElement.mouseEvents != null)
                    for (int i = 0; i < ladderElement.mouseEvents.Length; i++)
                    {
                        if (e.Clicks > 0)
                        {
                            for (int eb = 0; eb < e.Clicks; eb++)
                                ladderElement.mouseEvents[i].Click(ladderElement, e.X, LocalMousePosition.Y, (int)e.Button);
                        }
                    }
            }
            else
            {
                hasShiftOn = false;
                selectedElements.Add(LadderDesign.ClickedElement);
                for (int r = 0; r < selectedElements.Count; r++)
                    if (selectedElements[r] != null && selectedElements[r].customDrawing != null)
                    {
                        try
                        {
                            ICustomDrawing customDrawing = selectedElements[r].customDrawing;
                            if (customDrawing != null)
                                customDrawing.Draw(graphics, selectedElements[r]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                selectedElements.Clear();
                LadderDesign.ClickedElement = null;
            }
        }

        private bool ContexMenuLadderCanvas(LadderElement clickedLadderElement)
        {
            if (xm.CurrentScreen != "MainForm#Main")
            {
                LadderElement rootElement = clickedLadderElement.getRoot();
                LadderElement firstRungElement = rootElement.Elements.First();
                foreach (Attribute attribute in firstRungElement.Attributes.ToList())
                {
                    if (attribute.Name == "isCommented")
                    {
                        return true;
                    }
                }
            }
            else
            {
                LadderElement rootElement = clickedLadderElement.getRoot();
                if (rootElement.Elements.Count() > 1)
                {
                    foreach (LadderElement ld in rootElement.Elements)
                    {
                        if (ld.CustomType == "LadderDrawing.LadderBlock")
                        {
                            foreach (Attribute attribute in ld.Attributes.ToList())
                            {
                                if (attribute.Name == "isCommented")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (LadderElement ld in rootElement.Elements)
                    {
                        if (ld.CustomType == "LadderDrawing.LadderBlock")
                        {
                            foreach (Attribute attribute in ld.Attributes.ToList())
                            {
                                if (attribute.Name == "isCommented")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

            }
            return false;
        }

        private void ShowTagDetails()
        {

            if (LadderDesign.ClickedElement != null)
            {

                if (LadderDesign.ClickedElement.Attributes["Caption"].ToString() != null && LadderDesign.ClickedElement.Attributes["Caption"].ToString() != "Comments" && LadderDesign.ClickedElement.Attributes["Caption"].ToString() != "FunctionBlock")
                {
                    var TooltipAns = LadderDesign.ClickedElement.Attributes["Caption"].ToString();
                    var ans = XMPS.Instance.LoadedProject.Tags.Find(d => d.Tag == TooltipAns);
                    if (ans != null)
                    {
                        toolTip.Active = true;
                        toolTip.AutomaticDelay = 1000;
                        toolTip.AutoPopDelay = 3000;
                        var DefaultIOCheck = (ans.Tag.Contains("Digital")) ? "" : ans.Label;
                        toolTip.Show("Address :" + $"{ans.LogicalAddress}" + " " + DefaultIOCheck, this);
                    }
                    else
                    {
                        toolTip.Active = true;
                        toolTip.Show(TooltipAns, this);
                    }
                }
                else if (LadderDesign.ClickedElement.Attributes["Caption"].ToString() == "FunctionBlock")
                {
                    string _tooltipText = "";
                    List<string> list = new List<string>();

                    var Input1 = LadderDesign.ClickedElement.Attributes["input1"].ToString();
                    if (Input1 != "-")
                    {
                        if (Input1 != "")
                            list.Add(Input1);
                    }


                    var Input2 = LadderDesign.ClickedElement.Attributes["input2"].ToString();
                    if (Input2 != "-")
                    {
                        if (Input2 != "")
                            list.Add(Input2);
                    }


                    var Input3 = LadderDesign.ClickedElement.Attributes["input3"].ToString();
                    if (Input3 != "-")
                    {
                        if (Input3 != "")
                            list.Add(Input3);
                    }


                    var Input4 = LadderDesign.ClickedElement.Attributes["input4"].ToString();
                    if (Input4 != "-")
                    {
                        if (Input4 != "")
                            list.Add(Input4);
                    }


                    var Input5 = LadderDesign.ClickedElement.Attributes["input5"].ToString();
                    if (Input5 != "-")
                    {
                        if (Input5 != "")
                            list.Add(Input5);
                    }


                    var Output1 = LadderDesign.ClickedElement.Attributes["output1"].ToString();
                    if (Output1 != "-")
                    {
                        if (Output1 != "")
                            list.Add(Output1);
                    }


                    var Output2 = LadderDesign.ClickedElement.Attributes["output2"].ToString();
                    if (Output2 != "-")
                    {
                        if (Output2 != "")
                            list.Add(Output2);
                    }


                    var Output3 = LadderDesign.ClickedElement.Attributes["output3"].ToString();
                    if (Output3 != "-")
                    {
                        if (Output3 != "")
                            list.Add(Output3);
                    }

                    if (list.Count != 0)
                    {
                        _tooltipText = string.Join(", ", list);
                        toolTip.Show(_tooltipText, this);
                    }
                    else
                    {
                        toolTip.Show("FunctionBlock", this);
                    }
                }


            }
        }

        bool hasShiftOn = false;
        private void LadderCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsLoggedIn()) return;
            if (e.Shift)
            {
                hasShiftOn = true;
            }
            else if (e.KeyValue == 46) // Delete Key Press
            {
                if(fullyRungElements.Count > 0)
                {
                    int rungNo = 0;
                    foreach(LadderElement ladderElement in fullyRungElements)
                    {
                        rungNo++;
                        bool islast = fullyRungElements.Count == rungNo ? true : false;
                        LadderDesign.ClickedElement = ladderElement;
                        DeleteRungbyContextMenu(islast);
                        LadderDesign.ClickedElement = null;
                    }
                    Invalidate();
                    Update();
                    Refresh();
                    fullyRungElements.Clear();
                    selectedElements.Clear();
                }
                else
                {
                    DeleteWithCounter(e);
                }
            }
        }

        public void DeleteWithCounter(KeyEventArgs e)
        {
            ItemDeleted(this, e);
            DeleteSelectedElement();
        }

        private void DeleteSelectedElement()
        {
            if (IsLoggedIn()) return;
            LadderDesign L = this.getDesignView();
            isDeleted = true;
            L.DeleteSelectedControl(isDeleted);
            int delRungNo = -1, delHeight = -1;
            foreach (LadderElement rungElements in L.Elements)
            {
                if (rungElements.Elements.Count == 0)
                {
                    delRungNo = rungElements.Position.Index;
                    delHeight = rungElements.getHeight();
                    this.getDesignView().Elements.Remove(rungElements);
                    break;
                }

            }
            if (delRungNo >= 0)
            {
                foreach (LadderElement rungElements in L.Elements)
                {
                    if (rungElements.Position.Index >= delRungNo)
                    {
                        rungElements.Position.Index--;
                        rungElements.Position.Height -= delHeight;
                    }

                }
            }
            LadderEditorControl parent = (LadderEditorControl)this.Parent;
            this.Refresh();
            LadderDesign.currentRungScroll = parent.VerticalScrollValue;
            parent.ReScale();
            Invalidate();
            Update();
            Refresh();
            LadderDesign.ClickedElement = null;
            selectedElements.Clear();
            LadderDrawing.Global.ClearActive();
        }

        public void DeleteRung(LadderElement selectedElement)
        {
            if (IsLoggedIn()) return;
            LadderDesign L = this.getDesignView();
            LadderDesign.ClickedElement = selectedElement;

            ItemDeleted(this, new KeyEventArgs(Keys.Delete));
            int delRungNo = -1, delHeight = -1;
            LadderElement rootElement = selectedElement.getRoot();
            delRungNo = rootElement.Position.Index;
            delHeight = rootElement.getHeight();
            this.getDesignView().Elements.Remove(rootElement);

            if (delRungNo >= 0)
            {
                foreach (LadderElement rungElements in L.Elements)
                {
                    if (rungElements.Position.Index >= delRungNo)
                    {
                        rungElements.Position.Index--;
                       // rungElements.Position.Height -= delHeight;
                    }

                }
            }
            LadderEditorControl parent = (LadderEditorControl)this.Parent;
            parent.ReScale();
            Invalidate();
            Update();
            Refresh();
            LadderDesign.ClickedElement = null;
            LadderDrawing.Global.ClearActive();
        }
        private void LadderCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            // if PN status Element is present then set to ClickedElement for performing next function.
            if (LadderDesign.PNStatusElement != null)
            {
                LadderDesign.ClickedElement = LadderDesign.PNStatusElement;
                LadderDesign.PNStatusElement = null;
            }
            //if (IsLoggedIn()) return;
            if (hasShiftOn)
            {
                hasShiftOn = false;
            }
            //Adding additional logic for the Opening HELP Instruction
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    if (LadderDesign.ClickedElement == null) return;
                    MovetoNextControl(e.KeyCode, LadderDesign.ClickedElement);
                    break;
                case Keys.Enter:
                    if (e.Modifiers == Keys.Control)
                        OnDoubleClickEvent(this, new MouseEventArgs(MouseButtons.Left, 1, LadderDesign.ClickedElement.Position.X + 10, LadderDesign.ClickedElement.Position.Y, 0));
                    break;
                case Keys.F1:
                    if (LadderDesign.ClickedElement != null && LadderDesign.ClickedElement.Attributes["caption"].ToString() == "FunctionBlock")
                    {
                        string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\InstructionInformationFile.chm");
                        string topicName = LadderDesign.ClickedElement.Attributes["function_name"].ToString();
                        if (topicIdMap.TryGetValue(topicName, out string topicPath))
                        {
                            Help.ShowHelp(this, filePath, HelpNavigator.Topic, topicPath);
                        }
                        // Help.ShowHelp(this, chmFilePath, HelpNavigator.TopicId, helpContextValue.ToString());
                    }
                    else if (LadderDesign.ClickedElement == null)
                    {
                        string filePath = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\MessungSystems\XMPS2000\InstructionInformationFile.chm");
                        Help.ShowHelp(this, filePath, HelpNavigator.Topic, "Files/AND.htm");

                    }
                    break;
            }
        }
        private void MovetoNextControl(Keys keyCode, LadderElement clickedElement)
        {
            if (clickedElement.customDrawing.toString() == "Rung") return;

            if (keyCode == Keys.Right)
            {
                HeighlightNextControl(clickedElement, 1);
            }
            else if (keyCode == Keys.Left)
            {
                HeighlightNextControl(clickedElement, -1);

            }
            else if (keyCode == Keys.Down)
            {
                HeighlightControlBelow(clickedElement, 1);
            }
            else if (keyCode == Keys.Up)
            {
                HeighlightControlBelow(clickedElement, -1);
            }
        }

        private void HeighlightControlBelow(LadderElement clickedElement, int val)
        {
            LadderElement NextElement = new LadderElement();
            LadderElement nextRung = new LadderElement();
            try
            {
                if (clickedElement.Position.RelateTo.Count > 0 && clickedElement.Elements.Count > 0)
                {
                    if (val > 0)
                        NextElement = clickedElement.Elements.Where(e => e.Position.RelateTo.Count() > 0).FirstOrDefault();
                    else
                        NextElement = clickedElement.Position.Parent.customDrawing.toString() == "DummyParallelParent" ? clickedElement.Position.RelateTo[0] : clickedElement.Position.Parent;
                }
                else if (clickedElement.Position.RelateTo.Count > 0 && clickedElement.Elements.Count == 0 && val < 0)
                {
                    NextElement = clickedElement.Position.Parent.customDrawing.toString() == "DummyParallelParent" ? clickedElement.Position.RelateTo[0] : clickedElement.Position.Parent;
                }
                else if (clickedElement.Position.Index > 0 && clickedElement.getRoot().Elements[clickedElement.Position.Index - 1].customDrawing.toString() == "DummyParallelParent")
                {
                    if (clickedElement.getRoot().Elements[clickedElement.Position.Index - 1].Elements.Count() > 0)
                    {
                        NextElement = clickedElement.getRoot().Elements[clickedElement.Position.Index - 1].Elements[0];
                    }
                }
                else if (clickedElement.customDrawing.toString().Contains("Coil") && clickedElement.Elements.Count > 0 && val > 0)
                {
                    NextElement = clickedElement.Elements[0];
                }
                else if (clickedElement.customDrawing.toString().Contains("Coil") && val < 0)
                {
                    NextElement = clickedElement.Position.Parent;
                }
                else
                {
                    ///////Go to next or previous rung 
                    if (val > 0)
                        nextRung = this.getDesignView().Elements.Where(e => e.Position.Y > clickedElement.getRoot().Position.Y).FirstOrDefault();
                    else
                        nextRung = this.getDesignView().Elements.Where(e => e.Position.Y < clickedElement.getRoot().Position.Y).LastOrDefault();
                    if (nextRung == null)
                        return;
                    NextElement = nextRung.Elements.Where(t => t.customDrawing.toString() != "Comment" && t.customDrawing.toString() != "HorizontalLine" && t.customDrawing.toString() != "DummyParallelParent").FirstOrDefault();
                }
                if (NextElement == null) return;
                clickedElement = NextElement;
                Graphics graphics = this.CreateGraphics();
                setGraphics(ref graphics);
                ICustomDrawing customDrawing = LadderDesign.ClickedElement.customDrawing;
                if (customDrawing != null)
                    customDrawing.Draw(graphics, LadderDesign.ClickedElement);
                LadderDesign.ClickedElement = null;

                LadderDesign.ClickedElement = clickedElement;
                if (clickedElement.customDrawing != null)
                {
                    clickedElement.customDrawing.OnSelect(graphics, clickedElement);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HeighlightNextControl(LadderElement clickedElement, int val)
        {
            LadderElement NextElement = new LadderElement();
            try
            {
                if (clickedElement.Position.RelateTo.Count > 0 || clickedElement.Position.Parent != clickedElement.getRoot())
                {
                    if (val > 0)
                        if (clickedElement.Position.ConnectTo.Count() == 0)
                            NextElement = clickedElement.Position.Parent.Position.ConnectTo[clickedElement.Position.Parent.Position.ConnectTo.FindIndex(c => c == clickedElement) + 1];
                        else
                            NextElement = clickedElement.Position.ConnectTo[clickedElement.Position.Parent.Position.ConnectTo.FindIndex(c => c == clickedElement) + 1];
                    else
                        NextElement = clickedElement.Position.Parent;

                }
                else
                {
                    if (val > 0)
                        NextElement = LadderDesign.ClickedElement.getRoot().Elements.Where(t => t.Position.Y == clickedElement.Position.Y && t.Position.X > clickedElement.Position.X + 10 && t.customDrawing.toString() != "Comment" && t.customDrawing.toString() != "HorizontalLine").FirstOrDefault();
                    else
                        NextElement = LadderDesign.ClickedElement.getRoot().Elements.Where(t => t.customDrawing.toString() != "HorizontalLine" && t.customDrawing.toString() != "Comment" && t.customDrawing.toString() != "DummyParallelParent" && t.Position.Y == clickedElement.Position.Y && t.Position.X < clickedElement.Position.X - 10).LastOrDefault();
                }
                if (NextElement == null) return;
                clickedElement = NextElement;
                Graphics graphics = this.CreateGraphics();
                setGraphics(ref graphics);
                ICustomDrawing customDrawing = LadderDesign.ClickedElement.customDrawing;
                if (customDrawing != null)
                    customDrawing.Draw(graphics, LadderDesign.ClickedElement);
                LadderDesign.ClickedElement = null;

                LadderDesign.ClickedElement = clickedElement;
                if (clickedElement.customDrawing != null)
                {
                    clickedElement.customDrawing.OnSelect(graphics, clickedElement);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LadderCanvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.PackFunctioBlock.Active = false;
            this.UnPackFunctioBlock.Active = false;
            this.FunctionBlockTool.Active = false;
            this.isPackMethodCalled = false;
            this.isUnPackMethodCalled = false;
            this.isFBToolTipMethodCalled = false;
            if (LadderDesign.ClickedElement != null)
            {
            }
        }
        private void DoubleClickEvent(MouseEventArgs e)
        {
            this.toolTip.Active = false;
            this.timerToolTip.Stop();

            if (IsLoggedIn())
            {
                if (IsInMainBlock())
                {
                    LadderElement ladderElement = laderPattern.Elements.MouseHover(null, e.X, e.Y < 0 ? 32767 + -1 * (-32767 - e.Y) : e.Y);

                    if (ladderElement != null)
                    {
                        if (OnDoubleClickEvent != null && e.Clicks >= 2 && ladderElement.customDrawing.ToString() != "LadderDrawing.BlankLine")
                        {
                            LadderDesign.ClickedElement = ladderElement;
                            OnDoubleClickEvent(this, e);

                        }
                    }
                }
            }
            else
            {
                LadderElement ladderElement = laderPattern.Elements.MouseHover(null, e.X, e.Y < 0 ? 32767 + -1 * (-32767 - e.Y) : e.Y);
                Graphics graphics = this.CreateGraphics();
                setGraphics(ref graphics);
                if (ladderElement != null)
                {
                    if (e.Clicks > 0)
                    {
                        this.toolTip.Hide(this);
                        this.timerToolTip.Stop();
                        //add to selection
                        if (hasShiftOn)
                        {
                            selectedElements.Add(ladderElement);
                            LadderDesign.ClickedElement = null;
                        }
                        //remove focus
                        if (LadderDesign.ClickedElement != null && ladderElement.customDrawing != null)
                        {
                            try
                            {
                                ICustomDrawing customDrawing = LadderDesign.ClickedElement.customDrawing;
                                if (customDrawing != null)
                                    customDrawing.Draw(graphics, LadderDesign.ClickedElement);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        LadderDesign.ClickedElement = ladderElement;
                        if (ladderElement.customDrawing != null)
                        {
                            ladderElement.customDrawing.OnSelect(graphics, ladderElement);
                        }

                        if (OnDoubleClickEvent != null && e.Clicks >= 2 && ladderElement.customDrawing.ToString() != "LadderDrawing.BlankLine")
                        {
                            OnDoubleClickEvent(this, e);
                        }
                    }
                    if (ladderElement.mouseEvents != null)
                        for (int i = 0; i < ladderElement.mouseEvents.Length; i++)
                        {
                            if (e.Clicks > 0)
                            {
                                for (int eb = 0; eb < e.Clicks; eb++)
                                    ladderElement.mouseEvents[i].Click(ladderElement, e.X, e.Y < 0 ? 32767 + -1 * (-32767 - e.Y) : e.Y, (int)e.Button);
                            }
                        }
                }
            }
        }

        private void LadderCanvas_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void LadderCanvas_DragDrop(object sender, DragEventArgs e)
        {
            Point newpoint = this.PointToClient(new Point(e.X, e.Y < 0 ? 32767 + -1 * (-32767 - e.Y) : e.Y));
            LadderElement onelement = laderPattern.Elements.MouseHover(null, newpoint.X, newpoint.Y);
            if (onelement != null && OnDragCreate != null)
            {
                OnDragCreate(onelement, e);
            }
        }

        private XMPS xm = XMPS.Instance;
        private bool IsLoggedIn()
        {
            if (xm.PlcStatus == "LogIn")
                return true;

            return false;
        }
        private bool IsInMainBlock()
        {
            return xm.presentInMain;
        }

        private void CntxMenuDelete_Click(object sender, EventArgs e)
        {
            DeleteRungbyContextMenu();
            Invalidate();
            Update();
            Refresh();
        }

        public void DeleteRungbyContextMenu(bool islast = true)
        {
            if (LadderDesign.ClickedElement == null) return;
            LadderDesign L = this.getDesignView();
            int currentDeleteRungNo = 0;
            // If there are no multiple selected elements, then set the clicked element as the selected element
            if (selectedElements.Count == 0)
                selectedElements.Add(LadderDesign.ClickedElement);
            foreach (LadderElement r in selectedElements)
            {
                LadderDesign.ClickedElement = r.getRoot();
                ItemDeleted(this, new KeyEventArgs(Keys.Execute));
                if (xm.CurrentScreen.ToString().StartsWith("MainForm"))
                    LadderDesign.ClickedElement = r;
                isDeleted = true;
                L.DeleteRung(r.getRoot(), isDeleted);
                int delRungNo = -1, delHeight = -1;
                foreach (LadderElement rungElements in L.Elements)
                {
                    if (rungElements.Elements.Count == 0)
                    {
                        delRungNo = rungElements.Position.Index;
                        delHeight = rungElements.getHeight();
                        this.getDesignView().Elements.Remove(rungElements);
                        break;
                    }

                }

                if (delRungNo >= 0)
                {
                    currentDeleteRungNo = delRungNo;
                    foreach (LadderElement rungElements in L.Elements)
                    {
                        if (rungElements.Position.Index >= delRungNo)
                        {
                            rungElements.Position.Index--;
                           // rungElements.Position.Height -= delHeight;
                        }

                    }
                }
            }
            LadderEditorControl parent = (LadderEditorControl)this.Parent;
            if (this.getDesignView().Elements.Count > 0 && islast)
            {
                this.Refresh();
                LadderDesign.currentRungScroll = parent.VerticalScrollValue;
               
            }
            parent.ReScale();
           
            LadderDesign.ClickedElement = null;
            selectedElements.Clear();
            LadderDrawing.Global.ClearActive();
            this.getDesignView().SetStateForUndoRedo();       //Insert current element into undo stack after ddelete opeation perform.

            if(this.getDesignView().Elements.Count > 0 && islast)
            {
                LadderElement ladderElement = parent.getCanvas().getDesignView().m_Height_dic.Count == currentDeleteRungNo ?
                                           parent.getCanvas().getDesignView().m_Height_dic.ElementAt(currentDeleteRungNo - 1).Value
                                          : parent.getCanvas().getDesignView().m_Height_dic.ElementAt(currentDeleteRungNo).Value;

                laderPattern.Elements.HighlightRungElements(this, ladderElement, true);
            }
        }

        private void CntxMenuInsAftr_Click(object sender, EventArgs e)
        {
            ///<>
            ///Added Check for the Adding only 10 Rungs in Interrupt Logic Block
            string currentScreenName = xm.CurrentScreen.Split('#')[1];
            if (currentScreenName.StartsWith("Interrupt_Logic_Block") && LadderDesign.Active.Elements.Count >= 10)
            {
                MessageBox.Show("Maximum Limit of Rung For Interrupt Block Exceed", "XMPS 2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (LadderDesign.ClickedElement == null) return;
            LadderEditorControl parent = (LadderEditorControl)this.Parent;
            LadderDesign L = this.getDesignView();
            int intRungNo = LadderDesign.ClickedElement.getRoot().Position.Index + 1;

            foreach (LadderElement rungElements in L.Elements)
            {
                if (rungElements.Position.Index >= intRungNo)
                {
                    rungElements.Position.Index++;
                }

            }
            parent.InsertRung(intRungNo);
            Invalidate();
            Update();
            Refresh();
            LadderDesign.ClickedElement = null;
            LadderDrawing.Global.ClearActive();
            //showing focus on rung which is inseting after.
            LadderElement ladderElement = parent.getCanvas().getDesignView().m_Height_dic.ElementAt(intRungNo).Value;
            parent.getCanvas().getDesignView().Elements.HighlightRungElements(parent.getCanvas(), ladderElement, true);
        }

        private void timerToolTip_Tick(object sender, EventArgs e)
        {
            if (isToolTipShowing)
            {
                this.FunctionBlockTool.Hide(this);
                this.PackFunctioBlock.Hide(this);
                this.UnPackFunctioBlock.Hide(this);
                this.isToolTipShowing = false;
            }
        }

        public void ToolTipText()
        {
            if (LadderDesign.HoverElement != null)
            {
                if (LadderDesign.HoverElement.Attributes["Caption"].ToString() != null && LadderDesign.HoverElement.Attributes["Caption"].ToString() != "Comments" && LadderDesign.HoverElement.Attributes["Caption"].ToString() != "FunctionBlock")
                {
                    var TooltipAns = LadderDesign.HoverElement.Attributes["Caption"].ToString();
                    var ans = XMPS.Instance.LoadedProject.Tags.Find(d => d.Tag.Trim() == TooltipAns.Trim());
                    if (ans != null)
                    {
                        var DefaultIOCheck = (ans.Tag.Contains("Digital")) ? "Bool" : ans.Label;

                        string toolTiptext = $"{ans.LogicalAddress}" + ":" +
                                             $"{(DefaultIOCheck != null ? DefaultIOCheck : "")}" + ":" +
                                             $"{(ans.InitialValue != null ? ans.InitialValue : "")}" + ":" +
                                             $"{(ans.Retentive ? "RET" : "")}";
                        toolTip.Show(toolTiptext, this);
                    }
                    else
                    {
                        toolTip.Show(TooltipAns, this);
                    }
                }
                else
                {
                    toolTip.Show("FunctionBlock", this);
                }
            }
        }
        private void LadderCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (XMPS.Instance.PlcStatus == "LogIn" && e.Button == MouseButtons.Right)
                return;
            this.isPackMethodCalled = false;
            this.isUnPackMethodCalled = false;
            this.isFBToolTipMethodCalled = false;
            this.FunctionBlockTool.Active = false;
            this.toolTip.Active = false;
            this.timerToolTip.Stop();
            if (e.Button == MouseButtons.Right && xm.CurrentScreen.StartsWith("MainForm"))
                CntxMenuInsAftr.Visible = false;
            else
                CntxMenuInsAftr.Visible = true;

        }

        private void CntxMenuCommtRung_Click(object sender, EventArgs e)
        {
            commentRung();
            if (xm.CurrentScreen.StartsWith("MainForm#"))
            {
                MainLogicRefresh(this, e);
                LadderDesign.ClickedElement = null;
            }
            getDesignView().SetStateForUndoRedo();
            Refresh();
        }

        public void commentRung()
        {
            if (LadderDesign.ClickedElement == null) return;
            LadderDesign L = this.getDesignView();
            Graphics graphics = this.CreateGraphics();
            LadderElement selectedElementComment = LadderDesign.ClickedElement;
            LadderElement rootElement = selectedElementComment.getRoot();
            if (selectedElementComment.CustomType == "LadderDrawing.LadderBlock")
            {
                if (rootElement.Elements.Count() > 1)
                {
                    foreach (LadderElement ld in rootElement.Elements)
                    {
                        if (ld.CustomType == "LadderDrawing.LadderBlock")
                        {
                            Attribute attribute = new Attribute();
                            attribute.Name = "isCommented";
                            ld.Attributes.Add(attribute);
                        }
                    }
                    xm.LoadedProject.MainLadderLogic[rootElement.Position.Index] = "'" +
                        xm.LoadedProject.MainLadderLogic[rootElement.Position.Index];
                }
                else
                {
                    if (!xm.LoadedProject.MainLadderLogic[rootElement.Position.Index].ToString().StartsWith("'"))
                    {
                        xm.LoadedProject.MainLadderLogic[rootElement.Position.Index] = "'" +
                            xm.LoadedProject.MainLadderLogic[rootElement.Position.Index];
                    }
                }
                return;
            }
            foreach (LadderElement ladderElement in rootElement.Elements)
            {
                Attribute attribute = new Attribute();
                attribute.Name = "isCommented";
                ladderElement.Attributes.Add(attribute);
                if (ladderElement.CustomType == "LadderDrawing.Coil")
                {
                    GetCoilParallelElementForCommentRung(ladderElement);
                }
            }

        }

        private void GetCoilParallelElementForCommentRung(LadderElement ladderElement)
        {
            //Base case -> if Element is null stop
            if (ladderElement.Elements.Count == 0)
                return;

            //Get The Next Element 
            LadderElement coilParallel = ladderElement.Elements.First();
            Attribute attribute = new Attribute();
            attribute.Name = "isCommented";
            coilParallel.Attributes.Add(attribute);
            GetCoilParallelElementForCommentRung(coilParallel);

        }

        private void cntxMenuUncommentRung_Click(object sender, EventArgs e)
        {
            LadderElement selectedElementComment = LadderDesign.ClickedElement;
            LadderElement rootElement = selectedElementComment.getRoot();
            if (selectedElementComment.CustomType == "LadderDrawing.LadderBlock" || xm.CurrentScreen.StartsWith("MainForm"))
            {
                if (rootElement.Elements.Count() > 1)
                {
                    foreach (LadderElement ld in rootElement.Elements)
                    {
                        if (ld.CustomType == "LadderDrawing.LadderBlock")
                        {
                            foreach (Attribute attribute in ld.Attributes.ToList())
                            {
                                if (attribute.Name == "isCommented")
                                {
                                    ld.Attributes.Remove(attribute);
                                }
                            }
                            if (selectedElementComment.CustomType == "LadderDrawing.Contact")
                            {
                                LadderElement ld1 = rootElement.Elements.Where(T => T.CustomType == "LadderDrawing.LadderBlock").FirstOrDefault();
                                xm.LoadedProject.MainLadderLogic[rootElement.Position.Index] =
                            xm.LoadedProject.MainLadderLogic[rootElement.Position.Index].Replace("'", "");
                            }
                            else
                            {
                                xm.LoadedProject.MainLadderLogic[rootElement.Position.Index] =
                            xm.LoadedProject.MainLadderLogic[rootElement.Position.Index].Replace("'", "");
                            }
                        }
                    }

                }
                else
                {
                    xm.LoadedProject.MainLadderLogic[rootElement.Position.Index] =
                        xm.LoadedProject.MainLadderLogic[rootElement.Position.Index].Replace("'", "");
                }
                MainLogicRefresh(this, e);
                LadderDesign.ClickedElement = null;
            }
            else if (selectedElementComment != null)
            {

                foreach (LadderElement ladderElement in rootElement.Elements)
                {
                    if ((ladderElement.CustomType == "LadderDrawing.Coil"))
                    {
                        GetCoilParallelElementForUnCommentRung(ladderElement);
                    }
                    else
                    {
                        foreach (Attribute attribute in ladderElement.Attributes.ToList())
                        {
                            if (attribute.Name == "isCommented")
                            {
                                ladderElement.Attributes.Remove(attribute);
                            }
                        }
                    }

                }
            }
            getDesignView().SetStateForUndoRedo();
            Refresh();
        }

        private void GetCoilParallelElementForUnCommentRung(LadderElement ladderElement)
        {
            foreach (Attribute attribute in ladderElement.Attributes.ToList())
            {
                if (attribute.Name == "isCommented")
                {
                    ladderElement.Attributes.Remove(attribute);
                }
            }
            if (ladderElement.Elements.Count == 0)
                return;
            LadderElement coilParallel = ladderElement.Elements.First();
            GetCoilParallelElementForUnCommentRung(coilParallel);
        }

        private void LadderCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            //Checking if PNStatusElement is having element data if then clear and referesh canvas.
            if (LadderDesign.PNStatusElement != null)
            {
                LadderDesign.PNStatusElement = null;
                LadderDesign.currentRungScroll = 0;
                ((LadderEditorControl)this.Parent).RefreshCanvas();
            }
            if (XMPS.Instance.PlcStatus == "LogIn" && e.Button == MouseButtons.Right)
                return;
            if (e.Clicks > 1)
            {
                //adding All double click logic event Logic 
                this.PackFunctioBlock.Active = false;
                this.UnPackFunctioBlock.Active = false;
                this.FunctionBlockTool.Active = false;
                this.isPackMethodCalled = false;
                this.isUnPackMethodCalled = false;
                this.isFBToolTipMethodCalled = false;
                DoubleClickEvent(e);
            }
        }

        public void ChaneCursorPosition(Point erroredRungPosition)
        {
            this.AutoScroll = true;
            Point screenPosition = this.PointToScreen(erroredRungPosition);
            Cursor.Position = screenPosition;
        }

        private void CntxMenuCrossRef_Click(object sender, EventArgs e)
        {
            if (LadderDesign.ClickedElement != null)
            {
                CrossReferanceClicked(this.ParentForm,e);
            }

        }
    }

    public class ToolTipText
    {
        public string LogicalAddress { get; set; }
        public XMIOConfig Tag { get; set; }
        public string Type { get; set; }
    }

}


