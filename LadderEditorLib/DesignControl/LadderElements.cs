using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
namespace LadderDrawing
{
    public class LadderElement
    {
        string m_Id = "";
        bool m_Negation = false;
        public string Id { set { m_Id = value; } get { return m_Id; } }
        public bool Negation { set { m_Negation = value; } get { return m_Negation; } }
        string m_Name = "";
        public string Name { set { m_Name = value; } get { return m_Name; } }
        Attributes m_Attributes = new Attributes();
        public Attributes Attributes { set { m_Attributes = value; } get { return m_Attributes; } }
        Position m_Position = new Position();
        public Position Position { set { m_Position = value; } get { return m_Position; } }
        LadderDrawingTypes m_Type = 0;
        public LadderDrawingTypes Type { set { m_Type = value; } get { return m_Type; } }
        LineDirection m_Direction = 0;
        public LineDirection Direction { set { m_Direction = value; } get { return m_Direction; } }
        LinePosition m_LinePosition = LinePosition.CenterMid;
        public LinePosition LinePosition { set { m_LinePosition = value; } get { return m_LinePosition; } }
        int m_LineWidth = 1;
        public int LineWidth { set { m_LineWidth = value; } get { return m_LineWidth; } }
        Color m_LineColor = Color.Black;
        public Color LineColor { set { m_LineColor = value; } get { return m_LineColor; } }
        Color m_BackgroundColor = Color.Transparent;
        public Color BackgroundColor { set { m_BackgroundColor = value; } get { return m_BackgroundColor; } }
        LadderElements m_Elements = new LadderElements();
        public LadderElements Elements { set { m_Elements = value; } get { return m_Elements; } }
        string m_Customtype = "";
        public string CustomType { set { m_Customtype = value; if (customDrawing == null && value != "") CreateCustom(this.GetType().Assembly.CreateInstance(value), Position.X, Position.Y, Position.Width, Position.Height); } get { return m_Customtype; } }
        public ICustomDrawing customDrawing = null;
        public MouseEvent[] mouseEvents = null;
        public LadderElement(LadderElement DeletedRungByContextMenu)
        {
            this.CustomType = DeletedRungByContextMenu.CustomType;
            this.Elements = new LadderElements();
            this.Position.Parent = DeletedRungByContextMenu.Position.Parent;
            this.Position.Index = DeletedRungByContextMenu.Position.Index;
            this.Id = DeletedRungByContextMenu.Id;
            foreach (LadderElement element in DeletedRungByContextMenu.Elements)
            {
                LadderElement ele = new LadderElement();
                ele.CustomType = element.CustomType;
                ele.Id = element.Id;
                if (element.Attributes.Count > 0)
                {
                    foreach (Attribute attribute in element.Attributes)
                    {
                        ele.Attributes.Add(attribute);
                    }
                }
                if (element.Elements.Count > 0)
                {
                    foreach (LadderElement ele1 in element.Elements)
                    {
                        ele.Elements.Add(ele1);
                    }
                }
                this.Elements.Add(ele);
            }
        }
        public LadderElement getRoot()
        {
            LadderElement root = this;
            while (root.Position.Parent != null)
                root = root.Position.Parent;
            return root;
        }
        public LadderElement getRoot(Type type)
        {
            LadderElement root = this;
            while (root.Position.Parent != null)
            {
                if (root.GetType().Equals(type))
                    break;
                root = root.Position.Parent;
            }
            return root;
        }
        public int getX()
        {
            int x = 0;
            LadderElement element = this;
            while (element != null)
            {
                x += element.Position.X;
                element = element.Position.Parent;
            }
            return x;
        }
        public int getWidth()
        {
            int x = Position.Width;
            for (int e = 0; e < Elements.Count; e++)
            {
                int childWidth = Elements[e].getWidth();
                x += childWidth;
            }
            return x;
        }
        public int getY()
        {
            int y = 0;
            LadderElement element = this;
            while (element != null)
            {
                y += element.Position.Y;
                element = element.Position.Parent;
            }
            return y;
        }
        public int getHeight()
        {
            int maxY = Position.Height;
            for (int e = 0; e < Elements.Count; e++)
            {
                int childHeight = Elements[e].getHeight();
                if (maxY < Elements[e].Position.Y + childHeight)
                {
                    maxY = Elements[e].Position.Y + childHeight;
                }
            }
            return maxY;
        }

