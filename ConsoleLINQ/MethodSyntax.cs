namespace ConsoleLINQ;

public static class MyAverageExtensions
{
    public static double MyAverage(this List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
        {
            throw new InvalidOperationException("Cannot calculate the average of an empty or null list.");
        }

        return numbers.Average(); // Use LINQ's Average method internally
    }
}

internal class MethodSyntax
{
    internal static void RunTest()
    {
        List<int> numbers1 = [5, 4, 1, 3, 9, 8, 6, 7, 2, 0];
        List<int> numbers2 = [15, 14, 11, 13, 19, 18, 16, 17, 12, 10];

        // Query #4.
        double average = numbers1.MyAverage();
        Console.WriteLine(average);

        // Query #5.
        IEnumerable<int> concatenationQuery = numbers1.Concat(numbers2);

        // Better: Create a new variable to store
        // the method call result
        IEnumerable<int> numbersQuery =
            from num in numbers1
            where num is > 3 and < 7
            select num;

        var numCount2 = numbersQuery.Count();
        int numCount3 = numbers1.Count(n => n is > 3 and < 7);

        // Handle null values in query expressions
        var query1 =
            from c in categories
            where c != null
            join p in products on c.ID equals p?.CategoryID
            select new
            {
                Category = c.Name,
                Name = p.Name
            };

        // Handle exceptions in query expressions
        // Not very useful as a general purpose method.
        string SomeMethodThatMightThrow(string s) =>
            s[4] == 'C' ?
                throw new InvalidOperationException() :
                @"C:\newFolder\" + s;

        // Data source.
        string[] files = ["fileA.txt", "fileB.txt", "fileC.txt"];

        // Demonstration query that throws.
        var exceptionDemoQuery =
            from file in files
            let n = SomeMethodThatMightThrow(file)
            select n;

        try
        {
            foreach (var item in exceptionDemoQuery)
            {
                Console.WriteLine($"Processing {item}");
            }
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }

        /* Output:
            Processing C:\newFolder\fileA.txt
            Processing C:\newFolder\fileB.txt
            Operation is not valid due to the current state of the object.
         */

        /* Query objects are composable, meaning that you can return a query from a method. 
         * Objects that represent queries don't store the resulting collection, but rather 
         * the steps to produce the results when needed. The advantage of returning query 
         * objects from methods is that they can be further composed or modified
         */
        IEnumerable<string> QueryMethod1(int[] ints) =>
            from i in ints
            where i > 4
            select i.ToString();

        int[] nums = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];

        var myQuery1 = QueryMethod1(nums);
        foreach (var s in myQuery1)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("You can modify a query by using query composition.");
        myQuery1 =
            from item in myQuery1
            orderby item descending
            select item;

        foreach (var s in myQuery1)
        {
            Console.WriteLine(s);
        }

        // Execute the modified query.
        Console.WriteLine("\nResults of executing modified myQuery1:");
        foreach (var s in myQuery1)
        {
            Console.WriteLine(s);
        }

        void QueryMethod2(int[] ints, out IEnumerable<string> returnQ) =>
            returnQ =
                from i in ints
                where i < 4
                select i.ToString();

        QueryMethod2(nums, out IEnumerable<string> myQuery2);
        // Execute the returned query.
        foreach (var s in myQuery2)
        {
            Console.WriteLine(s);
        }


    }

    #region Data

    class Product
    {
        public required string Name { get; init; }
        public required int CategoryID { get; init; }
    }

    class Category
    {
        public required string Name { get; init; }
        public required int ID { get; init; }
    }

    // Specify the first data source.
    readonly static List<Category> categories =
    [
        new Category {Name = "Beverages", ID = 001},
        new Category {Name="Condiments", ID=002},
        new Category {Name="Vegetables", ID=003},
        new Category {Name="Grains", ID=004},
        new Category {Name="Fruit", ID=005}
    ];

    // Specify the second data source.
    readonly static List<Product> products =
    [
      new Product {Name="Cola",  CategoryID=001},
      new Product {Name="Tea",  CategoryID=001},
      new Product {Name="Mustard", CategoryID=002},
      new Product {Name="Pickles", CategoryID=002},
      new Product {Name="Carrots", CategoryID=003},
      new Product {Name="Bok Choy", CategoryID=003},
      new Product {Name="Peaches", CategoryID=005},
      new Product {Name="Melons", CategoryID=005},
    ];
    #endregion

}
