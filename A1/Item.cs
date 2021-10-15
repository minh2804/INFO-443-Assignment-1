using System;

namespace A1
{
	public class BookItem : Item<BookItem>
	{
		public BookItem(int id, string name, double price, int quantity)
			: base(id, name, price, quantity)
		{
		}

		public string? Author { get; init; }
		public string Title => Name;

		public override string ToJSON()
		{
			return $"{{ \"ID\": {ID}, \"Type\": \"Book\", \"Title\": \"{Title}\", \"Author\": \"{Author ?? "N/A"}\", \"Price\": {Price}, \"Quantity\": {Quantity} }}";
		}
	}

	public class FoodItem : Item<FoodItem>
	{
		public FoodItem(int id, string name, double price, int quantity)
			: base(id, name, price, quantity)
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
		public int Quantity { get; } = 1;

		public abstract Item SetNewQuantity(int value);

		public abstract string ToJSON();

		protected Item(int id, string name, double price, int quantity)
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
			if (quantity < 1)
			{
				throw new ArgumentException("Quantity value cannot be less than 1.", nameof(quantity));
			}
			Quantity = quantity;
		}
	}

	public abstract class Item<T> : Item where T : Item<T>
	{
		public Item(int id, string name, double price, int quantity)
			: base(id, name, price, quantity)
		{
		}

		public override T SetNewQuantity(int value)
		{
			return (T?)Activator.CreateInstance(typeof(T), ID, Name, Price, value) ?? throw new InvalidOperationException("Constructor not found.");
		}
	}

	public class MaterialItem : Item<MaterialItem>
	{
		public MaterialItem(int id, string name, double price, int quantity)
			: base(id, name, price, quantity)
		{
		}

		public string? Description { get; init; }

		public override string ToJSON()
		{
			return $"{{ \"ID\": {ID}, \"Type\": \"Material\", \"Description\": \"{Description ?? "N/A"}\", \"Price\": {Price}, \"Quantity\": {Quantity} }}";
		}
	}
}
