namespace FileOpExamples;

public class FileUsingStatementExample
{
    public static void RunTest()
    {
        var numbers = new List<int>();

        //var filePath = "E:\\eDev\\HGW\\csharp\\learning\\FileOpExamples\\numbers.txt";
        var filePath = @"E:\eDev\HGW\csharp\learning\FileOpExamples\numbers.txt";

        try
        {
            using StreamReader reader = File.OpenText(filePath);
            string? line;
            while ((line = reader.ReadLine()) is not null)
            {
                if (int.TryParse(line, out int number))
                {
                    numbers.Add(number);
                }
                else
                {
                    Console.WriteLine($"Skipping invalid line: {line}");
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        foreach (var number in numbers)
            Console.WriteLine(number);

        Console.WriteLine(string.Join(", ", numbers));

    }

    public static IEnumerable<int> LoadNumbers(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"The file at path {filePath} was not found.");

        using StreamReader reader = File.OpenText(filePath);

        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            if (int.TryParse(line, out int number))
            {
                yield return number; // Use yield to return numbers lazily
            }
            else
            {
                Console.WriteLine($"Skipping invalid line: {line}");
            }
        }
    }
}
