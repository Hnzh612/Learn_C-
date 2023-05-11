using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.MyNamespace2;

namespace AccessControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Car car = new Car();
            //car.turboAccelerate();
            //Console.WriteLine(car.Speed);
            Bus bus = new Bus();
            bus.SlowAccelerate();
            Console.WriteLine(bus.Speed);
        }
    }
    class Bus : Vehicle
    {
        public void SlowAccelerate()
        {
            Burn(1);
            _rpm += 500;
        }
    }
}
