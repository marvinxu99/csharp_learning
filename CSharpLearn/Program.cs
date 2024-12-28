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
        await DiscardTest.RunTest();
        DiscardTest2.RunTest();

        // Deconstruct
        ExampleClassDeconstruction.RunTest();


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
    }
}
