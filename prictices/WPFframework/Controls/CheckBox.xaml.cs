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
    /// CheckBox_.xaml 的交互逻辑
    /// </summary>
    public partial class CheckBox_ : Window
    {
        public CheckBox_()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UIElementCollection children = Lesson.Children;
            StringBuilder sb = new StringBuilder("我的选课为：");

            foreach (UIElement item in children)
            {
                if (item is CheckBox && (item as CheckBox).IsChecked.Value)
                {
                    sb.Append((item as CheckBox).Content + ",");
                }
            }
            MessageBox.Show(sb.ToString());
        }
    }
}
