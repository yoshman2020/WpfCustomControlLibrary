using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCustomControlLibrary
{
    /// <summary>
    /// スピン付き数値入力
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NumericUpDown()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数値
        /// </summary>
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// 数値
        /// </summary>
        public static DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", typeof(string), typeof(NumericUpDown),
                new PropertyMetadata("0"));

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
        public static DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum", typeof(double), typeof(NumericUpDown));

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
        public static DependencyProperty MaximumProperty =
            DependencyProperty.Register(
                "Maximum", typeof(double), typeof(NumericUpDown));

        /// <summary>
        /// 文字列フォーマット
        /// </summary>
        public string StringFormat
        {
            get => (string)GetValue(StringFormatProperty);
            set => SetValue(StringFormatProperty, value);
        }

        /// <summary>
        /// 文字列フォーマット
        /// </summary>
        public static DependencyProperty StringFormatProperty =
            DependencyProperty.Register(
                "StringFormat", typeof(string), typeof(NumericUpDown),
                new PropertyMetadata("{0:0}"));

        /// <summary>
        /// 増加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdUp_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(Value, out var doubleValue))
            {
                return;
            }
            if (doubleValue <= Maximum - 1)
            {
                doubleValue++;
            }
            else
            {
                doubleValue = Maximum;
            }
            Value = doubleValue.ToString();
            LostFocusCommand?.Execute(Value);
        }

        /// <summary>
        /// 減少
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdDown_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(Value, out var doubleValue))
            {
                return;
            }
            if (Minimum + 1 <= doubleValue)
            {
                doubleValue--;
            }
            else
            {
                doubleValue = Minimum;
            }
            Value = doubleValue.ToString();
            LostFocusCommand?.Execute(Value);
        }

        #region TextChanged

        /// <summary>
        /// テキスト変更
        /// </summary>
        private ICommand TextChangedCommand;

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.RegisterAttached("TextChanged",
                typeof(ICommand),
                typeof(NumericUpDown),
                new UIPropertyMetadata(null, OnTextChanged));

        /// <summary>
        /// テキスト変更
        /// </summary>
        public ICommand TextChanged
        {
            get => (ICommand)GetValue(TextChangedProperty);
            set => SetValue(TextChangedProperty, value);
        }

        /// <summary>
        /// テキスト変更
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTextChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is NumericUpDown uc)) return;
            if (e.NewValue is ICommand) uc.TextChangedCommand =
                    e.NewValue as ICommand;
        }

        /// <summary>
        /// テキスト変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextChangedCommand?.Execute(((TextBox)sender).Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region LostFocus

        /// <summary>
        /// フォーカス喪失
        /// </summary>
        private ICommand LostFocusCommand;

        public static readonly DependencyProperty LostFocusProperty =
            DependencyProperty.RegisterAttached("LostFocus",
                typeof(ICommand),
                typeof(NumericUpDown),
                new UIPropertyMetadata(null, OnLostFocus));

        /// <summary>
        /// フォーカス喪失
        /// </summary>
        public new ICommand LostFocus
        {
            get => (ICommand)GetValue(LostFocusProperty);
            set => SetValue(LostFocusProperty, value);
        }

        /// <summary>
        /// フォーカス喪失
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnLostFocus(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is NumericUpDown uc)) return;
            if (e.NewValue is ICommand) uc.LostFocusCommand =
                    e.NewValue as ICommand;
        }

        /// <summary>
        /// フォーカス喪失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextNum_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.TryParse((sender as TextBox).Text, out var doubleValue))
                {
                    Value = doubleValue.ToString();
                }
                LostFocusCommand?.Execute(((TextBox)sender).Text);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
