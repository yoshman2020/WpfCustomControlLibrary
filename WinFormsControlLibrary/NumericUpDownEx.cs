using System;
using System.Windows.Forms;

namespace WinFormsControlLibrary
{
    public partial class NumericUpDownEx : NumericUpDown
    {
        public NumericUpDownEx()
        {
            InitializeComponent();
        }

        public event EventHandler UpDownButtonClicked;

        public override void UpButton()
        {
            base.UpButton();
            UpDownButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public override void DownButton()
        {
            base.DownButton();
            UpDownButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
