namespace CSharpLearn;

static class Constants
{
    public const double Pi = 3.14159;
    public const int SpeedOfLight = 300000; // km per sec.
}

class ConstantsTest
{
    public static void RunTest()
    {
        double radius = 5.3;
        double area = Constants.Pi * (radius * radius);
        Console.WriteLine($"Calculated area = {area:F2}");

        int secsFromSun = 149476000 / Constants.SpeedOfLight; // in km
        Console.WriteLine($"It takes {secsFromSun} seconds for light to travel from the Sun to the Earth.");
    }
}