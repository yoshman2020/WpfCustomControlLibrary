using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfCustomControlLibrary.Converter
{
    /// <summary>
    /// 数値を文字列に変換するコンバータ
    /// </summary>
    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleToStringConverter : IValueConverter
    {
        /// <summary>
        /// 数値を文字列に変換
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        /// <summary>
        /// 文字列を数値に変換
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse(value.ToString(), out var doubleValue))
            {
                return 0;
            }
            return doubleValue;
        }
    }
}
