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
                // Here, we are using reflection to access the properties of an anonymous type. 
                // The anonymous type was created by the LINQ query in the CharIndexer method, 
                // and it has properties named "Letter" and "Count". Because the type is anonymous, 
                // we don't know its name at compile time, so we can't access its properties directly 
                // in the usual way (i.e., c.Letter and c.Count). Instead, we use reflection to access 
                // the properties. We get the Type of the object (c.GetType()), then we get the PropertyInfo 
                // for each property (GetProperty("Letter") and GetProperty("Count")), and finally we get 
                // the value of each property for the object (GetValue(c, null)).
                //
                // Note that while reflection can be slower and more compute-heavy than direct code operations, 
                // the performance impact in this specific case is likely to be negligible, because we're only 
                // using reflection to access a couple of properties of a small number of objects.
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

        // Remember IEnumerable is an interface that defines one method, 'GetEnumerator', which returns an IEnumerator. This allows the objects of the implementing class to be iterated over in a foreach loop. When we say IEnumerable<object>, we're saying that we have a collection of objects that can be iterated over. We designate <object> because we're dealing with anonymous types

        // GroupBy(c => c) is a Linq method that groups elements of a collection based on a key. The key is defined by the lambda expression. In this case, 'c => c' means we're grouping by the character itself. So all instances of a particular character in the string will be grouped together. 

        // Select is a method that projects each element of a sequence into a new form. In this case, we're transforming each group 'g' into a new anonymous object with two properties: "Letter" and "Count". Here, g stands for group and it represents each group of characters created by the groupby method. 'g.Key' gives us the character that was used to group the elements, and 'g.Count()' gives us the number of elements in a group.
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


