using Log;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SerialPort_wpf.Models;
using SerialPort_wpf.Enums;
using System.Diagnostics;
using SerialPortClass;
using SerialPort_wpf.Modules;

namespace SerialPort_wpf.Tools
{
    public class DataProcesser
    {
        #region filed
        public static string s_Separator => ",";
        private static DataProcesser d_instance;
        private static object _obj = new object();
        private static SerialPort _sp;
        private static byte s_Head = 0xAA;
        private static bool s_Running = false;
        private static LogCore s_Runlog = LogCore.CreateInstance();
        private static readonly AutoResetEvent _autoHandle = new AutoResetEvent(false);
        private static Predicate<DeviceInfo> _conditionReturn;
        private static Func<DeviceInfo, MSPResult> _conditionResult;
        #endregion

        #region event
        public static event Action<int, string> DataReceiveEventHandle;
        #endregion

        #region Constructor
        public static DataProcesser CreateInstance(SerialPort temp)
        {
            if(null == d_instance)
            {
                lock(_obj)
                {
                    if(null == d_instance )
                    {
                        if(_sp == null || _sp != temp)
                        {
                            d_instance = new DataProcesser();
                            s_Runlog.AsyncLog($"==================================================================[数据处理和通讯已准备好]==================================================================");
                            _sp = temp;
                        }
                    }
                }
            }
            return d_instance;
        }
        private DataProcesser() 
        { 
            s_Running = true;
            Thread _thread = new Thread(ReceivedMethod);
            _thread.IsBackground = true;
            _thread.Start();
        }
        #endregion

        #region senddata
        private static Stopwatch Stopwatch = new Stopwatch();
        /// <summary>
        /// 数据发出后,应该在限定的时间内返回满足指定条件的数据,满足 则返回true
        /// 如果不满足或在限定时间外收到了返回，则会尝试重复执行指定的次数,超出改次数后 方法返回false
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_condition"></param>
        /// <param name="_repeats"></param>
        /// <param name="_timeout"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static async Task<bool> SendDataAsync(byte[] _data,Predicate<DeviceInfo> _condition, int _repeats = 3,int _timeout = 500, string methodName = "")
        {
            return await Task.Run(() =>
            {
                try
                {
                    _conditionReturn = _condition;
                    for(int i = 0;i < _repeats;i++)
                    {
                        SendData(_data);
                        Stopwatch.Restart();
                        if (_autoHandle.WaitOne(_timeout))
                        {
                            Stopwatch.Stop();
                            return true;
                        }
                        Stopwatch.Stop();
                        if(_data == null || _data.Length == 0)
                        {
                            throw new ArgumentException("发送的数据不对");
                        }
                        s_Runlog.AsyncLog($"超时发生");
                    }
                    if (_data == null || _data.Length == 0)
                    {
                        throw new ArgumentException("发送的数据不对");
                    }
                    s_Runlog.AsyncLog($"多次超时，{DataConverter.HexBytesToHexStr(_data)}发送失败!!");
                    return false;
                }
                catch (Exception ex)
                {
                    if(Stopwatch.IsRunning)
                    {
                        Stopwatch.Stop();
                    }
                    s_Runlog.AsyncLog($"SendDataAsync:task Error Happend!:{ex.Message}");
                    return false;
                }
                finally
                {
                    _conditionResult = null;
                }
            });
        }

        public static async Task<bool> SendDataAsync(byte[] _data, Func<DeviceInfo,MSPResult> _func, int _repeats = 3, int _timeout = 500, string methodName = "")
        {
            return await Task.Run(() =>
            {
                try
                {
                    _conditionResult = _func;
                    for (int i = 0; i < _repeats; i++)
                    {
                        SendData(_data);
                        Stopwatch.Restart();
                        if (_autoHandle.WaitOne(_timeout))
                        {
                            Stopwatch.Stop();
                            return true;
                        }
                        Stopwatch.Stop();
                        if (_data == null || _data.Length <= 0)
                        {
                            throw new ArgumentException("asdasdsdasdasd");
                        }                        //{System.Reflection.MethodBase.GetCurrentMethod()}()
                        s_Runlog.AsyncLog($"超时发生！！！！当前允许的超时为{_timeout}ms,重发【{DataConverter.HexBytesToHexStr(_data)}】");
                    }
                    if (_data == null || _data.Length <= 0)
                    {
                        throw new ArgumentException("asdasdsdasdasd");
                    }
                    s_Runlog.AsyncLog($"多次超时，{DataConverter.HexBytesToHexStr(_data)}发送失败!!");
                    return false;
                }
                catch (Exception ex)
                {
                    if (Stopwatch.IsRunning)
                    {
                        Stopwatch.Stop();
                    }
                    s_Runlog.AsyncLog($"SendDataAsync:task Error Happend!:{ex.Message}");
                    return false;
                }
                finally
                {
                    _conditionResult = null;
                }
            });
        }

