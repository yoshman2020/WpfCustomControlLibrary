using WpfCustomControlLibrarySample;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfCustomControlLibrarySample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region propertyChanged

        /// <summary>
        /// プロパティ変更
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティ設定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

        #endregion

        #region NumericSlider

        #region Value

        private int numericSliderValue = 128;

        public int NumericSliderValue { get => numericSliderValue; set => SetProperty(ref numericSliderValue, value); }

        private double numericSliderValueD;

        public double NumericSliderValueD { get => numericSliderValueD; set => SetProperty(ref numericSliderValueD, value); }

        #endregion

        #region DragCompleted

        private RelayCommand numericSliderDragCompletedCommand;

        public ICommand NumericSliderDragCompletedCommand
        {
            get
            {
                if (numericSliderDragCompletedCommand == null)
                {
                    numericSliderDragCompletedCommand = new RelayCommand(NumericSliderDragCompleted);
                }

                return numericSliderDragCompletedCommand;
            }
        }

        private void NumericSliderDragCompleted(object commandParameter)
        {
        }

        #endregion

        #region TextChanged

        private RelayCommand numericSliderTextChangedCommand;

        public ICommand NumericSliderTextChangedCommand
        {
            get
            {
                if (numericSliderTextChangedCommand == null)
                {
                    numericSliderTextChangedCommand = new RelayCommand(NumericSliderTextChanged);
                }

                return numericSliderTextChangedCommand;
            }
        }

        private void NumericSliderTextChanged(object commandParameter)
        {
        }

        #endregion

        #region LostFocus

        private RelayCommand numericSliderLostFocusCommand;

        public ICommand NumericSliderLostFocusCommand
        {
            get
            {
                if (numericSliderLostFocusCommand == null)
                {
                    numericSliderLostFocusCommand = new RelayCommand(NumericSliderLostFocus);
                }

                return numericSliderLostFocusCommand;
            }
        }

        private void NumericSliderLostFocus(object commandParameter)
        {
        }

        #endregion

        #endregion

        #region NumericUpdown

        #region Value

        private int numericUpDownValue = 128;

        public int NumericUpDownValue { get => numericUpDownValue; set => SetProperty(ref numericUpDownValue, value); }

        #endregion

        #region TextChanged

        private RelayCommand numericUpdownTextChangedCommand;

        public ICommand NumericUpdownTextChangedCommand
        {
            get
            {
                if (numericUpdownTextChangedCommand == null)
                {
                    numericUpdownTextChangedCommand = new RelayCommand(NumericUpdownTextChanged);
                }

                return numericUpdownTextChangedCommand;
            }
        }

        private void NumericUpdownTextChanged(object commandParameter)
        {
        }

        #endregion

        #region LostFocus

        private RelayCommand numericUpdownLostFocusCommand;

        public ICommand NumericUpdownLostFocusCommand
        {
            get
            {
                if (numericUpdownLostFocusCommand == null)
                {
                    numericUpdownLostFocusCommand = new RelayCommand(NumericUpdownLostFocus);
                }

                return numericUpdownLostFocusCommand;
            }
        }

        private void NumericUpdownLostFocus(object commandParameter)
        {
        }

        #endregion

        #endregion

        #region RangeSlider

        #region LowerValue

        private int rangeSliderLowerValue = 100;

        public int RangeSliderLowerValue { get => rangeSliderLowerValue; set => SetProperty(ref rangeSliderLowerValue, value); }

        private double rangeSliderLowerValueD = 100;

        public double RangeSliderLowerValueD { get => rangeSliderLowerValueD; set => SetProperty(ref rangeSliderLowerValueD, value); }

        #endregion

        #region UpperValue

        private double rangeSliderUpperValue = 200;

        public double RangeSliderUpperValue { get => rangeSliderUpperValue; set => SetProperty(ref rangeSliderUpperValue, value); }

        private double rangeSliderUpperValueD = 200;

        public double RangeSliderUpperValueD { get => rangeSliderUpperValueD; set => SetProperty(ref rangeSliderUpperValueD, value); }

        #endregion

        #region DragCompleted

        private RelayCommand rangeSliderDragCompletedCommand;

        public ICommand RangeSliderDragCompletedCommand
        {
            get
            {
                if (rangeSliderDragCompletedCommand == null)
                {
                    rangeSliderDragCompletedCommand = new RelayCommand(RangeSliderDragCompleted);
                }

                return rangeSliderDragCompletedCommand;
            }
        }

        private void RangeSliderDragCompleted(object commandParameter)
        {
        }

        #endregion

        #region TextChanged

        private RelayCommand rangeSliderTextChangedCommand;

        public ICommand RangeSliderTextChangedCommand
        {
            get
            {
                if (rangeSliderTextChangedCommand == null)
                {
                    rangeSliderTextChangedCommand = new RelayCommand(RangeSliderTextChanged);
                }

                return rangeSliderTextChangedCommand;
            }
        }

        private void RangeSliderTextChanged(object commandParameter)
        {
        }

        #endregion

        #region LostFocus

        private RelayCommand rangeSliderLostFocusCommand;

        public ICommand RangeSliderLostFocusCommand
        {
            get
            {
                if (rangeSliderLostFocusCommand == null)
                {
                    rangeSliderLostFocusCommand = new RelayCommand(RangeSliderLostFocus);
                }

                return rangeSliderLostFocusCommand;
            }
        }

        private void RangeSliderLostFocus(object commandParameter)
        {
        }

        #endregion

        #endregion

    }
}
