//Program By Aswad Mirza 2021-08-17 for Uken Games QA Engineer challenge
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
    class Program
    {
        static void Main(string[] args)
        {

            //Bad practice because this is a local path, you want a relative path to the directory
            //string path = @"C:\Users\LordD\Documents\GitHub\UkenGamesCSharpTest\UkenGamesCSharpTest\UkenGamesCSharpTest\src\1.txt";
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //string path = "src/1.txt";
            //string path = Environment.CurrentDirectory;
            string path = "../../../src/1.txt";
            List<int> numbers;
            int frequency = 0;
            int lowestNumber;

            Console.WriteLine(path);
            Console.WriteLine("hi");
            
            string[] lines = File.ReadAllLines(path);
            Console.WriteLine("Reading File 1");
            foreach (string line in lines) {
                Console.WriteLine(line);
            }

            Console.WriteLine("Testing reading values as int");
            numbers = ReadNumbersFromString(lines);
            foreach (int number in numbers) {
                Console.WriteLine(number);
            }

            Console.WriteLine("Sorting the list");
            numbers.Sort();
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
            frequency = FrequencyCount(numbers, 0);
            Console.WriteLine($"The number 0 occurs {frequency} times");

            //frequency = FrequencyCount(numbers, 9);
            //Console.WriteLine($"The number 9 occurs {frequency} times");

           // CompareFrequencies(numbers, 0, 10);

            Console.WriteLine("Comparing entire list");

            lowestNumber =CompareEntireList(numbers);
            Console.WriteLine($"The lowest number is {lowestNumber} it is repeated: {frequency} times");
            
        }

        

        //A method to help get a list of numbers from an array of strings
        
        private static List<int> ReadNumbersFromString(String[] lines) {
            List<int> numbers = new List<int>();
            foreach (string line in lines) {
                numbers.Add(int.Parse(line));
            }
            return numbers;
        }

        // A method to help count the frequency of one number in the list
        //verified it works
        private static int FrequencyCount(List<int> numbers, int element) {
            int frequency = 0;

            //sorts the list of numbers before checking
            bool continueSearch = true;
            numbers.Sort();
           
            // finds the index of the first occurence of the element within the list
            int index = numbers.IndexOf(element);

            //Will break out of the loop if the element we are looking for does not match the next item or we are at the end of the list
            while (continueSearch && index <numbers.Count) {
                if (element == numbers[index])
                {
                    frequency++;
                }
                else {
                    continueSearch = false;
                }
              
                index++;
            }
            Console.WriteLine($"The number {element} occurs {frequency} times");
            return frequency;
        }



        //Method to compare  frequency counts
        //if it returns -1, element 1 is less occuring, 
        // if it returns 0, element 1 and 2 are the same occurences
        // if it returns 1 element 1 is occuring more
        public static int CompareFrequencies(List<int> numbers, int element1, int element2) {
            int comparison;
            comparison=FrequencyCount(numbers, element1).CompareTo(FrequencyCount(numbers, element2));

            //Debugging
            if (comparison == -1)
            {
                Console.WriteLine($"{element1} occurs less then {element2}");
            }
            else if (comparison == 0)
            {
                Console.WriteLine($"{element1} occurs equal to {element2}");
            }
            else {
                Console.WriteLine($"{element1} occurs more then {element2}");
            }
            
            return comparison;
           
        }

        //compares entire list and gets the lowest occuring number
        public static int CompareEntireList(List<int> numbers) {
            numbers.Sort();
            //By default in a sorted list the lowest number is the first item
            int lowestNumber = numbers[0];
            int index = numbers.LastIndexOf(numbers[0]);
            int nextNumber=numbers[index];
            bool continueSearch = true;
            if (index + 1 < numbers.Count) {
                nextNumber = numbers[index + 1];
            }
            while (continueSearch) {
                int comparison = CompareFrequencies(numbers, lowestNumber, nextNumber);
                //First we check if the next number is smaller then the current lowest number, if it is then that number is considered the lowest
                //if (CompareFrequencies(numbers, lowestNumber, nextNumber) == 1) {
                if (comparison == 1) { 
                    lowestNumber = nextNumber;
                }
                //Then we check if the numbers occurent the same amount of times, in which case we check their value
               //else if (CompareFrequencies(numbers, lowestNumber, nextNumber) == 0)
               else if(comparison==0)
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
                else {
                    continueSearch = false;
                }
            }

            Console.WriteLine($"Lowest occuring number is {lowestNumber}");
            return lowestNumber;
        
        }

       
    }
}
