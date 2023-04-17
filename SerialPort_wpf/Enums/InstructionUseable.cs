using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Enums
{
    internal enum InstructionUseable
    {
        Procedure = 1,
        SetUp = 2,
        Module = 4,
        TearDown = 8
    }
}
