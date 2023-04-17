using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Binding
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Book book1 = new Book();
            Book book2 = new Book();
            book1.Name = "学习新思想";
            book1.Author = "Hnzh";
            book2.Name = "争做新青年";
            book2.Author = "Zq";
            this.DataContext = book2;
        }
    }
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }
}
