using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApplication
{
    class Boiler
    {
        private int temp;
        private int pressure;
        public Boiler(int t,int p) 
        {
            temp = t;
            pressure = p;
        }
        public int getTemp()
        {
            return temp;
        }
        public int getPressure()
        {
            return pressure;
        }
    }
    // 事件发布器
    class DelegateBoilerEvent
    {
        public delegate void BoilerLogHandler(string status);

        // 基于上面的委托定义事件
        public event BoilerLogHandler BoilerEventLog;
        public void LogProcess()
        {
            string remarks = "OK";
            Boiler b = new Boiler(100, 12);
            int t = b.getTemp();
            int p = b.getPressure();
            if(t > 150 || t < 80 || p < 12 || p > 15)
            {
                remarks = "Need Maintenance";
            }
            OnBoilerEventLog("Logging Info:\n");
            OnBoilerEventLog($"Temparature {t} \nPressure: {p}");
            OnBoilerEventLog($"\nMessage:{remarks}");

        }
        protected void OnBoilerEventLog(string message)
        {
            if(BoilerEventLog != null)
            {
                BoilerEventLog(message);
            }
        }
    }

    // 该类保留写入日志文件的条款
    class BoilerInfoLogger
    {
        private string FilePath { get;  set; }
        StreamWriter sw;
        public BoilerInfoLogger(string filename)
        {
            string _dirPath = $"{Environment.CurrentDirectory}/Log";
            if(!Directory.Exists(_dirPath)) { Directory.CreateDirectory(_dirPath); }
            FilePath = $"{_dirPath}/{filename}.log" ;
            if(!File.Exists(FilePath)) { File.Create(FilePath); }
            sw = new StreamWriter(FilePath);
        }
        public void Logger(string info)
        {
            sw.WriteLine(info);
        }
        public void Close()
        {
            sw.Close();
        }
    }

    // 事件订阅器
    class Program
    {
        static void Logger(string info)
        {
            Console.WriteLine(info);
        }
        static void Main(string[] args)
        {
            BoilerInfoLogger filelog = new BoilerInfoLogger("boiler");
            DelegateBoilerEvent boilerEvent = new DelegateBoilerEvent();
            boilerEvent.BoilerEventLog += new
            DelegateBoilerEvent.BoilerLogHandler(Logger);
            boilerEvent.BoilerEventLog += new
            DelegateBoilerEvent.BoilerLogHandler(filelog.Logger);
            boilerEvent.LogProcess();
            Console.ReadLine();
            filelog.Close();

        }
    }
}
