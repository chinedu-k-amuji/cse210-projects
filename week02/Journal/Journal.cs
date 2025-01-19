using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class Journal
{
    private List<JournalEntry> _entries = new List<JournalEntry>();
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "How much did I spend today?",
        "How did I work on myself today for personal growth?"
    };

    public void AddEntry()
    {
        Random rnd = new Random();
        string prompt = _prompts[rnd.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        _entries.Add(new JournalEntry(prompt, response, date));
    }

    public void DisplayEntries()
    {
        foreach (var entry in _entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine("Date,Prompt,Response");
            foreach (var entry in _entries)
            {
                string prompt = EscapeCsvField(entry.Prompt);
                string response = EscapeCsvField(entry.Response);
                writer.WriteLine($"{entry.Date},{prompt},{response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        _entries.Clear();
        using (StreamReader reader = new StreamReader(filename))
        {
            string line;
            // Skip header line
            reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = SplitCsvLine(line);
                if (parts.Length == 3)
                {
                    _entries.Add(new JournalEntry(parts[1], parts[2], parts[0]));
                }
            }
        }
    }

    private string EscapeCsvField(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            field = $"\"{field}\"";
        }
        return field;
    }

    private string[] SplitCsvLine(string line)
    {
        var matches = Regex.Matches(line, "(\"[^\"]*(?:\"\"[^\"]*)*\"|[^,]+)(?:,|$)");
        var fields = new List<string>();
        foreach (Match match in matches)
        {
            string field = match.Value;
            if (field.StartsWith("\""))
            {
                field = field.Substring(1, field.Length - 2).Replace("\"\"", "\"");
            }
            fields.Add(field);
        }
        return fields.ToArray();
    }
}
