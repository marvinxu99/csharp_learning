// https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/write-linq-queries

namespace ConsoleLINQ;

internal class LINQQueries
{
    internal static void RunTest()
    {
        int[] numbers = [5, 10, 8, 3, 6, 12];

        Console.Write($"Average: {numbers.Average():F2}\n");

        //Query syntax:
        IEnumerable<int> numQuery1 =
            from num in numbers
            where num % 2 == 0
            orderby num
            select num;

        //Method syntax:
        IEnumerable<int> numQuery2 = numbers
            .Where(num => num % 2 == 0)
            .OrderBy(n => n);

        foreach (int i in numQuery1)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine(System.Environment.NewLine);
        foreach (int i in numQuery2)
        {
            Console.Write(i + " ");
        }


        numbers = [5, 4, 1, 3, 9, 8, 6, 7, 2, 0];

        // The query variables can also be implicitly typed by using var

        // Query #1.
        IEnumerable<int> filteringQuery =
            from num in numbers
            where num is < 3 or > 7
            select num;

        // Query #2.
        IEnumerable<int> orderingQuery =
            from num in numbers
            where num is < 3 or > 7
            orderby num ascending
            select num;

        var orderingQuery2 = numbers
            .Where(num => num % 2 == 0)
            .OrderByDescending(n => n)
            .Select(num => num);

        // Query #3.
        string[] groupingQuery = ["carrots", "cabbage", "broccoli", "beans", "barley"];
        IEnumerable<IGrouping<char, string>> queryFoodGroups =
            from item in groupingQuery
            group item by item[0];
    }
}
