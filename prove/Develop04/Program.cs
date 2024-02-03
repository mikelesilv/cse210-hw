using System;
using System.Threading;

// Base class for all mindfulness activities
class MindfulnessActivity
{
    protected int duration;

    public MindfulnessActivity(int duration)
    {
        this.duration = duration;
    }

    // Display the starting message and prepare to begin
    public virtual void StartActivity(string activityName, string description)
    {
        Console.WriteLine($"{activityName} - {description}");
        Console.Write("Enter the duration in seconds: ");
        duration = int.Parse(Console.ReadLine());

        Console.WriteLine($"Get ready to begin. Starting in 3...");
        Thread.Sleep(1000);
        Console.Write("\b2...");
        Thread.Sleep(1000);
        Console.Write("\b1...");
        Thread.Sleep(1000);
        Console.WriteLine("\bGo!");
    }

    // Display the ending message
    public virtual void EndActivity(string activityName)
    {
        Console.WriteLine($"Good job! You have completed {activityName} for {duration} seconds.");
        Thread.Sleep(2000);
    }
}

// Breathing Activity class
class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity(int duration) : base(duration) { }

    public override void StartActivity(string activityName, string description)
    {
        base.StartActivity(activityName, description);

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine(i % 2 == 0 ? "Breathe in..." : "Breathe out...");
            Thread.Sleep(1000);
        }

        EndActivity(activityName);
    }
}

// Reflection Activity class
class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] reflectionQuestions = {
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

    public ReflectionActivity(int duration) : base(duration) { }

    public override void StartActivity(string activityName, string description)
    {
        base.StartActivity(activityName, description);

        Random random = new Random();

        for (int i = 0; i < duration;)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            Thread.Sleep(2000);

            foreach (string question in reflectionQuestions)
            {
                Console.WriteLine(question);
                Thread.Sleep(2000);
            }

            i += reflectionQuestions.Length + 1; // +1 for the prompt
        }

        EndActivity(activityName);
    }
}

// Listing Activity class
class ListingActivity : MindfulnessActivity
{
    private string[] listPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity(int duration) : base(duration) { }

    public override void StartActivity(string activityName, string description)
    {
        base.StartActivity(activityName, description);

        Random random = new Random();
        string listPrompt = listPrompts[random.Next(listPrompts.Length)];

        Console.WriteLine(listPrompt);
        Thread.Sleep(3000);

        Console.WriteLine($"Think about this prompt for {duration} seconds...");

        for (int i = 0; i < duration; i++)
        {
            Thread.Sleep(1000);
        }

        Console.WriteLine($"You listed {random.Next(5, 15)} items.");
        EndActivity(activityName);
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Mindfulness Activities Menu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice (1-4): ");
            int choice = int.Parse(Console.ReadLine());

            MindfulnessActivity activity;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity(0);
                    activity.StartActivity("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
                    break;
                case 2:
                    activity = new ReflectionActivity(0);
                    activity.StartActivity("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
                    break;
                case 3:
                    activity = new ListingActivity(0);
                    activity.StartActivity("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    continue;
            }
        }
    }
}
