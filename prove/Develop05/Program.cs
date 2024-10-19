using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Exceeding Requirements:
    // The program keeps a log of how many times each activity was performed.
    // This feature encourages users to engage with the app more regularly and track their mindfulness journey.

    // Base class for mindfulness activities
    abstract class MindfulnessActivity
    {
        protected int Duration; // Duration for the activity

        public virtual void StartActivity()
        {
            // Common starting procedure for all activities
            Console.Write("Enter the duration for this activity in seconds: ");
            Duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Prepare to begin...");
            Thread.Sleep(3000); // Pause before starting
        }

        public virtual void FinishActivity()
        {
            // Common finishing procedure for all activities
            Console.WriteLine("Great job! You have completed the activity.");
            Thread.Sleep(2000);
            Console.WriteLine($"Duration: {Duration} seconds.");
            Thread.Sleep(2000);
        }

        protected void ShowSpinner(int seconds)
        {
            // Displays a spinner for the specified number of seconds
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".", Console.CursorLeft);
                Thread.Sleep(1000); // Simulate delay
            }
            Console.WriteLine();
        }
    }

    // Breathing Activity class
    class BreathingActivity : MindfulnessActivity
    {
        public override void StartActivity()
        {
            base.StartActivity(); // Call base class startActivity
            Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");

            DateTime endTime = DateTime.Now.AddSeconds(Duration); // Set end time for activity
            while (DateTime.Now < endTime)
            {
                Console.WriteLine("Breathe in...");
                ShowSpinner(3); // Pause with spinner
                Console.WriteLine("Breathe out...");
                ShowSpinner(3); // Pause with spinner
            }

            FinishActivity(); // Call base class finishActivity
        }
    }

    // Reflection Activity class
    class ReflectionActivity : MindfulnessActivity
    {
        private readonly string[] prompts = {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need."
        };

        private readonly string[] questions = {
            "Why was this experience meaningful to you?",
            "How did you get started?",
            "What did you learn about yourself through this experience?"
        };

        public override void StartActivity()
        {
            base.StartActivity(); // Call base class startActivity
            Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
            Console.WriteLine(prompts[new Random().Next(prompts.Length)]); // Display a random prompt

            DateTime endTime = DateTime.Now.AddSeconds(Duration); // Set end time for activity
            while (DateTime.Now < endTime)
            {
                Console.WriteLine(questions[new Random().Next(questions.Length)]); // Select a random question
                ShowSpinner(5); // Pause with spinner
            }

            FinishActivity(); // Call base class finishActivity
        }
    }

    // Listing Activity class
    class ListingActivity : MindfulnessActivity
    {
        private readonly string[] prompts = {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?"
        };

        public override void StartActivity()
        {
            base.StartActivity(); // Call base class startActivity
            Console.WriteLine("This activity will help you reflect on the good things in your life.");
            Console.WriteLine(prompts[new Random().Next(prompts.Length)]); // Display a random prompt

            DateTime startTime = DateTime.Now; // Start time for activity
            List<string> items = new List<string>(); // List to store user inputs

            // User lists items until the duration is reached
            while (DateTime.Now < startTime.AddSeconds(Duration))
            {
                Console.Write("List an item (or type 'done' to finish): ");
                string item = Console.ReadLine();
                if (item.ToLower() == "done")
                {
                    break; // Exit loop if user types 'done'
                }
                items.Add(item); // Add item to the list
            }

            Console.WriteLine($"You listed {items.Count} items."); // Display the count of items listed
            FinishActivity(); // Call base class finishActivity
        }
    }

    // Main function to run the application
    class Program
    {
        static void Main(string[] args)
        {
            var activities = new Dictionary<string, Func<MindfulnessActivity>>()
            {
                { "1", () => new BreathingActivity() },
                { "2", () => new ReflectionActivity() },
                { "3", () => new ListingActivity() }
            };

            // Log to keep track of how many times each activity is performed
            var activityLog = new Dictionary<string, int>
            {
                { "Breathing", 0 },
                { "Reflection", 0 },
                { "Listing", 0 }
            };

            while (true)
            {
                // Display menu options
                Console.WriteLine("Choose an activity:");
                Console.WriteLine("1: Breathing Activity");
                Console.WriteLine("2: Reflection Activity");
                Console.WriteLine("3: Listing Activity");
                Console.WriteLine("4: Exit");

                string choice = Console.ReadLine();
                if (choice == "4")
                {
                    break; // Exit the loop
                }

                if (activities.TryGetValue(choice, out Func<MindfulnessActivity> activityFunc))
                {
                    MindfulnessActivity activity = activityFunc();
                    activity.StartActivity(); // Start the selected activity

                    // Increment the activity log for the chosen activity
                    if (choice == "1")
                    {
                        activityLog["Breathing"]++;
                    }
                    else if (choice == "2")
                    {
                        activityLog["Reflection"]++;
                    }
                    else if (choice == "3")
                    {
                        activityLog["Listing"]++;
                    }

                    // Display activity log
                    Console.WriteLine("\nActivity Log:");
                    foreach (var log in activityLog)
                    {
                        Console.WriteLine($"{log.Key}: {log.Value} times");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again."); // Handle invalid input
                }
            }
        }
    }
}
