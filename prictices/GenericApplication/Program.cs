namespace GenericApplication
{
    /*
     使用泛型是一种增强程序功能的计数，具体表现在以下几个方面：
        它有助于最大限度地重用代码、保护类型的安全以及提高性能。
        可以创建自己的泛型接口、泛型类、泛型方法、泛型事件和泛型委托
     */


    delegate T NumberChanger<T>(T n);
    class Program
    {
        static void Main(string[] args)
        {
            //MyGennericArray<int> myInt = new MyGennericArray<int> (5);
            //for(int i = 0;i < 5; i++) 
            //{
            //    myInt.setItem(i, i*2);
            //}
            //for(int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine(myInt.getItem(i));
            //}

            //MyGennericArray<char> myString = new MyGennericArray<char> (5);
            //for(int i =0; i < 5; i++)
            //{
            //    myString.setItem(i, (char)(i+97));
            //}
            //for(int i = 0;i < 5; i++)
            //{
            //    Console.WriteLine(myString.getItem(i));
            //}


            // 泛型方法
            //int a, b;
            //char c, d;
            //a = 10;
            //b = 20;
            //c = 'i';
            //d = 'v';

            //Console.WriteLine($"a={a},b={b},c={c},d={d}");

            //MyGenericMethod.Swap<int>(ref a, ref b);
            //MyGenericMethod.Swap<char>(ref c, ref d);

            //Console.WriteLine($"a={a},b={b},c={c},d={d}");

            // 泛型委托
            NumberChanger<int> nc1 = new NumberChanger<int>(MyGenericDelegate.AddNum);
            NumberChanger<int> nc2 = new NumberChanger<int> (MyGenericDelegate.MulNum);

            nc1(5);
            nc2(10);
            Console.WriteLine(MyGenericDelegate.getNum());

        }
    }
}