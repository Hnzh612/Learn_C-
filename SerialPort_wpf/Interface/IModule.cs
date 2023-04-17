using SerialPort_wpf.Enums;
using SerialPort_wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Interface
{
    public interface IModule
    {
        /// <summary>
        /// 当前模块类型
        /// </summary>
        PlatformArea CurrentPlatform { get; set; }
        /// <summary>
        /// 当前操作的点
        /// </summary>
        XYZDPoint CurrentXYZPoint { get; set; }
        /// <summary>
        /// 当前区域获取的内容顺序
        /// </summary>
        int CurrentIndex { get; set; }
        /// <summary>
        /// 当前模块是否在使用中
        /// </summary>
        bool IsWorking { get; set; }
        /// <summary>
        /// 当前模块包含的控制命令
        /// </summary>
        int AllSteps { get; set; }
        /// <summary>
        /// 当前执行的命令的条数
        /// </summary>
        int StepIndex { get; set; }
        /// <summary>
        /// 重复执行的次数
        /// </summary>
        int Repeats { get; set; }
        int ModuleID { get; }
    }
}