        public void AddToReferenceDictionary(Dictionary<string, LadderElement> refdict)
        {
            if (! refdict.TryGetValue(Id, out _))
            {
                refdict.Add(Id, this);
                foreach (var element in Elements)
                    element.AddToReferenceDictionary(refdict);
            }
        }

        public int getHeightbyY()
        {
            int y = Position.Y;
            int maxY = 0;
            for (int e = 0; e < Elements.Count; e++)
            {
                int childHeight = Elements[e].getHeightbyY();
                if (maxY < Elements[e].Position.Y + childHeight)
                {
                    maxY = Elements[e].Position.Y + childHeight;
                    y += maxY;
                }
            }
            return y;
        }
        public LadderElement()
        {
            Id = Guid.NewGuid().ToString();
            m_Elements.Parent = this;
        }
        private static void WriteAttribute(ref BinaryWriter writter, ref object value, string name)
        {
            PropertyInfo info = value.GetType().GetProperty(name);
            if (info != null)
            {
                writter.Write(info.GetValue(value).ToString());
            }
        }
        private static void ReadAttribute(ref BinaryReader reader, ref object value, string name)
        {
            PropertyInfo info = value.GetType().GetProperty(name);
            if (info != null)
            {
                info.SetValue(value, Convert.ChangeType(reader.ReadString(), info.PropertyType), null);
            }
        }
        private static LadderElement Create(ref BinaryReader reader)
        {
            LadderElement element = new LadderElement();
            ICustomDrawing createnew = (ICustomDrawing)element.GetType().Assembly.CreateInstance(reader.ReadString());
            int x = Convert.ToInt32(reader.ReadString());
            int y = Convert.ToInt32(reader.ReadString());
            element = createnew.Create(x, y);
            return element;
        }
        private static LadderElement readElement(ref BinaryReader reader, LadderElements parent)
        {
            LadderElement element = Create(ref reader);
            object refValue;
            refValue = element.Position;
            parent.Add(element);
            ReadAttribute(ref reader, ref refValue, "Index");
            refValue = element;
            ReadAttribute(ref reader, ref refValue, "Id");
            ReadAttribute(ref reader, ref refValue, "Name");
            int count = reader.ReadInt32();
            string[] strlist = new string[count];
            for (int x = 0; x < count; x++)
            {
                strlist[x] = reader.ReadString();
                if (strlist[x] != null && strlist[x] != "")
                {
                    LadderElement root = parent.Parent;
                    element.Position.RelateTo.Add(root.Elements.FindById(strlist[x]));
                }
            }
            count = reader.ReadInt32();
            for (int x = 0; x < count; x++)
            {
                string att_name = reader.ReadString();
                string att_value = reader.ReadString();
                Attribute attribute = new Attribute();
                attribute.Name = att_name;
                attribute.Value = att_value;
                element.Attributes.Add(attribute);
            }
            count = reader.ReadInt32();
            element.Elements.Parent = element;
            for (int x = 0; x < count; x++)
            {
                readElement(ref reader, element.Elements);
            }
            return element;
        }
        private static LadderElement writeElement(ref BinaryWriter writter, LadderElement element)
        {
            object refValue = element;
            WriteAttribute(ref writter, ref refValue, "CustomType");
            refValue = element.Position;
            WriteAttribute(ref writter, ref refValue, "X");
            WriteAttribute(ref writter, ref refValue, "Y");
            WriteAttribute(ref writter, ref refValue, "Index");
            if (refValue != null)
                WriteAttribute(ref writter, ref refValue, "Id");
            else
                writter.Write("0");
            refValue = element;
            WriteAttribute(ref writter, ref refValue, "Id");
            WriteAttribute(ref writter, ref refValue, "Name");
            writter.Write(element.Position.RelateTo.Count);
            for (int x = 0; x < element.Position.RelateTo.Count; x++)
            {
                writter.Write(element.Position.RelateTo[x].Id);
            }
            int count = element.Attributes.Count;
            writter.Write(element.Attributes.Count);
            for (int x = 0; x < count; x++)
            {
                writter.Write(element.Attributes[x].Name);
                writter.Write(element.Attributes[x].Value.ToString());
            }
            writter.Write(element.Elements.Count);
            for (int x = 0; x < element.Elements.Count; x++)
            {
                writeElement(ref writter, element.Elements[x]);
            }
            return element;
        }
        public static LadderElements fromStringFormat(string text)
        {
            if (text.Length < 4)
                return new LadderElements();
            MemoryStream stream = new MemoryStream();
            byte[] data = Convert.FromBase64String(text);
            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            BinaryReader reader = new BinaryReader(stream);
            LadderElements elements = new LadderElements();
            int count = reader.ReadInt32();
            for (int x = 0; x < count; x++)
            {
                readElement(ref reader, elements);
            }
            reader.Close();
            return elements;
        }
        public static string toStringFormat(LadderElements elements)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writter = new BinaryWriter(stream);
            writter.Write(elements.Count);
            for (int x = 0; x < elements.Count; x++)
            {
                writeElement(ref writter, elements[x]);
            }
            writter.Close();
            return Convert.ToBase64String(stream.ToArray());
        }
        public bool IsInside(int x, int y)
        {
            if (x >= getX() && x <= getX() + m_Position.Width && y >= getY() && y <= getY() + m_Position.Height)
            {
                return true;
            }
            return false;
        }
        public void AddMouseEvent(MouseEvent mouseEvent)
        {
            Array.Resize<MouseEvent>(ref mouseEvents, mouseEvents.Length + 1);
            mouseEvents[mouseEvents.Length - 1] = mouseEvent;
        }
        public void CreateDivision(int x, int y)
        {
            Type = LadderDrawingTypes.Division;
            this.Position.X = x;
            this.Position.Y = y;
        }
        public void CreateCustom(object drawing, int x, int y, int width, int height)
        {
            m_Customtype = drawing.GetType().FullName;
            customDrawing = (ICustomDrawing)drawing;
            this.Type = LadderDrawingTypes.CustomDrawing;
            this.Position.X = x;
            this.Position.Y = y;
            this.Position.Width = width;
            this.Position.Height = height;
        }
        public void CreateTable(int rows, int columns, int columnwidth, int rowheight)
        {
            this.Type = LadderDrawingTypes.TableLayout;
            for (int x = 0; x < rows; x++)
            {
                LadderElement newRow = new LadderElement();
                newRow.Type = LadderDrawingTypes.TableLayoutRow;
                newRow.Position.RowIndex = x;
                newRow.Position.Width = columnwidth * columns;
                newRow.Position.Height = rowheight;
                newRow.Position.Y = rowheight * x;
                for (int y = 0; y < columns; y++)
                {
                    LadderElement newCell = new LadderElement();
                    newCell.Position.RowIndex = x;
                    newCell.Position.ColumnIndex = y;
                    newCell.Position.X = columnwidth * y;
                    newCell.Position.Width = columnwidth;
                    newCell.Position.Height = rowheight;
                    newRow.Elements.Add(newCell);
                }
                Elements.Add(newRow);
            }
            this.Position.Width = columnwidth * columns;
            this.Position.Height = rowheight * rows;
        }
        public void CreateTable(int rows, int columns, int[] columnwidth, int[] rowheight)
        {
            this.Type = LadderDrawingTypes.TableLayout;
            int xAxis = 0;
            int yAxis = 0;
            for (int x = 0; x < rows; x++)
            {
                LadderElement newRow = new LadderElement();
                newRow.Type = LadderDrawingTypes.TableLayoutRow;
                newRow.Position.RowIndex = x;
                newRow.Position.Y = yAxis;
                Elements.Add(newRow);
                xAxis = 0;
                for (int y = 0; y < columns; y++)
                {
                    LadderElement newCell = new LadderElement();
                    newCell.Type = LadderDrawingTypes.TableLayoutCell;
                    newCell.Position.RowIndex = x;
                    newCell.Position.ColumnIndex = y;
                    newCell.Position.X = xAxis;
                    newCell.Position.Width = columnwidth[y];
                    newCell.Position.Height = rowheight[x];
                    xAxis += columnwidth[y];
                    newRow.Elements.Add(newCell);
                }
                newRow.Position.Width = xAxis;
                newRow.Position.Height = rowheight[x];
                yAxis += rowheight[x];
            }
            Position.Width = xAxis;
            Position.Height = yAxis;
        }
        public void Render()
        {
            Graphics graphics;
            Point point1 = new Point();
            Point point2 = new Point();
            graphics = LadderDesign.Window;
            if (LinePosition == LinePosition.CenterMid)
            {
                point1 = new Point((LineWidth / 2), (m_Position.Height / 2) - (LineWidth / 2));
                point2 = new Point(m_Position.Width - (LineWidth / 2), ((m_Position.Height / 2) - (LineWidth / 2)));
            }
            if (m_Type == LadderDrawingTypes.Connector)
            {
                if (LinePosition == LinePosition.CenterMid)
                {
                    graphics.DrawLine(new Pen(LineColor), point1, point2);
                }
            }
            if (m_Type == LadderDrawingTypes.CustomDrawing)
            {
                if (customDrawing != null)
                {
                    Elements.Parent = this;
                    Elements.Render();
                    customDrawing.Draw(graphics, this);
                    int xx = this.getX();
                    int yy = this.getY();
                }
            }
            else if (m_Type == LadderDrawingTypes.ConnectorInput)
            {
                if (LinePosition == LinePosition.CenterMid)
                {
                    graphics.DrawLine(new Pen(LineColor), point1, point2);
                }
            }
            else if (m_Type == LadderDrawingTypes.TableLayoutCell)
            {
                Elements.Parent = this;
                Elements.Render();
            }
            else if (m_Type == LadderDrawingTypes.TableLayoutRow)
            {
                Elements.Parent = this;
                Elements.Render();
            }
            else if (m_Type == LadderDrawingTypes.TableLayout)
            {
                Elements.Parent = this;
                Elements.Render();
            }
            else if (m_Type == LadderDrawingTypes.Division)
            {
                Elements.Parent = this;
                Elements.Render();
            }
        }

