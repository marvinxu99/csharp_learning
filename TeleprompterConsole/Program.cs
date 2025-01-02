namespace TeleprompterConsole;

public class Program
{
    public static async Task Main()
    {
        string filePath = "E:\\eDev\\HGW\\csharp\\learning\\TeleprompterConsole\\sampleQuotes.txt";

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (s, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };
        try
        {
            await ShowTeleprompter(filePath, cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nTeleprompter canceled.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }

    private static async Task ShowTeleprompter(string filePath, CancellationToken token, int delay = 200, int lineWidth = 70)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"The file '{filePath}' does not exist.");
            return;
        }

        var words = ReadFrom(filePath, lineWidth);
        foreach (var word in words)
        {
            token.ThrowIfCancellationRequested();
            Console.Write(word);
            if (!string.IsNullOrWhiteSpace(word))
            {
                await Task.Delay(delay, token);
            }
        }
    }

    static IEnumerable<string> ReadFrom(string file, int lineWidth)
    {
        string? line;

        using var reader = File.OpenText(file);
        while ((line = reader.ReadLine()) != null)
        {
            //yield return line;
            var words = line.Split(' ');
            var lineLength = 0;
            foreach (var word in words)
            {
                yield return word + " ";
                lineLength += word.Length + 1;
                if (lineLength > lineWidth)
                {
                    yield return Environment.NewLine;
                    lineLength = 0;
                }
            }
            yield return Environment.NewLine;
        }

    }
}
