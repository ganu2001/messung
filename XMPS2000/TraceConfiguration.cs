using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using XMPS2000.Core;
using XMPS2000.Core.Base.Helpers;
using XMPS2000.Properties;
using static XMPS2000.TraceConfiguration;

namespace XMPS2000
{
    public class TraceConfiguration : Form
    {
        XMPS xm;
        private Button btnAdd;
        private LinkLabel llblAddVariable;
        private LinkLabel lblDeleteVariable;
        private TreeView treeView1;
        private GroupBox groupBox1;
        private Label lblName;
        private Label Axixname;
        private TextBox txtName;
        private TextBox maximumTextBox;
        private TextBox minimumTextBox;
        private Label lblMaximum;
        private Label lblMinimum;
        private CheckBox autoCheckBox;
        private Label lblScalingFactor;
        private TextBox txtOffset;
        private Label lblMultplier;
        private Label lblOffset;
        private TextBox txtMultplier;
        private Button btnClose;
        private int groupBoxIndex = 1;
        private string ActiveGroupBoxName;
        private ComboBox cmbSelectVariable;
        private ComboBox cmbGrpColor;
        private ComboBox cmbGraphType;
        private NumericUpDown numericUpDown;
        private Button btnAddVariable;
        private ErrorProvider errorProvider = new ErrorProvider();
        public List<VariableSettings> variableSettingsList;
        private Button btnSet;
        public Dictionary<string, NodeSettings> nodeSettings;
        public TraceConfiguration(ref List<VariableSettings> remvariableSettingsList, ref Dictionary<string, NodeSettings> remnodeSettings)
        {
            InitializeComponent();
            xm = XMPS.Instance;
            TreeNode rootNode = new TreeNode("Diagram_1");
            TreeNode timeAxisNode = new TreeNode("Time axis (x)");
            TreeNode yAxisNode = new TreeNode("Y axis");
            variableSettingsList = remvariableSettingsList;
            nodeSettings = remnodeSettings;
            // Add child nodes to the root node
            rootNode.Nodes.Add(timeAxisNode);
            rootNode.Nodes.Add(yAxisNode);
            treeView1.Nodes.Add(rootNode);
            if (variableSettingsList.Count() > 0)
            {
                foreach (var setting in variableSettingsList)
                {
                    foreach (TreeNode rootNode1 in this.treeView1.Nodes)
                    {
                        foreach (TreeNode childNode in rootNode.Nodes)
                        {
                            if (childNode.Text == "Y axis")
                            {
                                TreeNode variableNode = new TreeNode(setting.VariableName);
                                childNode.Nodes.Add(variableNode);
                            }
                        }
                    }
                }
            }
            treeView1.ExpandAll();
            this.treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.llblAddVariable = new System.Windows.Forms.LinkLabel();
            this.lblDeleteVariable = new System.Windows.Forms.LinkLabel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSet = new System.Windows.Forms.Button();
            this.txtMultplier = new System.Windows.Forms.TextBox();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.lblMultplier = new System.Windows.Forms.Label();
            this.lblOffset = new System.Windows.Forms.Label();
            this.lblScalingFactor = new System.Windows.Forms.Label();
            this.maximumTextBox = new System.Windows.Forms.TextBox();
            this.minimumTextBox = new System.Windows.Forms.TextBox();
            this.lblMaximum = new System.Windows.Forms.Label();
            this.lblMinimum = new System.Windows.Forms.Label();
            this.autoCheckBox = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.Axixname = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(387, 425);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(104, 34);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(507, 425);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(98, 34);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // llblAddVariable
            // 
            this.llblAddVariable.AutoSize = true;
            this.llblAddVariable.Location = new System.Drawing.Point(55, 409);
            this.llblAddVariable.Name = "llblAddVariable";
            this.llblAddVariable.Size = new System.Drawing.Size(67, 13);
            this.llblAddVariable.TabIndex = 2;
            this.llblAddVariable.TabStop = true;
            this.llblAddVariable.Text = "Add Variable";
            this.llblAddVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblAddVariable_LinkClicked);
            // 
            // lblDeleteVariable
            // 
            this.lblDeleteVariable.AutoSize = true;
            this.lblDeleteVariable.Location = new System.Drawing.Point(55, 439);
            this.lblDeleteVariable.Name = "lblDeleteVariable";
            this.lblDeleteVariable.Size = new System.Drawing.Size(79, 13);
            this.lblDeleteVariable.TabIndex = 3;
            this.lblDeleteVariable.TabStop = true;
            this.lblDeleteVariable.Text = "Delete Variable";
            this.lblDeleteVariable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblDeleteVariable_LinkClicked);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(59, 47);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(214, 346);
            this.treeView1.TabIndex = 4;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSet);
            this.groupBox1.Controls.Add(this.txtMultplier);
            this.groupBox1.Controls.Add(this.txtOffset);
            this.groupBox1.Controls.Add(this.lblMultplier);
            this.groupBox1.Controls.Add(this.lblOffset);
            this.groupBox1.Controls.Add(this.lblScalingFactor);
            this.groupBox1.Controls.Add(this.maximumTextBox);
            this.groupBox1.Controls.Add(this.minimumTextBox);
            this.groupBox1.Controls.Add(this.lblMaximum);
            this.groupBox1.Controls.Add(this.lblMinimum);
            this.groupBox1.Controls.Add(this.autoCheckBox);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.Axixname);
            this.groupBox1.Location = new System.Drawing.Point(294, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 358);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(235, 329);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(47, 23);
            this.btnSet.TabIndex = 13;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // txtMultplier
            // 
            this.txtMultplier.Location = new System.Drawing.Point(122, 291);
            this.txtMultplier.Name = "txtMultplier";
            this.txtMultplier.Size = new System.Drawing.Size(160, 20);
            this.txtMultplier.TabIndex = 12;
            this.txtMultplier.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(122, 257);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(160, 20);
            this.txtOffset.TabIndex = 11;
            this.txtOffset.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // lblMultplier
            // 
            this.lblMultplier.AutoSize = true;
            this.lblMultplier.Location = new System.Drawing.Point(42, 294);
            this.lblMultplier.Name = "lblMultplier";
            this.lblMultplier.Size = new System.Drawing.Size(46, 13);
            this.lblMultplier.TabIndex = 10;
            this.lblMultplier.Text = "Multplier";
            this.lblMultplier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOffset
            // 
            this.lblOffset.AutoSize = true;
            this.lblOffset.Location = new System.Drawing.Point(42, 260);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(35, 13);
            this.lblOffset.TabIndex = 9;
            this.lblOffset.Text = "Offset";
            // 
            // lblScalingFactor
            // 
            this.lblScalingFactor.AutoSize = true;
            this.lblScalingFactor.Location = new System.Drawing.Point(23, 235);
            this.lblScalingFactor.Name = "lblScalingFactor";
            this.lblScalingFactor.Size = new System.Drawing.Size(80, 13);
            this.lblScalingFactor.TabIndex = 8;
            this.lblScalingFactor.Text = "Scaling Factors";
            // 
            // maximumTextBox
            // 
            this.maximumTextBox.Location = new System.Drawing.Point(122, 206);
            this.maximumTextBox.Name = "maximumTextBox";
            this.maximumTextBox.Size = new System.Drawing.Size(160, 20);
            this.maximumTextBox.TabIndex = 7;
            this.maximumTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // minimumTextBox
            // 
            this.minimumTextBox.Location = new System.Drawing.Point(122, 172);
            this.minimumTextBox.Name = "minimumTextBox";
            this.minimumTextBox.Size = new System.Drawing.Size(160, 20);
            this.minimumTextBox.TabIndex = 6;
            this.minimumTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // lblMaximum
            // 
            this.lblMaximum.AutoSize = true;
            this.lblMaximum.Location = new System.Drawing.Point(40, 206);
            this.lblMaximum.Name = "lblMaximum";
            this.lblMaximum.Size = new System.Drawing.Size(51, 13);
            this.lblMaximum.TabIndex = 5;
            this.lblMaximum.Text = "Maximum";
            // 
            // lblMinimum
            // 
            this.lblMinimum.AutoSize = true;
            this.lblMinimum.Location = new System.Drawing.Point(40, 178);
            this.lblMinimum.Name = "lblMinimum";
            this.lblMinimum.Size = new System.Drawing.Size(48, 13);
            this.lblMinimum.TabIndex = 4;
            this.lblMinimum.Text = "Minimum";
            // 
            // autoCheckBox
            // 
            this.autoCheckBox.AutoSize = true;
            this.autoCheckBox.Location = new System.Drawing.Point(24, 138);
            this.autoCheckBox.Name = "autoCheckBox";
            this.autoCheckBox.Size = new System.Drawing.Size(48, 17);
            this.autoCheckBox.TabIndex = 3;
            this.autoCheckBox.Text = "Auto";
            this.autoCheckBox.UseVisualStyleBackColor = true;
            this.autoCheckBox.CheckedChanged += new System.EventHandler(this.autoCheckBox_CheckedChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(24, 93);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(243, 20);
            this.txtName.TabIndex = 2;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(23, 70);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // Axixname
            // 
            this.Axixname.AutoSize = true;
            this.Axixname.Location = new System.Drawing.Point(23, 34);
            this.Axixname.Name = "Axixname";
            this.Axixname.Size = new System.Drawing.Size(35, 13);
            this.Axixname.TabIndex = 0;
            this.Axixname.Text = "label1";
            // 
            // TraceConfiguration
            // 
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(641, 479);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.lblDeleteVariable);
            this.Controls.Add(this.llblAddVariable);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAdd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TraceConfiguration";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Trace Configuration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GroupBox existingGroupBox = this.Controls.OfType<GroupBox>()
                                 .FirstOrDefault(g => g.Name == "grpBoxVariable");
            if (e.Node.Text == "Time axis (x)" || e.Node.Text == "Y axis")
            {
                ResetYAxisChildNodesForeColor();
                if (existingGroupBox != null)
                {
                    this.Controls.Remove(existingGroupBox);
                }
                groupBox1.Visible = true;
                groupBox1.Text = "Scales";
                ActiveGroupBoxName = e.Node.Text;
                Axixname.Text = e.Node.Text;
                LoadSettingsForNode(e.Node.Text); // Load settings for the clicked node
            }
            else if (e.Node.Parent != null && e.Node.Parent.Text == "Y axis")
            {
                ResetYAxisChildNodesForeColor();
                SetNodeForeColor(e.Node, Color.Red);
                if (ActiveGroupBoxName == "grpBoxVariable")
                {
                    displayVariabledata(e.Node.Text);
                }
                else
                {
                    ActiveGroupBoxName = "grpBoxVariable";
                    addGroupBox();
                    displayVariabledata(e.Node.Text);
                }
                GroupBox newlyAddedGroupBox = this.Controls.OfType<GroupBox>()
                                 .FirstOrDefault(g => g.Name == "grpBoxVariable");
                if (existingGroupBox != null)
                    existingGroupBox.Enabled = false;
                else if (newlyAddedGroupBox != null)
                    newlyAddedGroupBox.Enabled = false;
                e.Node.ForeColor = Color.Red;
            }
        }

        private void ResetYAxisChildNodesForeColor()
        {
            foreach (TreeNode topNode in treeView1.Nodes)
            {
                if (topNode.LastNode.Text == "Y axis")
                {
                    TreeNode childNodeOfY = topNode.LastNode;
                    foreach (TreeNode childNode in childNodeOfY.Nodes)
                    {
                        SetNodeForeColor(childNode, Color.Black);
                    }
                }
            }
        }
        private void SetNodeForeColor(TreeNode node, Color color)
        {
            node.ForeColor = color;
        }
        private void displayVariabledata(string nodeName)
        {
            var settings = variableSettingsList.FirstOrDefault(t => t.VariableName.Equals(nodeName));
            if (settings != null)
            {
                this.cmbSelectVariable.SelectedItem = null;
                this.cmbGrpColor.SelectedItem = null;
                this.cmbGrpColor.SelectedItem = null;
                this.numericUpDown.Minimum = 1;
                this.cmbSelectVariable.SelectedItem = settings.VariableName;
                this.cmbGrpColor.SelectedItem = settings.GroupColor;
                this.cmbGraphType.SelectedItem = settings.GraphType;
                this.numericUpDown.Value = settings.SeriesWidth;

            }
        }
        private void llblAddVariable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetYAxisChildNodesForeColor();
            GroupBox existingGroupBox = this.Controls.OfType<GroupBox>()
                                  .FirstOrDefault(g => g.Name == "grpBoxVariable");
            if (existingGroupBox != null)
            { 
                existingGroupBox.Visible = true;
                existingGroupBox.Enabled = true;
                cmbGrpColor.SelectedIndex = -1;
                cmbGraphType.SelectedIndex = -1;
                numericUpDown.Value = 1;
                cmbSelectVariable.SelectedIndex = -1;
                ActiveGroupBoxName = existingGroupBox.Name;
                foreach (GroupBox groupBox in this.Controls.OfType<GroupBox>())
                {
                    if (groupBox != existingGroupBox)
                    {
                        groupBox.Visible = false;
                    }
                }
            }
            else
            {
                addGroupBox();
            }
        }

        private void addGroupBox()
        {
            groupBox1.Visible = false;
            GroupBox newGroupBox = new GroupBox();
            newGroupBox.Location = new System.Drawing.Point(294, 35);
            newGroupBox.Name = "grpBoxVariable";
            newGroupBox.Size = new System.Drawing.Size(311, 358);
            newGroupBox.TabIndex = groupBoxIndex;
            newGroupBox.TabStop = false;
            newGroupBox.Text = "Variable Settings";

            Label lblVariable = new Label();
            lblVariable.AutoSize = true;
            lblVariable.Location = new System.Drawing.Point(23, 70);
            lblVariable.Name = "lblVariable";
            lblVariable.Size = new System.Drawing.Size(51, 20);
            lblVariable.TabIndex = 1;
            lblVariable.Text = "Variable";

            cmbSelectVariable = new ComboBox();
            cmbSelectVariable.Location = new System.Drawing.Point(90, 70);
            cmbSelectVariable.Name = "cmbSelectVariable";
            cmbSelectVariable.Size = new System.Drawing.Size(150, 20);
            cmbSelectVariable.DataSource = (xm.LoadedProject.Tags.Select(T => T.Tag).ToList());
            cmbSelectVariable.SelectedIndex = -1;

            Label lblGrpColor = new Label();
            lblGrpColor.AutoSize = true;
            lblGrpColor.Location = new System.Drawing.Point(23, 100);
            lblGrpColor.Name = "lblGrpColor";
            lblGrpColor.Size = new System.Drawing.Size(71, 20);
            lblGrpColor.TabIndex = 1;
            lblGrpColor.Text = "Group Color";

            cmbGrpColor = new ComboBox();
            cmbGrpColor.Location = new System.Drawing.Point(90, 100);
            cmbGrpColor.Name = "cmbGrpColor";
            cmbGrpColor.Size = new System.Drawing.Size(150, 20);
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                cmbGrpColor.Items.Add(color);
            }
            cmbGrpColor.SelectedIndex = -1;

            Label lblGraphType = new Label();
            lblGraphType.AutoSize = true;
            lblGraphType.Location = new System.Drawing.Point(23, 130);
            lblGraphType.Name = "lblGraphtype";
            lblGraphType.Size = new System.Drawing.Size(71, 20);
            lblGraphType.TabIndex = 1;
            lblGraphType.Text = "Graph Type";

            cmbGraphType = new ComboBox();
            cmbGraphType.Location = new System.Drawing.Point(90, 130);
            cmbGraphType.Name = "cmbGraphType";
            cmbGraphType.Size = new System.Drawing.Size(150, 20);
            cmbGraphType.Items.AddRange(new object[] { "Line", "Bar", "StepLine" });
            cmbGraphType.SelectedIndex = -1;

            Label lblSeriesWidth = new Label();
            lblSeriesWidth.AutoSize = true;
            lblSeriesWidth.Location = new System.Drawing.Point(23, 160);
            lblSeriesWidth.Name = "SeriesWidth";
            lblSeriesWidth.Size = new System.Drawing.Size(71, 20);
            lblSeriesWidth.TabIndex = 1;
            lblSeriesWidth.Text = "Series Width";

            numericUpDown = new NumericUpDown();
            numericUpDown.Location = new System.Drawing.Point(90, 160);
            numericUpDown.Name = "numericUpDown";
            numericUpDown.Size = new System.Drawing.Size(150, 20);
            numericUpDown.Minimum = 1;
            numericUpDown.Controls[0].Visible = false;

            btnAddVariable = new Button();
            btnAddVariable.Location = new System.Drawing.Point(150, 300);
            btnAddVariable.Name = "btnAddVariable";
            btnAddVariable.Size = new System.Drawing.Size(90, 25);
            btnAddVariable.Text = "Add Variable";
            this.btnAddVariable.Click += new System.EventHandler(this.btnAddVariable_click);

            newGroupBox.Controls.Add(lblVariable);
            newGroupBox.Controls.Add(cmbSelectVariable);
            newGroupBox.Controls.Add(lblGrpColor);
            newGroupBox.Controls.Add(cmbGrpColor);
            newGroupBox.Controls.Add(lblGraphType);
            newGroupBox.Controls.Add(cmbGraphType);
            newGroupBox.Controls.Add(lblSeriesWidth);
            newGroupBox.Controls.Add(numericUpDown);
            newGroupBox.Controls.Add(btnAddVariable);
            newGroupBox.Visible = true;
            ActiveGroupBoxName = newGroupBox.Name;
            this.Controls.Add(newGroupBox);
        }

        private void btnAddVariable_click(object sender, EventArgs e)
        {
            if (ActiveGroupBoxName == "grpBoxVariable")
            {
                if (cmbSelectVariable.SelectedItem == null || cmbGrpColor.SelectedItem == null || cmbGraphType.SelectedItem == null)
                {
                    MessageBox.Show("Please select all required fields (Variable, Group Color, and Graph Type).", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                VariableSettings settings = new VariableSettings
                {
                    VariableName = this.cmbSelectVariable.SelectedItem.ToString(),
                    GroupColor = (KnownColor)Enum.Parse(typeof(KnownColor), cmbGrpColor.SelectedItem.ToString()),
                    GraphType = cmbGraphType.SelectedItem.ToString(),
                    SeriesWidth = (int)numericUpDown.Value
                };
                if (variableSettingsList.Count(t => t.VariableName.Equals(cmbSelectVariable.Text)) == 0)
                {
                    errorProvider.Clear();
                    variableSettingsList.Add(settings);
                }
                else
                {
                    errorProvider.SetError(cmbSelectVariable, "Variable name must be unique");
                    return;
                }
                cmbGrpColor.SelectedIndex = -1;
                cmbGraphType.SelectedIndex = -1;
                numericUpDown.Value = 1;
                cmbSelectVariable.SelectedIndex = -1;
                foreach (TreeNode rootNode in this.treeView1.Nodes)
                {
                    foreach (TreeNode childNode in rootNode.Nodes)
                    {
                        if (childNode.Text == "Y axis")
                        {
                            TreeNode variableNode = new TreeNode(settings.VariableName);
                            childNode.Nodes.Add(variableNode);
                            treeView1.ExpandAll(); 
                            return;
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(!nodeSettings.ContainsKey("Time axis (x)") || !nodeSettings.ContainsKey("Y axis"))
            {
                MessageBox.Show("First Add X and Y Axis data", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (variableSettingsList.Count ==0)
            {
                MessageBox.Show("First Add al least one variable", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                VariableSettings settings = variableSettingsList.FirstOrDefault(v => v.VariableName == e.Node.Text);
                if (settings != null)
                {
                    e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, e.Node.TreeView.Font, e.Bounds, e.Node.ForeColor);
                    Size textSize = TextRenderer.MeasureText(e.Node.Text, e.Node.TreeView.Font);
                    int boxX = e.Bounds.X + textSize.Width + 4;
                    int boxY = e.Bounds.Y + (e.Bounds.Height - 12) / 2;
                    Rectangle rect = new Rectangle(boxX, boxY, 12, 12);
                    using (Brush brush = new SolidBrush(Color.FromKnownColor(settings.GroupColor)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                        e.Graphics.DrawRectangle(Pens.Black, rect);
                    }
                    e.DrawDefault = false;
                }
                else
                {
                    e.DrawDefault = true;
                }
            }
            else
            {
                e.DrawDefault = true;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void lblDeleteVariable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Level == 2)
            {
                TreeNode yAxisNode = treeView1.SelectedNode.Parent;
                string deleytedNodeName = treeView1.SelectedNode.Text;
                if (yAxisNode != null && yAxisNode.Text == "Y axis")
                {
                    if (variableSettingsList.Count > 1)
                    {
                        var itemToRemove = variableSettingsList.FirstOrDefault(v => v.VariableName == deleytedNodeName);
                        if (itemToRemove != null)
                        {
                            variableSettingsList.Remove(itemToRemove);
                            yAxisNode.Nodes.Remove(treeView1.SelectedNode);
                            this.cmbSelectVariable.SelectedIndex = -1;
                            this.cmbGrpColor.SelectedIndex = -1;
                            this.numericUpDown.Minimum = 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("At least one variable setting must remain.", "XMPS2000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                treeView1.SelectedNode = null;
            }
        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            double offset = 0;
            double multiplier = 0;
            double minimum = 0;
            double maximum = 0;
            if(!autoCheckBox.Checked)
            {
                if (!double.TryParse(minimumTextBox.Text, out minimum))
                {
                    errorProvider.SetError(minimumTextBox, "Minimum cannot be null or zero.");
                    return;
                }
                else
                {
                    errorProvider.SetError(minimumTextBox, string.Empty);
                }
                if (!double.TryParse(maximumTextBox.Text, out maximum) || maximum == 0)
                {
                    errorProvider.SetError(maximumTextBox, "Maximum cannot be null or zero.");
                    return;
                }
                else
                {
                    errorProvider.SetError(maximumTextBox, string.Empty);
                }
            }
            // Check if offset and multiplier are valid
            if (!double.TryParse(txtOffset.Text, out offset) || offset == 0)
            {
                errorProvider.SetError(txtOffset, "Offset cannot be null or zero.");
                return;
            }
            else
            {
                errorProvider.SetError(txtOffset, string.Empty);
            }

            if (!double.TryParse(txtMultplier.Text, out multiplier) || multiplier == 0)
            {
                errorProvider.SetError(txtMultplier, "Multiplier cannot be null or zero.");
                return;
            }
            else
            {
                errorProvider.SetError(txtMultplier, string.Empty);
            }
            SaveCurrentSettings(offset, multiplier, minimum, maximum);
            txtName.Text = null;
            minimumTextBox.Text = null;
            maximumTextBox.Text = null;
            txtOffset.Text = null;
            txtMultplier.Text = null;
            autoCheckBox.Checked = false;
            minimumTextBox.Enabled = true;
            maximumTextBox.Enabled = true;
            return;
        }
        private void SaveCurrentSettings(double offset, double multiplier, double minimum, double maximum)
        {
            if (treeView1.SelectedNode != null)
            {
                var settings = new NodeSettings
                {
                    AxixName = Axixname.Text,
                    Name = txtName.Text,
                    Minimum = minimum,
                    Maximum = maximum,
                    Auto = autoCheckBox.Checked,
                    Offset = offset,
                    Multiplier = multiplier
                };
                nodeSettings[Axixname.Text] = settings;
            }
        }

        private void LoadSettingsForNode(string nodeName)
        {
            if (nodeSettings.TryGetValue(nodeName, out NodeSettings settings))
            {
                txtName.Text = settings.Name;
                minimumTextBox.Text = settings.Minimum.ToString();
                maximumTextBox.Text = settings.Maximum.ToString();
                autoCheckBox.Checked = settings.Auto;
                txtOffset.Text = settings.Offset.ToString();
                txtMultplier.Text = settings.Multiplier.ToString();
            }
            else
            {
                // Reset to default values if no settings are saved for this node
                txtName.Text = "";
                minimumTextBox.Text = "";
                maximumTextBox.Text = "";
                autoCheckBox.Checked = false;
                txtOffset.Text = "";
                txtMultplier.Text = "";
            }
        }
        private void autoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            minimumTextBox.Enabled = !autoCheckBox.Checked;
            maximumTextBox.Enabled = !autoCheckBox.Checked;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !char.IsLetter(txtName.Text[0]))
            {
                MessageBox.Show("Name must start with a character.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Text = string.Empty;
            }
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                errorProvider.Clear();
                bool allowNegative = textBox == minimumTextBox || textBox == maximumTextBox;
                bool isValid;
                if (textBox.Text == "-")
                {
                    isValid = true;
                }
                else
                {
                    isValid = textBox.Text != "" ? int.TryParse(textBox.Text, System.Globalization.NumberStyles.AllowLeadingSign, null, out _) ||
                    double.TryParse(textBox.Text, System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint, null, out _) : true;
                    errorProvider.SetError(textBox, isValid ? "" : "Invalid input");
                }
            }
        }
        public class NodeSettings
        {
            public string AxixName { get; set; }
            public string Name { get; set; }
            public double Minimum { get; set; }
            public double Maximum { get; set; }
            public bool Auto { get; set; }
            public double Offset { get; set; }
            public double Multiplier { get; set; }
        }
        public class VariableSettings
        {
            public string VariableName { get; set; }
            public KnownColor GroupColor { get; set; }
            public string GraphType { get; set; }
            public int SeriesWidth { get; set; }
        }
    }
}
