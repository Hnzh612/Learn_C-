using SerialPort_wpf.Enums;
using SerialPort_wpf.Interface;
using SerialPort_wpf.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Models
{
    public abstract class Module : IModule, IProcedureRule, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string _propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
        #region 构造
        public Module()
        {

        }
        public int ModuleID { get; set; }
        #endregion

        #region SetUp
        public abstract Task<bool> SetUp();
        #endregion

        #region Execute
        public abstract Task<bool> Execute();
        #endregion

        #region TearDown
        public abstract Task<bool> TearDown();
        #endregion

        #region CheckData
        public abstract Task<bool> CheckData();
        #endregion

        #region FrontModule
        public Module FrontModule { get; set; }
        #endregion

        #region NextModule
        public Module NextModule { get; set; } = null;
        #endregion

        #region Property
        public ModuleTypeEnum CurrentTypeEnum { get; set; } = ModuleTypeEnum.Empty;
        public SimpleInfo[] simpleInfos { get; set; }
        public object Data { get; set; }
        public PlatformArea CurrentPlatform { get; set; } = PlatformArea.Null;
        public XYZDPoint CurrentXYZPoint { get; set; }
        #region CurrentIndex
        int _currentIndex = 0;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public bool IsWorking { get; set; }
        public int AllSteps { get; set; }
        public int StepIndex { get; set; }
        public int Repeats { get; set; }

        #endregion

        #region  Method

        #endregion

        #region 析构
        #endregion
    }
}
