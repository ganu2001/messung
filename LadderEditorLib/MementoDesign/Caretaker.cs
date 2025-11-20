using LadderDrawing;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LadderEditorLib.MementoDesign
{
    public class Caretaker
    {
        private Stack<LadderDesignMemento> UndoStack = new Stack<LadderDesignMemento>();
        private Stack<LadderDesignMemento> RedoStack = new Stack<LadderDesignMemento>();
        public LadderDesignMemento getUndoMemento()
        {
            if (UndoStack.Count > 0 && UndoStack.Count !=1)
            {
                RedoStack.Push(UndoStack.Pop());
                return UndoStack.Count == 0 ? null : UndoStack.Peek().GetLadderDesignMemento();
            }
            else
            {
                MessageBox.Show("Stack is Empty Can't Perform Undo Operation in LadderDesign", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
        public LadderDesignMemento getRedoMemento()
        {
            if (RedoStack.Count != 0)
            {
                LadderDesignMemento m = RedoStack.Pop();
                UndoStack.Push(m);
                return m.GetLadderDesignMemento();
            }
            else
            {
                MessageBox.Show("Stack is Empty Can't Perform Redo Operation in LadderDesign", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
        public void InsertMementoForUndoRedo(LadderDesignMemento memento)
        {
            if(memento != null)
            {
                if(UndoStack.Count > 20)
                {
                    Stack<LadderDesignMemento> tempStack = new Stack<LadderDesignMemento>();
                    while (UndoStack.Count > 1)
                    {
                        tempStack.Push(UndoStack.Pop());
                    }
                    UndoStack.Pop();
                    while (tempStack.Count > 0)
                    {
                        UndoStack.Push(tempStack.Pop());
                    }
                }
                UndoStack.Push(memento);
                RedoStack.Clear();
            }
        }
        internal void SetStateForUndoRedo(LadderDesign ladderDesign)
        {
            LadderDesignMemento memento = new LadderDesignMemento(ladderDesign);
            InsertMementoForUndoRedo(memento);
        }
    }
}
