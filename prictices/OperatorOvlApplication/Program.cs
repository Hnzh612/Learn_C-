// See https://aka.ms/new-console-template for more information
using OperatorOvlApplication;

Box box1 = new Box();
Box box2 = new Box();
Box box3 = new Box();
Box box4 = new Box();
double volume = 0;

// box1 详情
box1.setLength(6.0);
box1.setWidth(7.0);
box1.setHeight(8.0);

// box2 详情
box2.setLength(3.0);
box2.setWidth(2.0);
box2.setHeight(1.0);

// box1 的体积
volume = box1.getVolume();
Console.WriteLine($"box1 的体积为：{volume}");

// box2 的体积
volume = box2.getVolume();
Console.WriteLine($"box2 的体积为：{volume}");

// box3 的体积
box3 = box1 + box2;
volume = box3.getVolume();
Console.WriteLine($"box3：{box3}");
Console.WriteLine($"box3 的体积为：{volume}");


bool status = false;
status = box1 != box2;
status = box1 == box2;

Console.WriteLine();
