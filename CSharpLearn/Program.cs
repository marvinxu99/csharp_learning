using System.Text;

namespace CSharpLearn;


internal class Program
{
    static void Main(string[] args)
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
        PersonRecTest.runTest();

        // Constants
        ConstantsTest.RunTest();

        // Generics
        GenericsTest.RunTest();
        GenericListTest.RunTest();

        // Generic Delegate
        GenericDelegateTest.RunTest();
    }
}
