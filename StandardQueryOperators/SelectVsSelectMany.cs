namespace StandardQueryOperators;

class Bouquet
{
    public required List<string> Flowers { get; init; }
}

internal class SelectVsSelectMany
{
    internal static void RunTest()
    {
        List<Bouquet> bouquets =
            [
                new Bouquet { Flowers = ["sunflower", "daisy", "daffodil", "larkspur"] },
                new Bouquet { Flowers = ["tulip", "rose", "orchid"] },
                new Bouquet { Flowers = ["gladiolis", "lily", "snapdragon", "aster", "protea"] },
                new Bouquet { Flowers = ["larkspur", "lilac", "iris", "dahlia"] }
            ];

        IEnumerable<List<string>> query1 = bouquets.Select(bq => bq.Flowers);

        IEnumerable<string> query2 = bouquets.SelectMany(bq => bq.Flowers);

        Console.WriteLine("Results by using Select():");
        // Note the extra foreach loop here.
        foreach (IEnumerable<string> collection in query1)
        {
            Console.WriteLine($"Collection:");
            foreach (string item in collection)
            {
                Console.WriteLine(item);
            }
        }

        Console.WriteLine("\nResults by using SelectMany():");
        foreach (string item in query2)
        {
            Console.WriteLine($"Collection:");
            Console.WriteLine(item);
        }
    }
}
