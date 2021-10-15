# Usage example
```
public class Program
{
	public static void Main(string[] args)
	{
		POSTerminal terminal = new();

		// Creating the first order
		POSTerminal.Order order1 = terminal.CreateOrder();
		order1.Items.Add(A1.Tests.MockDatabase.GetItemByID(1));
		order1.Items.Add(A1.Tests.MockDatabase.GetItemByID(2).SetNewQuantity(5));
		order1.Save();

		// Creating the second order
		POSTerminal.Order order2 = terminal.CreateOrder();
		order2.Items.Add(A1.Tests.MockDatabase.GetItemByID(3));
		order2.Save();

		// Editing the first order
		POSTerminal.Order modifiedOrder1 = terminal.EditOrder(order1.ID);
		modifiedOrder1.Items.Add(A1.Tests.MockDatabase.GetItemByID(4));
		modifiedOrder1.Save();

		while (!terminal.HasNextOrder())
		{
			Reciept reciept = terminal.ProcessNextOrder(); // order2 will be printed out first, then the modifiedOrder1.
		}
	}
}
```
