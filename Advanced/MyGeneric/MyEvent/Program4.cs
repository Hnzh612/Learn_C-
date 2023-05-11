using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyEvent
{
    internal class Program4
    {
        /// <summary>
        /// 事件的声明
        ///     完整声明
        ///     简略声明（字段式声明）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            Waiter waiter = new Waiter();
            customer.Order += Waiter.Action;
            customer.Action();

            OrderEventArgs e = new OrderEventArgs();
            e.DishName = "ManHanQuanXi";
            e.size = "large";
            Customer badGuy = new Customer();
            badGuy.Order += Waiter.Action;
            //badGuy.Order.Invoke(customer,e);

            customer.PayTheBill();
        }
    }
    public class OrderEventArgs:EventArgs
    {
        public string DishName { get; set; }
        public string size { get; set; }
    }
    //public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);
    // 事件的拥有者
    public class Customer
    {
        // 完整声明
        //private OrderEventHandler orderEventHandler;

        //public event OrderEventHandler Order
        //{
        //    add
        //    {
        //        orderEventHandler += value;
        //    }
        //    remove
        //    {
        //        orderEventHandler -= value;
        //    }
        //}

        // 简略声明 像字段但是不是字段
        public event EventHandler Order; 
        public double Bill { get; set;}
        public void PayTheBill()
        {
            Console.WriteLine($"I will pay ${Bill}");
        }
        public void WalkIn()
        {
            Console.WriteLine("Walk into the restaurant.");
        }
        public void SitDown()
        {
            Console.WriteLine("Sit down.");
        }
        public void Think()
        {
            for(int i = 0;i < 5; i++)
            {
                Console.WriteLine("Let me think...");
                Thread.Sleep(1000);
            }
            this.OnOrder("Kongpao Chicken", "large");
        }
        // 事件触发 用On方法名
        protected void OnOrder(string dishName, string size)
        {
            //if(this.orderEventHandler != null)
            //{
            //    OrderEventArgs e = new OrderEventArgs();
            //    e.DishName = "Kongpao Chicken";
            //    e.size = "large";
            //    this.orderEventHandler.Invoke(this,e);
            //}
            if (this.Order != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = dishName;
                e.size = size;
                this.Order.Invoke(this, e);
            }
        }
        public void Action()
        {
            Console.ReadLine();
            this.WalkIn();
            this.SitDown();
            this.Think();
        }
    }

    // 事件的响应者
    public class Waiter
    {
        public static void Action(object sender, EventArgs e)
        {
            Customer customer = sender as Customer;
            OrderEventArgs orderinfo = e as OrderEventArgs;
            Console.WriteLine($"I will serve you the dish - {orderinfo.DishName}");
            double price = 10;
            switch(orderinfo.size)
            {
                case "small": 
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }
            customer.Bill += price;
        }
    }
}
