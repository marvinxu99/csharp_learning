namespace TeleprompterConsole;

public class Program
{
    public static async Task Main()
    {
        string filePath = "E:\\eDev\\HGW\\csharp\\learning\\TeleprompterConsole\\sampleQuotes.txt";

        await RunTeleprompter(filePath);
    }

    private static async Task RunTeleprompter(string filePath)
    {
        var config = new TelePrompterConfig();

        var displayTask = ShowTeleprompter(config, filePath);

        var speedTask = GetInput(config);

        await Task.WhenAny(displayTask, speedTask);
    }

    private static async Task ShowTeleprompter(TelePrompterConfig config, string filePath, int lineWidth = 70)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"The file '{filePath}' does not exist.");
            return;
        }

        var words = ReadFrom(filePath, lineWidth);
        foreach (var word in words)
        {
            Console.Write(word);
            if (!string.IsNullOrWhiteSpace(word))
            {
                await Task.Delay(config.DelayInMilliseconds);
            }
        }
        config.SetDone();
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

    private static async Task GetInput(TelePrompterConfig config)
    {
        Action work = () =>
        {
            do
            {
                var key = Console.ReadKey(true);
                if (key.KeyChar == '>')
                {
                    config.UpdateDelay(-100);
                }
                else if (key.KeyChar == '<')
                {
                    config.UpdateDelay(100);
                }
                else if (key.KeyChar == 'X' || key.KeyChar == 'x')
                {
                    config.SetDone();
                }
            } while (!config.Done);
        };
        await Task.Run(work);
    }
}
