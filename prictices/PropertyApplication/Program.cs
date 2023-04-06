namespace PropertyApplication
{

    class Student
    {
        private string code = "N.A";
        private string name = "not known";
        private int age = 0;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public override string ToString()
        {
            return $"Student Info: Code = {Code} , Name = {Name} ,Age = {Age}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
           Student s = new Student();

            s.Code = "001";
            s.Name = "Hnzh";
            s.Age = 25;

            Console.WriteLine(s.ToString());
        }
    }
}