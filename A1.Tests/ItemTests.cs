using System;
using Xunit;

namespace A1.Tests
{
	public class ItemTests
	{
		[Theory]
		[InlineData(-1, "Item", 1.99, 1, "id")]
		[InlineData(int.MinValue, "Item", 1.99, 1, "id")]
		[InlineData(1, "", 1.99, 1, "name")]
		[InlineData(1, "      \t    \r\n     ", 1.99, 1, "name")]
		[InlineData(1, "Item", -1, 1, "price")]
		[InlineData(1, "Item", double.MinValue, 1, "price")]
		[InlineData(1, "Item", 2.99, 0, "quantity")]
		[InlineData(1, "Item", 2.99, int.MinValue, "quantity")]
		public void Constructor_InvalidArgument_ThrowsException(int id, string name, double price, int quantity, string paramName)
		{
			Assert.Throws<ArgumentException>(paramName, () => new BookItem(id, name, price, quantity) { Author = "Me" });
			Assert.Throws<ArgumentException>(paramName, () => new FoodItem(id, name, price, quantity));
			Assert.Throws<ArgumentException>(paramName, () => new MaterialItem(id, name, price, quantity) { Description = "Very hard" });
		}

		[Theory]
		[InlineData(1, "Book", 1.99, 12, typeof(BookItem))]
		[InlineData(2, "Food", 2.99, 2, typeof(FoodItem))]
		[InlineData(3, "Material", 3.99, 99, typeof(MaterialItem))]
		public void Constructor_ValidArgument_ReturnsItem(int id, string name, double price, int quantity, Type ItemType)
		{
			Item item = (Item)Activator.CreateInstance(ItemType, id, name, price, quantity);
			Assert.NotNull(item);
			Assert.Equal(id, item.ID);
			Assert.Equal(name, item.Name);
			Assert.Equal(price, item.Price);
			Assert.Equal(quantity, item.Quantity);
		}

		[Theory]
		[InlineData(1, 12, typeof(BookItem))]
		[InlineData(2, 9, typeof(FoodItem))]
		[InlineData(8, 99, typeof(MaterialItem))]
		public void SetNewQuantity_ChangeQuantity_ReturnsNewItem(int initialQuantity, int newQuantity, Type ItemType)
		{
			Item item = (Item)Activator.CreateInstance(ItemType, 1, "Name1", 1.99, initialQuantity);
			Assert.NotNull(item);
			Item newItem = item.SetNewQuantity(newQuantity);
			Assert.NotEqual(item.Quantity, newItem.Quantity);
		}
	}
}
