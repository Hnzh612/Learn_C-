using SerialPort_wpf.Enums;
using SerialPort_wpf.Modules;
using SerialPort_wpf.Static;
using SerialPort_wpf.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Log;

namespace SerialPort_wpf.Models
{
    public class PipetteGunModule : Module
    {
        public PipetteGunModule()
        {
            CurrentTypeEnum = Enums.ModuleTypeEnum.PipetteGunModule;
        }
        public PipetteGunModule(float? coefficient)
        {
            CurrentTypeEnum = Enums.ModuleTypeEnum.PipetteGunModule;
        }
        /// <summary>
        ///  吸液体积(μl)[0, 1000]
        /// </summary>
        public int? ImbibitionVolume { get; set; }
        /// <summary>
        /// 吸液速度(μl/s)[0, 2000]
        /// </summary>
        public int? ImbibitionSpeed { get; set; }
        /// <summary>
        /// 截留速度(ul/s)[0, 2000]
        /// </summary>
        public int? InterceptionSpeed { get; set; }
        /// <summary>
        /// 排液体积(μl)[0,100000]
        /// </summary>
        public int? DrainVolume { get; set; }
        /// <summary>
        /// 回吸体积0.01ul[0,10000]
        /// </summary>
        public int? ResorptionVolume { get; set; }
        /// <summary>
        /// 排液速度(μl/s)[0,20000]
        /// </summary>
        public int? DrainSpeed { get; set; }
        /// <summary>
        /// 停止速度(ul/s)[0,2000]
        /// </summary>
        public int? StopSpeed { get; set; }
        /// <summary>
        /// 系数
        /// </summary>
        public float? Coefficient { get; set; }

