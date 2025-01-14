namespace ConsoleLINQ;

internal class QueryTextFileContent
{
    internal static void RunTest()
    {
        // Query for files with a specified attribute or name
        string startFolder = """E:\eDev\HGW\csharp\learning""";
        // Or
        // string startFolder = "/usr/local/share/dotnet/sdk";

        DirectoryInfo dir = new(startFolder);
        var fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);


        string searchTerm = "change";

        var queryMatchingFiles = from file in fileList
                                 where file.Extension == ".txt"
                                 let fileText = File.ReadAllText(file.FullName)
                                 where fileText.Contains(searchTerm)
                                 select file.FullName;

        // Execute the query.
        Console.WriteLine($"""The term "{searchTerm}" was found in:""");
        foreach (string filename in queryMatchingFiles)
        {
            Console.WriteLine(filename);
        }
    }
}
