using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Enums
{
    public enum Simpletype
    {
        /// <summary>
        /// 正常的样品
        /// </summary>
        Normal,
        /// <summary>
        /// LQC 质控品
        /// </summary>
        LQC,
        /// <summary>
        /// MQC 质控品
        /// </summary>
        MQC,
        /// <summary>
        /// HQC 质控品
        /// </summary>
        HQC,
        /// <summary>
        /// 空白质控品
        /// </summary>
        Empty,
        /// <summary>
        ///校准品 
        /// </summary>
        Calibration
    }
}
