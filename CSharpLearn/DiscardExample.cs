using System.Globalization;

namespace CSharpLearn;

internal class DiscardTest
{
    public static async Task RunTest()
    {
        // Example (1)
        object?[] objects = [CultureInfo.CurrentCulture,
                   CultureInfo.CurrentCulture.DateTimeFormat,
                   CultureInfo.CurrentCulture.NumberFormat,
                   new ArgumentException(), null];

        foreach (var obj in objects)
            ProvidesFormatInfo(obj);

        // Example (2)
        string[] dateStrings = ["05/01/2018 14:57:32.8", "2018-05-01 14:57:32.8",
                      "2018-05-01T14:57:32.8375298-04:00", "5/01/2018",
                      "5/01/2018 14:57:32.80 -07:00",
                      "1 May 2018 2:57:32.8 PM", "16-05-2018 1:00:32 PM",
                      "Fri, 15 May 2018 20:10:57 GMT"];

        foreach (string dateString in dateStrings)
        {
            if (DateTime.TryParse(dateString, out _))
                Console.WriteLine($"'{dateString}': valid");
            else
                Console.WriteLine($"'{dateString}': invalid");
        }

        // Standalone discard
        await ExecuteAsyncMethods();
    }

    static void ProvidesFormatInfo(object? obj) =>
        Console.WriteLine(obj switch
        {
            IFormatProvider fmt => $"{fmt.GetType()} object",
            null => "A null object reference: Its use could result in a NullReferenceException",
            _ => "Some object type without format information"
        });

    private static async Task ExecuteAsyncMethods()
    {
        Console.WriteLine("About to launch a task...");
        //_ = Task.Run(() =>
        await Task.Run(() =>
        {
            var iterations = 0;
            //for (int ctr = 0; ctr < int.MaxValue; ctr++)
            for (int ctr = 0; ctr < 100; ctr++)
                iterations++;
            Console.WriteLine("Completed looping operation...");
            //throw new InvalidOperationException();
        });
        Console.WriteLine("Before Task.Delay().");
        await Task.Delay(2000);
        Console.WriteLine("Exiting after 2 second delay");
    }
    // The example displays output like the following:
    //       About to launch a task...
    //       Completed looping operation...
    //       Exiting after 5 second delay
}

public class DiscardTest2
{
    public static void RunTest()
    {
        var (_, _, _, pop1, _, pop2) = QueryCityDataForYears("New York City", 1960, 2010);

        Console.WriteLine($"Population change, 1960 to 2010: {pop2 - pop1:N0}");
    }

    private static (string, double, int, int, int, int) QueryCityDataForYears(string name, int year1, int year2)
    {
        int population1 = 0, population2 = 0;
        double area;

        if (name == "New York City")
        {
            area = 468.48;
            if (year1 == 1960)
            {
                population1 = 7781984;
            }
            if (year2 == 2010)
            {
                population2 = 8175133;
            }
            return (name, area, year1, population1, year2, population2);
        }

        return ("", 0, 0, 0, 0, 0);
    }
}
// The example displays the following output:
//      Population change, 1960 to 2010: 393,149
