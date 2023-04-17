using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Enums
{
    /// <summary>
    /// 平台区域划分
    /// </summary>
    public enum PlatformArea
    {
        /// <summary>
        /// 默认
        /// </summary>
        Null,
        /// <summary>
        /// 样品区
        /// </summary>
        Sample_Area,
        /// <summary>
        /// 混匀区
        /// </summary>
        Mix_Area,
        /// <summary>
        /// 震荡区
        /// </summary>
        Congestion_Area,
        /// <summary>
        /// 正压区
        /// </summary>
        Barotropic_Area,
        /// <summary>
        /// 氮吹区
        /// </summary>
        Nitrogen_Blowing_Area,
        /// <summary>
        /// 存样区
        /// </summary>
        Storage_Area
    }
}
