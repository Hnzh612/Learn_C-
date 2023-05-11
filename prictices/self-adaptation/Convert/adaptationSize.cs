using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace self_adaptation
{
    public class adaptationHeightSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double psh = SystemParameters.PrimaryScreenHeight;

            double sizeMultiplier = psh / 1080;
            double backdata = System.Convert.ToDouble(value.ToString()) * sizeMultiplier;
            return backdata;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class adaptationWidthSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           double psw = SystemParameters.PrimaryScreenWidth;

            double sizeMultiplier = psw / 1920;
            double backdata = System.Convert.ToDouble(value.ToString()) * sizeMultiplier;
            return backdata;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class adaptationMarginSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double psw = SystemParameters.PrimaryScreenWidth;
            double psh = SystemParameters.PrimaryScreenHeight;

            double widthMultiplier = psw / 1920;
            double heightMultiplier = psh / 1080;

            Thickness margin = (Thickness)value;
            //margin.Top = -(margin.Top - margin.Top*heightMultiplier);
            //margin.Right = -(margin.Right - margin.Right * widthMultiplier);
            //margin.Bottom = -(margin.Bottom - margin.Bottom * heightMultiplier);
            //margin.Left = -(margin.Left - margin.Left * widthMultiplier);
            margin.Top = -(margin.Top - margin.Top * heightMultiplier);
            margin.Right = -(margin.Right - margin.Right * widthMultiplier);
            margin.Bottom = -(margin.Bottom - margin.Bottom * heightMultiplier);
            margin.Left = -(margin.Left - margin.Left * widthMultiplier);
            return margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
