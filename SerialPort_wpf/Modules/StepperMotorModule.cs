using SerialPort_wpf.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SerialPort_wpf.Enums;
using SerialPort_wpf.Tools;
using SerialPort_wpf.Models;
using Log;

namespace SerialPort_wpf.Modules
{
    public partial class StepperMotorModule : UserControl
    {
        public PlatformArea CurrentPlatform { get; set; } = PlatformArea.Null;

        public XYZDPoint CurrentXYZPoint { get; set; } = new XYZDPoint(0, 0, 0);

        /// <summary>
        /// 静态且全局,当有任何有关电机步进的操作的时候,这个属性应该相应的做出改变
        /// </summary>
        public static XYZDPoint CurrentLocation { get; set; } = new XYZDPoint(0, 0, 0);

        public int CurrentIndex { get; set; } = -1;

        public XYZDPoint[,] Area { get; set; } = null;

        public bool IsWorking { get; set; } = false;

        public int AllSteps { get; set; } = -1;

        public int StepIndex { get; set; } = -1;

        public int Repeats { get; set; } = -1;
        public static int PipleGun_XY_Addr => 2;
        public static int PipleGun_Z1_Addr => 5;
        public static int PipleGun_Z2_Addr => 6;
        public static int PipleGun_Z3_Addr => 7;
        public static int PipleGun_Z4_Addr => 8;

