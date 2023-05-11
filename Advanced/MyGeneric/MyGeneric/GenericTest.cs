using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    public class GenericTest
    {
        /// <summary>
        /// 泛型方法，为了一个方法能满足不同类型的需求
        /// 一个方法完成多个实体的查询
        /// 一个方法完成不同的类型的数据展示
        /// 任意一个实体，转换成一个JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tParameter"></param>
        public void Show<T>(T tParameter)
        {

        }
    }
}
