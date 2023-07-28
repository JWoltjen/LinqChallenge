using System;
using System.Text.RegularExpressions;

namespace LinqChallenge
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            string words = "Create a LINQ query that takes in a string and returns a list of all the letters in order, regardless of case";

            string moreWords = "Modify the LINQ query to return a list of anonymous objects, each with a Letter property and a Count property. Populate the count with the number of times a letter is used. Order the list by letter count(max first) and then by character.";

            var indexedChars = CharIndexer(moreWords);
            foreach(var c in indexedChars)
            {
                {
                    var letter = c.GetType().GetProperty("Letter").GetValue(c, null);
                    var count = c.GetType().GetProperty("Count").GetValue(c, null);
                    Console.WriteLine($"Letter: {letter}, Count: {count}");
                }
            }
            Console.ReadLine();
        }
        static List<char> CharSorter(string words)
        {
            var sortedChars = words 
                    .ToCharArray()
                    .Where(char.IsLetter)
                    .OrderBy(char.ToLower)
                    .ToList();
            return sortedChars;
        }

        static IEnumerable<object> CharIndexer(string words)
        {
            var indexedChars = words
                .ToLower()
                .ToCharArray()
                .Where(char.IsLetter)
                .GroupBy(c => c)
                .Select(g => new { Letter = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.Letter)
                .ToList();

            return indexedChars;
        }
    }
}


