using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace self_adaptation
{
    public static class GetChildren
    {
        public static IList<Control> GetControls(this DependencyObject parent)
        {
            var result = new List<Control>();
            for (int x = 0; x < VisualTreeHelper.GetChildrenCount(parent); x++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, x);
                var instance = child as Control;

                if (null != instance)
                    result.Add(instance);

                result.AddRange(child.GetControls());
            }
            return result;
        }
    }
}
