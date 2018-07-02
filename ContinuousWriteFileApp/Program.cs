using System;
using System.IO;
using System.Threading;

namespace ContinuousWriteFileApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filename = "Output.csv";
            var filePah = $@"C:\TestFileWatcher\{filename}";
            Console.WriteLine("Continuous create 1000 line of random records, each record is generated at intervals of around 1 second.");
            Console.WriteLine($"Target File: {filePah}");

            CreateFile(filePah);
            AppendTextSimulator(filePah);

            Console.ReadLine();
        }

        /// <summary>
        /// 建立檔案
        /// </summary>
        /// <param name="filePah">檔案路徑</param>
        private static void CreateFile(string filePah)
        {
            try
            {
                if (File.Exists(filePah))
                {
                    File.Delete(filePah);
                }
                else
                {
                    using (File.Create(filePah))
                    {
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 產生資料的模擬器
        /// </summary>
        /// <param name="filePah">檔案路徑</param>
        private static void AppendTextSimulator(string filePah)
        {
            var rand = new Random();
            var date = DateTime.Now;
            for (var i = 0; i < 1000; i++)
            {
                Thread.Sleep(rand.Next(1000, 1200));
                using (var sw = File.AppendText(filePah))
                {
                    sw.WriteLine($@"{i},{date.ToShortDateString()},{rand.Next(1, 10)},{rand.Next(50, 100)},{(float)rand.Next(1, 10) / 10}");
                }
            }
        }
    }
}
