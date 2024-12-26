namespace CSharpLearn;


public record PersonRec(string FirstName, string LastName, string[] PhoneNumbers);

public record PersonRec2(string FirstName, string LastName)
{
    public required string[] PhoneNumbers { get; init; }
}

public class PersonRecTest
{
    public static void runTest()
    {
        // PersonRec
        {
            var phoneNumbers = new string[2];
            PersonRec person1 = new("Nancy", "Davolio", phoneNumbers);
            PersonRec person2 = new("Nancy", "Davolio", phoneNumbers);
            Console.WriteLine(person1 == person2); // output: True

            person1.PhoneNumbers[0] = "555-1234";
            Console.WriteLine(person1 == person2); // output: True
            Console.WriteLine(person1);
            Console.WriteLine(person2);

            Console.WriteLine(ReferenceEquals(person1, person2)); // output: False
        }

        // PersonRec2
        {
            PersonRec2 person1 = new("Nancy", "Davolio") { PhoneNumbers = new string[1] };
            Console.WriteLine(person1);
            // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }

            PersonRec2 person2 = person1 with { FirstName = "John" };
            Console.WriteLine(person2);
            // output: Person { FirstName = John, LastName = Davolio, PhoneNumbers = System.String[] }
            Console.WriteLine(person1 == person2); // output: False

            person2 = person1 with { PhoneNumbers = new string[1] };
            Console.WriteLine(person2);
            // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }
            Console.WriteLine(person1 == person2); // output: False

            person2 = person1 with { };
            Console.WriteLine(person1 == person2); // output: True
        }

    }
}