        #region PipetteGunMoveTo
        /// <summary>
        /// 移动移液枪[z1作为参考]最里面的枪到指定的地方
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static async Task<bool> PipetteGunMoveTo(XYZDPoint point, PipetteGun gun)
        {
            if (point.x <= 0 || point.y <= 0) { return false; }
            long current_X = point.x - CurrentLocation.x;
            long current_Y = point.y - CurrentLocation.y;
            long current_Z = point.z - CurrentLocation.z;
            if (CurrentLocation.z >= 1920)
            {
                LogCore.CreateInstance().AsyncLog($"当前Z轴不在区间内,如果移动会导致撞断轴");
                return false;
            }
            if (current_X != 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X, current_X >= 0, (int)Math.Abs(current_X)), DataProcesser.ConditionReturn2))
                {
                    LogCore.CreateInstance().AsyncLog("GetMakeStepperMotorOffSet Motor2_PipetteGun_X failed!,ConditionReturn2"); return false;
                }
            }
            if (current_Y != 0)
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y, current_Y >= 0, (int)Math.Abs(current_Y)), DataProcesser.ConditionReturn2))
                {
                    LogCore.CreateInstance().AsyncLog("GetMakeStepperMotorOffSet Motor6_PipetteGun_Y failed!,ConditionReturn2"); return false;
                }
            }
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y), DataProcesser.ConditionReturn0, 50, 200)//Y到达指定地点
                || !await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X), DataProcesser.ConditionReturn0, 50, 200))////X到达指定地点
            {
                LogCore.CreateInstance().AsyncLog("GetStepperMotorState Motor2_PipetteGun_X||Motor6_PipetteGun_Y failed!,ConditionReturn0"); return false;
            }
            if (current_Z > 0)
            {
                if ((gun & PipetteGun.PipetteGunZ1) == PipetteGun.PipetteGunZ1)//z1
                {
                    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1, true, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2))
                    {
                        LogCore.CreateInstance().AsyncLog("GetMakeStepperMotorOffSet Motor13_PipetteGun_Z1 failed!,ConditionReturn2"); return false;
                    }
                    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1), DataProcesser.ConditionReturn0, 50, 200))
                    {
                        LogCore.CreateInstance().AsyncLog("GetStepperMotorState Motor13_PipetteGun_Z1 failed!,ConditionReturn0"); return false;
                    }
                }
                if ((gun & PipetteGun.PipetteGunZ2) == PipetteGun.PipetteGunZ2)//z2
                {
                    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2, true, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2))
                    {
                        LogCore.CreateInstance().AsyncLog("GetMakeStepperMotorOffSet Motor13_PipetteGun_Z1 failed!,ConditionReturn2"); return false;
                    }
                    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2), DataProcesser.ConditionReturn0, 50, 200))
                    {
                        LogCore.CreateInstance().AsyncLog("GetStepperMotorState Motor13_PipetteGun_Z1 failed!,ConditionReturn0"); return false;
                    }
                }
                if ((gun & PipetteGun.PipetteGunZ3) == PipetteGun.PipetteGunZ3)//z3
                {
                    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3, true, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2))
                    {
                        LogCore.CreateInstance().AsyncLog("GetMakeStepperMotorOffSet Motor13_PipetteGun_Z1 failed!,ConditionReturn2"); return false;
                    }
                    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3), DataProcesser.ConditionReturn0, 50, 200))
                    {
                        LogCore.CreateInstance().AsyncLog("GetStepperMotorState Motor13_PipetteGun_Z1 failed!,ConditionReturn0"); return false;
                    }
                }
                if ((gun & PipetteGun.PipetteGunZ4) == PipetteGun.PipetteGunZ4)//z4
                {
                    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4, true, (int)Math.Abs(current_Z)), DataProcesser.ConditionReturn2))
                    {
                        LogCore.CreateInstance().AsyncLog("GetMakeStepperMotorOffSet Motor13_PipetteGun_Z1 failed!,ConditionReturn2"); return false;
                    }
                    if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4), DataProcesser.ConditionReturn0, 50, 200))
                    {
                        LogCore.CreateInstance().AsyncLog("GetStepperMotorState Motor13_PipetteGun_Z1 failed!,ConditionReturn0"); return false;
                    }
                }
            }
            LogCore.CreateInstance().AsyncLog($"==================Current:[{CurrentLocation}]  Move:{current_X}|{current_Y}   tartget:[{point}]==================");
            CurrentLocation = new XYZDPoint(point.x, point.y, point.z);
            return true;
        }
        #endregion
        #region SetStepperMotorBackToZero
        /// <summary>  
        ///  1.获取电机状态,查看是否在0位
        ///  2.如果不在0位,发送归零命令
        ///  3.再次检查点击状态是否在0位
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="stepper"></param>
        /// <returns></returns>
        public static async Task<bool> SetStepperMotorBackToZero(int addr, StepperMotorEnum stepper, bool dir = false, int loop = 50, int interal = 500)
        {
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(addr, stepper), DataProcesser.ConditionReturn5, 1, 500))//1. 检测电机是否在零位,检测一次,超时500ms      
            {
                if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorBackToZero(addr, stepper, dir), DataProcesser.ConditionReturn2))//2.发送回零命令
                {
                    LogCore.CreateInstance().AsyncLog("GetStepperMotorBackToZero failed!,ConditionReturn2"); return false;
                }
            }
            bool REULT = await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(addr, stepper), DataProcesser.ConditionReturn5, loop, interal);//3，每隔loop ms监测一次是否在零位,检测持续loop*interal /1000 s
            if (REULT)
            {
                CurrentLocation = new XYZDPoint(CurrentLocation.x, CurrentLocation.y, 0);
            }
            return REULT;
        }
        #endregion

        /// <summary>
        /// 获取 [设置升降频时间] 命令
        /// </summary>
        public static byte[] GetFrequencyUpDownTime(
            [ParaDescrible("(板|枪)地址", 1, 16), DefaultValue(1)] int _addr,
            [ParaDescrible("电机编号")] StepperMotorEnum _smNumber,
            [ParaDescrible("升降频时间", 32, 9999)] int _milliseconds)
        {
            if (!CheckFrequencyTime(_milliseconds))
            {
                throw new ArgumentOutOfRangeException($"{_milliseconds} out of  range.");
            }
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.A}{(int)_smNumber}{DataProcesser.s_Separator}{_milliseconds}");
        }
        /// <summary>
        /// 获取 [电机状态] 命令
        /// </summary>
        /// <param name="_smNumber"></param>
        /// <returns></returns>
        public static byte[] GetStepperMotorState([ParaDescrible("(板|枪)地址", 1, 16), DefaultValue(1)] int _addr, [ParaDescrible("电机编号")] StepperMotorEnum _smNumber)
        {
            LogCore.CreateInstance().AsyncLog($"获取电机状态({_addr},{_smNumber})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.C}{(int)_smNumber}");
        }
        /// <summary>
        ///获取 [电机回零] 命令
        /// </summary>
        /// <param name="_smNumber"></param>
        /// <param name="_isForwardDirection"></param>
        /// <returns></returns>
        public static byte[] GetStepperMotorBackToZero([ParaDescrible("(板|枪)地址", 1, 16), DefaultValue(1)] int _addr, [ParaDescrible("电机编号")] StepperMotorEnum _smNumber, [ParaDescrible("电机回零方向")] bool _isForwardDirection)
        {
            LogCore.CreateInstance().AsyncLog($"设置电机回零({_addr},{_smNumber},{_isForwardDirection})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.H}{(int)_smNumber}{DataProcesser.s_Separator}{(_isForwardDirection ? "0" : "1")}");
        }
        /// <summary>
        /// 获取 [移动电机] 命令
        /// 电机转动360° 在区间[1600,1800]≈10cm
        /// 每转动一周 位移大概10cm
        /// </summary>
        /// <param name="_smNumber"></param>
        /// <param name="_isForwardDirection"></param>
        /// <param name="_steps">步数</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>

        public static byte[] GetMakeStepperMotorOffSet([ParaDescrible("(板|枪)地址", 1, 16), DefaultValue(1)] int _addr, [ParaDescrible("电机编号")] StepperMotorEnum _smNumber, [ParaDescrible("电机移动方向")] bool _isForwardDirection, [ParaDescrible("电机移动步数", 0, 9999)] int _steps)
        {
            if (!CheckStepperMotorStep(_steps))
            {
                throw new ArgumentOutOfRangeException($"{_steps} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"让电机移动({_addr},{_smNumber},{_isForwardDirection},{_steps})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.P}{(int)_smNumber}{DataProcesser.s_Separator}{(_isForwardDirection ? "0" : "1")}{DataProcesser.s_Separator}{_steps}");
        }
        /// <summary>
        /// 获取 [设置电机运行频率] 命令
        /// </summary>
        /// <param name="_smNumber"></param>
        /// <param name="_Hz"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] GetAlterStepperMotorSpeed([ParaDescrible("(板|枪)地址", 1, 16), DefaultValue(1)] int _addr, [ParaDescrible("电机编号")] StepperMotorEnum _smNumber, [ParaDescrible("频率", 250, 9999)] int _Hz)
        {
            if (!CheckStepprMotorSpeed(_Hz))
            {
                throw new ArgumentOutOfRangeException($"{_Hz} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"设置电机运行频率({_addr},{_smNumber},{_Hz})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.S}{(int)_smNumber}{DataProcesser.s_Separator}{_Hz}");
        }
        /// <summary>
        ///获取 [停止电机]  命令
        /// </summary>
        /// <param name="_smNumber"></param>
        /// <returns></returns>
        public static byte[] GetMakeStepperMotorStop([ParaDescrible("(板|枪)地址", 1, 16), DefaultValue(1)] int _addr, [ParaDescrible("电机编号")] StepperMotorEnum _smNumber)
        {
            LogCore.CreateInstance().AsyncLog($"让电机停止({_addr},{_smNumber})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.T}{(int)_smNumber}");
        }
        /// <summary>
        /// 获取 [电磁阀开关控制]  命令 0=off 1=on
        /// </summary>
        /// <param name="_number"></param>
        /// <param name="_state"></param>
        /// <returns></returns>
        public static byte[] GetSolenoidValveOnOff([ParaDescrible("电磁阀编号", 1, 5), DefaultValue(1)] int _number, [ParaDescrible("开/关")] bool _state)
        {
            if (_number < 1
               || _number > 5)
            {
                throw new ArgumentException("GetSolenoidValveOnOff Argument error");
            }
            return DataProcesser.PackageBytes(3, $"{CommandTypeEnum.D},{_number},{(_state ? "1" : "0")}");
        }
        /// <summary>
        /// 获取 [加热膜控制] 命令
        /// </summary>
        /// <param name="_number">加热膜编号</param>
        /// <param name="_ssd">加热温度,0则关闭</param>
        /// <returns></returns>
        public static byte[] GetHeatingFilmControl([ParaDescrible("加热膜编号", 1, 3), DefaultValue(1)] int _number, [ParaDescrible("摄氏度")] ushort _ssd)
        {
            if (_number < 1
                || _number > 3//4，5预留位
                || _ssd < 0
                || _ssd > 110)
            {
                throw new ArgumentException("GetHeatingFilmControl Argument error");
            }
            return DataProcesser.PackageBytes(3, $"{CommandTypeEnum.R},{_number},{_ssd}");
        }
        /// <summary>
        /// 获取 [气路控制] 命令
        /// </summary>
        /// <param name="_qilu">1：氮气气路 2：正压气路</param>
        /// <param name="_qybl">气压比例 (千分比) 为0则关闭</param>
        /// <param name="_qlbl">气流比例 (千分比) 为0则关闭</param>
        /// <returns></returns>
        public static byte[] GetGasCircuitSetting([ParaDescrible("气路选择")] int _qilu, [ParaDescrible("气压比例")] int _qybl, [ParaDescrible("气流比例")] int _qlbl)
        {
            if (
                (_qilu != 1 && _qilu != 2)
                || _qybl < 0
                || _qybl > 1000
                || _qlbl < 0
                || _qlbl > 1000)
            {
                throw new ArgumentException("GetGasCircuitSetting Argument error");
            }
            return DataProcesser.PackageBytes(3, $"{CommandTypeEnum.U},{_qilu},{_qybl},{_qlbl}");
        }
        #region Argument Check
        private static bool CheckFrequencyTime(int _milliseconds)
        {
            return _milliseconds >= 32 && _milliseconds <= 9999;
        }
        private static bool CheckAddr(int _addr)
        {
            return _addr <= 16;
        }
        private static bool CheckStepperMotorStep(int _steps)
        {
            return _steps >= 0 && _steps <= 99999;
        }
        private static bool CheckStepprMotorSpeed(int _Hz)
        {
            return _Hz >= 250 && _Hz <= 99999;
        }

        public XYZDPoint GetXYZPoint(object _par)
        {
            throw new NotImplementedException();
        }

        public bool RunCommmondList()
        {
            throw new NotImplementedException();
        }
        public bool CheckData()
        {
            return false;
        }
        #endregion
        public bool Execute()
        {
            throw new NotImplementedException();
        }
    }
}
