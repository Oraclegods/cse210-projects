using System;

class Program
{
    static void Main(string[] args)
    {
         string playAgain;

        do
        {
            // Initialize random number generator and generate a magic number between 1 and 100
            Random random = new Random();
            int magicNumber = random.Next(1, 101);

            int guess = 0;
            int numberOfGuesses = 0;

            Console.WriteLine("Welcome to the 'Guess My Number' game!");

            // Loop until the user guesses the correct number
            while (guess != magicNumber)
            {
                // Ask the user for their guess
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                numberOfGuesses++;

                // Determine if the guess is higher, lower, or correct
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"You guessed it in {numberOfGuesses} guesses!");
                }
            }

            // Ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine().ToLower();
        } while (playAgain == "yes");

        Console.WriteLine("Thanks for playing!");
    }
    }