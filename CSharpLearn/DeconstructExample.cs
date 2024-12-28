namespace CSharpLearn;

public class Person2(string fname, string mname, string lname,
                  string cityName, string stateName)
{
    public string FirstName { get; set; } = fname;
    public string MiddleName { get; set; } = mname;
    public string LastName { get; set; } = lname;
    public string City { get; set; } = cityName;
    public string State { get; set; } = stateName;

    // Return the first and last name.
    public void Deconstruct(out string fname, out string lname)
    {
        fname = FirstName;
        lname = LastName;
    }

    public void Deconstruct(out string fname, out string mname, out string lname)
    {
        fname = FirstName;
        mname = MiddleName;
        lname = LastName;
    }

    public void Deconstruct(out string fname, out string lname,
                            out string city, out string state)
    {
        fname = FirstName;
        lname = LastName;
        city = City;
        state = State;
    }
}

public class ExampleClassDeconstruction
{
    public static void RunTest()
    {
        var p = new Person2("John", "Quincy", "Adams", "Boston", "MA");

        // Deconstruct the person object.
        var (fName, lName, city, state) = p;
        Console.WriteLine($"Hello {fName} {lName} of {city}, {state}!");
    }
}
// The example displays the following output:
//    Hello John Adams of Boston, MA!