// BreathingActivity.cs
using System;
using System.Threading;

namespace MindfulnessApp
{
    class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.") { }

        protected override void PerformActivity()
        {
            int elapsedSeconds = 0;
            while (elapsedSeconds < duration)
            {
                Console.WriteLine("Breathe in...");
                PauseWithAnimation(4);  // Breathe in for 4 seconds
                elapsedSeconds += 4;
                if (elapsedSeconds >= duration) break; // Check to avoid exceeding duration

                Console.WriteLine("Breathe out...");
                PauseWithAnimation(4); // Breathe out for 4 seconds
                elapsedSeconds += 4;
            }
        }
    }
}


