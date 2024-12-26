namespace CSharpLearn;

// Type parameter T in angle brackets.
public class GenericList<T>
{
    // The nested class is also generic, and
    // holds a data item of type T.
    private class Node(T t)
    {
        // T as property type.
        public T Data { get; set; } = t;

        public Node? Next { get; set; }
    }

    // First item in the linked list
    private Node? head;

    // T as parameter type.
    public void AddHead(T t)
    {
        var n = new Node(t);
        n.Next = head;
        head = n;
    }

    // T in method return type.
    public IEnumerator<T> GetEnumerator()
    {
        Node? current = head;

        while (current is not null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }
}

public class GenericListTest
{
    public static void RunTest()
    {
        // A generic list of int.
        GenericList<int> list = new();

        // Add ten int values.
        for (int x = 0; x < 10; x++)
        {
            list.AddHead(x);
        }

        // Write them to the console.
        foreach (int i in list)
        {
            Console.Write($"{i} ");
        }

        Console.WriteLine("Done");
    }
}