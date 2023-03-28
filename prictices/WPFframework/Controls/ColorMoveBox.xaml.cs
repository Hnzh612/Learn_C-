using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// ColorMoveBox.xaml 的交互逻辑
    /// </summary>
    public partial class ColorMoveBox : Window
    {
        public ColorMoveBox()
        {
            InitializeComponent();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            // 首先判断按键的方位，比如上下左右
            Border preBox = MouseMoveColorChange();
            int index = Convert.ToInt32(preBox.Name.Replace("b", ""));
            if (e.Key == Key.Up) 
            {
                index = index - 3 >= 1? index - 3 : index;
            }
            else if (e.Key == Key.Down)
            {
                index = index + 3 <= 9 ? index + 3 : index;

            }
            else if (e.Key == Key.Right)
            {
                index = index + 1 <=9  ? index + 1 : index;
            }
            else if (e.Key == Key.Left)
            {
                index = index - 1 >= 1 ? index - 1 : index;
            }

            object nextBox =  gridContent.FindName("b"+index);

            if (nextBox != null && preBox != nextBox)
            {
                (nextBox as Border).Background = new SolidColorBrush(Colors.White);
                preBox.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void Bind_MouseMove(object sender, MouseEventArgs e)
        {
            Border preBox = MouseMoveColorChange();
            
            if ((e.MouseDevice.Target as Border).Name != preBox.Name)
            {
                (e.MouseDevice.Target as Border).Background = new SolidColorBrush(Colors.White);
                preBox.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private Border MouseMoveColorChange()
        {
            // 获取白色的 border 元素

            UIElementCollection children = gridContent.Children;
            Border curBorder = null;
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is Border && ((children[i] as Border).Background as SolidColorBrush).Color.Equals(Colors.White))
                {
                    curBorder = children[i] as Border;
                }
            }
            Border preBox = gridContent.FindName(curBorder.Name) as Border;
            return preBox;
        }
    }
}
