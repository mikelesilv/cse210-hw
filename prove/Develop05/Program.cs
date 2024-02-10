using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Points { get; protected set; }
    public bool IsComplete { get; protected set; }

    public Goal(string name, string description)
    {
        Name = name;
        Description = description;
        Points = 0;
        IsComplete = false;
    }

    public virtual void RecordCompletion()
    {
        IsComplete = true;
    }

    public virtual void DisplayStatus()
    {
        Console.WriteLine($"{Name} ({Description}) - Completed: {(IsComplete ? "Yes" : "No")}");
    }

    public virtual int GetPoints()
    {
        return Points;
    }
}

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description)
    {
        Points = points;
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int pointsPerCompletion) : base(name, description)
    {
        Points = pointsPerCompletion;
    }

    public override void RecordCompletion()
    {
        Points++;
        base.RecordCompletion(); // Don't forget to call base method to set IsComplete
    }
}

public class ChecklistGoal : Goal
{
    public int TargetCount { get; }
    public int CompletedCount { get; set; }

    public ChecklistGoal(string name, string description, int pointsPerCompletion, int targetCount) : base(name, description)
    {
        Points = pointsPerCompletion;
        TargetCount = targetCount;
        CompletedCount = 0;
    }

    public override void RecordCompletion()
    {
        CompletedCount++;
        if (CompletedCount == TargetCount)
        {
            Points += 500;
            IsComplete = true;
        }
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"{Name} ({Description}) - Completed {CompletedCount}/{TargetCount} times");
    }
}

public class EternalQuestProgram
{
    private List<Goal> goals;
    private int totalPoints;

    public EternalQuestProgram()
    {
        goals = new List<Goal>();
        totalPoints = 0;
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
        totalPoints += goal.GetPoints(); // Update total points
        Console.WriteLine($"Goal '{goal.Name}' added successfully.");
    }

    public void RecordGoalCompletion(string goalName)
    {
        Goal goal = goals.Find(g => g.Name == goalName);
        if (goal != null)
        {
            goal.RecordCompletion();
            totalPoints += goal.GetPoints();
            Console.WriteLine($"Goal '{goal.Name}' completed successfully.");
        }
        else
        {
            Console.WriteLine($"Goal '{goalName}' not found.");
        }
    }

    public void DisplayGoals()
    {
        foreach (Goal goal in goals)
        {
            goal.DisplayStatus();
        }
    }

    public void DisplayTotalScore()
    {
        Console.WriteLine($"Total Score: {totalPoints}");
    }

    public List<Goal> GetGoals()
    {
        return goals;
    }

    public void LoadGoals(string fileName)
    {
        try
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{fileName}.txt");
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string name = parts[0];
                    string description = parts[1];
                    int points = int.Parse(parts[2]);
                    bool isComplete = bool.Parse(parts[3]);

                    Goal goal;
                    if (parts.Length == 4)
                        goal = new SimpleGoal(name, description, points);
                    else
                    {
                        int targetCount = int.Parse(parts[4]);
                        int completedCount = int.Parse(parts[5]);
                        goal = new ChecklistGoal(name, description, points, targetCount);
                        ((ChecklistGoal)goal).CompletedCount = completedCount;
                    }

                    if (isComplete)
                        goal.RecordCompletion();
                    AddGoal(goal);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }
    }

    public void SaveGoals(string fileName)
    {
        try
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{fileName}.txt");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Goal goal in goals)
                {
                    if (goal is ChecklistGoal checklistGoal)
                    {
                        writer.WriteLine($"{goal.Name},{goal.Description},{goal.Points},{goal.IsComplete},{checklistGoal.TargetCount},{checklistGoal.CompletedCount}");
                    }
                    else
                    {
                        writer.WriteLine($"{goal.Name},{goal.Description},{goal.Points},{goal.IsComplete}");
                    }
                }
            }
            Console.WriteLine($"Goals saved successfully to {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        EternalQuestProgram program = new EternalQuestProgram();

        while (true)
        {
            Console.WriteLine("\nEternal Quest Program");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddGoal(program);
                    program.DisplayTotalScore();
                    break;
                case "2":
                    program.DisplayGoals();
                    break;
                case "3":
                    SaveGoals(program);
                    break;
                case "4":
                    LoadGoals(program);
                    break;
                case "5":
                    RecordGoalCompletion(program);
                    program.DisplayTotalScore(); // Display Total Score after recording event
                    break;
                case "6":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 6.");
                    break;
            }
        }
    }

    static void AddGoal(EternalQuestProgram program)
    {
        Console.WriteLine("Select goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");
        string typeChoice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter a short description of the goal: ");
        string description = Console.ReadLine();

        Console.Write("Enter points for completion: ");
        int points;
        while (!int.TryParse(Console.ReadLine(), out points))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
            Console.Write("Enter points for completion: ");
        }

        switch (typeChoice)
        {
            case "1":
                program.AddGoal(new SimpleGoal(name, description, points));
                break;
            case "2":
                program.AddGoal(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter target count for the checklist goal: ");
                int targetCount;
                while (!int.TryParse(Console.ReadLine(), out targetCount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                    Console.Write("Enter target count for the checklist goal: ");
                }
                program.AddGoal(new ChecklistGoal(name, description, points, targetCount));
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void SaveGoals(EternalQuestProgram program)
    {
        Console.Write("Enter the name for the file to save goals: ");
        string fileName = Console.ReadLine();
        program.SaveGoals(fileName);
    }

    static void LoadGoals(EternalQuestProgram program)
    {
        Console.Write("Enter the name of the file to load goals from: ");
        string fileName = Console.ReadLine();
        program.LoadGoals(fileName);
    }

    static void RecordGoalCompletion(EternalQuestProgram program)
    {
        program.DisplayGoals();
        Console.Write("Enter the name of the goal to record completion: ");
        string goalName = Console.ReadLine();
        program.RecordGoalCompletion(goalName);
    }
}
