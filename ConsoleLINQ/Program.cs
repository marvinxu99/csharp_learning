namespace ConsoleLINQ;

internal class Program
{
    static void Main()
    {
        // FaroShuffle.RunTest();

        // Specify the data source.
        int[] scores = [97, 92, 81, 60];
        int[] scores2 = [97, 92, 81, 60];

        // Define the query expression.
        IEnumerable<int> scoreQuery =
            from score in scores
            where score > 80
            select score;

        // Execute the query.
        foreach (var i in scoreQuery)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        // Output: 97 92 81

        // The Three Parts of a LINQ Query:
        // 1. Data source.
        int[] numbers = [0, 1, 2, 3, 4, 5, 6];

        // 2. Query creation.
        // numQuery is an IEnumerable<int>
        var numQuery =
            from num in numbers
            where (num % 2) == 0
            select num;

        // 3. Query execution.
        foreach (int num in numQuery)
        {
            Console.Write("{0,1} ", num);
        }
        Console.WriteLine();

    }
}
