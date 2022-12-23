using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace WinFormsControlLibrary
{
    [DefaultEvent("ValueChangedEnd")]
    public partial class NumericTrack : UserControl
    {
        #region variables

        /// <summary>
        /// ロガー
        /// </summary>
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(
            MethodBase.GetCurrentMethod()?.DeclaringType);

        /// <summary>
        /// マウス押下中
        /// </summary>
        private bool _clicked;

        #endregion variables

        #region constructors

        /// <summary>
        /// トラックバーと数値入力のコントロール
        /// </summary>
        public NumericTrack()
        {
            InitializeComponent();

            numericUpDown1.UpDownButtonClicked += NumericUpDown1_UpDownButtonClicked;

            LabelText = "";
            Value = 0;
            Minimum = 0;
            Maximum = 100;
            Step = 1;
        }

        #endregion constructors

        #region delegates

        /// <summary>
        /// 値変更時のイベントハンドラ
        /// </summary>
        [Category("Action")]
        [Description("コントロールの値が変更するときに発生します。")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// ラックバースクロール終了時およびテキスト変更時のイベントハンドラ
        /// </summary>
        /// <remarks>
        /// スクロール時に毎回イベントを発生させたくない場合は
        /// ValueChangedではなくこちらを使用してください
        /// </remarks>
        [Category("Action")]
        [Description("トラックバースクロールが終了するときに発生します。")]
        public event EventHandler ValueChangedEnd;

        #endregion delegates

        #region events

        /// <summary>
        /// トラックバースクロール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            Logger.Debug("▽");

            try
            {
                Value = trackBar1.Value;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                Logger.Debug("△");
            }
        }

        /// <summary>
        /// トラックバーマウス押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            _clicked = true;
        }

        /// <summary>
        /// トラックバーマウス離す
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_clicked)
            {
                return;
            }

            _clicked = false;
            ValueChangedEnd?.Invoke(this, e);
        }

        /// <summary>
        /// NumericUpdown値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Logger.Debug("▽");

            try
            {
                Value = Convert.ToInt32(numericUpDown1.Value);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                Logger.Debug("△");
            }
        }

        /// <summary>
        /// NumericUpdownフォーカス喪失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumericUpDown1_Leave(object sender, EventArgs e)
        {
            Logger.Debug("▽");

            try
            {
                if (Value != numericUpDown1.Value)
                {
                    Value = Convert.ToInt32(numericUpDown1.Value);
                    ValueChangedEnd?.Invoke(this, e);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                Logger.Debug("△");
            }
        }

        private void NumericUpDown1_UpDownButtonClicked(object sender, EventArgs e)
        {
            Logger.Debug("▽");

            try
            {
                ValueChangedEnd?.Invoke(this, e);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                Logger.Debug("△");
            }
        }

        #endregion events

        #region properties

        /// <summary>
        /// テキスト
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("コントロールに関連付けられたテキストです。")]
        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
        }

        /// <summary>
        /// 値
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("コントロールの値です。")]
        public int Value
        {
            get => trackBar1.Value;
            set
            {
                trackBar1.Value = GetSteppedValue(value);
                numericUpDown1.Value = trackBar1.Value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 最小値
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("コントロールの最小値です。")]
        public int Minimum
        {
            get => trackBar1.Minimum;
            set
            {
                trackBar1.Minimum = value;
                numericUpDown1.Minimum = value;
                labelMin.Text = value.ToString();
                SetTickFrequency();
            }
        }

        /// <summary>
        /// 最大値
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(100)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("コントロールの最大値です。")]
        public int Maximum
        {
            get => trackBar1.Maximum;
            set
            {
                trackBar1.Maximum = value;
                numericUpDown1.Maximum = value;
                labelMax.Text = value.ToString();
                SetTickFrequency();
            }
        }

        /// <summary>
        /// ステップサイズ
        /// </summary>
        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(1)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("ステップサイズです。")]
        public int Step
        {
            get => trackBar1.SmallChange;
            set
            {
                Value = GetSteppedValue(Value);

                trackBar1.SmallChange = value;
                numericUpDown1.Increment = value;
                SetTickFrequency();
            }
        }

        #endregion properties

        #region methods

        /// <summary>
        /// 目盛間のデルタ値設定
        /// </summary>
        private void SetTickFrequency()
        {
            trackBar1.TickFrequency = Convert.ToInt32(
                Math.Round((double)(trackBar1.Maximum -
                trackBar1.Minimum) / 10));
        }

        /// <summary>
        /// ステップ単位の値取得
        /// </summary>
        /// <param name="value">計算前の値</param>
        /// <returns>ステップ単位の値</returns>
        private int GetSteppedValue(double value)
        {
            if (value < Minimum)
            {
                return Minimum;
            }
            if (Maximum < value)
            {
                value = Maximum;
            }
            return Convert.ToInt32(Math.Round(
                (double)(value - Minimum) / Step) * Step) + Minimum;
        }

        #endregion methods
    }
}
