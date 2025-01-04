namespace ConsoleLINQ;

internal class FaroShuffle
{
    internal static void RunTest()
    {
        //var startingDeck2 = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));
        var startingDeck = (from s in Suits()
                            from r in Ranks()
                            select new { Suit = s, Rank = r })
                            .LogQuery("Starting Deck")
                            .ToArray();


        // Display each card that we've generated and placed in startingDeck in the console
        foreach (var card in startingDeck)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine();

        // Shuffling using InterleaveSequenceWith<T>();
        // 52 cards in a deck, so 52 / 2 = 26
        var top = startingDeck.Take(26);
        var bottom = startingDeck.Skip(26);
        var shuffle = top.InterleaveSequenceWith(bottom);
        foreach (var c in shuffle)
        {
            Console.WriteLine(c);
        }

        Console.WriteLine();
        var times = 0;
        shuffle = startingDeck;
        do
        {
            // Out shuffle
            shuffle = shuffle.Take(26).LogQuery("c")
                .InterleaveSequenceWith(shuffle.Skip(26).LogQuery("Bottom Half"))
                .LogQuery("Shuffle")
                .ToArray();

            // In shuffle
            /*
            shuffle = shuffle.Skip(26).LogQuery("Bottom Half")
                .InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top Half"))
                .LogQuery("Shuffle")
                .ToArray();
            */

            foreach (var card in shuffle)
            {
                Console.WriteLine(card);
            }
            Console.WriteLine();
            times++;

        } while (!startingDeck.SequenceEquals(shuffle));

        Console.WriteLine(times);

    }

    static IEnumerable<string> Suits()
    {
        yield return "clubs";
        yield return "diamonds";
        yield return "hearts";
        yield return "spades";
    }

    static IEnumerable<string> Ranks()
    {
        yield return "two";
        yield return "three";
        yield return "four";
        yield return "five";
        yield return "six";
        yield return "seven";
        yield return "eight";
        yield return "nine";
        yield return "ten";
        yield return "jack";
        yield return "queen";
        yield return "king";
        yield return "ace";
    }
}
