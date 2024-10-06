using System;

class Fraction
{
    private int _numerator;
    private int _denominator;

    // Constructor 1: No parameters, initializes to 1/1
    public Fraction()
    {
        _numerator = 1;
        _denominator = 1;
    }

    // Constructor 2: One parameter for the numerator, denominator defaults to 1
    public Fraction(int numerator)
    {
        _numerator = numerator;
        _denominator = 1;
    }

    // Constructor 3: Two parameters for numerator and denominator
    public Fraction(int numerator, int denominator)
    {
        _numerator = numerator;
        // Ensure denominator is not zero
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        _denominator = denominator;
    }

    // Method to get the fraction as a string "numerator/denominator"
    public string GetFractionString()
    {
        return $"{_numerator}/{_denominator}";
    }

    // Method to get the decimal value of the fraction
    public double GetDecimalValue()
    {
        return (double)_numerator / _denominator;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create fractions using different constructors
        Fraction frac1 = new Fraction(); // 1/1
        Fraction frac2 = new Fraction(6); // 6/1
        Fraction frac3 = new Fraction(3, 4); // 3/4

        // Display the fractions and their decimal values
        Console.WriteLine("Fraction 1 (using first constructor):");
        Console.WriteLine(frac1.GetFractionString()); // 1/1
        Console.WriteLine(frac1.GetDecimalValue());   // 1.0

        Console.WriteLine("\nFraction 2 (using second constructor):");
        Console.WriteLine(frac2.GetFractionString()); // 6/1
        Console.WriteLine(frac2.GetDecimalValue());   // 6.0

        Console.WriteLine("\nFraction 3 (using third constructor):");
        Console.WriteLine(frac3.GetFractionString()); // 3/4
        Console.WriteLine(frac3.GetDecimalValue());   // 0.75
    }
}
