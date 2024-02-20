using System;
using System.Collections.Generic;

public enum ActivityType
{
    Running,
    Cycling,
    Swimming
}

public abstract class Activity
{
    protected DateTime Date { get; }
    protected int DurationMinutes { get; }

    public Activity(DateTime date, int durationMinutes)
    {
        Date = date;
        DurationMinutes = durationMinutes;
    }

    public abstract double CalculateDistance();
    public abstract double CalculateSpeed();
    public abstract double CalculatePace();

    public string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({DurationMinutes} min) - " +
               $"Distance: {CalculateDistance():F2} km, Speed: {CalculateSpeed():F2} kph, Pace: {CalculatePace():F2} min/km";
    }
}

public class Running : Activity
{
    private double Distance { get; }

    public Running(DateTime date, int durationMinutes, double distance) : base(date, durationMinutes)
    {
        if (distance < 0)
            throw new ArgumentException("Distance cannot be negative.");
        Distance = distance;
    }

    public override double CalculateDistance()
    {
        return Distance;
    }

    public override double CalculateSpeed()
    {
        if (DurationMinutes == 0)
            return 0;
        return Distance / (DurationMinutes / 60.0);
    }

    public override double CalculatePace()
    {
        if (Distance == 0)
            return 0;
        return (DurationMinutes / 60.0) / Distance;
    }
}

public class Cycling : Activity
{
    private double Speed { get; }

    public Cycling(DateTime date, int durationMinutes, double speed) : base(date, durationMinutes)
    {
        if (speed < 0)
            throw new ArgumentException("Speed cannot be negative.");
        Speed = speed;
    }

    public override double CalculateDistance()
    {
        return Speed * (DurationMinutes / 60.0);
    }

    public override double CalculateSpeed()
    {
        return Speed;
    }

    public override double CalculatePace()
    {
        if (Speed == 0)
            return 0;
        return 60.0 / Speed;
    }
}

public class Swimming : Activity
{
    private int Laps { get; }

    public Swimming(DateTime date, int durationMinutes, int laps) : base(date, durationMinutes)
    {
        if (laps < 0)
            throw new ArgumentException("Laps cannot be negative.");
        Laps = laps;
    }

    public override double CalculateDistance()
    {
        return Laps * 50 / 1000.0;
    }

    public override double CalculateSpeed()
    {
        if (DurationMinutes == 0)
            return 0;
        return (Laps * 50 / 1000.0) / (DurationMinutes / 60.0);
    }

    public override double CalculatePace()
    {
        if (Laps == 0)
            return 0;
        return (DurationMinutes / 60.0) / (Laps * 50 / 1000.0);
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Running(new DateTime(2022, 11, 3), 30, 4.8),
            new Cycling(new DateTime(2022, 11, 3), 30, 6.0),
            new Swimming(new DateTime(2022, 11, 3), 30, 10)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
