﻿// https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/query-expression-basics

namespace ConsoleLINQ;

record City(string Name, long Population);
record Country(string Name, double Area, long Population, List<City> Cities);
record Product(string Name, string Category);

internal class QueryExpressionBasics
{
    internal static void RunTest()
    {
        //Query syntax
        //IEnumerable<City> queryMajorCities =
        var queryMajorCities =
            from city in cities
            where city.Population > 100000
            orderby city.Name
            select city.Name;

        // Execute the query to produce the results
        foreach (var city in queryMajorCities)
        {
            Console.WriteLine(city);
        }

        // Method-based syntax
        Console.WriteLine("----Method-based syntax----");
        //IEnumerable<City> queryMajorCities2 = cities.Where(c => c.Population > 100000 && c.Name == "Tokyo").OrderBy(c => c.Name);
        var queryMajorCities2 = cities
            .Where(c => c.Population > 100000 && c.Name == "Tokyo")
            .OrderBy(c => c.Name)
            .Select(c => c.Name);

        // Execute the query to produce the results
        foreach (var city in queryMajorCities2)
        {
            Console.WriteLine(city);
        }

        // Grouping example
        Console.WriteLine("\n----Grouping by first letter of country name----");
        var queryCountryGroups =
            from country in countries
            group country by country.Name[0] into countryGroup
            orderby countryGroup.Key
            select countryGroup;

        foreach (var group in queryCountryGroups)
        {
            Console.WriteLine($"Countries starting with '{group.Key}':");
            foreach (var country in group)
            {
                Console.WriteLine($"  {country.Name}");
            }
        }

        Console.WriteLine("\n----Grouping by first letter of country name----");
        var queryCountryGroups2 =
            from country in countries
            group country by country.Name[0];

        foreach (var group in queryCountryGroups2)
        {
            Console.WriteLine($"Countries starting with '{group.Key}':");
            foreach (var country in group)
            {
                Console.WriteLine($"  {country.Name}");
            }

        }

        // select clause
        Console.WriteLine("select clause projects a sequence of anonymous types:");
        var queryNameAndPop =
            from country in countries
            select new
            {
                Name = country.Name,
                Pop = country.Population
            };
        foreach (var country in queryNameAndPop)
        {
            Console.WriteLine(country.ToString());
        }

        // percentileQuery is an IEnumerable<IGrouping<int, Country>>
        Console.WriteLine("---Continuation with 'into':");
        var percentileQuery =
            from country in countries
            let percentile = (int)country.Population / 1000
            group country by percentile into countryGroup
            where countryGroup.Key >= 20
            orderby countryGroup.Key
            select countryGroup;

        // grouping is an IGrouping<int, Country>
        foreach (var group in percentileQuery)
        {
            Console.WriteLine($"Group {group.Key}:");
            foreach (var country in group)
            {
                Console.WriteLine(country.Name + ":" + country.Population);
            }
        }

        // Filtering, ordering, and joining
        var querySortedCountries =
            from country in countries
            orderby country.Area ascending, country.Population descending
            select country;


        // let clause
        string[] names = ["Svetlana Omelchenko", "Claire O'Donnell", "Sven Mortensen", "Cesar Garcia"];

        IEnumerable<string> queryFirstNames =
            from name in names
            let firstName = name.Split(' ')[0]
            select firstName;

        foreach (var s in queryFirstNames)
        {
            Console.Write(s + " ");
        }

        //Output: Svetlana Claire Sven Cesar
    }

    static readonly City[] cities = [
        new City(Name: "Tokyo", Population: 37_833_000),
        new City("Delhi", 30_290_000),
        new City("Shanghai", 27_110_000),
        new City("São Paulo", 22_043_000),
        new City("Mumbai", 20_412_000),
        new City("Beijing", 20_384_000),
        new City("Cairo", 18_772_000),
        new City("Dhaka", 17_598_000),
        new City("Osaka", 19_281_000),
        new City("New York-Newark", 18_604_000),
        new City("Karachi", 16_094_000),
        new City("Chongqing", 15_872_000),
        new City("Istanbul", 15_029_000),
        new City("Buenos Aires", 15_024_000),
        new City("Kolkata", 14_850_000),
        new City("Lagos", 14_368_000),
        new City("Kinshasa", 14_342_000),
        new City("Manila", 13_923_000),
        new City("Rio de Janeiro", 13_374_000),
        new City("Tianjin", 13_215_000)
    ];

    static readonly Country[] countries = [
        new Country (Name: "Vatican City", Area: 0.44, Population: 526, Cities: [new City("Vatican City", 826)]),
        new Country ("Monaco", 2.02, 38_000, [new City("Monte Carlo", 38_000)]),
        new Country ("Nauru", 21, 10_900, [new City("Yaren", 1_100)]),
        new Country ("Tuvalu", 26, 11_600, [new City("Funafuti", 6_200)]),
        new Country ("San Marino", 61, 33_900, [new City("San Marino", 4_500)]),
        new Country ("Liechtenstein", 160, 38_000, [new City("Vaduz", 5_200)]),
        new Country ("Marshall Islands", 181, 58_000, [new City("Majuro", 28_000)]),
        new Country ("Saint Kitts & Nevis", 261, 53_000, [new City("Basseterre", 13_000)])
    ];
}
