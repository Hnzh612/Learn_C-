using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace overrideExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car c = new XPeng();
            c.Run();
        }
        class Vehicle
        {
            public virtual void Run()
            {
                Console.WriteLine("I'm running!");
            }
        }
        class Car : Vehicle
        {
            public int Speed { get; set; }
            public override void Run()
            {
                Console.WriteLine("Car is running");
            }
        }
        class XPeng : Car
        {
            public override void Run()
            {
                Console.WriteLine("小鹏 is running");
            }
        }
    }
}
