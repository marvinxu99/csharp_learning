// https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/string-interpolation

namespace StringInterpolationTest;

internal class Program
{
    static void Main()
    {
        double a = 3;
        double b = 4;
        Console.WriteLine($"Area of the right triangle with legs of {a} and {b} is {0.5 * a * b}");
        Console.WriteLine($"Length of the hypotenuse of the right triangle with legs of {a} and {b} is {CalculateHypotenuse(a, b)}");
        static double CalculateHypotenuse(double leg1, double leg2) => Math.Sqrt(leg1 * leg1 + leg2 * leg2);
        // Output:
        // Area of the right triangle with legs of 3 and 4 is 6
        // Length of the hypotenuse of the right triangle with legs of 3 and 4 is 5

        // Specify a format string for an interpolation expression
        // {<interpolationExpression>:<formatString>}
        var date = new DateTime(1731, 11, 25);
        Console.WriteLine($"On {date:dddd, MMMM dd, yyyy} L. Euler introduced the letter e to denote {Math.E:F5}.");
        // Output:
        // On Sunday, November 25, 1731 L. Euler introduced the letter e to denote 2.71828.

        // Control the field width and alignment
        // {<interpolationExpression>,<alignment>:<formatString>}
        const int NameAlignment = -9;
        const int ValueAlignment = 7;
        a = 3;
        b = 4;
        Console.WriteLine($"Three classical Pythagorean means of {a} and {b}:");
        Console.WriteLine($"|{"Arithmetic",NameAlignment}|{0.5 * (a + b),ValueAlignment:F3}|");
        Console.WriteLine($"|{"Geometric",NameAlignment}|{Math.Sqrt(a * b),ValueAlignment:F3}|");
        Console.WriteLine($"|{"Harmonic",NameAlignment}|{2 / (1 / a + 1 / b),ValueAlignment:F3}|");
        // Output:
        // Three classical Pythagorean means of 3 and 4:
        // |Arithmetic|  3.500|
        // |Geometric|  3.464|
        // |Harmonic |  3.429|

        // Use a ternary conditional operator ?: in an interpolation expression
        var rand = new Random();
        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine($"Coin flip: {(rand.NextDouble() < 0.5 ? "heads" : "tails")}");
        }

        var cultures = new System.Globalization.CultureInfo[]
        {
            System.Globalization.CultureInfo.GetCultureInfo("en-US"),
            System.Globalization.CultureInfo.GetCultureInfo("en-GB"),
            System.Globalization.CultureInfo.GetCultureInfo("nl-NL"),
            System.Globalization.CultureInfo.GetCultureInfo("en-CA"),
            System.Globalization.CultureInfo.InvariantCulture
        };
        //date = DateTime.Now;
        date = new DateTime(2023, 8, 23);
        var number = 31_415_926.536;
        foreach (var culture in cultures)
        {
            var cultureSpecificMessage = string.Create(culture, $"{date,24}{number,20:N3}");
            Console.WriteLine($"{culture.Name,-10}{cultureSpecificMessage}");
        }
        // Output is similar to:
        //en - US       8 / 23 / 2023 12:00:00 AM      31,415,926.536
        //en - GB         23 / 08 / 2023 00:00:00      31,415,926.536
        //nl - NL         23 - 08 - 2023 00:00:00      31.415.926,536
        //en - CA     2023 - 08 - 23 12:00:00 a.m.     31,415,926.536
        //                08 / 23 / 2023 00:00:00      31,415,926.536

    }
}
