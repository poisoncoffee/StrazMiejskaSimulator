using System;
using System.Collections.Generic;
using System.Text;

namespace StrazMiejskaSimulator.Utilities
{
    class Helpers
    {

        private static readonly Random getrandom = new Random();

        public static void PrintArray(string[] arrayToPrint)
        {
            foreach (string particularString in arrayToPrint)
            {
                Console.WriteLine(particularString);
            }
        }

        public static void PrintList(List<string> theList)
        {
            foreach (string thestring in theList)
            {
                Console.WriteLine(thestring);
            }
        }

        public static void PrintDictionary(Dictionary<string, string> theDict)
        {
            foreach (KeyValuePair<string, string> pair in theDict)
            {
                Console.WriteLine(pair);
            }
            Console.WriteLine("Dictionary Length: " + theDict.Count);
        }

        public static void PrintNestedDictionary(Dictionary<string, Dictionary<string, string>> theDict)
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> pair in theDict)
            {
                Console.WriteLine(pair);
            }
            Console.WriteLine("Dictionary Length: " + theDict.Count);
        }

        public static void PrintListDictionary(Dictionary<string, List<string>> theDict)
        {
            foreach (KeyValuePair<string, List<string>> pair in theDict)
            {
                foreach (string childString in pair.Value)
                {
                    Console.WriteLine(pair.Key + " : " + childString);
                }
            }
            Console.WriteLine("Dictionary Length: " + theDict.Count);
        }

        public static int GetRandomValue(int min, int max)
        {
            {
                lock (getrandom) 
                {
                    return getrandom.Next(min, max);
                }
            }
        }


    }
}
