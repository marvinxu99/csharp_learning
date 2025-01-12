using System.Xml.Linq;
namespace StandardQueryOperators;

internal class Program
{
    static void Main()
    {
        var students = Sources.Students;
        var departments = Sources.Departments;
        var teachers = Sources.Teachers;

        //////////////////////////////////////
        // to XML
        var studentsToXML = new XElement("Root",
            from student in students
            let scores = string.Join(",", student.Scores)
            select new XElement("student",
                        new XElement("First", student.FirstName),
                        new XElement("Last", student.LastName),
                        new XElement("Scores", scores)
                    ) // end "student"
                ); // end "Root"

        // Execute the query.
        Console.WriteLine(studentsToXML);

        ////////////////////////////////////////////////////
        // Use the results of one query as the data source for a subsequent query
        var orderedQuery = from department in departments
                           join student in students on department.ID equals student.DepartmentID into studentGroup
                           orderby department.Name
                           select new
                           {
                               DepartmentName = department.Name,
                               Students = from student in studentGroup
                                          orderby student.LastName
                                          select student
                           };

        foreach (var departmentList in orderedQuery)
        {
            Console.WriteLine(departmentList.DepartmentName);
            foreach (var student in departmentList.Students)
            {
                Console.WriteLine($"  {student.LastName,-10} {student.FirstName,-10}");
            }
        }

        // Query method
        var orderedQuery2 = departments
            .GroupJoin(students, department => department.ID, student => student.DepartmentID,
                (department, studentGroup) => new
                {
                    DepartmentName = department.Name,
                    Students = studentGroup.OrderBy(student => student.LastName)
                }
            )
            .OrderBy(department => department.DepartmentName);


        foreach (var departmentList in orderedQuery2)
        {
            Console.WriteLine(departmentList.DepartmentName);
            foreach (var student in departmentList.Students)
            {
                Console.WriteLine($"  {student.LastName,-10} {student.FirstName,-10}");
            }
        }

        // Select Many
        List<string> phrases = ["an apple a day", "the quick brown fox"];
        var query = from phrase in phrases
                    from word in phrase.Split(' ')
                    select word;
        foreach (string s in query)
        {
            Console.WriteLine(s);
        }
        // Method approach
        List<string> phrases2 = ["an apple a day", "the quick brown fox"];
        var query2 = phrases2.SelectMany(phrase => phrase.Split(' '));
        foreach (string s in query2)
        {
            Console.WriteLine(s);
        }

        IEnumerable<int> numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        IEnumerable<char> letters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'];

        var method = numbers
            .SelectMany(number => letters,
                (number, letter) => (number, letter)
            );

        foreach (var item in method)
        {
            Console.WriteLine(item);
        }

        ////////////////////////////////////
        // Zip example
        foreach ((int number, char letter) in numbers.Zip(letters))
        {
            Console.WriteLine($"Number: {number} zipped with letter: '{letter}'");
        }

        var zipped = numbers.Zip(letters);
        foreach (var item in zipped)
        {
            Console.WriteLine(item);
        }

        // A string array with 8 elements.
        IEnumerable<string> emoji = ["🤓", "🔥", "🎉", "👀", "⭐", "💜", "✔", "💯"];

        foreach ((int number, char letter, string em) in numbers.Zip(letters, emoji))
        {
            Console.WriteLine(
                $"Number: {number} is zipped with letter: '{letter}' and emoji: {em}");
        }

        // Use the Enumerable.Zip<TFirst,TSecond,TThird>(IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>) 
        var zipped2 = numbers.Zip(letters, emoji);
        foreach (var item in zipped2)
        {
            Console.WriteLine(item);
        }

        // Zip - The third overload accepts a Func<TFirst, TSecond, TResult> argument that acts as a results selector
        foreach (string rslt in
            numbers.Zip(letters, (number, letter) => $"{number} = {letter} ({(int)letter})"))
        {
            Console.WriteLine(rslt);
        }

        /////////////////////////////////////
        // Let clause example
        //LetSample1.RunTest();

        ////////////////////////////////////
        // Select Vs SelectMany
        //SelectVsSelectMany.RunTest();

        ////////////////////////////////////
        // https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/set-operations
        // Set Operations: Distinct/DistinctBy, Except/ExceptBy, Intersect/IntersectBy, Union/UnionBy

        ///////////////////////////////////
        // Sort Data - https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/sorting-data
        // OrderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse

        ////////////////////////////////////
        // Quantifier operations - https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/quantifier-operations
        // All, Any, Contains
        //QuantifierOperations.RunTest();


        //////////////////////////////////////////
        // Partitioning data - Dividing an input sequence into two sections, without rearranging the elements,
        // and then returning one of the sections.
        // Skip, SkipWhile, Take. TakeWhile, Chunk
        foreach (int number in Enumerable.Range(0, 8).Take(3))
        {
            Console.Write($"{number} ");
        }
        Console.WriteLine();

        foreach (int number in Enumerable.Range(0, 8).Skip(3))
        {
            Console.Write($"{number} ");
        }
        Console.WriteLine();

        foreach (int number in Enumerable.Range(0, 8).TakeWhile(n => n < 5))
        {
            Console.Write($"{number} ");
        }
        Console.WriteLine();

        /////////////////////////////////////////////////
        // Converting data types
        // AsEnumerable, AsQuerable, Cast, OfType, ToArray, ToDictionary, ToList, ToLookup
        IEnumerable<Student> people = students;

        var query3 = from Student student in students
                     where student.Year == GradeLevel.ThirdYear
                     select student;

        var query4 = people
            .Cast<Student>()
            .Where(student => student.Year == GradeLevel.ThirdYear);

        foreach (Student student in query3)
        {
            Console.WriteLine(student.FirstName);
        }

        // Join Examples
        JoinExamples.RunTest();

    }
}
