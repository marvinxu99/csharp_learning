
//WorkWithIntegers();

WorkWithDecimals();


void WorkWithDecimals()
{
    decimal min = decimal.MinValue;
    decimal max = decimal.MaxValue;
    Console.WriteLine($"The range of the decimal type is {min} to {max}");

    double a = 1.0;
    double b = 3.0;
    Console.WriteLine(a / b);

    decimal c = 1.0M;
    decimal d = 3.0M;
    Console.WriteLine(c / d);

    double radius = 2.50;
    double area = Math.PI * radius * radius;
    Console.WriteLine(area);
}

void WorkWithDoubles()
{
    double a = 5;
    double b = 4;
    double c = 2;
    double d = (a + b) / c;
    Console.WriteLine(d);
    double e = 19;
    double f = 23;
    double g = 8;
    double h = (e + f) / g;
    Console.WriteLine(h);
    double max = double.MaxValue;
    double min = double.MinValue;
    Console.WriteLine($"The range of double is {min} to {max}");
    double third = 1.0 / 3.0;
    Console.WriteLine(third);
}

void WorkWithInts()
{
    int max = int.MaxValue;
    int min = int.MinValue;
    Console.WriteLine($"The range of integers is {min} to {max}");

    long max1 = long.MaxValue;
    long min1 = long.MinValue;
    Console.WriteLine($"The range of long is {min1} to {max1}");

    double max2 = double.MaxValue;
    double min2 = double.MinValue;
    Console.WriteLine($"The range of double is {min2} to {max2}");


    int what = max + 3;
    Console.WriteLine($"An example of overflow: {what}");

}

void WorkWithIntegers()
{
    int a = 18;
    int b = 6;
    int c = a + b;
    Console.WriteLine(c);


    // subtraction
    c = a - b;
    Console.WriteLine(c);

    // multiplication
    c = a * b;
    Console.WriteLine(c);

    // division
    c = a / b;
    Console.WriteLine(c);
}