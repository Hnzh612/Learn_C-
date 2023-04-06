using AttributeApplication.Attributes;
using System.Reflection;

namespace AttributeApplication
{
    class Program
    {
        // 特性：
        // 一、特性的简介--使用场景（框架上、类上面、方法上、属性上、字段上、参数上）、
        // 特性的本质是类，继承自 Attribute 类
        // 二、特性简单的定义、查找、使用
        // 三、常用的特性验证类编写+自动特性验证
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle(4.5, 7.5);
            r.Display();

            Type type = typeof(Rectangle);
            foreach (object attributes in type.GetCustomAttributes(false))
            {
                DeBugInfo dbi = (DeBugInfo)attributes;
                if(null != dbi)
                {
                    Console.WriteLine($"BugNo：{dbi.BugNo}");
                    Console.WriteLine($"Developer：{dbi.Developer}");
                    Console.WriteLine($"Last Reviewed：{dbi.LastReview}");
                    Console.WriteLine($"Remarks：{dbi.message}");
                }
            }

            foreach( MethodInfo m in type.GetMethods())
            {
                if (m.Module.Name.Contains("AttributeApplication.dll"))
                {
                    foreach (Attribute attribute in m.GetCustomAttributes(true))
                    {
                        DeBugInfo dbi = (DeBugInfo)attribute;
                        if (null != dbi)
                        {
                            Console.WriteLine($"Bug No：{dbi.BugNo}，for Method：{m.Name}");
                            Console.WriteLine($"Developer：{dbi.Developer}");
                            Console.WriteLine($"Last Reviewed：{dbi.LastReview}");
                            Console.WriteLine($"Remarks：{dbi.Message}");
                        }
                    }
                }

            }
        }
    }
}