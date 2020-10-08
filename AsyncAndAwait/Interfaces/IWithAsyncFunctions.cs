using System.Threading.Tasks;

namespace FooClasses
{
    internal interface IWithAsyncBase
    {
        Task ClassMain();
    }
    internal interface IWithAsyncFunctions
    {
        Coffee PourCoffee();
        void ApplyButter(Toast toast);
        void ApplyJam(Toast toast);
        Task<Bacon> FryBaconAsync(int slices);
        Task<Egg> FryEggsAsync(int howMany);
        Task<Toast> ToastBreadAsync(int slices);
        Juice PourOJ();
    }
}