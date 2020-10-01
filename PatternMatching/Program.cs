using System;

namespace PatternMatching
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-*-*-*-*-* Pattern.PatternMain *-*-*-*-*-");
            Pattern.PatternMain();

            Console.ReadLine();
        }
    }

    public class Square
    {
        public double Side { get; }
        public double Area
        {
            get
            {
                return Math.Pow(Side, 2.0);
            }
        }
        public Square(double side)
        {
            Side = side;
        }
    }
    public class Circle
    {
        public double Radius { get; }
        public double Area
        {
            get
            {
                return Math.Pow(Radius, 2.0) * Math.PI;
            }
        }
        public Circle(double radius)
        {
            Radius = radius;
        }
    }
    public struct Rectangle
    {
        public double Length { get; }
        public double Height { get; }
        public double Area
        {
            get
            {
                return Length * Height;
            }
        }
        public Rectangle(double length, double height)
        {
            Length = length;
            Height = height;
        }
    }
    public class Triangle
    {
        public double Base { get; }
        public double Height { get; }
        public double Area
        {
            get
            {
                return (Base * Height) / 2;
            }
        }
        public Triangle(double @base, double height)
        {
            Base = @base;
            Height = height;
        }
    }

    internal static class Pattern
    {

        /// <summary>
        /// C# 7.0 or higher
        /// </summary>
        public static double ComputeArea(object shape)
        {
            if (shape is Square square)
            {
                var s = square;
                return s.Area;
            }
            else if (shape is Circle circle)
            {
                var c = circle;
                return c.Area;
            }
            else
            {
                throw new ArgumentException(
                   message: "shape is not a recognized shape",
                   paramName: nameof(shape)
                   );
            }
        }
        public static double ComputeAreaSwitch(object shape)
        {
            switch (shape)
            {
                case Square s:
                    return s.Area;
                case Circle c:
                    return c.Area;
                case Rectangle r:
                    return r.Area;
                default:
                    throw new ArgumentException(
                        message: "shape is not a recognized shape",
                        paramName: nameof(shape));
            }
        }
        public static double ComputeAreaWhen1(object shape)
        {
            switch (shape)
            {
                // default value in these conditions.
                case Square s when s.Side == 0:
                case Circle c when c.Radius == 0:
                case Triangle t when t.Base == 0 || t.Height == 0:
                case Rectangle r when r.Length == 0 || r.Height == 0:
                    return 0;

                case Square s:
                    return s.Area;
                case Circle c:
                    return c.Area;
                case Triangle t:
                    return t.Area;
                case Rectangle r:
                    return r.Area;

                // check the null situation
                case null:
                    throw new ArgumentNullException(paramName: nameof(shape), message: "Shape must not be null");

                default:
                    throw new ArgumentException(
                        message: "shape is not a recognized shape",
                        paramName: nameof(shape));
            }
        }
        public static double ComputeArea_VeryShort(object shape)
        {
            // c# 8.0
            return shape switch
            {
                Square s when s.Side == 0 => 0,
                Circle c when c.Radius == 0 => 0,
                Triangle t when t.Base == 0 || t.Height == 0 => 0,
                Rectangle r when r.Length == 0 || r.Height == 0 => 0,

                Square s => s.Area,
                Circle c => c.Area,
                Rectangle r => r.Area,

                null => throw new ArgumentNullException(paramName: nameof(shape), message: "Shape must not be null"),

                // these 2 handles any other statements.
                // 1)
                //_ => throw new ArgumentException(
                //message: "shape is not a recognized shape",
                //paramName: nameof(shape)),
                // 2)
                { } => throw new ArgumentException(
                message: "shape is not a recognized shape",
                paramName: nameof(shape)),
            };
        }

        public static void Pattern1()
        {
            var obj = new Circle(5.0);
            var result = ComputeArea_VeryShort(obj);
            Console.WriteLine($"{obj.GetType()} area is {result}");
        }

        public static object CreateShape(string Description)
        {
            switch (Description)
            {
                case "circle":
                    return new Circle(2);
                case "square":
                    return new Circle(2);
                case "large-circle":
                    return new Circle(30);

                // preceding code uses the ?. operator to ensure that it doesn't accidentally throw a NullReferenceException. The default case handles any other string values that aren't understood by this command parser.
                case var o when (o?.Trim().Length ?? 0) == 0:
                    return new Circle(6);

                default:
                    return "invalid shape description";
            }
        }
        public static object CreateShape_VeryShort(string Description)
        {
            // c# 8.0
            return Description switch
            {
                "circle" => new Circle(2),
                "square" => new Square(16),
                "large-circle" => new Circle(30),
                // preceding code uses the ?. operator to ensure that it doesn't accidentally throw a NullReferenceException. The default case handles any other string values that aren't understood by this command parser.
                var o when (o?.Trim().Length ?? 0) == 0 => new Circle(2),
                _ => "invalid shape description",
            };
        }

        public static void Pattern2()
        {
            var obj = CreateShape_VeryShort("square");
            var result = ComputeArea_VeryShort(obj);
            Console.WriteLine($"{obj.GetType()} area is {result}");
        }
        public static void PatternMain()
        {
            Console.WriteLine("-*-*-*-*-* Pattern1 *-*-*-*-*-");
            Pattern1();
            Console.WriteLine("-*-*-*-*-* Pattern2 *-*-*-*-*-");
            Pattern2();
        }
    }
}
