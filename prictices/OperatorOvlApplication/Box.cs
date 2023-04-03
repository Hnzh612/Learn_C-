using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorOvlApplication
{
    class Box
    {
        private double length;
        private double width;
        private double height;

        public double getVolume()
        {
            return length * width *height;
        }
        public void setLength(double l)
        {
            length = l;
        }
        public void setWidth(double w) 
        { 
            width = w;
        }
        public void setHeight(double h)
        {
            height = h;
        }

        // 重载 + 运算符来把两个 Box 对象相加
        public static Box operator+ (Box left, Box right)
        {
            Box box = new Box();
            box.length = left.length + right.length;
            box.width = left.width + right.width;
            box.height = left.height + right.height;
            return box;
        }

        // 重载 == 比较两个对象是否相同
        public static bool operator == (Box left, Box right)
        {
            bool status = false;
            if(left.height== right.height && left.width==right.width&&left.length==right.length) 
            {
                status = true;
            }
            return status;
        }
        public static bool operator !=(Box left, Box right)
        {
            bool status = false;
            if (left.height != right.height || left.width != right.width || left.length != right.length)
            {
                status = true;
            }
            return status;
        }

        public override string ToString()
        {
            return String.Format($"{length},{width},{height}");
        }
    }
}
