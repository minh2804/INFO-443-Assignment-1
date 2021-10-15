using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace A1
{
	public class ItemEqualityComparer : IEqualityComparer<Item>
	{
		public bool Equals(Item? x, Item? y)
		{
			return x?.ID.Equals(y) ?? y?.ID.Equals(x) ?? true;
		}

		public int GetHashCode([DisallowNull] Item obj)
		{
			return obj.ID.GetHashCode();
		}
	}
}
