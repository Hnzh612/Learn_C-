// See https://aka.ms/new-console-template for more information

using FaceToObj;
using MultipleInheritance;

//FaceToObj.Rectangle rect = new FaceToObj.Rectangle();
//rect.setWidth(5);
//rect.setHeight(5);

//Console.WriteLine(rect.getArea());

MultipleInheritance.Rectangle rec = new MultipleInheritance.Rectangle();
rec.setWidth(5);
rec.setHeight(5);

Console.WriteLine($"{ rec.getArea()} + { rec.getCost(rec.getArea())}");