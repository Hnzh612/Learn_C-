using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Models
{
    public enum MSPResult
    {
        /// <summary>
        /// 同步超时
        /// </summary>
        TimeOut,
        /// <summary>
        /// 指定时间内返回且条件符合
        /// </summary>
        Ok,
        /// <summary>
        /// 指定时间内返回,但条件不符合
        /// </summary>
        NotMatch
    }
}
