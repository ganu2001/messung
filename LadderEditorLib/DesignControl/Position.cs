using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadderDrawing
{
    public class Position 
    {
        public int _x = -1, _y = -1;

        int m_X = 0;
        public int X { set { m_X = value; } get { return m_X; } }

        int m_Y = 0;
        public int Y { set { m_Y = value; } get { return m_Y; } }

        int m_Width = 0;
        public int Width { set { m_Width = value; } get { return m_Width; } }

        int m_Height = 0;
        public int Height { set { m_Height = value; } get { return m_Height; } }

        int m_Index = -1;
        public int Index { set { m_Index = value; } get { return m_Index; } }

        int m_RowIndex = -1;
        public int RowIndex { set { m_RowIndex = value; } get { return m_RowIndex; } }

        int m_ColumnIndex = -1;
        public int ColumnIndex { set { m_ColumnIndex = value; } get { return m_ColumnIndex; } }

        Visiblity m_Visiblity = Visiblity.Visible;
        public Visiblity Visiblity { set { m_Visiblity = value; } get { return m_Visiblity; } }

        LadderElement m_Parent;
        public LadderElement Parent { set { m_Parent = value; } get { return m_Parent; } }

        /*
        LadderElement m_ConnectedTo;
        public LadderElement ConnectedTo { set { m_ConnectedTo = value; } get { return m_ConnectedTo; } }

        LadderElement m_ConnectedFrom;
        public LadderElement ConnectedFrom { set { m_ConnectedFrom = value; } get { return m_ConnectedFrom; } }
        */
        LadderElements m_RelateTo = new LadderElements();

        LadderElements m_ConnectTo = new LadderElements();
        public LadderElements RelateTo { set { m_RelateTo = value; } get { return m_RelateTo; } }

        public LadderElements ConnectTo { set { m_ConnectTo = value; } get { return m_ConnectTo; } }

        /*
        public void ConnectTo(LadderElement element)
        {
            m_ConnectedTo = element;
            element.Position.ConnectedFrom = this.Parent;
        }*/

        public void RelatedTo(LadderElement element)
        {
            m_RelateTo.Add(element);
        }

        public void ConnectedTo(LadderElement element)
        {
            m_ConnectTo.Add(element);
        }

        public void SetVisible()
        {
            m_Visiblity = Visiblity.Visible;
        }

        public void Hide()
        {
            m_Visiblity = Visiblity.Hidden;
        }

        public Position Clone()
        {
            var clonedObject = this.MemberwiseClone() as Position; //create a shallow-copy of the object
            clonedObject.RelateTo = clonedObject.RelateTo.Clone();
            clonedObject.ConnectTo = clonedObject.ConnectTo.Clone();
            return clonedObject;
        }
    }
}
