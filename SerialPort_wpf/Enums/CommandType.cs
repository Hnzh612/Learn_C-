using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Enums
{
    /// <summary>
    /// Stepper motor & Grapple & Solenoid command type.
    /// </summary>
    public enum CommandTypeEnum
    {
        /// <summary>
        /// 设置升降频时间
        /// </summary>
        A,
        /// <summary>
        /// 查询步进电机状态
        /// </summary>
        C,
        /// <summary>
        /// 电磁阀控制
        /// </summary>
        D,
        /// <summary>
        /// 电机回0
        /// </summary>
        H,
        /// <summary>
        /// 电机移动固定距离
        /// </summary>
        P,
        /// <summary>
        /// 设置电机运行速度
        /// </summary>
        S,
        /// <summary>
        /// 电机停止运行
        /// </summary>
        T,
        /// <summary>
        /// 抓钩控制
        /// </summary>
        G,
        /// <summary>
        /// 加热膜控制
        /// </summary>
        R,
        /// <summary>
        /// 气压气路控制
        /// </summary>
        U,
        /// <summary>
        /// 移液枪初始化
        /// </summary>
        It,
        /// <summary>
        /// 移液枪吸液
        /// </summary>
        Ia,
        /// <summary>
        /// 移液枪排液
        /// </summary>
        Da,
        /// <summary>
        /// 移液枪去掉TIP头
        /// </summary>
        Dt,
        /// <summary>
        /// 移液枪液面探测
        /// </summary>
        Ld,
        /// <summary>
        /// 移液枪活塞移动到绝对位置
        /// </summary>
        Mp,
        /// <summary>
        /// 移液枪活塞相对当前位置向上移动吸液指定距离
        /// </summary>
        Up,
        /// <summary>
        /// 移液枪活塞相对当前位置向下移动排液指定距离（N4避免针尖挂液）
        /// </summary>
        Dp,
        /// <summary>
        /// 移液枪检测TIP头型号
        /// </summary>
        Rr,
        /// <summary>
        /// 混匀震荡控制
        /// </summary>
        Z
    }
}
