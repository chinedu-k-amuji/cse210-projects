using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            QuestManager manager = new QuestManager();
            manager.LoadGoals();

            string option = string.Empty;
            while (option != "7")
            {
                Console.Clear();
                Console.WriteLine("Eternal Quest");
                Console.WriteLine("1. View Goals");
                Console.WriteLine("2. Add Simple Goal");
                Console.WriteLine("3. Add Eternal Goal");
                Console.WriteLine("4. Add Checklist Goal");
                Console.WriteLine("5. Record Goal Completion");
                Console.WriteLine("6. View Score");
                Console.WriteLine("7. Save and Exit");

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        manager.DisplayGoals();
                        break;
                    case "2":
                        manager.AddSimpleGoal();
                        break;
                    case "3":
                        manager.AddEternalGoal();
                        break;
                    case "4":
                        manager.AddChecklistGoal();
                        break;
                    case "5":
                        manager.RecordCompletion();
                        break;
                    case "6":
                        manager.DisplayScore();
                        break;
                    case "7":
                        manager.SaveGoals();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }

    public class QuestManager
    {
        private List<Goal> goals = new List<Goal>();
        private int score = 0;
        private const string FileName = "goals.txt";

        public void DisplayGoals()
        {
            Console.Clear();
            foreach (Goal goal in goals)
            {
                Console.WriteLine(goal.Display());
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void AddSimpleGoal()
        {
            Console.Clear();
            Console.Write("Enter the name of the goal: ");
            string name = Console.ReadLine();
            Console.Write("Enter the point value: ");
            int points = int.Parse(Console.ReadLine());

            goals.Add(new SimpleGoal(name, points));
        }

        public void AddEternalGoal()
        {
            Console.Clear();
            Console.Write("Enter the name of the goal: ");
            string name = Console.ReadLine();
            Console.Write("Enter the point value: ");
            int points = int.Parse(Console.ReadLine());

            goals.Add(new EternalGoal(name, points));
        }

        public void AddChecklistGoal()
        {
            Console.Clear();
            Console.Write("Enter the name of the goal: ");
            string name = Console.ReadLine();
            Console.Write("Enter the point value: ");
            int points = int.Parse(Console.ReadLine());
            Console.Write("Enter the required completions: ");
            int requiredCompletions = int.Parse(Console.ReadLine());
            Console.Write("Enter the bonus points: ");
            int bonusPoints = int.Parse(Console.ReadLine());

            goals.Add(new ChecklistGoal(name, points, requiredCompletions, bonusPoints));
        }

        public void RecordCompletion()
        {
            Console.Clear();
            Console.WriteLine("Select a goal to record completion:");
            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].Display()}");
            }

            int selectedGoal = int.Parse(Console.ReadLine()) - 1;
            if (selectedGoal >= 0 && selectedGoal < goals.Count)
            {
                int points = goals[selectedGoal].RecordCompletion();
                if (points > 0)
                {
                    score += points;
                    Console.WriteLine($"You earned {points} points!");
                }
                else
                {
                    score += points; // For negative points
                    Console.WriteLine($"You lost {Math.Abs(points)} points!");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void DisplayScore()
        {
            Console.Clear();
            Console.WriteLine($"Current score: {score}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void SaveGoals()
        {
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                writer.WriteLine(score);
                foreach (Goal goal in goals)
                {
                    writer.WriteLine(goal.ToString());
                }
            }
        }

        public void LoadGoals()
        {
            if (File.Exists(FileName))
            {
                using (StreamReader reader = new StreamReader(FileName))
                {
                    score = int.Parse(reader.ReadLine());
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        Goal goal = Goal.Parse(line);
                        goals.Add(goal);
                    }
                }
            }
        }
    }

    public abstract class Goal
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public bool IsCompleted { get; set; }

        protected Goal(string name, int points)
        {
            Name = name;
            Points = points;
            IsCompleted = false;
        }

        public abstract string Display();
        public abstract int RecordCompletion();
        public override string ToString()
        {
            return $"{GetType().Name}:{Name}:{Points}:{IsCompleted}";
        }

        public static Goal Parse(string line)
        {
            string[] parts = line.Split(':');
            string type = parts[0];
            string name = parts[1];
            int points = int.Parse(parts[2]);
            bool isCompleted = bool.Parse(parts[3]);

            switch (type)
            {
                case "SimpleGoal":
                    return new SimpleGoal(name, points) { IsCompleted = isCompleted };
                case "EternalGoal":
                    return new EternalGoal(name, points) { IsCompleted = isCompleted };
                case "ChecklistGoal":
                    int currentCount = int.Parse(parts[4]);
                    int requiredCompletions = int.Parse(parts[5]);
                    int bonusPoints = int.Parse(parts[6]);
                    return new ChecklistGoal(name, points, requiredCompletions, bonusPoints) { IsCompleted = isCompleted, CurrentCount = currentCount };
                default:
                    throw new Exception("Unknown goal type.");
            }
        }
    }

    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, int points) : base(name, points)
        {
        }

        public override string Display()
        {
            return $"[ {(IsCompleted ? "X" : " ")} ] {Name} ({Points} points)";
        }

        public override int RecordCompletion()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                return Points;
            }
            return 0;
        }
    }

    public class EternalGoal : Goal
    {
        public EternalGoal(string name, int points) : base(name, points)
        {
        }

        public override string Display()
        {
            return $"[ ] {Name} ({Points} points each time)";
        }

        public override int RecordCompletion()
        {
            return Points;
        }
    }

    public class ChecklistGoal : Goal
    {
        public int CurrentCount { get; set; }
        public int RequiredCompletions { get; set; }
        public int BonusPoints { get; set; }

        public ChecklistGoal(string name, int points, int requiredCompletions, int bonusPoints) : base(name, points)
        {
            RequiredCompletions = requiredCompletions;
            BonusPoints = bonusPoints;
            CurrentCount = 0;
        }

        public override string Display()
        {
            return $"[ {(IsCompleted ? "X" : " ")} ] {Name} ({Points} points each time, {BonusPoints} bonus after {RequiredCompletions} completions) - Completed {CurrentCount}/{RequiredCompletions} times";
        }

        public override int RecordCompletion()
        {
            if (!IsCompleted)
            {
                CurrentCount++;
                if (CurrentCount >= RequiredCompletions)
                {
                    IsCompleted = true;
                    return Points + BonusPoints;
                }
                return Points;
            }
            return 0;
        }

        public override string ToString()
        {
            return $"{base.ToString()}:{CurrentCount}:{RequiredCompletions}:{BonusPoints}";
        }
    }
}
