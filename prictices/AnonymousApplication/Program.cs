namespace AnonymousApplication
{
    delegate void NumberChanger(int i);
    internal class Program
    {
        static int num = 10;
        public static void AddNum(int p)
        {
            num += p;
            Console.WriteLine("Named Method:{0}",num);
        }
        public static void MulNum(int p)
        {
            num *= p;
            Console.WriteLine("Named Method:{0}", num);
        }
        static void Main(string[] args)
        {
            NumberChanger nc = delegate (int i)
            {
                Console.WriteLine("Anonymous Method:{0}", i);
            };

            NumberChanger nc1 = new NumberChanger(AddNum);
            NumberChanger nc2 = new NumberChanger(MulNum);
            nc(5);
            nc1(10);
            nc2(2);
        }
    }
}