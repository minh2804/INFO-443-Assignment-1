using System;
using System.Collections.Generic;
using System.Linq;

namespace A1
{
	public partial class POSTerminal
	{
		public Order CreateOrder()
		{
			return new(this);
		}

		public Order EditOrder(int id)
		{
			return new(Orders.FirstOrDefault(order => order.ID == id) ?? throw new ArgumentException("Order not found.", nameof(id)));
		}

		public bool HasNextOrder()
		{
			return Orders.Count > 0;
		}

		public void PrintOrderStatus(int id)
		{
			Order order = Orders.FirstOrDefault(order => order.ID == id) ?? throw new ArgumentException("Order not found.", nameof(id));
			Console.WriteLine("Order is in queue...");
			Console.WriteLine(order.ToJSON());
		}

		public Reciept ProcessNextOrder()
		{
			Reciept reciept = new(GetNextOrder());
			reciept.Print();
			return reciept;
		}

		protected Queue<Order> Orders { get; set; } = new();

		protected Order GetNextOrder()
		{
			if (!Orders.TryDequeue(out Order? order))
			{
				throw new InvalidOperationException("There is no pending order.");
			}
			return order;
		}
	}
}
