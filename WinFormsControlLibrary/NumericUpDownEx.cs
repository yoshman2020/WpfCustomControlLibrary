using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinFormsControlLibrary
{
    public partial class NumericUpDownEx : NumericUpDown
    {
        /// <summary>
        /// NumericUpDown拡張クラス
        /// </summary>
        public NumericUpDownEx()
        {
            InitializeComponent();
        }

        /// <summary>
        /// UpDownボタン押下時のイベント
        /// </summary>
        [Category("Action")]
        [Description("UpDownボタンクリック時に発生します。")]
        public event EventHandler UpDownButtonClicked;

        /// <summary>
        /// Upボタン押下
        /// </summary>
        public override void UpButton()
        {
            base.UpButton();
            UpDownButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Downボタン押下
        /// </summary>
        public override void DownButton()
        {
            base.DownButton();
            UpDownButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
