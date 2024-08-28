using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace WpfCustomControlLibrary.Helpers
{
    /// <summary>
    /// 文字列フォーマットヘルパー
    /// </summary>
    /// <remarks>
    /// <code>
    /// <TextBlock 
    ///     local:StringFormatProperty.Format="{Binding FormatString}"
    ///     local:StringFormatProperty.Value="{Binding Value}"
    ///     Text="{Binding (local:StringFormatHelper.FormattedValue)}"
    /// />
    /// </code>
    /// </remarks>
    public partial class StringFormatHelper
    {
        #region Value

        /// <summary>
        /// 値
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.RegisterAttached(
            "Value", typeof(object), typeof(StringFormatHelper),
            new PropertyMetadata(null, OnValueChanged));

        /// <summary>
        /// 値変更
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private static void OnValueChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            RefreshFormattedValue(obj);
        }

        /// <summary>
        /// 値取得
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetValue(DependencyObject obj)
        {
            return obj.GetValue(ValueProperty);
        }

        /// <summary>
        /// 値設定
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="newValue"></param>
        public static void SetValue(DependencyObject obj, object newValue)
        {
            obj.SetValue(ValueProperty, newValue);
        }

        #endregion

        #region Format

        /// <summary>
        /// フォーマット
        /// </summary>
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.RegisterAttached(
            "Format", typeof(string), typeof(StringFormatHelper),
            new PropertyMetadata(null, OnFormatChanged));

        /// <summary>
        /// フォーマット変更
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private static void OnFormatChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            RefreshFormattedValue(obj);
        }

        /// <summary>
        /// フォーマット取得
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetFormat(DependencyObject obj)
        {
            return (string)obj.GetValue(FormatProperty);
        }

        /// <summary>
        /// フォーマット設定
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="newFormat"></param>
        public static void SetFormat(DependencyObject obj, string newFormat)
        {
            obj.SetValue(FormatProperty, newFormat);
        }

        #endregion

        #region FormattedValue

        /// <summary>
        /// フォーマット後の値
        /// </summary>
        public static readonly DependencyProperty FormattedValueProperty =
            DependencyProperty.RegisterAttached(
            "FormattedValue", typeof(string), typeof(StringFormatHelper),
            new PropertyMetadata(null, OnFormattedValueChanged));

        /// <summary>
        /// フォーマット後の値変更
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private static void OnFormattedValueChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            obj.SetValue(ValueProperty, GetFormattedValue(obj));
        }

        /// <summary>
        /// フォーマット後の値取得
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetFormattedValue(DependencyObject obj)
        {
            return (string)obj.GetValue(FormattedValueProperty);
        }

        /// <summary>
        /// フォーマット後の値設定
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="newFormattedValue"></param>
        public static void SetFormattedValue(
            DependencyObject obj, string newFormattedValue)
        {
            obj.SetValue(FormattedValueProperty, newFormattedValue);
        }

        #endregion

        /// <summary>
        /// フォーマットリフレッシュ
        /// </summary>
        /// <param name="obj"></param>
        private static void RefreshFormattedValue(DependencyObject obj)
        {
            var value = GetValue(obj);
            var format = GetFormat(obj);

            if (value == null)
            {
                return;
            }

            if (format != null)
            {
                if (!format.StartsWith("{0:"))
                {
                    format = string.Format("{{0:{0}}}", format);
                }

                if (MyRegex().IsMatch(format) &&
                    double.TryParse(value.ToString(), out double dValue))
                {
                    SetFormattedValue(obj, string.Format(format, dValue));
                }
                else
                {
                    SetFormattedValue(obj, string.Format(format, value));
                }
            }
            else
            {
                SetFormattedValue(
                    obj, value == null ? string.Empty : value.ToString() ?? "");
            }
        }

        [GeneratedRegex(@".*:[#0]*\.?[#0]*.*")]
        private static partial Regex MyRegex();
    }
}
