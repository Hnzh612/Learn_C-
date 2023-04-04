using System.Security.Cryptography.X509Certificates;

delegate int NumberChanger(int n);
namespace DelegateApplication
{
    class Program
    {
        public delegate void GreetingDelegate(string name);
        static int num = 10;
        public static int AddNum(int p)
        {
            num += p;
            return num;
        }

        public static int MultNum(int q)
        {
            num *= q;
            return num;
        }
        public static int getNum() 
        { 
            return num; 
        }


        static void Main(string[] args)
        {
            NumberChanger nc;
            NumberChanger nc1 = new NumberChanger(AddNum);
            NumberChanger nc2 = new NumberChanger(MultNum);

            //nc1(24);
            //Console.WriteLine($"Value of Num: {getNum()}");
            //nc2(5);
            //Console.WriteLine($"Value of Num: {getNum()}");

            //nc = nc1;
            //nc += nc2;

            //// 调用多播
            //nc(5);
            //Console.WriteLine($"Value of Num: {getNum()}");


            Program p = new Program();
            GreetingDelegate gd = new GreetingDelegate(EnglishGreeting);
            Greeting("xiaoYe",gd);

        }

        public static void Greeting(string name, GreetingDelegate callback)
        {
            callback(name);
        }
        public static void EnglishGreeting(string name)
        {
            Console.WriteLine("Hello, " + name);
        }

        public static void ChineseGreeting(string name)
        {
            Console.WriteLine("你好， " + name);
        }

        public static void GermanyGreeting(string name)
        {
            Console.WriteLine("Hallo， " + name);
        }
    }
}