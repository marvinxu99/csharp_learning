//
// Virtual interface members:
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members
//
// IAdditionOperators<TSelf, TOther, TResult> interface
//

using System.Numerics;

namespace InterfaceStaticMethod;

public record Translation<T>(T XOffset, T YOffset) : IAdditiveIdentity<Translation<T>, Translation<T>>
    where T : IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
{
    public static Translation<T> AdditiveIdentity =>
        new Translation<T>(XOffset: T.AdditiveIdentity, YOffset: T.AdditiveIdentity);
}

public record Point<T>(T X, T Y) : IAdditionOperators<Point<T>, Translation<T>, Point<T>>
    where T : IAdditionOperators<T, T, T>, IAdditiveIdentity<T, T>
{
    public static Point<T> operator +(Point<T> left, Translation<T> right) =>
        left with { X = left.X + right.XOffset, Y = left.Y + right.YOffset };
}

public class Program
{
    public static void Main()
    {
        var pt = new Point<int>(3, 4);

        var translate = new Translation<int>(5, 10);

        var final = pt + translate;

        Console.WriteLine(pt);
        Console.WriteLine(translate);
        Console.WriteLine(final);
        //OUTPUT:
        //Point { X = 3, Y = 4 }
        //Translation { XOffset = 5, YOffset = 10 }
        //Point { X = 8, Y = 14 }

    }
}



