// ListingActivity.cs
using System;
using System.Collections.Generic;

namespace MindfulnessApp
{
    class ListingActivity : Activity
    {
        private List<string> prompts = new List<string>()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
        private Random random = new Random();

        public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.") { }

        protected override void PerformActivity()
        {
             if (prompts.Count == 0)
            {
                Console.WriteLine("No more prompts available for this session.  Please restart the application.");
                return;
            }

            int promptIndex = random.Next(prompts.Count);
            string prompt = prompts[promptIndex];
            prompts.RemoveAt(promptIndex); // Ensure each prompt is used only once per session

            Console.WriteLine(prompt);
            Console.WriteLine("You have 5 seconds to think about the prompt.");
            PauseWithAnimation(5);

            Console.WriteLine("Start listing items:");

            List<string> items = new List<string>();
            DateTime startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalSeconds < duration)
            {
                Console.Write("Enter an item (or press Enter to finish early): ");
                string item = Console.ReadLine();
                if (string.IsNullOrEmpty(item))
                {
                    break; // User pressed Enter, stop listing
                }
                items.Add(item);
            }

            Console.WriteLine($"You listed {items.Count} items.");
        }
    }
}