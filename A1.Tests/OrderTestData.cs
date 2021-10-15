using System.Collections.Generic;

namespace A1.Tests
{
	public static class OrderTestData
	{
		public static List<(Item[], double)> Orders { get; } = new()
		{
			(
				new Item[]
				{
					MockDatabase.GetItemByID(1, 2),
					MockDatabase.GetItemByID(2, 10),
					MockDatabase.GetItemByID(3, 5)
				},
				210
			),
			(
				new Item[]
				{
					MockDatabase.GetItemByID(4, 4),
					MockDatabase.GetItemByID(5, 70),
					MockDatabase.GetItemByID(6, 22)
				},
				1469.04
			)
		};
	}
}
