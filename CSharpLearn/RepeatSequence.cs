// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members
//
// Static Interface Method:
// The motivating scenario for allowing static methods, including operators, in interfaces is to support
// generic math algorithms.

namespace CSharpLearn;

public interface IGetNext<T> where T : IGetNext<T>
{
    static abstract T operator ++(T other);
}

public struct RepeatSequence : IGetNext<RepeatSequence>
{
    private const char Ch = 'A';
    public string Text = new(Ch, 1);

    public RepeatSequence() { }

    public static RepeatSequence operator ++(RepeatSequence other)
        => other with { Text = other.Text + Ch };

    public override readonly string ToString() => Text;

}

public class RepeatSequenceTest
{
    public static void RunTest()
    {
        var str = new RepeatSequence();

        for (int i = 0; i < 10; i++)
            Console.WriteLine(str++);
    }
}