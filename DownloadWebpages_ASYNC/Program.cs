using static System.Console;
using DownloadWebpages_ASYNC;

/*
// non-cancelable
WriteLine($"\n-*-*-*-*-* DownloadWebpages_processTasksAsTheyComplete.DownloadWebpagesMain *-*-*-*-*-\n");
await new DownloadWebpages_processTasksAsTheyComplete().DownloadWebpagesMain();
*/

// cancelable
WriteLine($"\n-*-*-*-*-* DownloadWebpages_waitUntilComplete.DownloadWebpagesMain *-*-*-*-*-\n");
await new DownloadWebpages_waitUntilComplete().DownloadWebpagesMain();


ReadKey();
