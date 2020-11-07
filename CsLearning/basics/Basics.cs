using System;
using System.Collections.Generic;

namespace CsLearning.Numbers
{
    static class Numeric
    {
        public static void numberMain()
        {
            Precision();
        }
        private static void Precision()
        {
            long minl = long.MinValue;
            long maxl = long.MaxValue;
            var aaa = "The range of " + minl.GetType().ToString() + " type is " + minl.ToString() + " to {maxl}";
            Console.WriteLine($"The range of {minl.GetType()} type is {minl} to {maxl}");

            double mind = double.MinValue;
            double maxd = double.MaxValue;
            Console.WriteLine($"The range of {mind.GetType()} type is {mind} to {maxd}");

            decimal min = decimal.MinValue;
            decimal max = decimal.MaxValue;
            Console.WriteLine($"The range of {min.GetType()} type is {min} to {max}");

            double a = 1.0;
            double b = 3.0;
            Console.WriteLine($"{a / b}");

            var a2 = 1.0M;
            decimal b2 = 3.0M;
            Console.WriteLine($"Greater precition: {a2 / b2}");


        }
    }
}

namespace CsLearning.Condition
{
    static class Conditional
    {
        public static void conditionMain()
        {
            //Conditions();
            //LoopWhile();
            //LoopDoWhile();
            //LoopFor();
            TurnBasedGame();
        }
        private static void Conditions()
        {
            int a = 5;
            int b = 3;
            if (a + b > 10)
                Console.WriteLine("The answer is greater than 10");
            else
                Console.WriteLine("The answer is not greater than 10");

            int c = 4;
            if ((a + b + c > 10) && (a > b))
            {
                Console.WriteLine("The answer is greater than 10");
                Console.WriteLine("And the first number is greater than the second");
            }
            else
            {
                Console.WriteLine("The answer is not greater than 10");
                Console.WriteLine("Or the first number is not greater than the second");
            }

            if ((a + b + c > 10) || (a > b))
            {
                Console.WriteLine("The answer is greater than 10");
                Console.WriteLine("Or the first number is greater than the second");
            }
            else
            {
                Console.WriteLine("The answer is not greater than 10");
                Console.WriteLine("And the first number is not greater than the second");
            }


        }
        private static void LoopWhile()
        {
            int counter = 0;
            while (counter < 10)
            {
                counter++;
                Console.WriteLine($"Hello to the while loop. The counter is: {counter}");
            }
        }
        private static void LoopDoWhile()
        {
            int counter = 0;
            do
            {
                counter++;
                Console.WriteLine($"Hello to the do-while loop. The counter is: {counter}");
            } while (counter < 10);
        }
        private static void LoopFor()
        {
            // a = counter++ == a = counter
            // counter = counter + 1

            // a = ++counter == a = counter + 1

            for (int counter = 0; counter < 10; ++counter)
            {
                Console.WriteLine($"Hello to the for loop. The counter is: {counter}");
            }
        }
        private static void TurnBasedGame()
        {
            int hero = 10;
            int monster = 10;

            const int maxAttack = 10;
            int turnCounter = 0;
            RNGCryptoServiceProvider weapon = new RNGCryptoServiceProvider();
            do
            {
                turnCounter++;
                Console.WriteLine($"-*-*-*-*-*-*-*-* turn: {turnCounter} has started *-*-*-*-*-*-*-*-");
                // hero attack to monster
                int attack = weapon.Next(1, maxAttack);
                monster -= attack;
                Console.WriteLine($"Monster was damaged lost {attack} healt and now has {monster}");

                // If the monster is already dead, the game is over.
                // go and check the healts.
                if (monster <= 0) continue;  // break


                // hero attack to monster
                attack = weapon.Next(1, maxAttack);
                hero -= attack;
                Console.WriteLine($"Hero was damaged lost {attack} healt and now has {hero}");


            } while (hero > 0 && monster > 0);

            Console.WriteLine($"Monster healt {monster}");
            Console.WriteLine($"Hero healt {hero}");
            Console.WriteLine(hero > monster ? "Hero wins!" : "Monster wins!");

        }
    }
}

namespace CsLearning.Colls
{
    static class Coll
    {
        public static void collMain()
        {
            //list();
            fibonacci();
        }
        private static void list()
        {
            var names = new List<string> { "WEIRDo", "bana", "suna" };

            Console.WriteLine($"how much data inside of the list: {names.Count}");

            names.Add("Mustafa");
            names.Add("Selçuk");

            Console.WriteLine($"how much data inside of the list: {names.Count}");

            names.Remove("suna");
            foreach (var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}");
            }
            Console.WriteLine($"first data of the list: {names[0]}");
            Console.WriteLine($"how much data inside of the list: {names.Count}\n\n");

            // IndexOf
            var index = names.IndexOf("suna");
            if (index != -1)
                Console.WriteLine($"Found at{index}");
            else
                Console.WriteLine("Can't found");
            Console.WriteLine("\n");

            //Sort
            names.Sort();
            foreach (var name in names)
            {
                Console.WriteLine($"Hello {name.ToUpper()}");
            }

        }
        private static void fibonacci(int NumberOfFibonacci = 50)
        {
            var fibonacciNumbers = new List<long> { 1, 1 };

            while (fibonacciNumbers.Count < NumberOfFibonacci)
            {
                // new way of indexing ;)))))
                var previous = fibonacciNumbers[^1];  // fibonacciNumbers.Count - 1
                var previous2 = fibonacciNumbers[^2];  // fibonacciNumbers.Count - 2

                fibonacciNumbers.Add(previous + previous2);
            }

            foreach (var name in fibonacciNumbers)
                Console.WriteLine(name);
        }
    }
}