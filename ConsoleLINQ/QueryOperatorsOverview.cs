﻿// https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/

namespace ConsoleLINQ;

/****
 *  Please refer to the StandardQueryOperators project
 */

internal class QueryOperatorsOverview
{
    internal static void RunTest()
    {
        string sentence = "the quick brown fox jumps over the lazy dog";
        // Split the string into individual words to create a collection.
        string[] words = sentence.Split(' ');

        // Using query expression syntax.
        var query = from word in words
                    group word.ToUpper() by word.Length into gr
                    orderby gr.Key
                    select new { Length = gr.Key, Words = gr };

        // Using method-based query syntax.
        var query2 = words.
            GroupBy(w => w.Length, w => w.ToUpper()).
            Select(g => new { Length = g.Key, Words = g }).
            OrderBy(o => o.Length);

        foreach (var obj in query2)
        {
            Console.WriteLine("Words of length {0}:", obj.Length);
            foreach (string word in obj.Words)
                Console.WriteLine(word);
        }
    }
}
