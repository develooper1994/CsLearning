using static System.Console;
using System.Threading.Tasks;

namespace FileOperationsAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
            WriteLine($"\n-*-*-*-*-* FileProcess.FileWriteMain *-*-*-*-*-\n");
            var fp_ST = new FileProcess_SingleTask();
            await fp_ST.FileWriteMain();
            */

            WriteLine($"\n-*-*-*-*-* FileProcess.FileWriteMain *-*-*-*-*-\n");
            var fp_PT = new FileProcess_ParallelTask();
            await fp_PT.FileProcess_ParallelTaskMain();


            ReadKey();
        }
    }
}
