using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPort_wpf.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
    public class ParaDescribleAttribute : Attribute
    {
        public string Describe { get;private set; }
        public object Min { get; private set; }
        public object Max { get; private set; }
        public ParaDescribleAttribute(string _describle)
        {
            Describe = _describle;
        }
        public ParaDescribleAttribute(string _describle,object _min,object _max)
        {
            Describe = _describle;
            Min = _min;
            Max = _max;
        }
    }
}
