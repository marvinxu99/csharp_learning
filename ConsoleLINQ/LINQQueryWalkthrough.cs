// https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/walkthrough-writing-queries-linq

namespace ConsoleLINQ;


public record Student(string First, string Last, int ID, int[] Scores);


internal class LINQQueryWalkthrough
{
    internal static void RunTest()
    {
        // Create the query.
        // The first line could also be written as "var studentQuery ="
        //IEnumerable<Student> studentQuery =
        //    from student in students
        //    where student.Scores[0] > 90
        //    select student;

        var studentQuery =
            from student in students
            where student.Scores.Min() >= 80
            let average = student.Scores.Average()
            orderby average
            select new
            {
                student.Last,
                student.ID,
                Average = average,
                Min = student.Scores.Min(),
                Max = student.Scores.Max()
            };

        foreach (var student in studentQuery)
        {
            Console.WriteLine(student.ToString());
        }

        // Group the results
        IEnumerable<IGrouping<char, Student>> studentGroupQuery =
            from student in students
            group student by student.Last[0];

        foreach (IGrouping<char, Student> studentGroup in studentGroupQuery)
        {
            Console.WriteLine(studentGroup.Key);
            foreach (Student student in studentGroup)
            {
                Console.WriteLine($"   {student.Last}, {student.First}");
            }
        }

        // Order the groups by their key value
        Console.WriteLine();
        var studentQuery4 =
            from student in students
            group student by student.Last[0] into studentGroup
            orderby studentGroup.Key
            select studentGroup;

        foreach (var groupOfStudents in studentQuery4)
        {
            Console.WriteLine(groupOfStudents.Key);
            foreach (var student in groupOfStudents)
            {
                Console.WriteLine($"   {student.Last}, {student.First}");
            }
        }

        // This query returns those students whose
        // first test score was higher than their
        // average score.
        var studentQuery5 =
            from student in students
            let totalScore = student.Scores[0] + student.Scores[1] +
                student.Scores[2] + student.Scores[3]
            where totalScore / 4 < student.Scores[0]
            select $"{student.Last}, {student.First}";

        foreach (string s in studentQuery5)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine();


    }

    // Create a data source by using a collection initializer.
    static IEnumerable<Student> students =
    [
        new Student(First: "Svetlana", Last: "Omelchenko", ID: 111, Scores: [97, 92, 81, 60]),
    new Student(First: "Claire",   Last: "O'Donnell",  ID: 112, Scores: [75, 84, 91, 39]),
    new Student(First: "Sven",     Last: "Mortensen",  ID: 113, Scores: [88, 94, 65, 91]),
    new Student(First: "Cesar",    Last: "Garcia",     ID: 114, Scores: [97, 89, 85, 82]),
    new Student(First: "Debra",    Last: "Garcia",     ID: 115, Scores: [35, 72, 91, 70]),
    new Student(First: "Fadi",     Last: "Fakhouri",   ID: 116, Scores: [99, 86, 90, 94]),
    new Student(First: "Hanying",  Last: "Feng",       ID: 117, Scores: [93, 92, 80, 87]),
    new Student(First: "Hugo",     Last: "Garcia",     ID: 118, Scores: [92, 90, 83, 78]),

    new Student("Lance",   "Tucker",      119, [68, 79, 88, 92]),
    new Student("Terry",   "Adams",       120, [99, 82, 81, 79]),
    new Student("Eugene",  "Zabokritski", 121, [96, 85, 91, 60]),
    new Student("Michael", "Tucker",      122, [94, 92, 91, 91])
    ];
}
