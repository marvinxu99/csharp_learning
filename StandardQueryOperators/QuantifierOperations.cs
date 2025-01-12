namespace StandardQueryOperators;

internal class QuantifierOperations
{
    internal static void RunTest()
    {
        var students = Sources.Students;

        IEnumerable<string> names = from student in students
                                    where student.Scores.All(score => score > 70)
                                    select $"{student.FirstName} {student.LastName}: {string.Join(", ", student.Scores.Select(s => s.ToString()))}";

        foreach (string name in names)
        {
            Console.WriteLine($"{name}");
        }

        // Method
        IEnumerable<string> names2 = students
            .Where(student => student.Scores.All(score => score > 70))
            .Select(student => $"{student.FirstName} {student.LastName}: {string.Join(", ", student.Scores.Select(s => s.ToString()))}");
        foreach (string name in names2)
        {
            Console.WriteLine($"{name}");
        }

    }
}
