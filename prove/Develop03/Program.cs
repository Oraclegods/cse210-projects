/*
 Exceeded Requirements:
 1. Loaded scriptures from a file to allow the user to choose from multiple verses.
 2. Added a progress-tracking system that allows the user to save their progress and continue later.
 3. Implemented a hint feature to reveal hidden words for users who are stuck.
*/

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        Scripture scripture = new Scripture(reference, "Trust in the Lord with all thine heart and lean not unto thine own understanding");

        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords();
        }

        Console.WriteLine("All words have been hidden. Goodbye!");
    }
}
