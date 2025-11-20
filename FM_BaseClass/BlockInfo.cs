using System;

namespace ClassList
{
	public class BlockInfo
	{
		private string _strBlockName;

		public string strBlockName
		{
			get
			{
				return this._strBlockName;
			}
			set
			{
				this._strBlockName = value;
			}
		}

		public BlockInfo(string blockName)
		{
			this._strBlockName = blockName;
		}
	}
}