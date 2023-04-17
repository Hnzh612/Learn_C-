using SerialPort_wpf.Attributes;
using SerialPort_wpf.Enums;
using SerialPort_wpf.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Log;

namespace SerialPort_wpf.Modules
{
    /// <summary>
    /// PipetteGunModule.xaml 的交互逻辑
    /// </summary>
    public partial class PipetteGunModuleView : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string _propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
        #region property
        #endregion
        public MainWindow GetMainWindow()
        {
            var temp = this.Parent;
            while (!(temp is MainWindow))
            {
                temp = (temp as FrameworkElement).Parent;
            }
            return temp as MainWindow;
        }
        /// <summary>
        /// 初始化所有的移液枪
        /// </summary>
        /// <param name="_n1"></param>
        /// <param name="_n2"></param>
        /// <param name="_n3"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] PipetteGunInitialization([ParaDescrible("(板|枪)地址", 1, 16)] int _addr, [ParaDescrible("速度", 200, 64000)] int _n1, [ParaDescrible("功率", 0, 100)] int _n2, [ParaDescrible("是否顶出TIP头(0|1|2)", 0, 2)] int _n3)
        {
            if (_n1 < 0 || _n1 > 64000 || _n2 < 0 || _n2 > 100 || _n3 < 0 || _n3 > 2)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_n1}/{_n2}/{_n3} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"初始化移液枪({_addr},{_n1},{_n2},{_n3})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.It}{_n1}{DataProcesser.s_Separator}{_n2}{DataProcesser.s_Separator}{_n3}");
        }
        /// <summary>
        /// 控制移液枪吸液
        /// </summary>
        /// <returns></returns>
        public static byte[] PipetteGunAspirate([ParaDescrible("(板|枪)地址", 1, 16)] int _addr, [ParaDescrible("吸液体积ul", 0, 1000)] int _n1, [ParaDescrible("吸液速度1ul/s", 0, 2000)] int _n2, [ParaDescrible("截留速度1ul/s", 0, 2000)] int _n3)
        {
            if (_n1 < 0 || _n1 > 1000 || _n2 < 0 || _n2 > 2000 || _n3 < 0 || _n3 > 2000)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_n1}/{_n2}/{_n3} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"移液枪吸液({_addr},{_n1},{_n2},{_n3})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.Ia}{_n1 * 100}{DataProcesser.s_Separator}{_n2}{DataProcesser.s_Separator}{_n3}");
        }
        /// <summary>
        /// 控制移液枪排液
        /// </summary>
        /// <returns></returns>
        public static byte[] PipetteGunDrain([ParaDescrible("(板|枪)地址", 1, 16)] int _addr, [ParaDescrible("排液体积ul", 0, 100000)] int _n1, [ParaDescrible("回吸体积ul", 0, 10000)] int _n2, [ParaDescrible("排液速度1ul/s", 0, 2000)] int _n3, [ParaDescrible("停止速度1ul/s", 0, 2000)] int _n4)
        {
            if (_n1 < 0 || _n1 > 100000 || _n2 < 0 || _n2 > 10000 || _n3 < 0 || _n3 > 2000 || _n4 < 0 || _n4 > 2000)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_n1}/{_n2}/{_n3}/{_n4} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"移液枪排液({_addr},{_n1},{_n2},{_n3})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.Da}{_n1 * 100}{DataProcesser.s_Separator}{_n2 * 100}{DataProcesser.s_Separator}{_n3}{DataProcesser.s_Separator}{_n4}");
        }
        /// <summary>
        /// 去掉TIP头
        /// </summary>
        /// <param name="_addr"></param>
        /// <param name="_n1"></param>
        /// <param name="_n2">0[True]:无论检测到与否 都将顶出，1[False]：检测到后再顶出</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] PipetteGunRemoveTIP([ParaDescrible("(板|枪)地址", 1, 16)] int _addr, [ParaDescrible("顶出速度ul/s", 0, 100000)] int _n1, [ParaDescrible("顶出", 0, 1)] bool _n2)
        {
            if (_n1 < 0 || _n1 > 100000)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_n1} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"去掉TIP头({_addr},{_n1},{_n2})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.Dt}{_n1}{DataProcesser.s_Separator}{(_n2 ? "0" : "1")}");
        }
        /// <summary>
        /// 检测是否有TIP头
        /// </summary>
        /// <param name="_addr"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] PipetteGunIsHaveTIP([ParaDescrible("(板|枪)地址", 1, 16)] int _addr)
        {
            if (_addr < 0 || _addr > 10)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_addr} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"检测是否有TIP头({_addr})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.Rr}{3}{DataProcesser.s_Separator}{1}");
        }
        /// <summary>
        /// 液面检测
        /// </summary>
        /// <param name="_addr"></param>
        /// <param name="_n1"></param>
        /// <param name="_n2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] PipetteGunLiquidLevelDetection([ParaDescrible("(板|枪)地址", 1, 16)] int _addr, [ParaDescrible("是否自动上报", 0, 1)] bool _n1, [ParaDescrible("超时ms，0不检测超时", 0, 10000)] int _n2)
        {
            if (_n2 < 0 || _n2 > 10000)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_n2} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"液面探测({_addr},{_n1},{_n2})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.Ld}{(_n1 ? "1" : "0")}{DataProcesser.s_Separator}{_n2}");
        }
        /// <summary>
        /// 控制顶枪头的活塞移动到绝对位置
        /// </summary>
        /// <param name="_addr"></param>
        /// <param name="_n1"></param>
        /// <param name="_n2"></param>
        /// <param name="_n3"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] PipetteGunMove([ParaDescrible("(板|枪)地址", 1, 16)] int _addr, [ParaDescrible("位置", 0, 768000)] int _n1, [ParaDescrible("运行速度", 0, 256000)] int _n2, [ParaDescrible("停止速度", 0, 256000)] int _n3)
        {
            if (_n1 < 0 || _n1 > 768000 || _n2 < 0 || _n2 > 256000 || _n3 < 0 || _n3 > 256000)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_n1}/{_n2}/{_n3} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"绝对位置移动({_addr},{_n1},{_n2},{_n3})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.Mp}{_n1}{DataProcesser.s_Separator}{_n2}{DataProcesser.s_Separator}{_n3}");
        }
        /// <summary>
        /// 控制顶枪头的活塞相对当前位置向上移动吸液指定距离
        /// </summary>
        /// <param name="_addr"></param>
        /// <param name="_n1"></param>
        /// <param name="_n2"></param>
        /// <param name="_n3"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] PipetteGunMoveUpRelative([ParaDescrible("(板|枪)地址", 1, 16)] int _addr, [ParaDescrible("相对距离位置", 0, 768000)] int _n1, [ParaDescrible("运行速度", 0, 256000)] int _n2, [ParaDescrible("停止速度", 0, 256000)] int _n3)
        {
            if (_n1 < 0 || _n1 > 768000 || _n2 < 0 || _n2 > 256000 || _n3 < 0 || _n3 > 256000)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_n1}/{_n2}/{_n3} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"相对位置向上移动({_addr},{_n1},{_n2},{_n3})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.Up}{_n1}{DataProcesser.s_Separator}{_n2}{DataProcesser.s_Separator}{_n3}");
        }
        /// <summary>
        /// 控制顶枪头的活塞相对当前位置向下移动排液指定距离。
        /// </summary>
        /// <param name="_addr"></param>
        /// <param name="_n1"></param>
        /// <param name="_n2"></param>
        /// <param name="_n3"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] PipetteGunMoveDownRelative([ParaDescrible("(板|枪)地址", 1, 16)] int _addr, [ParaDescrible("相对距离位置", 0, 768000)] int _n1, [ParaDescrible("运行速度", 0, 256000)] int _n2, [ParaDescrible("停止速度", 0, 256000)] int _n3, [ParaDescrible("回弹速度", 0, 76800)] int _n4)
        {
            if (_n1 < 0 || _n1 > 768000 || _n2 < 0 || _n2 > 256000 || _n3 < 0 || _n3 > 256000 || _n4 < 0 || _n4 > 76800)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_n1}/{_n2}/{_n3}/{_n4} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"相对位置向下移动({_addr},{_n1},{_n2},{_n3})");
            return DataProcesser.PackageBytes((ushort)_addr, $"{CommandTypeEnum.Dp}{_n1}{DataProcesser.s_Separator}{_n2}{DataProcesser.s_Separator}{_n3}{DataProcesser.s_Separator}{_n4}");
        }
        /// <summary>
        /// 控制顶枪头的活塞相对当前位置向下移动排液指定距离。
        /// </summary>
        /// <param name="_addr"></param>
        /// <param name="_n1"></param>
        /// <param name="_n2"></param>
        /// <param name="_n3"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] GetPipetteGunState([ParaDescrible("移液枪地址", 5, 8)] int _addr)
        {
            if (_addr < 5 || _addr > 8)
            {
                throw new ArgumentOutOfRangeException($"[{System.Reflection.MethodBase.GetCurrentMethod()}]{_addr} out of  range.");
            }
            LogCore.CreateInstance().AsyncLog($"查询移液枪状态({_addr})");
            return DataProcesser.PackageBytes((ushort)_addr, $"?");
        }
    }
}
