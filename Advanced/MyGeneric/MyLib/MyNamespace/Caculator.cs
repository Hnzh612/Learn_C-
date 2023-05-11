using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.MyNamespace
{
    class Caculator
    {
        // public 外部可以引用 internal MyLib内部可以引用
        public double Add(double a, double b)
        {
            return a + b;
        }
    }
}
