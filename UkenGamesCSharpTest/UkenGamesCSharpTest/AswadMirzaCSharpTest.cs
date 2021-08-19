//Program By Aswad Mirza 2021-08-18 for Uken Games QA Engineer challenge
/*
 ### C# Challenge

1. Create a C# script that can open up a text file (files can be found in **src** folder), and tell us the number that has repeated the fewest number of times. Each number is on it's own line and is an integer.
3. If two numbers have the same frequency count, return the smaller of the two numbers.
4. Solve the problem without using LINQ.
5. Output example 1: File: 1.txt, Number: 32, Repetead: 3 times
6. Print the output for all the files in a single run
7. Create a repo on github and commit the files that you've staged in your local repository.
8. Add a detailed instruction on how to setup/launch your project.
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace UkenGamesCSharpTest
{
    class AswadMirzaCSharpTest
    {
        /* 
         In main we go to the text files in the src folder one by one, and store each entry in an array of strings
         that array is then converted into a List of integers which is then sorted 
        the entire list is compared and the lowest repeating number is found, and the data is outputted
        this is repeated for each file.
        
        NOTE: I am going by the assumption that by "lowest repeating number it must occur at least twice
        since you cant say a number occuring once is repetition"

        NOTE: Because this is a console application other methods used in this class have to be static because main is static.
         */

        static void Main(string[] args)
        {
            string path;
            List<int> numbers;
            int frequency = 0;
            int lowestNumber;

            for (int i = 1; i <= 5; i++)
            {
                //Relative path to the src folder and the text files
                path = $"../../../src/{i}.txt";
                string[] lines = File.ReadAllLines(path);
                numbers = ReadNumbersFromString(lines);
                numbers.Sort();
                lowestNumber = CompareEntireList(numbers);
                frequency = FrequencyCount(numbers, lowestNumber);
                Console.WriteLine($"File:{i}.txt, Number:{lowestNumber}, Repeated: {frequency} times");
            }
        }

        //A method to help get a list of numbers from an array of strings

        private static List<int> ReadNumbersFromString(String[] lines)
        {
            List<int> numbers = new List<int>();
            foreach (string line in lines)
            {
                numbers.Add(int.Parse(line));
            }
            return numbers;
        }
        // A method to help count the frequency of one number in the list 
        // Assumes list is already sorted
        private static int FrequencyCount(List<int> numbers, int element)
        {
            int frequency = 0;
            bool continueSearch = true;
            // finds the index of the first occurence of the element within the list
            int index = numbers.IndexOf(element);
            //Will break out of the loop if the element we are looking for does not match the next item or we are at the end of the list
            while (continueSearch && index < numbers.Count)
            {
                if (element == numbers[index])
                {
                    frequency++;
                }
                else
                {
                    continueSearch = false;
                }
                index++;
            }
            return frequency;
        }
        /*
         Method to compare  frequency counts
        if it returns -1, element 1 is less occuring, 
        if it returns 0, element 1 and 2 are the same occurences
        if it returns 1 element 1 is occuring more

        Assumes list is already sorted
         */
        private static int CompareFrequencies(List<int> numbers, int element1, int element2)
        {
            int comparison;
            int element1Frequency = FrequencyCount(numbers, element1);
            int element2Frequency = FrequencyCount(numbers, element2);
            comparison = element1Frequency.CompareTo(element2Frequency);
            switch (comparison)
            {
                case -1:

                    /*
                     A check in case the first number occurs only once, in which case it is not a repeating number
                    and must not be checked
                     */
                    if (element1Frequency == 1)
                    {
                        comparison = 1;
                    }
                    break;
                case 0:
                    break;
                default:
                    /*
                     If the next number is less occuring then our first number, but it only occurs once, disregard it
                     */
                    if (element2Frequency == 1)
                    {
                        comparison = -1;
                    }
                    break;
            }
            return comparison;

        }
        /*
            compares entire list and gets the lowest occuring number
            Assumes list is already sorted
        */
        public static int CompareEntireList(List<int> numbers)
        {
            //By default in a sorted list the lowest number is the first item
            int lowestNumber = numbers[0];
            int index = numbers.LastIndexOf(numbers[0]);
            int nextNumber = numbers[index];
            bool continueSearch = true;
            if (index + 1 < numbers.Count)
            {
                nextNumber = numbers[index + 1];
            }
            else
            {
                continueSearch = false;
            }
            while (continueSearch)
            {
                int comparison = CompareFrequencies(numbers, lowestNumber, nextNumber);
                //First we check if the next number is smaller then the current lowest number, if it is then that number is considered the lowest
                //if (CompareFrequencies(numbers, lowestNumber, nextNumber) == 1) {
                if (comparison == 1)
                {
                    lowestNumber = nextNumber;
                }
                //Then we check if the numbers occurent the same amount of times, in which case we check their value
                //else if (CompareFrequencies(numbers, lowestNumber, nextNumber) == 0)
                else if (comparison == 0)
                {
                    if (lowestNumber > nextNumber)
                    {
                        lowestNumber = nextNumber;
                    }
                }

                // In a sorted list the next unique element is the next index after the last current element
                // so if we want a new next number, lets jump to the number after the current number
                // if there is no number after the last number or the list is at the end we just stop comparing
                index = numbers.LastIndexOf(nextNumber);
                if (index + 1 < numbers.Count)
                {
                    nextNumber = numbers[index + 1];
                }
                else
                {
                    continueSearch = false;
                }
            }
            return lowestNumber;

        }


    }
}
