using System.Collections.Generic;

namespace A1.Tests
{
	public static class OrderTestData
	{
		public static int InvalidID => -1;

		public static List<(Item[], double)> Orders { get; } = new()
		{
			(
				new Item[]
				{
					new BookItem(1, "Book1", 5) { Author = "Tom", Quantity = 2 },
					new FoodItem(2, "Food2", 10) { Quantity = 10 },
					new MaterialItem(3, "Material3", 20) { Description = "Very hard", Quantity = 5 }
				},
				210
			),
			(
				new Item[]
				{
					new BookItem(4, "Book4", 7.99) { Author = "Tom", Quantity = 4 },
					new FoodItem(5, "Food5", 12.99) { Quantity = 70 },
					new MaterialItem(6, "Material6", 23.99) { Description = "Very hard", Quantity = 22 }
				},
				1469.04
			)
		};
	}
}
