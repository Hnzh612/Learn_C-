using SerialPort_wpf.Attributes;
using SerialPort_wpf.Enums;
using SerialPort_wpf.Interface;
using SerialPort_wpf.Models;
using SerialPort_wpf.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SerialPort_wpf.Modules
{
    internal class MixingVibrationModuleView : UserControl,IModule
    {
        public PlatformArea CurrentPlatform { get; set; } = PlatformArea.Mix_Area;

        public XYZDPoint CurrentXYZPoint { get; set; }

        public int CurrentIndex { get; set; }

        public XYZDPoint[,] Area { get; set; }

        public bool IsWorking { get; set; }

        public int AllSteps { get; set; }

        public int StepIndex { get; set; }

        public int Repeats { get; set; }


        public int ModuleID { get; private set; }

        /// <summary>
        ///获取 [混匀震荡控制]  命令
        /// </summary>
        /// <returns></returns>
        public static byte[] SetMixVibrationSpeed([ParaDescrible("转速r/min", 0, 1200)] int _Hz, [ParaDescrible("震荡时间(s)", 0, 999999)] int _second)
        {
            if (!CheckMixVibrationSpeed(_Hz) || !CheckMixVibrationTime(_second))
            {
                throw new ArgumentOutOfRangeException($"{_Hz} out of  range.");
            }
            return DataProcesser.PackageBytes((ushort)ControlBoard.StepperMotor, $"{CommandTypeEnum.Z}{_Hz}{DataProcesser.s_Separator}{_second}");
        }
        private static bool CheckMixVibrationSpeed(int _Hz)
        {
            return _Hz >= 0 && _Hz <= 1200;
        }
        private static bool CheckMixVibrationTime(int _second)
        {
            return _second >= 0 && _second <= int.MaxValue;
        }
    }
}
