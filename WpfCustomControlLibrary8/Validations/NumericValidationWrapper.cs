using System.Windows;

namespace WpfCustomControlLibrary.Validations
{
    /// <summary>
    /// 数値バリデーションラッパークラス
    /// </summary>
    public class NumericValidationWrapper : DependencyObject
    {
        /// <summary>
        /// 最小値
        /// </summary>
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// 最小値
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum", typeof(double), typeof(NumericValidationWrapper));

        /// <summary>
        /// 最大値
        /// </summary>
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// 最大値
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum", typeof(double), typeof(NumericValidationWrapper));
    }
}
