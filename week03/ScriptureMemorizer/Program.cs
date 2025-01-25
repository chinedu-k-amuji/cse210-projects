// I made my program work with five different scriptures instead of a single one. And those scriptures are chosen at random to present to users.
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            new Scripture(new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."),
            new Scripture(new Reference("Isaiah", 41, 10), "Fear thou not; for I am with the: be not dismayed; for I am thy God: I will strengthen the; yea, I will help thee; yea, I will uphold thee with the right hand of my righteousness."),
            new Scripture(new Reference("Psalm", 70, 1, 2), "Make hast, O God, to deliver me; make haste to help me, O Lord. Let them be ashamed and confounded that seek after my soul: let them be turned backward, and put to confusion, that desire my hurt. Let them be turned back for a reward of their shame that say, Aha, aha."),
            new Scripture(new Reference("Genesis", 1, 3), "And God said, Let there be light: and there was light.")
        };

        Random random = new Random();
        Scripture scripture = scriptures[random.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            scripture.Display();

            Console.WriteLine("Press Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }
            else
            {
                scripture.HideWords();
                if (scripture.IsCompletelyHidden())
                {
                    Console.Clear();
                    scripture.Display();
                    break;
                }
            }
        }
    }
}

class Scripture
{
    public Reference Reference { get; private set; }
    public List<Word> Words { get; private set; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void Display()
    {
        Console.WriteLine($"{Reference.ToString()}: {string.Join(" ", Words.Select(word => word.Display()))}");
    }

    public void HideWords()
    {
        Random random = new Random();
        int wordsToHide = random.Next(1, 4);

        for (int i = 0; i < wordsToHide; i++)
        {
            List<Word> visibleWords = Words.Where(word => !word.IsHidden).ToList();
            if (visibleWords.Count == 0) break;

            Word wordToHide = visibleWords[random.Next(visibleWords.Count)];
            wordToHide.Hide();
        }
    }

    public bool IsCompletelyHidden()
    {
        return Words.All(word => word.IsHidden);
    }
}

class Reference
{
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int StartVerse { get; private set; }
    public int? EndVerse { get; private set; }

    public Reference(string book, int chapter, int startVerse, int? endVerse = null)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse.HasValue ? $"{Book} {Chapter}:{StartVerse}-{EndVerse}" : $"{Book} {Chapter}:{StartVerse}";
    }
}

class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public string Display()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}
