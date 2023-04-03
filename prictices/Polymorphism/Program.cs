// See https://aka.ms/new-console-template for more information
using Polymorphism;

// 创建一个 List<Shape> 对象，并向该对象添加 Circle、Triangle 和 Rectangle
var shapes = new List<Shape>
        {
            new Rectangle(),
            new Triangle(),
            new Circle()
        };

// 使用 foreach 循环对该列表的派生类进行循环访问，并对其中的每个 Shape 对象调用 Draw 方法
foreach (var shape in shapes)
{
    shape.Draw();
}

Console.WriteLine("按下任意键退出。");
Console.ReadKey();