// See https://aka.ms/new-console-template for more information
using System;


public class Shape
{
    public string Name { get; set; }

    public virtual double CalculateArea()
    {
        return 0;
    }
}

public class Circle :Shape
{
    public double Radius { get; set; }

    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle :Shape
{
    public double Height { get; set; }

    public double Width { get; set; }

    public override double CalculateArea()
    {
        return Height * Width;
    }
}


public class Triangle : Shape
{
    public double Base { get; set; }

    public double TriangleHeight { get; set; }


    public override double CalculateArea()
    { 
        return (Base * TriangleHeight) *0.5;
    }
}


class Program
{
   public static void PrintShapeArea(Shape shape)
    {
        Console.WriteLine($"Shape: {shape.Name}, Area :{shape.CalculateArea()}");
    }

    public static void Main(string[] args)
    {
        Circle circle = new Circle { Name = "Circle", Radius = 5 };
        Rectangle rectangle = new Rectangle { Name = "Rectangle", Width = 4, Height = 6 };
        Triangle triangle = new Triangle { Name = "Triangle", Base = 3, TriangleHeight = 4 };


        PrintShapeArea(circle);
        PrintShapeArea(rectangle);
        PrintShapeArea(triangle);
    }
} 