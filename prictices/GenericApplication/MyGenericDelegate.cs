using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericApplication
{
    class MyGenericDelegate
    {
        static int num = 10;
        public static int AddNum(int p)
        {
            num += p;
            return num;
        }
        public static int MulNum(int p)
        {
            num *= p; 
            return num;
        }
        public static int getNum()
        {
            return num;
        }
    }
}
