using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XMPS2000.Core;

namespace XMPS2000
{
    public partial class CompileErrors : Form
    {
        XMPS xm;
        private string currentLogicBlock = "";
        public CompileErrors()
        {
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                ctrl.Dispose();
            }
            this.Controls.Clear();


            xm = XMPS.Instance;
            InitializeComponent();
        }


        public void ShowsErrorInListView(string[] errors,string errortype)
        {
            // OPTIMIZATION 1: Move logicBlocks creation outside the loop
            // This reduces time complexity from O(n²) to O(n) where n is number of errors
            // Previously, this LINQ query executed once per error message
            List<string> logicBlocks = xm.LoadedProject.Blocks
                .Where(T => T.Type.Equals("LogicBlock") || T.Type.Equals("InterruptLogicBlock"))
                .Select(T => T.Name)
                .ToList();

            // OPTIMIZATION 2: Convert to HashSet for O(1) lookup instead of O(m) where m is logicBlocks count
            // Contains() on List<string> is O(m), on HashSet<string> it's O(1)
            HashSet<string> logicBlocksSet = new HashSet<string>(logicBlocks);

            int yPos = 10;
            int margin = 0;

            int currentLogicBlockIdx = -1;
            List<int> logicBlockIndices = new List<int>(); // Store indices of logic block errors

            // 1. Prepare your data and preprocess keys
            List<string> errorKeys = errors.Select(err => err.Split('\r')[0]).ToList();

            for (int i = 0; i < errorKeys.Count; i++)
            {
                if (logicBlocksSet.Contains(errorKeys[i]))
                    logicBlockIndices.Add(i);
            }
            System.Windows.Forms.ListView listViewErrors = new System.Windows.Forms.ListView(); 
            mainErrorPanel.Controls.Add(listViewErrors);
            // 2. Setup ListView (in Form Load or initialization)
            listViewErrors.View = View.Details;
            listViewErrors.VirtualMode = true;
            listViewErrors.FullRowSelect = true;
            listViewErrors.Columns.Add(errortype, 1200); // Set width as needed
            listViewErrors.RetrieveVirtualItem += (sender, e) =>
            {
                // Get error at index e.ItemIndex
                string errMsg = errors[e.ItemIndex];
                string errKey = errorKeys[e.ItemIndex];
                bool isLogicBlockError = logicBlocksSet.Contains(errKey);

                // Calculate logic block context
                string logicBlockName = "";
                for (int idx = logicBlockIndices.Count - 1; idx >= 0; idx--)
                {
                    if (logicBlockIndices[idx] <= e.ItemIndex)
                    {
                        logicBlockName = errors[logicBlockIndices[idx]];
                        break;
                    }
                }
                // Optionally pad non-logic block errors
                string displayedText = isLogicBlockError ? errMsg : new string(' ', 8) + errMsg;
                var item = new System.Windows.Forms.ListViewItem(displayedText);
                item.ForeColor = Color.Black;
                item.Tag = logicBlockName; // Store logic block for this row, if desired
                e.Item = item;
            };
            listViewErrors.VirtualListSize = errors.Count();

            // 3. Add click handler for interaction
            listViewErrors.ItemSelectionChanged += listViewErrors_ItemActivate;

            // 4. Add listViewErrors to your main panel/control as needed
            mainErrorPanel.Controls.Add(listViewErrors);
            listViewErrors.Dock = DockStyle.Fill;
        }

        private void listViewErrors_ItemActivate(object sender, EventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null || listView.SelectedIndices.Count == 0)
                return;

            int selectedIdx = listView.SelectedIndices[0];

            // Now you can use 'listView' here instead of 'listViewErrors'
            string errorMessage = listView.FocusedItem.Text.Trim();

            string logicBlockName = "";
            if (listView.Items.Count > selectedIdx && listView.Items[selectedIdx].Tag is string tag)
            {
                logicBlockName = tag.Split('\r')[0];
            }
            else
            {
                logicBlockName = ""; // fallback if needed
            }
            int colonIndex = errorMessage.IndexOf(':');
            if (colonIndex != -1)
            {
                string rung = errorMessage.Substring(0, colonIndex);
                int forIndex = errorMessage.IndexOf("for", colonIndex, StringComparison.OrdinalIgnoreCase);
                if (forIndex != -1 || (Regex.IsMatch(rung, @"^\d+$") && !string.IsNullOrEmpty(rung)))
                {
                    string address = (forIndex != -1) ? errorMessage.Substring(forIndex + 4).Trim() : "";
                    // Call the same method as before, adapted for ListView
                    ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain"))
                        ?.ShowErroredLogicBlock(logicBlockName, rung, address);
                }
                else
                {
                    int rowNumber = 0;
                    var match = Regex.Match(errorMessage, @"row no-(\d+)");
                    if (match.Success)
                    {
                        rowNumber = int.Parse(match.Groups[1].Value);
                    }
                    rung = rung.Trim();
                    switch (rung)
                    {
                        case "MODBUS RTU Master":
                        case "MODBUS RTU Slaves":
                        case "MODBUS TCP Client":
                        case "MODBUS TCP Server":
                        case "MQTT Subscribe":
                        case "MQTT Publish":
                        case "Schedule":
                        case "Binary Value":
                        case "Multistate Value":
                        case "Analog Value":
                        case "TagsForm":
                        case "Main":
                            ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain"))
                                ?.ShowErroredFrmMainGrid(rung, rowNumber);
                            break;
                        case null:
                            return;
                    }
                    if (rung.StartsWith("TagsForm@"))
                    {
                        ((frmMain)Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.GetType().Name == "frmMain"))
                            ?.ShowErroredFrmMainGrid(rung, rowNumber);
                    }
                }
            }
        }
    }
    
}
