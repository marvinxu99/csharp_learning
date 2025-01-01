namespace IndexesAndRanges;

internal class Program
{
    private readonly static string[] words = [
                    // index from start     index from end
        "first",    // 0                    ^10
        "second",   // 1                    ^9
        "third",    // 2                    ^8
        "fourth",   // 3                    ^7
        "fifth",    // 4                    ^6
        "sixth",    // 5                    ^5
        "seventh",  // 6                    ^4
        "eighth",   // 7                    ^3
        "ninth",    // 8                    ^2
        "tenth"     // 9                    ^1
    ];              // 10 (or words.Length) ^0

    static void Main()
    {
        RunTest1();

        RunTest2();

        RunTest3();

        // Impilicit range operator expressions
        Range implicitRange = 3..^5;

        Range explicitRange = new(
            start: new Index(value: 3, fromEnd: false),
            end: new Index(value: 5, fromEnd: true));

        if (implicitRange.Equals(explicitRange))
        {
            Console.WriteLine(
                $"The implicit range '{implicitRange}' equals the explicit range '{explicitRange}'");
        }
        // Sample output:
        //     The implicit range '3..^5' equals the explicit range '3..^5'
    }

    private static void RunTest3()
    {
        int[] sequence = Sequence(1000);

        for (int start = 0; start < sequence.Length; start += 100)
        {
            Range r = start..(start + 10);
            var (min, max, average) = MovingAverage(sequence, r);
            Console.WriteLine($"From {r.Start} to {r.End}:    \tMin: {min},\tMax: {max},\tAverage: {average}");
        }

        for (int start = 0; start < sequence.Length; start += 100)
        {
            Range r = ^(start + 10)..^start;
            var (min, max, average) = MovingAverage(sequence, r);
            Console.WriteLine($"From {r.Start} to {r.End}:  \tMin: {min},\tMax: {max},\tAverage: {average}");
        }

        (int min, int max, double average) MovingAverage(int[] subSequence, Range range) =>
            (
                subSequence[range].Min(),
                subSequence[range].Max(),
                subSequence[range].Average()
            );

        int[] Sequence(int count) => [.. Enumerable.Range(0, count).Select(x => (int)(Math.Sqrt(x) * 100))];
    }

    private static void RunTest2()
    {
        int[] numbers = [.. Enumerable.Range(0, 100)];
        Console.WriteLine($"Total length = {numbers.Length}");
        Console.WriteLine(string.Join(" ", numbers));

        int x = 12;
        int y = 25;
        int z = 36;

        Console.WriteLine($"{numbers[^x]} is the same as {numbers[numbers.Length - x]}");
        Console.WriteLine($"{numbers[x..y].Length} is the same as {y - x}");

        Console.WriteLine("numbers[x..y] and numbers[y..z] are consecutive and disjoint:");
        Span<int> x_y = numbers[x..y];
        Span<int> y_z = numbers[y..z];
        Console.WriteLine($"\tnumbers[x..y] is {x_y[0]} through {x_y[^1]}, numbers[y..z] is {y_z[0]} through {y_z[^1]}");

        Console.WriteLine("numbers[x..^x] removes x elements at each end:");
        Span<int> x_x = numbers[x..^x];
        Console.WriteLine($"\tnumbers[x..^x] starts with {x_x[0]} and ends with {x_x[^1]}");

        Console.WriteLine("numbers[..x] means numbers[0..x] and numbers[x..] means numbers[x..^0]");
        Span<int> start_x = numbers[..x];
        Span<int> zero_x = numbers[0..x];
        Console.WriteLine($"\t{start_x[0]}..{start_x[^1]} is the same as {zero_x[0]}..{zero_x[^1]}");
        Span<int> z_end = numbers[z..];
        Span<int> z_zero = numbers[z..^0];
        Console.WriteLine($"\t{z_end[0]}..{z_end[^1]} is the same as {z_zero[0]}..{z_zero[^1]}");
    }

    private static void RunTest1()
    {
        Console.WriteLine($"The last word is < {words[^1]} >."); // The last word is < tenth >.

        string[] secondThirdFourth = words[1..4]; // contains "second", "third" and "fourth"
        // < second >< third >< fourth >
        foreach (var word in secondThirdFourth)
            Console.Write($"< {word} >");
        Console.WriteLine();

        string[] lastTwo = words[^2..^0]; // contains "ninth" and "tenth"
        // < ninth >< tenth >
        foreach (var word in lastTwo)
            Console.Write($"< {word} >");
        Console.WriteLine();

        string[] allWords = words[..]; // contains "first" through "tenth".
        string[] firstPhrase = words[..4]; // contains "first" through "fourth"
        string[] lastPhrase = words[6..]; // contains "seventh", "eight", "ninth" and "tenth"

        List<string[]> lists = [allWords, firstPhrase, lastPhrase];
        foreach (var list in lists)
            Console.WriteLine(string.Join(", ", list));

        // Declare ranges or indices as variables
        Index thirdFromEnd = ^3;
        Console.WriteLine($"< {words[thirdFromEnd]} > "); // < eighth > 

        Range phrase = 1..4;
        string[] text = words[phrase];   // < second >< third >< fourth >
        foreach (var word in text)
            Console.Write($"< {word} >");
        Console.WriteLine();

    }
}
