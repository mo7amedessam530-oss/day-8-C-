using System;

namespace CsharpDay06Giza01
{
    // 1. Interfaces & Vehicles
    public interface IVehicle
    {
        void StartEngine();
        void StopEngine();
    }

    public class Car : IVehicle
    {
        public void StartEngine() => Console.WriteLine("Car engine started.");
        public void StopEngine() => Console.WriteLine("Car engine stopped.");
    }

    public class Bike : IVehicle
    {
        public void StartEngine() => Console.WriteLine("Bike engine started.");
        public void StopEngine() => Console.WriteLine("Bike engine stopped.");
    }

    // 2. Shape Series
    public interface IShapeSeries
    {
        int CurrentShapeArea { get; set; }
        void GetNextArea();
        void ResetSeries();
    }

    public class SquareSeries : IShapeSeries
    {
        private int currentIndex = 0;
        public int CurrentShapeArea { get; set; }

        public void GetNextArea()
        {
            currentIndex++;
            CurrentShapeArea = currentIndex * currentIndex;
        }

        public void ResetSeries()
        {
            currentIndex = 0;
            CurrentShapeArea = 0;
        }
    }

    public class CircleSeries : IShapeSeries
    {
        private int radius = 0;
        public int CurrentShapeArea { get; set; }

        public void GetNextArea()
        {
            radius++;
            CurrentShapeArea = (int)(Math.PI * radius * radius);
        }

        public void ResetSeries()
        {
            radius = 0;
            CurrentShapeArea = 0;
        }
    }

    // 3. Geometric Shape Hierarchy
    public abstract class GeometricShape
    {
        public double Dimension1 { get; set; }
        public double Dimension2 { get; set; }

        public abstract double CalculateArea();
        public abstract double Perimeter { get; }
    }

    public class Triangle : GeometricShape
    {
        public override double CalculateArea() => 0.5 * Dimension1 * Dimension2;
        public override double Perimeter => Dimension1 + Dimension2 + Math.Sqrt((Dimension1 * Dimension1) + (Dimension2 * Dimension2));
    }

    public class RectangleShape : GeometricShape
    {
        public override double CalculateArea() => Dimension1 * Dimension2;
        public override double Perimeter => 2 * (Dimension1 + Dimension2);
    }

    public class ShapeModel : IComparable<ShapeModel>
    {
        public string Name { get; set; } = string.Empty;
        public double Area { get; set; }

        public int CompareTo(ShapeModel? other)
        {
            if (other == null) return 1;
            return this.Area.CompareTo(other.Area);
        }
    }

    // 4. Factory Pattern & Sorting
    public class ShapeFactory
    {
        public static GeometricShape CreateShape(string shapeType, double dim1, double dim2)
        {
            if (shapeType.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
            {
                return new RectangleShape { Dimension1 = dim1, Dimension2 = dim2 };
            }
            else if (shapeType.Equals("Triangle", StringComparison.OrdinalIgnoreCase))
            {
                return new Triangle { Dimension1 = dim1, Dimension2 = dim2 };
            }
            throw new ArgumentException("Invalid shape type");
        }
    }

    public class SortingAlgorithms
    {
        public static void SelectionSort(int[] numbers)
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (numbers[j] < numbers[minIndex])
                    {
                        minIndex = j;
                    }
                }
                int temp = numbers[minIndex];
                numbers[minIndex] = numbers[i];
                numbers[i] = temp;
            }
        }
    }

    // 5. Program Entry Point
    class Program
    {
        static void PrintTenShapes(IShapeSeries series)
        {
            series.ResetSeries();
            Console.WriteLine($"--- Printing 10 Shapes for {series.GetType().Name} ---");
            for (int i = 0; i < 10; i++)
            {
                series.GetNextArea();
                Console.WriteLine($"Shape {i + 1} Area: {series.CurrentShapeArea}");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            IShapeSeries squareSeries = new SquareSeries();
            PrintTenShapes(squareSeries);

            IShapeSeries circleSeries = new CircleSeries();
            PrintTenShapes(circleSeries);

            GeometricShape rect = ShapeFactory.CreateShape("Rectangle", 5.0, 10.0);
            Console.WriteLine($"Rectangle Area: {rect.CalculateArea()}, Perimeter: {rect.Perimeter}");

            int[] areas = { 45, 12, 78, 3, 22, 90, 15 };
            SortingAlgorithms.SelectionSort(areas);
            Console.WriteLine("\nSorted Areas using SelectionSort:");
            Console.WriteLine(string.Join(", ", areas));
        }
    }
}