// See https://aka.ms/new-console-template for more information

using prictices;

double a = 28600000;
double b = 472;
double c = 1060;
double sum = a+b+c;

Dictionary<string,double> map = new Dictionary<string,double>();
map.Add("HPV16", a);
map.Add("HPV18", b);
map.Add("HPV32", c);
Capacity.VirusMap = map;
Capacity.allExpotend = sum;
Dictionary<string,double> capacity =  Capacity.CalPercentage();
Console.WriteLine(capacity["HPV16"].ToString("P"));
Console.WriteLine(capacity["HPV18"].ToString("P"));
Console.WriteLine(capacity["HPV32"].ToString("P"));

