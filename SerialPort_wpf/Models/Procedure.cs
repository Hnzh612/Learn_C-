using SerialPort_wpf.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Log;
using SerialPort_wpf.Tools;

namespace SerialPort_wpf.Models
{
    /// <summary>
    /// 流程类
    /// </summary>
    public class Procedure : INotifyPropertyChanged, IProcedureRule
    {
        private static Procedure Instance;
        private static object _obj = new object();
        public static Procedure CreateInstance()
        {
            if (Instance == null)
            {
                lock (_obj)
                {
                    if (Instance == null)
                    {
                        Instance = new Procedure();
                    }
                }
            }
            return Instance;
        }

        private Procedure()
        {
            //CreatedTime = DateTime.Now;//.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public Procedure InitClear()
        {
            return new Procedure();
        }
        #region proerty
        /// <summary>
        /// 脚本的描述
        /// </summary>
        [XmlAttribute(nameof(Describle))]
        public string Describle { get; set; }
        public Procedure SetDescrible(string temp)
        {
            Describle = temp;
            return this;
        }
        /// <summary>
        /// 脚本的名称
        /// </summary>
        [XmlAttribute(nameof(Name))]
        public string Name { get; set; }
        public Procedure SetName(string temp)
        {
            Name = temp;
            return this;
        }
        /// <summary>
        /// 结束时间-开始时间 可以计算平均运行时间
        /// </summary>
        [XmlAttribute(nameof(StartTime))]
        public DateTime StartTime { get; private set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [XmlAttribute(nameof(EndTime))]
        public DateTime EndTime { get; private set; }
        /// <summary>
        /// 当前脚本执行的次数
        /// </summary>
        [XmlAttribute(nameof(RunningCount))]
        public int RunningCount { get; private set; }
        public void SetRunningCount(int temp)
        {
            RunningCount = temp;
        }
        /// <summary>
        ///最后一次执行这个脚本的时间（可以查看从上次运行这个脚本后已经过了多长时间）
        /// </summary>
        public DateTime LastRunTime { get; private set; }
        private readonly static int MaxSimples = 96;
        public event Action<string> ProcedureExecuteInfo;
        public static XYZDPoint SafePoint { get; private set; } = new XYZDPoint();
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Func
        private void Notification(string _message)
        {
            ProcedureExecuteInfo?.Invoke(_message);
            LogCore.CreateInstance().AsyncLog(_message);
        }
        public void ClearProcedureExecuteInfo()
        {
            ProcedureExecuteInfo = null;
        }
        public async Task<bool> CheckData()
        {
            if (MaxSimples != 96
                || LastRunTime.Subtract(DateTime.Now).TotalMilliseconds <= 0
                || EndTime.Subtract(StartTime).TotalMilliseconds <= 0
                || HeadModule == null
                )
            {
                return false;
            }
            return true;
        }
        public async Task<bool> SetUp()
        {
            Notification("========================================================[Procedure.SetUp]========================================================");
            StartTime = DateTime.Now;
            //检查样本信息
            //if (SimpleInfos==null)
            //{
            //    _log.AsyncLog("SimpleInfos was null.");
            //    return false;
            //}
            //if (SimpleInfos.Where(s=>s.IsUsable==true)==null)
            //{
            //    _log.AsyncLog("SimpleInfos is empty.");
            //    return false;
            //}
            ////检查模组信息
            ///检查枪头
            //if (ModuleCollection == null || ModuleCollection.Count <= 0)
            //{
            //    _log.AsyncLog("Modules was null.");
            //    return false;
            //}
            //////检查电机是否在忙、运动、或异常
            ////if ()
            ////{

            ////}
            //////检查电机是否回零
            ////if ()
            ////{

            ////}
            ////if ()
            ////{

            ////}
            return true;
        }

        public async Task<bool> Execute()
        {
            if (!await SetUp())
            {
                Notification("Procedure setup failed!.");
                return false;
            }
            Module module = HeadModule;
            Notification("========================================================[Procedure.Execute]========================================================");
            while (module != null)
            {
                if (!await module.Execute())
                {
                    Notification("========================================================[Procedure Failed!]========================================================");
                    return false;
                }
                module = module.NextModule;
            }
            if (!await TearDown())
            {
                Notification("Procedure teardown failed!.");
                return false;
            }
            RunningCount++;
            Notification("========================================================[Procedure.Ok]========================================================");
            return true;
        }
        public async Task<bool> TearDown()
        {
            EndTime = DateTime.Now;
            Notification("========================================================[Procedure.TearDown]========================================================");
            return true;
        }
        public bool Save()
        {
            return XMLRW.SaveProcedureInstance();
        }
        public bool Read(string xmlFile)
        {
            if (string.IsNullOrEmpty(xmlFile) || !File.Exists(xmlFile))
            {
                throw new FileNotFoundException(xmlFile);
            }
            return XMLRW.SetProcedureInstance();
        }
        #endregion
        public Module HeadModule { get; set; }
        SimpleInfo[] infos = null;
        public SimpleInfo[] GetSimples()
        {
            return infos;
        }
        public void SetSimples(SimpleInfo[] info)
        {
            infos = info;
        }
    }
}