        public LadderElement Clone()
        {
            var clonedObject = this.MemberwiseClone() as LadderElement; //create a shallow-copy of the object
            clonedObject.Position = clonedObject.Position.Clone();
            clonedObject.Position.Parent = clonedObject.Position.Parent?.MemberwiseClone() as LadderElement;
            clonedObject.Elements = clonedObject.Elements.Clone(clonedObject);

            return clonedObject;
        }
    }
    public class LadderElements : List<LadderElement>, System.ICloneable
    {
        LadderElement m_Parent = null;
        public LadderElement Parent { set { m_Parent = value; } get { return m_Parent; } }
        public LadderElements() { }
        public LadderElements(List<LadderElement> toCopy)
        : base(toCopy)
        { }

        public new LadderElement Add(LadderElement ladderElement)
        {
            if (ladderElement == null)
                return null;
            ladderElement.Position.Parent = this.Parent;
            ladderElement.Position.Index = base.Count;
            base.Add(ladderElement);
            return ladderElement;
        }
        public new LadderElement Insert(int index, LadderElement ladderElement)
        {
            ladderElement.Position.Parent = this.Parent;
            ladderElement.Position.Index = index;
            base.Insert(index, ladderElement);
            return ladderElement;
        }
        public LadderElement MouseHover(LadderElement parent, int x, int y)
        {
            LadderElement foundlastElement = null;
            for (int i = 0; i < this.Count; i++)
            {
                if (parent != null)
                {
                }
                if (this[i].IsInside(Convert.ToInt32(x), Convert.ToInt32(y)))
                {
                    foundlastElement = this[i];
                }
                LadderElement foundChild = this[i].Elements.MouseHover(this[i], x, y);
                if (foundChild != null)
                    foundlastElement = foundChild;
            }
            return foundlastElement;
        }
        public void setDimention()
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Elements.setDimention();
                int currentX = this[i].getX() + this[i].Position.Width;
                int currentY = this[i].getY() + this[i].Position.Height;
                int posWidth = LadderCanvas.Active.Width;
                int posHeight = LadderCanvas.Active.Height;
                if (currentX > posWidth)
                {
                    LadderCanvas.Active.Width = currentX;
                }
                if (currentY > posHeight)
                {
                    LadderCanvas.Active.Height = currentY;
                }
            }
        }
        public LadderElement FindById(string id)
        {
            for (int x = 0; x < this.Count; x++)
            {
                if (this[x].Id.Equals(id))
                    return this[x];
                LadderElement found = this[x].Elements.FindById(id);
                if (found != null)
                    return found;
            }
            return null;
        }
        public void Render()
        {
            int index = 0;
            for (int i = this.Count - 1; i >= 0; i--)
            {
                this[i].Position.Index = i;
                this[i].Position.Parent = Parent;
                this[i].Render();
                index++;
            }
            if (Parent.customDrawing.GetType() == typeof(Rung))
            {
                setDimention();
            }
        }

        public LadderElements Clone(LadderElement parent = null)
        {
            var clonedObject = this.MemberwiseClone() as LadderElements; //create a shallow-copy of the object
            for (int i = 0; i < clonedObject.Count; i++)
            {
                clonedObject[i] = clonedObject[i].Clone();
                clonedObject[i].Position.Parent = parent;
            }
            return clonedObject;
        }

        object System.ICloneable.Clone()
        {
            return this.Clone();
        }
        public void ChangeBackGroundColor(LadderCanvas value, int x, int y, bool shiftKeyPress, ref List<LadderElement> selectedElements, bool isOnSelect = false)
        {
            if(!shiftKeyPress)
                value.Refresh();
            // Find the ladder element with the highest Position.Y <= y
            LadderElement foundLastElement = this
                .Where(t => t.Position.Y <= y)
                .OrderByDescending(t => t.Position.Y)
                .FirstOrDefault();

            if (foundLastElement == null) return;

            //ICustomDrawing customDrawing = foundLastElement.customDrawing;
            //customDrawing?.ShowFullSelection(value.getGraphics(), foundLastElement);
            if(isOnSelect)
            {
                LadderElement foundFirstElement = foundLastElement.Elements.FirstOrDefault(t => t.CustomType.Equals("LadderDrawing.Contact") ||
                                t.CustomType.Equals("LadderDrawing.Coil") || t.CustomType.Equals("LadderDrawing.FunctionBlock"));
                if (!selectedElements.Contains(foundFirstElement) && IsRungSelected(foundLastElement))
                    selectedElements.Add(foundFirstElement);
            }
            HighlightRungElements(value, foundLastElement, isOnSelect);
        }

        private bool IsRungSelected(LadderElement foundFirstElement)
        {
            if(foundFirstElement != null)
            {
                if (foundFirstElement.Elements.Any(t => t.CustomType.Equals("LadderDrawing.FunctionBlock")))
                    return true;
                else if (foundFirstElement.Elements.Any(t => t.CustomType.Equals("LadderDrawing.Coil")) &&
                        foundFirstElement.Elements.Any(t => t.CustomType.Equals("LadderDrawing.Contact")))
                    return true;
                else if (foundFirstElement.Elements.Any(t => t.CustomType.Equals("LadderDrawing.Coil")))
                    return true;
            }
            return false;
        }

        public void HighlightRungElements(LadderCanvas value, LadderElement foundLastElement, bool isOnSelect)
        {
            // Process all child elements
            foreach (LadderElement ladderElement in foundLastElement.Elements)
            {
                if (ladderElement.CustomType.Equals("LadderDrawing.DummyParallelParent") || ladderElement.CustomType.Equals("LadderDrawing.Coil"))
                {
                    if(ladderElement.CustomType.Equals("LadderDrawing.Coil"))
                    {
                        HighlightFullRung(value.getGraphics(), ladderElement, isOnSelect);
                    }
                    foreach (LadderElement parallelElement in ladderElement.Elements)
                    {
                        HighlightFullRung(value.getGraphics(), parallelElement, isOnSelect);
                        if (parallelElement.Elements.Count > 0)
                        {
                            CheckInChildElement(value, parallelElement, isOnSelect);
                        }                    
                    }
                }
                else
                {
                    HighlightFullRung(value.getGraphics(), ladderElement, isOnSelect);
                }
            }
        }

        private void CheckInChildElement(LadderCanvas value, LadderElement parallelElement, bool isOnSelect)
        {
            // Process deeper levels in a single loop
            foreach (LadderElement element in parallelElement.Elements)
            {
                HighlightFullRung(value.getGraphics(), element, isOnSelect);

                if(element.Elements.Any())
                {
                    CheckInChildElement(value, element, isOnSelect);
                }
            }
        }

        private void HighlightFullRung(Graphics graphics, LadderElement ladderElement, bool isOnSelect)
        {
            if(!isOnSelect)
                ladderElement.customDrawing?.ShowFullSelection(graphics, ladderElement);
            else
                ladderElement.customDrawing?.OnSelect(graphics, ladderElement);
        }

    }
}
