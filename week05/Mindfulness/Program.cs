//I ensured that no random prompts/questions were selected until they were used at least once in that session.
// Program.cs

using System;
using System.Collections.Generic;

namespace MindfulnessApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MindfulnessApp app = new MindfulnessApp();
            app.Run();
        }
    }

    class MindfulnessApp
    {
        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Mindfulness Activities Menu:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BreathingActivity breathingActivity = new BreathingActivity();
                        breathingActivity.Execute();
                        break;
                    case "2":
                        ReflectionActivity reflectionActivity = new ReflectionActivity();
                        reflectionActivity.Execute();
                        break;
                    case "3":
                        ListingActivity listingActivity = new ListingActivity();
                        listingActivity.Execute();
                        break;
                    case "4":
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine(); // Add a newline for better readability
            }
        }
    }
}


// Activity.cs
namespace MindfulnessApp
{
    abstract class Activity
    {
        protected string name;
        protected string description;
        protected int duration;

        public Activity(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public virtual void Execute()
        {
            StartActivity();
            PerformActivity();
            EndActivity();
        }

        protected void StartActivity()
        {
            Console.WriteLine($"\n{name}");
            Console.WriteLine(description);

            Console.Write("Enter the duration of the activity in seconds: ");
            duration = int.Parse(Console.ReadLine());

            Console.WriteLine("Prepare to begin...");
            PauseWithAnimation(3); // Pause for 3 seconds
        }

        protected abstract void PerformActivity();

        protected void EndActivity()
        {
            Console.WriteLine("Great job!");
            PauseWithAnimation(2);
            Console.WriteLine($"You have completed the {name} for {duration} seconds.");
            PauseWithAnimation(2);
        }

        protected void PauseWithAnimation(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"\r{i}..."); // \r returns cursor to beginning of line
                Thread.Sleep(1000);
            }
            Console.WriteLine("\r   "); // Clear the countdown
        }


        protected void DisplaySpinner(int seconds)
        {
            string[] spinnerFrames = { "|", "/", "-", "\\" };
            int frameIndex = 0;
            for (int i = 0; i < seconds * 4; i++) // 4 frames per second
            {
                Console.Write($"\r{spinnerFrames[frameIndex]}");
                frameIndex = (frameIndex + 1) % spinnerFrames.Length;
                Thread.Sleep(250); // Adjust for smoother animation
            }
            Console.Write("\r   "); // Clear the spinner
        }
    }
}