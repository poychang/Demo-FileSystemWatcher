using System;
using System.IO;
using System.Linq;
using System.Text;

namespace FileSystemWatcherConsoleApp
{
    public class Watcher
    {
        private readonly FileSystemWatcher _watch;

        public Watcher(string watchFolder)
        {
            _watch = new FileSystemWatcher
            {
                // 設定要監看的資料夾
                Path = watchFolder,
                // 設定要監看的變更類型
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                // 設定要監看的檔案類型
                Filter = "*.CSV",
                // 設定是否監看子資料夾
                IncludeSubdirectories = false,
                // 設定是否啟動元件，必須要設定為 true，否則事件是不會被觸發
                EnableRaisingEvents = true
            };
        }

        /// <summary>
        /// 設定監看新增檔案的觸發事件
        /// </summary>
        public Watcher WatchCreated()
        {
            _watch.Created += new FileSystemEventHandler(_Watch_Created);
            return this;
        }

        /// <summary>
        /// 設定監看修改檔案的觸發事件
        /// </summary>
        public Watcher WatchChanged()
        {
            _watch.Changed += new FileSystemEventHandler(_Watch_Changed);
            return this;
        }

        /// <summary>
        /// 設定監看重新命名的觸發事件
        /// </summary>
        public Watcher WatchRenamed()
        {
            _watch.Renamed += new RenamedEventHandler(_Watch_Renamed);
            return this;
        }

        /// <summary>
        /// 設定監看刪除檔案的觸發事件
        /// </summary>
        public Watcher WatchDeleted()
        {
            _watch.Deleted += new FileSystemEventHandler(_Watch_Deleted);
            return this;
        }

        /// <summary>
        /// 當所監看的資料夾有建立文字檔時觸發
        /// </summary>
        private static void _Watch_Created(object sender, FileSystemEventArgs e)
        {
            var sb = new StringBuilder();
            var dirInfo = new DirectoryInfo(e.FullPath);

            sb.AppendLine($"新建檔案於：{dirInfo.FullName.Replace(dirInfo.Name, "")}");
            sb.AppendLine($"新建檔案名稱：{dirInfo.Name}");
            sb.AppendLine($"建立時間：{dirInfo.CreationTime}");
            sb.AppendLine($"目錄下共有：{dirInfo.Parent?.GetFiles().Length} 檔案");
            sb.AppendLine($"目錄下共有：{dirInfo.Parent?.GetDirectories().Length} 資料夾");
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// 當所監看的資料夾有文字檔檔案內容有異動時觸發
        /// </summary>
        private static void _Watch_Changed(object sender, FileSystemEventArgs e)
        {
            var sb = new StringBuilder();
            var dirInfo = new DirectoryInfo(e.FullPath);

            sb.AppendLine($"被異動的檔名為：{e.Name}");
            sb.AppendLine($"檔案所在位址為：{e.FullPath.Replace(e.Name, "")}");
            sb.AppendLine($"異動內容時間為：{dirInfo.LastWriteTime}");
            sb.AppendLine($"最後一筆內容：{File.ReadLines(e.FullPath).Last()}");
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// 當所監看的資料夾有文字檔檔案重新命名時觸發
        /// </summary>
        private static void _Watch_Renamed(object sender, RenamedEventArgs e)
        {
            var sb = new StringBuilder();
            var fileInfo = new FileInfo(e.FullPath);

            sb.AppendLine($"檔名更新前：{e.OldName}");
            sb.AppendLine($"檔名更新後：{e.Name}");
            sb.AppendLine($"檔名更新前路徑：{e.OldFullPath}");
            sb.AppendLine($"檔名更新後路徑：{e.FullPath}");
            sb.AppendLine($"建立時間：{fileInfo.LastAccessTime}");
            Console.WriteLine(sb.ToString());
        }

        /// <summary>
        /// 當所監看的資料夾有文字檔檔案有被刪除時觸發
        /// </summary>
        private static void _Watch_Deleted(object sender, FileSystemEventArgs e)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"被刪除的檔名為：{e.Name}");
            sb.AppendLine($"檔案所在位址為：{e.FullPath.Replace(e.Name, "")}");
            sb.AppendLine($"刪除時間：{DateTime.Now}");
            Console.WriteLine(sb.ToString());
        }
    }
}
