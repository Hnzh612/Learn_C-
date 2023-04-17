using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Enums
{
    /// <summary>
    ///步进电机池 
    ///目前所有步进电机的数量（2022-11-25版本）(控制器通讯协议-改3.doc)
    /// </summary>
    public enum StepperMotorEnum
    {
        /// <summary>
        /// 1号电机--勾爪臂 X轴电机
        /// </summary>
        Motor1_Claw_X = 1,
        /// <summary>
        /// 2号电机--移液枪臂X轴电机
        /// </summary>
        Motor2_PipetteGun_X = 2,
        /// <summary>
        /// 3号电机--氮吹Y轴电机
        /// </summary>
        Motor3_NitrogenBlowing_Y = 3,
        /// <summary>
        /// （4,8,12号电机预留）
        /// </summary>
        Motor_4 = 4,
        /// <summary>
        /// 5号电机--勾爪臂 Y轴电机   
        /// </summary>
        Motor5_Claw_Y = 5,
        /// <summary>
        /// 6号电机--移液枪Y轴电机 
        /// </summary>
        Motor6_PipetteGun_Y = 6,
        /// <summary>
        /// 7号电机--正压 Y轴电机 
        /// </summary>
        Motor7_Barotropic_Y = 7,
        /// <summary>
        /// （4,8,12号电机预留）
        /// </summary>
        Motor_8 = 8,
        /// <summary>
        /// 9号电机--勾爪臂Z轴电机
        /// </summary>
        Motor9_Claw_Z = 9,
        /// <summary>
        /// 10号电机--氮吹 Z轴电机  
        /// </summary>
        Motor10_NitrogenBlowing_Z = 10,
        /// <summary>
        /// 11号电机--混匀震荡电机
        /// </summary>
        Motor11_Mix = 11,
        /// <summary>
        /// （4,8,12号电机预留）
        /// </summary>
        Motor_12 = 12,
        /// <summary>
        /// 移液枪1 Z轴电机
        /// </summary>
        Motor13_PipetteGun_Z1 = 13,
        /// <summary>
        /// 移液枪2 Z轴电机
        /// </summary>
        Motor14_PipetteGun_Z2 = 14,
        /// <summary>
        /// 移液枪3 Z轴电机
        /// </summary>
        Motor15_PipetteGun_Z3 = 15,
        /// <summary>
        /// 移液枪4 Z轴电机
        /// </summary>
        Motor16_PipetteGun_Z4 = 16,
        /// <summary>
        /// 钩爪的控制电机（抓/放）非Z
        /// </summary>
        Mottor20_Claw = 20,
        /// <summary>
        /// 加热膜 21
        /// </summary>
        Mottor21_HeatingFilm,
        /// <summary>
        /// 电磁阀及24V开关 22
        /// </summary>
        Motor22_Solenoid,
        /// <summary>
        /// 气路控制 23
        /// </summary>
        Motor23_PneumaticControl,

    }
}
