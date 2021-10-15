# Test Results
Line coverage: 93.75%

Branch coverage: 78.12%

See the 'coverage.cobertura.xml' file for the full report.

# Example Usage
```
public class Program
{
	// Task: Create 2 orders, modify the first 1, then process both.
	public static void Main(string[] args)
	{
		POSTerminal terminal = new();

		// Creating the first order
		POSTerminal.Order order1 = terminal.CreateOrder();
		order1.Items.Add(A1.Tests.MockDatabase.GetItemByID(1));
		order1.Items.Add(A1.Tests.MockDatabase.GetItemByID(2).SetNewQuantity(5));
		order1.Save(); // Save must be called to update the queue.

		// Creating the second order
		POSTerminal.Order order2 = terminal.CreateOrder();
		order2.Items.Add(A1.Tests.MockDatabase.GetItemByID(3));
		order2.Save();

		// Editing the first order
		POSTerminal.Order modifiedOrder1 = terminal.EditOrder(order1.ID);
		modifiedOrder1.Items.Add(A1.Tests.MockDatabase.GetItemByID(4));
		modifiedOrder1.Save(); // If save is not called, then nothing will change.

		while (!terminal.HasNextOrder())
		{
			Reciept reciept = terminal.ProcessNextOrder(); // order2 will be processed first, then the modifiedOrder1.
			reciept.Print();
		}
	}
}
```
