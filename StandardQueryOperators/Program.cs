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
        // The equivalent method approach
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
        var zipped2 = numbers.Zip(letters, emoji);
        foreach (var item in zipped2)
        {
            Console.WriteLine(item);
        }

        /////////////////////////////////////
        // Let clause example
        //LetSample1.RunTest();



    }
}
