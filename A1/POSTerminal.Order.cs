using System.Collections.Generic;
using System.Linq;

namespace A1
{
	public partial class POSTerminal
	{
		public class Order
		{
			public Order(POSTerminal context)
			{
				ID = CurrentOrderID++;
				Items = new(new ItemEqualityComparer());
				Context = context;
			}

			public Order(Order other)
			{
				ID = other.ID;
				Items = new(other.Items, new ItemEqualityComparer());
				Context = other.Context;
			}

			public int ID { get; }
			public HashSet<Item> Items { get; }
			public double TotalPrice => Items.Sum(item => item.Price * item.Quantity);

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
