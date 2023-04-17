using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Log;
using SerialPort_wpf.Models;
using SerialPort_wpf.Tools;
using SerialPort_wpf.Modules;
using SerialPort_wpf.Enums;
using SerialPort_wpf.Static;
using SerialPortClass;
using System.Xml.Serialization;
using System.IO;

namespace SerialPort_wpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 构造方法
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _MARKER.Visibility = Visibility.Visible;
                _map = ReadStepMapConfigFile();
                _logAction = (s) => { _log.Text = s; };
                SerialPortFinder.TargetSerialPortFoundEventHandler += (state, com) =>
                {
                    if (state == ComFinderStateEnum.Error)
                    {
                        MessageBox.Show("寻找失败");
                    }
                    else if (state == ComFinderStateEnum.Found)
                    {
                        DataProcesser.CreateInstance(SerialPortFinder.RightPort);
                        DataProcesser.DataReceiveEventHandle += DataProcesser_DataReceivedEventHandle;
                        SetLog("连接成功");
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            POST_Start();
                        }));
                    }
                    else
                    {
                        MessageBox.Show($"{state}:{com}");
                    }
                };
                SerialPortFinder.CreateInstance().InitializeSerialPortData(StopBits.One, Parity.None, 115200, 8).SetHandShake(new byte[] { 0xAA, 0x00, 0x00, 0xAA }).SetReceiveData(new byte[] { 0x55, 0x00, 0x00, 0x00, 0x55 }).SetWaitTime(100, 100).Start();
            }
            catch (Exception ex)
            {

                LogCore.CreateInstance().AsyncLog(ex);
            }
        }
        #endregion

        #region 事件订阅
        Action<string> _logAction;
        private void DataProcesser_DataReceivedEventHandle(int arg1, string arg2)
        {
            DeviceInfo info;
            SetLog($"[DataState={arg1}]  [Data={arg2}]");
        }
        public void SetLog(string str)
        {
            this.Dispatcher.Invoke(_logAction, str);
        }
        #endregion

        #region POST_Start
        int xxx = 0;
        int maxCheck = 10;
        private static bool IsClosing = false;
        async Task<bool> POST_Start(int _repeats = 3, int _timeOut = 500)
        {
            try
            {
                _MARKER.Visibility = Visibility.Visible;
                _jindu.Value = 0;
                int _xpd = 6;
                /*
                 * 获取 x y,z1,z2,z3,z4电机状态
                 * 获取 xy z
                 * 
                 */
                if (xxx++ > maxCheck)
                {
                    return false;
                }
                await Task.Delay(1 * 1000);
                _jindu.Value = 0; _jinduText.Text=$"初始化电机速度--{_jindu.Value}%";
                LogCore.CreateInstance().AsyncLog("自检");
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X, 8000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y, 5000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1, 30000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2, 30000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3, 30000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4, 30000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor1_Claw_X, 8000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor5_Claw_Y, 8000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetAlterStepperMotorSpeed(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z, 20000), DataProcesser.ConditionReturn2);
                _jindu.Value += _xpd; _jinduText.Text = $"初始化电机速度--{_jindu.Value}%";
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X, 1000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y, 1000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1, 1000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2, 1000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3, 1000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4, 1000), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor1_Claw_X, 800), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor5_Claw_Y, 800), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetFrequencyUpDownTime(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z, 800), DataProcesser.ConditionReturn2);
                _jindu.Value += _xpd; _jinduText.Text = $"检测移液枪 Z1 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1))//检测移液枪 Z1 是否在零位           
                {
                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测移液枪 Z2 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2))//检测移液枪 Z2 是否在零位
                {
                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测移液枪 Z3 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3))//检测移液枪 Z3 是否在零位
                {
                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测移液枪 Z4 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4))//检测移液枪 Z4 是否在零位
                {
                    return await POST_Start();
                }

                _jindu.Value += _xpd; _jinduText.Text = $"检测移液枪 X 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X))//检测移液枪 X 是否在零位
                {
                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测移液枪 Y 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y))//检测移液枪 Y 是否在零位
                {

                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测抓钩 Z 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor9_Claw_Z))//检测抓钩 Z 是否在零位
                {

                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测抓钩 X 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor1_Claw_X))//检测抓钩 X 是否在零位
                {

                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测抓钩 Y 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor5_Claw_Y))//检测抓钩 Y 是否在零位
                {
                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测氮吹 Z 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor10_NitrogenBlowing_Z))//检测氮吹 Z 是否在零位
                {
                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测氮吹 Y 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor3_NitrogenBlowing_Y))//检测氮吹 Y 是否在零位
                {
                    return await POST_Start();
                }
                _jindu.Value += _xpd; _jinduText.Text = $"检测正压 Y 是否在零位--{_jindu.Value}%";
                if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor7_Barotropic_Y))//检测正压 Y 是否在零位
                {
                    return await POST_Start();
                }
                if (_jindu.Value > 95)
                {
                    _jindu.Value = 95;
                }
                _jindu.Value += 1; _jinduText.Text = $"开始去掉枪头--{_jindu.Value}%";
                await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X, true, (int)StepMap.Waste1.x), DataProcesser.ConditionReturn2);
                await DataProcesser.SendDataAsync(StepperMotorModule.GetMakeStepperMotorOffSet(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y, true, (int)StepMap.Waste1.y), DataProcesser.ConditionReturn2);
                if (await DataProcesser.SendDataAsync(StepperMotorModule.GetStepperMotorState(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y), DataProcesser.ConditionReturn0, 15, 200))//1.重复检测移液枪是否已经到达了指定位置
                {
                    _jindu.Value += _xpd; _jinduText.Text = $"初始化移液枪--{_jindu.Value}%";
                    await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunInitialization(StepperMotorModule.PipleGun_Z1_Addr, 30000, 100, 1), DataProcesser.ConditionReturn2, 5, 1000);
                    await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunInitialization(StepperMotorModule.PipleGun_Z2_Addr, 30000, 100, 1), DataProcesser.ConditionReturn2, 5, 1000);
                    await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunInitialization(StepperMotorModule.PipleGun_Z3_Addr, 30000, 100, 1), DataProcesser.ConditionReturn2, 5, 1000);
                    await DataProcesser.SendDataAsync(PipetteGunModuleView.PipetteGunInitialization(StepperMotorModule.PipleGun_Z4_Addr, 30000, 100, 1), DataProcesser.ConditionReturn2, 5, 1000);
                    await DataProcesser.SendDataAsync(PipetteGunModuleView.GetPipetteGunState(StepperMotorModule.PipleGun_Z1_Addr), DataProcesser.ConditionReturn0, 30, 100);
                    await DataProcesser.SendDataAsync(PipetteGunModuleView.GetPipetteGunState(StepperMotorModule.PipleGun_Z2_Addr), DataProcesser.ConditionReturn0, 30, 100);
                    await DataProcesser.SendDataAsync(PipetteGunModuleView.GetPipetteGunState(StepperMotorModule.PipleGun_Z3_Addr), DataProcesser.ConditionReturn0, 30, 100);
                    await DataProcesser.SendDataAsync(PipetteGunModuleView.GetPipetteGunState(StepperMotorModule.PipleGun_Z4_Addr), DataProcesser.ConditionReturn0, 30, 100);
                    if (!await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor2_PipetteGun_X)//检测抓钩 X 是否在零位
                        || !await StepperMotorModule.SetStepperMotorBackToZero(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor6_PipetteGun_Y))
                    {

                        return await POST_Start();
                    }
                }
                _jindu.Value += _xpd; _jinduText.Text = $"初始化混匀模块--{_jindu.Value}%";
                await DataProcesser.SendDataAsync(MixingVibrationModuleView.SetMixVibrationSpeed(100, 1), DataProcesser.ConditionReturn2);
                await Task.Delay(1 * 1000);
                _jindu.Value = 100; _jinduText.Text = $"自检完成--{_jindu.Value}%";
                await Task.Delay(1000);
                if (this != null && IsClosing)
                {
                    return false;
                }

                _MARKER.Visibility = Visibility.Hidden;
                _main.Visibility = Visibility.Visible;
                return true;
            }
            catch (Exception ex)
            {
                LogCore.CreateInstance().AsyncLog($"SendDataAsync:task Error Happend!:{ex.Message}");
                return false;
            }
        }
        #endregion

        #region 属性
        XMLStepMap _map {  get; set; }
        string mapPath { get; set; }
        #endregion

        #region 方法
        public XMLStepMap ReadStepMapConfigFile()
        {
            XMLStepMap xsm = new XMLStepMap();
            XmlSerializer mySerializer = new XmlSerializer(typeof(XMLStepMap));
            mapPath = Directory.GetCurrentDirectory();
            if (string.IsNullOrEmpty(mapPath))
            {
                throw new Exception("Directory.GetCurrentDirectory() error");
            }
            foreach(string file in Directory.GetFiles(mapPath, "*.xml"))
            {
                try
                {
                    using(var myFileStream = new FileStream(file, FileMode.Open))
                    {
                        xsm = (XMLStepMap)mySerializer.Deserialize(myFileStream);
                        if(xsm != null && xsm.Version >= 1)
                        {
                            mapPath = file;
                            return xsm;
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
            return null;
        }
        #endregion

        #region 遍历样品区

        #endregion

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_map.SimpleArea1.Start.x <= 0 || _map.SimpleArea1.Start.y <= 0
                || _map.SimpleArea1.End.x <= 0 || _map.SimpleArea1.End.y <= 0
                || _map.SimpleArea2.Start.x <= 0 || _map.SimpleArea2.Start.y <= 0
                || _map.SimpleArea2.End.x <= 0 || _map.SimpleArea2.End.y <= 0
                || _map.SimpleArea3.Start.x <= 0 || _map.SimpleArea3.Start.y <= 0
                || _map.SimpleArea3.End.x <= 0 || _map.SimpleArea3.End.y <= 0
                || _map.SimpleArea4.Start.x <= 0 || _map.SimpleArea4.Start.y <= 0
                || _map.SimpleArea4.End.x <= 0 || _map.SimpleArea4.End.y <= 0)
            {
                MessageBox.Show("数据文件不可用,验证不通过,或者Map定位不全！请检查定位文件！"); return;
            }
            _grid.IsEnabled = false;
            int _columnCount = 4; // 不同机器可能会不一样
            XYZDPoint[,] points = new XYZDPoint[24, _columnCount]; // 24行 4列
            List<StepLine> _lines = new List<StepLine>();
            _lines.Add(_map.SimpleArea4);
            _lines.Add(_map.SimpleArea3);
            _lines.Add(_map.SimpleArea2);
            _lines.Add(_map.SimpleArea1);
            for (int i = 0; i < 24; i++)
            {
                for (int j = 0; j < _lines.Count; j++)
                {
                    //float rowsXStep = (_lines[j].End.x - _lines[j].Start.x) / 20.0f;  // X轴不变 可以不要 加上也影响不大，几步对于点击来说几乎没移动，在误差允许范围内。
                    float rowsYStep = (_lines[j].End.y - _lines[j].Start.y) / 20.0f;
                    //int _currentPointX = (int)(_lines[j].Start.x + rowsXStep * i);
                    int _currentPointX = (int)(_lines[j].Start.x);
                    int _currentPointY = (int)(_lines[j].Start.y + rowsYStep * i);
                    points[i, j] = new XYZDPoint(_currentPointX, _currentPointY, _lines[j].Start.z);
                }
            }
            XYZDPoint[] simples = new XYZDPoint[96]; 
            for (int i = 0;i < 96; i++)
            {
                simples[i] = new XYZDPoint(points[i % 24,3 - (i/24)]);
                if(i % 4 == 0)
                {
                    if(!await Controller.PGMove(simples[i]))
                    {
                        MessageBox.Show($"ERROR{simples[i]}");
                        return;
                    }
                }
            }
            _grid.IsEnabled = true;
            MessageBox.Show("遍历完成");
        }
    }
}
