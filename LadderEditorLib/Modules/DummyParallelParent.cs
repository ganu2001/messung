using System.Drawing;

namespace LadderDrawing
{
    internal class DummyParallelParent : CustomDrawing, ICustomDrawing
    {
        public LadderElement Create(int x, int y)
        {
            LadderElement element = new LadderElement();
            element.Position.X = x;
            element.Position.Y = y;

            return element;
        }

        public void Paint(Pen newPen, Graphics graphics, LadderElement element)
        {            
        }

        public void Draw(Graphics graphics, LadderElement element)
        {            
        }

        public void OnSelect(Graphics graphics, LadderElement element)
        {            
        }

        public string toString()
        {
            return "DummyParallelParent";
        }

        public void ShowFullSelection(Graphics graphics, LadderElement element)
        {
        }
    }

}

