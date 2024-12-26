//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!\n");


//var person = new Person("Alice", "Johnson");
//var (firstName, lastName) = person;

//Console.WriteLine($"{firstName} {lastName}"); // Outputs: Alice Johnson

//var person2 = person with { LastName = "Wesley" };
//var (first, last) = person2;
//Console.WriteLine($"{first} {last}"); // Outputs: Alice Johnson


//public record Person(string FirstName, string LastName);


public record Person(string FirstName, string LastName)
{
    // Custom method
    public string FullName() => $"{FirstName} {LastName}";

    // Override ToString
    public override string ToString() => $"Person: {FullName()}";
}

class Program
{
    static void Main()
    {
        var person = new Person("Alice", "Johnson");

        Console.WriteLine(person.FullName()); // Outputs: Alice Johnson

        Console.WriteLine(person);            // Outputs: Person: Alice Johnson


        string s = "The answer is " + 5.ToString();
        // Outputs: "The answer is 5"
        Console.WriteLine(s);

        Type type = 12345.GetType();
        // Outputs: "System.Int32"
        Console.WriteLine(type);
    }
}
