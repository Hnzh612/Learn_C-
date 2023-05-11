using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.MyNamespace2
{
    // private会把成员限制在本类体之内
    public class Vehicle
    {
        private string Owner { get; set; }
        protected int _fuel;
        protected int _rpm;

        public void Accelerate()
        {
            _rpm += 1000;
            Burn(1);
        }

        public void Refuel()
        {
            _fuel = 100;
        }
        // protected 一般用在方法上 可以和 internal 组合
        protected void Burn(int fuel)
        {
            _fuel-= fuel;
        }
         
        public int Speed { get { return _rpm / 100; }  }
    }
    public class Car:Vehicle
    {
        public void turboAccelerate()
        {
            Burn(2);
            _rpm += 3000;
        }   
    }

}
