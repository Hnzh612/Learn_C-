using Log;
using SerialPort_wpf.Enums;
using SerialPort_wpf.Models;
using SerialPort_wpf.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Tools
{
    public class Controller
    {
        #region 属性
        /// <summary>
        /// 移液枪地址
        /// </summary>
        public static int PipetteGunAddr { get; set; } = 2;
        /// <summary>
        /// 移液枪当前位置
        /// </summary>
        public static XYZDPoint PGLocation { get; set; } = new XYZDPoint(0, 0, 0);
        /// <summary>
        /// 抓钩当前位置
        /// </summary>
        public static XYZDPoint ZGLocation { get; set; } = new XYZDPoint(0, 0, 0);
        /// <summary>
        /// 液枪Z的最大安全距离,这个距离指当前Z可以不回缩的移动也不会碰撞到任何物体
        /// </summary>
        public static int SafeStep { get; set; } = 5000;
        public static int TargetStepZ { get; set; } = 25000;
        public static bool CoverTargetStepZ { get; set; } = false;
        #endregion

        #region 检测位置
        /// <summary>
        /// 指示是否电机已经运动完成，并处于空闲状态，默认查询状态 10S，间隔 200ms
        /// </summary>
        /// <param name="stepper"></param>
        /// <param name="repeats"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static async Task<bool> IsStepperMotorFree(StepperMotorEnum stepper, int repeats = 50, int timeout = 200)
        {
            return await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, stepper), DataProcesser.ConditionReturn0, 50, 200);
        }
        #endregion

        #region 重置位置
        /// <summary>
        /// 重置移液枪位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static async Task<bool> ResetPGXYZ()
        {
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor16_PipetteGun_Z4, false), DataProcesser.ConditionReturn2))//重置Z4
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor15_PipetteGun_Z3, false), DataProcesser.ConditionReturn2))//重置Z3
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor14_PipetteGun_Z2, false), DataProcesser.ConditionReturn2))//重置Z2
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor13_PipetteGun_Z1, false), DataProcesser.ConditionReturn2))//重置Z1
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1), DataProcesser.ConditionReturn5, 50, 200)//Z1到达指定地点
               || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2), DataProcesser.ConditionReturn5, 50, 200)//Z2到达指定地点
               || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3), DataProcesser.ConditionReturn5, 50, 200)//Z3到达指定地点
               || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4), DataProcesser.ConditionReturn5, 50, 200))//Z4到达指定地点
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor6_PipetteGun_Y, false), DataProcesser.ConditionReturn2))//重置Y
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor2_PipetteGun_X, false), DataProcesser.ConditionReturn2))//重置X
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X), DataProcesser.ConditionReturn5, 50, 200)//X到达指定地点
               || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y), DataProcesser.ConditionReturn5, 50, 200))//Y到达指定地点
            {
                return false;
            }
            PGLocation = new XYZDPoint(0, 0, 0);
            return true;
        }
        public static async Task<bool> ResetPG_Only_Z()
        {
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor16_PipetteGun_Z4, false), DataProcesser.ConditionReturn2))//重置Z4
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor15_PipetteGun_Z3, false), DataProcesser.ConditionReturn2))//重置Z3
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor14_PipetteGun_Z2, false), DataProcesser.ConditionReturn2))//重置Z2
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, StepperMotorEnum.Motor13_PipetteGun_Z1, false), DataProcesser.ConditionReturn2))//重置Z1
            {
                return false;
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1), DataProcesser.ConditionReturn5, 50, 200)//Z1到达指定地点
               || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2), DataProcesser.ConditionReturn5, 50, 200)//Z2到达指定地点
               || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3), DataProcesser.ConditionReturn5, 50, 200)//Z3到达指定地点
               || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4), DataProcesser.ConditionReturn5, 50, 200))//Z4到达指定地点
            {
                return false;
            }
            PGLocation = new XYZDPoint(PGLocation.x, PGLocation.y, 0);
            return true;
        }
        /// <summary>
        /// 重置抓钩位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public static async Task<bool> ResetZG()
        //{
        //    //1/先打开抓钩
        //    //2.收Z，收XY
        //    if (
        //        !await DataProcesser.SendDataAsync(ClawModuleView.GetControlClawState(false), DataProcesser.ConditionReturn2))//重置Z
        //    {
        //        return false;
        //    }
        //    await Task.Delay(500);

        //    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, MSPretreatment.Enums.StepperMotorEnum.Motor9_Claw_Z, false), DataProcesser.ConditionReturn2)
        //        || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z), DataProcesser.ConditionReturn5, 50, 200))
        //    {
        //        return false;
        //    }
        //    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, MSPretreatment.Enums.StepperMotorEnum.Motor1_Claw_X, false), DataProcesser.ConditionReturn2)//重置X
        //     || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, MSPretreatment.Enums.StepperMotorEnum.Motor5_Claw_Y, false), DataProcesser.ConditionReturn2)//重置Y
        //     )
        //    {
        //        return false;
        //    }
        //    ZGLocation = new XYZDPoint(0, 0, 0);
        //    return true;
        //}
        //public static async Task<bool> ResetZG_Only_Z()
        //{
        //    if (!await DataProcesser.SendDataAsync(ClawModuleView.GetControlClawState(false), DataProcesser.ConditionReturn2))//重置Z
        //    {
        //        return false;
        //    }
        //    await Task.Delay(500);
        //    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(PipetteGunAddr, MSPretreatment.Enums.StepperMotorEnum.Motor9_Claw_Z, false), DataProcesser.ConditionReturn2))//重置Z4
        //    {
        //        return false;
        //    }
        //    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z), DataProcesser.ConditionReturn5, 50, 200))//Z1到达指定地点
        //    {
        //        return false;
        //    }
        //    ZGLocation = new XYZDPoint(ZGLocation.x, ZGLocation.y, 0);
        //    return true;
        //}
        #endregion

        #region 移动
        public static async Task<bool> ClawMove(long _X, long _y, long _z)
        {
            return await ClawMove(new XYZDPoint(_X, _y, _z));
        }
        public static async Task<bool> ClawMove(XYZDPoint point)
        {
            if (point.x <= 0 || point.y <= 0) { return false; }
            long current_X = point.x - ZGLocation.x;
            long current_Y = point.y - ZGLocation.y;
            int back_z = 0;
            if (point.z == 0)
            {
                if (!await ResetPG_Only_Z())
                    return false;
            }
            if (ZGLocation.z > SafeStep)
            {       //回退到5000位置处
                back_z = (int)ZGLocation.z - SafeStep;
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z, false, back_z), DataProcesser.ConditionReturn2)
                   || !await IsStepperMotorFree(StepperMotorEnum.Motor9_Claw_Z))
                {
                    return false;
                }
            }
            long current_Z = (CoverTargetStepZ ? TargetStepZ : point.z) - ZGLocation.z;
            if (current_X != 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor1_Claw_X, current_X >= 0, (int)Math.Abs(current_X)), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            if (current_Y != 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor5_Claw_Y, current_Y >= 0, (int)Math.Abs(current_Y)), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor5_Claw_Y), DataProcesser.ConditionReturn0or5, 50, 200)//Y到达指定地点
                || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor1_Claw_X), DataProcesser.ConditionReturn0, 50, 200))////X到达指定地点
            {
                return false;
            }
            if (current_Z > 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z, current_Z > 0, (int)Math.Abs(current_Z) + back_z), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            else if (current_Z == 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z, back_z > 0, (int)Math.Abs(back_z)), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            else
            {
                current_Z = current_Z + back_z;
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z, current_Z > 0, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            LogCore.CreateInstance().AsyncLog($"==================================={ZGLocation}--->{current_X}|{current_Y}|{current_Z}--->[{point.x}|{point.y}|{(CoverTargetStepZ ? TargetStepZ : point.z)}]===================================");
            ZGLocation = new XYZDPoint(point.x, point.y, (CoverTargetStepZ ? TargetStepZ : point.z));
            return await IsStepperMotorFree(StepperMotorEnum.Motor9_Claw_Z);
        }
        public static async Task<bool> PGMove(long _X, long _y, long _z)
        {
            return await PGMove(new XYZDPoint(_X, _y, _z));
        }
        public static async Task<bool> PGMove(XYZDPoint point)
        {
            if (point.x <= 0 || point.y <= 0) { return false; }
            long current_X = point.x - PGLocation.x;
            long current_Y = point.y - PGLocation.y;
            int back_z = 0;
            if (point.z == 0)
            {
                if (!await ResetPG_Only_Z())
                    return false;
            }
            if (PGLocation.z > SafeStep)
            {       //回退到5000位置处
                back_z = (int)PGLocation.z - SafeStep;
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1, false, back_z), DataProcesser.ConditionReturn2)
                   || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2, false, back_z), DataProcesser.ConditionReturn2)
                   || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3, false, back_z), DataProcesser.ConditionReturn2)
                   || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4, false, back_z), DataProcesser.ConditionReturn2)
                   || !await IsStepperMotorFree(StepperMotorEnum.Motor13_PipetteGun_Z1)
                || !await IsStepperMotorFree(StepperMotorEnum.Motor14_PipetteGun_Z2)
                || !await IsStepperMotorFree(StepperMotorEnum.Motor15_PipetteGun_Z3)
                || !await IsStepperMotorFree(StepperMotorEnum.Motor16_PipetteGun_Z4))
                {
                    return false;
                }
            }
            long current_Z = (CoverTargetStepZ ? TargetStepZ : point.z) - PGLocation.z;
            if (current_X != 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X, current_X >= 0, (int)Math.Abs(current_X)), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            if (current_Y != 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y, current_Y >= 0, (int)Math.Abs(current_Y)), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y), DataProcesser.ConditionReturn0or5, 50, 200)//Y到达指定地点
                || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X), DataProcesser.ConditionReturn0, 50, 200))////X到达指定地点
            {
                return false;
            }
            if (current_Z > 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1, current_Z > 0, (int)Math.Abs(current_Z) + back_z), DataProcesser.ConditionReturn2)
                    || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2, current_Z > 0, (int)Math.Abs(current_Z) + back_z), DataProcesser.ConditionReturn2)
                    || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3, current_Z > 0, (int)Math.Abs(current_Z) + back_z), DataProcesser.ConditionReturn2)
                    || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4, current_Z > 0, (int)Math.Abs(current_Z) + back_z), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            else if (current_Z == 0)
            {

                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1, back_z > 0, (int)Math.Abs(back_z)), DataProcesser.ConditionReturn2)
                        || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2, back_z > 0, (int)Math.Abs(back_z)), DataProcesser.ConditionReturn2)
                        || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3, back_z > 0, (int)Math.Abs(back_z)), DataProcesser.ConditionReturn2)
                        || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4, back_z > 0, (int)Math.Abs(back_z)), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            else
            {
                current_Z = current_Z + back_z;
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1, current_Z > 0, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2)
                        || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2, current_Z > 0, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2)
                        || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3, current_Z > 0, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2)
                        || !await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4, current_Z > 0, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2))
                {
                    return false;
                }
            }
            LogCore.CreateInstance().AsyncLog($"==================================={PGLocation}--->{current_X}|{current_Y}|{current_Z}--->[{point.x}|{point.y}|{(CoverTargetStepZ ? TargetStepZ : point.z)}]===================================");
            PGLocation = new XYZDPoint(point.x, point.y, (CoverTargetStepZ ? TargetStepZ : point.z));
            return await IsStepperMotorFree(StepperMotorEnum.Motor13_PipetteGun_Z1)
                && await IsStepperMotorFree(StepperMotorEnum.Motor14_PipetteGun_Z2)
                && await IsStepperMotorFree(StepperMotorEnum.Motor15_PipetteGun_Z3)
                && await IsStepperMotorFree(StepperMotorEnum.Motor16_PipetteGun_Z4);
        }
        #endregion

    }
}
