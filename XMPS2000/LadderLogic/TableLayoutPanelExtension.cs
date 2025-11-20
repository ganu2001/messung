using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000.LadderLogic
{
    internal static class TableLayoutPanelExtension
    {
        public static void RemoveRowControlsAndHideRow(this TableLayoutPanel panel, int rowIndex)
        {
            // delete all controls of row that we want to delete
            for (int i = 0; i < panel.ColumnCount; i++)
            {
                var control = panel.GetControlFromPosition(i, rowIndex);
                panel.Controls.Remove(control);
            }
            // Then set it's height to 0
            panel.RowStyles[rowIndex].Height = 0;
        }

        // Approach 1:
        // ref: https://stackoverflow.com/questions/15535214/removing-a-specific-row-in-tablelayoutpanel
        public static void RemoveArbitraryRow(this TableLayoutPanel panel, int rowIndex)
        {
            if (rowIndex >= panel.RowCount)
                return;
            
            panel.SuspendLayout();

            // delete all controls of row that we want to delete
            for (int i = 0; i < panel.ColumnCount; i++)
            {
                var control = panel.GetControlFromPosition(i, rowIndex);
                panel.Controls.Remove(control);
            }

            // move up row controls that comes after row we want to remove
            for (int i = rowIndex + 1; i < panel.RowCount; i++)
            {
                for (int j = 0; j < panel.ColumnCount; j++)
                {
                    var control = panel.GetControlFromPosition(j, i);
                    if (control != null)
                    {
                        panel.SetRow(control, i - 1);
                    }
                }
            }

            var removeStyle = panel.RowCount - 1;

            if (panel.RowStyles.Count > removeStyle)
                panel.RowStyles.RemoveAt(removeStyle);

            panel.RowCount--;

            panel.ResumeLayout(false);
            panel.PerformLayout();
        }
    }
}
