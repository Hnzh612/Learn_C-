// See https://aka.ms/new-console-template for more information

int[] ar;
ar = new int[] { 1, 2, 3 };

string name = "H,n,z,h,h";

string[] strArray = name.Split(",");
foreach (string str in strArray)
{
    Console.Write(str+ " ");
}