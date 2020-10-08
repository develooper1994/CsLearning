using static System.Console;
using System.Threading.Tasks;

namespace FooClasses
{
    class BaseClass : IWithoutAsyncFunctions
    {
        public Coffee PourCoffee()
        {
            WriteLine("Pouring coffee");
            return new Coffee();
        }

        public void ApplyJam(Toast toast) =>
            WriteLine("Putting jam on the toast");

        public void ApplyButter(Toast toast) =>
            WriteLine("Putting butter on the toast");

        public Toast ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
                WriteLine("Putting a slice of bread in the toaster");

            WriteLine("Start toasting...");
            Task.Delay(3000).Wait();
            WriteLine("Remove toast from toaster");

            return new Toast();
        }

        public Bacon FryBacon(int slices)
        {
            WriteLine($"putting {slices} slices of bacon in the pan");
            WriteLine("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
                WriteLine("flipping a slice of bacon");

            WriteLine("cooking the second side of bacon...");
            Task.Delay(3000).Wait();
            WriteLine("Put bacon on plate");

            return new Bacon();
        }

        public Egg FryEggs(int howMany)
        {
            WriteLine("Warming the egg pan...");
            Task.Delay(3000).Wait();
            WriteLine($"cracking {howMany} eggs");
            WriteLine("cooking the eggs ...");
            Task.Delay(3000).Wait();
            WriteLine("Put eggs on plate");

            return new Egg();
        }

        public Juice PourOJ()
        {
            WriteLine("Pouring orange juice");
            return new Juice();
        }
    }

    class WithoutAsync : BaseClass, IWithoutAsyncBase
    {
        public virtual void ClassMain()
        {
            WriteLine("-*-*-*-*-* coffee is prepearing *-*-*-*-*-");
            Coffee cup = PourCoffee();
            WriteLine("-*-*-*-*-* coffee is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* egg is prepearing *-*-*-*-*-");
            Egg eggs = FryEggs(2);
            WriteLine("-*-*-*-*-* egg is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* bacon is prepearing *-*-*-*-*-");
            Bacon bacon = FryBacon(3);
            WriteLine("-*-*-*-*-* bacon is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* toast is prepearing *-*-*-*-*-");
            Toast toast = ToastBread(2);
            ApplyButter(toast);
            ApplyJam(toast);
            WriteLine("-*-*-*-*-* toast is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* Juice is prepearing *-*-*-*-*-");
            Juice oj = PourOJ();  // oj = orange juice
            WriteLine("-*-*-*-*-* Juice is ready *-*-*-*-*-");
            WriteLine("-*-*-*-*-* Breakfast is ready! *-*-*-*-*-");
        }
    }
}
