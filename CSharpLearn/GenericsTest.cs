namespace CSharpLearn;

public class GenericsTest
{
    public static void RunTest()
    {
        List<int> list = new();

        // Add ten int values.
        for (int x = 0; x < 10; x++)
        {
            list.Add(x);
        }

        // Write them to the console without a trailing comma.
        for (int i = 0; i < list.Count; i++)
        {
            if (i == list.Count - 1) // Last element
                Console.Write($"{list[i]}");
            else
                Console.Write($"{list[i]}, ");
        }

        Console.WriteLine(); // Add a newline

    }
}

public class GenericDelegateTest
{
    // Generic delegate
    public delegate T CalculatorOperation<T>(T x, T y);

    public static T Execute<T>(T a, T b, CalculatorOperation<T> operation)
    {
        return operation(a, b);
    }

    public static void RunTest()
    {
        // Execute with integers
        int result1 = Execute(10, 20, (x, y) => x + y);
        Console.WriteLine($"Addition of integers: {result1}"); // Output: 30

        // Execute with strings
        string result2 = Execute("Hello", "World", (a, b) => $"{a} {b}");
        Console.WriteLine($"Concatenation of strings: {result2}"); // Output: Hello World
    }
}