        public static bool SendData(byte[] data)
        {
            try
            {
                if(_sp != null)
                {
                    if(_sp.IsOpen)
                    {
                        _sp.Write(data, 0, data.Length);
                        s_Runlog.AsyncLog($"发送数据：【{Encoding.ASCII.GetString(data)}");
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                s_Runlog.AsyncLog($"SendData:Error Happend!:{ex.Message}");
                return false;
            }
        }
        public static bool SendData(string data)
        {
            try
            {
                if (_sp != null)
                {
                    if (_sp.IsOpen)
                    {
                        _sp.Write(data);
                        s_Runlog.AsyncLog($"发送数据:{data}");
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region receiveddata
        /// <summary>
        /// 接收数据
        /// </summary>
        public static void ReceivedMethod()
        {
            List<byte> AllReceivedData = new List<byte>();
            int? _spCanReadLength = 0;
            while(s_Running)
            {
                try
                {
                    _spCanReadLength = _sp?.BytesToRead;
                    if(_spCanReadLength >= 1)
                    {
                        byte[] _receivedData = new byte[_spCanReadLength.Value];
                        _sp.Read(_receivedData, 0, _receivedData.Length);
                        AllReceivedData.AddRange(_receivedData);
                        byte[] _result = UnPackageProtocol(ref AllReceivedData);
                        if(_result != null && _result.Length >=1)
                        {//[0x55] [Addr] [State] [DataLen] [Data] [Check]
                            DeviceInfo data = new DeviceInfo(_result[1], _result[2], _result[3] == 0x00 ? "" : Encoding.ASCII.GetString(_result.SubBytes(4, 3 + _result[3])));
                            if(_conditionReturn?.Invoke(data) == true)
                            {
                                _autoHandle.Set();
                            }
                            Notification(data);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if(AllReceivedData != null || AllReceivedData.Count >= 1)
                    {
                        s_Runlog.AsyncLog(ex, $"ReceivedMethod happened【{DataConverter.HexBytesToHexStr(AllReceivedData.ToArray())}】!:{ex.Message}");
                    }
                    throw ex;
                }
            }
        }
        #endregion

        #region package
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_dataList"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string PackageData(CommandTypeEnum _key, int[] _dataList)
        {
            // 入参检查
            if(_dataList == null || _dataList.Length <= 0)
            {
                throw new ArgumentException("数据列表_dataList为空");
            }
            return $"{_key}{string.Join(s_Separator,_dataList).Replace("-1","")}";
        }

        /// <summary>
        /// 包头  Uint8 0xAA
        /// 地址 Uint8  0-4：控制板  5-8：移液枪地址
        /// 数据长度 Uint8 数据区的长度
        /// 数据区 Char[长度不固定]
        /// 校验 Uint8  AA++ 数据区的和的后8位
        /// </summary>
        /// <param name="_address"></param>
        /// <param name="_asciiData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] PackageBytes(ushort _address, string _asciiData)
        {
            //入参检查
            if(_address <=0 || _address > 16)
            {
                throw new ArgumentException($"地址{_address}错误");
            }
            if (string.IsNullOrEmpty(_asciiData))
            {
                throw new ArgumentException("数据指令为空");
            }
            List<byte> packageData = new List<byte>();
            int sum = s_Head;
            packageData.Add(s_Head);
            byte _addr = (byte)_address;
            packageData.Add(_addr);
            sum += _addr;
            byte[] _data = Encoding.ASCII.GetBytes(_asciiData);
            byte _length = (byte)_data.Length;
            packageData.Add(_length);
            sum += _length;
            foreach(byte b in _data)
            {
                packageData.Add(b);
                sum += b;
            }
            byte _check = (byte)(sum & 0xff);
            packageData.Add(_check);
            return packageData.ToArray();
        }
        #endregion

        #region UnPackage
        private static bool CheckData(byte[] _bigBytes)
        {
            if(_bigBytes == null || _bigBytes.Length <=0 ||  _bigBytes.Length < 2)
            {
                throw new ArgumentException("UnPackage.CheckData:data length can not <2.");
            }
            return _bigBytes.GetSum(0, _bigBytes.Length - 2) == _bigBytes[_bigBytes.Length - 1];
        }

        public static byte[] UnPackageProtocol(ref byte[] _bigBytes)
        {
            try
            {
                int _bigLength = _bigBytes.Length;
                for (int i = 0; i < _bigLength; i++)
                {
                    if (i <= _bigLength - 5)//[0x55] [Addr] [State] [DataLen] [Data] [Check]
                    {
                        // 包头+（[state]<=44）
                        if (_bigBytes[i] == 0x55 && _bigBytes[i + 2] <= 44)
                        {
                            int _dataLength = _bigBytes[i + 3];
                            int _checkByteIndex = _dataLength + 4;
                            if (_checkByteIndex >= _bigBytes.Length)
                            {
                                return null;
                            }
                            if (_bigBytes.GetSum(i, _checkByteIndex) == _bigBytes[_checkByteIndex])
                            {
                                byte[] newBytes = new byte[_checkByteIndex - i + 1];
                                Array.Copy(_bigBytes, i, newBytes, 0, newBytes.Length);
                                _bigBytes = _bigBytes.RemoveRange(i, newBytes.Length - 1);
                                return newBytes;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                s_Runlog.AsyncLog($"UnPackageSSSWProtocol:ex:{e.Message}");
            }
            return null;
        }
        public static byte[] UnPackageProtocol(ref List<byte> _bigBytes)
        {
            try
            {
                int _bigLength = _bigBytes.Count;
                for (int i = 0; i < _bigLength; i++)
                {
                    if (i <= _bigLength - 5)//[0x55] [Addr] [State] [DataLen] [Data] [Check]
                    {                       //  55     00      02      00  57
                                            //  55     04      05      00  5E
                        if (_bigBytes[i] == 0x55 && _bigBytes[i + 2] <= 25)//包头+([state]<=17)
                        {
                            int _dataLenght = _bigBytes[i + 3];//0
                            int _checkByteIndex = i + _dataLenght + 4;//(00 02 0x checkbit)
                            if (_checkByteIndex >= _bigBytes.Count)
                            {
                                return null;
                            }
                            if (_bigBytes.ToArray().GetSum(i, _checkByteIndex) == _bigBytes[_checkByteIndex])
                            {
                                s_Runlog.AsyncLog($"收到数据：{DataConverter.HexBytesToHexStr(_bigBytes.ToArray())} [stateCode:{(int)_bigBytes[i + 2]}]");
                                byte[] newBytes = new byte[_checkByteIndex - i + 1];
                                Array.Copy(_bigBytes.ToArray(), i, newBytes, 0, newBytes.Length);
                                _bigBytes.RemoveRange(i, newBytes.Length);
                                return newBytes;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                s_Runlog.AsyncLog($"UnPackageSSSWProtocol:ex:{e.Message}");
            }
            return null;
        }
        #endregion

        #region Notification
        private static void Notification(DeviceInfo _data)
        {
            DataReceiveEventHandle?.Invoke(_data.DataState, $"【{_data.Data}】");
        }
        #endregion

        #region Condition
        public static bool ConditionReturn0(DeviceInfo _info)
        {
            // return ConditionReturnAll(_info);
            return _info.DataState == 0;
        }
        public static bool ConditionReturn0or5(DeviceInfo _info)
        {
            // return ConditionReturnAll(_info);
            return _info.DataState == 0 || _info.DataState == 5;
        }
        public static bool ConditionReturn2(DeviceInfo _info)
        {
            // return ConditionReturnAll(_info);
            return _info.DataState == 2;
        }
        public static bool ConditionReturn4(DeviceInfo _info)
        {
            //return ConditionReturnAll(_info);
            return _info.DataState == 4;
        }
        public static bool ConditionReturn5(DeviceInfo _info)
        {
            // return ConditionReturnAll(_info);
            return _info.DataState == 5;
        }
        public static bool ConditionReturnHasTip(DeviceInfo _info)
        {
            s_Runlog.AsyncLog($"检测枪头[state:{_info.DataState}] [data：{_info.Data}]");
            return _info.DataState == 2 && (_info.Data == "31" || _info.Data == "1");
        }
        public static bool ConditionReturnAll(DeviceInfo _info)
        {
            return _info.DataState == 0 || _info.DataState == 1 || _info.DataState == 2 || _info.DataState == 4 || _info.DataState == 5;
        }
        //public static bool ConditionReturnStop(DeviceInfo _info)
        //{
        //    s_Runlog.AsyncLog($"检测枪头[state:{_info.DataState}] [data：{_info.Data}]");
        //    if (_info.DataState != 4)
        //    {
        //        return false;
        //    }
        //    switch (_info.DataAddr)
        //    {
        //        case 5: SendData(StepperMotorModule.GetMakeStepperMotorStop(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor13_PipetteGun_Z1)); return true;
        //        case 6: SendData(StepperMotorModule.GetMakeStepperMotorStop(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor14_PipetteGun_Z2)); return true;
        //        case 7: SendData(StepperMotorModule.GetMakeStepperMotorStop(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor15_PipetteGun_Z3)); return true;
        //        case 8: SendData(StepperMotorModule.GetMakeStepperMotorStop(StepperMotorModule.PipleGun_XY_Addr, StepperMotorEnum.Motor16_PipetteGun_Z4)); return true;
        //        default: s_Runlog.AsyncLog($"检测枪头[DataAddr:{_info.DataAddr}]out of range！"); return false;
        //    }
        //}
        #endregion
    }
}
