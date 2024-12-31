namespace CSharpLearn;

public class YieldStatementExample
{
    public static void RunTest()
    {
        foreach (int i in ProduceEvenNumbers(9))
        {
            Console.Write(i);
            Console.Write(" ");
        }
        // Output: 0 2 4 6 8

        static IEnumerable<int> ProduceEvenNumbers(int upto)
        {
            for (int i = 0; i <= upto; i += 2)
            {
                yield return i;
            }
        }

        Console.WriteLine("\n Yield Break Test:");
        YieldBreakTest();
    }

    public static void YieldBreakTest()
    {
        Console.WriteLine(string.Join(" ", TakeWhilePositive([2, 3, 4, 5, -1, 3, 4])));
        // Output: 2 3 4 5

        Console.WriteLine(string.Join(" ", TakeWhilePositive([9, 8, 7])));
        // Output: 9 8 7

        static IEnumerable<int> TakeWhilePositive(IEnumerable<int> numbers)
        {
            foreach (int n in numbers)
            {
                if (n > 0)
                {
                    yield return n;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
