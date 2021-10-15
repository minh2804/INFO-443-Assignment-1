using System;
using Xunit;

namespace A1.Tests
{
	public class ItemTests
	{
		[Theory]
		[InlineData(-1, "Item", 1.99, "id")]
		[InlineData(int.MinValue, "Item", 1.99, "id")]
		[InlineData(1, "", 1.99, "name")]
		[InlineData(1, "      \t    \r\n     ", 1.99, "name")]
		[InlineData(1, "Item", -1, "price")]
		[InlineData(1, "Item", double.MinValue, "price")]
		public void Constructor_InvalidArgument_ThrowsException(int id, string name, double price, string paramName)
		{
			Assert.Throws<ArgumentException>(paramName, () => new BookItem(id, name, price) { Author = "Me", Quantity = 12 });
			Assert.Throws<ArgumentException>(paramName, () => new FoodItem(id, name, price) { Quantity = 12 });
			Assert.Throws<ArgumentException>(paramName, () => new MaterialItem(id, name, price) { Description = "Very hard", Quantity = 12 });
		}

		[Theory]
		[InlineData(1, "Book", 1.99, typeof(BookItem))]
		[InlineData(2, "Food", 2.99, typeof(FoodItem))]
		[InlineData(3, "Material", 3.99, typeof(MaterialItem))]
		public void Constructor_ValidArgument_ReturnsItem(int id, string name, double price, Type ItemType)
		{
			Assert.NotNull(Activator.CreateInstance(ItemType, id, name, price));
		}

		[Theory]
		[InlineData(0, "Item", double.MaxValue)]
		[InlineData(1, "Item", 1.99)]
		[InlineData(int.MaxValue, "Item", 1.99)]
		public void Constructor_ValidArgument_ThrowsException(int id, string name, double price)
		{
			Item[] items = new Item[]
			{
				new BookItem(id, name, price) { Author = "Me", Quantity = 12 },
				new FoodItem(id, name, price) { Quantity = 12 },
				new MaterialItem(id, name, price) { Description = "Very hard", Quantity = 12 }
			};
			foreach (Item item in items)
			{
				Assert.Equal(id, item.ID);
				Assert.Equal(name, item.Name);
				Assert.Equal(price, item.Price);
			}
		}
	}
}
