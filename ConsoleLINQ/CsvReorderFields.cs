namespace ConsoleLINQ;

internal class CsvReorderFields
{
    internal static void RunTest()
    {
        var filePath = """E:\eDev\HGW\csharp\learning\ConsoleLINQ\spreadsheet1.csv""";
        var filePath2 = """E:\eDev\HGW\csharp\learning\ConsoleLINQ\spreadsheet2.csv""";

        string[] lines = File.ReadAllLines(filePath);

        // Create the query. Put field 2 first, then
        // reverse and combine fields 0 and 1 from the old field
        IEnumerable<string> query = from line in lines
                                    let fields = line.Split(',')
                                    orderby fields[2]
                                    select $"{fields[2]}, {fields[1]} {fields[0]}";

        File.WriteAllLines(filePath2, query.ToArray());

        /* Output to spreadsheet2.csv:
        111, Svetlana Omelchenko
        112, Claire O'Donnell
        113, Sven Mortensen
        114, Cesar Garcia
        115, Debra Garcia
        116, Fadi Fakhouri
        117, Hanying Feng
        118, Hugo Garcia
        119, Lance Tucker
        120, Terry Adams
        121, Eugene Zabokritski
        122, Michael Tucker
        */
    }
}
