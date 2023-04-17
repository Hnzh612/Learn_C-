using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Models
{
    /// <summary>
    /// Serialport return data state;
    /// return  datas state  &  data content.
    /// </summary>
    public readonly struct DeviceInfo
    {
        public DeviceInfo(int _dataAddr, int _state, string _data)
        {
            DataAddr = _dataAddr;
            DataState = _state;
            Data = _data;
        }
        /// <summary>
        /// Addr code.
        /// </summary>
        public readonly int DataAddr;
        /// <summary>
        /// State code.
        /// </summary>
        public readonly int DataState;
        /// <summary>
        /// data string.
        /// </summary>
        public readonly string Data;
    }
}
