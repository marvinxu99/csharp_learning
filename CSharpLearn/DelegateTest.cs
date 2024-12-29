// Delegate
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions

namespace CSharpLearn;

internal class DelegateTest
{
    static readonly Action<string> actionExample1 = x => Console.WriteLine($"x is: {x}");

    static readonly Action<string, string> actionExample2 = (x, y) =>
        Console.WriteLine($"x is: {x}, y is {y}");

    static readonly Func<string, int> funcExample1 = x => Convert.ToInt32(x);

    static readonly Func<int, int, int> funcExample2 = (x, y) => x + y;

    public static void RunTest()
    {
        actionExample1("string for x");

        actionExample2("string for x", "string for y");

        Console.WriteLine($"The value is {funcExample1("1")}");

        Console.WriteLine($"The sum is {funcExample2(1, 2)}");

    }
}