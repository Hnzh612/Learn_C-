using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polymorphism
{
    public class Shape
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Height { get; set; }
        public int Width { get; set; }

        // 虚方法
        public virtual void Draw()
        {
            Console.WriteLine("执行基类的画图任务");
        }
    }

    class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("画一个圆形");
            base.Draw();
        }
    }
    class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("画一个长方形");
            base.Draw();
        }
    }
    class Triangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("画一个三角形");
            base.Draw();
        }
    }
}
