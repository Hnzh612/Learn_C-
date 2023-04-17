using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using Log;

namespace SerialPortClass
{
    /// <summary>
    /// 查找指定目标串口（根据返回的协议判断）
    /// 1.设定查找串口的间隔事件
    /// 2.设定串口数据返回的超时事件
    /// 3.设定用户检验串口返回数据的数组
    /// 4.通知外部已经找到了串口
    /// </summary>
    public class SerialPortFinder : IDisposable
    {
        private static SerialPortFinder s_instance;
        private static bool _isWorking = false;
        private static readonly object _obj = new object();
        private Predicate<byte[]> _condition;
        public StopBits StopBits { get; private set; }
        public Parity Parity { get; private set; }
        public int BaudRate { get; private set; }
        public int DataBits { get; private set; }
        public static SerialPort RightPort { get; private set; }
        private static readonly object s_lock = new object();

        private int BeforeOpenWaitTime = 100;
        private int AfterOpenWaitTime = 1500;
        public byte[] Handshake { get; private set; }
        public byte[] BytesShouldBeReturn { get; private set; }
        public string STargetPortName { get; private set; }
        private string[] ComNames => SerialPort.GetPortNames();
        public static event Action<ComFinderStateEnum, string> TargetSerialPortFoundEventHandler;
        public static SerialPortFinder CreateInstance()
        {
            if(null == s_instance)
            {
                lock(_obj)
                {
                    if(null == s_instance) 
                    { 
                        s_instance = new SerialPortFinder();
                    }
                }
            }
            return s_instance;
        }
        private SerialPortFinder() { }
        public SerialPortFinder InitializeSerialPortData(StopBits stopBits,Parity parity,int baudRate,int dataBits)
        {
            StopBits = stopBits;
            Parity = parity;
            BaudRate = baudRate;
            DataBits = dataBits;
            return this;
        }
        public SerialPortFinder SetWaitTime(int befor,int after)
        {
            BeforeOpenWaitTime = befor;
            AfterOpenWaitTime = after;
            return this;
        }
        public SerialPortFinder SetHandShake(byte[] hsBytes)
        {
            Handshake = hsBytes;
            return this;
        }
        public SerialPortFinder SetReceiveData(byte[] hsBytes)
        {
            if (hsBytes == null || hsBytes.Length <= 1)
            {
                throw new ArgumentNullException("BytesShouldBeReturn should not be null.");
            }
            BytesShouldBeReturn = hsBytes;
            return this;
        }
        public SerialPortFinder SetReceiveDataCondition(Predicate<byte[]> _condition) 
        {
            this._condition = _condition;
            return this;
        }
        private void TargetFound(ComFinderStateEnum finderState, string comName)
        {
            if(finderState == ComFinderStateEnum.Found)
            {
                STargetPortName = comName;
                TargetSerialPortFoundEventHandler?.Invoke(finderState, comName);
            }
        }
        public void Start()
        {
            if(ComNames == null ||  ComNames.Length <= 0)
            {
                _isWorking = false;
                TargetFound(ComFinderStateEnum.Error, "");
            }
            if(!_isWorking)
            {
                FinderStartWork();
            }
        }
        public bool IsContains(byte[] bigBytes, byte[] smallBytes)
        {
            if(bigBytes == null || bigBytes.Length <= 1 || smallBytes == null || smallBytes.Length <= 1)
            {
                throw new ArgumentNullException("Pls check your arguments.");
            }
            if(bigBytes.Length < smallBytes.Length)
            {
                return false;
            }
            if(bigBytes.Length < 5)
            {
                return false;
            }
            for(int i = 0; i < bigBytes.Length; i++)
            {
                if (bigBytes[i] == smallBytes[i] && bigBytes[i+1] == smallBytes[i+1] && bigBytes[i+2] == smallBytes[i+2] && bigBytes[i+3] == smallBytes[i+3] && bigBytes[i+4] == smallBytes[i+4]) 
                {
                    return true;
                }
            }
            return false;
        }
        private async void FinderStartWork()
        {
            await Task.Run(() =>
            {
                bool IsFound = false;
                while(!IsFound)
                {
                    try
                    {
                        Parallel.ForEach(ComNames, (portName) =>
                        {
                            bool thisPortIsRight = false;
                            bool thisPortIsOpened = false;
                            SerialPort parallel_sp = new SerialPort
                            {
                                StopBits = StopBits,
                                Parity = Parity,
                                BaudRate = BaudRate,
                                DataBits = DataBits,
                            };
                            if (BytesShouldBeReturn == null || BytesShouldBeReturn.Length <= 1)
                            {
                                throw new ArgumentNullException($"Pls Check {nameof(BytesShouldBeReturn)}");
                            }
                            try
                            {
                                if (!parallel_sp.IsOpen)
                                {
                                    parallel_sp.PortName = portName;
                                    Thread.Sleep(BeforeOpenWaitTime);
                                    parallel_sp.Open();
                                    parallel_sp.DiscardOutBuffer();
                                    parallel_sp.DiscardInBuffer();
                                    if (Handshake != null && Handshake.Length >= 1)
                                    {
                                        parallel_sp.Write(Handshake, 0, Handshake.Length);
                                    }
                                    Thread.Sleep(AfterOpenWaitTime);
                                }
                                if (parallel_sp.IsOpen)
                                {
                                    if (parallel_sp.BytesToRead <= 0)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        byte[] receivedData = new byte[parallel_sp.BytesToRead];
                                        parallel_sp.Read(receivedData, 0, receivedData.Length);
                                        if (IsContains(receivedData, BytesShouldBeReturn) || _condition?.Invoke(receivedData) == true)
                                        {
                                            lock (s_lock)
                                            {
                                                if (!IsFound)
                                                {
                                                    thisPortIsRight = IsFound = true;
                                                    RightPort = parallel_sp;
                                                    TargetFound(ComFinderStateEnum.Found, portName);
                                                }
                                            }   
                                        }
                                    };
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                LogCore.CreateInstance().AsyncLog(ex);
                            }
                            finally
                            {
                                if(parallel_sp.IsOpen && !thisPortIsOpened && !thisPortIsRight)
                                {
                                    parallel_sp.Close();
                                }
                            }
                        });

                    }
                    catch (Exception ex)
                    {
                        LogCore.CreateInstance().AsyncLog(ex);
                    }
                }
            });
        }
        bool isDisposing = false;
        public void Dispose()
        {
            if (!isDisposing)
            {
                isDisposing = true;
                if (RightPort != null)
                {
                    RightPort.Dispose();
                }
            }
        }
    }
}
