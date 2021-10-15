using System;
using System.Collections.Generic;

namespace A1
{
	public static class MockDatabase
	{
		public static int InvalidID => -1;

		public static Item GetItemByID(int id)
		{
			return Inventory.Find(item => item.ID == id) ?? throw new ArgumentException("Item not found.", nameof(id));
		}

		public static Item GetItemByID(int id, int quantity)
		{
			return Inventory.Find(item => item.ID == id)?.SetNewQuantity(quantity) ?? throw new ArgumentException("Item not found.", nameof(id));
		}

		private static int DefaultQuantity => 1;

		private static List<Item> Inventory { get; } = new()
		{
			new BookItem(1, "Book1", 5, DefaultQuantity) { Author = "Tom" },
			new FoodItem(2, "Food2", 10, DefaultQuantity),
			new MaterialItem(3, "Material3", 20, DefaultQuantity) { Description = "Very hard" },
			new BookItem(4, "Book4", 7.99, DefaultQuantity) { Author = "Tom" },
			new FoodItem(5, "Food5", 12.99, DefaultQuantity),
			new MaterialItem(6, "Material6", 23.99, DefaultQuantity) { Description = "Very very hard" },
			new MaterialItem(7, "Material7", 50.99, DefaultQuantity) { Description = "Very very very hard" }
		};
	}
}
