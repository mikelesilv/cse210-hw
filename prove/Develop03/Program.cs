using System;
using System.Collections.Generic;
using System.Linq;

public class Word
{
    public string Text { get; set; }
    public bool IsHidden { get; set; }

    public Word(string text, bool isHidden)
    {
        Text = text;
        IsHidden = isHidden;
    }
}

public class Reference
{
    public string Verse { get; set; }

    public Reference(string verse)
    {
        Verse = verse;
    }
}

public class Scripture
{
    public Reference Reference { get; set; }
    public List<Word> Words { get; set; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word, false)).ToList();
    }

    // Additional constructors for handling verse range, if needed
}

class Program
{
    static void Main()
    {
        // Example usage
        Reference reference = new Reference("Joseph Smith-History");
        Scripture scripture = new Scripture(reference, "I saw a pillar of light exactly over my head, above the brightness of the sun, which descended gradually until it fell upon me. When the light rested upon me I saw two Personages, whose brightness and glory defy all description, standing above me in the air. One of them spake unto me, calling me by name and said, pointing to the other—This is My Beloved Son. Hear Him!");

        while (!AllWordsHidden(scripture))
        {
            DisplayScripture(scripture);
            Console.WriteLine("Press Enter to hide more words or type 'quit' to end.");
            
            string input = Console.ReadLine();
            
            if (input.ToLower() == "quit")
                break;

            HideRandomWords(scripture);
            Console.Clear();
        }
    }

    static void DisplayScripture(Scripture scripture)
    {
        Console.WriteLine($"{scripture.Reference.Verse}\n");

        foreach (var word in scripture.Words)
        {
            Console.Write(word.IsHidden ? "---- " : $"{word.Text} ");
        }

        Console.WriteLine("\n");
    }

    static void HideRandomWords(Scripture scripture)
    {
        Random random = new Random();
        int wordsToHide = random.Next(1, scripture.Words.Count);

        for (int i = 0; i < wordsToHide; i++)
        {
            int index = random.Next(scripture.Words.Count);
            scripture.Words[index].IsHidden = true;
        }
    }

    static bool AllWordsHidden(Scripture scripture)
    {
        return scripture.Words.All(word => word.IsHidden);
    }
}
