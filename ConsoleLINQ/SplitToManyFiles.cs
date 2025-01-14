namespace ConsoleLINQ;

internal class SplitToManyFiles
{
    internal static void RunTest()
    {
        var filePath1 = """E:\eDev\HGW\csharp\learning\ConsoleLINQ\Names1.txt""";
        var filePath2 = """E:\eDev\HGW\csharp\learning\ConsoleLINQ\Names2.txt""";

        string[] fileA = File.ReadAllLines(filePath1);
        string[] fileB = File.ReadAllLines(filePath2);

        // Concatenate and remove duplicate names
        var mergeQuery = fileA.Union(fileB);

        // Group the names by the first letter in the last name.
        var groupQuery = from name in mergeQuery
                         let n = name.Split(',')[0]
                         group name by n[0] into g
                         orderby g.Key
                         select g;

        foreach (var g in groupQuery)
        {
            string fileName = $"testFile_{g.Key}.txt";

            Console.WriteLine(g.Key);

            using StreamWriter sw = new(fileName);
            foreach (var item in g)
            {
                sw.WriteLine(item);
                // Output to console for example purposes.
                Console.WriteLine($"   {item}");
            }
        }

    }
}
