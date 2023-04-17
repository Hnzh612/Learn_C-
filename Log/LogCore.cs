using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    /// <summary>
    /// 读写分离，20+线程  100000任务数压力
    /// 缓冲性能提升
    /// </summary>
    public class LogCore
    {
        private static LogCore Instance { get; set; }
        private static object _lockIO = new object();
        public string FilePath { get; private set; }
        private LogCore()
        {
            string _dirPath = $"{Environment.CurrentDirectory}/SSLog/{DateTime.Now.ToString("yyyy_MM")}";
            if (!Directory.Exists(_dirPath)) { Directory.CreateDirectory(_dirPath); };
            FilePath = $"{_dirPath}/{DateTime.Now.ToString("dd")}.log";

            if (!File.Exists(FilePath)) { File.Create(FilePath).Close(); };
        }
        public static LogCore CreateInstance()
        {
            if (Instance == null)
            {
                lock (_lockIO)
                {
                    if (Instance == null)
                    {
                        Instance = new LogCore();
                    }
                }
            }
            return Instance;
        }
        public void AsyncLog(string message, [CallerMemberName] string info = "")
        {
            string fm_1 = $"[{DateTime.Now.ToString("HH:mm:ss:ff")}] [{info}] [Info]:";
            lock (_lockIO)
            {
                using (StreamWriter outputFile = new StreamWriter(FilePath, append: true))
                {
                    outputFile.WriteLineAsync(fm_1 + message);
                }
            }
        }
        public void AsyncLog(Exception ex, [CallerMemberName] string info = "")
        {
            string fm_1 = $"[{DateTime.Now.ToString("HH:mm:ss:ff")}] [{info}] [Error]:";
            lock (_lockIO)
            {
                using (StreamWriter outputFile = new StreamWriter(FilePath, append: true))
                {
                    outputFile.WriteLineAsync($"{fm_1}\r\nExMessage:{ex.Message}\r\nExStackTrace:{ex.StackTrace}\r\nExSource:{ex.Source}\r\nExTargetSite:{ex.TargetSite}");
                }
            }
        }
        public void AsyncLog(string message, Exception ex, [CallerMemberName] string info = "")
        {
            string fm_1 = $"[{DateTime.Now.ToString("HH:mm:ss:ff")}] [{info}] [Error]:";
            lock (_lockIO)
            {
                using (StreamWriter outputFile = new StreamWriter(FilePath, append: true))
                {
                    outputFile.WriteLineAsync($"{fm_1}\r\n{message}\r\nExMessage:{ex.Message}\r\nExStackTrace:{ex.StackTrace}\r\nExSource:{ex.Source}\r\nExTargetSite:{ex.TargetSite}");
                }
            }
        }
    }
}
