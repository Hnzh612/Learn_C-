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
using System.Windows.Shapes;
using WPFframework.Entity;

namespace WPFframework.Controls
{
    /// <summary>
    /// DataBinding.xaml 的交互逻辑
    /// </summary>
    public partial class DataBinding : Window
    {
        public DataBinding()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 可以将数据上下文绑定到需要的元素或者父元素上
            // 子元素的数据优先级比父元素的高
            MyGrid.DataContext = new MyData() { Title = "午时已到"};
            lab1.DataContext = new MyData() { Title = "该吃饭了" };
        }
    }
}
