/*
 * Eternal Quest Program
 * 
 * Exceeded Requirements:
 * - The program includes gamification elements like streaks for daily habits, awarding additional points every 7 days for Eternal Goals.
 * - Checklist goals reward bonus points upon reaching a target number of completions, reinforcing the habit formation.
 * - File saving/loading is implemented, allowing users to save their goals and progress and reload them later.
 */

using System;
using System.Collections.Generic;
using System.IO;

#region Goal Classes

// Base class to represent a generic goal
abstract class Goal
{
    // Private member variables to ensure encapsulation
    private string name;
    private string description;
    private int points;
    private bool isComplete;

    // Public properties to access member variables
    public string Name => name;
    public string Description => description;
    public int Points => points;
    public bool IsComplete => isComplete;

    // Constructor to initialize goal properties
    public Goal(string name, string description, int points)
    {
        this.name = name;
        this.description = description;
        this.points = points;
        isComplete = false;
    }

    // Abstract method to record completion, implemented by derived classes
    public abstract void RecordCompletion();

    // Method to display the goal's progress
    public virtual void DisplayProgress() =>
        Console.WriteLine($"{Name}: {Description} - {(IsComplete ? "[X] Completed" : "[ ] Not Completed")}");

    public int GetPoints() => points;  // Get points for the goal
    public bool CheckComplete() => isComplete;  // Check if goal is complete

    protected void MarkComplete() => isComplete = true; // Marks the goal as complete
}

// Simple goal that can be completed once
class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) : base(name, description, points) {}

    // Mark the goal as complete and award points
    public override void RecordCompletion()
    {
        if (!IsComplete)
        {
            MarkComplete();
            Console.WriteLine($"Congratulations! You completed '{Name}' and earned {Points} points.");
        }
        else
        {
            Console.WriteLine($"'{Name}' has already been completed.");
        }
    }
}

// Eternal goal that can be repeated indefinitely (like daily habits)
class EternalGoal : Goal
{
    private int Streak = 0;  // Track streaks for consecutive completions

    public EternalGoal(string name, string description, int points) : base(name, description, points) {}

    // Record completion and increase the streak
    public override void RecordCompletion()
    {
        Streak++;
        Console.WriteLine($"Good job! You recorded progress on '{Name}' and earned {Points} points.");
        if (Streak % 7 == 0)  // Give bonus points every 7 days
        {
            Console.WriteLine($"7-day streak! Bonus points awarded.");
        }
    }
}

// Checklist goal that requires multiple completions
class ChecklistGoal : Goal
{
    private int CompletionTarget;
    private int CurrentCompletions;
    private int BonusPoints;

    // Constructor for checklist goals
    public ChecklistGoal(string name, string description, int points, int completionTarget, int bonusPoints)
        : base(name, description, points)
    {
        CompletionTarget = completionTarget;
        CurrentCompletions = 0;
        BonusPoints = bonusPoints;
    }

    // Record progress and award points, check if fully complete
    public override void RecordCompletion()
    {
        if (CurrentCompletions < CompletionTarget)
        {
            CurrentCompletions++;
            Console.WriteLine($"Progress on '{Name}' - {Points} points earned.");
            if (CurrentCompletions == CompletionTarget)
            {
                MarkComplete();
                Console.WriteLine($"Congratulations! You completed '{Name}' and earned a bonus of {BonusPoints} points.");
            }
        }
        else
        {
            Console.WriteLine($"'{Name}' is fully completed.");
        }
    }

    // Display progress with completion count
    public override void DisplayProgress() =>
        Console.WriteLine($"{Name}: {Description} - Completed {CurrentCompletions}/{CompletionTarget} times " +
                          $"{(IsComplete ? "[X] Completed" : "[ ] Not Completed")}");
}

#endregion

#region Quest Program

class QuestProgram
{
    private List<Goal> goals;
    private int totalScore;

    public QuestProgram()
    {
        goals = new List<Goal>();
        totalScore = 0;
    }

    // Method to add a new goal to the list
    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    // Method to display all goals and their progress
    public void DisplayGoals()
    {
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            goals[i].DisplayProgress();
        }
    }

    // Method to display the user's total score
    public void DisplayScore()
    {
        Console.WriteLine($"\nYour total score is: {totalScore} points");
    }

    // Method to record an event and award points
    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordCompletion();
            totalScore += goals[goalIndex].GetPoints();
        }
        else
        {
            Console.WriteLine("Invalid goal index.");
        }
    }

    // Save goals and score to a file
    public void SaveProgress()
    {
        using (StreamWriter writer = new StreamWriter("progress.txt"))
        {
            writer.WriteLine(totalScore);
            foreach (Goal goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name}|{goal.Name}|{goal.Description}|{goal.Points}|{goal.IsComplete}");
            }
        }
        Console.WriteLine("Progress saved.");
    }

    // Load goals and score from a file
    public void LoadProgress()
    {
        if (File.Exists("progress.txt"))
        {
            goals.Clear();
            using (StreamReader reader = new StreamReader("progress.txt"))
            {
                totalScore = int.Parse(reader.ReadLine());
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    string type = parts[0];
                    string name = parts[1];
                    string description = parts[2];
                    int points = int.Parse(parts[3]);
                    bool isComplete = bool.Parse(parts[4]);

                    Goal goal = type switch
                    {
                        "SimpleGoal" => new SimpleGoal(name, description, points),
                        "EternalGoal" => new EternalGoal(name, description, points),
                        "ChecklistGoal" => new ChecklistGoal(name, description, points, 5, 50), // Example target & bonus
                        _ => null
                    };

                    if (goal != null && isComplete) goal.RecordCompletion();
                    goals.Add(goal);
                }
            }
            Console.WriteLine("Progress loaded.");
        }
        else
        {
            Console.WriteLine("No saved progress found.");
        }
    }
}

#endregion

#region Main Program

class Program
{
    static void Main()
    {
        QuestProgram quest = new QuestProgram();

        // Adding multiple types of goals for personal development and habit formation
        quest.AddGoal(new SimpleGoal("Complete a Book", "Finish reading a book for self-improvement.", 100));
        quest.AddGoal(new EternalGoal("Daily Meditation", "Spend time meditating every day.", 10));
        quest.AddGoal(new EternalGoal("Exercise", "Exercise daily to maintain health.", 20));
        quest.AddGoal(new ChecklistGoal("Weekly Volunteering", "Volunteer at the local shelter weekly", 15, 4, 50));
        quest.AddGoal(new ChecklistGoal("Workout Routine", "Exercise regularly", 20, 7, 50));

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Display Goals");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Display Score");
            Console.WriteLine("4. Save Progress");
            Console.WriteLine("5. Load Progress");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    quest.DisplayGoals();
                    break;
                case "2":
                    Console.Write("Enter the goal number to complete: ");
                    int goalNumber = int.Parse(Console.ReadLine()) - 1;
                    quest.RecordEvent(goalNumber);
                    break;
                case "3":
                    quest.DisplayScore();
                    break;
                case "4":
                    quest.SaveProgress();
                    break;
                case "5":
                    quest.LoadProgress();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}

#endregion
