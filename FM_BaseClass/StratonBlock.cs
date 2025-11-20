using System;

namespace ClassList
{
	public class StratonBlock
	{
		private string _blockType;

		private string _dataType;

		private string _blockName;

		private int _serialNo;

		public string BlockName
		{
			get
			{
				return this._blockName;
			}
			set
			{
				this._blockName = value;
			}
		}

		public string BlockType
		{
			get
			{
				return this._blockType;
			}
			set
			{
				this._blockType = value;
			}
		}

		public string DataType
		{
			get
			{
				return this._dataType;
			}
			set
			{
				this._dataType = value;
			}
		}

		public int Number
		{
			get
			{
				return this._serialNo;
			}
			set
			{
				this._serialNo = value;
			}
		}

		public StratonBlock(int num, string name, string datatype, string blockType)
		{
			this.Number = num;
			this.BlockName = name;
			this.DataType = datatype;
			this.BlockType = blockType;
		}
	}
}