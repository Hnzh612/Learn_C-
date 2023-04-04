using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceImplementer
{

    interface IParentInterface
    {
        void ParentInterfaceMethod();
    }
    interface IMyInterface : IParentInterface
    {
        void MethodToImplement();
    }

    class InterfaceImplementer : IMyInterface
    {

        static void Main()
        {
            InterfaceImplementer iImp = new InterfaceImplementer();
            iImp.MethodToImplement();
            iImp.ParentInterfaceMethod();
        }
        public void MethodToImplement()
        {
            Console.WriteLine("MethodToImplement() called.");
        }

        public void ParentInterfaceMethod()
        {
            Console.WriteLine("ParentInterfaceMethod() called.");
        }
    }
}
