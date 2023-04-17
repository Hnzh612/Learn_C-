using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_prictices
{
    public struct StepLine
    {
        public StepLine(XYZDPoint _x, XYZDPoint _y)
        {
            Start = _x;
            End = _y;
        }
        public void SetStart(XYZDPoint _x)
        {
            Start = _x;
        }
        public void SetEnd(XYZDPoint _y)
        {
            End = _y;
        }
        /// <summary>
        /// 开始坐标
        /// </summary>
        public XYZDPoint Start { get; set; }
        /// <summary>
        /// 结尾坐标
        /// </summary>
        public XYZDPoint End { get; set; }
    }
}
