using System;

namespace FileSystemWatcherConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var monitoringPath = @"C:\TestFileWatcher\";
            Console.WriteLine("Monitoring file changed in target folder.");
            Console.WriteLine($"Target Folder: {monitoringPath}");

            new Watcher(monitoringPath)
                .WatchCreated()
                .WatchChanged();

            Console.ReadLine();
        }
    }
}
