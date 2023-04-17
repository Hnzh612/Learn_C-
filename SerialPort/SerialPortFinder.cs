using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace SerialPort
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
        public StopBits stopBits { get; private set; }
    }
}
