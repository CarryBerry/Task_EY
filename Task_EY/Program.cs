using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using Task_EY.EntityModel;

namespace Task_EY
{
    class Program
    {
        static void Main(string[] args)
        {
            bool actionStopper = true;

            while (actionStopper)
            {
                Console.WriteLine("Please, enter amount of files to generate. Default amount: 100.000");

                int amount = 0;

                while (amount <= 0)
                {
                    amount = Convert.ToInt32(Console.ReadLine());
                }

                FileCreator creator = new FileCreator();
                creator.CreateMany(amount);
                
                Console.WriteLine("{0} files were created", amount);

                Console.WriteLine("1 - merge files,");
                Console.WriteLine("2 - transfer created files to database,");
                Console.WriteLine("Please, enter number to proceed:");

                int answer = 0;

                while (answer < 1 || answer > 2)
                {
                    answer = Convert.ToInt32(Console.ReadLine());
                }

                if (answer == 1)
                {
                    Console.WriteLine("Enter numbers of a files to merge");
                    Console.WriteLine("First file number:");
                    string firstNumber = "0";

                    while (Convert.ToInt32(firstNumber) <= 0)
                    {
                        firstNumber = Console.ReadLine();
                    }

                    string firstName = "file" + firstNumber + ".txt";

                    Console.WriteLine("Second file number:");
                    string secondNumber = firstNumber;

                    while (secondNumber == firstNumber || Convert.ToInt32(secondNumber) <= 0)
                    {
                        secondNumber = Console.ReadLine();
                    }
                    
                    string secondName = "file" + secondNumber + ".txt"; 

                    Console.WriteLine("Would you like to remove all lines which contains specific chars?");
                    Console.WriteLine("Answer? Y/N");

                    var key = Console.ReadKey().Key;
                    Console.WriteLine();

                    if (key == ConsoleKey.Y)
                    {
                        Console.WriteLine("Enter your chars, please");
                        string charsToRemove = Console.ReadLine();

                        try
                        {
                            int deletedLines = Merger.MergeFiles(firstName, secondName, charsToRemove);
                            Console.WriteLine("{0} lines were deleted", deletedLines);
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    else if (key == ConsoleKey.N)
                    {
                        Merger.MergeFiles(firstName, secondName);
                    }
                   
                    Console.WriteLine("Merging complete.");
                }
                else if (answer == 2)
                {
                    DbHandler handler = new DbHandler();

                    handler.DbAddCreated(amount);
                }

                Console.WriteLine("Would you like to know the sum of all the first numbers in all rows?");
                Console.WriteLine("Or the median of all the second numbers in all rows?");
                Console.WriteLine("Answer? Y/N");

                var k = Console.ReadKey().Key;

                if (k == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    Console.WriteLine("Sum of all the first numbers is {0}", DBNumberManupulator.GetFirstColumnSum());
                    Console.WriteLine("Median of all the second numbers is {0}", DBNumberManupulator.GetSecondColumnMedian());
                }
                else if (k == ConsoleKey.N)
                {
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Would you like to exit?");
                Console.WriteLine("Y/N");

                var decision = Console.ReadKey().Key;

                if (decision == ConsoleKey.Y)
                {
                    actionStopper = false;
                }
                else if (decision == ConsoleKey.N)
                {
                    continue;
                }
            }
        }
    }
}
