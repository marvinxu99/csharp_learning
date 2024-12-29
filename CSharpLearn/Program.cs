using System.Text;

namespace CSharpLearn;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        StringBuilder builder = new();
        builder.AppendLine("The following arguments are passed:");

        // Display the command line arguments using the args variable.
        if (args.Length == 0)
        {
            builder.AppendLine("No arguments were passed.");
        }
        else
        {
            foreach (var arg in args)
            {
                builder.AppendLine($"Argument={arg}");
            }
        }

        Console.WriteLine(builder.ToString());

        // Run tests
        //RunTestPart1();

        // Discard 
        //await DiscardTest.RunTest();
        //DiscardTest2.RunTest();

        // Async Test
        //await AsyncTest.RunTest();

        // Calling unmanaged code
        //[DllImport("kernel32.dll")]
        //static extern bool Beep(int frequency, int duration);
        //Beep(1000, 500); // Beeps at 1000 Hz for 500 ms

        //string[] vowels1 = { "a", "e", "i", "o", "u" };
        string[] vowels1 = ["a", "e", "i", "o", "u"];
        Console.WriteLine(string.Join(", ", vowels1));

        DelegateTest.RunTest();

        //ReflectionExample.RunTest();
        //ReflectionExample2.RunTest();
        typeof(ReflectionExample2).InspectMembers();
    }

    private static void RunTestPart1()
    {

        ///////////////////////////////////////////////////
        // Person
        // Correct initialization of Person object
        Person person = new() { Name = "Wesley" };
        Person p2 = new("Wesley");

        // Subscribe to the NameChanged event
        person.NameChanged += newName => Console.WriteLine($"Name changed to {newName}");

        // Change the name
        person.ChangeName("Bob"); // Outputs: Name changed to Bob
        person.ChangeName("Alice"); // Outputs: Name changed to Alice

        ///////////////////////////////////////////////////
        // PersonRec
        PersonRecTest.RunTest();

        // Constants
        ConstantsTest.RunTest();

        // Generics
        GenericsTest.RunTest();
        GenericListTest.RunTest();

        // Generic Delegate
        GenericDelegateTest.RunTest();

        // Anonymous Type
        var v = new { Amount = 108, Message = "Hello" };

        // Rest the mouse pointer over v.Amount and v.Message in the following
        // statement to verify that their inferred types are int and string.
        Console.WriteLine(v.Amount + v.Message);
        Console.WriteLine(v);

        // Extension Method
        string s = "Hello Extension Methods";
        int i = s.WordCount();
        Console.WriteLine($"Word count: {i}");

        // Inheritance - WorkItem
        WorkItemTest.RunTest();

        // Polymorphism 
        ShapeTest.RunTest();

        // Pattern matching
        PatternChecking.RunTest();
        PatternChecking.ListPatternTest();

        // Deconstruct
        ExampleClassDeconstruction.RunTest();

        // Extension Method
        ExampleExtension.RunTest();

        /////////////////////////////////
        /// System.Collections.Generic.KeyValuePair<TKey,TValue> type provides Deconstruct()
        Dictionary<string, int> snapshotCommitMap = new(StringComparer.OrdinalIgnoreCase)
        {
            ["https://github.com/dotnet/docs"] = 16_465,
            ["https://github.com/dotnet/runtime"] = 114_223,
            ["https://github.com/dotnet/installer"] = 22_436,
            ["https://github.com/dotnet/roslyn"] = 79_484,
            ["https://github.com/dotnet/aspnetcore"] = 48_386
        };

        foreach (var (repo, commitCount) in snapshotCommitMap)
        {
            Console.WriteLine(
                $"The {repo} repository had {commitCount:N0} commits as of November 10th, 2021.");
        }

    }
}
