using System;

namespace A1
{
	public class Reciept
	{
		public Reciept(POSTerminal.Order order)
		{
			Order = new(order);
		}

		public POSTerminal.Order Order { get; }

		public void Print()
		{
			Console.WriteLine(Order.ToJSON());
		}
	}
}
