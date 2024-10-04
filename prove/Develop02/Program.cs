/* 
  Exceeding Requirements:
  
  To exceed the core requirements, this program allows saving and loading the journal in JSON format.
  - `SaveJournalAsJson()` saves the entire journal as a structured JSON file.
  - `LoadJournalFromJson()` loads the journal entries from a JSON file, replacing the current entries.

  JSON format provides more flexibility for data exchange, enabling users to share or process their journal entries using other tools.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json; // Import for JSON serialization

namespace JournalApp
{
    // Entry class to represent a journal entry
    public class Entry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        // Constructor to initialize a new journal entry
        public Entry(string prompt, string response)
        {
            Prompt = prompt;
            Response = response;
            Date = DateTime.Now.ToShortDateString(); // Store date as string
        }

        // Method to display the entry
        public void Display()
        {
            Console.WriteLine($"{Date}: {Prompt}");
            Console.WriteLine($"Response: {Response}\n");
        }
    }

    // Journal class to manage journal entries
    public class Journal
    {
        private List<Entry> _entries = new List<Entry>(); // Stores list of entries
        private List<string> _prompts = new List<string> // List of random prompts
        {
            "Who was the most interesting person you interacted with today?",
            "What was the best part of your day?",
            "How did you see the hand of the Lord in your life today?",
            "What was the strongest emotion you felt today?",
            "If you had one thing you could do over today, what would it be?"
        };

        // Method to add a new entry
        public void AddEntry()
        {
            Random rand = new Random();
            string prompt = _prompts[rand.Next(_prompts.Count)]; // Random prompt
            Console.WriteLine(prompt); // Display prompt to user
            string response = Console.ReadLine(); // Get user response
            _entries.Add(new Entry(prompt, response)); // Add new entry to the list
        }

        // Method to display all journal entries
        public void DisplayJournal()
        {
            foreach (var entry in _entries)
            {
                entry.Display(); // Call the Entry class' display method
            }
        }

        // Method to save the journal to a JSON file
        public void SaveJournalAsJson(string filename)
        {
            string jsonString = JsonSerializer.Serialize(_entries);
            File.WriteAllText(filename, jsonString);
            Console.WriteLine($"Journal saved as {filename}");
        }

        // Method to load the journal from a JSON file
        public void LoadJournalFromJson(string filename)
        {
            string jsonString = File.ReadAllText(filename);
            _entries = JsonSerializer.Deserialize<List<Entry>>(jsonString);
            Console.WriteLine($"Journal loaded from {filename}");
        }

        // Method to save the journal to a text file (fallback option)
        public void SaveJournalAsText(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in _entries)
                {
                    writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }
        }

        // Method to load the journal from a text file (fallback option)
        public void LoadJournalFromText(string filename)
        {
            _entries.Clear(); // Clear current entries before loading new ones
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        _entries.Add(new Entry(parts[1], parts[2]) { Date = parts[0] });
                    }
                }
            }
        }
    }

    // Program class to manage the menu and user interface
    public class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal(); // Create a new journal instance
            while (true)
            {
                Console.WriteLine("Journal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a JSON file");
                Console.WriteLine("4. Load the journal from a JSON file");
                Console.WriteLine("5. Save the journal to a text file");
                Console.WriteLine("6. Load the journal from a text file");
                Console.WriteLine("7. Quit");

                string choice = Console.ReadLine(); // Get user input

                switch (choice)
                {
                    case "1":
                        journal.AddEntry(); // Add a new journal entry
                        break;
                    case "2":
                        journal.DisplayJournal(); // Display all journal entries
                        break;
                    case "3":
                        Console.Write("Enter filename to save (e.g., journal.json): ");
                        string saveJsonFilename = Console.ReadLine();
                        journal.SaveJournalAsJson(saveJsonFilename); // Save journal as JSON
                        break;
                    case "4":
                        Console.Write("Enter filename to load (e.g., journal.json): ");
                        string loadJsonFilename = Console.ReadLine();
                        journal.LoadJournalFromJson(loadJsonFilename); // Load journal from JSON
                        break;
                    case "5":
                        Console.Write("Enter filename to save (e.g., journal.txt): ");
                        string saveTextFilename = Console.ReadLine();
                        journal.SaveJournalAsText(saveTextFilename); // Save journal as text
                        break;
                    case "6":
                        Console.Write("Enter filename to load (e.g., journal.txt): ");
                        string loadTextFilename = Console.ReadLine();
                        journal.LoadJournalFromText(loadTextFilename); // Load journal from text
                        break;
                    case "7":
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}

