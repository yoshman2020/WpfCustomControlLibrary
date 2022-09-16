using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCustomControlLibrary
{
    /// <summary>
    /// 範囲スライダー
    /// </summary>
    public partial class RangeSlider : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RangeSlider()
        {
            InitializeComponent();

            this.Loaded += Slider_Loaded;
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Slider_Loaded(object sender, RoutedEventArgs e)
        {
            LowerSlider.ValueChanged += LowerSlider_ValueChanged;
            UpperSlider.ValueChanged += UpperSlider_ValueChanged;
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
        public static DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text", typeof(string), typeof(RangeSlider),
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
        public static DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register(
                "LabelFontSize", typeof(double), typeof(RangeSlider),
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
        public static DependencyProperty LabelWidthProperty =
            DependencyProperty.Register(
                "LabelWidth", typeof(GridLength), typeof(RangeSlider),
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
        public static DependencyProperty SliderWidthProperty =
            DependencyProperty.Register(
                "SliderWidth", typeof(GridLength), typeof(RangeSlider),
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
        public static DependencyProperty NumericWidthProperty =
            DependencyProperty.Register(
                "NumericWidth", typeof(GridLength), typeof(RangeSlider),
                new PropertyMetadata(new GridLength(1, GridUnitType.Star)));

        /// <summary>
        /// 最小値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LowerSlider_ValueChanged(
            object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpperSlider.Value = Math.Max(UpperSlider.Value, LowerSlider.Value);
        }

        /// <summary>
        /// 最大値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpperSlider_ValueChanged(
            object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LowerSlider.Value = Math.Min(UpperSlider.Value, LowerSlider.Value);
        }

        /// <summary>
        /// スライダーの最大値
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// スライダーの最大値
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double),
                typeof(RangeSlider), new UIPropertyMetadata(0d));

        /// <summary>
        /// スライダーの最大値
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// スライダーの最大値
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(double),
                typeof(RangeSlider), new UIPropertyMetadata(1d));

        /// <summary>
        /// 最小値
        /// </summary>
        public double LowerValue
        {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        /// <summary>
        /// 最小値
        /// </summary>
        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double),
                typeof(RangeSlider), new UIPropertyMetadata(0d));

        /// <summary>
        /// 最大値
        /// </summary>
        public double UpperValue
        {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        /// <summary>
        /// 最大値
        /// </summary>
        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(double),
                typeof(RangeSlider), new UIPropertyMetadata(0d));

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
        public static DependencyProperty MinMaxFontSizeProperty =
            DependencyProperty.Register(
                "MinMaxFontSize", typeof(double), typeof(RangeSlider),
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
        public static DependencyProperty StringFormatProperty =
            DependencyProperty.Register(
                "StringFormat", typeof(string), typeof(RangeSlider),
                new PropertyMetadata("{0:0}"));

        #region DragCompleted

        /// <summary>
        /// スライダードラッグ完了
        /// </summary>
        private ICommand DragCompletedCommand;

        /// <summary>
        /// スライダードラッグ完了
        /// </summary>
        public static readonly DependencyProperty DragCompletedProperty =
             DependencyProperty.RegisterAttached("DragCompleted",
             typeof(ICommand),
             typeof(RangeSlider),
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
            if (!(d is RangeSlider uc)) return;
            if (e.NewValue is ICommand) uc.DragCompletedCommand =
                    e.NewValue as ICommand;
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
        private ICommand TextChangedCommand;

        public static readonly DependencyProperty TextChangedProperty =
            DependencyProperty.RegisterAttached("TextChanged",
                typeof(ICommand),
                typeof(RangeSlider),
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
            if (!(d is RangeSlider uc)) return;
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
                typeof(RangeSlider),
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
            if (!(d is RangeSlider uc)) return;
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
