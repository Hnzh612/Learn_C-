using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Models
{
    /// <summary>
    /// 台机上每个区域只需要记住对应的四个角的坐标
    /// 剩下的可以计算，误差由生产部门同事保证
    /// </summary>
    public struct StepRect
    {
        /// <summary>
        /// 左上角坐标
        /// </summary>
        public XYZDPoint TopLeft;
        /// <summary>
        /// 右上角坐标
        /// </summary>
        public XYZDPoint TopRight;
        /// <summary>
        /// 右下角坐标
        /// </summary>
        public XYZDPoint BottomRight;
        /// <summary>
        /// 左下角坐标
        /// </summary>
        public XYZDPoint BottomLeft;
    }
}
