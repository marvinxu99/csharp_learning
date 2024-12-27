namespace CSharpLearn;

internal class PatternChecking
{
    public static void RunTest()
    {
        // "is expression"
        object greeting = "Hello, World!";
        if (greeting is string message)
        {
            Console.WriteLine(message.ToLower());  // output: hello, world!
        }

        // Declaration pattern
        int? maybe = 12;

        if (maybe is int number)
        {
            Console.WriteLine($"The nullable int 'maybe' has the value {number}");
        }
        else
        {
            Console.WriteLine("The nullable int 'maybe' doesn't hold a value");
        }

        // "switch expression"


        // ?? and ??= operators - the null-coalescing operators
        Console.WriteLine("\n===?? and ??= operators - the null-coalescing operators===");
        List<int>? numbers = null;
        int? a = null;

        Console.WriteLine((numbers is null)); // expected: true

        // if numbers is null, initialize it. Then, add 5 to numbers
        //(numbers ??= new List<int>()).Add(5);
        //numbers ??= new List<int> { 5 };
        numbers ??= [5];

        Console.WriteLine(string.Join(" ", numbers));  // output: 5
        Console.WriteLine((numbers is null)); // expected: false        


        Console.WriteLine((a is null)); // expected: true
        Console.WriteLine((a ?? 3)); // expected: 3 since a is still null 
                                     // if a is null then assign 0 to a and add a to the list
        numbers!.Add(a ??= 0);
        Console.WriteLine((a is null)); // expected: false        
        Console.WriteLine(string.Join(" ", numbers));  // output: 5 0
        Console.WriteLine(a);  // output: 0
    }

    // Type tests
    public static T MidPoint<T>(IEnumerable<T> sequence)
    {
        if (sequence is IList<T> list)
        {
            return list[list.Count / 2];
        }
        else if (sequence is null)
        {
            throw new ArgumentNullException(nameof(sequence), "Sequence can't be null.");
        }
        else
        {
            int halfLength = sequence.Count() / 2 - 1;
            if (halfLength < 0) halfLength = 0;
            return sequence.Skip(halfLength).First();
        }
    }

    public enum Operation
    {
        SystemTest,
        Start,
        Stop,
        Reset
    }

    internal static State RunDiagnostics() => new("Diagnostics Run");
    internal static State StartSystem() => new("System Started");
    internal static State StopSystem() => new("System Stopped");
    internal static State ResetToReady() => new("System Reset");

    public class State(string status)
    {
        public string Status { get; } = status;
    }

    public static State PerformOperation(Operation command) =>
    command switch
    {
        Operation.SystemTest => RunDiagnostics(),
        Operation.Start => StartSystem(),
        Operation.Stop => StopSystem(),
        Operation.Reset => ResetToReady(),
        _ => throw new ArgumentException("Invalid enum value for command", nameof(command)),
    };

    public static State PerformOperation(ReadOnlySpan<char> command) =>
    command switch
    {
        "SystemTest" => RunDiagnostics(),
        "Start" => StartSystem(),
        "Stop" => StopSystem(),
        "Reset" => ResetToReady(),
        _ => throw new ArgumentException("Invalid string value for command", nameof(command)),
    };

    // Relational pattern
    public static string WaterState(int tempInFahrenheit) =>
    tempInFahrenheit switch
    {
        (> 32) and (< 212) => "liquid",
        < 32 => "solid",
        > 212 => "gas",
        32 => "solid/liquid transition",
        212 => "liquid / gas transition",
    };

    // Multiple inputs
    public record Order(int Items, decimal Cost);
    public static decimal CalculateDiscount(Order order) =>
    order switch
    {
        { Items: > 10, Cost: > 1000.00m } => 0.10m,
        { Items: > 5, Cost: > 500.00m } => 0.05m,
        { Cost: > 250.00m } => 0.02m,
        null => throw new ArgumentNullException(nameof(order), "Can't calculate discount on null order"),
        //var someObject => 0m,
        _ => 0m
    };


    public static IEnumerable<string[]> ReadRecords()
    {
        //string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions.csv");
        //Console.WriteLine($"Looking for file at: {filePath}");

        //if (!File.Exists(filePath))
        //{
        //    throw new FileNotFoundException($"The file '{filePath}' was not found.");
        //}

        // Path to the CSV file
        //string filePath = "transactions.csv";
        string filePath = @"E:\eDev\HGW\csharp\learning\CSharpLearn\transactions.csv";

        // Ensure the file exists
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{filePath}' was not found.");
        }

        // Read all lines from the file and parse them into string arrays
        foreach (var line in File.ReadLines(filePath))
        {
            // Skip empty or whitespace lines
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            // Split the line by commas and trim whitespace from each part
            string[] fields = line.Split(',')
                                  .Select(field => field.Trim())
                                  .ToArray();

            yield return fields;
        }
    }

    public static void ListPatternTest()
    {

        decimal balance = 0m;
        foreach (string[] transaction in ReadRecords())
        {
            balance += transaction switch
            {
            [_, "DEPOSIT", _, var amount] => decimal.Parse(amount),
            [_, "WITHDRAWAL", .., var amount] => -decimal.Parse(amount),
            [_, "INTEREST", var amount] => decimal.Parse(amount),
            [_, "FEE", var fee] => -decimal.Parse(fee),
                _ => throw new InvalidOperationException($"Record {string.Join(", ", transaction)} is not in the expected format!"),
            };
            Console.WriteLine($"Record: {string.Join(", ", transaction)}, New balance: {balance:C}");
        }
    }
}
