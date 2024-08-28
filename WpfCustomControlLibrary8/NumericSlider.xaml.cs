using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCustomControlLibrary
{
    /// <summary>
    /// スライダー付き数値入力
    /// </summary>
    public partial class NumericSlider : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NumericSlider()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ラベル
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// ラベル
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text", typeof(string), typeof(NumericSlider),
                new PropertyMetadata("", TextPropertyChanged));

        /// <summary>
        /// ラベル変更時
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void TextPropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.ReadLocalValue(TextProperty) !=
                DependencyProperty.UnsetValue &&
                (string)d.GetValue(TextProperty) != "" &&
                d.ReadLocalValue(LabelWidthProperty) ==
                DependencyProperty.UnsetValue)
            {
                // テキストが設定されていて幅設定されていない場合
                d.SetValue(LabelWidthProperty,
                    new GridLength(2, GridUnitType.Star));
            }

        }

        /// <summary>
        /// フォントサイズ
        /// </summary>
        public double LabelFontSize
        {
            get => (double)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        /// <summary>
        /// フォントサイズ
        /// </summary>
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register(
                "LabelFontSize", typeof(double), typeof(NumericSlider),
                new PropertyMetadata(12.0, OnLabelFontSizePropertyChanged));

        /// <summary>
        /// フォントサイズ変更時
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnLabelFontSizePropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.ReadLocalValue(LabelFontSizeProperty) ==
                DependencyProperty.UnsetValue ||
                (double)d.GetValue(LabelFontSizeProperty) == 0)
            {
                // 未設定の場合はフォントサイズを継承
                d.SetValue(LabelFontSizeProperty, d.GetValue(FontSizeProperty));
            }
        }

        /// <summary>
        /// ラベルのGridの幅
        /// </summary>
        public GridLength LabelWidth
        {
            get => (GridLength)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        /// <summary>
        /// ラベルのGridの幅
        /// </summary>
        public static readonly DependencyProperty LabelWidthProperty =
            DependencyProperty.Register(
                "LabelWidth", typeof(GridLength), typeof(NumericSlider),
                new PropertyMetadata(new GridLength(0)));

        /// <summary>
        /// スライダーのGridの幅
        /// </summary>
        public GridLength SliderWidth
        {
            get => (GridLength)GetValue(SliderWidthProperty);
            set => SetValue(SliderWidthProperty, value);
        }

        /// <summary>
        /// スライダーのGridの幅
        /// </summary>
        public static readonly DependencyProperty SliderWidthProperty =
            DependencyProperty.Register(
                "SliderWidth", typeof(GridLength), typeof(NumericSlider),
                new PropertyMetadata(new GridLength(5, GridUnitType.Star)));

        /// <summary>
        /// 数値のGridの幅
        /// </summary>
        public GridLength NumericWidth
        {
            get => (GridLength)GetValue(NumericWidthProperty);
            set => SetValue(NumericWidthProperty, value);
        }

        /// <summary>
        /// 数値のGridの幅
        /// </summary>
        public static readonly DependencyProperty NumericWidthProperty =
            DependencyProperty.Register(
                "NumericWidth", typeof(GridLength), typeof(NumericSlider),
                new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

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
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value", typeof(string), typeof(NumericSlider),
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
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(
                "Minimum", typeof(double), typeof(NumericSlider));

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
                "Maximum", typeof(double), typeof(NumericSlider));

        /// <summary>
        /// 最小最大値フォントサイズ
        /// </summary>
        public double MinMaxFontSize
        {
            get => (double)GetValue(MinMaxFontSizeProperty);
            set => SetValue(MinMaxFontSizeProperty, value);
        }

        /// <summary>
        /// 最小最大値フォントサイズ
        /// </summary>
        public static readonly DependencyProperty MinMaxFontSizeProperty =
            DependencyProperty.Register(
                "MinMaxFontSize", typeof(double), typeof(NumericSlider),
                new PropertyMetadata(12.0, OnMinMaxFontSizePropertyChanged));

        /// <summary>
        /// 最小最大値フォントサイズ変更時
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnMinMaxFontSizePropertyChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.ReadLocalValue(MinMaxFontSizeProperty) ==
                DependencyProperty.UnsetValue ||
                (double)d.GetValue(MinMaxFontSizeProperty) == 0)
            {
                // 未設定の場合はフォントサイズを継承
                d.SetValue(MinMaxFontSizeProperty, d.GetValue(FontSizeProperty));
            }
        }

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
        public static readonly DependencyProperty StringFormatProperty =
            DependencyProperty.Register(
                "StringFormat", typeof(string), typeof(NumericSlider),
                new PropertyMetadata("{0:0}"));

        #region DragCompleted

        /// <summary>
        /// スライダードラッグ完了
        /// </summary>
        private ICommand? DragCompletedCommand;

        /// <summary>
        /// スライダードラッグ完了
        /// </summary>
        public static readonly DependencyProperty DragCompletedProperty =
             DependencyProperty.RegisterAttached("DragCompleted",
             typeof(ICommand),
             typeof(NumericSlider),
             new UIPropertyMetadata(null, OnDragCompletedChanged));

        /// <summary>
        /// スライダードラッグ完了
        /// </summary>
        public ICommand DragCompleted
        {
            get => (ICommand)GetValue(DragCompletedProperty);
            set => SetValue(DragCompletedProperty, value);
        }

        /// <summary>
        /// スライダードラッグ完了コマンド変更
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDragCompletedChanged(
            DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not NumericSlider uc) return;
            if (e.NewValue is ICommand command) uc.DragCompletedCommand =
                    command;
        }

        /// <summary>
        /// スライダードラッグ完了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_DragCompleted(
            object sender,
            System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            try
            {
                DragCompletedCommand?.Execute(((Slider)sender).Value);
            }
            catch (Exception)
            {
            }
        }

        #endregion DragCompleted

        #region TextChanged

        /// <summary>
        /// テキスト変更
        /// </summary>
        private ICommand? TextChangedCommand;

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.RegisterAttached("TextChanged",
                typeof(ICommand),
                typeof(NumericSlider),
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
            if (d is not NumericSlider uc) return;
            if (e.NewValue is ICommand command) uc.TextChangedCommand =
                    command;
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
        private ICommand? LostFocusCommand;

        public static readonly DependencyProperty LostFocusProperty =
            DependencyProperty.RegisterAttached("LostFocus",
                typeof(ICommand),
                typeof(NumericSlider),
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
            if (d is not NumericSlider uc) return;
            if (e.NewValue is ICommand command) uc.LostFocusCommand =
                    command;
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
