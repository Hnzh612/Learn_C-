using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    public class CommonMethod
    {
        /// <summary>
        /// V1.0 object 方法
        /// 1 任何父类出现的地方，都可以用子类代替
        /// 2 object 是一切类型的父类
        /// 
        /// 两个问题：
        /// 1 装箱拆箱 会造成新能损耗
        /// 2 类型安全问题，可能会有，因为传值的对象是没有限制的
        /// </summary>
        /// <param name="oParameter"></param>
        public static void ShowObject(object oParameter)
        {
            Console.WriteLine(oParameter.ToString());
        }

        /// <summary>
        /// V2.0 泛型方法 方法名称后面加上尖括号，里面是类型参数
        /// 
        /// 泛型为什么也可以支持多种不同类型的参数？
        /// 泛型声明方法时，并没有写死类型
        /// T要等着调用的时候才指定
        /// 设计思想--延迟声明，推迟一切可以推迟的。
        /// 
        /// 泛型不是语法糖，是编译器框架提供的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tParameter"></param>
        public static void Show<T>(T tParameter)
        {
            Console.WriteLine(tParameter.ToString());
        }
    }
}
