using System;

namespace A1
{
	public class BookItem : Item
	{
		public BookItem(int id, string name, double price)
			: base(id, name, price)
		{
		}

		public string? Author { get; init; }
		public string Title => Name;

		public override string ToJSON()
		{
			return $"{{ \"ID\": {ID}, \"Type\": \"Book\", \"Title\": \"{Title}\", \"Author\": \"{Author ?? "N/A"}\", \"Price\": {Price}, \"Quantity\": {Quantity} }}";
		}
	}

	public class FoodItem : Item
	{
		public FoodItem(int id, string name, double price)
			: base(id, name, price)
		{
		}

		public override string ToJSON()
		{
			return $"{{ \"ID\": {ID}, \"Type\": \"Food\", \"Price\": {Price}, \"Quantity\": {Quantity} }}";
		}
	}

	public abstract class Item
	{
		public int ID { get; }
		public string Name { get; }
		public double Price { get; }
		public int Quantity { get; init; } = 1;

		public abstract string ToJSON();

		protected Item(int id, string name, double price)
		{
			if (id < 0)
			{
				throw new ArgumentException("ID cannot be less than 0.", nameof(id));
			}
			ID = id;
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Name cannot be null or empty.", nameof(name));
			}
			Name = name;
			if (price < 0)
			{
				throw new ArgumentException("Price cannot be less than 0.", nameof(price));
			}
			Price = price;
		}
	}

	public class MaterialItem : Item
	{
		public MaterialItem(int id, string name, double price)
			: base(id, name, price)
		{
		}

		public string? Description { get; init; }

		public override string ToJSON()
		{
			return $"{{ \"ID\": {ID}, \"Type\": \"Material\", \"Description\": \"{Description ?? "N/A"}\", \"Price\": {Price}, \"Quantity\": {Quantity} }}";
		}
	}
}
