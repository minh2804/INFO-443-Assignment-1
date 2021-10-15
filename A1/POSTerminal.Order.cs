using System.Collections.Generic;
using System.Linq;

namespace A1
{
	public partial class POSTerminal
	{
		/// <summary>
		/// A builder class for building a single customer order.
		/// </summary>
		public class Order
		{
			/// <summary>
			/// Initializes a new empty order which will be used to take in order from the customer.
			/// </summary>
			/// <param name="context">A instance of <see cref="POSTerminal"/>.</param>
			public Order(POSTerminal context)
			{
				ID = CurrentOrderID++;
				Items = new(new ItemEqualityComparer());
				Context = context;
			}

			/// <summary>
			/// Copies from another instance of <see cref="Order"/>. Only the list of items is deep-copied.
			/// </summary>
			/// <param name="other">A instance of <see cref="Order"/></param>
			public Order(Order other)
			{
				ID = other.ID;
				Items = new(other.Items, new ItemEqualityComparer());
				Context = other.Context;
			}

			public int ID { get; }
			public HashSet<Item> Items { get; }
			public double TotalPrice => Items.Sum(item => item.Price * item.Quantity);

			/// <summary>
			/// Takes a snapshot of the current state and save it to <see cref="POSTerminal"/>. Each
			/// time <see cref="Save"/> is called, the previous snapshot will be replaced with the
			/// new state and moved back to the end of processing queue. If the order contains an
			/// empty list of items, then it will be considered as cancelled and remove from the queue.
			/// </summary>
			/// <remarks>
			/// Modifying the current state after saving will not update to the queue. You will need
			/// to call <see cref="Save"/> again for the update to take effect.
			/// </remarks>
			public void Save()
			{
				if (Context.Orders.FirstOrDefault(order => order.ID != ID) is not null)
				{
					Context.Orders = new(Context.Orders.Where(order => order.ID != ID));
				}
				if (Items.Any())
				{
					Context.Orders.Enqueue(new(this));
				}
			}

			/// <summary>
			/// Gets a JSON formatted string of the current state.
			/// </summary>
			/// <returns>A JSON formatted string</returns>
			public string ToJSON()
			{
				string result = $"\"OrderID\": {ID}, \"TotalPrice\": {TotalPrice}, \"Items\": [";
				if (!Items.Any())
				{
					return result += "]";
				}
				foreach (Item item in Items.SkipLast(1))
				{
					result += item.ToJSON() + ", ";
				}
				return result += $"{Items.Last().ToJSON()}]";
			}

			private static int CurrentOrderID { get; set; }
			private POSTerminal Context { get; }
		}
	}
}
