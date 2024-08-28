using System.Windows.Data;

namespace WpfCustomControlLibrary.Converters
{
    /// <summary>
    /// フォントサイズ変換
    /// </summary>
    /// <remarks>
    /// <example>
    /// <code>
    /// FontSize="{Binding Path=ActualHeight,
    /// RelativeSource={RelativeSource Mode = FindAncestor,
    ///     AncestorType = Grid},
    /// Converter ={StaticResource FontSizeConverter}}"
    /// </code>
    /// </example>
    /// </remarks>
    public class FontSizeConverter : IValueConverter
    {
        /// <summary>
        /// フォントサイズ変換
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            double actualHeight = System.Convert.ToDouble(value);
            if (actualHeight == 0)
            {
                return 12;
            }
            int fontSize = (int)(actualHeight * .5);
            if (int.TryParse((string)parameter, out var rowCount)
                && rowCount != 0)
            {
                fontSize /= rowCount;
            }
            return fontSize;
        }

        /// <summary>
        /// フォントサイズ変換
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
