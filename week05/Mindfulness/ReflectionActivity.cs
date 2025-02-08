// ReflectionActivity.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace MindfulnessApp
{
    class ReflectionActivity : Activity
    {
        private List<string> prompts = new List<string>()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> questions = new List<string>()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        private Random random = new Random();

        public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.") { }

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

            int elapsedSeconds = 0;
            while (elapsedSeconds < duration)
            {
                if (questions.Count == 0)
                {
                    Console.WriteLine("All questions have been asked for this prompt.  Please restart the application for more questions.");
                    break; // No more questions
                }

                int questionIndex = random.Next(questions.Count);
                string question = questions[questionIndex];
                questions.RemoveAt(questionIndex); // Ensure each question is used only once per prompt

                Console.WriteLine(question);
                DisplaySpinner(5); // Pause for 5 seconds
                elapsedSeconds += 5;
            }
        }
    }
}
