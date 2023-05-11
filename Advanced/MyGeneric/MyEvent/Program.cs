using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvent
{
    /* 初步了解事件
    * 定义：单词Event，译为事件
    * 角色：使对象或类具备通知能力的成员
    * 使用：用于对象或类间的动作协调与信息传递（消息推送）
    * 原理：事件模型（event model）中的两个“5”
    *      “发生->响应”中的五个部分----闹钟响了你起床、孩子饿了你做饭...这里隐含着“订阅”关系
    *      “发生->响应”中的五个动作----1、我有一个事件->2、一个人或者一群人关心我的这个事件->3、我的这个事件发生了
    *      ->4、关心这个事件的人会被依次通知到->5、被通知到的人根据拿到的事件信息对事件进行响应
    * 提示：
    *      事件多用于桌面、手机端等开发的客户端编程，因为这些程序经常是通过事件来“驱动”的
    *      各种编程语言对这个机制的实现方法不尽相同
    *      Java语言里没有事件这种成员，也没有委托这种数据类型。Java的“事件”是使用接口来实现的
    *      MVC、MVP、MVVM等模式，是事件模式更高级、更有效的玩法
    *      日常开发中，使用已知事件的机会比较多，自己声明事件的机会比较少，所以先学使用
    */
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    Timer timer = new Timer();
        //    timer.Interval = 1000;
        //    Boy boy = new Boy();
        //    Girl girl = new Girl();
        //    timer.Elapsed += boy.Action;
        //    timer.Elapsed += girl.Action;
        //    timer.Start();
        //    Console.ReadLine();
        //}
    }
    class Boy
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Jump");
        }
    }

    class Girl
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("sing");
        }
    }
}
