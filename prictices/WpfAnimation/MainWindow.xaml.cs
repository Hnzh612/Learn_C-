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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAnimation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 宽度动画
            //DoubleAnimation da = new DoubleAnimation();
            //da.From = 120; // 起始值
            //da.To = 300; // 结束值
            //da.Duration = TimeSpan.FromMilliseconds(2000);
            //da.RepeatBehavior = RepeatBehavior.Forever;
            //(sender as Button).BeginAnimation(WidthProperty, da);

            //代码层面没问题
        }
    }
}
