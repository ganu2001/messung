using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LadderDrawing
{
    //Basic Postioning Class & enums
    public enum LinePosition
    {
        CenterMid = 0,
        TopMid,
        BottomMid
    }

    public enum LineDirection
    {
        LeftToRight = 0,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }

    public enum LadderDrawingTypes
    {
        Connector = 0,
        ConnectorAnd,
        ConnectorOr,
        ConnectorNot,
        ConnectorInput,
        ConnectorFunction,
        TableLayout,
        TableLayoutRow,
        TableLayoutCell,
        CustomDrawing,
        Division
    }

    public enum Visiblity
    {
        Visible = 0,
        Hidden,
        Layer
    }
    //End of Enum declaration

    //Design Classes
    public class Global
    {
        static LadderElement Active = null;

        public static void SelectActive(LadderElement element, Graphics graphics)
        {
            if (Active != null && Active.customDrawing != null)
            {
                Active.customDrawing.Draw(graphics, Active);
                Active = null;
            }
            Active = element;
            if (Active != null && Active.customDrawing != null)
            {
                Active.customDrawing.OnSelect(graphics, Active);
            }
        }

        public static void SelectCurrent(Graphics graphics)
        {
            if (Active != null && Active.customDrawing != null)
            {
                Active.customDrawing.OnSelect(graphics, Active);
            }
        }

        public static void ClearActive()
        {
            Active = null;
        }
    }

    public class CustomDrawing
    {
        public LadderElement Element { set; get; }
    }

    public interface ICustomDrawing
    {
        LadderElement Create(int x,int y);
        void Draw(Graphics graphics, LadderElement element);
        void OnSelect(Graphics graphics, LadderElement element);
        void ShowFullSelection(Graphics graphics, LadderElement element);
        string toString();
    }

    public interface MouseEvent
    {
        void Moving(LadderElement element, int x, int y);
        void Click(LadderElement element, int x, int y, int key);
        void Down(LadderElement element, int x, int y, int key);
        void Up(LadderElement element, int x, int y, int key);
    }
}
