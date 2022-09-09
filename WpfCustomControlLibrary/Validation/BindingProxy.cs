using System.Windows;

namespace WpfCustomControlLibrary.Validation
{
    /// <summary>
    /// バインド用プロキシ
    /// </summary>
    public class BindingProxy : Freezable
    {
        /// <summary>
        /// インスタンス作成
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        /// <summary>
        /// データ
        /// </summary>
        public object Data
        {
            get => (object)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        /// <summary>
        /// データ
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object),
                typeof(BindingProxy), new PropertyMetadata(null));
    }
}
