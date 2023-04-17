using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SerialPort_wpf.Models
{
    /// <summary>
    /// 指定的一个三维的点，通常用来描述 在操作台中XYZ轴步进电机的步数(步数会转换成50空占比的脉冲发送给下位机)步距角360/1600
    /// </summary>
    public struct XYZDPoint
    {
        public XYZDPoint(long _X, long _y, long _z)
        {
            x = _X;
            y = _y;
            z = _z;
        }
        public XYZDPoint(XYZDPoint point)
        {
            x = point.x;
            y = point.y;
            z = point.z;
        }
        public XYZDPoint(Point point)
        {
            x = (long)point.X;
            y = (long)point.Y;
            z = 0;
        }
        public long x;
        public long y;
        public long z;
        public static XYZDPoint operator +(XYZDPoint point, XYZDPoint temp)
        {
            return new XYZDPoint(point.x + temp.x, point.y + temp.y, point.z + temp.z);
        }
        public static XYZDPoint operator -(XYZDPoint point, XYZDPoint temp)
        {
            return new XYZDPoint(point.x - temp.x, point.y - temp.y, point.z - temp.z);
        }
        public override string ToString()
        {
            return $"{x}|{y}|{z}";
        }
    }
}
