using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloOOP
{
    internal class Program
    {
        // 什么是继承
        // 继承就是子类在完整接受父类成员的前提下，对父类进行横向和纵向的扩展，横向扩展指的是对类成员个数的扩充，纵向扩展指的是对类成员版本的更新或者是重写。
        static void Main(string[] args)
        {
            //Type t = typeof(Vehicle);
            //Type tb = t.BaseType;
            //Console.WriteLine(tb.FullName); System.Object
            // 是一个 is a  子类的实例也是父类的实例
            //Vehicle vehicle = new Vehicle();
            //Vehicle vehicle1 = new Car();
            //Car car = new Car();
            //Object o1 = new Vehicle();
            //Object o2 = new Car();
            //Console.WriteLine(vehicle is Object);
            Car car = new Car("Hnzh");
            car.ShowOwner();
        }
    }
    // sealed 密封，不能被继承
    // 一个类只能继承一个基类
    // 子类的访问级别不能超过父类
    public class Vehicle
    {
        public Vehicle(string owner) 
        {
            this.Owner = owner;
        }
        public string Owner { get; set; }
    }
    // 在继承的过程中，实例构造器是不能被继承的
    class Car:Vehicle
    {
        //public Car():base("N/A")
        //{
        //    this.Owner = "Car Owner";
        //}
        public Car(string owner):base(owner)
        {
            
        }
        public void ShowOwner()
        {
            Console.WriteLine(Owner);
        }
    }
    //class RaceCar : Car
    //{

    //}
}
