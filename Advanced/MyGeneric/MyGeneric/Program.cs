using MyGeneric;

Console.WriteLine(typeof(List<>));
Console.WriteLine(typeof(Dictionary<,>));

int i = 0;
string s = "123";
object o = 321;

Console.WriteLine("*************************************");
CommonMethod.ShowObject(i);
CommonMethod.ShowObject(s);
CommonMethod.ShowObject(o);

Console.WriteLine("*************************************");
CommonMethod.Show(i); // 如果可以从参数类型推断，可以省略类型参数---语法糖（编译器提供的功能）
CommonMethod.Show(s);
CommonMethod.Show(o);

Console.WriteLine("*************************************");


