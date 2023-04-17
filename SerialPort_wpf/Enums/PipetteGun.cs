using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Enums
{
    public enum PipetteGun
    {
        None = 0,
        /// <summary>
        /// 移液枪1
        /// </summary>
        PipetteGunZ1 = 01,
        /// <summary>
        /// 移液枪2 靠近X轴为4
        /// </summary>
        PipetteGunZ2 = 02,
        /// <summary>
        /// 移液枪3 靠近X轴为4
        /// </summary>
        PipetteGunZ3 = 04,
        /// <summary>
        /// 移液枪4 靠近X轴为4
        /// </summary>
        PipetteGunZ4 = 08
    }
}
