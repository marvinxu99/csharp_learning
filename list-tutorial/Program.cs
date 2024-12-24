//WorkWithString();

List<int> fibonacciNumbers = [1, 1];

var previous = fibonacciNumbers[fibonacciNumbers.Count - 1];
var previous2 = fibonacciNumbers[fibonacciNumbers.Count - 2];

fibonacciNumbers.Add(previous + previous2);

// var count = fibonacciNumbers.Count;
while (fibonacciNumbers.Count < 20)
{
    var prev = fibonacciNumbers[fibonacciNumbers.Count - 1];
    var prev2 = fibonacciNumbers[fibonacciNumbers.Count - 2];

    fibonacciNumbers.Add(prev + prev2);
}

foreach (var item in fibonacciNumbers)
{
    Console.WriteLine(item);
}


void WorkWithString()
{
    List<string> names = ["<name>", "Ana", "Felipe"];
    foreach (var name in names)
    {
        Console.WriteLine($"Hello {name.ToUpper()}!");
    }

    Console.WriteLine();
    names.Add("Maria");
    names.Add("Bill");
    names.Remove("Ana");

    Console.WriteLine($"The list has {names.Count} people in it!\n");

    // Second foreach loop with index
    foreach (var (name, index) in names.Select((value, i) => (value, i)))
    {
        Console.WriteLine($"Hello {index} {name.ToUpper()}!");
    }

    var index2 = names.IndexOf("Felipe");
    if (index2 == -1)
    {
        Console.WriteLine($"When an item is not found, IndexOf returns {index2}");
    }
    else
    {
        Console.WriteLine($"The name {names[index2]} is at index {index2}");
    }

    index2 = names.IndexOf("Not Found");
    if (index2 == -1)
    {
        Console.WriteLine($"When an item is not found, IndexOf returns {index2}");
    }
    else
    {
        Console.WriteLine($"The name {names[index2]} is at index {index2}");

    }

    names.Sort();
    foreach (var name in names)
    {
        Console.WriteLine($"Hello {name.ToUpper()}!");
    }
}

