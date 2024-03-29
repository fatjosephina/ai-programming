﻿using System;
using System.Collections.Generic;

namespace PermutationsAndCounting
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] charArray = GetCharArray();

            DisplayPermutations(charArray);

            DisplayOrderedPartitions(charArray);

            DisplayCombinations(charArray);
        }

        // Ask for and receive the 5 characters and save them to an array
        // I can use recursion to help ensure that I get the correct number of characters
        static char[] GetCharArray()
        {
            Console.WriteLine("Please enter 5 characters and then press Enter.");
            char[] charArray = Console.ReadLine().ToCharArray();

            // If the character array does not have a Length of 5, that means the user didn't enter 5 characters, so we want to try again
            if (charArray.Length != 5)
            {
                Console.WriteLine("Incorrect amount of characters.\n");
                return GetCharArray();
            }
            return charArray;
        }

        // This code formats the results and calls the CalculatePermutations method which calculates nPr and the Arrange method which displays all the permutations
        static void DisplayPermutations(char[] charArray)
        {
            Console.WriteLine("~~Permutations~~");
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("-5P" + i + "-");
                int count = CalculatePermutations(5, i);
                Console.WriteLine("Count = " + count);
                Console.Write("S = { ");
                Arrange(charArray, i, "permutation");
                Console.WriteLine("}");
            }
        }

        static void DisplayOrderedPartitions(char[] charArray)
        {
            Console.WriteLine("~~Ordered Partitions~~");
            int count = CalculateOrderedPartitions(5, charArray);
            Console.WriteLine("Count = " + count);
            Console.Write("S = { ");
            Arrange(charArray, 5, "orderedPartition");
            Console.WriteLine("}");
        }

        static void DisplayCombinations(char[] charArray)
        {
            Console.WriteLine("~~Combinations~~");
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine("-5C" + i + "-");
                int count = CalculateCombinations(5, i);
                Console.WriteLine("Count = " + count);
                Console.Write("S = { ");
                Arrange(charArray, i, "combination");
                Console.WriteLine("}");
            }
        }

        // Value array created here is where we will store our permutation
        // Used already array will keep track of which characters have already been added to the value array
        // The HashList keeps track of all the arrangements already created, which is necessary for ordered partitions and combinations
        // Combinations also need an additional value index array which keeps track of the original indices (in the charArray) of the characters added to the value array
        static void Arrange(char[] charArray, int sample, string arrangementType)
        {
            char[] value = new char[sample];
            bool[] usedAlready = new bool[charArray.Length];

            // Call the recursive function
            if (arrangementType == "permutation")
            {
                Permute(charArray, value, usedAlready, 0, sample);
            }
            else if (arrangementType == "orderedPartition")
            {
                HashSet<string> previousArrangements = new HashSet<string>();
                OrderPartition(charArray, value, usedAlready, 0, 5, previousArrangements);
            }
            else if (arrangementType == "combination")
            {
                HashSet<string> previousArrangements = new HashSet<string>();
                int[] valueIndices = new int[sample];
                Combine(charArray, value, usedAlready, valueIndices, 0, sample, previousArrangements);
            }

        }

        // This function writes the permutation to the console and exits when it has created one, otherwise it will continue adding characters to the value string
        static void Permute(char[] charArray, char[] value, bool[] usedAlready, int index, int sample)
        {
            // If this statement is true, then we have formed a complete permutation of r characters and can print it to the console
            if (index == sample)
            {
                Console.Write(new string(value) + " ");
                return;
            }

            // Iterate through all the characters in the character array
            for (int i = 0; i < charArray.Length; i++)
            {
                // If the bool variable at the corresponding place has not been used already, we add to the value array starting from the start position
                // Index variable is always 0 at the start but increases with every recursion
                if (!usedAlready[i])
                {
                    value[index] = charArray[i];
                    // Used already makes sure we don't use the same character from the original array twice
                    usedAlready[i] = true;
                    // Call the function recursively, all values have changed except for charArray and sample
                    Permute(charArray, value, usedAlready, index + 1, sample);
                    // Set used already back to false so we can use the same character again in the next permutation
                    usedAlready[i] = false;
                }
            }
        }

        // Similar to the recursive function for permutations but this time we keep track
        static void OrderPartition(char[] charArray, char[] value, bool[] usedAlready, int index, int sample, HashSet<string> previousPermutations)
        {
            // If this statement is true, then we have formed a complete permutation of r characters and can print it to the console
            if (index == sample)
            {
                string permutation = new string(value);

                // If the ordered partition has already been written, we won't write it
                if (!previousPermutations.Contains(permutation))
                {
                    Console.Write(permutation + " ");

                    // Add it to the hash set
                    previousPermutations.Add(permutation);
                }
                return;
            }

            // Iterate through all the characters in the character array
            for (int i = 0; i < charArray.Length; i++)
            {
                // If the bool variable at the corresponding place has not been used already, we add to the value array starting from the start position
                // Index variable is always 0 at the start but increases with every recursion
                if (!usedAlready[i])
                {
                    value[index] = charArray[i];
                    // Used already makes sure we don't use the same character from the original array twice
                    usedAlready[i] = true;
                    // Call the function recursively, all values have changed except for charArray and sample
                    OrderPartition(charArray, value, usedAlready, index + 1, sample, previousPermutations);
                    // Set used already back to false so we can use the same character again in the next permutation
                    usedAlready[i] = false;
                }
            }
        }

        // We have a bit more logic for the combine helper version, including a method which calculates if a hash set contains all the same characters as a string
        static void Combine(char[] charArray, char[] value, bool[] usedAlready, int[] valueIndices, int index, int sample, HashSet<string> previousCombinations)
        {
            // If this statement is true, then we have formed a complete permutation of r characters and can print it to the console
            if (index == sample)
            {
                string combination = new string(value);

                // This makes the string that we're going to use which combinations have previously been created
                // The reason why we care about indices here and not during ordered partitions is because we want to display all combinations even if they are visually identical to previous ones
                string combinationIndices = "";
                foreach (int j in valueIndices)
                {
                    combinationIndices += j;
                }

                // If the combination has already been written, we won't write it
                if (!HashSetContainsSameCharacters(previousCombinations, combinationIndices))
                {
                    Console.Write(combination + " ");

                    // Add it to the hash set
                    previousCombinations.Add(combinationIndices);
                }
                return;
            }

            // Iterate through all the characters in the character array
            for (int i = index; i < charArray.Length; i++)
            {
                // If the bool variable at the corresponding place has not been used already, we add to the value array starting from the start position
                // Also check if we have enough characters left to form a combination of the correct length
                if (!usedAlready[i] && sample - index <= charArray.Length - i)
                {
                    value[index] = charArray[i];
                    valueIndices[index] = i;
                    // Used already makes sure we don't use the same character from the original array twice
                    usedAlready[i] = true;
                    // Call the function recursively, all values have changed except for charArray and sample
                    Combine(charArray, value, usedAlready, valueIndices, index + 1, sample, previousCombinations);
                    // Set used already back to false so we can use the same character again in the next permutation
                    usedAlready[i] = false;
                }
            }
        }

        // This is a function for checking if a hash set contains a string with all the same characters as a given string
        static bool HashSetContainsSameCharacters(HashSet<string> hashSet, string combination)
        {
            // Create a hash set of characters from the potential combination
            HashSet<char> charSet = new HashSet<char>(combination);

            foreach (string s in hashSet)
            {
                // Create a hash set of characters from the strings in the hash set
                HashSet<char> currentCharSet = new HashSet<char>(s);

                // If the two hash sets have the same number of characters and they are equal, return true
                if (charSet.Count == currentCharSet.Count && charSet.SetEquals(currentCharSet))
                {
                    return true;
                }
            }

            // If we have iterated through all strings in the hash set and haven't found a match, return false
            return false;
        }

        // We can use recursion to calculate factorials, if n = 1 we return it, otherwise we remove 1 with each recursion
        // If n = 0, then we don't use the formula and instead know that the answer will always be 1
        static int CalculateFactorial(int n)
        {
            if (n == 0)
                return 1;
            else if (n == 1)
                return n;
            else
                return n * CalculateFactorial(n - 1);
        }

        // With permutations, we replace the variables in the formula n! / (n-r)!
        static int CalculatePermutations(int n, int r)
        {
            return CalculateFactorial(n) / CalculateFactorial(n - r);
        }

        // For ordered partitions, we need n and the array since we need to check how many characters are repeated
        static int CalculateOrderedPartitions(int n, char[] charArray)
        {
            // Use a dictionary instead of an array because it is more dynamic
            Dictionary<char, int> elementsByKind = new Dictionary<char, int>();
            foreach (char c in charArray)
            {
                if (elementsByKind.ContainsKey(c))
                {
                    // If the dictionary already has the character key, just increase the value
                    elementsByKind[c]++;
                }
                else
                {
                    // If the dictionary doesn't have the character key, we add it to the dictionary and set the value to 1
                    elementsByKind[c] = 1;
                }
            }

            // Create the divisor for the equation n! / ( r1!*r2!....*rk!) and then calculate its value
            int divisor = 1;
            foreach (char d in elementsByKind.Keys)
            {
                divisor *= CalculateFactorial(elementsByKind[d]);
            }
            return CalculateFactorial(n) / divisor;
        }

        // To calculate combinations, we just replace the variables in the formula n! / (n-r)!*r!
        static int CalculateCombinations(int n, int r)
        {
            return CalculateFactorial(n) / (CalculateFactorial(n - r) * CalculateFactorial(r));
        }
    }
}
