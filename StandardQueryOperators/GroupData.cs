// https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/grouping-data

namespace StandardQueryOperators;

internal class GroupData
{
    internal static void RunTest()
    {
        List<int> numbers = [35, 44, 200, 84, 3987, 4, 199, 329, 446, 208];

        IEnumerable<IGrouping<int, int>> query = from number in numbers
                                                 group number by number % 2;

        IEnumerable<IGrouping<int, int>> query2 = numbers
            .GroupBy(number => number % 2);

        foreach (var group in query)
        {
            Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");
            foreach (int i in group)
            {
                Console.WriteLine(i);
            }
        }

        ///////////////////////////////////////////////
        //
        var students = Sources.Students;

        var groupByFirstLetterQuery =
            from student in students
            let firstLetter = student.LastName[0]
            group student by firstLetter;

        var groupByFirstLetterQuery2 = students
            .GroupBy(student => student.LastName[0]);

        foreach (var studentGroup in groupByFirstLetterQuery)
        {
            Console.WriteLine($"Key: {studentGroup.Key}");
            foreach (var student in studentGroup)
            {
                Console.WriteLine($"\t{student.LastName}, {student.FirstName}");
            }
        }

        //////////////////////////////////////////////////
        // Group by a range example
        static int GetPercentile(Student s)
        {
            double avg = s.Scores.Average();
            return avg > 0 ? (int)avg / 10 : 0;
        }

        var groupByPercentileQuery =
            from student in students
            let percentile = GetPercentile(student)
            group new
            {
                student.FirstName,
                student.LastName
            } by percentile into percentGroup
            orderby percentGroup.Key
            select percentGroup;

        var groupByPercentileQuery2 = students
            .Select(student => new { student, percentile = GetPercentile(student) })
            .GroupBy(student => student.percentile)
            .Select(percentGroup => new
            {
                percentGroup.Key,
                Students = percentGroup.Select(s => new { s.student.FirstName, s.student.LastName })
            })
            .OrderBy(percentGroup => percentGroup.Key);

        foreach (var studentGroup in groupByPercentileQuery)
        {
            Console.WriteLine($"Key: {studentGroup.Key * 10}");
            foreach (var item in studentGroup)
            {
                Console.WriteLine($"\t{item.LastName}, {item.FirstName}");
            }
        }

        // Group by comparison example
        var groupByHighAverageQuery =
            from student in students
            group new
            {
                student.FirstName,
                student.LastName
            } by student.Scores.Average() > 75 into studentGroup
            select studentGroup;

        var groupByHighAverageQuery2 = students
            .GroupBy(student => student.Scores.Average() > 75)
            .Select(group => new
            {
                group.Key,
                Students = group.AsEnumerable().Select(s => new { s.FirstName, s.LastName })
            });

        foreach (var studentGroup in groupByHighAverageQuery)
        {
            Console.WriteLine($"Key: {studentGroup.Key}");
            foreach (var student in studentGroup)
            {
                Console.WriteLine($"\t{student.FirstName} {student.LastName}");
            }
        }

        // Group by anonymous type
        var groupByCompoundKey =
            from student in students
            group student by new
            {
                FirstLetterOfLastName = student.LastName[0],
                IsScoreOver85 = student.Scores[0] > 85
            } into studentGroup
            orderby studentGroup.Key.FirstLetterOfLastName
            select studentGroup;

        var groupByCompoundKey2 = students
            .GroupBy(student => new
            {
                FirstLetterOfLastName = student.LastName[0],
                IsScoreOver85 = student.Scores[0] > 85
            })
            .OrderBy(studentGroup => studentGroup.Key.FirstLetterOfLastName);

        foreach (var scoreGroup in groupByCompoundKey)
        {
            var s = scoreGroup.Key.IsScoreOver85 ? "more than 85" : "less than 85";
            Console.WriteLine($"Name starts with {scoreGroup.Key.FirstLetterOfLastName} who scored {s}");
            foreach (var item in scoreGroup)
            {
                Console.WriteLine($"\t{item.FirstName} {item.LastName}");
            }
        }

    }
}
