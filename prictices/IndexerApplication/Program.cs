﻿namespace IndexerApplication
{
    class IndexedNames
    {
        static public int size = 10;
        private string[] namelist = new string[size];
        public IndexedNames()
        {
            for(int i = 0; i < size; i++)
            {
                namelist[i] = "N.A";
            }
        }
        public string this[int index]
        {
            get
            {
                string temp;
                if(index >=0 && index < size)
                {
                    temp = namelist[index];
                }
                else
                {
                    temp = "";
                }
                return temp;
            }
            set
            {
                if( index >= 0 && index < size )
                    namelist[index] = value;
            }
        }
        public int this[string name]
        {
            get
            {
                int index = 0;
                while(index < size )
                {
                    if (namelist[index] == name)
                    {
                        return index;
                    }
                    index++;
                }
                return index;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IndexedNames names = new IndexedNames();
            names[0] = "Zara";
            names[1] = "Riz";
            names[2] = "Nuha";
            names[3] = "Asif";
            names[4] = "Davinder";
            names[5] = "Sunil";
            names[6] = "Rubic";
            for (int i = 0; i < IndexedNames.size; i++) 
            {
                Console.WriteLine(names[i]);
            }
            Console.WriteLine(names["Riz"]);
        }
    }
}