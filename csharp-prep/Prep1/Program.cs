using System;

class Program
{
    // asking a uer for their names
    static void Main(string[] args)
    {
        Console.Write("What is your name? ");
        string first = Console.ReadLine();

        Console.Write("What is your last name? ");
        String last = Console.ReadLine();

        Console.WriteLine($"Your name is {last}, {first} {last}");
    }
}