using System;
using System.Text.RegularExpressions;

namespace LinqChallenge
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            string words = "Create a LINQ query that takes in a string and returns a list of all the letters in order, regardless of case";
            List<char> sortedChars = CharSorter(words);
            foreach(char c in sortedChars)
            {
                Console.WriteLine(c);
            }
            Console.ReadLine();
        }
        static List<char> CharSorter(string words)
        {
            var sortedWords = words
                    .ToCharArray()
                    .Where(char.IsLetter)
                    .OrderBy(char.ToLower)
                    .ToList();
            return sortedWords;
        }
    }
}


