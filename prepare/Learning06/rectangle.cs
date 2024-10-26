// Rectangle.cs
using System;

public class Rectangle : Shape
{
    private double _width;
    private double _height;

    // Constructor
    public Rectangle(string color, double width, double height) : base(color)
    {
        _width = width;
        _height = height;
    }

    // Override GetArea method
    public override double GetArea()
    {
        return _width * _height;
    }
}
