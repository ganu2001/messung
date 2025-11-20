using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace ClassList
{
    public class TagManager
    {
        private ArrayList _tagMgrBlockList;

        private ArrayList _tagMgrTagName;

        private ArrayList _tagMgrTagID;

        private int _tagMgrMaxTagID;

        private int _tagMgrMaxBlockID;

        private bool _tagMgrDirtyFlag = false;

        private DataSet _tagMgrdsSystemTag;

        private ArrayList _tagMgrUndefinedTagList;

        private ArrayList _tagMgrTempBlockList;

        private ArrayList _tagMgrSortedBlockList;

        private bool _isSpecialPlcTagPresent = false;

        private string tempOrigianl;

        private string tempOrigianl_1;

        public bool DirtyFlag
        {
            get
            {
                return this._tagMgrDirtyFlag;
            }
            set
            {
                this._tagMgrDirtyFlag = value;
            }
        }

        public bool IsSpecialRangePLCTag
        {
            get
            {
                return this._isSpecialPlcTagPresent;
            }
            set
            {
                this._isSpecialPlcTagPresent = value;
            }
        }

        public int MaxTagID
        {
            get
            {
                return this._tagMgrMaxTagID;
            }
            set
            {
                this._tagMgrMaxTagID = value;
            }
        }

        public ArrayList TagNameList
        {
            get
            {
                return this._tagMgrTagName;
            }
        }

        public TagManager()
        {
            this._tagMgrBlockList = new ArrayList();
            this._tagMgrSortedBlockList = new ArrayList();
            this._tagMgrTagName = new ArrayList();
            this._tagMgrTagID = new ArrayList();
            this._tagMgrTempBlockList = new ArrayList();
            this._tagMgrUndefinedTagList = new ArrayList();
            this._tagMgrdsSystemTag = new DataSet();
            //if (!CommonConstants.g_Support_IEC_Ladder)
            //{
            //    this._tagMgrdsSystemTag.ReadXml(CommonConstants.DEFAULT_NODETAG_FILE);
            //}
            //else
            //{
            //    this._tagMgrdsSystemTag.ReadXml("DefaultNodeTag_IEC.xml");
            //}
            this._tagMgrMaxTagID = 0;
            this._tagMgrMaxBlockID = 0;
        }



        private string GetXMLFileName(string strTableName, string strSearchColName, string strCondColName, string strColValue)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml("ModelInformation.xml");
            string str = "";
            dataSet.Tables[strTableName].CaseSensitive = true;
            DataRow[] dataRowArray = dataSet.Tables[strTableName].Select(string.Concat(strCondColName, "='", strColValue, "'"));
            DataRow[] dataRowArray1 = dataRowArray;
            for (int i = 0; i < (int)dataRowArray1.Length; i++)
            {
                str = dataRowArray1[i][strSearchColName].ToString();
            }
            dataSet.Dispose();
            return str;
        }

        public void InsertTagName(int piTagNameIndex, string pstrTagName, bool pblFlagUpdate)
        {
            if (pblFlagUpdate)
            {
                this._tagMgrTagName.RemoveAt(piTagNameIndex);
            }
            if (piTagNameIndex <= this._tagMgrTagName.Count)
            {
                this._tagMgrTagName.Insert(piTagNameIndex, pstrTagName);
            }
            else
            {
                this._tagMgrTagName.Insert(0, pstrTagName);
            }
        }

    }
}