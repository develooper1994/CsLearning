using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

// Summary: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/walkthrough-writing-queries-linq
// A very good example game with Linq: https://github.com/dotnet/try-samples/blob/master/notebooks/linq/index.ipynb

// 1) Let the Compiler Handle Generic Type Declarations for "GENERICS"

namespace LINQs
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-*-*-*-*-* Linq1.Linq1Main() *-*-*-*-*-");
            LinqBasic.Linq1Main();

            Console.WriteLine("\n-*-*-*-*-* LinqSyntax.LinqSyntaxMain() *-*-*-*-*-");
            LinqSyntax.LinqSyntaxMain();


            Console.WriteLine("\n-*-*-*-*-* LinqExecution.LinqExecutionMain() *-*-*-*-*-");
            LinqExecution.LinqExecutionMain();

            Console.WriteLine("\n-*-*-*-*-* LinqKeywords.LinqKeywordsMain() *-*-*-*-*-");
            LinqKeywords.LinqKeywordsMain();

            Console.WriteLine("\n-*-*-*-*-* LinqDataTransform.LinqDataTransformMain() *-*-*-*-*-");
            LinqDataTransform.LinqDataTransformMain();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

    internal static class LinqBasic
    {
        public static void Linq1Main(string region = "consume-sequence")
        {
            var result = region switch
            {
                "consume-sequence" => ConsumeSequence(),
                _ => throw new ArgumentException("A --region argument must be passed", nameof(region))
            };
            Console.WriteLine($"Query result is: {result}");
        }

        internal static IEnumerable<int> ConsumeSequence()
        {
            #region consume-sequence
            var sequence = Enumerable.Range(1, 10).Select(x => x);  // x => new Random().Next(1,20)
            var squareOfOddNumbers = from n in sequence
                                     where n % 2 == 0
                                     // orderby n ascending  // ascending and descending. default: ascending
                                     select n * n;

            foreach (var item in squareOfOddNumbers)
            {
                Console.Write($"{item}, ");
            }
            return squareOfOddNumbers;

            #endregion
        }

    }

    internal static class LinqSyntax
    {
        public static void LinqSyntaxMain()
        {
            int[] numbers = { 5, 10, 8, 3, 6, 12 };
            Console.WriteLine("-*-*-*-*-* LinqSyntaxQuery() *-*-*-*-*-");
            LinqSyntaxQuery(numbers);
            Console.WriteLine("-*-*-*-*-* LinqSyntaxMethod() *-*-*-*-*-");
            LinqSyntaxMethod(numbers);
        }
        public static void LinqSyntaxQuery(int[] numbers)
        {
            //Query syntax:
            IEnumerable<int> numQuery1 =
                from num in numbers
                where num % 2 == 0
                orderby num
                select num;

            foreach (int i in numQuery1)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(System.Environment.NewLine);
        }
        public static void LinqSyntaxMethod(int[] numbers)
        {
            //Method syntax:
            IEnumerable<int> numQuery2 =
                numbers
                .Where(num => num % 2 == 0)
                .OrderBy(n => n);

            foreach (int i in numQuery2)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(System.Environment.NewLine);
        }
    }

    internal static class LinqExecution
    {
        public static void LinqExecutionMain()
        {
            int[] numbers = { 5, 10, 8, 3, 6, 12 };
            Console.WriteLine("-*-*-*-*-* LazyExecution() *-*-*-*-*-");
            LazyExecution(numbers);
            Console.WriteLine("-*-*-*-*-* ImmediateExecution() *-*-*-*-*-");
            ImmediateExecution(numbers);
        }
        private static void LazyExecution(int[] numbers)
        {
            var evenNumQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            int evenNumCount = evenNumQuery.Count();
            Console.WriteLine($"evenNumCount: {evenNumCount}");
        }
        private static void ImmediateExecution(int[] numbers)
        {
            ///<summary>
            /// To force immediate execution of any query and cache its results, you can call the ToList or ToArray methods.
            /// </summary>
            List<int> numQuery2 =
                (from num in numbers
                 where (num % 2) == 0
                 select num).ToList();

            // or like this:
            // numQuery3 is still an int[]

            int[] numQuery3 =
                (from num in numbers
                 where (num % 2) == 0
                 select num).ToArray();
            foreach (int i in numQuery3)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine(System.Environment.NewLine);
        }


    }

    internal static class LinqKeywords
    {
        private class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public List<int> Scores;
        }
        public static void LinqKeywordsMain()
        {
            Console.WriteLine("-*-*-*-*-* GROUPBY() *-*-*-*-*-");
            GROUPBY();

            Console.WriteLine("-*-*-*-*-* GROUPBY_bool() *-*-*-*-*-");
            GROUPBY_bool();

            Console.WriteLine("-*-*-*-*-* GROUPBY_numeric() *-*-*-*-*-");
            GROUPBY_numeric();

            Console.WriteLine("-*-*-*-*-* LET() *-*-*-*-*-");
            LET();

        }
        public static void GROUPBY()
        {
            string[] words = { "böğürtlen", "şempanze", "abaküs", "muz", "elma", "peynir", "ayakkabı", "battaniye", "ezogelin çorbası", "paspas", "maliye", "şambali" };

            // Create the query.
            var wordGroups =
                from w in words
                group w by w[0];

            // Execute the query.
            foreach (var wordGroup in wordGroups)
            {
                Console.WriteLine("Words that start with the letter '{0}':", wordGroup.Key);
                foreach (var word in wordGroup)
                {
                    Console.WriteLine("   {0}", word);
                }
            }
        }
        public static void GROUPBY_bool()
        {
            List<Student> students = new List<Student>
            {
               new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 72, 81, 60}},
               new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
               new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {99, 89, 91, 95}},
               new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {72, 81, 65, 84}},
               new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {97, 89, 85, 82}}
            };

            // Group by true or false.
            // Query variable is an IEnumerable<IGrouping<bool, Student>>
            var booleanGroupQuery =
                from student in students
                group student by student.Scores.Average() >= 80; //pass or fail!

            // Execute the query and access items in each group
            foreach (var studentGroup in booleanGroupQuery)
            {
                Console.WriteLine(studentGroup.Key == true ? "High averages" : "Low averages");
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }
        }
        public static void GROUPBY_numeric()
        {
            List<Student> students = new List<Student>
            {
               new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 72, 81, 60}},
               new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
               new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {99, 89, 91, 95}},
               new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {72, 81, 65, 84}},
               new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {97, 89, 85, 82}}
            };

            // Group by true or false.
            // Query variable is an IEnumerable<IGrouping<bool, Student>>
            var studentQuery =
                from student in students
                let avg = Convert.ToInt32(student.Scores.Average())
                group student by (avg / 10) into g
                orderby g.Key
                select g;

            // Execute the query and access items in each group
            foreach (var studentGroup in studentQuery)
            {
                int temp = studentGroup.Key * 10;
                Console.WriteLine("Students with an average between {0} and {1}", temp, temp + 10);
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }
        }
        public static void LET()
        {
            string[] strings =
            {
                "A penny saved is a penny earned.",
                "The early bird catches the worm.",
                "The pen is mightier than the sword."
            };

            // Without using let, you would have to call ToLower in each predicate in the where clause.

            // Split the sentence into an array of words
            // and select those whose first letter is a vowel.
            var earlyBirdQuery =
                from sentence in strings
                let words = sentence.Split(' ')
                from word in words
                let w = word.ToLower()
                where w[0] == 'a' || w[0] == 'e'
                    || w[0] == 'i' || w[0] == 'o'
                    || w[0] == 'u'
                select word;

            // Execute the query.
            foreach (var v in earlyBirdQuery)
            {
                Console.WriteLine("\"{0}\" starts with a vowel", v);
            }

        }
    }

    internal static class LinqDataTransform
    {
        class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public List<int> Scores;
        }

        class Teacher
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public string City { get; set; }
        }

        public static void LinqDataTransformMain()
        {
            //Console.WriteLine("\n-*-*-*-*-* JoiningMultipleInputs() *-*-*-*-*-");
            //JoiningMultipleInputs();

            //Console.WriteLine("\n-*-*-*-*-* PerformingOperationsOnSourceElements() *-*-*-*-*-");
            //PerformingOperationsOnSourceElements();

            Console.WriteLine("\n-*-*-*-*-* TransformingInMemoryObjectsIntoXML() *-*-*-*-*-");
            TransformingInMemoryObjectsIntoXML();

        }

        private static void JoiningMultipleInputs()
        {

            List<Student> students = new List<Student>()
            {
                new Student { First="Svetlana",
                    Last="Omelchenko",
                    ID=111,
                    Street="123 Main Street",
                    City="Seattle",
                    Scores= new List<int> { 97, 92, 81, 60 } },
                new Student { First="Claire",
                    Last="O’Donnell",
                    ID=112,
                    Street="124 Main Street",
                    City="Redmond",
                    Scores= new List<int> { 75, 84, 91, 39 } },
                new Student { First="Sven",
                    Last="Mortensen",
                    ID=113,
                    Street="125 Main Street",
                    City="Lake City",
                    Scores= new List<int> { 88, 94, 65, 91 } },
            };

            // Create the second data source.
            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher { First="Ann", Last="Beebe", ID=945, City="Seattle" },
                new Teacher { First="Alex", Last="Robinson", ID=956, City="Redmond" },
                new Teacher { First="Michiyo", Last="Sato", ID=972, City="Tacoma" }
            };

            // Create the query.
            var studentsInSeattle =
                    from student in students
                    where student.City == "Seattle"
                    select student.Last;
            var teachersInSeattle =
                    from teacher in teachers
                    where teacher.City == "Seattle"
                    select teacher.Last;

            var peopleInSeattle =
                (studentsInSeattle).Concat(teachersInSeattle);

            Console.WriteLine("The following students and teachers live in Seattle:");
            // Execute the query.
            foreach (var person in peopleInSeattle)
            {
                Console.WriteLine(person);
            }
        }
        private static void PerformingOperationsOnSourceElements()
        {
            // Data source.
            double[] radii = { 1, 2, 3 };

            /*
            // LINQ query using method syntax.
            IEnumerable<string> output =
                radii.Select(r => $"Area for a circle with a radius of '{r}' = {r * r * Math.PI:F2}");
            */


            // LINQ query using query syntax.
            IEnumerable<string> output =
                from rad in radii
                select $"Area for a circle with a radius of '{rad}' = {rad * rad * Math.PI:F2}";


            foreach (string s in output)
            {
                Console.WriteLine(s);
            }
        }
        private static void TransformingInMemoryObjectsIntoXML()
        {
            // Create the data source by using a collection initializer.
            // The Student class was defined previously in this topic.
            List<Student> students = new List<Student>()
            {
                new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores = new List<int>{97, 92, 81, 60}},
                new Student {First="Claire", Last="O’Donnell", ID=112, Scores = new List<int>{75, 84, 91, 39}},
                new Student {First="Sven", Last="Mortensen", ID=113, Scores = new List<int>{88, 94, 65, 91}},
            };

            // Create the query.
            var studentsToXML = new XElement("Root",
                from student in students
                let scores = string.Join(",", student.Scores)
                select new XElement("student",
                           new XElement("First", student.First),
                           new XElement("Last", student.Last),
                           new XElement("Scores", scores)
                        ) // end "student"
                    ); // end "Root"

            // Execute the query.
            Console.WriteLine(studentsToXML);
        }

    }
}
