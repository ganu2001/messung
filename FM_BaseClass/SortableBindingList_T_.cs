using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassList
{
	public class SortableBindingList<T> : BindingList<T>
	{
		private bool _isSorted;

		private ListSortDirection _sortDirection;

		private PropertyDescriptor _sortProperty;

		protected override bool IsSortedCore
		{
			get
			{
				return this._isSorted;
			}
		}

		protected override ListSortDirection SortDirectionCore
		{
			get
			{
				return this._sortDirection;
			}
		}

		protected override PropertyDescriptor SortPropertyCore
		{
			get
			{
				return this._sortProperty;
			}
		}

		protected override bool SupportsSortingCore
		{
			get
			{
				return true;
			}
		}

		public SortableBindingList()
		{
		}

		public SortableBindingList(IList<T> list) : base(list)
		{
		}

		protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			this._sortProperty = prop;
			this._sortDirection = direction;
			List<T> items = base.Items as List<T>;
			if (items != null)
			{
				items.Sort(new Comparison<T>(this.Compare));
				this._isSorted = true;
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
		}

		private int Compare(T lhs, T rhs)
		{
			int num = this.OnComparison(lhs, rhs);
			if (this._sortDirection == ListSortDirection.Descending)
			{
				num = -num;
			}
			return num;
		}

		private int OnComparison(T lhs, T rhs)
		{
			int num;
			object obj = (lhs == null ? null : this._sortProperty.GetValue(lhs));
			object obj1 = (rhs == null ? null : this._sortProperty.GetValue(rhs));
			if (obj == null)
			{
				num = (obj1 == null ? 0 : -1);
			}
			else if (obj1 == null)
			{
				num = 1;
			}
			else if (!(obj is IComparable))
			{
				num = (!obj.Equals(obj1) ? obj.ToString().CompareTo(obj1.ToString()) : 0);
			}
			else
			{
				num = ((IComparable)obj).CompareTo(obj1);
			}
			return num;
		}

		protected override void RemoveSortCore()
		{
			this._sortDirection = ListSortDirection.Ascending;
			this._sortProperty = null;
		}
	}
}