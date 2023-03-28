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

namespace WPFframework.Controls
{
    /// <summary>
    /// Button.xaml 的交互逻辑
    /// </summary>
    public partial class Button : Window
    {
        public Button()
        {
            InitializeComponent();
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            MessageBox.Show("午时已到");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("精准射击");
        }
    }
}
