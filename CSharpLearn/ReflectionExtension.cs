using System.Reflection;

namespace CSharpLearn;

public static class ReflectionExtensions2
{
    public static void InspectMembers(this Type type)
    {
        // Specify binding flags
        BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                             BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

        // Get all members
        MemberInfo[] members = type.GetMembers(flags);
        Console.WriteLine($"Type {type.Name} has {members.Length} members:");

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

public class ReflectionExample2
{
    public static void RunTest()
    {
        // Inspect the Automobile type
        typeof(Automobile).InspectMembers();
    }
}
