using System;
using static System.Console;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FileOperationsAsync
{
    internal class FileProcess_ParallelTask
    {
        public string dirName => "TextDir";

        private const int bufferSize = 4096;
        private const bool useAsync = true;

        public async Task FileProcess_ParallelTaskMain(string method = "default")
        {
            // 1) default, 2) custom
            await ProcessWriteAsync(method: method);

            ///<summary>
            /// - custom version doesn't work due to "autorization"
            /// - multiple filestream object trying to access files. this cause big problems
            /// - Need more efford to fix that problem but I am done for this little project.
            /// </summary>
            await ProcessReadAsync(method: method);
        }

        protected async Task ProcessWriteAsync(string dirName = "TextDir", string method = "default") =>
            await SimpleParallelWriteAsync(dirName, method);

        protected async Task SimpleParallelWriteAsync(string dirName = "TextDir", string method = "default")
        {
            string folder = Directory.CreateDirectory(dirName).Name;
            var writeTaskList = new List<Task>();

            for (var idx = 0; idx <10; idx++)
            {
                string fileName = $"file-{idx:00}.txt";
                string filePath = $"{folder}/{fileName}";
                string content = $"Number: {idx}{Environment.NewLine}";

                var writeTask = WriteAllTextAsync(filePath, content, method);
                writeTaskList.Add(writeTask);
            }

            await Task.WhenAll(writeTaskList);
        }

        protected static Task WriteAllTextAsync(string filePath, string content, string method="default")
        {
            return method switch
            {
                "custom" => CustomWriteAllTextAsync(filePath, content),
                "default" => File.WriteAllTextAsync(filePath, content),
                _ => File.WriteAllTextAsync(filePath, content)
            };
        }

        private static Task CustomWriteAllTextAsync(string filePath, string content)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(content);

            var sourceStream =
                new FileStream(
                    filePath,
                    FileMode.Create, FileAccess.Write, FileShare.None,
                    bufferSize: 4096, useAsync: true);

            Task writeTask = sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            return writeTask;
        }

        protected async Task ProcessReadAsync(string dirName = "TextDir", string method = "default")
        {
            var contents = await SimpleParallelReadAsync(dirName, method);
            foreach (var content in contents)
            {
                Write($"{content.Key} : {content.Value}");
            }
        }

        protected async Task<Dictionary<string, string>> SimpleParallelReadAsync(string dirName = "TextDir", string method = "default")
        {
            var files = Directory.GetFiles(dirName);
            var lenght = files.Length;
            //using FileStream sourceStream = new FileStream(
            //    dirName,
            //    FileMode.Open, FileAccess.Read, FileShare.None,
            //    bufferSize: bufferSize, useAsync: useAsync
            //    );
            var readTasksQuery =
                from file in files
                select ReadAllTextAsync(file, method);

            var readTasksAndPaths = readTasksQuery.ToList();

            //var contentList = new List<string>();
            var contentList = new Dictionary<string, string>();

            while(readTasksAndPaths.Any())
            {
               var (readFilePath, _) = readTasksAndPaths[0];

               var readTasks = (from readTaskAndPath in readTasksAndPaths
                                 select readTaskAndPath.ReadTask).ToList();
                // Get task and start
                var completedTask = await Task.WhenAny(readTasks);
                var taskIdx = readTasksAndPaths.FindIndex(
                    (TaskAndPath) =>
                    {
                        return TaskAndPath.ReadTask == completedTask;
                    }
                    );
                readTasksAndPaths.RemoveAt(taskIdx);

                // Get the result
                var result = await completedTask;
                contentList.Add(readFilePath, result);
            }

            return contentList;
        }

        protected (string filePath, Task<string> ReadTask) ReadAllTextAsync(string filePath, string method = "default")
        {
            var selectedTask = method switch
            {
                "default" => File.ReadAllTextAsync(filePath),
                _ => File.ReadAllTextAsync(filePath)
            };
            return (filePath, selectedTask);
        }

        protected (string filePath, Task<string> ReadTask) ReadAllTextAsync(string filePath, FileStream sourceStream, string method = "default")
        {
            var selectedTask = method switch
            {
                "custom" => CustomReadAllTextAsync(sourceStream),
                "default" => File.ReadAllTextAsync(filePath),
                _ => CustomReadAllTextAsync(sourceStream)
            };
            return (filePath, selectedTask);
        }

        [Obsolete]
        private static async Task<string> CustomReadAllTextAsync(FileStream sourceStream)
        {
            //using var sourceStream = new FileStream(
            //    filePath,
            //    FileMode.Open, FileAccess.Read, FileShare.None,
            //    bufferSize: bufferSize, useAsync: useAsync
            //    );

            var strBuilder = new StringBuilder();

            var buffer = new byte[256];
            int offset = 0;
            int count = buffer.Length;

            int numRead;
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