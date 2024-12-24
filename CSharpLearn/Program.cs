using System.Text;

namespace CSharpLearn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            StringBuilder builder = new();
            builder.AppendLine("The following arguments are passed:");

            // Display the command line arguments using the args variable.
            foreach (var arg in args)
            {
                builder.AppendLine($"Argument={arg}");
            }

            Console.WriteLine(builder.ToString());

        }
    }
}
