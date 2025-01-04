namespace ConsoleLINQ;

internal class IntroLINQ
{
    internal static void RunTest()
    {
        // Specify the data source.
        int[] scores = [90, 71, 82, 93, 75, 82];

        // Define the query expression.
        IEnumerable<int> scoreQuery =
            from score in scores
            where score > 80
            orderby score descending
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

        int evenNumCount = numQuery.Count();
        Console.WriteLine($"Count: {evenNumCount}");

        // 3. Query execution.
        foreach (int num in numQuery)
        {
            Console.Write("{0,1} ", num);
        }
        Console.WriteLine();


        List<int> numbers2 = [1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20];

        IEnumerable<int> queryFactorsOfFour =
            from num in numbers2
            where num % 4 == 0
            select num;

        // Store the results in a new variable
        // without executing a foreach loop.
        var factorsofFourList = queryFactorsOfFour.ToList();
        Console.WriteLine(string.Join(", ", factorsofFourList));

        // Read and write from the newly created list to demonstrate that it holds data.
        Console.WriteLine(factorsofFourList[2]);
        factorsofFourList[2] = 0;
        Console.WriteLine(factorsofFourList[2]);

    }
}
