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
}
