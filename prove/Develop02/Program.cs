using System;
using System.IO;
using System.Collections.Generic;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public JournalEntry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }

    public override string ToString()
    {
        return $"{Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

class JournalProgram
{
    private List<JournalEntry> journalEntries = new List<JournalEntry>();
    private string filename = "journal.txt";

    public void Run()
{
    LoadJournal();

    while (true)
    {
        Console.WriteLine("\n1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Exit");

        int choice = GetChoice();

        switch (choice)
        {
            case 1:
                WriteNewEntry();
                break;
            case 2:
                DisplayJournal();
                break;
            case 3:
                SaveJournal();
                break;
            case 4:
                LoadJournal();  // You can choose to reload the journal if needed
                break;
            case 5:
                Console.WriteLine("Exiting the program. Goodbye!");
                SaveJournal();
                return;
            default:
                Console.WriteLine("Invalid choice. Please choose again.");
                break;
        }
    }
}

    private int GetChoice()
    {
        Console.Write("Enter your choice (1-5): ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.Write("Invalid input. Please enter a number between 1 and 5: ");
        }
        return choice;
    }

    private void WriteNewEntry()
    {
        Console.WriteLine("Writing a new entry...");
        Console.WriteLine("Choose a prompt:");

        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        for (int i = 0; i < prompts.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {prompts[i]}");
        }

        int promptChoice = GetChoice();
        string selectedPrompt = prompts[promptChoice - 1];

        Console.Write("Enter your response: ");
        string response = Console.ReadLine();

        string currentDate = DateTime.Now.ToShortDateString();
        JournalEntry entry = new JournalEntry(selectedPrompt, response, currentDate);
        journalEntries.Add(entry);

        Console.WriteLine("Entry added successfully!");
    }

    private void DisplayJournal()
    {
        Console.WriteLine("\nJournal Entries:");
        foreach (var entry in journalEntries)
        {
            Console.WriteLine(entry);
        }
    }

    private void SaveJournal()
    {
        Console.Write("Enter a filename to save the journal: ");
        string newFilename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(newFilename))
        {
            foreach (var entry in journalEntries)
            {
                outputFile.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }

        Console.WriteLine("Journal saved successfully!");
    }

    private void LoadJournal()
    {
        Console.Write("Enter a filename to load the journal: ");
        string loadFilename = Console.ReadLine();

        if (File.Exists(loadFilename))
        {
            journalEntries.Clear();
            string[] lines = File.ReadAllLines(loadFilename);

            foreach (var line in lines)
            {
                string[] parts = line.Split(",");
                if (parts.Length == 3)
                {
                    string date = parts[0];
                    string prompt = parts[1];
                    string response = parts[2];
                    JournalEntry entry = new JournalEntry(prompt, response, date);
                    journalEntries.Add(entry);
                }
            }

            Console.WriteLine("Journal loaded successfully!");
        }
        else
        {
            Console.WriteLine("File not found. Creating a new journal.");
        }
    }
}

class Program
{
    static void Main()
    {
        JournalProgram journalProgram = new JournalProgram();
        journalProgram.Run();
    }
}
