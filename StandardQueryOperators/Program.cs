using System.Xml.Linq;
namespace StandardQueryOperators;

internal class Program
{
    static void Main()
    {
        //////////////////////////////////////
        // to XML
        var students = Sources.Students;
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

        /////////////////////////////////////
        // Let clause example
        //LetSample1.RunTest();

    }
}
