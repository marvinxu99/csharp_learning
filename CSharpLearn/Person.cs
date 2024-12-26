namespace CSharpLearn;

public class Person
{
    // Field
    private string name = "UNKNOWN";

    // Property
    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be empty.");
            name = value;
        }
    }

    // Event
    public event Action<string> NameChanged = delegate { };

    // Parameterless constructor
    public Person() { }

    //Constructor
    public Person(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        Name = name;
    }

    // Method
    public void ChangeName(string newName)
    {
        Name = newName;
        NameChanged?.Invoke(newName); // Notify subscribers about the name change
    }
}