        public override async Task<bool> CheckData()
        {
            return ImbibitionVolume != null
                  && ImbibitionSpeed != null
                  && DrainVolume != null
                  && DrainSpeed != null;
        }
        public override async Task<bool> Execute()
        {
            if (!await SetUp())
            {
                return false;
            }
            LogCore.CreateInstance().AsyncLog("========================================================[PipetteGunModule.Execute]========================================================");
            //初始吸液枪速度
            if (!await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1, 10000), DataProcesser.ConditionReturn2)
             || !await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2, 10000), DataProcesser.ConditionReturn2)
             || !await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3, 10000), DataProcesser.ConditionReturn2)
             || !await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4, 10000), DataProcesser.ConditionReturn2))
            {
                return false;
            }

            return await OneForAll();
        }

        private async Task<bool> OneForAll()
        {
            SimpleInfo[] infos = Procedure.CreateInstance().GetSimples();//.OrderBy(p=>p.Index).ToArray();
            for (int i = 0; i < infos.Count(); i += 4)
            {
                if (infos[i].IsUsable
                    || infos[i + 1].IsUsable
                    || infos[i + 2].IsUsable
                    || infos[i + 3].IsUsable)
                {
                    int fgAddr = i - i % 4;
                    int tipaddr = i / 4;
                    PipetteGun gun = PipetteGun.None;
                    if (infos[i].IsUsable)
                    {
                        gun = gun | PipetteGun.PipetteGunZ1;
                    }
                    if (infos[i + 1].IsUsable)
                    {
                        gun = gun | PipetteGun.PipetteGunZ2;
                    }
                    if (infos[i + 2].IsUsable)
                    {
                        gun = gun | PipetteGun.PipetteGunZ3;
                    }
                    if (infos[i + 3].IsUsable)
                    {
                        gun = gun | PipetteGun.PipetteGunZ4;
                    }
                    //1.取枪头
                    //if (!await TakeTIPs(StepMap.TIPArea1[i / 48, i / 4], gun))
                    {
                        LogCore.CreateInstance().AsyncLog($"{infos[fgAddr].Index} TakeTIPs Failed!!"); return false;
                    }
                    //2.吸液
                    if (!await Aspirate(fgAddr, infos))
                    {
                        LogCore.CreateInstance().AsyncLog($"{infos[fgAddr].Index} Aspirate Failed!!"); return false;
                    }
                    //3.排液
                    if (!await PipetteDrain(StepMap.MixingVibrationArea[i / 48, i / 4], gun, DrainVolume.Value, DrainSpeed.Value, ResorptionVolume.Value, StopSpeed.Value))
                    {
                        LogCore.CreateInstance().AsyncLog($"{infos[fgAddr].Index} PipetteDrain Failed!!"); return false;
                    }

                    //4.丢弃枪头
                    if (!await StepperMotorModule.PipetteGunMoveTo(StepMap.Waste1, PipetteGun.None))
                    {
                        LogCore.CreateInstance().AsyncLog($"PipetteMoveTo {StepMap.Waste1} Failed!!");
                        return false;
                    }
                    if (!await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunRemoveTIP(StepperMotorModule.PipleGun_Z1_Addr, 32000, false), DataProcesser.ConditionReturn2, 5, 1000)
                    || !await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunRemoveTIP(StepperMotorModule.PipleGun_Z2_Addr, 32000, false), DataProcesser.ConditionReturn2, 5, 1000)
                    || !await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunRemoveTIP(StepperMotorModule.PipleGun_Z3_Addr, 32000, false), DataProcesser.ConditionReturn2, 5, 1000)
                    || !await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunRemoveTIP(StepperMotorModule.PipleGun_Z4_Addr, 32000, false), DataProcesser.ConditionReturn2, 5, 1000))
                    {
                        LogCore.CreateInstance().AsyncLog($"PipetteGunRemoveTIP {StepMap.Waste1} Failed!!");
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 吸液[4个枪]
        /// </summary>
        /// <returns></returns>
        async Task<bool> Aspirate(int NearlyIndex, SimpleInfo[] infos)
        {
            if (await StepperMotorModule.PipetteGunMoveTo(infos[NearlyIndex].CurrentLocation, PipetteGun.None))
            {
                if (infos[NearlyIndex].IsUsable)
                {
                    if (!await AspirateSingle(StepperMotorEnum.Motor13_PipetteGun_Z1, 25500, 10 * 1000, ImbibitionVolume.Value, ImbibitionSpeed.Value, InterceptionSpeed.Value)) { return false; }
                }
                if (infos[NearlyIndex + 1].IsUsable)
                {
                    if (!await AspirateSingle(StepperMotorEnum.Motor14_PipetteGun_Z2, 25500, 10 * 1000, ImbibitionVolume.Value, ImbibitionSpeed.Value, InterceptionSpeed.Value)) { return false; }
                }
                if (infos[NearlyIndex + 2].IsUsable)
                {
                    if (!await AspirateSingle(StepperMotorEnum.Motor15_PipetteGun_Z3, 25500, 10 * 1000, ImbibitionVolume.Value, ImbibitionSpeed.Value, InterceptionSpeed.Value)) { return false; }
                }
                if (infos[NearlyIndex + 3].IsUsable)
                {
                    if (!await AspirateSingle(StepperMotorEnum.Motor16_PipetteGun_Z4, 25500, 10 * 1000, ImbibitionVolume.Value, ImbibitionSpeed.Value, InterceptionSpeed.Value)) { return false; }
                }
                return await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1)
                    && await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2)
                    && await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3)
                    && await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4);
            }
            return false;
        }
        async Task<bool> AspirateSingle(StepperMotorEnum stepper, int maxZstep, int timeOut, int xiyeliang, int xiyesudu, int jieliusudu)
        {
            int PipleGun_Addr = 0;
            switch (stepper)
            {
                case StepperMotorEnum.Motor13_PipetteGun_Z1: PipleGun_Addr = StepperMotorModule.PipleGun_Z1_Addr; break;
                case StepperMotorEnum.Motor14_PipetteGun_Z2: PipleGun_Addr = StepperMotorModule.PipleGun_Z2_Addr; break;
                case StepperMotorEnum.Motor15_PipetteGun_Z3: PipleGun_Addr = StepperMotorModule.PipleGun_Z3_Addr; break;
                case StepperMotorEnum.Motor16_PipetteGun_Z4: PipleGun_Addr = StepperMotorModule.PipleGun_Z4_Addr; break;
                default: break;
            }
            if (await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, stepper, true, maxZstep), DataProcesser.ConditionReturn2)//指示Z下降
                && await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunLiquidLevelDetection(PipleGun_Addr, true, timeOut), DataProcesser.ConditionReturn4, 1, timeOut)//页面探测
                && await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorStop(StepperMotorModule.PipleGun_XY_Addr, stepper), DataProcesser.ConditionReturn2)
                && await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, stepper, true, 1350), DataProcesser.ConditionReturn2))
            /*
             * 
             * 此处应该有计算耗材体积以便于计算出下探多少步后 可以满足吸取量的要求,目前暂时设定固定值3350
             * 
             * 
             * 
             */
            {
                if (await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, stepper), DataProcesser.ConditionReturn0, 20, 100))
                {
                    LogCore.CreateInstance().AsyncLog("GetStepperMotorState failed"); return false;
                }
                return await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunAspirate(PipleGun_Addr, xiyeliang, xiyesudu, jieliusudu), DataProcesser.ConditionReturn2)
                    && await DataProcesser.SendDataAsync(PipetteGunModuleView.GetPipetteGunState(PipleGun_Addr), DataProcesser.ConditionReturn0, 30, 100);
            }
            return false;
        }
        private async Task<bool> TakeTIPs(XYZDPoint point, PipetteGun gun)
        {
            if (!await StepperMotorModule.PipetteGunMoveTo(point, gun))
            {
                LogCore.CreateInstance().AsyncLog($"PipetteMoveTo {point} Failed!!");
                return false;
            }
            if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1))//检测移液枪 Z1 是否在零位
            {
                LogCore.CreateInstance().AsyncLog("SetStepperMotorBackToZero Z1 Failed!"); return false;
            }
            if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2))//检测移液枪 Z2 是否在零位
            {
                LogCore.CreateInstance().AsyncLog("SetStepperMotorBackToZero Z2 Failed!"); return false;
            }
            if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3))//检测移液枪 Z3 是否在零位
            {
                LogCore.CreateInstance().AsyncLog("SetStepperMotorBackToZero Z3 Failed!"); return false;
            }
            if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4))//检测移液枪 Z4 是否在零位
            {
                LogCore.CreateInstance().AsyncLog("SetStepperMotorBackToZero Z4 Failed!"); return false;
            }
            return true;
        }
        private async Task<bool> PipetteDrain(XYZDPoint point, PipetteGun gun, int pytj, int pysd, int hxtj, int tzsd)
        {
            if (!await StepperMotorModule.PipetteGunMoveTo(point, gun))
            {
                LogCore.CreateInstance().AsyncLog($"PipetteMoveTo {point} Failed!!");
                return false;
            }
            int temp = (int)gun;
            if ((temp & 1) == 1)//z1
            {
                if (!await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunDrain(StepperMotorModule.PipleGun_Z1_Addr, pytj, hxtj, pysd, tzsd), DataProcesser.ConditionReturn2))
                {
                    LogCore.CreateInstance().AsyncLog($"PipetteGunDrain Z1 Failed!!");
                }
            }
            if ((temp & 2) == 1)//z2
            {
                if (!await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunDrain(StepperMotorModule.PipleGun_Z2_Addr, pytj, hxtj, pysd, tzsd), DataProcesser.ConditionReturn2))
                {
                    LogCore.CreateInstance().AsyncLog($"PipetteGunDrain Z2 Failed!!");
                }
            }
            if ((temp & 4) == 1)//z3
            {
                if (!await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunDrain(StepperMotorModule.PipleGun_Z3_Addr, pytj, hxtj, pysd, tzsd), DataProcesser.ConditionReturn2))
                {
                    LogCore.CreateInstance().AsyncLog($"PipetteGunDrain Z3 Failed!!");
                }
            }
            if ((temp & 8) == 1)//z4
            {
                if (!await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunDrain(StepperMotorModule.PipleGun_Z4_Addr, pytj, hxtj, pysd, tzsd), DataProcesser.ConditionReturn2))
                {
                    LogCore.CreateInstance().AsyncLog($"PipetteGunDrain Z1 Failed!!");
                }
            }
            await Task.Delay(2000);
            return await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1)
                && await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2)
                && await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3)
                && await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4);
        }
        public override async Task<bool> SetUp()
        {
            if (!await CheckData())
            {
                return false;
            }
            LogCore.CreateInstance().AsyncLog("========================================================[PipetteGunModule.SetUp]========================================================");
            SimpleInfo[] simpleInfos = Procedure.CreateInstance().GetSimples();
            for (int i = 0; i < simpleInfos.Count(); i++)
            {
                if (simpleInfos[i].IsUsable)
                {

                }
            }
            Procedure.CreateInstance().SetSimples(simpleInfos);

            return true;
        }
        public override async Task<bool> TearDown()
        {
            LogCore.CreateInstance().AsyncLog("========================================================[PipetteGunModule.SetUp]========================================================");
            return true;
        }
    }
}
