using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Example
    {
        static void Main(string[] args)
        {
            ProdctFactory prodctFactory = new ProdctFactory();
            WrapFactory wrapFactory = new WrapFactory();
            Logger logger = new Logger();

            Func<Product> func1 = new Func<Product>(prodctFactory.MakePizze);
            Func<Product> func2 = new Func<Product>(prodctFactory.MakeToyCar);
            Action<Product> log = new Action<Product> (logger.Log);

            Box box1 = wrapFactory.WarpProduct(func1, log);
            Box box2 = wrapFactory.WarpProduct(func2, log);

            Console.WriteLine(box1.Product.Name);
            Console.WriteLine(box2.Product.Name);
        }
    }
    class Logger
    {
        public void Log(Product product)
        {
            Console.WriteLine($"Product {product.Name}.Created at {DateTime.Now}.Price is {product.Price}");
        }
    }
    class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
    class Box
    {
        public Product Product { get; set; }
    }
    class WrapFactory
    {
        public Box WarpProduct(Func<Product> getProduct,Action<Product> logCallback)
        {
            Box box = new Box();
            Product product = getProduct();
            if(product.Price >= 50)
            {
                logCallback(product);
            }
            box.Product = product;
            return box;
        }
    }
    class ProdctFactory
    {
        public Product MakePizze()
        {
            Product product = new Product();
            product.Name = "Pizze";
            product.Price = 12;
            return product;
        }
        public Product MakeToyCar()
        {
            Product product = new Product();
            product.Name = "ToyCar";
            product.Price = 100;
            return product;
        }
    }
}
