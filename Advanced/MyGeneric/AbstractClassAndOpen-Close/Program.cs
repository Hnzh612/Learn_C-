using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClassAndOpen_Close
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle v = new Car();
            v.Run();
        }
    }
    // 抽象类指的是函数成员们没有被完全实现的类
    abstract class Vehicle
    {
        public abstract void Run();
        public void Stop()
        {
            Console.WriteLine("Stopped!");
        }
    }
    class Car: Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("Car is running...");
        } 
    }
    class Truck: Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("Truck is running...");
        }
    }
    class RaceCar : Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("RaceCar is running...");
        }
    }
}
