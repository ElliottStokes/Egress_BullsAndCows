using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Bulls_and_Cows
{
    public class Program
    {
        static void Main()
        {
            GenerateNumber generate = new GenerateNumber();

            Console.WriteLine("Welcome to Bulls and Cows!\nGenerating Number...");

            //Generate the computers secret number
            int newNumber = 0, pos = 0;
            while(pos < 4) {
                newNumber = generate.generateNumber(pos + 1);
                generate.genNumbers[pos] = newNumber;
                pos++;
            }

            string userInput; //Holds the numbers the user inputs
            int bulls = 0, cows = 0; //Counts how many bulls and cows the user finds

            Console.WriteLine();
            while (bulls != 4)
            {
                try
                {
                    //Sets bulls and cows to 0 to reset the count
                    bulls = 0;
                    cows = 0;
                    //Takes the user input
                    Console.Write("Input number:\n>");
                    userInput = Console.ReadLine();

                    //Exception handling for user input
                    if (userInput.Length != 4)
                        throw new Exception("Only enter 4 numbers (no spaces)"); //Checks there are only 4 numbers
                    for (int i = 0; i < 3; i++)
                    {
                        if ((int.Parse(userInput[i].ToString())) < 1 || (int.Parse(userInput[i].ToString())) > 9)
                            throw new Exception("Only enter numbers between 1 and 9"); //Checks the numbers are between 1 and 9
                        for (int j = i + 1; j <= 3; j++)
                            if (userInput[i] == userInput[j])
                                throw new Exception("Only enter numbers once per guess"); //Checks no numbers are repeated
                    }

                    //Check the user input with generated number
                    for (int i = 0; i <= 3; i++)
                        if (generate.genNumbers[i] == (int.Parse(userInput[i].ToString())))
                            bulls++;
                        else if (generate.genArray[(int.Parse(userInput[i].ToString())) - 1] == 1)
                            cows++;
                    //Give user feedback on the number they guessed
                    Console.WriteLine("{0} => bulls {1}, cows {2}", userInput, bulls, cows);
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nError: {0}\n", e.Message);
                }
            }
            Console.WriteLine("Game over!");
        }

        class GenerateNumber 
        {
            public int[] genNumbers; //Holds the generated numbers
            public int[] genArray; //Frequency array of the generated numbers

            public GenerateNumber()
            {
                genNumbers = new int[4];
                genArray = new int[9];
                for (int i = 0; i < 8; i++)
                    genArray[i] = 0; //Sets all the frequency array values to 0
            }

            public int generateNumber(int count)
            {
                Random rand = new Random();
                int x;

                Thread.Sleep(20); //This is needed beacuse the way the random object works will initialize a new number using the clock
                x = rand.Next(1, 10); //Generate a random number between 1 and 9
                if (genArray[x - 1] == 1)
                { //If the number has already been generated generate another number
                    return -1;
                }
                else
                { //Set the frequency array value to 1
                    genArray[x - 1] = 1;
                    return x;
                }
            }
        }
    }
}