using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMPS2000
{
    public partial class BlocksWindow : Form
    {
        private TreeNode _listObjectTreeNode;
        private DataFormats.Format dragFBFormat = DataFormats.GetFormat("CF_K5FUNCBLOCK");

        public BlocksWindow()
        {
            InitializeComponent();
        }

        public void AddIECInstructionInTreeNode()
        {
            DataSet InstnDataSet = new DataSet();
            try
            {
                try
                {
                    InstnDataSet.ReadXml("InstructionGrp.xml");
                    DataRow[] objDataRow = InstnDataSet.Tables["Group"].Select();
                    if (this.tvBlocks.Nodes != null)
                    {
                        this.tvBlocks.Nodes.Clear();
                    }
                    int count = 0;
                    DataRow[] dataRowArray = objDataRow;
                    for (int i = 0; i < (int)dataRowArray.Length; i++)
                    {
                        DataRow objRow = dataRowArray[i];
                        string InstructionGrp = Convert.ToString(objRow["InstnGrp"]);
                        TreeNode objNode = this.tvBlocks.Nodes.Add(InstructionGrp);
                        if (count > 0)
                        {
                            this.AddInstructionSubNode(objNode, InstnDataSet, InstructionGrp, count);
                        }
                        if ((objNode.Nodes.Count != 0 ? true : count == 0))
                        {
                            count++;
                        }
                        else
                        {
                            this.tvBlocks.Nodes.Remove(objNode);
                        }
                    }
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    MessageBox.Show("Instruction list file not found " + fileNotFoundException.Message);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error occured in adding Instruction " + exception.Message );
                }
            }
            finally
            {
                if (InstnDataSet != null)
                {
                    ((IDisposable)InstnDataSet).Dispose();
                }
            }
        }

        private void AddInstructionSubNode(TreeNode objNode, DataSet objSet, string GrpName, int cnt)
        {
            string ModelID = string.Concat("Model","");
            DataTable objTable = objSet.Tables["SubNode"];
            try
            {
                if (objTable.Columns.Contains(ModelID))
                {
                    string[] grpName = new string[] { "InstnGrp='", GrpName, "' and ", ModelID, "= True" };
                    DataRow[] dataRowArray = objTable.Select(string.Concat(grpName));
                    for (int i = 0; i < (int)dataRowArray.Length; i++)
                    {
                        DataRow dr = dataRowArray[i];
                        objNode.Nodes.Add(Convert.ToString(dr["Name"]));
                        //BlockInfo objBlockInfo = new BlockInfo(Convert.ToString(dr["Name"]));
                        //this.tvBlocks.Nodes[cnt].Nodes[this.tvBlocks.Nodes[cnt].Nodes.Count - 1].Tag = objBlockInfo;
                    }
                }
            }
            finally
            {
                if (objTable != null)
                {
                    ((IDisposable)objTable).Dispose();
                }
            }
        }

        public void AddBlocksData()
        {
            this.tvBlocks.Nodes.Add("Arithmetic", "Arithmetic", 0, 0);
            this.ArithmeticBlockData();
            this.BooleansBlockData();
            this.ClockBlockData();
            this.ComparisonsBlockData();
            this.ConversionsBlockData();
            this.CountersBlockData();
            this.MathBlockData();
            this.PIDBlockData();
            this.PlusBlockData();
            this.RegistersBlockData();
            this.RegistersTypedBlockData();
            this.TimersBlockData();
        }

        public void AddBlocksTreeNodeData()
        {
            //unsafe
            //{
            //    DBReg.GetVersion();
            //    uint nbBlock = DBReg.GetNbBlock(4095);
            //    uint[] numArray = new uint[nbBlock];
            //    uint blocks = DBReg.GetBlocks(4095, numArray);
            //    this.tvBlocks.Nodes.Add("All", "All", 0, 0);
            //    for (int i = 0; i < nbBlock; i++)
            //    {
            //        uint num = numArray[i];
            //        uint num1 = 0;
            //        uint num2 = 0;
            //        uint num3 = 0;
            //        uint num4 = 0;
            //        IntPtr blockDesc = DBReg.GetBlockDesc(num, ref num1, ref num2, ref num3, ref num4);
            //        string stringAnsi = Marshal.PtrToStringAnsi(blockDesc);
            //        uint commentLength = DBReg.GetCommentLength(1, 1, stringAnsi);
            //        StringBuilder stringBuilder = new StringBuilder((int)(commentLength + 1));
            //        blocks = DBReg.GetComment(1, 1, stringAnsi, stringBuilder, commentLength + 1);
            //        CListItem cListItem = new CListItem()
            //        {
            //            m_sName = stringAnsi,
            //            m_sComment = stringBuilder.ToString(),
            //            m_bDBObject = false,
            //            m_bInstanciable = ((num1 & 4) != 0 || (num1 & 64) != 0 ? true : (num1 & 1024) != 0),
            //            m_nbIn = (int)num3,
            //            m_nbOut = (int)num4,
            //            m_dwID = num
            //        };
            //    }
            //}
        }
        public void ArithmeticBlockData()
        {
            int count = 0;
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("* (*Multiply*)", "* (*Multiply*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "* (*Multiply*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("+ (*Addition*)", "+ (*Addition*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "+ (*Addition*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("- (*Subtraction*)", "- (*Subtraction*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "- (*Subtraction*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("/ (*Divide*)", "/ (*Divide*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "/ (*Divide*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("LIMIT (*Truncate a value*)", "LIMIT (*Truncate a value*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "LIMIT (*Truncate a value*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MAX (*Maximum*)", "MAX (*Maximum*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MAX (*Maximum*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MIN (*Minimum*)", "MIN (*Minimum*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MIN (*Minimum*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MOD (*Modulo*)", "MOD (*Modulo*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MOD (*Modulo*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ODD (*Odd test*)", "ODD (*Odd test*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ODD (*Odd test*)");
        }

        public void MathBlockData()
        {
            int count = 6;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Maths", "Maths", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ABS (*Absolute value*)", "ABS (*Absolute value*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ABS (*Absolute value*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ABSL (*Absolute value (LREAL)*)", "ABSL (*Absolute value (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ABSL (*Absolute value (LREAL)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ACOS (*Arc-cosine*)", "ACOS (*Arc-cosine*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ACOS (*Arc-cosine*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ACOSL (*Arc-cosine (LREAL)*)", "ACOSL (*Arc-cosine (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ACOSL (*Arc-cosine (LREAL)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ASIN (*Arc-sine*)", "ASIN (*Arc-sine*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ASIN (*Arc-sine*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ASINL (*Arc-sine (LREAL)*)", "ASINL (*Arc-sine (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ASINL (*Arc-sine (LREAL)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ATAN (*Arc-tangent*)", "ATAN (*Arc-tangent*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ATAN (*Arc-tangent*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ATAN2 (*Arc-tangent of Y/X*)", "ATAN2 (*Arc-tangent of Y/X*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ATAN2 (*Arc-tangent of Y/X*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ATAN2L (*Arc-tangent of Y/X (LREAL)*)", "ATAN2L (*Arc-tangent of Y/X (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ATAN2L (*Arc-tangent of Y/X (LREAL)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ATANL (*Arc-tangent (LREAL)*)", "ATANL (*Arc-tangent (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ATANL (*Arc-tangent (LREAL)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("COS (*Cosine*)", "COS (*Cosine*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "COS (*Cosine*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("COSL (*Cosine (LREAL)*)", "COSL (*Cosine (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "COSL (*Cosine (LREAL)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("EXPT (*Exponent*)", "EXPT (*Exponent*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "EXPT (*Exponent*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("LOG (*Logarithm (base 10)*)", "LOG (*Logarithm (base 10)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "LOG (*Logarithm (base 10)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MODLR", "MODLR", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MODLR");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("POW (*Power*)", "POW (*Power*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "POW (*Power*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("POWL (*Power (LREAL)*)", "POWL (*Power (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "POWL (*Power (LREAL)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SIN (*Sine*)", "SIN (*Sine*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SIN (*Sine*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SINL (*Sine (LREAL)*)", "SINL (*Sine (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SINL (*Sine (LREAL)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SQRTL (*Square root (LREAL)*)", "SQRTL (*Square root (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SQRTL (*Square root (LREAL)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TAN (*Tangent*)", "TAN (*Tangent*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TAN (*Tangent*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TANL (*Tangent (LREAL)*)", "TANL (*Tangent (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TANL (*Tangent (LREAL)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TRUNC (*Truncate decimal part*)", "TRUNC (*Truncate decimal part*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TRUNC (*Truncate decimal part*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TRUNCL (*Truncate decimal part (LREAL)*)", "TRUNCL (*Truncate decimal part (LREAL)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TRUNCL (*Truncate decimal part (LREAL)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("USEDEGREES (*Use degrees as angle unit*)*)", "USEDEGREES (*Use degrees as angle unit*)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "USEDEGREES (*Use degrees as angle unit*)*)");
        }

        public void PIDBlockData()
        {
            int count = 7;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("PID", "PID", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("JS_DEADTIME (*analog delay*)", "JS_DEADTIME (*analog delay*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "JS_DEADTIME (*analog delay*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("JS_LEADLAG (*Signal lead / lag*)", "JS_LEADLAG (*Signal lead / lag*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "JS_LEADLAG (*Signal lead / lag*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("JS_PID (*PID regulator setpoint balance*)", "JS_PID (*PID regulator setpoint balance*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "JS_PID (*PID regulator setpoint balance*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("JS_RAMP (*Limit variation speed*)", "JS_RAMP (*Limit variation speed*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "JS_RAMP (*Limit variation speed*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("PID", "PID", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "PID");
        }

        public void PlusBlockData()
        {
            int count = 8;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Plus!", "Plus!", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CTDR (*DOWN counter - with rising edge detection*)", "CTDR (*DOWN counter - with rising edge detection*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CTDR (*DOWN counter - with rising edge detection*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CTUDR (*UP/DOWN counter - with rising edge detection*)", "CTUDR (*UP/DOWN counter - with rising edge detection*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CTUDR (*UP/DOWN counter - with rising edge detection*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CTUR (*UP counter - with rising edge detection*)", "CTUR (*UP counter - with rising edge detection*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CTUR (*UP counter - with rising edge detection*)");
        }

        public void RegistersBlockData()
        {
            int count = 9;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("AND_MASK (*Bit to bit AND mask*)", "AND_MASK (*Bit to bit AND mask*)", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("HIBYTE (*High part of a word*)", "HIBYTE (*High part of a word*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "HIBYTE (*High part of a word*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("HIWORD (*High part of a double word*)", "HIWORD (*High part of a double word*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "HIWORD (*High part of a double word*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("LOBYTE (*Low part of a word*)", "LOBYTE (*Low part of a word*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "LOBYTE (*Low part of a word*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("LOWORD (*Low part of a double word*)", "LOWORD (*Low part of a double word*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "LOWORD (*Low part of a double word*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MAKEDWORD (*Pack words to double word*)", "MAKEDWORD (*Pack words to double word*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MAKEDWORD (*Pack words to double word*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MAKEWORD (*Pack bytes to word*)", "MAKEWORD (*Pack bytes to word*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MAKEWORD (*Pack bytes to word*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("MBSHIFT (*Multibyte shift/rotate*)", "MBSHIFT (*Multibyte shift/rotate*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "MBSHIFT (*Multibyte shift/rotate*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_MASK (*Bit to bit negation*)", "NOT_MASK (*Bit to bit negation*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_MASK (*Bit to bit negation*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_MASK (*Bit to bit OR mask*)", "OR_MASK (*Bit to bit OR mask*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_MASK (*Bit to bit OR mask*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("PACK8 (*Fill byte with bits*)", "PACK8 (*Fill byte with bits*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "PACK8 (*Fill byte with bits*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL (*Rotate left*)", "ROL (*Rotate left*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL (*Rotate left*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR (*Rotate Right*)", "ROR (*Rotate Right*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR (*Rotate Right*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SETBIT (*Set a bit of a register*)", "SETBIT (*Set a bit of a register*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SETBIT (*Set a bit of a register*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SHL (*Shift left*)", "SHL (*Shift left*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SHL (*Shift left*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SHR (*Shift right*)", "SHR (*Shift right*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SHR (*Shift right*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TESTBIT (*Test a bit of a register*)", "TESTBIT (*Test a bit of a register*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TESTBIT (*Test a bit of a register*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("UNPACK8 (*Get bits from byte*)", "UNPACK8 (*Get bits from byte*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "UNPACK8 (*Get bits from byte*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("XOR_MASK (*Bit to bit XOR mask*)", "XOR_MASK (*Bit to bit XOR mask*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "XOR_MASK (*Bit to bit XOR mask*)");
        }

        public void RegistersTypedBlockData()
        {
            int count = 10;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("AND_BYTE (*Bit to bit AND (BYTE)*)", "AND_BYTE (*Bit to bit AND (BYTE)*)", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND_DINT (*Bit to bit AND (DINT)*)", "AND_DINT (*Bit to bit AND (DINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND_DINT (*Bit to bit AND (DINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND_DWORD (*Bit to bit AND (DWORD)*)", "AND_DWORD (*Bit to bit AND (DWORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND_DWORD (*Bit to bit AND (DWORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND_INT (*Bit to bit AND (INT)*)", "AND_INT (*Bit to bit AND (INT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND_INT (*Bit to bit AND (INT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND_SINT (*Bit to bit AND (SINT)*)", "AND_SINT (*Bit to bit AND (SINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND_SINT (*Bit to bit AND (SINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND_UDINT (*Bit to bit AND (UDINT)*)", "AND_UDINT (*Bit to bit AND (UDINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND_UDINT (*Bit to bit AND (UDINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND_UINT (*Bit to bit AND (UINT)*)", "AND_UINT (*Bit to bit AND (UINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND_UINT (*Bit to bit AND (UINT)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND_USINT (*Bit to bit AND (USINT)*)", "AND_USINT (*Bit to bit AND (USINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND_USINT (*Bit to bit AND (USINT)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("AND_WORD (*Bit to bit AND (WORD)*)", "AND_WORD (*Bit to bit AND (WORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "AND_WORD (*Bit to bit AND (WORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_DINT (*Bit to bit negation (DINT)*)", "NOT_DINT (*Bit to bit negation (DINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_DINT (*Bit to bit negation (DINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_DWORD (*Bit to bit negation (DWORD)*)", "NOT_DWORD (*Bit to bit negation (DWORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_DWORD (*Bit to bit negation (DWORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_INT (*Bit to bit negation (INT)*)", "NOT_INT (*Bit to bit negation (INT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_INT (*Bit to bit negation (INT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_SINT (*Bit to bit negation (SINT)*)", "NOT_SINT (*Bit to bit negation (SINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_SINT (*Bit to bit negation (SINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_UDINT (*Bit to bit negation (UDINT)*)", "NOT_UDINT (*Bit to bit negation (UDINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_UDINT (*Bit to bit negation (UDINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_UINT (*Bit to bit negation (UINT)*)", "NOT_UINT (*Bit to bit negation (UINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_UINT (*Bit to bit negation (UINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_USINT (*Bit to bit negation (USINT)*)", "NOT_USINT (*Bit to bit negation (USINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_USINT (*Bit to bit negation (USINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NOT_WORD (*Bit to bit negation (WORD)*)", "NOT_WORD (*Bit to bit negation (WORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NOT_WORD (*Bit to bit negation (WORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_BYTE (*Bit to bit OR (BYTE)*)", "OR_BYTE (*Bit to bit OR (BYTE)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_BYTE (*Bit to bit OR (BYTE)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_DINT (*Bit to bit OR (DINT)*)", "OR_DINT (*Bit to bit OR (DINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_DINT (*Bit to bit OR (DINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_DWORD (*Bit to bit OR (DWORD)*)", "OR_DWORD (*Bit to bit OR (DWORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_DWORD (*Bit to bit OR (DWORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_INT (*Bit to bit OR (INT)*)", "OR_INT (*Bit to bit OR (INT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_INT (*Bit to bit OR (INT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_SINT (*Bit to bit OR (SINT)*)", "OR_SINT (*Bit to bit OR (SINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_SINT (*Bit to bit OR (SINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_UDINT (*Bit to bit OR (UDINT)*)", "OR_UDINT (*Bit to bit OR (UDINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_UDINT (*Bit to bit OR (UDINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_UINT (*Bit to bit OR (UINT)*)", "OR_UINT (*Bit to bit OR (UINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_UINT (*Bit to bit OR (UINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_USINT (*Bit to bit OR (USINT)*)", "OR_USINT (*Bit to bit OR (USINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_USINT (*Bit to bit OR (USINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR_WORD (*Bit to bit OR (WORD)*)", "OR_WORD (*Bit to bit OR (WORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR_WORD (*Bit to bit OR (WORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_BYTE (*Rotate left (BYTE)*)", "ROL_BYTE (*Rotate left (BYTE)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_BYTE (*Rotate left (BYTE)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_DINT (*Rotate left (DINT)*)", "ROL_DINT (*Rotate left (DINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_DINT (*Rotate left (DINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_DWORD (*Rotate left (DWORD)*)", "ROL_DWORD (*Rotate left (DWORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_DWORD (*Rotate left (DWORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_INT (*Rotate left (INT)*)", "ROL_INT (*Rotate left (INT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_INT (*Rotate left (INT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_SINT (*Rotate left (SINT)*)", "ROL_SINT (*Rotate left (SINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_SINT (*Rotate left (SINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_UDINT (*Rotate left (UDINT)*)", "ROL_UDINT (*Rotate left (UDINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_UDINT (*Rotate left (UDINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_UINT (*Rotate left (UINT)*)", "ROL_UINT (*Rotate left (UINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_UINT (*Rotate left (UINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_USINT (*Rotate left (USINT)*)", "ROL_USINT (*Rotate left (USINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_USINT (*Rotate left (USINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROL_WORD (*Rotate left (WORD)*)", "ROL_WORD (*Rotate left (WORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROL_WORD (*Rotate left (WORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROLB (*Rotate left (8 bits)*)", "ROLB (*Rotate left (8 bits)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROLB (*Rotate left (8 bits)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROLW (*Rotate left (16 bits)*)", "ROLW (*Rotate left (16 bits)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROLW (*Rotate left (16 bits)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_BYTE (*Rotate Right (BYTE)*)", "ROR_BYTE (*Rotate Right (BYTE)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_BYTE (*Rotate Right (BYTE)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_DINT (*Rotate Right (DINT)*)", "ROR_DINT (*Rotate Right (DINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_DINT (*Rotate Right (DINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_DWORD (*Rotate Right (DWORD)*)", "ROR_DWORD (*Rotate Right (DWORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_DWORD (*Rotate Right (DWORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_INT (*Rotate Right (INT)*)", "ROR_INT (*Rotate Right (INT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_INT (*Rotate Right (INT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_SINT (*Rotate Right (SINT)*)", "ROR_SINT (*Rotate Right (SINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_SINT (*Rotate Right (SINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_UDINT (*Rotate Right (UDINT)*)*)", "ROR_UDINT (*Rotate Right (UDINT)*)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_UDINT (*Rotate Right (UDINT)*)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_UINT (*Rotate Right (UINT)*)", "ROR_UINT (*Rotate Right (UINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_UINT (*Rotate Right (UINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_USINT (*Rotate Right (USINT)*)", "ROR_USINT (*Rotate Right (USINT)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_USINT (*Rotate Right (USINT)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ROR_WORD (*Rotate Right (WORD)*)", "ROR_WORD (*Rotate Right (WORD)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ROR_WORD (*Rotate Right (WORD)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("RORB (*Rotate Right (8 bits)*)", "RORB (*Rotate Right (8 bits)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "RORB (*Rotate Right (8 bits)*)");
        }

        public void TimersBlockData()
        {
            int count = 11;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Timers", "Timers", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("BLINK (*Blink signal generator*)", "BLINK (*Blink signal generator*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "BLINK (*Blink signal generator*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("BLINKA (*Asymetric blink signal*)", "BLINKA (*Asymetric blink signal*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "BLINKA (*Asymetric blink signal*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SIG_GEN (*Signal generator*)", "SIG_GEN (*Signal generator*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SIG_GEN (*Signal generator*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TMD (*Down-counting stop watch*)", "TMD (*Down-counting stop watch*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TMD (*Down-counting stop watch*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TMU (*Up-counting stop watch*)", "TMU (*Up-counting stop watch*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TMU (*Up-counting stop watch*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TMUSEC (*Up-counting stop watch (seconds)*)", "TMUSEC (*Up-counting stop watch (seconds)*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TMUSEC (*Up-counting stop watch (seconds)*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TOF (*Off timer*)", "TOF (*Off timer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TOF (*Off timer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TOFR (*Off timer with RESET*)", "TOFR (*Off timer with RESET*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TOFR (*Off timer with RESET*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TON (*On timer*)", "TON (*On timer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TON (*On timer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TP (*Pulse timer*)", "TP (*Pulse timer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TP (*Pulse timer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("TPR (*Pulse timer with RESET*)", "TPR (*Pulse timer with RESET*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "TPR (*Pulse timer with RESET*)");
        }

        public void BooleansBlockData()
        {
            int count = 1;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("& (*Boolean AND*)", "& (*Boolean AND*)", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("F_TRIG (*Falling pulse detection*)", "F_TRIG (*Falling pulse detection*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "F_TRIG (*Falling pulse detection*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("FLIPFLOP (*Flipflop bistable*)", "FLIPFLOP (*Flipflop bistable*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "FLIPFLOP (*Flipflop bistable*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("OR (*Boolean OR*)", "OR (*Boolean OR*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "OR (*Boolean OR*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("R_TRIG (*Rising pulse detection*)", "R_TRIG (*Rising pulse detection*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "R_TRIG (*Rising pulse detection*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("RS (*Reset dominant bistable*)", "RS (*Reset dominant bistable*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "RS (*Reset dominant bistable*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SEMA (*Semaphore*)", "SEMA (*Semaphore*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SEMA (*Semaphore*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("SR (*Set dominant bistable*)", "SR (*Set dominant bistable*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "SR (*Set dominant bistable*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("XOR (*Exclusive OR*)", "XOR (*Exclusive OR*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "XOR (*Exclusive OR*)");
        }

        public void ClockBlockData()
        {
            int count = 2;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("DAY_TIME (*Get current day and time (STRING)*)", "DAY_TIME (*Get current day and time (STRING)*)", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTAT (*Pulse at a date/time*)", "DTAT (*Pulse at a date/time*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTAT (*Pulse at a date/time*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTCURDATE (*Get current date stamp*)", "DTCURDATE (*Get current date stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTCURDATE (*Get current date stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTCURDATETIME (*Get current date and time*)", "DTCURDATETIME (*Get current date and time*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTCURDATETIME (*Get current date and time*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTCURTIME (*Get current time stamp*)", "DTCURTIME (*Get current time stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTCURTIME (*Get current time stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTDAY (*Get day from date stamp*)", "DTDAY (*Get day from date stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTDAY (*Get day from date stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTEVERY (*Periodical pulse*)", "DTEVERY (*Periodical pulse*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTEVERY (*Periodical pulse*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTFORMAT (*Format current date*)", "DTFORMAT (*Format current date*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTFORMAT (*Format current date*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTHOUR (*Get hours from time stamp*)", "DTHOUR (*Get hours from time stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTHOUR (*Get hours from time stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTMONTH (*Get month from date stamp*)", "DTMONTH (*Get month from date stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTMONTH (*Get month from date stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTMS (*Get milliseconds from time stamp*)", "DTMS (*Get milliseconds from time stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTMS (*Get milliseconds from time stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTSEC (*Get seconds from time stamp*)", "DTSEC (*Get seconds from time stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTSEC (*Get seconds from time stamp*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("DTYEAR (*Get year from date stamp*)", "DTYEAR (*Get year from date stamp*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "DTYEAR (*Get year from date stamp*)");
        }

        public void ComparisonsBlockData()
        {
            int count = 3;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("< (*Less than*)", "< (*Less than*)", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("<= (*Less or equal*)", "<= (*Less or equal*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "<= (*Less or equal*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("<> (*Is not equal*)", "<> (*Is not equal*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "<> (*Is not equal*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("= (*Is equal*)", "= (*Is equal*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "= (*Is equal*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("> (*Greater than*)", "> (*Greater than*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "> (*Greater than*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add(">= (*Greater or equal*)", ">= (*Greater or equal*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, ">= (*Greater or equal*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CMP (*3 output comparison*)", "CMP (*3 output comparison*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CMP (*3 output comparison*)");
        }

        public void ConversionsBlockData()
        {
            int count = 4;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("ANY_TO_BOOL (*Convert to boolean*)", "ANY_TO_BOOL (*Convert to boolean*)", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_DINT (*Convert to 32 bit integer*)", "ANY_TO_DINT (*Convert to 32 bit integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_DINT (*Convert to 32 bit integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_INT (*Convert to 16 bit integer*)", "ANY_TO_INT (*Convert to 16 bit integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_INT (*Convert to 16 bit integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_LINT (*Convert to long integer*)", "ANY_TO_LINT (*Convert to long integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_LINT (*Convert to long integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_LREAL (*Convert to double precision real*)", "ANY_TO_LREAL (*Convert to double precision real*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_LREAL (*Convert to double precision real*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_REAL (*Convert to real*)", "ANY_TO_REAL (*Convert to real*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_REAL (*Convert to real*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_SINT (*Convert to small integer*)", "ANY_TO_SINT (*Convert to small integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_SINT (*Convert to small integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_STRING (*Convert to string*)", "ANY_TO_STRING (*Convert to string*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_STRING (*Convert to string*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_TIME (*Convert to time*)", "ANY_TO_TIME (*Convert to time*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_TIME (*Convert to time*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_UDINT (*Convert to unsigned 32 bit integer*)", "ANY_TO_UDINT (*Convert to unsigned 32 bit integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_UDINT (*Convert to unsigned 32 bit integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_UINT (*Convert to unsigned 16 bit integer*)", "ANY_TO_UINT (*Convert to unsigned 16 bit integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_UINT (*Convert to unsigned 16 bit integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("ANY_TO_USINT (*Convert to unsigned small integer*)", "ANY_TO_USINT (*Convert to unsigned small integer*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "ANY_TO_USINT (*Convert to unsigned small integer*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("BCD_TO_BIN (*BCD to binary conversion*)", "BCD_TO_BIN (*BCD to binary conversion*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "BCD_TO_BIN (*BCD to binary conversion*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("BIN_TO_BCD (*binary to BCD conversion*)", "BIN_TO_BCD (*binary to BCD conversion*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "BIN_TO_BCD (*binary to BCD conversion*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("NUM_TO_STRING (*Convert number to string*)", "NUM_TO_STRING (*Convert number to string*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "NUM_TO_STRING (*Convert number to string*)");
        }

        public void CountersBlockData()
        {
            int count = 5;
            this._listObjectTreeNode = this.tvBlocks.Nodes.Add("Counters", "Counters", 0, 0);
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CTD (*Down counter*)", "CTD (*Down counter*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CTD (*Down counter*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CTU (*Up counter*)", "CTU (*Up counter*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CTU (*Up counter*)");
            this._listObjectTreeNode = this.tvBlocks.Nodes[count].Nodes.Add("CTUD (*Up-down counter*)", "CTUD (*Up-down counter*)", 3, 3);
            this.AddBlockInfoTagData(this.tvBlocks, count, "CTUD (*Up-down counter*)");
        }

        public void AddBlockInfoTagData(TreeView objTreeView, int Count, string BlockName)
        {
            //BlockInfo objBlockInfo = new BlockInfo(BlockName);
            //objTreeView.Nodes[Count].Nodes[objTreeView.Nodes[Count].Nodes.Count - 1].Tag = objBlockInfo;
        }

        
        private void BlocksWindow_Load(object sender, EventArgs e)
        {

        }

        private void BlocksWindow_Shown(object sender, EventArgs e)
        {
            AddBlocksData();
            AddBlocksTreeNodeData();
        }

        private void tvBlocks_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //if (e.Item is TreeNode)
            //{
            //    TreeNode tn = e.Item as TreeNode;
            //    if ((tn == null || tn.Tag == null ? false : tn.Tag is BlockInfo))
            //    {
            //        BlockInfo bi = tn.Tag as BlockInfo;
            //        //if (CommonConstants.g_IEC_OnLine_Edit)
            //        //{
            //        //    if ((bi.strBlockName == null ? false : bi.strBlockName.Length > 0))
            //        //    {
            //        //        if (PrizmMDI._application.IsBlockNameDefined(this._prizmMDISelProjectID, bi.strBlockName))
            //        //        {
            //        //            MessageBox.Show("UDFB function block instance is not supported in online Edit mode.", "Flexisoft : Online Change", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //        //            return;
            //        //        }
            //        //    }
            //        //}
            //        DataObject dob = new DataObject();
            //        string str = bi.strBlockName;
            //        string strTemp = "";
            //        int i = 0;
            //        while (i < str.Length)
            //        {
            //            if (str[i] != '(')
            //            {
            //                if (str[i] != ' ')
            //                {
            //                    strTemp = string.Concat(strTemp, str[i]);
            //                }
            //                i++;
            //            }
            //            else
            //            {
            //                break;
            //            }
            //        }
            //        bi.strBlockName = strTemp;
            //        MemoryStream memStream = new MemoryStream();
            //        try
            //        {
            //            StreamWriter sw = new StreamWriter(memStream);
            //            try
            //            {
            //                sw.Write(bi.strBlockName);
            //                sw.Write('\0');
            //                sw.Flush();
            //                dob.SetData(this.dragFBFormat.Name, memStream);
            //                base.DoDragDrop(dob, DragDropEffects.All);
            //            }
            //            finally
            //            {
            //                if (sw != null)
            //                {
            //                    ((IDisposable)sw).Dispose();
            //                }
            //            }
            //        }
            //        finally
            //        {
            //            if (memStream != null)
            //            {
            //                ((IDisposable)memStream).Dispose();
            //            }
            //        }
            //    }
            //}
        }
    }
}
