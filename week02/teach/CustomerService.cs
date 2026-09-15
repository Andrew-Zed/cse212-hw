/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Add one customer and serve them
        // Expected Result: Customer information is displayed
        Console.WriteLine("Test 1");
        var service = new CustomerService(4);
        service.AddNewCustomer();
        service.ServeCustomer();


        // Defect(s) Found: 

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add multiple customers and serve them
        // Expected Result: Customers are served in FIFO order
        Console.WriteLine("Test 2");
        service = new CustomerService(4);
        service.AddNewCustomer();
        service.AddNewCustomer();
        Console.WriteLine($"Before serving customers: {service}");
        service.ServeCustomer();
        service.ServeCustomer();
        Console.WriteLine($"After serving customers: {service}");
        // Defect(s) Found: 

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below

        // Test 3
        // Scenario: Serve customer from empty queue
        // Expected Result: Error message is displayed
        Console.WriteLine("Test 3");
        service = new CustomerService(4);
        service.ServeCustomer();
        // Defect(s) Found: Need to check queue length before serving

        // Test 4
        // Scenario: Add customer when queue is full
        // Expected Result: Queue full message displayed
        Console.WriteLine("Test 4");
        service = new CustomerService(4);
        service.AddNewCustomer();
        service.AddNewCustomer();
        service.AddNewCustomer();
        service.AddNewCustomer();
        service.AddNewCustomer();

        Console.WriteLine($"Service Queue: {service}");

        // Defect(s) Found: Need >= instead of >

        // Test 5
        // Scenario: Create queue with size 0
        // Expected Result: Size defaults to 10
        Console.WriteLine("Test 5");
        service = new CustomerService(0);
        Console.WriteLine($"Size should be 10: {service}");
        // Defect(s) Found: 

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        if (_queue.Count <= 0)
        {
            Console.WriteLine("No Customers in Queue.");
            return;
        }
        var customer = _queue[0];
        Console.WriteLine(customer);
        _queue.RemoveAt(0);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}