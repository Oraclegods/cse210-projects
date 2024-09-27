using System;

class Program
{
    static void Main(string[] args)
    {
        {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();
        int gradePercentage = int.Parse(input);

        // Determine the letter grade
        string letter = "";
        string sign = "";

        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Determine if the user passed the course
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Sorry, you did not pass the course. Better luck next time!");
        }

        // Stretch Challenge: Determine the sign for the grade
        if (letter != "A" && letter != "F") // Exclude A+ and F+ or F-
        {
            int lastDigit = gradePercentage % 10;

            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
            else
            {
                sign = ""; // No sign
            }
        }
        else if (letter == "A" && gradePercentage < 93) // Handle A- case
        {
            sign = "-";
        }

        // Print out the final grade with letter and sign
        Console.WriteLine($"Your grade is: {letter}{sign}");
    }
}
    }
}