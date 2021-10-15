using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace A1.Tests
{
	public class POSTerminalTests
	{
		public POSTerminalTests(ITestOutputHelper output)
		{
			Output = output;
		}

		[Fact]
		public void EditOrderMethod_AccessANonExistingOrder_ThrowsException()
		{
			POSTerminal terminal = new();

			foreach (Item[] items in OrderTestData.Orders.Select(order => order.Item1))
			{
				POSTerminal.Order order = terminal.CreateOrder();
				foreach (Item item in items)
				{
					order.Items.Add(item);
				}
				order.Save();
			}

			Assert.Throws<ArgumentException>("id", () => terminal.EditOrder(MockDatabase.InvalidID));
		}

		[Fact]
		public void EditOrderMethod_AddNewItemToAnExistingOrder_EditedOrderShouldBeProcessedLast()
		{
			POSTerminal terminal = new();

			// Populate the first order.
			POSTerminal.Order firstOrder = terminal.CreateOrder();
			foreach (Item item in OrderTestData.Orders.First().Item1)
			{
				firstOrder.Items.Add(item);
			}
			firstOrder.Save();
			int firstOrderID = firstOrder.ID; // This order will be edited later.

			// Populate the remaining orders.
			foreach (Item[] items in OrderTestData.Orders.Skip(1).Select(order => order.Item1))
			{
				POSTerminal.Order order = terminal.CreateOrder();
				foreach (Item item in items)
				{
					order.Items.Add(item);
				}
				order.Save();
			}

			// Edit the first order
			POSTerminal.Order currentFirstOrder = terminal.EditOrder(firstOrderID);
			currentFirstOrder.Items.Add(MockDatabase.GetItemByID(7));
			currentFirstOrder.Save(); // This order should be updated and move to the back of the queue.

			while (!terminal.HasNextOrder())
			{
				Reciept reciept = terminal.ProcessNextOrder();
				Output.WriteLine(reciept.Order.ToJSON());
				if (!terminal.HasNextOrder()) // This is the last order in the queue
				{
					Assert.Equal(firstOrderID, reciept.Order.ID);
				}
			}
		}

		[Fact]
		public void EditOrderMethod_AddNewItemToAnExistingOrderButNeverSave_NothingChange()
		{
			POSTerminal terminal = new();

			// Populate the first order.
			POSTerminal.Order firstOrder = terminal.CreateOrder();
			foreach (Item item in OrderTestData.Orders.First().Item1)
			{
				firstOrder.Items.Add(item);
			}
			firstOrder.Save();
			int firstOrderID = firstOrder.ID; // This order will be edited later.

			// Populate the remaining orders.
			foreach (Item[] items in OrderTestData.Orders.Skip(1).Select(order => order.Item1))
			{
				POSTerminal.Order order = terminal.CreateOrder();
				foreach (Item item in items)
				{
					order.Items.Add(item);
				}
				order.Save();
			}

			// Edit the first order
			POSTerminal.Order currentFirstOrder = terminal.EditOrder(firstOrderID);
			currentFirstOrder.Items.Add(MockDatabase.GetItemByID(7));
			// currentFirstOrder.Save() is not called so this order will not be updated and will
			// remain in the front of the queue.

			// Only need to assert the first/front of the queue.
			Reciept reciept = terminal.ProcessNextOrder();
			Output.WriteLine(reciept.Order.ToJSON());
			Assert.Equal(firstOrderID, reciept.Order.ID);
		}

		[Fact]
		public void EditOrderMethod_RemoveAllItems_OrderShouldBeCancelled()
		{
			POSTerminal terminal = new();

			// Populate the first order.
			POSTerminal.Order firstOrder = terminal.CreateOrder();
			foreach (Item item in OrderTestData.Orders.First().Item1)
			{
				firstOrder.Items.Add(item);
			}
			firstOrder.Save();
			int firstOrderID = firstOrder.ID; // This order will be edited later.

			// Populate the remaining orders.
			foreach (Item[] items in OrderTestData.Orders.Skip(1).Select(order => order.Item1))
			{
				POSTerminal.Order order = terminal.CreateOrder();
				foreach (Item item in items)
				{
					order.Items.Add(item);
				}
				order.Save();
			}

			// Edit the first order
			POSTerminal.Order currentFirstOrder = terminal.EditOrder(firstOrderID);
			currentFirstOrder.Items.Clear();
			Output.WriteLine(currentFirstOrder.ToJSON());
			currentFirstOrder.Save(); // This order should be updated and move to the back of the queue.

			while (!terminal.HasNextOrder())
			{
				Reciept reciept = terminal.ProcessNextOrder();
				Output.WriteLine(reciept.Order.ToJSON());
				Assert.NotEqual(firstOrderID, reciept.Order.ID);
			}
		}

		[Fact]
		public void PrintOrderStatus_AccessANonExistingOrder_ThrowsException()
		{
			POSTerminal terminal = new();

			foreach (Item[] items in OrderTestData.Orders.Select(order => order.Item1))
			{
				POSTerminal.Order order = terminal.CreateOrder();
				foreach (Item item in items)
				{
					order.Items.Add(item);
				}
				order.Save();
			}

			Assert.Throws<ArgumentException>("id", () => terminal.PrintOrderStatus(MockDatabase.InvalidID));
		}

		[Fact]
		public void ProcessNextOrderMethod_BasicCase_ReturnsValidReciept()
		{
			POSTerminal terminal = new();

			foreach (Item[] items in OrderTestData.Orders.Select(order => order.Item1))
			{
				POSTerminal.Order order = terminal.CreateOrder();
				foreach (Item item in items)
				{
					order.Items.Add(item);
				}
				order.Save();
			}

			foreach ((Item[] items, double expectedTotalPrice) in OrderTestData.Orders)
			{
				Reciept reciept = terminal.ProcessNextOrder();
				Output.WriteLine(reciept.Order.ToJSON());
				Assert.Equal(items, reciept.Order.Items);
				Assert.Equal(expectedTotalPrice, reciept.Order.TotalPrice);
			}
			Assert.False(terminal.HasNextOrder());
		}

		private ITestOutputHelper Output { get; }
	}
}
