using System.Reflection;

namespace CSharpLearn;

public class SimpleClass { }

public class Automobile(string make, string model, int year)
{
    public string Make { get; } = !string.IsNullOrWhiteSpace(make)
        ? make
        : throw new ArgumentException("The make cannot be null, empty, or whitespace.", nameof(make));

    public string Model { get; } = !string.IsNullOrWhiteSpace(model)
        ? model
        : throw new ArgumentException("The model cannot be null, empty, or whitespace.", nameof(model));

    public int Year { get; } = (year >= 1857 && year <= DateTime.Now.Year + 2)
        ? year
        : throw new ArgumentException("The year is out of range.");

    public override string ToString() => $"{Year} {Make} {Model}";
}


public class ReflectionExample
{
    public static void RunTest()
    {
        //Type t = typeof(SimpleClass);
        Type t = typeof(Automobile);

        BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                             BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

        MemberInfo[] members = t.GetMembers(flags);
        Console.WriteLine($"Type {t.Name} has {members.Length} members: ");

        foreach (MemberInfo member in members)
        {
            string access = "";
            string stat = "";

            if (member is MethodBase method)
            {
                access = method.IsPublic ? "Public" :
                         method.IsPrivate ? "Private" :
                         method.IsFamily ? "Protected" :
                         method.IsAssembly ? "Internal" :
                         method.IsFamilyOrAssembly ? "Protected Internal" : "Unknown";

                stat = method.IsStatic ? "Static" : "Instance";
            }
            else if (member is FieldInfo field)
            {
                access = field.IsPublic ? "Public" :
                         field.IsPrivate ? "Private" :
                         field.IsFamily ? "Protected" :
                         field.IsAssembly ? "Internal" :
                         field.IsFamilyOrAssembly ? "Protected Internal" : "Unknown";

                stat = field.IsStatic ? "Static" : "Instance";
            }
            else if (member is PropertyInfo property)
            {
                MethodInfo? getMethod = property.GetMethod;
                access = getMethod?.IsPublic == true ? "Public" :
                         getMethod?.IsPrivate == true ? "Private" :
                         getMethod?.IsFamily == true ? "Protected" :
                         getMethod?.IsAssembly == true ? "Internal" :
                         getMethod?.IsFamilyOrAssembly == true ? "Protected Internal" : "Unknown";

                stat = getMethod?.IsStatic == true ? "Static" : "Instance";
            }

            string output = $"{member.Name,-20} ({member.MemberType,-10}): {access,-20}{stat,-10}, Declared by {member.DeclaringType}";
            Console.WriteLine(output);
        }
    }
}
// The example displays the following output:
//	Type SimpleClass has 9 members:
//	ToString (Method):  Public, Declared by System.Object
//	Equals (Method):  Public, Declared by System.Object
//	Equals (Method):  Public Static, Declared by System.Object
//	ReferenceEquals (Method):  Public Static, Declared by System.Object
//	GetHashCode (Method):  Public, Declared by System.Object
//	GetType (Method):  Public, Declared by System.Object
//	Finalize (Method):  Internal, Declared by System.Object
//	MemberwiseClone (Method):  Internal, Declared by System.Object
//	.ctor (Constructor):  Public, Declared by SimpleClass