namespace StandardQueryOperators;

internal class LetSample1
{
    internal static void RunTest()
    {
        string[] strings =
        [
            "A penny saved is a penny earned.",
            "The early bird catches the worm.",
            "The pen is mightier than the sword."
        ];

        // Split the sentence into an array of words
        // and select those whose first letter is a vowel.
        var earlyBirdQuery =
            from sentence in strings
            let words = sentence.Split(' ')
            from word in words
            let w = word.ToLower()
            where w[0] == 'a' || w[0] == 'e'      // more efficient this way
                || w[0] == 'i' || w[0] == 'o'
                || w[0] == 'u'
            //where "aeiou".Contains(w[0])
            select word;

        // Execute the query.
        foreach (var v in earlyBirdQuery)
        {
            Console.WriteLine("\"{0}\" starts with a vowel", v);
        }
    }
}
