using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Enums
{
    public enum ControlBoard
    {
        /// <summary>
        /// 电机控制板
        /// </summary>
        StepperMotor = 02,
        /// <summary>
        /// 加热阀、电磁阀、24V开关、比例阀 气路控制功能控制板地址
        /// </summary>
        Valve = 03,
        /// 移液枪1泵地址（靠近X轴为1）
        /// </summary>
        PipetteGun1 = 05,
        /// <summary>
        /// 移液枪2泵地址（靠近X轴为1）
        /// </summary>
        PipetteGun2 = 06,
        /// <summary>
        /// 移液枪3泵地址（靠近X轴为1）
        /// </summary>
        PipetteGun3 = 07,
        /// <summary>
        /// 移液枪4泵地址（靠近X轴为1）
        /// </summary>
        PipetteGun4 = 08
    }
}
