namespace FileOpExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileUsingStatementExample.RunTest();

            var filePath = @"E:\eDev\HGW\csharp\learning\FileOpExamples\numbers.txt";
            var numbers = FileUsingStatementExample.LoadNumbers(filePath);
            Console.WriteLine(string.Join(", ", numbers));

        }
    }
}
