using System;
using static System.Console;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadWebpages_ASYNC
{
    class DownloadWebpages_waitUntilComplete : IDownloadWebpages_waitUntilComplete
    {
        private static readonly CancellationTokenSource s_cts = new CancellationTokenSource();

        protected static HttpClient s_client = new HttpClient
        {
            MaxResponseContentBufferSize = 1_000_000
        };

        protected IEnumerable<string> s_UrlList = new string[]
        {
            "https://github.com/develooper1994/CsLearning",
            "http://selcukcaglar.ml/",
            "https://www.google.com.tr/",
            "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/cancel-an-async-task-or-a-list-of-tasks",
            "https://github.com/develooper1994/MasterThesis",
            "https://github.com/develooper1994/Superresolution_Recognition",
            "https://github.com/develooper1994/kornia_exercises",
            "https://www.btkakademi.gov.tr/",
            "https://www.youtube.com/",
            "https://dotnet.microsoft.com/",
            "https://www.python.org/",
            "https://nodejs.org/en/",
            "https://visualstudio.microsoft.com/tr/",
            "https://nim-lang.org/",
            "https://www.rust-lang.org/",
        };

        protected static Action StopProcessFunc1 =>
        () =>
        {
            while (ReadKey().Key != ConsoleKey.Enter)
            {
                WriteLine("Press the ENTER key to cancel...");
            }

            WriteLine("\nENTER key pressed: cancelling downloads.\n");
            s_cts.Cancel();
        };

        public async Task DownloadWebpagesMain()
        {
            WriteLine(@"Application started...
                        Press Enter key to cancel...
                        ");

            var stopProcessTask = Task.Run(StopProcessFunc1);

            Task SumPageSizeTask = SumPageSizeMeasureAsync(SumPageSizeAsync);
            await Task.WhenAny(new[] { stopProcessTask, SumPageSizeTask });

            WriteLine("Application Ending...");
        }

        protected async Task SumPageSizeMeasureAsync(Func<Task> SumPageSize)
        {
            ///<summary>
            /// I can't make it parallel. because every call depends on before.
            /// </summary>
            var stopwatch = Stopwatch.StartNew();
            await SumPageSize?.Invoke();

            stopwatch.Stop();
            WriteLine($"Ellapsed time:        {stopwatch.Elapsed}");
        }

        private async Task SumPageSizeAsync()
        {
            var total = 0;
            foreach (var url in s_UrlList)
            {
                int contentLenght = await ProcessUrlAsync(url, s_client, s_cts.Token);
                total += contentLenght;
            }
            WriteLine($"Total bytes returned: {total:#,#}");
        }

        private static async Task<int> ProcessUrlAsync(string url, HttpClient client, CancellationToken token)
        {
            HttpResponseMessage response = await client.GetAsync(url, token);
            byte[] content = await response.Content.ReadAsByteArrayAsync();
            WriteLine($"{url,-60} {content.Length,10:#,#}");

            return content.Length;
        }

    }
}
