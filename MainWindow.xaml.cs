using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Labs
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
    }

    public class SymbolColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            if (s == "X") return new SolidColorBrush(Color.FromRgb(0x4F, 0xC3, 0xF7));
            if (s == "O") return new SolidColorBrush(Color.FromRgb(0xEF, 0x9A, 0x9A));
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        { throw new NotImplementedException(); }
    }
}