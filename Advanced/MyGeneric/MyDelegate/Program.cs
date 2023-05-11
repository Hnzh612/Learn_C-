using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDelegate
{
    // 声明委托的时候 委托与所封装的方法必须“类型兼容”
    public delegate double Cacl(double x, double y);
    class Program
    {
        //static void Main(string[] args)
        //{
        //    // 委托是一种类
        //    //Type t = typeof(Action);
        //    //Console.WriteLine(t.IsClass);

        //    //Calculator calculator = new Calculator();
        //    //Action action = new Action(calculator.Report);
        //    //// 直接调用
        //    //calculator.Report();
        //    //// 间接调用
        //    //action.Invoke();
        //    //action();

        //    //Func<int, int, int> func1 = new Func<int, int, int>(calculator.Add);
        //    //Func<int, int, int> func2 = new Func<int, int, int>(calculator.Sub);
        //    //func2.Invoke(200,100);
        //    //int z = func1.Invoke(100, 200);
        //    //Console.WriteLine(z); 
        //    //z = func2.Invoke(100, 200);
        //    //Console.WriteLine(z);

        //    Calculator calculator = new Calculator();
        //    Cacl cacl1 = new Cacl(calculator.Add);
        //    Cacl cacl2 = new Cacl(calculator.Sub);
        //    Cacl cacl3 = new Cacl(calculator.Mul);
        //    Cacl cacl4 = new Cacl(calculator.Div);

        //    double a = 100;
        //    double b = 200;
        //    double c = 0;
        //    c = cacl1(a,b);
        //    Console.WriteLine(c);
        //    c = cacl2(a, b);
        //    Console.WriteLine(c);
        //    c = cacl3(a, b);
        //    Console.WriteLine(c);
        //    c = cacl4(a, b);
        //    Console.WriteLine(c);
        //}
    }
     class Calculator
    {
        public void Report()
        {
            Console.WriteLine("I have 3 methods.");
        }
        public double Add(double a, double b) 
        {
            double result = a + b;
            return result;
        }
        public double Sub(double a, double b)
        {
            double result = a - b;
            return result;
        }
        public double Mul(double a, double b)
        {
            double result = a * b;
            return result;
        }
        public double Div(double a, double b)
        {
            double result = a / b;
            return result;
        }

    }
}
