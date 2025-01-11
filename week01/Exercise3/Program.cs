using System;

class GuessMyNumber
{
    static void Main(string[] args)
    {
        // Core Requirement: Start by asking the user for the magic number.
        Console.Write("What is the magic number? ");
        int magicNumber = int.Parse(Console.ReadLine());

        // Core Requirement: Ask the user for a guess.
        Console.Write("What is your guess? ");
        int guess = int.Parse(Console.ReadLine());

        // Core Requirement: Determine if the user needs to guess higher or lower.
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
            Console.WriteLine("You guessed it!");
        }

        // Core Requirement: Add a loop that keeps looping as long as the guess does not match the magic number.
        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

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
                Console.WriteLine("You guessed it!");
            }
        }

        // Stretch Challenge: Keep track of how many guesses the user has made and inform them of it at the end of the game.
        int guessCount = 1; // Initialize guess count
        Random random = new Random();
        magicNumber = random.Next(1, 101); // Generate a random number from 1 to 100

        while (guess != magicNumber)
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());
            guessCount++;

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
                Console.WriteLine("You guessed it!");
                Console.WriteLine($"It took you {guessCount} guesses.");
            }
        }

        // Stretch Challenge: Ask the user if they want to play again.
        Console.Write("Do you want to play again (yes/no)? ");
        string playAgain = Console.ReadLine().ToLower();

        while (playAgain == "yes")
        {
            guessCount = 1; // Reset guess count
            magicNumber = random.Next(1, 101); // Generate a new random number

            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());

            while (guess != magicNumber)
            {
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
                    Console.WriteLine("You guessed it!");
                    Console.WriteLine($"It took you {guessCount} guesses.");
                }

                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                guessCount++;
            }

            Console.Write("Do you want to play again (yes/no)? ");
            playAgain = Console.ReadLine().ToLower();
        }
    }
}
