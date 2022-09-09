using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfCustomControlLibrary.Converter
{
    /// <summary>
    /// 列挙型をBooleanに変換するコンバータ
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// 列挙型をBooleanに変換
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(parameter is string parameterString))
            {
                return DependencyProperty.UnsetValue;
            }

            if (!Enum.IsDefined(value.GetType(), value))
            {
                return DependencyProperty.UnsetValue;
            }

            var parameterValue = Enum.Parse(value.GetType(), parameterString);
            return parameterValue.Equals(value);
        }

        /// <summary>
        /// Booleanを列挙型に変換
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (!(parameter is string parameterString))
            {
                return DependencyProperty.UnsetValue;
            }

            if (true.Equals(value))
            {
                return Enum.Parse(targetType, parameterString);
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
