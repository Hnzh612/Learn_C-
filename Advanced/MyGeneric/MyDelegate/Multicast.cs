using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    public class Multicast
    {
        //static void Main(string[] args)
        //{
        //    Student student1 = new Student() { ID = 1,PenColor = ConsoleColor.Yellow };
        //    Student student2 = new Student() { ID = 2,PenColor = ConsoleColor.Gray };
        //    Student student3 = new Student() { ID = 3,PenColor = ConsoleColor.Blue };

        //    // 显式异步调用
        //    //Thread thread1 = new Thread(new ThreadStart(student1.DoHomewokr));
        //    //Thread thread2 = new Thread(new ThreadStart(student2.DoHomewokr));
        //    //Thread thread3 = new Thread(new ThreadStart(student3.DoHomewokr));

        //    //thread1.Start();
        //    //thread2.Start();
        //    //thread3.Start();

        //    Task task1 = new Task(new Action(student1.DoHomewokr));
        //    Task task2 = new Task(new Action(student2.DoHomewokr));
        //    Task task3 = new Task(new Action(student3.DoHomewokr));

        //    task1.Start();
        //    task2.Start();
        //    task3.Start();

        //    // 直接同步调用
        //    //student1.DoHomewokr();
        //    //student2.DoHomewokr();
        //    //student3.DoHomewokr();

        //    // 间接同步调用
        //    //Action action1 = new Action(student1.DoHomewokr);
        //    //Action action2 = new Action(student2.DoHomewokr);
        //    //Action action3 = new Action(student3.DoHomewokr);

        //    //action1.Invoke();
        //    //action2.Invoke();
        //    //action3.Invoke();

        //    //action1 += action2;
        //    //action1 += action3;
        //    //action1.Invoke();

        //    // 间接异步调用
        //    //action1.BeginInvoke(null,null);
        //    //action2.BeginInvoke(null,null);
        //    //action3.BeginInvoke(null,null);

        //    for (int i = 0; i < 10; i++)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        Console.WriteLine($"Main is sleep {i}");
        //        Thread.Sleep(500);
        //    }

        //}
    }
    class Student
    {
        public int ID { get; set; }
        public ConsoleColor PenColor { get; set; }
        public void DoHomewokr()
        {
            for(int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine($"Student {ID} doing homework {i} hour(S)");
                Thread.Sleep(500);
            }
        }
    }
}
