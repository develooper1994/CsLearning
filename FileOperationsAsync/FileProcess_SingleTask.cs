using System;
using static System.Console;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileOperationsAsync
{
    class FileProcess_SingleTask
    {
        public readonly string filePath = "simple.txt";
        public readonly string text = "Hello Mustafa Selçuk Çağlar \n";

        private const int bufferSize = 4096;
        private const bool useAsync = true;

        public async Task FileWriteMain()
        {
            await ProcessWriteAsync();
            await ProcessReadAsync();
        }

        // Simple async file Write and Read
        protected async Task SimpleWriteAsync(string filePath = "simple.txt", string text = "Hello World \n") =>
            await File.WriteAllTextAsync(filePath, text);

        protected async Task<string> SimpleReadAsync(string filePath = "simple.txt") =>
            await File.ReadAllTextAsync(filePath);

        // Realistic and Simple async file Write
        protected async Task ProcessWriteAsync(string filePath = "simple.txt", string text = "Hello World \n")
        {
            await WriteTextAsync(filePath, text);
        }

        protected async Task WriteTextAsync(string filePath = "simple.txt", string text = "Hello World \n")
        {
            var encodedText = Encoding.Unicode.GetBytes(text);

            using var sourceStream = new FileStream(
                filePath,
                FileMode.Create, FileAccess.Write, FileShare.None,
                bufferSize: bufferSize, useAsync: useAsync
                );

            var offset = 0;
            var count = encodedText.Length;
            await sourceStream.WriteAsync(encodedText, offset, count);
        }

        // Realistic and Simple async file Read
        protected async Task ProcessReadAsync(string filePath = "simple.txt")
        {
            try
            {
                if (File.Exists(filePath) != false)
                {
                    var text = await ReadTextAsync(filePath);
                    WriteLine(text);
                }
                else
                {
                    WriteLine($"file not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
        }

        private async Task<string> ReadTextAsync(string filePath)
        {
            using var sourceStream = new FileStream(
                filePath,
                FileMode.Open, FileAccess.Read, FileShare.None,
                bufferSize: bufferSize, useAsync: useAsync
                );

            var strBuilder = new StringBuilder();

            var buffer = new byte[256];
            int numRead = 0;
            int offset = 0;
            int count = buffer.Length;

            while ((numRead = await sourceStream.ReadAsync(buffer, offset, count))
                != 0)
            {
                var text = Encoding.Unicode.GetString(buffer, offset, count);
                strBuilder.Append(text);
            }
            return strBuilder.ToString();
        }
    }
}
