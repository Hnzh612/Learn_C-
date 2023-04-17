using SerialPort_wpf.Enums;
using SerialPort_wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Interface
{
    internal interface IArea
    {
        /// <summary>
        /// 当前模块的区域标记
        /// </summary>
        PlatformArea CurrentModuleArea { get; }
        /// <summary>
        /// 当前区域内选择的内容坐标
        /// </summary>
        XYZDPoint GetChosenContentXYZ();
    }
}
