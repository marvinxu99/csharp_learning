namespace ConsoleLINQ;

internal class CsvJoinFiles
{
    internal static void RunTest()
    {
        string[] names = File.ReadAllLines(@"E:\eDev\HGW\csharp\learning\ConsoleLINQ\Names.csv");
        string[] scores = File.ReadAllLines(@"E:\eDev\HGW\csharp\learning\ConsoleLINQ\Scores.csv");

        var scoreQuery = from name in names
                         let nameFields = name.Split(',')
                         from id in scores
                         let scoreFields = id.Split(',')
                         where Convert.ToInt32(nameFields[2]) == Convert.ToInt32(scoreFields[0])
                         select $"{nameFields[0]},{scoreFields[1]},{scoreFields[2]},{scoreFields[3]},{scoreFields[4]}";

        Console.WriteLine("\r\nMerge two spreadsheets:");
        foreach (string item in scoreQuery)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("{0} total names in list", scoreQuery.Count());
    }
}
