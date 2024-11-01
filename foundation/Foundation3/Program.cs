/*
Exceed Requirement: 
This program categorizes each activity by intensity level (Light, Moderate, Intense) based on pace or speed. 
The intensity level provides extra feedback to the user about their workout's difficulty, offering a personalized summary experience.

*/

using System;
using System.Collections.Generic;

public abstract class Activity
{
    private DateTime date;
    private int durationMinutes;

    public Activity(DateTime date, int durationMinutes)
    {
        this.date = date;
        this.durationMinutes = durationMinutes;
    }

    public DateTime Date => date;
    public int DurationMinutes => durationMinutes;

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();
    
    // New method to categorize intensity based on speed or pace
    public abstract string GetIntensityLevel();

    public virtual string GetSummary()
    {
        return $"{Date.ToString("dd MMM yyyy")} {GetType().Name} ({DurationMinutes} min) - " +
               $"Distance: {GetDistance():F1} km, Speed: {GetSpeed():F1} kph, Pace: {GetPace():F2} min per km, " +
               $"Intensity: {GetIntensityLevel()}";
    }
}

// Running class with intensity categorized by pace
public class Running : Activity
{
    private double distanceKm;

    public Running(DateTime date, int durationMinutes, double distanceKm)
        : base(date, durationMinutes)
    {
        this.distanceKm = distanceKm;
    }

    public override double GetDistance() => distanceKm;
    public override double GetSpeed() => (GetDistance() / DurationMinutes) * 60;
    public override double GetPace() => DurationMinutes / GetDistance();

    public override string GetIntensityLevel()
    {
        double pace = GetPace();
        if (pace < 5) return "Intense";
        if (pace < 8) return "Moderate";
        return "Light";
    }
}

// Cycling class with intensity categorized by speed
public class Cycling : Activity
{
    private double speedKph;

    public Cycling(DateTime date, int durationMinutes, double speedKph)
        : base(date, durationMinutes)
    {
        this.speedKph = speedKph;
    }

    public override double GetDistance() => (speedKph * DurationMinutes) / 60;
    public override double GetSpeed() => speedKph;
    public override double GetPace() => 60 / speedKph;

    public override string GetIntensityLevel()
    {
        double speed = GetSpeed();
        if (speed > 20) return "Intense";
        if (speed > 10) return "Moderate";
        return "Light";
    }
}

// Swimming class with intensity categorized by pace
public class Swimming : Activity
{
    private int lapCount;

    public Swimming(DateTime date, int durationMinutes, int lapCount)
        : base(date, durationMinutes)
    {
        this.lapCount = lapCount;
    }

    public override double GetDistance() => (lapCount * 50) / 1000.0;
    public override double GetSpeed() => (GetDistance() / DurationMinutes) * 60;
    public override double GetPace() => DurationMinutes / GetDistance();

    public override string GetIntensityLevel()
    {
        double pace = GetPace();
        if (pace < 2) return "Intense";
        if (pace < 4) return "Moderate";
        return "Light";
    }
}

class Program
{
    static void Main()
    {
        var activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 4.8),
            new Cycling(new DateTime(2022, 11, 3), 30, 15),
            new Swimming(new DateTime(2022, 11, 3), 30, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
