using System;
using System.Collections.Generic;
using System.Linq;

namespace A1
{
	/// <summary>
	/// POSTerminal handles the creation, processing, and modification of customer orders.
	/// </summary>
	public partial class POSTerminal
	{
		/// <summary>
		/// Creates a new empty order. This is used to build and store a customer's order information.
		/// </summary>
		/// <returns><see cref="Order"/></returns>
		public Order CreateOrder()
		{
			return new(this);
		}

		/// <summary>
		/// Creates an <see cref="Order"/> instance refering to an existing order. This is used to
		/// modify an existing order.
		/// </summary>
		/// <param name="id">An id of an existing order.</param>
		/// <returns><see cref="Order"/></returns>
		/// <exception cref="ArgumentException">Order not found.</exception>
		public Order EditOrder(int id)
		{
			return new(Orders.FirstOrDefault(order => order.ID == id) ?? throw new ArgumentException("Order not found.", nameof(id)));
		}

		/// <summary>
		/// Checks whether there is a next pending order to be processed.
		/// </summary>
		/// <returns><see cref="Boolean"/></returns>
		public bool HasNextOrder()
		{
			return Orders.Count > 0;
		}

		/// <summary>
		/// Prints a report of an existing order.
		/// </summary>
		/// <param name="id">An id of an existing order.</param>
		/// <exception cref="ArgumentException">Order not found.</exception>
		public void PrintOrderStatus(int id)
		{
			Order order = Orders.FirstOrDefault(order => order.ID == id) ?? throw new ArgumentException("Order not found.", nameof(id));
			Console.WriteLine("Order is in queue...");
			Console.WriteLine(order.ToJSON());
		}

		/// <summary>
		/// Processes the next pending order. Once the order is processed, it will be removed from
		/// the queue.
		/// </summary>
		/// <returns>A reciept of the processed order.</returns>
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
