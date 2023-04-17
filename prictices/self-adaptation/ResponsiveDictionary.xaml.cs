using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace self_adaptation
{
    public partial class ResponsiveDictionary
    {
        public void CloseSystem(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否关闭系统", "询问", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }
    }
}
