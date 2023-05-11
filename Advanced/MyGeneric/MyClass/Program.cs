using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib.MyNamespace;

namespace MyClass
{
    class Program
    {
        /* 什么是"类"
                是一种数据结构
                是一种数据类型
                代表现实世界中的"种类"
        */
        static void Main(string[] args)
        {
            //Caculator calc = new Caculator();
            //Console.WriteLine(calc.Add(1.22,2.33));
            Student stu; // 创建了变量
            Student student = new Student(1,"Hnzh"); // 创建了类的实例
            Student student1 = new Student(2, "nzh");
            Student student2 = new Student(3, "zh");
            Student student3 = new Student(4, "h");
            Console.WriteLine(Student.Amount);
        }
    }
    // C# 中类声明即定义
    class Student
    {
        public static int Amount { get; set; }
        // 静态构造器
        static Student()
        {
            Amount = 1;
        }
        // 构造函数
        public Student(int id, string name)
        {
            ID = id;
            Name = name;
            Student.Amount++;
        }
        // 析构函数
        ~Student() 
        {
            Amount--;
            Console.WriteLine("Bye bye!Release the system resources..."); 
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public void Report()
        {
            Console.WriteLine($"I'm #{ID} student,my name is {Name}");
        }
        // 成员类
        class Computer
        {

        }
    }
}

