using System.Collections.Generic;
using static System.Console;
using System.Threading.Tasks;

namespace FooClasses
{
    internal class WithAsync_VeryBad1 : BaseClass, IWithAsyncBase, IWithAsyncFunctions
    {
        public async Task ClassMain()
        {
            WriteLine("-*-*-*-*-* coffee is prepearing *-*-*-*-*-");
            Coffee cup = PourCoffee();
            WriteLine("-*-*-*-*-* coffee is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* eggs is prepearing *-*-*-*-*-");
            Egg eggs = await FryEggsAsync(2);
            WriteLine("-*-*-*-*-* eggs is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* bacon is prepearing *-*-*-*-*-");
            Bacon bacon = await FryBaconAsync(3);
            WriteLine("-*-*-*-*-* bacon is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* toast is prepearing *-*-*-*-*-");
            Toast toast = await ToastBreadAsync(2);
            ApplyButter(toast);
            ApplyJam(toast);
            WriteLine("-*-*-*-*-* toast is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* oj is prepearing *-*-*-*-*-");
            Juice oj = PourOJ();  // oj = orange juice
            WriteLine("-*-*-*-*-* oj is ready *-*-*-*-*-");
            WriteLine("Breakfast is ready!");
        }

        public async Task ClassMain_LittleOptimized1()
        {
            WriteLine("-*-*-*-*-* coffee is prepearing *-*-*-*-*-");
            var cup = PourCoffee();
            WriteLine("-*-*-*-*-* coffee is ready *-*-*-*-*-");

            // starting tasks immediately
            var eggTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = ToastBreadAsync(2);

            WriteLine("-*-*-*-*-* toast is prepearing *-*-*-*-*-");
            var toast = await toastTask;
            ApplyButter(toast);
            ApplyJam(toast);
            WriteLine("-*-*-*-*-* toast is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* eggs is prepearing *-*-*-*-*-");
            var eggs = await eggTask;
            WriteLine("-*-*-*-*-* eggs is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* bacon is prepearing *-*-*-*-*-");
            var bacon = await baconTask;
            WriteLine("-*-*-*-*-* bacon is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* oj is prepearing *-*-*-*-*-");
            var oj = PourOJ();  // oj = orange juice
            WriteLine("-*-*-*-*-* oj is ready *-*-*-*-*-");

            WriteLine("Breakfast is ready!");
        }

        public async Task ClassMain_LittleOptimized2()
        {
            WriteLine("-*-*-*-*-* coffee is prepearing *-*-*-*-*-");
            var cup = PourCoffee();
            WriteLine("-*-*-*-*-* coffee is ready *-*-*-*-*-");

            // starting tasks immediately
            var eggTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            WriteLine("-*-*-*-*-* eggs is prepearing *-*-*-*-*-");
            var eggs = await eggTask;
            WriteLine("-*-*-*-*-* eggs is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* bacon is prepearing *-*-*-*-*-");
            var bacon = await baconTask;
            WriteLine("-*-*-*-*-* bacon is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* toast is prepearing *-*-*-*-*-");
            var toast = await toastTask;
            WriteLine("-*-*-*-*-* toast is ready *-*-*-*-*-");

            WriteLine("-*-*-*-*-* oj is prepearing *-*-*-*-*-");
            var oj = PourOJ();  // oj = orange juice
            WriteLine("-*-*-*-*-* oj is ready *-*-*-*-*-");

            WriteLine("Breakfast is ready!");
        }

        public async Task ClassMain_Efficient()
        {
            WriteLine("-*-*-*-*-* coffee is prepearing *-*-*-*-*-");
            var cup = PourCoffee();
            WriteLine("-*-*-*-*-* coffee is ready *-*-*-*-*-");

            var eggsTask = FryEggsAsync(2);
            var baconTask = FryBaconAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask};

            while(breakfastTasks.Count > 0)
            {
                var finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                    WriteLine("eggs are ready");
                else if (finishedTask == baconTask)
                    WriteLine("bacon is ready");
                else if (finishedTask == toastTask)
                    WriteLine("toast is ready");
                else
                    WriteLine("WHAT??? is ready???");
                breakfastTasks.Remove(finishedTask);
            }

            WriteLine("-*-*-*-*-* oj is prepearing *-*-*-*-*-");
            var oj = PourOJ();  // oj = orange juice
            WriteLine("-*-*-*-*-* oj is ready *-*-*-*-*-");

            WriteLine("Breakfast is ready!");
        }

        public new Coffee PourCoffee() =>
            base.PourCoffee();

        public new void ApplyButter(Toast toast) =>
            base.ApplyButter(toast);

        public new void ApplyJam(Toast toast) =>
            base.ApplyJam(toast);

        async Task<Toast> MakeToastWithButterAndJamAsync(int number = 2)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }

        public async Task<Toast> ToastBreadAsync(int slices=2)
        {
            for (int slice = 0; slice < slices; slice++)
                WriteLine("Putting a slice of bread in the toaster");

            WriteLine("Start toasting...");
            await Task.Delay(3000);
            WriteLine("Remove toast from toaster");

            return new Toast();
        }

        public async Task<Bacon> FryBaconAsync(int slices=3)
        {
            WriteLine($"putting {slices} slices of bacon in the pan");
            WriteLine("cooking first side of bacon...");
            await Task.Delay(3000);
            for (int slice = 0; slice < slices; slice++)
            {
                WriteLine("flipping a slice of bacon");
            }
            WriteLine("cooking the second side of bacon...");
            await Task.Delay(3000);
            WriteLine("Put bacon on plate");

            return new Bacon();
        }

        public async Task<Egg> FryEggsAsync(int howMany=2)
        {
            WriteLine("Warming the egg pan...");
            await Task.Delay(3000);
            WriteLine($"cracking {howMany} eggs");
            WriteLine("cooking the eggs ...");
            await Task.Delay(3000);
            WriteLine("Put eggs on plate");

            return new Egg();
        }

        public new Juice PourOJ() =>
            base.PourOJ();
    }
}
