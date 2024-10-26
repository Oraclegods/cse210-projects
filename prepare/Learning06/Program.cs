// Program.cs
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create instances of Square, Rectangle, and Circle
        Square square = new Square("Red", 4.0);
        Rectangle rectangle = new Rectangle("Blue", 5.0, 3.0);
        Circle circle = new Circle("Green", 2.5);

        // Test individual instances
        Console.WriteLine($"Square Color: {square.GetColor()}, Area: {square.GetArea()}");
        Console.WriteLine($"Rectangle Color: {rectangle.GetColor()}, Area: {rectangle.GetArea()}");
        Console.WriteLine($"Circle Color: {circle.GetColor()}, Area: {circle.GetArea()}");

        // Create a list of shapes and add instances
        List<Shape> shapes = new List<Shape> { square, rectangle, circle };

        // Iterate through the list and display each shape's color and area
        Console.WriteLine("\nShape List:");
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Shape Color: {shape.GetColor()}, Area: {shape.GetArea()}");
        }
    }
}
