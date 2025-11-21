using ClassList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace ScreenInformation
{
	public class TagSelectionGUI_Ladder : Form
	{
		private IWindowsFormsEditorService _tagselWinFormEdService;

		private TagInformation _tagInfoInstance;

		private List<CommonConstants.Prizm3TagStructure> _allTagsList = new List<CommonConstants.Prizm3TagStructure>();

		private List<string> _lstNodeNames = new List<string>();

		private List<int> _lstTagTypes = new List<int>();

		private List<int> _lstTagSizes = new List<int>();

		private List<int> _lstPorts = new List<int>();

		private List<string> _lstIECDataTypes = new List<string>();

		private List<string> _lstNativeDataTypes = new List<string>();

		private string _selectedTagName = "";

		private string _tagname = "";

		private string _tagaddr = "";

		private ArrayList _defaultTagList;

		private ArrayList _defaultTagNamesList;

		private ArrayList _filterPorts;

		private ArrayList _filterNodeNames;

		private ArrayList _filterCat;

		private ArrayList _filterDataTypes;

		private ArrayList _filterTagSizes;

		private ArrayList _filterAttr;

		private bool _blisParentDirectChecked = true;

		private bool _blSkipAfterCheckEvent = false;

		private bool _blisCalledFromGrid = false;

		private bool _blhideSystemtagChecked = false;

		private bool _blhideUnusedtagChecked = false;

		private ResourceManager _resourceTagSelection = new ResourceManager(typeof(TagSelectionGUI_Ladder));

		private Point gridviewLocation;

		private Point filterPanelLocation;

		private System.Drawing.Size gridviewSize;

		private bool alreadyFocused = false;

		private IContainer components = null;

		private TextBox txt_TagName;

		private Button btn_ok;

		private DataGridView dgv_tagdetails;

		private Label lbl_Tagname;

		private Panel panel_filter;

		private ListBox lbx_dtype;

		private Label label5;

		private Label label4;

		private Label label3;

		private Label label2;

		private Label label1;

		private ListBox lbx_cat;

		private ListBox lbx_port;

		private ListBox lbx_attr;

		private Button btn_filterreset;

		private Button btn_Showfilter;

		private ListBox listBox1;

		private GroupBox gbx_globalfilter;

		private CheckBox chkbx_hideunsedtags;

		private CheckBox chkbx_hidesystags;

		private Button btn_addtag;

		private TextBox txt_TagAddr;

		private ToolTip tooltip_addtag;

		private ToolTip tooltip_filter_showhide;

		private Label lbl_Tagaddr;

		private ListBox lbx_node;

		private DataGridViewTextBoxColumn Col_No;

		private DataGridViewTextBoxColumn Col_Tagnm;

		private DataGridViewTextBoxColumn Col_Tagaddr;

		private DataGridViewTextBoxColumn Col_Datatype;

		private DataGridViewTextBoxColumn Col_Attr;

		private DataGridViewTextBoxColumn Col_Port;

		private DataGridViewTextBoxColumn Col_Nodenm;

		private DataGridViewTextBoxColumn Col_Cat;

		public ArrayList DefaultTagList
		{
			get
			{
				return this._defaultTagList;
			}
			set
			{
				this._defaultTagList = value;
			}
		}

		public ArrayList DefaultTagNamesList
		{
			get
			{
				return this._defaultTagNamesList;
			}
			set
			{
				this._defaultTagNamesList = value;
			}
		}

		public string SelectedTagName
		{
			get
			{
				return this._selectedTagName;
			}
			set
			{
				this._selectedTagName = value;
			}
		}

		public IWindowsFormsEditorService WindowFormEdService
		{
			get
			{
				return this._tagselWinFormEdService;
			}
			set
			{
				this._tagselWinFormEdService = value;
			}
		}

		public TagSelectionGUI_Ladder()
		{
			this.InitializeComponent();
			this._defaultTagList = new ArrayList();
			this._filterPorts = new ArrayList();
			this._filterNodeNames = new ArrayList();
			this._filterCat = new ArrayList();
			this._filterDataTypes = new ArrayList();
			this._filterTagSizes = new ArrayList();
			this._filterAttr = new ArrayList();
		}

		private void btn_addtag_Click(object sender, EventArgs e)
		{
			TagSelectionDelegateClass.CallAddTagGUI();
			if (this._evntUpdateList != null)
			{
				this._evntUpdateList();
			}
			this.FillTags();
			AutoCompleteStringCollection strcoll = new AutoCompleteStringCollection();
			foreach (string tag in this.DefaultTagNamesList)
			{
				strcoll.Add(tag);
			}
			this.txt_TagName.AutoCompleteCustomSource = strcoll;
		}

		private void btn_filterreset_Click(object sender, EventArgs e)
		{
			this.resetFilterData();
		}

		private void btn_ok_Click(object sender, EventArgs e)
		{
			this.finalizeInput();
		}

		private void btn_Showfilter_Click(object sender, EventArgs e)
		{
			if (!this.panel_filter.Visible)
			{
				this.panel_filter.Visible = true;
				this.dgv_tagdetails.Location = this.gridviewLocation;
				DataGridView dgvTagdetails = this.dgv_tagdetails;
				dgvTagdetails.Height = dgvTagdetails.Height - this.panel_filter.Height;
			}
			else
			{
				this.panel_filter.Visible = false;
				this.dgv_tagdetails.Location = this.panel_filter.Location;
				DataGridView height = this.dgv_tagdetails;
				height.Height = height.Height + this.panel_filter.Height;
			}
		}

		private void chkbx_hidesystags_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkbx_hidesystags.Checked)
			{
				this._blhideSystemtagChecked = false;
			}
			else
			{
				this._blhideSystemtagChecked = true;
			}
			this.FillTags();
		}

		private void chkbx_hideunsedtags_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkbx_hideunsedtags.Checked)
			{
				this._blhideUnusedtagChecked = false;
			}
			else
			{
				this._blhideUnusedtagChecked = true;
			}
			this.FillTags();
		}

		private void dgv_tagdetails_CellClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void dgv_tagdetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex > -1)
			{
				this._tagname = this.dgv_tagdetails.Rows[e.RowIndex].Cells["Col_Tagnm"].Value.ToString();
				this.txt_TagName.Text = this._tagname;
				if (this.IsSelectedTagCorrect(this._tagname))
				{
					this.SelectedTagName = this._tagname;
					base.Close();
				}
			}
		}

		private void dgv_tagdetails_SelectionChanged(object sender, EventArgs e)
		{
			if (!this._blisCalledFromGrid)
			{
				if (this.dgv_tagdetails.SelectedRows.Count > 0)
				{
					DataGridViewRow selrow = this.dgv_tagdetails.SelectedRows[0];
					string seltagnm = selrow.Cells["Col_Tagnm"].Value.ToString();
					this.txt_TagName.Text = seltagnm;
				}
			}
		}

		private void dgv_tagdetails_Sorted(object sender, EventArgs e)
		{
			int i = 0;
			int j = 1;
			while (i < this.dgv_tagdetails.Rows.Count)
			{
				this.dgv_tagdetails.Rows[i].Cells["Col_No"].Value = j.ToString();
				i++;
				j++;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void FillDataGrid(ArrayList pTagList)
		{
			this._blisCalledFromGrid = true;
			if (this.dgv_tagdetails.Rows.Count > 0)
			{
				this.dgv_tagdetails.Rows.Clear();
			}
			int i = 0;
			int j = 1;
			while (i < pTagList.Count)
			{
				CommonConstants.Prizm3TagStructure tag = (CommonConstants.Prizm3TagStructure)pTagList[i];
				DataGridViewRow drow = new DataGridViewRow();
				this.dgv_tagdetails.Rows.Add(drow);
				this.dgv_tagdetails.Rows[i].Cells["Col_No"].Value = j.ToString();
				this.dgv_tagdetails.Rows[i].Cells["Col_Tagnm"].Value = tag._TagName;
				if (!CommonConstants.g_Support_IEC_Ladder)
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Tagaddr"].Value = tag._TagAddress;
				}
				else if (tag._NodeID != 0)
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Tagaddr"].Value = tag._TagAddress;
				}
				else
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Tagaddr"].Value = "-";
				}
				if (!(!CommonConstants.g_Support_IEC_Ladder ? true : tag._NodeID != 0))
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Datatype"].Value = tag._StratonDataType;
				}
				else if (tag._TagType == 49)
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Datatype"].Value = "Coil";
				}
				else if (tag._TagType != 51)
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Datatype"].Value = "Register";
				}
				else
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Datatype"].Value = "Register-Bit";
				}
				if (tag._ReadWrite == Convert.ToByte(48))
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Attr"].Value = "ReadOnly";
				}
				else if (tag._ReadWrite != Convert.ToByte(49))
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Attr"].Value = "ReadWrite";
				}
				else
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Attr"].Value = "WriteOnly";
				}
				switch (tag._ComID)
				{
					case 0:
					{
						this.dgv_tagdetails.Rows[i].Cells["Col_Port"].Value = "-";
						break;
					}
					case 1:
					{
						this.dgv_tagdetails.Rows[i].Cells["Col_Port"].Value = this._resourceTagSelection.GetString("Com1");
						break;
					}
					case 2:
					{
						this.dgv_tagdetails.Rows[i].Cells["Col_Port"].Value = this._resourceTagSelection.GetString("Com2");
						break;
					}
					case 3:
					{
						this.dgv_tagdetails.Rows[i].Cells["Col_Port"].Value = this._resourceTagSelection.GetString("Ethernet");
						break;
					}
					case 4:
					{
						if ((CommonConstants.ProductDataInfo.iProductID == 1860 ? false : CommonConstants.ProductDataInfo.iProductID != 992))
						{
							this.dgv_tagdetails.Rows[i].Cells["Col_Port"].Value = this._resourceTagSelection.GetString("ExpansionPort");
						}
						else
						{
							this.dgv_tagdetails.Rows[i].Cells["Col_Port"].Value = this._resourceTagSelection.GetString("Com4");
						}
						break;
					}
				}
				this.dgv_tagdetails.Rows[i].Cells["Col_Nodenm"].Value = tag._NodeName;
				if (tag._TagBy != 0)
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Cat"].Value = this._resourceTagSelection.GetString("UserDefined");
				}
				else
				{
					this.dgv_tagdetails.Rows[i].Cells["Col_Cat"].Value = this._resourceTagSelection.GetString("Default");
				}
				i++;
				j++;
			}
			this.dgv_tagdetails.Sort(this.dgv_tagdetails.Columns["Col_Tagnm"], ListSortDirection.Ascending);
			this._blisCalledFromGrid = false;
		}

		private void FillFilterData()
		{
			string dtype = null;
			this.lbx_port.Items.Clear();
			this.lbx_port.Items.Add("-");
			foreach (int port in this._lstPorts)
			{
				switch (port)
				{
					case 1:
					{
						this.lbx_port.Items.Add(this._resourceTagSelection.GetString("Com1"));
						break;
					}
					case 2:
					{
						this.lbx_port.Items.Add(this._resourceTagSelection.GetString("Com2"));
						break;
					}
					case 3:
					{
						this.lbx_port.Items.Add(this._resourceTagSelection.GetString("Ethernet"));
						break;
					}
					case 4:
					{
						if ((CommonConstants.ProductDataInfo.iProductID == 1860 ? false : CommonConstants.ProductDataInfo.iProductID != 992))
						{
							this.lbx_port.Items.Add(this._resourceTagSelection.GetString("ExpansionPort"));
						}
						else
						{
							this.lbx_port.Items.Add(this._resourceTagSelection.GetString("Com4"));
						}
						break;
					}
				}
			}
			this.lbx_node.Items.Clear();
			this.lbx_node.Items.Add("-");
			foreach (string nodenm in this._lstNodeNames)
			{
				this.lbx_node.Items.Add(nodenm);
			}
			this.lbx_cat.Items.Clear();
			this.lbx_cat.Items.Add("-");
			foreach (int tagtype in this._lstTagTypes)
			{
				switch (tagtype)
				{
					case 0:
					{
						this.lbx_cat.Items.Add(this._resourceTagSelection.GetString("Default"));
						break;
					}
					case 1:
					{
						this.lbx_cat.Items.Add(this._resourceTagSelection.GetString("UserDefined"));
						break;
					}
				}
			}
			this.lbx_dtype.Items.Clear();
			this.lbx_dtype.Items.Add("-");
			if (!CommonConstants.g_Support_IEC_Ladder)
			{
				foreach (string dtype in this._lstNativeDataTypes)
				{
					this.lbx_dtype.Items.Add(dtype);
				}
			}
			else
			{
				foreach (string _lstIECDataType in this._lstIECDataTypes)
				{
					this.lbx_dtype.Items.Add(_lstIECDataType);
				}
			}
			this.lbx_attr.Items.Clear();
			this.lbx_attr.Items.Add("-");
			this.lbx_attr.Items.Add(this._resourceTagSelection.GetString("ReadOnly"));
			this.lbx_attr.Items.Add(this._resourceTagSelection.GetString("WriteOnly"));
			this.lbx_attr.Items.Add(this._resourceTagSelection.GetString("ReadWrite"));
		}

		public void FillInformation()
		{
			this._allTagsList = this._tagInfoInstance.Tags;
			this._lstNodeNames = this._tagInfoInstance.NodeNames;
			this._lstTagTypes = this._tagInfoInstance.TagTypes;
			this._lstTagSizes = this._tagInfoInstance.TagSizes;
			this._lstPorts = this._tagInfoInstance.Ports;
			this._lstIECDataTypes = this._tagInfoInstance.IECLadderDataTypes;
			this._lstNativeDataTypes = this._tagInfoInstance.NativeDataTypes;
		}

		private void FillTags()
		{
			int tagIndex;
			int typeIndex;
			byte _bttempTagtype;
			string str;
			List<CommonConstants.Prizm3TagStructure> _filterTagList = new List<CommonConstants.Prizm3TagStructure>();
			ArrayList TempTagList = new ArrayList();
			ArrayList retTagList = new ArrayList();
			TempTagList.AddRange(this.DefaultTagList);
			if (this._filterPorts.Count > 0)
			{
				for (tagIndex = 0; tagIndex < TempTagList.Count; tagIndex++)
				{
					for (int portIndex = 0; portIndex < this._filterPorts.Count; portIndex++)
					{
						if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._ComID == Convert.ToInt32(this._filterPorts[portIndex]))
						{
							retTagList.Add(TempTagList[tagIndex]);
						}
					}
				}
				if (retTagList.Count <= 0)
				{
					TempTagList.Clear();
				}
				else
				{
					TempTagList.Clear();
					TempTagList.AddRange(retTagList);
				}
			}
			retTagList.Clear();
			if (this._filterNodeNames.Count > 0)
			{
				for (tagIndex = 0; tagIndex < TempTagList.Count; tagIndex++)
				{
					for (int NodeIndex = 0; NodeIndex < this._filterNodeNames.Count; NodeIndex++)
					{
						if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._NodeName == this._filterNodeNames[NodeIndex].ToString())
						{
							retTagList.Add((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex]);
						}
					}
				}
				if (retTagList.Count <= 0)
				{
					TempTagList.Clear();
				}
				else
				{
					TempTagList.Clear();
					TempTagList.AddRange(retTagList);
				}
			}
			retTagList.Clear();
			if (this._filterCat.Count > 0)
			{
				for (tagIndex = 0; tagIndex < TempTagList.Count; tagIndex++)
				{
					for (typeIndex = 0; typeIndex < this._filterCat.Count; typeIndex++)
					{
						if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._TagBy == Convert.ToByte(this._filterCat[typeIndex]))
						{
							retTagList.Add((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex]);
						}
					}
				}
				if (retTagList.Count <= 0)
				{
					TempTagList.Clear();
				}
				else
				{
					TempTagList.Clear();
					TempTagList.AddRange(retTagList);
				}
			}
			retTagList.Clear();
			if (this._filterDataTypes.Count > 0)
			{
				for (tagIndex = 0; tagIndex < TempTagList.Count; tagIndex++)
				{
					for (typeIndex = 0; typeIndex < this._filterDataTypes.Count; typeIndex++)
					{
						if (!CommonConstants.g_Support_IEC_Ladder)
						{
							if (this._lstNativeDataTypes.Contains(this._filterDataTypes[typeIndex].ToString()))
							{
								_bttempTagtype = 0;
								str = this._filterDataTypes[typeIndex].ToString();
								if (str != null)
								{
									if (str == "Register")
									{
										_bttempTagtype = Convert.ToByte(50);
									}
									else if (str == "Register-Bit")
									{
										_bttempTagtype = Convert.ToByte(51);
									}
									else if (str == "Coil")
									{
										_bttempTagtype = Convert.ToByte(49);
									}
								}
								if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._TagType == _bttempTagtype)
								{
									retTagList.Add((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex]);
								}
							}
						}
						else if (this._lstNativeDataTypes.Contains(this._filterDataTypes[typeIndex].ToString()))
						{
							if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._NodeID != 0)
							{
								_bttempTagtype = 0;
								str = this._filterDataTypes[typeIndex].ToString();
								if (str != null)
								{
									if (str == "Register")
									{
										_bttempTagtype = Convert.ToByte(50);
									}
									else if (str == "Register-Bit")
									{
										_bttempTagtype = Convert.ToByte(51);
									}
									else if (str == "Coil")
									{
										_bttempTagtype = Convert.ToByte(49);
									}
								}
								if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._TagType == _bttempTagtype)
								{
									retTagList.Add((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex]);
								}
							}
						}
						else if (this._lstIECDataTypes.Contains(this._filterDataTypes[typeIndex].ToString()))
						{
							if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._NodeID == 0)
							{
								if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._StratonDataType == this._filterDataTypes[typeIndex].ToString())
								{
									retTagList.Add(TempTagList[tagIndex]);
								}
							}
						}
					}
				}
				if (retTagList.Count <= 0)
				{
					TempTagList.Clear();
				}
				else
				{
					TempTagList.Clear();
					TempTagList.AddRange(retTagList);
				}
			}
			retTagList.Clear();
			if (this._filterAttr.Count > 0)
			{
				for (tagIndex = 0; tagIndex < TempTagList.Count; tagIndex++)
				{
					for (int attrIndex = 0; attrIndex < this._filterAttr.Count; attrIndex++)
					{
						if (((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex])._ReadWrite == Convert.ToByte(this._filterAttr[attrIndex]))
						{
							retTagList.Add((CommonConstants.Prizm3TagStructure)TempTagList[tagIndex]);
						}
					}
				}
				if (retTagList.Count <= 0)
				{
					TempTagList.Clear();
				}
				else
				{
					TempTagList.Clear();
					TempTagList.AddRange(retTagList);
				}
			}
			retTagList.Clear();
			if (this._blhideSystemtagChecked)
			{
				for (int sysIndex = 0; sysIndex < TempTagList.Count; sysIndex++)
				{
					if (!((CommonConstants.Prizm3TagStructure)TempTagList[sysIndex])._IsTagSystem)
					{
						retTagList.Add((CommonConstants.Prizm3TagStructure)TempTagList[sysIndex]);
					}
				}
				if (retTagList.Count <= 0)
				{
					TempTagList.Clear();
				}
				else
				{
					TempTagList.Clear();
					TempTagList.AddRange(retTagList);
				}
			}
			retTagList.Clear();
			if (this._blhideUnusedtagChecked)
			{
				for (int uIndex = 0; uIndex < TempTagList.Count; uIndex++)
				{
					if (this._tagInfoInstance.IsTagUsed((CommonConstants.Prizm3TagStructure)TempTagList[uIndex]) != DeleteTagMessage.None)
					{
						retTagList.Add((CommonConstants.Prizm3TagStructure)TempTagList[uIndex]);
					}
				}
				if (retTagList.Count <= 0)
				{
					TempTagList.Clear();
				}
				else
				{
					TempTagList.Clear();
					TempTagList.AddRange(retTagList);
				}
			}
			this.FillDataGrid(TempTagList);
		}

		private void finalizeInput()
		{
			if (!(this.txt_TagName.Text.Trim() != ""))
			{
				MessageBox.Show("Tag name should not be blank", "Incorrect tag", MessageBoxButtons.OK);
			}
			else
			{
				this._tagname = this.txt_TagName.Text.Trim();
				if (!this.IsSelectedTagCorrect(this._tagname))
				{
					MessageBox.Show("Selected tag is not supported.", "Incorrect tag", MessageBoxButtons.OK);
				}
				else
				{
					this.SelectedTagName = this._tagname;
					base.Close();
				}
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
			ComponentResourceManager resources = new ComponentResourceManager(typeof(TagSelectionGUI_Ladder));
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
			this.txt_TagName = new TextBox();
			this.btn_ok = new Button();
			this.dgv_tagdetails = new DataGridView();
			this.lbl_Tagname = new Label();
			this.panel_filter = new Panel();
			this.lbx_cat = new ListBox();
			this.gbx_globalfilter = new GroupBox();
			this.chkbx_hideunsedtags = new CheckBox();
			this.chkbx_hidesystags = new CheckBox();
			this.btn_filterreset = new Button();
			this.label5 = new Label();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.lbx_port = new ListBox();
			this.lbx_attr = new ListBox();
			this.lbx_dtype = new ListBox();
			this.lbx_node = new ListBox();
			this.label4 = new Label();
			this.btn_Showfilter = new Button();
			this.listBox1 = new ListBox();
			this.btn_addtag = new Button();
			this.txt_TagAddr = new TextBox();
			this.tooltip_addtag = new ToolTip(this.components);
			this.tooltip_filter_showhide = new ToolTip(this.components);
			this.lbl_Tagaddr = new Label();
			this.Col_No = new DataGridViewTextBoxColumn();
			this.Col_Tagnm = new DataGridViewTextBoxColumn();
			this.Col_Tagaddr = new DataGridViewTextBoxColumn();
			this.Col_Datatype = new DataGridViewTextBoxColumn();
			this.Col_Attr = new DataGridViewTextBoxColumn();
			this.Col_Port = new DataGridViewTextBoxColumn();
			this.Col_Nodenm = new DataGridViewTextBoxColumn();
			this.Col_Cat = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dgv_tagdetails).BeginInit();
			this.panel_filter.SuspendLayout();
			this.gbx_globalfilter.SuspendLayout();
			base.SuspendLayout();
			this.txt_TagName.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.txt_TagName.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.txt_TagName.CausesValidation = false;
			this.txt_TagName.Location = new Point(66, 6);
			this.txt_TagName.Name = "txt_TagName";
			this.txt_TagName.Size = new System.Drawing.Size(149, 20);
			this.txt_TagName.TabIndex = 1;
			this.txt_TagName.Leave += new EventHandler(this.txt_TagName_Leave);
			this.txt_TagName.MouseUp += new MouseEventHandler(this.txt_TagName_MouseUp);
			this.txt_TagName.TextChanged += new EventHandler(this.txt_TagName_TextChanged);
			this.txt_TagName.KeyDown += new KeyEventHandler(this.txt_TagName_KeyDown);
			this.btn_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btn_ok.ForeColor = SystemColors.ControlText;
			this.btn_ok.Location = new Point(223, 6);
			this.btn_ok.Name = "btn_ok";
			this.btn_ok.Size = new System.Drawing.Size(30, 21);
			this.btn_ok.TabIndex = 3;
			this.btn_ok.Text = "OK";
			this.btn_ok.UseVisualStyleBackColor = true;
			this.btn_ok.Click += new EventHandler(this.btn_ok_Click);
			this.dgv_tagdetails.AllowUserToAddRows = false;
			this.dgv_tagdetails.AllowUserToDeleteRows = false;
			this.dgv_tagdetails.AllowUserToResizeRows = false;
			this.dgv_tagdetails.BackgroundColor = SystemColors.Window;
			this.dgv_tagdetails.CausesValidation = false;
			this.dgv_tagdetails.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgv_tagdetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
			this.dgv_tagdetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgv_tagdetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridViewColumnCollection columns = this.dgv_tagdetails.Columns;
			DataGridViewColumn[] colNo = new DataGridViewColumn[] { this.Col_No, this.Col_Tagnm, this.Col_Tagaddr, this.Col_Datatype, this.Col_Attr, this.Col_Port, this.Col_Nodenm, this.Col_Cat };
			columns.AddRange(colNo);
			this.dgv_tagdetails.Cursor = Cursors.Arrow;
			this.dgv_tagdetails.Location = new Point(11, 185);
			this.dgv_tagdetails.MultiSelect = false;
			this.dgv_tagdetails.Name = "dgv_tagdetails";
			this.dgv_tagdetails.ReadOnly = true;
			dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle10.BackColor = SystemColors.Control;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
			this.dgv_tagdetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.dgv_tagdetails.RowHeadersVisible = false;
			dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
			this.dgv_tagdetails.RowsDefaultCellStyle = dataGridViewCellStyle11;
			this.dgv_tagdetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgv_tagdetails.ShowCellErrors = false;
			this.dgv_tagdetails.ShowRowErrors = false;
			this.dgv_tagdetails.Size = new System.Drawing.Size(332, 259);
			this.dgv_tagdetails.StandardTab = true;
			this.dgv_tagdetails.TabIndex = 7;
			this.dgv_tagdetails.CellClick += new DataGridViewCellEventHandler(this.dgv_tagdetails_CellClick);
			this.dgv_tagdetails.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_tagdetails_CellDoubleClick);
			this.dgv_tagdetails.Sorted += new EventHandler(this.dgv_tagdetails_Sorted);
			this.lbl_Tagname.AutoSize = true;
			this.lbl_Tagname.Location = new Point(8, 8);
			this.lbl_Tagname.Name = "lbl_Tagname";
			this.lbl_Tagname.Size = new System.Drawing.Size(57, 13);
			this.lbl_Tagname.TabIndex = 0;
			this.lbl_Tagname.Text = "Tag Name";
			this.panel_filter.AutoScroll = true;
			this.panel_filter.BorderStyle = BorderStyle.FixedSingle;
			this.panel_filter.Controls.Add(this.gbx_globalfilter);
			this.panel_filter.Controls.Add(this.btn_filterreset);
			this.panel_filter.Controls.Add(this.label1);
			this.panel_filter.Controls.Add(this.lbx_dtype);
			this.panel_filter.Controls.Add(this.label2);
			this.panel_filter.Controls.Add(this.label4);
			this.panel_filter.Location = new Point(11, 31);
			this.panel_filter.Name = "panel_filter";
			this.panel_filter.Size = new System.Drawing.Size(332, 148);
			this.panel_filter.TabIndex = 6;
			this.lbx_cat.FormattingEnabled = true;
			this.lbx_cat.Location = new Point(85, 193);
			this.lbx_cat.Name = "lbx_cat";
			this.lbx_cat.SelectionMode = SelectionMode.MultiExtended;
			this.lbx_cat.Size = new System.Drawing.Size(80, 121);
			this.lbx_cat.TabIndex = 7;
			this.lbx_cat.Visible = false;
			this.lbx_cat.SelectedIndexChanged += new EventHandler(this.lbx_cat_SelectedIndexChanged);
			this.lbx_cat.MouseDown += new MouseEventHandler(this.lbx_MouseDown);
			this.gbx_globalfilter.Controls.Add(this.chkbx_hideunsedtags);
			this.gbx_globalfilter.Controls.Add(this.chkbx_hidesystags);
			this.gbx_globalfilter.FlatStyle = FlatStyle.System;
			this.gbx_globalfilter.Location = new Point(15, 14);
			this.gbx_globalfilter.Name = "gbx_globalfilter";
			this.gbx_globalfilter.Size = new System.Drawing.Size(188, 100);
			this.gbx_globalfilter.TabIndex = 1;
			this.gbx_globalfilter.TabStop = false;
			this.chkbx_hideunsedtags.AutoSize = true;
			this.chkbx_hideunsedtags.FlatStyle = FlatStyle.System;
			this.chkbx_hideunsedtags.Location = new Point(31, 65);
			this.chkbx_hideunsedtags.Name = "chkbx_hideunsedtags";
			this.chkbx_hideunsedtags.Size = new System.Drawing.Size(121, 18);
			this.chkbx_hideunsedtags.TabIndex = 1;
			this.chkbx_hideunsedtags.Text = "Hide Unused Tags";
			this.chkbx_hideunsedtags.UseVisualStyleBackColor = true;
			this.chkbx_hideunsedtags.CheckedChanged += new EventHandler(this.chkbx_hideunsedtags_CheckedChanged);
			this.chkbx_hidesystags.AutoSize = true;
			this.chkbx_hidesystags.FlatStyle = FlatStyle.System;
			this.chkbx_hidesystags.Location = new Point(31, 31);
			this.chkbx_hidesystags.Name = "chkbx_hidesystags";
			this.chkbx_hidesystags.Size = new System.Drawing.Size(118, 18);
			this.chkbx_hidesystags.TabIndex = 0;
			this.chkbx_hidesystags.Text = "Hide System Tags";
			this.chkbx_hidesystags.UseVisualStyleBackColor = true;
			this.chkbx_hidesystags.CheckedChanged += new EventHandler(this.chkbx_hidesystags_CheckedChanged);
			this.btn_filterreset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btn_filterreset.ForeColor = SystemColors.ControlText;
			this.btn_filterreset.Location = new Point(118, 122);
			this.btn_filterreset.Name = "btn_filterreset";
			this.btn_filterreset.Size = new System.Drawing.Size(85, 21);
			this.btn_filterreset.TabIndex = 2;
			this.btn_filterreset.Text = "Reset Filter";
			this.btn_filterreset.UseVisualStyleBackColor = true;
			this.btn_filterreset.Click += new EventHandler(this.btn_filterreset_Click);
			this.label5.AutoSize = true;
			this.label5.Location = new Point(70, 213);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Category";
			this.label5.Visible = false;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(117, 237);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Port";
			this.label3.Visible = false;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(10, 124);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Attribute";
			this.label2.Visible = false;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(237, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Data Type";
			this.lbx_port.FormattingEnabled = true;
			this.lbx_port.Location = new Point(169, 196);
			this.lbx_port.Name = "lbx_port";
			this.lbx_port.SelectionMode = SelectionMode.MultiExtended;
			this.lbx_port.Size = new System.Drawing.Size(60, 121);
			this.lbx_port.TabIndex = 5;
			this.lbx_port.Visible = false;
			this.lbx_port.SelectedIndexChanged += new EventHandler(this.lbx_port_SelectedIndexChanged);
			this.lbx_port.MouseDown += new MouseEventHandler(this.lbx_MouseDown);
			this.lbx_attr.FormattingEnabled = true;
			this.lbx_attr.Location = new Point(12, 197);
			this.lbx_attr.Name = "lbx_attr";
			this.lbx_attr.SelectionMode = SelectionMode.MultiExtended;
			this.lbx_attr.Size = new System.Drawing.Size(70, 121);
			this.lbx_attr.TabIndex = 4;
			this.lbx_attr.Visible = false;
			this.lbx_attr.SelectedIndexChanged += new EventHandler(this.lbx_attr_SelectedIndexChanged);
			this.lbx_attr.MouseDown += new MouseEventHandler(this.lbx_MouseDown);
			this.lbx_dtype.FormattingEnabled = true;
			this.lbx_dtype.Location = new Point(210, 21);
			this.lbx_dtype.Name = "lbx_dtype";
			this.lbx_dtype.SelectionMode = SelectionMode.MultiExtended;
			this.lbx_dtype.Size = new System.Drawing.Size(108, 121);
			this.lbx_dtype.TabIndex = 3;
			this.lbx_dtype.SelectedIndexChanged += new EventHandler(this.lbx_dtype_SelectedIndexChanged);
			this.lbx_dtype.MouseDown += new MouseEventHandler(this.lbx_MouseDown);
			this.lbx_node.FormattingEnabled = true;
			this.lbx_node.Location = new Point(125, 195);
			this.lbx_node.Name = "lbx_node";
			this.lbx_node.SelectionMode = SelectionMode.MultiExtended;
			this.lbx_node.Size = new System.Drawing.Size(90, 121);
			this.lbx_node.TabIndex = 6;
			this.lbx_node.Visible = false;
			this.lbx_node.SelectedIndexChanged += new EventHandler(this.lbx_node_SelectedIndexChanged);
			this.lbx_node.MouseDown += new MouseEventHandler(this.lbx_MouseDown);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(153, 4);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(33, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Node";
			this.label4.Visible = false;
			this.btn_Showfilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btn_Showfilter.ForeColor = SystemColors.ControlText;
			this.btn_Showfilter.Image = (Image)resources.GetObject("btn_Showfilter.Image");
			this.btn_Showfilter.Location = new Point(313, 6);
			this.btn_Showfilter.Name = "btn_Showfilter";
			this.btn_Showfilter.Size = new System.Drawing.Size(30, 21);
			this.btn_Showfilter.TabIndex = 5;
			this.btn_Showfilter.Text = "+";
			this.tooltip_filter_showhide.SetToolTip(this.btn_Showfilter, "Show/Hide Filter");
			this.btn_Showfilter.UseVisualStyleBackColor = true;
			this.btn_Showfilter.Click += new EventHandler(this.btn_Showfilter_Click);
			this.listBox1.BorderStyle = BorderStyle.FixedSingle;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new Point(130, 22);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = SelectionMode.MultiExtended;
			this.listBox1.Size = new System.Drawing.Size(99, 119);
			this.listBox1.TabIndex = 14;
			this.btn_addtag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.btn_addtag.ForeColor = SystemColors.ControlText;
			this.btn_addtag.Location = new Point(253, 6);
			this.btn_addtag.Name = "btn_addtag";
			this.btn_addtag.Size = new System.Drawing.Size(60, 21);
			this.btn_addtag.TabIndex = 4;
			this.btn_addtag.Text = "Add Tag";
			this.btn_addtag.TextAlign = ContentAlignment.TopCenter;
			this.tooltip_addtag.SetToolTip(this.btn_addtag, "Add Tag");
			this.btn_addtag.UseVisualStyleBackColor = true;
			this.btn_addtag.Click += new EventHandler(this.btn_addtag_Click);
			this.txt_TagAddr.AutoCompleteMode = AutoCompleteMode.Suggest;
			this.txt_TagAddr.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.txt_TagAddr.CausesValidation = false;
			this.txt_TagAddr.Location = new Point(73, 6);
			this.txt_TagAddr.Name = "txt_TagAddr";
			this.txt_TagAddr.Size = new System.Drawing.Size(97, 20);
			this.txt_TagAddr.TabIndex = 2;
			this.txt_TagAddr.Visible = false;
			this.txt_TagAddr.TextChanged += new EventHandler(this.txt_TagAddr_TextChanged);
			this.lbl_Tagaddr.AutoSize = true;
			this.lbl_Tagaddr.Location = new Point(229, 9);
			this.lbl_Tagaddr.Name = "lbl_Tagaddr";
			this.lbl_Tagaddr.Size = new System.Drawing.Size(67, 13);
			this.lbl_Tagaddr.TabIndex = 8;
			this.lbl_Tagaddr.Text = "Tag Address";
			this.lbl_Tagaddr.Visible = false;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			this.Col_No.DefaultCellStyle = dataGridViewCellStyle2;
			this.Col_No.HeaderText = "No";
			this.Col_No.Name = "Col_No";
			this.Col_No.ReadOnly = true;
			this.Col_No.SortMode = DataGridViewColumnSortMode.NotSortable;
			this.Col_No.Width = 30;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Col_Tagnm.DefaultCellStyle = dataGridViewCellStyle3;
			this.Col_Tagnm.HeaderText = "     Tag Name";
			this.Col_Tagnm.Name = "Col_Tagnm";
			this.Col_Tagnm.ReadOnly = true;
			this.Col_Tagnm.Width = 180;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			this.Col_Tagaddr.DefaultCellStyle = dataGridViewCellStyle4;
			this.Col_Tagaddr.HeaderText = "Tag Address";
			this.Col_Tagaddr.Name = "Col_Tagaddr";
			this.Col_Tagaddr.ReadOnly = true;
			this.Col_Tagaddr.Visible = false;
			this.Col_Tagaddr.Width = 90;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Col_Datatype.DefaultCellStyle = dataGridViewCellStyle5;
			this.Col_Datatype.HeaderText = "   Data Type";
			this.Col_Datatype.Name = "Col_Datatype";
			this.Col_Datatype.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Col_Attr.DefaultCellStyle = dataGridViewCellStyle6;
			this.Col_Attr.HeaderText = "Attribute";
			this.Col_Attr.Name = "Col_Attr";
			this.Col_Attr.ReadOnly = true;
			this.Col_Attr.Visible = false;
			this.Col_Attr.Width = 70;
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Col_Port.DefaultCellStyle = dataGridViewCellStyle7;
			this.Col_Port.HeaderText = "   Port";
			this.Col_Port.Name = "Col_Port";
			this.Col_Port.ReadOnly = true;
			this.Col_Port.Visible = false;
			this.Col_Port.Width = 60;
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Col_Nodenm.DefaultCellStyle = dataGridViewCellStyle8;
			this.Col_Nodenm.HeaderText = "    Node";
			this.Col_Nodenm.Name = "Col_Nodenm";
			this.Col_Nodenm.ReadOnly = true;
			this.Col_Nodenm.Visible = false;
			this.Col_Nodenm.Width = 90;
			dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.Col_Cat.DefaultCellStyle = dataGridViewCellStyle9;
			this.Col_Cat.HeaderText = "   Category";
			this.Col_Cat.Name = "Col_Cat";
			this.Col_Cat.ReadOnly = true;
			this.Col_Cat.Visible = false;
			this.Col_Cat.Width = 84;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(356, 451);
			base.Controls.Add(this.dgv_tagdetails);
			base.Controls.Add(this.lbx_cat);
			base.Controls.Add(this.btn_Showfilter);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.btn_addtag);
			base.Controls.Add(this.lbx_node);
			base.Controls.Add(this.btn_ok);
			base.Controls.Add(this.lbl_Tagaddr);
			base.Controls.Add(this.lbx_port);
			base.Controls.Add(this.panel_filter);
			base.Controls.Add(this.lbl_Tagname);
			base.Controls.Add(this.lbx_attr);
			base.Controls.Add(this.txt_TagName);
			base.Controls.Add(this.txt_TagAddr);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TagSelectionGUI_Ladder";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Select Tag";
			base.Shown += new EventHandler(this.TagSelectionGUI_Shown);
			base.FormClosing += new FormClosingEventHandler(this.TagSelectionGUI_FormClosing);
			base.Load += new EventHandler(this.TagSelectionGUI_Load);
			((ISupportInitialize)this.dgv_tagdetails).EndInit();
			this.panel_filter.ResumeLayout(false);
			this.panel_filter.PerformLayout();
			this.gbx_globalfilter.ResumeLayout(false);
			this.gbx_globalfilter.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public bool IsSelectedTagCorrect(string pselTag)
		{
			bool _blFound = false;
			int index = 0;
			while (index < this.DefaultTagList.Count)
			{
				if (!(((CommonConstants.Prizm3TagStructure)this.DefaultTagList[index])._TagName == pselTag))
				{
					index++;
				}
				else
				{
					_blFound = true;
					break;
				}
			}
			return _blFound;
		}

		private void lbx_attr_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._filterAttr.Clear();
			for (int i = 0; i < this.lbx_attr.SelectedItems.Count; i++)
			{
				if (this.lbx_attr.SelectedItems[i].ToString() != "-")
				{
					if (this.lbx_attr.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("ReadOnly"))
					{
						this._filterAttr.Add(48);
					}
					if (this.lbx_attr.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("WriteOnly"))
					{
						this._filterAttr.Add(49);
					}
					if (this.lbx_attr.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("ReadWrite"))
					{
						this._filterAttr.Add(50);
					}
				}
			}
			this.FillTags();
		}

		private void lbx_cat_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._filterCat.Clear();
			for (int i = 0; i < this.lbx_cat.SelectedItems.Count; i++)
			{
				if (this.lbx_cat.SelectedItems[i].ToString() != "-")
				{
					if (this.lbx_cat.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("Default"))
					{
						this._filterCat.Add(0);
					}
					if (this.lbx_cat.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("UserDefined"))
					{
						this._filterCat.Add(1);
					}
				}
			}
			this.FillTags();
		}

		private void lbx_dtype_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._filterDataTypes.Clear();
			for (int i = 0; i < this.lbx_dtype.SelectedItems.Count; i++)
			{
				if (this.lbx_dtype.SelectedItems[i].ToString() != "-")
				{
					this._filterDataTypes.Add(this.lbx_dtype.SelectedItems[i].ToString());
				}
			}
			this.FillTags();
		}

		private void lbx_MouseDown(object sender, MouseEventArgs e)
		{
			if (((ListBox)sender).IndexFromPoint(e.Location.X, e.Location.Y) == -1 && ((ListBox)sender).Items.Count > 0)
			{
				((ListBox)sender).ClearSelected();
				((ListBox)sender).SelectedIndex = ((ListBox)sender).Items.Count - 1;
			}
		}

		private void lbx_node_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._filterNodeNames.Clear();
			for (int i = 0; i < this.lbx_node.SelectedItems.Count; i++)
			{
				if (this.lbx_node.SelectedItems[i].ToString() != "-")
				{
					this._filterNodeNames.Add(this.lbx_node.SelectedItems[i].ToString());
				}
			}
			this.FillTags();
		}

		private void lbx_port_SelectedIndexChanged(object sender, EventArgs e)
		{
			this._filterPorts.Clear();
			for (int i = 0; i < this.lbx_port.SelectedItems.Count; i++)
			{
				if (this.lbx_port.SelectedItems[i].ToString() != "-")
				{
					if (this.lbx_port.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("Com1"))
					{
						this._filterPorts.Add(1);
					}
					if (this.lbx_port.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("Com2"))
					{
						this._filterPorts.Add(2);
					}
					if (this.lbx_port.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("Ethernet"))
					{
						this._filterPorts.Add(3);
					}
					if (!(CommonConstants.ProductDataInfo.iProductID == 1860 ? false : CommonConstants.ProductDataInfo.iProductID != 992))
					{
						if (this.lbx_port.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("Com4"))
						{
							this._filterPorts.Add(4);
						}
					}
					else if (this.lbx_port.SelectedItems[i].ToString() == this._resourceTagSelection.GetString("ExpansionPort"))
					{
						this._filterPorts.Add(4);
					}
				}
			}
			this.FillTags();
		}

		private void lbxTagNames_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.finalizeInput();
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				base.Close();
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void resetFilterData()
		{
			this.lbx_dtype.SelectedIndex = -1;
			this.lbx_attr.SelectedIndex = -1;
			this.lbx_port.SelectedIndex = -1;
			this.lbx_node.SelectedIndex = -1;
			this.lbx_cat.SelectedIndex = -1;
			this.chkbx_hidesystags.Checked = false;
			this.chkbx_hideunsedtags.Checked = false;
			this.FillDataGrid(this.DefaultTagList);
			if (this.dgv_tagdetails.Rows.Count > 0)
			{
				this.txt_TagName.Text = this.dgv_tagdetails.Rows[0].Cells["Col_Tagnm"].Value.ToString();
				this.setSelectionFromTagName(this.dgv_tagdetails.Rows[0].Cells["Col_Tagnm"].Value.ToString());
			}
		}

		private void SetAutoCompleteSourceTagAddress(ArrayList pTagaddrList)
		{
			AutoCompleteStringCollection strcolladdr = new AutoCompleteStringCollection();
			for (int index = 0; index < pTagaddrList.Count; index++)
			{
				strcolladdr.Add(((CommonConstants.Prizm3TagStructure)pTagaddrList[index])._TagAddress.ToString());
			}
			this.txt_TagAddr.AutoCompleteCustomSource = strcolladdr;
		}

		private void SetAutoCompleteSourceTagName(ArrayList pTagnmList)
		{
			AutoCompleteStringCollection strcoll = new AutoCompleteStringCollection();
			foreach (string tag in pTagnmList)
			{
				strcoll.Add(tag);
			}
			this.txt_TagName.AutoCompleteCustomSource = strcoll;
		}

		private void setSelectionFromTagAddr(string ptagaddr)
		{
			int index = 0;
			while (index < this.dgv_tagdetails.Rows.Count)
			{
				if (!(this.dgv_tagdetails.Rows[index].Cells["Col_Tagaddr"].Value.ToString() == ptagaddr))
				{
					index++;
				}
				else
				{
					this._blisCalledFromGrid = true;
					this.dgv_tagdetails.Rows[index].Selected = true;
					TextBox txtTagName = this.txt_TagName;
					string str = this.dgv_tagdetails.Rows[index].Cells["Col_Tagnm"].Value.ToString();
					string str1 = str;
					this._tagname = str;
					txtTagName.Text = str1;
					this._blisCalledFromGrid = false;
					break;
				}
			}
			if (index >= this.dgv_tagdetails.Rows.Count)
			{
				this.dgv_tagdetails.ClearSelection();
			}
		}

		private void setSelectionFromTagName(string ptagnm)
		{
			int index = 0;
			while (index < this.dgv_tagdetails.Rows.Count)
			{
				if (!(this.dgv_tagdetails.Rows[index].Cells["Col_Tagnm"].Value.ToString() == ptagnm))
				{
					index++;
				}
				else
				{
					this._blisCalledFromGrid = true;
					this.dgv_tagdetails.Rows[index].Selected = true;
					TextBox txtTagAddr = this.txt_TagAddr;
					string str = this.dgv_tagdetails.Rows[index].Cells["Col_Tagaddr"].Value.ToString();
					string str1 = str;
					this._tagaddr = str;
					txtTagAddr.Text = str1;
					this._blisCalledFromGrid = false;
					break;
				}
			}
			if (index >= this.dgv_tagdetails.Rows.Count)
			{
				this.dgv_tagdetails.ClearSelection();
			}
		}

		private void TagSelectionGUI_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.dgv_tagdetails.Rows.Clear();
			this.dgv_tagdetails.SelectionChanged -= new EventHandler(this.dgv_tagdetails_SelectionChanged);
			this.dgv_tagdetails.Location = this.gridviewLocation;
			this.panel_filter.Location = this.filterPanelLocation;
			this.dgv_tagdetails.Size = this.gridviewSize;
		}

		private void TagSelectionGUI_Load(object sender, EventArgs e)
		{
			this._filterPorts = new ArrayList();
			this._filterNodeNames = new ArrayList();
			this._filterCat = new ArrayList();
			this._filterDataTypes = new ArrayList();
			this._filterTagSizes = new ArrayList();
			this._filterAttr = new ArrayList();
			this._tagInfoInstance = TagInformation.getTagInformationInstance();
			this.FillInformation();
			this.FillDataGrid(this.DefaultTagList);
			this.chkbx_hidesystags.Checked = true;
			TextBox txtTagName = this.txt_TagName;
			string selectedTagName = this.SelectedTagName;
			string str = selectedTagName;
			this._tagname = selectedTagName;
			txtTagName.Text = str;
			this.SetAutoCompleteSourceTagName(this.DefaultTagNamesList);
			this.SetAutoCompleteSourceTagAddress(this.DefaultTagList);
			this.FillFilterData();
			this.panel_filter.Visible = true;
			this.gridviewLocation = this.dgv_tagdetails.Location;
			this.filterPanelLocation = this.panel_filter.Location;
			this.gridviewSize = this.dgv_tagdetails.Size;
			this.txt_TagName.GotFocus += new EventHandler(this.txt_TagName_GotFocus);
		}

		private void TagSelectionGUI_Shown(object sender, EventArgs e)
		{
			this.dgv_tagdetails.SelectionChanged += new EventHandler(this.dgv_tagdetails_SelectionChanged);
			this.setSelectionFromTagName(this._tagname);
		}

		private void txt_TagAddr_TextChanged(object sender, EventArgs e)
		{
			this._tagaddr = this.txt_TagAddr.Text.Trim();
			if (!this._blisCalledFromGrid)
			{
				this.setSelectionFromTagAddr(this._tagaddr);
			}
		}

		private void txt_TagName_GotFocus(object sender, EventArgs e)
		{
			if (Control.MouseButtons == System.Windows.Forms.MouseButtons.None)
			{
				this.txt_TagName.SelectAll();
				this.alreadyFocused = true;
			}
		}

		private void txt_TagName_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void txt_TagName_Leave(object sender, EventArgs e)
		{
			this.alreadyFocused = false;
		}

		private void txt_TagName_MouseUp(object sender, MouseEventArgs e)
		{
			if ((this.alreadyFocused ? false : this.txt_TagName.SelectionLength == 0))
			{
				this.alreadyFocused = true;
				this.txt_TagName.SelectAll();
			}
		}

		private void txt_TagName_TextChanged(object sender, EventArgs e)
		{
			this._tagname = this.txt_TagName.Text.Trim();
			if (!this._blisCalledFromGrid)
			{
				this.setSelectionFromTagName(this._tagname);
			}
		}

		public event TagSelectionGUI_Ladder.callAddTagGUI _evntCallAddTagGUI;

		public event TagSelectionGUI_Ladder.UpdateList _evntUpdateList;

		public delegate void callAddTagGUI();

		public delegate void UpdateList();
	}
}