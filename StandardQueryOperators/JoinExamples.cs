// https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/join-operations

namespace StandardQueryOperators;

internal class JoinExamples
{
    internal static void RunTest()
    {
        var students = Sources.Students;
        var departments = Sources.Departments;
        var teachers = Sources.Teachers;

        ///////////////////////////////////////////////////
        // Join operations
        // Join, GroupJoin
        //var queryJoin = from student in students
        //                join department in departments on student.DepartmentID equals department.ID
        //                select new { Name = $"{student.FirstName} {student.LastName}", DepartmentName = department.Name };

        var queryJoin = students.Join(departments,
            student => student.DepartmentID,
            department => department.ID,
            (student, department) => new { Name = $"{student.FirstName} {student.LastName}", DepartmentName = department.Name });

        foreach (var item in queryJoin)
        {
            Console.WriteLine($"{item.Name} - {item.DepartmentName}");
        }

        // Group by
        //var queryJoin2 = from student in students
        //                 join department in departments on student.DepartmentID equals department.ID
        //                 group student by department.Name into deptGroup
        //                 orderby deptGroup.Key
        //                 select new { Department = deptGroup.Key, Students = deptGroup.ToList() };

        //foreach (var dept in queryJoin2)
        //{
        //    Console.WriteLine(dept.Department);
        //    foreach (var student in dept.Students)
        //    {
        //        Console.WriteLine("    " + student.FirstName + " " + student.LastName);
        //    }
        //}

        IEnumerable<IEnumerable<Student>> studentGroups = from department in departments
                                                          join student in students on department.ID equals student.DepartmentID into studentGroup
                                                          select studentGroup;

        foreach (IEnumerable<Student> studentGroup in studentGroups)
        {
            Console.WriteLine("Group");
            foreach (Student student in studentGroup)
            {
                Console.WriteLine($"  - {student.FirstName}, {student.LastName}");
            }
        }

        // GroupJoin
        // Join department and student based on DepartmentId and grouping result
        IEnumerable<IEnumerable<Student>> studentGroups2 = departments.GroupJoin(students,
            department => department.ID, student => student.DepartmentID,
            (department, studentGroup) => studentGroup);

        foreach (IEnumerable<Student> studentGroup in studentGroups2)
        {
            Console.WriteLine("Group");
            foreach (Student student in studentGroup)
            {
                Console.WriteLine($"  - {student.FirstName}, {student.LastName}");
            }
        }

        // Composite key join
        // Join the two data sources based on a composite key consisting of first and last name,
        // to determine which employees are also students.
        IEnumerable<string> query10 =
            from teacher in teachers
            join student in students on new
            {
                FirstName = teacher.First,
                LastName = teacher.Last
            } equals new
            {
                student.FirstName,
                student.LastName
            }
            select teacher.First + " " + teacher.Last;

        IEnumerable<string> query11 = teachers
            .Join(students,
                teacher => new { FirstName = teacher.First, LastName = teacher.Last },
                student => new { student.FirstName, student.LastName },
                (teacher, student) => $"{teacher.First} {teacher.Last}"
            );

        string result = "The following people are both teachers and students:\r\n";
        foreach (string name in query10)
        {
            result += $"{name}\r\n";
        }
        Console.Write(result);

        // The first join matches Department.ID and Student.DepartmentID from the list of students and
        // departments, based on a common ID. The second join matches teachers who lead departments
        // with the students studying in that department.
        var query12 = from student in students
                      join department in departments on student.DepartmentID equals department.ID
                      join teacher in teachers on department.TeacherID equals teacher.ID
                      select new
                      {
                          StudentName = $"{student.FirstName} {student.LastName}",
                          DepartmentName = department.Name,
                          TeacherName = $"{teacher.First} {teacher.Last}"
                      };

        var query13 = students
           .Join(departments, student => student.DepartmentID, department => department.ID,
               (student, department) => new { student, department })
           .Join(teachers, commonDepartment => commonDepartment.department.TeacherID, teacher => teacher.ID,
               (commonDepartment, teacher) => new
               {
                   StudentName = $"{commonDepartment.student.FirstName} {commonDepartment.student.LastName}",
                   DepartmentName = commonDepartment.department.Name,
                   TeacherName = $"{teacher.First} {teacher.Last}"
               });

        foreach (var obj in query12)
        {
            Console.WriteLine($"""The student "{obj.StudentName}" studies in the department run by "{obj.TeacherName}".""");
        }

    }
}
