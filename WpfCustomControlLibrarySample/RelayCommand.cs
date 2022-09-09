using System;
using System.Windows.Input;

namespace WpfCustomControlLibrarySample
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// アクション
        /// </summary>
        private readonly Action<object> _action;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action<object> action)
        {
            _action = action;
        }

        #region ICommand Members

        /// <summary>
        /// 実行可能
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// 実行可能状態変更
        /// </summary>
#pragma warning disable CS0067 // イベントは使用されていません
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067 // イベントは使用されていません

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _action(parameter);
            }
        }

        #endregion
    }
}
