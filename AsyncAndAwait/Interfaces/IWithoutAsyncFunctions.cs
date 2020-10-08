namespace FooClasses
{
    interface IWithoutAsyncBase
    {
        void ClassMain();
    }

    interface IWithoutAsyncFunctions
    {

        Juice PourOJ();
        void ApplyJam(Toast toast);
        void ApplyButter(Toast toast);
        Toast ToastBread(int slices);
        Bacon FryBacon(int slices);
        Egg FryEggs(int howMany);
        Coffee PourCoffee();
    }
}