using static System.Console;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace DownloadWebpages_ASYNC
{
    class DownloadWebpages_processTasksAsTheyComplete : DownloadWebpages_waitUntilComplete
    {
        public new Task DownloadWebpagesMain() =>
            SumPageSizeMeasureAsync();

        private async Task SumPageSizeMeasureAsync() =>
            await SumPageSizeMeasureAsync(SumPageSizeAsync);

        private async Task SumPageSizeAsync()
        {
            var downloadTasksQuery =
                from url in s_UrlList
                select ProcessUrlAsync(url, s_client);

            var downloadTasks = downloadTasksQuery.ToList();

            int total = 0;
            while (downloadTasks.Any())
            {
                Task<int> completedTask = await Task.WhenAny(downloadTasks);
                downloadTasks.Remove(completedTask);
                total += await completedTask;
            }
        }

        private async Task<int> ProcessUrlAsync(string url, HttpClient client)
        {
            byte[] content = await client.GetByteArrayAsync(url);
            WriteLine($"{url,-60} {content.Length,10:#,#}");

            return content.Length;
        }
    }
}
